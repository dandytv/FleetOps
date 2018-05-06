using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FleetSys
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            // routes.Add(new SubdomainRoute());


       //     routes.MapRoute(
       //    name: "CustomRoute",
       //    url: "FleetSysV2/{controller}/{action}/{id}",
       //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
       //);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }

    public class SubdomainRoute : RouteBase
    {

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var host = httpContext.Request.Url.Host;
            var index = host.IndexOf(".");
            string[] segments = httpContext.Request.Url.PathAndQuery.Split('/');

            if (index < 0)
                return null;

            var subdomain = host.Substring(0, index);
            string controller = (segments.Count() > 0) ? segments[0] : "Account";
            string action = (segments.Count() > 1) ? segments[1] : "Index";

            var routeData = new RouteData(this, new MvcRouteHandler());
            routeData.Values.Add("controller", controller); //Goes to the relevant Controller  class
            routeData.Values.Add("action", action); //Goes to the relevant action method on the specified Controller
            routeData.Values.Add("id", subdomain); //pass subdomain as argument to action method
            return routeData;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //Implement your formating Url formating here
            return null;
        }

    }
}
