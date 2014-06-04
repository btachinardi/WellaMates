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
using WellaMates.Models;
using WellaMates.ViewModel;

namespace MACPortal.Controllers
{
    [AuthorizeUser(Roles = "Member")]
    [RedirectToAgreeToTerms]
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
                new AdministratorMonthlyDetailVM
                {
                    Monthly = monthly,
                    Response = new ResponseVM("Salvar", "SendResponse", "RefundAdministrator", "Monthlies", "RefundAdministrator")
                    {
                        Refund = monthly.Refund,
                        OwnerID = id,
                        OwnerType = ResponseOwnerType.MONTHLY
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
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
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