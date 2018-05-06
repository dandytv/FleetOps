using System.Web;
using System.Web.Optimization;

namespace FleetSys
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Scripts/UnbotrusiveScripts").Include(
                "~/Scripts/jquery.unobtrusive-ajax.min.js").Include(
                "~/Scripts/jquery.validate.min.js").Include(
                "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/MainScripts").Include(new string[]{"~/Content/scripts/jquery-2.1.1.min.js","~/Content/scripts/jquery-migrate-1.2.1.min.js",
            "~/Content/scripts/pace.min.js","~/Content/scripts/jquery.mmenu.min.js","~/Content/scripts/core.min.js","~/Content/scripts/jquery.cookie.min.js","~/Content/scripts/pageScripts.js",
            "~/Scripts/angular.js","~/Scripts/angular-route.js","~/Scripts/angular-sanitize.js","~/Content/scripts/bootstrap.min.js","~/Content/scripts/jqueryui-1.92.min.js",
            "~/Content/Plugins/Datatables/dataTables.js","~/Content/Plugins/Datatables/bootstrap.dataTables.js","~/Content/Plugins/Datatables/dataTables.colVis.js","~/Content/Plugins/Datepicker/js/bootstrap-datepicker.js",
            "~/Scripts/underscore.js", "~/Content/scripts/curreny.js","~/Scripts/uiselect.js"
            }));

            bundles.Add(new StyleBundle("~/Contents/Styles").Include(new string[]{"~/Content/css/bootstrap.css","~/Content/css/style.css","~/Content/font-awesome/css/font-awesome.css","~/Content/Plugins/PNotify/pnotify.custom.css","~/Content/Plugins/Datatables/dataTables.colVis.css",
            "~/Scripts/uiselect.css","~/Scripts/select2.css","~/Content/Plugins/Datepicker/css/datepicker3.css","~/Content/css/jquery-ui.css","~/Content/Plugins/Datatables/dataTables.css"
            }));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false ;
        }
    }
}
