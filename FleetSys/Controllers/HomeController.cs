using FleetOps.App_Start;
using FleetOps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FleetSys.Controllers
{
        

    [Authorize(Roles = "Internal")]
    public class HomeController : BaseClass
    {
        [AccessibilityXtra("Approval", "Index", "apr")]
        public ActionResult Index(){
            if (Session["UserModules"] == null)
            {
                var objUserAccessOps = new UserAccessOps();
                Session["UserModules"] = objUserAccessOps.UserIndexAccess();
            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
