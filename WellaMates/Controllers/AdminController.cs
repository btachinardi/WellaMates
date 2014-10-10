using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BootstrapMvcSample.Controllers;
using Microsoft.WindowsAzure.StorageClient;
using WellaMates.Extensions;
using OfficeOpenXml;
using WebMatrix.WebData;
using WellaMates.DAL;
using WellaMates.Filters;
using WellaMates.Helpers;
using WellaMates.Models;

namespace WellaMates.Controllers
{
    [AuthorizeUser(Roles = "Admin")]
    public class AdminController : BootstrapBaseController
    {
        private PortalContext db = new PortalContext();
        private SimpleRoleProvider roles;
        private SimpleMembershipProvider membership;

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/

        public ActionResult ClearUsers()
        {
            var check = new List<UserProfile>();
            var usersCleared = 0;
            foreach (var userProfile in db.UserProfiles)
            {
                if (check.Any(c => c.UserName == userProfile.UserName))
                {
                    db.UserProfiles.Remove(userProfile);
                    usersCleared++;
                }
                else
                {
                    check.Add(userProfile);
                }
            }
            db.SaveChanges();
            Success("Users Cleared: " + usersCleared);
            return RedirectToAction("Index");
        }

        //
        // GET: /Admin/

        public ActionResult ClearRefunds()
        {
            db.Files.RemoveRange(db.Files);
            db.Refunds.RemoveRange(db.Refunds);
            db.SaveChanges();
            Success("Refunds were successfully cleared!");
            return RedirectToAction("Index");
        }

        public ActionResult GetRolesForUser(string username)
        {
            var rolesForUser = Roles.Provider.GetRolesForUser(username);
            Success(String.Format("User '{0}' has the specified roles: {1}", username, string.Join(",", rolesForUser)));
            return RedirectToAction("Index");
        }

        public ActionResult GetUsersInRole(string role)
        {
            var usersInRole = Roles.GetUsersInRole(role);
            Success(string.Format("Role '{0}' has the following users: {1}", role, string.Join(",", usersInRole)));
            return RedirectToAction("Index");
        }

        public ActionResult AddUserToRole(string role, string username)
        {
            Roles.AddUserToRole(username, role);
            var rolesForUser = Roles.Provider.GetRolesForUser(username);
            Success(String.Format("User '{0}' added to role '{1}'. Current roles are: {2}", username, role, string.Join(",", rolesForUser)));
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportImage(HttpPostedFileBase Image)
        {
            string path = @"D:\Temp\";

            if (Image == null)
            {
                Error("Failed to upload image");
            }
            else
            {
                var name = AzureBlobSA.UploadImage(AzureBlobSA.TEST_CONTAINER, Image.InputStream, Image.FileName, Image.ContentType);
                Success(String.Format("Got image {0} of type {1} and size {2} {3} {4}",
                    Image.FileName, Image.ContentType, Image.ContentLength, name,
                    HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + Url.Action("GetImage", new
                    {
                        id = AzureBlobSA.EncodeTo64(name),
                        sas = AzureBlobSA.GetSasUrl(AzureBlobSA.TEST_CONTAINER)
                    })));
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteDatabase(string password)
        {
            if (password == "38493614")
            {
                db.Database.Delete();
                db.SaveChanges();
                Success("Banco de Dados Deletado com Sucesso");
            }
            else
            {
                Error("Senha incorreta");
            }
            return RedirectToAction("Index");
        }

        public ActionResult BrowseImages()
        {
            IEnumerable<IListBlobItem> items = AzureBlobSA.GetContainerFiles(AzureBlobSA.TEST_CONTAINER);
            List<string> urls = new List<string>();
            foreach (var item in items)
            {
                if (item is CloudBlockBlob)
                {
                    var blob = (CloudBlockBlob)item;
                    urls.Add(blob.Name);
                }
            }
            ViewBag.BlobFiles = urls;
            ViewBag.Sas = AzureBlobSA.GetSasUrl(AzureBlobSA.TEST_CONTAINER);
            return View();
        }

        public ActionResult GetImage(string id, string sas)
        {
            var fileName = AzureBlobSA.DecodeFrom64(id);
            return new RedirectResult(AzureBlobSA.GetSasBlobUrl(AzureBlobSA.TEST_CONTAINER, fileName, sas));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportReportTemplate(HttpPostedFileBase Excel)
        {
            var httpPostedFileBase = Excel;
            if (httpPostedFileBase != null && httpPostedFileBase.ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(httpPostedFileBase.FileName);
                var targetFolder = Server.MapPath("~/Content/uploaded/templates");
                if (!System.IO.Directory.Exists(targetFolder))
                {
                    System.IO.Directory.CreateDirectory(targetFolder);
                }
                string path = string.Format("{0}/{1}", targetFolder, "BasicReportTemplate.xlsx");
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                httpPostedFileBase.SaveAs(path);
            }
            Success("Successfully imported excel template!");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportExcell(HttpPostedFileBase Excell)
        {
            var httpPostedFileBase = Excell;
            if (httpPostedFileBase != null && httpPostedFileBase.ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(httpPostedFileBase.FileName);
                var targetFolder = Server.MapPath("~/Content/uploaded/excell");
                if (!System.IO.Directory.Exists(targetFolder))
                {
                    System.IO.Directory.CreateDirectory(targetFolder);
                }
                string path = string.Format("{0}/{1}", targetFolder,
                                            DateTime.Now.GetTimestamp() + "_" + httpPostedFileBase.FileName);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                httpPostedFileBase.SaveAs(path);

                string filePath = path;
                var existingFile = new FileInfo(filePath);

                // Open and read the XlSX file.
                using (var package = new ExcelPackage(existingFile))
                {
                    roles = (SimpleRoleProvider) Roles.Provider;
                    membership = (SimpleMembershipProvider) Membership.Provider;
                    try
                    {
                        // Get the work book in the file
                        ExcelWorkbook workBook = package.Workbook;
                        if (workBook != null)
                        {
                            var helper = new ExcelHelper(workBook, db);
                            helper.ProcessRefundAdministrator();
                            helper.ProcessManager();
                            helper.ProcessFreelancer();
                            helper.ProcessVisualization();
                            helper.ProcessUsersRoles();
                            helper.CleanupUsers();
                        }
                    }
                    catch (DbEntityValidationException e)
                    {
                        var errorString = e.Message;
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            errorString += "\n\nEntity of type \"" + eve.Entry.Entity.GetType().Name +
                                           "\" in state \"" + eve.Entry.State +
                                           "\" has the following validation errors:";
                            foreach (var ve in eve.ValidationErrors)
                            {
                                errorString += "\n- Property: \"" + ve.PropertyName + "\", Error: \"" + ve.ErrorMessage +
                                               "\"";
                            }
                        }
                        Error(errorString + "\n\n" + e);
                        return RedirectToAction("Index");
                    }
                    catch (InvalidDataException e)
                    {
                        Error(e.Message + "\n\n" + e);
                        return RedirectToAction("Index");
                    }
                    catch (InvalidOperationException e)
                    {
                        Error(e.Message + "\n\n" + e);
                        return RedirectToAction("Index");
                    }

                    Success("A planilha foi importada com sucesso!");
                    return RedirectToAction("Index");
                }
            }
            Error("Arquivo inválido ou não existente!");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
