using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utilities.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using FleetOps.Models;
using FleetOps.ViewModel;
using ModelSector;
using System.Collections;
using CCMS.ModelSector;


namespace FleetOps.Models
{
    public class AuditLogger : BaseClass
    {
        public List<AuditLoggerModel> GetAuditSearch(AuditLoggerModel _auditLog)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.Admin, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();

                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = string.IsNullOrEmpty(_auditLog.SelectedModule) ? new SqlParameter("@Module", DBNull.Value) : new SqlParameter("@Module", _auditLog.SelectedModule);
                Parameters[1] = string.IsNullOrEmpty(_auditLog.SelectedTblName) ? new SqlParameter("@TblName", DBNull.Value) : new SqlParameter("@TblName", _auditLog.SelectedTblName);
                Parameters[2] = new SqlParameter("@Date", ConvertDatetimeDB(_auditLog.Date));
                var execResult = objDataEngine.ExecuteCommand("WebAuditMaintSearch", CommandType.StoredProcedure, Parameters);
                var AuditSearch = new List<AuditLoggerModel>();

                while (execResult.Read())
                {
                    AuditSearch.Add(new AuditLoggerModel
                    {
                        SelectedTblName = Convert.ToString(execResult["TableName"]),
                        FieldName = Convert.ToString(execResult["FieldName"]),
                        RefKey = Convert.ToString(execResult["Ref Key"]),
                        ActionAud = Convert.ToString(execResult["Action"]),
                        OldData = Convert.ToString(execResult["Old Data"]),
                        NewData = Convert.ToString(execResult["New Data"]),
                        CreationDate = DateConverter(execResult["Creation Date"]),
                        //   UserId=Convert.ToString(execResult["UserId"])

                    });
                };
                return AuditSearch;
            }
            finally
            {

                objDataEngine.CloseConnection();
            }
        
        }
        //public MsgRetriever SaveAMaint(AuditLoggerModel _auditLog)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
        //    objDataEngine.InitiateConnection();
        //    SqlParameter[] Parameters = new SqlParameter[4];
        //    Parameters[0] = string.IsNullOrEmpty(_auditLog.SelectedModule) ? new SqlParameter("@Module", DBNull.Value) : new SqlParameter("@Module", _auditLog.SelectedModule);
        //    Parameters[1] = string.IsNullOrEmpty(_auditLog.TblName) ? new SqlParameter("@TblName", DBNull.Value) : new SqlParameter("@TblName", _auditLog.TblName);
        //    Parameters[2] = string.IsNullOrEmpty(_auditLog.Date) ? new SqlParameter("@Date", DBNull.Value) : new SqlParameter("@Date", _auditLog.Date);
        //    Parameters[3] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
        //    Parameters[3].Direction = ParameterDirection.ReturnValue;

        //    var Cmd = objDataEngine.ExecuteWithReturnValue("WebAuditMaintSearch", CommandType.StoredProcedure, Parameters);
        //    var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
        //    var Descp = GetMessageCode(Result);

        //    objDataEngine.CloseConnection();
        //    return Descp;

      //  }
        public AuditLoggerModel GetAudLoggerDetail(AuditLoggerModel _AudLogger)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);




            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = string.IsNullOrEmpty(_AudLogger.SelectedModule) ? new SqlParameter("@Module", DBNull.Value) : new SqlParameter("@Module", _AudLogger.SelectedModule);
                Parameters[1] = new SqlParameter("@AuditId", _AudLogger.AudId);

                var execResult = objDataEngine.ExecuteCommand("WebAuditMaintSelect", CommandType.StoredProcedure, Parameters);
                while (execResult.Read())
                {

                    var _auditLog = new AuditLoggerModel
                    {
                        AudId = Convert.ToString(execResult["AuditId"]),
                        SelectedTblName = Convert.ToString(execResult["TableName"]),
                        FieldName = Convert.ToString(execResult["FieldName"]),
                        RefKey = Convert.ToString(execResult["Ref Key"]),
                        subRefKey1 = Convert.ToString(execResult["Sub Ref Key1"]),
                        subRefKey2 = Convert.ToString(execResult["Sub Ref Key2"]),
                        ActionAud = Convert.ToString(execResult["Action"]),
                        OldData = Convert.ToString(execResult["Old Data"]),
                        NewData = Convert.ToString(execResult["New Data"]),
                        ActionBy = Convert.ToString(execResult["Actioned By"]),
                        CreationDate = Convert.ToString(execResult["Creation Date"]),
                    };
                    return _auditLog;
                };

                return new AuditLoggerModel();
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
    }
}