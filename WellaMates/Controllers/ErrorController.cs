using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;

namespace WellaMates.Controllers
{
    public class ErrorController : BootstrapBaseController
    {
        public ViewResult Index(string aspxerrorpath)
        {
            var errorPath = aspxerrorpath.Split('/');
            var controllerName = errorPath[1];
            var actionName = errorPath[2];
            var model = new HandleErrorInfo(Server.GetLastError(), controllerName, actionName);
            var test = "";
            return View("Error", model);
        }
        public ViewResult NotFound()
        {
            Response.StatusCode = 404; 
            return View("PageNotFound");
        }
    }
}