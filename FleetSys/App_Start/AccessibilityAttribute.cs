using System;
using System.Web.Mvc;
using Utilities.DAL;
using System.Collections.Generic;
using CCMS.ModelSector;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
namespace FleetOps.App_Start
{
    public class AccessibilityAttribute : ActionFilterAttribute
    {
        //public bool IsAdmin;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var _AccessibilityList = new List<Accessibility>();

            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var target = new StringBuilder();
                var OverRideController = filterContext.RequestContext.HttpContext.Request.QueryString["overrideController"];
                if (OverRideController != null)
                {
                    target.Append(OverRideController);
                }
                else
                {
                    target.Append(filterContext.RouteData.Values["Controller"]);
                }
                //target.Append(filterContext.RouteData.Values["Controller"]);
                target.Append("/").Append(filterContext.RouteData.Values["Action"]);

                var url = target.ToString();
                //if (url == "ApplicantCard/Index")
                //    url = "Applicant/Index";
                var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);

                try
                {
                    objDataEngine.InitiateConnection();
                    SqlParameter[] Parameters = new SqlParameter[4];
                    Parameters[0] = new SqlParameter("@Flag", 1);
                    Parameters[1] = new SqlParameter("@UserId", filterContext.HttpContext.User.Identity.Name);
                    Parameters[2] = new SqlParameter("@Url", url);
                    Parameters[3] = new SqlParameter("@SectionCd", "NULL");
                    var execResult = objDataEngine.ExecuteCommand("WebUserAccessURL", CommandType.StoredProcedure, Parameters);
                    while (execResult.Read())
                    {
                        _AccessibilityList.Add(new Accessibility
                        {
                            CtrlId = Convert.ToString(execResult["CtrlName"]),
                            Sts = Convert.ToInt16(execResult["Sts"]),
                            SectionName = Convert.ToString(execResult["Section Name"]),
                            Page = Convert.ToString(execResult["Page"]),
                            SectionShortCd = Convert.ToString(execResult["SectionCd"]),
                            Sequence = Convert.ToInt16(execResult["Seq"])
                        });
                    };
                }
                finally
                {

                    objDataEngine.CloseConnection();
                }


                if (!_AccessibilityList.Any() || _AccessibilityList.First().Sts == 0)
                {
                    filterContext.Result = new HttpStatusCodeResult(403);
                }

