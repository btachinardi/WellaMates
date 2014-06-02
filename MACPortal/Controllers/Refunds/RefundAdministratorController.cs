using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using HelloWorld.Code.DataAccess;
using MACPortal.Helpers;
using MACPortal.ViewModel;
using WellaMates.DAL;
using WellaMates.Filters;
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
            return HttpNotFound("NOT IMPLEMENTED");
        }


        public ActionResult RefundDetail(int id)
        {
            return View(MemberHelper.SetBaseMemberVM(
                new AdministratorRefundDetailVM
                {
                    Refund = db.Refunds.First(r => r.RefundID == id)
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
    }
}