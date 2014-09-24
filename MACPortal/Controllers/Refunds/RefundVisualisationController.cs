using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using HelloWorld.Code.DataAccess;
using JetBrains.Annotations;
using MACPortal.Helpers;
using MACPortal.ViewModel;
using PagedList;
using WellaMates.Controllers;
using WellaMates.DAL;
using WellaMates.Filters;
using WellaMates.Mailers;
using WellaMates.Models;
using WellaMates.ViewModel;

namespace MACPortal.Controllers
{
    [AuthorizeUser(Roles = "Visualisation")]
    public class RefundVisualisationController : BootstrapBaseController
    {

        protected PortalContext db = new PortalContext();

        /**
         * INDEX
         */
        public ActionResult Index()
        {
            return View(MemberHelper.SetBaseMemberVM(
                new VisualisationHomeVM
                {
                },
                MemberHelper.GetUserProfile(db)
                )
            );
        }
    }
}