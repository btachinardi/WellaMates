using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using HelloWorld.Code.DataAccess;
using MACPortal.Helpers;
using MACPortal.ViewModel;
using PagedList;
using WellaMates.DAL;
using WellaMates.Filters;
using WellaMates.Mailers;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace MACPortal.Controllers
{
    [AuthorizeUser(Roles = "Member")]
    [RedirectToAgreeToTerms]
    public class ManagerController : BootstrapBaseController
    {
        private const int PAGE_SIZE = 10;

        protected PortalContext db = new PortalContext();

        /**
         * INDEX
         */
        public ActionResult Index()
        {
            return View(MemberHelper.SetBaseMemberVM(
                new ManagerHomeVM
                {
                },
                MemberHelper.GetUserProfile(db)
                )
            );
        }

        public ActionResult Events(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var events = GetPagedList<Event>(sortOrder, currentFilter, searchString, page);
            return View(MemberHelper.SetBaseMemberVM(
                new ManagerEventsVM
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
                new ManagerMonthliesVM
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
                new ManagerVisitsVM
                {
                    Visits = visits
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult EventDetail(int id)
        {
            return View(MemberHelper.SetBaseMemberVM(
                new ManagerEventDetailVM
                {
                    Event = db.Events.First(r => r.EventID == id),
                    Response = new ResponseVM("Responder", "SendResponse", "Manager", "Events", "Manager")
                    {
                        OwnerID = id,
                        OwnerType = ResponseOwnerType.EVENT
                    }
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult MonthlyDetail(int id)
        {
            return View(MemberHelper.SetBaseMemberVM(
                new ManagerMonthlyDetailVM
                {
                    Monthly = db.Monthlies.First(r => r.MonthlyID == id),
                    Response = new ResponseVM("Responder", "SendResponse", "Manager", "Monthlies", "Manager")
                    {
                        OwnerID = id,
                        OwnerType = ResponseOwnerType.EVENT
                    }
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult VisitDetail(int id)
        {
            return View(MemberHelper.SetBaseMemberVM(
                new ManagerVisitDetailVM
                {
                    Visit = db.Visits.First(r => r.VisitID == id),
                    Response = new ResponseVM("Responder", "SendResponse", "Manager", "Visits", "Manager")
                    {
                        OwnerID = id,
                        OwnerType = ResponseOwnerType.EVENT
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
                new ManagerRefundItemDetailVM
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
                ResponseNotification(user, response);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        private void ResponseNotification(UserProfile user, ResponseVM response)
        {
            foreach (var update in response.Updates)
            {
                update.RefundItem = db.RefundItems.First(r => r.RefundItemID == update.RefundItemID);
            }
            if (response.Updates.Length == 0 || response.Updates.All(u => u.Status == RefundItemStatus.REJECTED_NO_APPEAL)) return;
            var mailer = new UserMailer();
            switch (response.OwnerType)
            {
                case ResponseOwnerType.EVENT:
                    var Event = db.Events.Find(response.OwnerID);
                    mailer.EventResponseNotification(
                        Event.Freelancer.RefundProfile.User.ContactInfo.Email,
                        "Freelancer",
                        user.PersonalInfo.ArtisticName ?? user.PersonalInfo.Name,
                        Event,
                        response.Updates,
                        Server.MapPath("~/Content/images/logo-wella.png")
                        ).Send();
                    break;

                case ResponseOwnerType.VISIT:
                    var visit = db.Visits.Find(response.OwnerID);
                    mailer.VisitResponseNotification(
                        visit.Freelancer.RefundProfile.User.ContactInfo.Email,
                        "Freelancer",
                        user.PersonalInfo.ArtisticName ?? user.PersonalInfo.Name,
                        visit,
                        response.Updates,
                        Server.MapPath("~/Content/images/logo-wella.png")
                        ).Send();
                    break;

                case ResponseOwnerType.MONTHLY:
                    var monthly = db.Monthlies.Find(response.OwnerID);
                    mailer.MonthlyResponseNotification(
                        monthly.Freelancer.RefundProfile.User.ContactInfo.Email,
                        "Freelancer",
                        user.PersonalInfo.ArtisticName ?? user.PersonalInfo.Name,
                        monthly,
                        response.Updates,
                        Server.MapPath("~/Content/images/logo-wella.png")
                        ).Send();
                    break;
            }
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
            var unitOfWork = new UnitOfWork(user, UnitOfWorkMode.Manager);
            var repository = unitOfWork.GetRepository<T>();
            var pageNumber = (page ?? 1);
            return repository.Search(currentFilter, sortOrder, null, pageNumber, PAGE_SIZE);
        }

        private bool CheckCreationAuth(UserProfile user)
        {
            if (user.RefundProfile == null ||
                user.RefundProfile.Manager == null)
            {
                return false;
            }
            return true;
        }
        

        /*public ActionResult Refunds(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
                currentFilter = searchString;
            }
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = currentFilter;

            var user = MemberHelper.GetUserProfile(db);
            var unitOfWork = new UnitOfWork(user, UnitOfWorkMode.Manager);
            var repository = unitOfWork.GetRefundRepository;
            var pageNumber = (page ?? 1);
            var refunds = repository.Search(currentFilter, sortOrder, null, pageNumber, PAGE_SIZE);
            return View(MemberHelper.SetBaseMemberVM(
                new ManagerRefundsVM
                {
                    Refunds = refunds
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult Monthly(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
                currentFilter = searchString;
            }
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = currentFilter;

            var user = MemberHelper.GetUserProfile(db);
            var unitOfWork = new UnitOfWork(user, UnitOfWorkMode.RefundManagerMonthly);
            var repository = unitOfWork.GetRefundRepository;
            var pageNumber = (page ?? 1);
            var refunds = repository.Search(currentFilter, sortOrder, null, pageNumber, PAGE_SIZE);
            return View(MemberHelper.SetBaseMemberVM(
                new ManagerRefundsVM
                {
                    Refunds = refunds
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult RefundDetail(int id)
        {
            var refund = db.Refunds.First(r => r.RefundID == id);
            return View(MemberHelper.SetBaseMemberVM(
                new ManagerRefundDetailVM
                {
                    Refund = db.Refunds.First(r => r.RefundID == id),
                    EditRefund = new EditRefundVM(refund, "Enviar", "SendResponse", "Manager", "Refunds", "Manager")
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        [System.Web.Mvc.HttpPost]
        public HttpResponseMessage SendResponse(RefundItemUpdate[] updates)
        {
            var user = MemberHelper.GetUserProfile(db);
            if (user.RefundProfile == null ||
                user.RefundProfile.Manager == null ||
                updates == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            if (MemberHelper.SendResponse(db, user.RefundProfile, updates))
            {
                ResponseNotification(user, updates);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        private void ResponseNotification(UserProfile user, RefundItemUpdate[] updates)
        {
            foreach (var update in updates)
            {
                update.RefundItem = db.RefundItems.First(r => r.RefundItemID == update.RefundItemID);
            }
            var mailer = new UserMailer();
            var refundItem = updates[0].RefundItem;
            var refund = refundItem.Refund;
            mailer.ResponseNotification(
                refund.Freelancer.RefundProfile.User.ContactInfo.Email,
                "Freelancer",
                user.PersonalInfo.ArtisticName ?? user.PersonalInfo.Name,
                refund.Name,
                updates,
                Server.MapPath("~/Content/images/logo-wella.png")
            ).Send();
        }
         */
    }
}