using Utilities.DAL;
using ModelSector;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace FleetOps.Models
{
    public class ReportOps : BaseClass
    {

        public object GetReport(ReportViewer rpt)
        {
           
            var objDataEngine = new FleetDataEngine(AccessMode.Admin, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = string.IsNullOrEmpty(rpt.SelectedRptType) ? new SqlParameter("@RptType", DBNull.Value) : new SqlParameter("@RptType", rpt.SelectedRptType);
                Parameters[1] = string.IsNullOrEmpty(rpt.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", rpt.RefKey);
                Parameters[2] = new SqlParameter("@RptDate", ConvertDatetimeDB(rpt.Date));
                var Collector = objDataEngine.FillDataSet("WebRptViewer", CommandType.StoredProcedure, Parameters);
                var objDataInfo = GetInfo(Collector.Tables[0]);
                //  var objListTable = CloneTable(Collector.Tables[0], 2000);
                return objDataInfo;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

        }
        public DataTable GetTableReport(ReportViewer rpt)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();

                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@RptType", rpt.SelectedRptType);
                Parameters[1] = new SqlParameter("@RefKey", rpt.RefKey);
                Parameters[2] = new SqlParameter("@RptDate", ConvertDatetimeDB(rpt.Date));
                var Collector = objDataEngine.FillDt("WebRptViewer", CommandType.StoredProcedure, Parameters);
                return Collector;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public List<object> GetRowReport(ReportViewer rpt)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();

                SqlParameter[] Parameters = new SqlParameter[3];

                Parameters[0] = string.IsNullOrEmpty(rpt.SelectedRptType) ? new SqlParameter("@RptType", DBNull.Value) : new SqlParameter("@RptType", rpt.SelectedRptType);
                Parameters[1] = string.IsNullOrEmpty(rpt.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", rpt.RefKey);
                Parameters[2] = new SqlParameter("@RptDate", ConvertDatetimeDB(rpt.Date));
                var Collector = objDataEngine.FillDataSet("WebRptViewer", CommandType.StoredProcedure, Parameters);
                var datarow = Collector.Tables[0].AsEnumerable().ToList();
                List<object> Rows = new List<object>();
                foreach (var x in datarow)
                {
                    Rows.Add(x.ItemArray);
                }
                return Rows;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        public List<ReportBrowser> WebRptBrowser(ReportBrowser _Browser)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();

                SqlParameter[] Parameters = new SqlParameter[2];

                Parameters[0] = new SqlParameter("@UserID", GetUserId);
                Parameters[1] = string.IsNullOrEmpty(_Browser.ReportDate) ? new SqlParameter("@RptDate", DBNull.Value) : new SqlParameter("@RptDate", ConvertDatetimeDB(_Browser.ReportDate));

                var execResult = objDataEngine.ExecuteCommand("WebRptBrowser", CommandType.StoredProcedure, Parameters);
                var _Reports = new List<ReportBrowser>();

                while (execResult.Read())
                {
                    _Reports.Add(new ReportBrowser
                    {
                        FileName = Convert.ToString(execResult["DiskFileName"]),
                        ReportCategory = Convert.ToString(execResult["RptCategory"]),
                        ReportName = Convert.ToString(execResult["RptName"])
                    });

                };
                return _Reports;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
    }
}