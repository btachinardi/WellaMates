using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using WellaMates.Code.DataAccess;
using JetBrains.Annotations;
using PagedList;
using WellaMates.Controllers;
using WellaMates.DAL;
using WellaMates.Filters;
using WellaMates.Helpers;
using WellaMates.Mailers;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace WellaMates.Controllers
{
    [AuthorizeUser(Roles = "RefundAdministrator")]
    public class RefundAdministratorController : BootstrapBaseController
    {
        private const int PAGE_SIZE = 10;

        protected PortalContext db = new PortalContext();

        /**
         * INDEX
         */
        public ActionResult Index()
        {
            return View(MemberHelper.SetBaseMemberVM(
                new AdministratorHomeVM
                {
                },
                MemberHelper.GetUserProfile(db)
                )
            );
        }


        [HttpPost]
        public FileResult GetReport(GetReportVM filters)
        {
            var templateDirectory = new DirectoryInfo(Server.MapPath("~/Content/uploaded/templates"));
            Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}.xlsx", filters.Title));
            return new FileContentResult(ExcelWriter.GenerateReport(templateDirectory, filters, db),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public ActionResult Events(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var events = GetPagedList<Event>(sortOrder, currentFilter, searchString, page);
            return View(MemberHelper.SetBaseMemberVM(
                new AdministratorEventsVM
                {
                    Events = events
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult Monthlies(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var monthlies = GetPagedList<Monthly>(sortOrder, currentFilter, searchString, page);
            return View(MemberHelper.SetBaseMemberVM(
                new AdministratorMonthliesVM
                {
                    Monthlies = monthlies
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult Visits(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var visits = GetPagedList<Visit>(sortOrder, currentFilter, searchString, page);
            return View(MemberHelper.SetBaseMemberVM(
                new AdministratorVisitsVM
                {
                    Visits = visits
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult EventDetail(int id)
        {
            var @event = db.Events.First(r => r.EventID == id);
            return View(MemberHelper.SetBaseMemberVM(
                new AdministratorEventDetailVM
                {
                    Event = @event,
                    Response = new ResponseVM("Salvar", "SendResponse", "RefundAdministrator", "Events", "RefundAdministrator")
                    {
                        Refund = @event.Refund,
                        OwnerID = id,
                        OwnerType = ResponseOwnerType.EVENT,
                        AllowAttachments = true
                    }
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult MonthlyDetail(int id)
        {
            var monthly = db.Monthlies.First(r => r.MonthlyID == id);
            return View(MemberHelper.SetBaseMemberVM(
                new AdministratorMonthlyDetailVM
                {
                    Monthly = monthly,
                    Response = new ResponseVM("Salvar", "SendResponse", "RefundAdministrator", "Monthlies", "RefundAdministrator")
                    {
                        Refund = monthly.Refund,
                        OwnerID = id,
                        OwnerType = ResponseOwnerType.MONTHLY,
                        AllowAttachments = true
                    }
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult VisitDetail(int id)
        {
            var visit = db.Visits.First(r => r.VisitID == id);
            return View(MemberHelper.SetBaseMemberVM(
                new AdministratorVisitDetailVM
                {
                    Visit = visit,
                    Response = new ResponseVM("Salvar", "SendResponse", "RefundAdministrator", "Visits", "RefundAdministrator")
                    {
                        Refund = visit.Refund,
                        OwnerID = id,
                        OwnerType = ResponseOwnerType.VISIT,
                        AllowAttachments = true
                    }
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult RefundItemDetail(int id)
        {
            var refundItem = db.RefundItems.First(r => r.RefundItemID == id);
            AzureBlobSA.ProcessFiles(refundItem.Files);
            return View(MemberHelper.SetBaseMemberVM(
                new AdministratorRefundItemDetailVM
                {
                    RefundItem = refundItem
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        [System.Web.Mvc.HttpPost]
        public HttpResponseMessage SendResponse(ResponseVM response)
        {
            var user = MemberHelper.GetUserProfile(db);
            if (!CheckCreationAuth(user))
                return new HttpResponseMessage(HttpStatusCode.Forbidden);

            if (MemberHelper.SendResponse(db, user.RefundProfile, response.Updates))
            {
                var responsible = "[ADMIN] " + (user.PersonalInfo.ArtisticName ?? user.PersonalInfo.Name);
                FreelancerController.ResponseNotification(response, responsible, db, Server);
                ManagerController.ResponseNotification(response, responsible, db, Server);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult CloseRefund(int RefundID, DateTime PaymentDate)
        {
            var user = MemberHelper.GetUserProfile(db);
            if (!CheckCreationAuth(user))
                return RedirectToAction("AccessDenied", "Account");


            var refund = db.Refunds.Find(RefundID);
            if (refund != null)
            {
                refund.PaymentDate = PaymentDate;
                refund.Update();
                db.Refunds.Attach(refund);
                db.Entry(refund).State = EntityState.Modified;
                db.SaveChanges();
                var freelancer = MemberHelper.GetRefundFreelancer(refund, db);
                var responsible = "[ADMIN] " + (user.PersonalInfo.ArtisticName ?? user.PersonalInfo.Name);
                NotifyRefundClosing(freelancer.RefundProfile.User.ContactInfo.Email, refund, responsible, "Freelancer");
                foreach (var manager in freelancer.Managers)
                {
                    NotifyRefundClosing(manager.RefundProfile.User.ContactInfo.Email, refund, responsible, "Manager");
                }
                Success("Data de Pagamento Modificada com Sucesso!");
            }
            else
            {
                Error("Houve um problema ao alterar a data de pagamento. Tente Novamente mais Tarde.");
            }

            // ReSharper disable once PossibleNullReferenceException
            return Redirect(this.Request.UrlReferrer.AbsolutePath);
        }

        private void NotifyRefundClosing(string email, Refund refund, string responsible, [AspMvcController]string controller)
        {
            new UserMailer().CloseRefundNotification(
                    email,
                    controller,
                    responsible,
                    MemberHelper.GetRefundOwner(refund, db),
                    Server.MapPath("~/Content/images/logo-wella.png")
                    ).Send();
        }


        private IPagedList<T> GetPagedList<T>(string sortOrder, string currentFilter, string searchString, int? page) where T : class
        {
            if (searchString != null)
            {
                page = 1;
                currentFilter = searchString;
            }
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = currentFilter;

            var user = MemberHelper.GetUserProfile(db);
            var unitOfWork = new UnitOfWork(user, UnitOfWorkMode.Administrator);
            var repository = unitOfWork.GetRepository<T>();
            var pageNumber = (page ?? 1);
            return repository.Search(currentFilter, sortOrder, null, pageNumber, PAGE_SIZE);
        }

        private bool CheckCreationAuth(UserProfile user)
        {
            if (user.RefundProfile == null ||
                user.RefundProfile.RefundAdministrator == null)
            {
                return false;
            }
            return true;
        }
    }
}