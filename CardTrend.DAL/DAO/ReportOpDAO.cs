using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.ReportViewer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAO
{
    public interface IReportOpDAO
    {
        object GetReport(ReportViewerDTO rpt);
        List<object> GetRowReport(ReportViewerDTO rpt);
        DataTable GetTableReport(ReportViewerDTO rpt);
    }
    public class ReportOpDAO : IReportOpDAO
    {
        private readonly string _connectionString = string.Empty;
        public ReportOpDAO(string connString)
        {
            _connectionString = connString;
        }
        public object GetReport(ReportViewerDTO rpt)
        {
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = string.IsNullOrEmpty(rpt.SelectedRptType) ? new SqlParameter("@RptType", DBNull.Value) : new SqlParameter("@RptType", rpt.SelectedRptType);
            Parameters[1] = string.IsNullOrEmpty(rpt.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", rpt.RefKey);
            Parameters[2] = new SqlParameter("@RptDate", NumberExtensions.ConvertDatetimeDB(rpt.Date));
            var Collector = FillDataSet("WebRptViewer", CommandType.StoredProcedure, Parameters);
            var objDataInfo = GetInfo(Collector.Tables[0]);
            return objDataInfo;
        }
        public List<object> GetRowReport(ReportViewerDTO rpt)
        {
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = string.IsNullOrEmpty(rpt.SelectedRptType) ? new SqlParameter("@RptType", DBNull.Value) : new SqlParameter("@RptType", rpt.SelectedRptType);
            Parameters[1] = string.IsNullOrEmpty(rpt.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", rpt.RefKey);
            Parameters[2] = new SqlParameter("@RptDate", NumberExtensions.ConvertDatetimeDB(rpt.Date));
            var Collector = FillDataSet("WebRptViewer", CommandType.StoredProcedure, Parameters);
            var dataRow = Collector.Tables[0].AsEnumerable().ToList();
            List<object> rows = new List<object>();
            foreach(var x in dataRow)
            {
                rows.Add(x.ItemArray);
            }
            return rows;
        }
        public DataTable GetTableReport(ReportViewerDTO rpt)
        {
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@RptType", rpt.SelectedRptType);
            Parameters[1] = new SqlParameter("@RefKey", rpt.RefKey);
            Parameters[2] = new SqlParameter("@RptDate", NumberExtensions.ConvertDatetimeDB(rpt.Date));
            var Collector = FillDataTable("WebRptViewer", CommandType.StoredProcedure, Parameters);
            return Collector;
        }
        public DataSet FillDataSet(string commandText, CommandType type, SqlParameter[] parameters)
        {
            SqlCommand sqlCommand = new SqlCommand();
            DataSet dataSet = new DataSet();
            using (var conn = new SqlConnection(_connectionString))
            {
                sqlCommand.CommandTimeout = 400;
                sqlCommand.Connection = conn;
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = type;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                if (parameters != null)
                    sqlCommand = this.AssignParameters(sqlCommand, parameters);
                sqlDataAdapter.Fill(dataSet);
                sqlDataAdapter.Dispose();
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
            }

            return dataSet;
        }
        public DataTable FillDataTable(string commandText, CommandType type, SqlParameter[] parameters)
        {
            SqlCommand sqlCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            using (var conn = new SqlConnection(_connectionString))
            {
                sqlCommand.CommandTimeout = 400;
                sqlCommand.Connection = conn;
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = type;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                if (parameters != null)
                    sqlCommand = this.AssignParameters(sqlCommand, parameters);
                sqlDataAdapter.Fill(dataTable);
                sqlDataAdapter.Dispose();
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
            }
            return dataTable;
        }
        private SqlCommand AssignParameters(SqlCommand cmd, SqlParameter[] cmdParameters)
        {
            if (cmdParameters == null)
                return cmd;
            foreach (SqlParameter cmdParameter in cmdParameters)
                cmd.Parameters.Add(cmdParameter);
            return cmd;
        }
        public object GetInfo(DataTable dt)
        {
            var datarow = dt.AsEnumerable().ToList();
            var filtered = datarow.Take(2000);
            List<object> Rows = new List<object>();
            string[] columnNames = dt.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();
            foreach (var x in datarow)
            {
                Rows.Add(x.ItemArray);
            }
            return new { columns = columnNames, rows = Rows };
        }
    }
}
