using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using WellaMates.Code.DataAccess;
using WellaMates.Controllers.Refunds;
using WellaMates.Extensions;
using WellaMates.Helpers;
using PagedList;
using WellaMates.DAL;
using WellaMates.Filters;
using WellaMates.Mailers;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace WellaMates.Controllers
{
    [AuthorizeUser(Roles = "Freelancer")]
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
                new FreelancerMonthlyDetailVM
                {
                    Monthly = monthly,
                    Response = new ResponseVM("Responder", "SendResponse", "Freelancer", "Monthlies", "Freelancer")
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
                new FreelancerVisitDetailVM
                {
                    Visit = visit,
                    Response = new ResponseVM("Responder", "SendResponse", "Freelancer", "Visits", "Freelancer")
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
                    new CreateEventVM("Enviar", "CreateEvent")
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
                    new CreateMonthlyVM(user.RefundProfile.Freelancer, "Enviar", "CreateMonthly")
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
            catch (Exception e)
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
                    new CreateVisitVM("Enviar", "CreateVisit")
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
                    SendVisitNotification(user, Visit);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    var errorMsg = error.ErrorMessage;
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
                ResponseNotification(response, user.PersonalInfo.ArtisticName ?? user.PersonalInfo.Name, db, Server);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        public static void ResponseNotification(ResponseVM response, string responsible, PortalContext db, HttpServerUtilityBase server)
        {
            foreach (var update in response.Updates)
            {
                update.RefundItem = db.RefundItems.First(r => r.RefundItemID == update.RefundItemID);
            }
            var mailer = new UserMailer();
            if (response.Updates.Length == 0 || response.Updates.All(u => u.Status == RefundItemStatus.REJECTED_NO_APPEAL)) return;
            switch (response.OwnerType)
            {
                case ResponseOwnerType.EVENT:
                    var @event = db.Events.Find(response.OwnerID);
                    foreach (var manager in @event.Freelancer.Managers)
                    {
                        mailer.EventResponseNotification(
                           manager.RefundProfile.User.ContactInfo.Email,
                           "Manager",
                           responsible,
                           @event,
                           response.Updates,
                           server.MapPath("~/Content/images/logo-wella.png")
                           ).Send();
                    }
                    
                    break;

                case ResponseOwnerType.VISIT:
                    var visit = db.Visits.Find(response.OwnerID);
                    foreach (var manager in visit.Freelancer.Managers)
                    {
                        mailer.VisitResponseNotification(
                            manager.RefundProfile.User.ContactInfo.Email,
                            "Manager",
                            responsible,
                            visit,
                            response.Updates,
                            server.MapPath("~/Content/images/logo-wella.png")
                            ).Send();
                    }
                    break;

                case ResponseOwnerType.MONTHLY:
                    var monthly = db.Monthlies.Find(response.OwnerID);
                    foreach (var manager in monthly.Freelancer.Managers)
                    {
                        mailer.MonthlyResponseNotification(
                            manager.RefundProfile.User.ContactInfo.Email,
                            "Manager",
                            responsible,
                            monthly,
                            response.Updates,
                            server.MapPath("~/Content/images/logo-wella.png")
                            ).Send();
                    }
                    break;
            }
        }


        private void SendEventNotification(UserProfile user, Event Event)
        {
            if (Event.Refund == null || Event.Refund.RefundItems == null || Event.Refund.RefundItems.Count <= 0)
            {
                return;
            }

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
            if (Visit.Refund == null || Visit.Refund.RefundItems == null || Visit.Refund.RefundItems.Count <= 0)
            {
                return;
            }
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
                    Monthly.Month.DisplayName(),
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
            if (refund.RefundItems == null)
                return;

            foreach (var refundItem in refund.RefundItems)
            {
                refundItem.Status = RefundItemStatus.SENT;
                if (refundItem.SubCategory == RefundItemSubCategory.TRANSPORTATION_KM)
                {
                    refundItem.Value = RefundConstants.KM_RATE*refundItem.KM;
                }
                refundItem.History = new Collection<RefundItemUpdate>
                {
                    new RefundItemUpdate
                    {
                        Date = DateTime.Now,
                        Status = RefundItemStatus.SENT,
                        RefundItem = refundItem,
                        RefundProfileID = user.UserID,
                        Comment = refundItem.Activity
                    }
                };
            }
        }
    }
}