                else
                {
                    filterContext.Controller.ViewData["Accessibility"] = _AccessibilityList;
                    filterContext.HttpContext.Session["CtrlAccessibility"] = _AccessibilityList;
                }
            }
            else
            {
                if (filterContext.HttpContext.Session["CtrlAccessibility"] != null)
                {
                    _AccessibilityList = (List<Accessibility>)filterContext.HttpContext.Session["CtrlAccessibility"];
                    filterContext.Controller.ViewData["Accessibility"] = _AccessibilityList;
                }
                else
                {
                    filterContext.Result = new HttpStatusCodeResult(403);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class AccessibilityXtraAttribute : ActionFilterAttribute
    {
        //public string type;

        private string OverrideController = null;
        private string OverrideAction = null;
        private string OverrideSection = null;

        public AccessibilityXtraAttribute() { }
        public AccessibilityXtraAttribute(string _OverrideController, string _OverrideAction, string _OverrideSection)
        {
            this.OverrideAction = _OverrideAction;
            this.OverrideController = _OverrideController;
            this.OverrideSection = _OverrideSection;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var _AccessibilityList = new List<Accessibility>();
            var target = new StringBuilder();
            //var SectionId = new Section();
            var _Controller = string.IsNullOrEmpty(this.OverrideController) ? filterContext.RequestContext.HttpContext.Request.QueryString["overrideController"] :
                this.OverrideController;

            var SectionCd = !string.IsNullOrEmpty(this.OverrideSection) ? this.OverrideSection : !string.IsNullOrEmpty(filterContext.RequestContext.HttpContext.Request.QueryString["OverridePrefix"]) ?
                filterContext.RequestContext.HttpContext.Request.QueryString["OverridePrefix"] : filterContext.RequestContext.HttpContext.Request.QueryString["Prefix"];


            var sectionlist = new List<String>();
            if (_Controller != null)
            {
                target.Append(_Controller);
            }
            else
            {
                target.Append(filterContext.RouteData.Values["Controller"]);
            }
            //target.Append(filterContext.RouteData.Values["Controller"]);
            //target.Append("/").Append(filterContext.RouteData.Values["Action"]);
            var _Action = string.IsNullOrEmpty(this.OverrideAction) ? filterContext.RequestContext.HttpContext.Request.QueryString["type"] : this.OverrideAction;
            //if (_QueryString == "Index")
            //{
            //if (_QueryString != null)
            //{
            if (string.IsNullOrEmpty(_Action))
                _Action = "Index";
            target.Append("/" + _Action);
            //application}
            //else
            //{
            //  target.Append("/").Append(filterContext.RouteData.Values["Action"]);
            //}

            //}
            //else
            //{
            //    target.Append("/" + _QueryString);
            //}
            var url = target.ToString();
            //if (url == "Dealer/Index")
            //    url = "Applicant/Index";
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);
            if (url.ToLower() == "EventConfiguration/Index".ToLower() && SectionCd == "dtl")
                SectionCd = "evc";
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[4];
                Parameters[0] = new SqlParameter("@Flag", 1);
                Parameters[1] = new SqlParameter("@UserId", filterContext.HttpContext.User.Identity.Name);
                Parameters[2] = new SqlParameter("@Url", url);
                Parameters[3] = new SqlParameter("@SectionCd", SectionCd);
                var execResult = objDataEngine.ExecuteCommand("WebUserAccessURL", CommandType.StoredProcedure, Parameters);
                while (execResult.Read())
                {
                    _AccessibilityList.Add(new Accessibility
                    {
                        CtrlId = Convert.ToString(execResult["CtrlName"]),
                        Sts = Convert.ToInt16(execResult["Sts"]),
                        SectionName = Convert.ToString(execResult["Section Name"]),
                        Page = Convert.ToString(execResult["Page"]),
                        SectionShortCd = Convert.ToString(execResult["SectionCd"])
                    });
                };
            }
            finally
            {

                objDataEngine.CloseConnection();
            } 

            if (!_AccessibilityList.Any() || _AccessibilityList.First().Sts == 0)
            {
                filterContext.Result = new HttpStatusCodeResult(403);
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                base.OnActionExecuting(filterContext);
            }
            var _SectionInfo = _AccessibilityList.FirstOrDefault(p => p.SectionShortCd.ToLower() == SectionCd.ToLower());
            if (_SectionInfo == null)
            {
                filterContext.Result = new HttpStatusCodeResult(403);
                base.OnActionExecuting(filterContext);
            }
            filterContext.Controller.ViewData["Accessibility"] = _AccessibilityList;
            filterContext.HttpContext.Session["CtrlAccessibility"] = _AccessibilityList;
            base.OnActionExecuting(filterContext);
        }

    }

    /// <summary>
    /// Checking on Page accessibility with section
    /// </summary>
    public class AccessibilitySectionAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var _AccessibilityList = new List<Accessibility>();

            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var target = new StringBuilder();
                var OverRideController = filterContext.RequestContext.HttpContext.Request.QueryString["overrideController"];
                if (OverRideController != null)
                {
                    target.Append(OverRideController);
                }
                else
                {
                    target.Append(filterContext.RouteData.Values["Controller"]);
                }

                string ovverrideController = filterContext.RequestContext.HttpContext.Request.QueryString["type"];
                if (String.IsNullOrEmpty(ovverrideController))
                    ovverrideController = "Index";

                target.Append("/").Append(ovverrideController);

                var url = target.ToString();
                //if (url == "ApplicantCard/Index")
                //    url = "Applicant/Index";
                var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);

                try
                {
                    objDataEngine.InitiateConnection();
                    SqlParameter[] Parameters = new SqlParameter[4];
                    Parameters[0] = new SqlParameter("@Flag", 1);
                    Parameters[1] = new SqlParameter("@UserId", filterContext.HttpContext.User.Identity.Name);
                    Parameters[2] = new SqlParameter("@Url", url);
                    Parameters[3] = new SqlParameter("@SectionCd", "NULL");
                    var execResult = objDataEngine.ExecuteCommand("WebUserAccessURL", CommandType.StoredProcedure, Parameters);
                    while (execResult.Read())
                    {
                        _AccessibilityList.Add(new Accessibility
                        {
                            CtrlId = Convert.ToString(execResult["CtrlName"]),
                            Sts = Convert.ToInt16(execResult["Sts"]),
                            SectionName = Convert.ToString(execResult["Section Name"]),
                            Page = Convert.ToString(execResult["Page"]),
                            SectionShortCd = Convert.ToString(execResult["SectionCd"]),
                            Sequence = Convert.ToInt16(execResult["Seq"])
                        });
                    };
                }
                finally
                {

                    objDataEngine.CloseConnection();
                }

                var SectionCd = filterContext.RequestContext.HttpContext.Request.QueryString["Prefix"] == null ? string.Empty : filterContext.RequestContext.HttpContext.Request.QueryString["Prefix"];
                var _SectionInfo = _AccessibilityList.FirstOrDefault(p => p.SectionShortCd.ToLower() == SectionCd.ToLower());


                if (!_AccessibilityList.Any() || _AccessibilityList.First().Sts == 0)
                {
                    filterContext.Result = new HttpStatusCodeResult(403);
                }
                else if (_SectionInfo == null)
                {
                    filterContext.Result = new HttpStatusCodeResult(403);
                    base.OnActionExecuting(filterContext);
                }


                else
                {
                    filterContext.Controller.ViewData["Accessibility"] = _AccessibilityList;
                    filterContext.HttpContext.Session["CtrlAccessibility"] = _AccessibilityList;
                }
            }
            else
            {
                if (filterContext.HttpContext.Session["CtrlAccessibility"] != null)
                {
                    _AccessibilityList = (List<Accessibility>)filterContext.HttpContext.Session["CtrlAccessibility"];
                    filterContext.Controller.ViewData["Accessibility"] = _AccessibilityList;
                }
                else
                {
                    filterContext.Result = new HttpStatusCodeResult(403);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}