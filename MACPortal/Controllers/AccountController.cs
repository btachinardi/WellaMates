using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BootstrapMvcSample.Controllers;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using WellaMates.Mailers;
using WellaMates.Models.Validation;
using WellaMates.DAL;
using WellaMates.Filters;
using WellaMates.Models;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using WellaMates.ViewModel;

namespace WellaMates.Controllers
{
    public class AccountController : BootstrapBaseController
    {
        PortalContext db = new PortalContext();
        //
        // GET: /Account/Login

        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")] 
        public ActionResult Login(string returnUrl)
        {
            if (Roles.IsUserInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            if (Roles.IsUserInRole("Member"))
            {
                return RedirectToAction("Index", "Member");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            Error("O usuário ou senha são inválidos.");
            return View(model);
        }

        //
        // GET: /Account/ForgotPassword

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid && WebSecurity.UserExists(model.UserName))
            {
                var user = db.UserProfiles.AsEnumerable().First(e => e.UserID == WebSecurity.GetUserId(model.UserName));
                var email = user.ContactInfo.Email;
                var token = Path.GetRandomFileName();
                var expiration = DateTime.Now.AddMinutes(60);

                user.RecoverPasswordToken = token;
                user.RecoverPasswordExpiration = expiration;

                string encryptedToken = Encryption.Encrypt(token);
                model.Email = email;

                var mailer = new UserMailer();
                mailer.ForgotPassword(email, encryptedToken, user.UserID, Server.MapPath("~/Content/images/logo-wella.png")).Send();
                db.SaveChanges();
                return View("ForgotPasswordEmailSent", model);
            }

            // If we got this far, something failed, redisplay form
            Error("O usuário '" + model.UserName + "' não existe.");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ChangePassword(string recoverPasswordKey, int userID)
        {
            var user = db.UserProfiles.Find(userID);
            var token = Encryption.Decrypt(recoverPasswordKey);
            if (user.RecoverPasswordToken != token)
            {
                Error("Código para recuperação de senha inválido.");
                return RedirectToAction("ForgotPassword");
            }
            if (user.RecoverPasswordExpiration < DateTime.Now)
            {

                Error("Este código para recuperação de senha expirou.");
                return RedirectToAction("ForgotPassword");
            }
            return View(new ChangePasswordModel { Token = recoverPasswordKey, UserName = user.UserName });
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var user = db.UserProfiles.AsEnumerable().First(e => e.UserID == WebSecurity.GetUserId(model.UserName));
            var token = Encryption.Decrypt(model.Token);
            if (user.RecoverPasswordToken != token)
            {
                return new HttpUnauthorizedResult("Código para recuperação de senha inválido.");
            }
            if (user.RecoverPasswordExpiration < DateTime.Now)
            {
                return new HttpUnauthorizedResult("Este código para recuperação de senha expirou.");
            }
            var membership = (SimpleMembershipProvider) Membership.Provider;
            membership.ResetPasswordWithToken(membership.GeneratePasswordResetToken(user.UserName), model.NewPassword);
            Success("Sua senha foi modificada com sucesso!");
            return RedirectToAction("Login");
        }

        //
        // POST: /Account/LogOff

        [AuthorizeUser]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/Register

        [AuthorizeUser(Roles = "Admin")]
        //[AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AuthorizeUser(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    Roles.AddUserToRole(model.UserName, "Member");
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Admin");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult AccessDenied()
        {
            if (Roles.IsUserInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Regulation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View(new ContactVM());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactVM contact)
        {
            var mailer = new UserMailer();
            mailer.Contact(contact.Name, contact.Email, contact.Title, contact.Message, Server.MapPath("~/Content/images/logo-wella.png")).Send();
            Success("Mensagem enviada com sucesso! Em breve entraremos em contato com você.");
            return RedirectToAction("Contact");
        }

        //
        // GET: /Account/Manage

        [AuthorizeUser]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // RefundProfile does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", String.Format("Unable to create local account. An account with the name \"{0}\" may already exist.", User.Identity.Name));
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Login", "Account");
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }
        
        public static string RegisterEmployee(string UserName, string Password)
        {
            // Attempt to register the user
            try
            {
                var membership = (SimpleMembershipProvider)Membership.Provider;
                var roles = (SimpleRoleProvider)Roles.Provider;
                membership.CreateUserAndAccount(UserName, Password);
                roles.AddUsersToRoles(new[] { UserName }, new[] { "Member" });
            }
            catch (MembershipCreateUserException e)
            {
                return ErrorCodeToString(e.StatusCode);
            }
            return null;
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "RefundProfile name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
