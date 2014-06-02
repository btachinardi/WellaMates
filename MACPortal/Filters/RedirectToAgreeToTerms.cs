using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MACPortal.Helpers;
using WebMatrix.WebData;
using WellaMates.DAL;
using WellaMates.Models;

namespace WellaMates.Filters
{
    public class RedirectToAgreeToTerms : ActionFilterAttribute
    {
        private const bool CLOSED = false;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var db = new PortalContext())
            {
                var user = MemberHelper.GetUserProfile(db);
                if (user.CurrentAcceptedAgreement != UserAgreement.CurrentVersion)
                {
                    if (filterContext.ActionDescriptor.ActionName == "PreRegister")
                    {
                        return;
                    }
                    var routeValues = new RouteValueDictionary(new
                    {
                        action = "PreRegister",
                        controller = "Member"
                    });

                    filterContext.Result = new RedirectToRouteResult(routeValues);
                }
// ReSharper disable ConditionIsAlwaysTrueOrFalse
                else if (CLOSED)
// ReSharper restore ConditionIsAlwaysTrueOrFalse
                {
                    if (filterContext.ActionDescriptor.ActionName == "PreRegisterDone" ||
                        filterContext.ActionDescriptor.ActionName == "PreAvatar")
                    {
                        return;
                    }
                    var routeValues = new RouteValueDictionary(new
                    {
                        action = "PreRegisterDone",
                        controller = "Member"
                    });

                    filterContext.Result = new RedirectToRouteResult(routeValues);
                }
                else if (filterContext.ActionDescriptor.ActionName == "PreRegister" ||
                         filterContext.ActionDescriptor.ActionName == "PreRegisterDone")
                {
                    var routeValues = new RouteValueDictionary(new
                    {
                        action = "Index",
                        controller = "Member"
                    });
                    filterContext.Result = new RedirectToRouteResult(routeValues);
                }
            }
        }
    }
}