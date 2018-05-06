using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FleetSys.Controllers
{
    public class InternalErrorController : Controller
    {
        // GET: InternalError
        public ActionResult Error403()
        {
            HttpContext.Response.StatusCode = 403;
            return View();
        }
        public ActionResult Error404()
        {
            HttpContext.Response.StatusCode = 404;
            return View();
        }
        public ActionResult Error500()
        {
            String Message=null;
            if (TempData["ExcMessage"] != null)
            {
                Message = TempData["ExcMessage"].ToString();
            }
            HttpContext.Response.StatusCode = 500;
            if (!string.IsNullOrEmpty(Message))
            {
                ViewBag.ExcMessage = Message;
            }   
            return View();
        }

        public ActionResult Inter()
        {
            return View();
        }
    }
}