using Utilities.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using FleetOps.Models;

namespace FleetOps.App_Start
{
    public class CustomLoadAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if (filterContext.HttpContext.Session["ProcessId"] == null)
                {

                    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
                    objDataEngine.InitiateConnection();
                    SqlParameter[] Parameters = new SqlParameter[1];
                    Parameters[0] = new SqlParameter("@IssNo", 1);
                    var execResult = objDataEngine.ExecuteCommand("[WebGetPrcs]", CommandType.StoredProcedure, Parameters);
                    while (execResult.Read())
                    {
                        var ProcessId = Convert.ToString(execResult["PrcsId"]);
                        var ProcessDate = BaseClass.DateConverter(execResult["PrcsDate"]);
                        filterContext.HttpContext.Session["ProcessId"] = ProcessId;
                        filterContext.HttpContext.Session["ProcessDate"] = ProcessDate;
                    };
                }
            }
            base.OnActionExecuting(filterContext);
        }


    }
}