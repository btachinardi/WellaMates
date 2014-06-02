using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BootstrapMvcSample.Controllers;
using MACPortal.Helpers;
using WebMatrix.WebData;
using WellaMates.DAL;
using WellaMates.Filters;
using WellaMates.Mailers;
using WellaMates.ViewModel;

namespace MACPortal.Controllers
{
    [RedirectToAgreeToTerms]
    [AuthorizeUserAttribute(Roles = "Member")]
    public class MemberController : BootstrapBaseController
    {
        protected readonly PortalContext db = new PortalContext();

        /**
         * INDEX
         */
        public virtual ActionResult Index()
        {
            return RedirectToAction("Index", MemberHelper.GetCurrentController(db));
        }

        #region Pre Registration / Closed Site

        /**
         * PRE REGISTER
         */
        public ActionResult PreRegister()
        {
            try
            {
                var user = MemberHelper.GetUserProfile(db);
                return View(new UserVM().Start(user));
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreRegister(UserVM registration)
        {
            var user = db.UserProfiles.First(u => u.UserID == registration.UserID);
            try
            {
                var membership = (SimpleMembershipProvider)Membership.Provider;
                membership.ResetPasswordWithToken(membership.GeneratePasswordResetToken(user.UserName), registration.NewPassword);
            }
            catch (Exception)
            {
                Error("Houve um erro na modificação de sua senha, que não foi alterada.");
            }
            registration.Finish(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /**
         * PRE REGISTER **DONE**
         */
        public ActionResult PreRegisterDone()
        {
            var user = MemberHelper.GetUserProfile(db);
            return View(new UserVM().Start(user, EditUserMode.COMPLETE, "Salvar"));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreRegisterDone(UserVM registration)
        {
            var user = db.UserProfiles.First(u => u.UserID == registration.UserID);
            try
            {
                var membership = (SimpleMembershipProvider)Membership.Provider;
                membership.ResetPasswordWithToken(membership.GeneratePasswordResetToken(user.UserName), registration.NewPassword);
            }
            catch (Exception)
            {
                Error("Houve um erro na modificação de sua senha, que não foi alterada.");
            }

            registration.Finish(user);
            db.SaveChanges();
            return View(new UserVM().Start(user, EditUserMode.COMPLETE, "Salvar"));
        }

        #endregion

        /**
         * CONTACT
         */
        public ActionResult Contact()
        {
            var user = MemberHelper.GetUserProfile(db);

            var contact = new ContactVM
            {
                Name = user.PersonalInfo.Name,
                Email = user.ContactInfo.Email
            };
            return View(MemberHelper.SetBaseMemberVM(
                new MemberContactVM
                {
                    Contact = contact
                },
                user)
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactVM contact)
        {
            var user = MemberHelper.GetUserProfile(db);

            contact.Name = user.PersonalInfo.Name;
            contact.Email = user.ContactInfo.Email;

            var mailer = new UserMailer();
            mailer.Contact(contact.Name, contact.Email, contact.Title, contact.Message, Server.MapPath("~/Content/images/logo-with-products-smaller.png")).Send();
            Success("Mensagem enviada com sucesso! Em breve entraremos em contato com você.");
            return RedirectToAction("Contact");
        }

        /**
         * REGULATION
         */
        public ActionResult Regulation()
        {
            var user = MemberHelper.GetUserProfile(db);

            return View(MemberHelper.SetBaseMemberVM(user)
            );
        }

        #region User Info

        /**
         * EDIT PROFILE
         */
        public ActionResult EditProfile()
        {
            return View(MemberHelper.SetBaseMemberVM(
                new FreelancerHomeVM
                    {
                    }, 
                MemberHelper.GetUserProfile(db)
            ));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserVM registration)
        {
            if (registration.Mode == EditUserMode.PASSWORD)
            {
                return ChangePassword(registration);
            }
            try
            {
                var user = db.UserProfiles.First(u => u.UserID == registration.UserID);
                registration.Finish(user);
                db.SaveChanges();
            }
            catch (Exception)
            {
                Error("Houve um erro na modificação de seu perfil, por favor tente novamente mais tarde.");
                return RedirectToAction("EditProfile");
            }
            Success("Seu Perfil foi atualizado com sucesso.");
            return RedirectToAction("EditProfile");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UserVM registration)
        {
            var user = db.UserProfiles.First(u => u.UserID == registration.UserID);
            try
            {
                var membership = (SimpleMembershipProvider)Membership.Provider;
                if (!membership.ChangePassword(user.UserName, registration.OldPassword, registration.NewPassword))
                {
                    Error("A senha está incorreta.");
                    return RedirectToAction("EditProfile");
                }
            }
            catch (Exception)
            {
                Error("Houve um erro na modificação de sua senha, por favor tente novamente mais tarde.");
                return RedirectToAction("EditProfile");
            }
            Success("Sua senha foi alterada com sucesso.");
            return RedirectToAction("EditProfile");
        }

        #endregion

        [HttpPost]
        public object UploadImage(HttpPostedFileBase File)
        {
            if (File == null || File.ContentLength == 0)
            {
                return "error:null";
            }
            var name = "";//AzureBlobSA.UploadImage(AzureBlobSA.REFUND_FILES_CONTAINER, File);
            return new
            {
                name,
                path = GetImagePath(
                        AzureBlobSA.EncodeTo64(name),
                        AzureBlobSA.GetSasUrl(AzureBlobSA.REFUND_FILES_CONTAINER)
                    )
            };
        }

        public string GetImagePath(string id, string sas)
        {
            var fileName = AzureBlobSA.DecodeFrom64(id);
            return AzureBlobSA.GetSasBlobUrl(AzureBlobSA.REFUND_FILES_CONTAINER, fileName, sas);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
