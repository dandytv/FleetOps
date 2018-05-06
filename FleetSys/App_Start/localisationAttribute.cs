using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;
using System.Web.Routing;
namespace FleetOps.App_Start
{
    public class localizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentContext = new HttpContextWrapper(HttpContext.Current);
            var routeData = RouteTable.Routes.GetRouteData(currentContext);
            if (routeData == null || routeData.Values.Count == 0) return;
            if (routeData.Values["language"] == null)
            {
                var cookie = filterContext.HttpContext.Request.Cookies["MyCCMSLang"];
                var langHeader = string.Empty;
                if (cookie != null)
                {
                    langHeader = cookie.Value;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                }
                else
                {
                    langHeader = filterContext.HttpContext.Request.UserLanguages[0];
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
                    HttpCookie _cookie = new HttpCookie("MyCCMSLang", Thread.CurrentThread.CurrentUICulture.Name);
                    _cookie.Expires = DateTime.Now.AddYears(1);
                    filterContext.HttpContext.Response.Cookies.Remove("MyCCMSLang");
                    filterContext.HttpContext.Response.Cookies.Add(_cookie);
                }
                //RedirectToUrlWithAppropriateLanguage(currentContext, routeData);
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new
                {
                    language = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName,
                    controller = routeData.Values["controller"],
                    action = routeData.Values["action"]
                }));
            }
            else
            {
                var languageCode = (string)routeData.Values["language"];
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(languageCode);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageCode);
                HttpCookie _cookie = new HttpCookie("MyCCMSLang", Thread.CurrentThread.CurrentUICulture.Name);
                _cookie.Expires = DateTime.Now.AddYears(1);
                filterContext.HttpContext.Response.Cookies.Remove("MyCCMSLang");
               filterContext.HttpContext.Response.Cookies.Add(_cookie);

            }
        }
    }
}


public class AutoLocalizingRoute : Route
{


    public AutoLocalizingRoute(string url, object defaults, object constraints)
        : base(url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new MvcRouteHandler()) { }


    public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
    {
        if (!values.ContainsKey("language"))
        {
            values["language"] = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

        }
        return base.GetVirtualPath(requestContext, values);
    }


}