using System;
using System.Web.Mvc;
using Utilities.DAL;
using System.Collections.Generic;
using CCMS.ModelSector;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Web.Routing;
using System.IO;
using System.Web.Security;

namespace FleetOps.App_Start
{
    public class Handle403Attribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new System.Web.Mvc.HttpStatusCodeResult(403);
                var viewContext = new HtmlHelper(new ViewContext(filterContext.Controller.ControllerContext, new WebFormView(filterContext.Controller.ControllerContext, "sdf"),
                    filterContext.Controller.ViewData, filterContext.Controller.TempData, TextWriter.Null), new ViewPage());
                var partial = System.Web.Mvc.Html.PartialExtensions.Partial(viewContext, "~/Views/Shared/Loginform.cshtml");
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.Write(partial.ToHtmlString());
            }
          //  base.HandleUnauthorizedRequest(filterContext);
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Session["LatestUrl"] = filterContext.HttpContext.Request.Url.PathAndQuery;
            }
            base.OnAuthorization(filterContext);
        }
    }
}