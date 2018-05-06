using CardTrend.Common.Helpers;
using FleetSys.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FleetSys
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AppConfigurationHelper.EncryptionConnectionString(ConfigurationManager.ConnectionStrings["pdb_ccmsContext"].ConnectionString);
            // Register mapping
            UnityConfig.RegisterMapping();
            //CardHolderCreateMapper.RegisterMapping();
            // end mapping
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
        }

    }
}
