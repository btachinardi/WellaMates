using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace WellaMates.Controllers
{
    [AuthorizeUser(Roles = "Member")]
    [RedirectToAgreeToTerms]
    public class FreelancerController : BootstrapBaseController
    {
        private const int PAGE_SIZE = 10;

        protected PortalContext db = new PortalContext();

        /**
         * INDEX
         */
        public ActionResult Index()
        {
            return View(MemberHelper.SetBaseMemberVM(
                new FreelancerHomeVM
                    {
                    }, 
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult Events(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var events = GetPagedList<Event>(sortOrder, currentFilter, searchString, page);
            return View(MemberHelper.SetBaseMemberVM(
                new FreelancerEventsVM
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
                new FreelancerMonthliesVM
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
                new FreelancerVisitsVM
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
                new FreelancerEventDetailVM
                {
                    Event = @event,
                    Response = new ResponseVM("Responder", "SendResponse", "Freelancer", "Events", "Freelancer")
                    {
                        Refund = @event.Refund,
                        OwnerID = id,
                        OwnerType = ResponseOwnerType.EVENT
                    }
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult MonthlyDetail(int id)
        {
            var monthly = db.Monthlies.First(r => r.MonthlyID == id);
            return View(MemberHelper.SetBaseMemberVM(
                new FreelancerMonthlyDetailVM
                {
                    Monthly = monthly,
                    Response = new ResponseVM("Responder", "SendResponse", "Freelancer", "Monthlies", "Freelancer")
                    {
                        Refund = monthly.Refund,
                        OwnerID = id,
                        OwnerType = ResponseOwnerType.MONTHLY,
                        AllowAttachments = true,
                    }
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult VisitDetail(int id)
        {
            var visit = db.Visits.First(r => r.VisitID == id);
            return View(MemberHelper.SetBaseMemberVM(
                new FreelancerVisitDetailVM
                {
                    Visit = visit,
                    Response = new ResponseVM("Responder", "SendResponse", "Freelancer", "Visits", "Freelancer")
                    {
                        Refund = visit.Refund,
                        OwnerID = id,
                        OwnerType = ResponseOwnerType.VISIT
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
                new FreelancerRefundItemDetailVM
                {
                    RefundItem = refundItem
                },
                MemberHelper.GetUserProfile(db))
            );
        }

        public ActionResult CreateEvent()
        {
            var user = MemberHelper.GetUserProfile(db);
            if (!CheckCreationAuth(user))
                return HttpNotFound("Você não possui autorização para acessar este recurso.");

            return View(MemberHelper.SetBaseMemberVM(
                new FreelancerEventCreateVM(
                    new CreateEventVM("Criar Evento", "CreateEvent")
                ),
                MemberHelper.GetUserProfile(db)));
        }

        [System.Web.Mvc.HttpPost]
        public HttpResponseMessage CreateEvent(Event Event)
        {
            var user = MemberHelper.GetUserProfile(db);
            if (!CheckCreationAuth(user))
                return new HttpResponseMessage(HttpStatusCode.Forbidden);

            try
            {
                if (ModelState.IsValid)
                {
                    Event.FreelancerID = user.UserID;
                    ProcessRefundCreation(Event.Refund, user);
                    db.Events.Add(Event);
                    db.SaveChanges();
                    if (Event.Refund.RefundItems.Count > 0)
                        SendEventNotification(user, Event);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult CreateMonthly()
        {
            var user = MemberHelper.GetUserProfile(db);
            if (!CheckCreationAuth(user))
                return HttpNotFound("Você não possui autorização para acessar este recurso.");

            return View(MemberHelper.SetBaseMemberVM(
                new FreelancerMonthlyCreateVM(
                    new CreateMonthlyVM("Criar Mensal", "CreateMonthly")
                ),
                MemberHelper.GetUserProfile(db)));
        }

        [System.Web.Mvc.HttpPost]
        public HttpResponseMessage CreateMonthly(Monthly Monthly)
        {
            var user = MemberHelper.GetUserProfile(db);
            if (!CheckCreationAuth(user))
                return new HttpResponseMessage(HttpStatusCode.Forbidden);

            try
            {
                if (ModelState.IsValid)
                {
                    Monthly.FreelancerID = user.UserID;
                    ProcessRefundCreation(Monthly.Refund, user);
                    db.Monthlies.Add(Monthly);
                    db.SaveChanges();
                    SendMonthlyNotification(user, Monthly);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult CreateVisit()
        {
            var user = MemberHelper.GetUserProfile(db);
            if (!CheckCreationAuth(user))
                return HttpNotFound("Você não possui autorização para acessar este recurso.");

            return View(MemberHelper.SetBaseMemberVM(
                new FreelancerVisitCreateVM(
                    new CreateVisitVM("Criar Visita", "CreateVisit")
                ),
                MemberHelper.GetUserProfile(db)));
        }

        [System.Web.Mvc.HttpPost]
        public HttpResponseMessage CreateVisit(Visit Visit)
        {
            var user = MemberHelper.GetUserProfile(db);
            if (!CheckCreationAuth(user))
                return new HttpResponseMessage(HttpStatusCode.Forbidden);

            try
            {
                if (ModelState.IsValid)
                {
                    Visit.FreelancerID = user.UserID;
                    ProcessRefundCreation(Visit.Refund, user);
                    db.Visits.Add(Visit);
                    db.SaveChanges();
                    if (Visit.Refund.RefundItems.Count > 0)
                        SendVisitNotification(user, Visit);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
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
            var mailer = new UserMailer();
            if (response.Updates.Length == 0 || response.Updates.All(u => u.Status == RefundItemStatus.REJECTED_NO_APPEAL)) return;
            foreach (var manager in user.RefundProfile.Freelancer.Managers)
            {
                switch (response.OwnerType)
                {
                    case ResponseOwnerType.EVENT:
                        mailer.EventResponseNotification(
                            manager.RefundProfile.User.ContactInfo.Email,
                            "Manager",
                            user.PersonalInfo.ArtisticName ?? user.PersonalInfo.Name,
                            db.Events.Find(response.OwnerID),
                            response.Updates,
                            Server.MapPath("~/Content/images/logo-wella.png")
                            ).Send();
                        break;

                    case ResponseOwnerType.VISIT:
                        mailer.VisitResponseNotification(
                            manager.RefundProfile.User.ContactInfo.Email,
                            "Manager",
                            user.PersonalInfo.ArtisticName ?? user.PersonalInfo.Name,
                            db.Visits.Find(response.OwnerID),
                            response.Updates,
                            Server.MapPath("~/Content/images/logo-wella.png")
                            ).Send();
                        break;

                    case ResponseOwnerType.MONTHLY:
                        mailer.MonthlyResponseNotification(
                            manager.RefundProfile.User.ContactInfo.Email,
                            "Manager",
                            user.PersonalInfo.ArtisticName ?? user.PersonalInfo.Name,
                            db.Monthlies.Find(response.OwnerID),
                            response.Updates,
                            Server.MapPath("~/Content/images/logo-wella.png")
                            ).Send();
                        break;
                }
            }
        }


        private void SendEventNotification(UserProfile user, Event Event)
        {
            var mailer = new UserMailer();
            foreach (var manager in user.RefundProfile.Freelancer.Managers)
            {
                mailer.SendEventNotification(
                    manager.RefundProfile.User.ContactInfo.Email,
                    Url.Action("EventDetail", "Manager", new { @id = Event.EventID }),
                    user.PersonalInfo.ArtisticName ?? user.PersonalInfo.Name,
                    Event.Name,
                    Server.MapPath("~/Content/images/logo-wella.png")
                ).Send();
            }
        }

        private void SendVisitNotification(UserProfile user, Visit Visit)
        {
            var mailer = new UserMailer();
            foreach (var manager in user.RefundProfile.Freelancer.Managers)
            {
                mailer.SendVisitNotification(
                    manager.RefundProfile.User.ContactInfo.Email,
                    Url.Action("VisitDetail", "Manager", new { @id = Visit.VisitID }),
                    user.PersonalInfo.ArtisticName ?? user.PersonalInfo.Name,
                    Visit.Date.ToString("dd/MM/yyyy"),
                    Server.MapPath("~/Content/images/logo-wella.png")
                ).Send();
            }
        }

        private void SendMonthlyNotification(UserProfile user, Monthly Monthly)
        {
            var mailer = new UserMailer();
            foreach (var manager in user.RefundProfile.Freelancer.Managers)
            {
                mailer.SendMonthlyNotification(
                    manager.RefundProfile.User.ContactInfo.Email,
                    Url.Action("MonthlyDetail", "Manager", new { @id = Monthly.MonthlyID }),
                    user.PersonalInfo.ArtisticName ?? user.PersonalInfo.Name,
                    Monthly.Month,
                    Server.MapPath("~/Content/images/logo-wella.png")
                ).Send();
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
            var unitOfWork = new UnitOfWork(user, UnitOfWorkMode.Freelancer);
            var repository = unitOfWork.GetRepository<T>();
            var pageNumber = (page ?? 1);
            return repository.Search(currentFilter, sortOrder, null, pageNumber, PAGE_SIZE);
        }

        private bool CheckCreationAuth(UserProfile user)
        {
            if (user.RefundProfile == null ||
                user.RefundProfile.Freelancer == null)
            {
                return false;
            }
            return true;
        }

        private void ProcessRefundCreation(Refund refund, UserProfile user)
        {
            refund.Update();
            foreach (var refundItem in refund.RefundItems)
            {
                refundItem.Status = RefundItemStatus.SENT;
                refundItem.History = new Collection<RefundItemUpdate>
                {
                    new RefundItemUpdate
                    {
                        Date = DateTime.Now,
                        Status = RefundItemStatus.SENT,
                        RefundItem = refundItem,
                        RefundProfileID = user.UserID
                    }
                };
            }
        }


        /**
        [System.Web.Mvc.HttpPost]
        public HttpResponseMessage CreateRefund(Refund refund)
        {
            var user = MemberHelper.GetUserProfile(db);
            if (user.RefundProfile == null ||
                user.RefundProfile.Freelancer == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            if (MemberHelper.GenerateUpdates(db, refund, user.RefundProfile, null))
            {
                if (refund.Sent)
                {
                    SendNotification(user, refund);
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        public ActionResult EditRefund(int id)
        {
            var refund = db.Refunds.First(r => r.RefundID == id);
            var user = MemberHelper.GetUserProfile(db);
            if (user.RefundProfile == null ||
                user.RefundProfile.Freelancer == null ||
                user.UserID != refund.FreelancerID ||
                refund.Status != RefundStatus.EDITING)
            {
                return HttpNotFound("Você não possui autorização para acessar este recurso.");
            }
            var isMonthly = refund.RefundItems.Any(ri => ri.Category == RefundItemCategory.SALARY);
            return View(MemberHelper.SetBaseMemberVM(
                new FreelancerEventCreateVM(
                    isMonthly ? new EditRefundVM(refund, "Salvar Mudanças", "EditRefund", "Freelancer", "Monthly") : 
                    new EditRefundVM(refund, "Salvar Mudanças", "EditRefund")
                ),
                MemberHelper.GetUserProfile(db)));
        }

        [System.Web.Mvc.HttpPost]
        public HttpResponseMessage EditRefund(Refund refund)
        {
            var user = MemberHelper.GetUserProfile(db);
            var oldRefund = db.Refunds.FirstOrDefault(r => r.RefundID == refund.RefundID);
            if (user.RefundProfile == null ||
                user.RefundProfile.Freelancer == null ||
                oldRefund == null || oldRefund.Status != RefundStatus.EDITING ||
                user.UserID != oldRefund.FreelancerID)
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            if (MemberHelper.GenerateUpdates(db, refund, user.RefundProfile, oldRefund))
            {
                if (refund.Sent)
                {
                    SendNotification(user, refund);
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        [System.Web.Mvc.HttpPost]
        public HttpResponseMessage SendRefund(int id)
        {
            var user = MemberHelper.GetUserProfile(db);
            var oldRefund = db.Refunds.FirstOrDefault(r => r.RefundID == id);
            if (user.RefundProfile == null ||
                user.RefundProfile.Freelancer == null ||
                oldRefund == null || oldRefund.Status != RefundStatus.EDITING ||
                user.UserID != oldRefund.FreelancerID)
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
            
            if (MemberHelper.SendRefund(db, user.RefundProfile, oldRefund))
            {
                SendNotification(user, oldRefund);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
         */
    }
}
