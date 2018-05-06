using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelSector;
using FleetOps.DAL;
using System.Data;
using System.Data.SqlClient;
using CCMS.ModelSector;
using System.Threading.Tasks;

namespace FleetOps.Models
{
    public class NegativeFileMaintOps : BaseClass
    {
        public List<NegativeFiles> WebNegativeUnlockListSelect(NegativeFiles Params)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[4];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(Params.SelectedInd) ? new SqlParameter("@Ind", DBNull.Value) : new SqlParameter("@Ind", Params.SelectedInd);
            Parameters[2] = String.IsNullOrEmpty(Params.SelectedRefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@refTo", Params.SelectedRefTo);
            Parameters[3] = String.IsNullOrEmpty(Params.SearchKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@refKey", Params.SearchKey);
            var execResult = objDataEngine.ExecuteCommand("WebNegativeUnlockListSelect", CommandType.StoredProcedure, Parameters);

            var _NegativeFiles = new List<NegativeFiles>();
            while (execResult.Read())
            {
                if (Params.SelectedInd == "E")
                {

                    _NegativeFiles.Add(new NegativeFiles
                    {
                        RefKey = Convert.ToString(execResult["RefKey"]),
                        LastUpdate = DateConverter(execResult["LastUpdDate"])
                    });
                }
                else if (Params.SelectedRefTo == "ACCT")
                {
                    _NegativeFiles.Add(new NegativeFiles
                  {
                      AcctNo = Convert.ToString(execResult["AcctNo"]),
                      //RefId = DateConverter(execResult["RefId"])
                      RefId = Convert.ToString(execResult["RefId"])
                  });
                }
                else
                {
                    _NegativeFiles.Add(new NegativeFiles
                    {
                        CardNo = Convert.ToString(execResult["CardNo"]),
                        RefId = Convert.ToString(execResult["RefId"])
                    });
                }
            };
            objDataEngine.CloseConnection();
            return _NegativeFiles;
        }

        public async Task<MsgRetriever> WebNegativeUnlockMaint(List<string> RefKey, string RefTo, string Func)
        {
            var Dt = new DataTable();
            Dt.Columns.Add("refKey");

            foreach (var item in RefKey)
            {
                var dr = Dt.NewRow();
                dr["RefKey"] = item;
                Dt.Rows.Add(dr);
            }
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[6];
            Parameters[0] = new SqlParameter("@Func", Func);
            Parameters[1] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[2] = String.IsNullOrEmpty(RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", RefTo);
            Parameters[3] = new SqlParameter("@RefKey", Dt);
            Parameters[4] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[5].Direction = ParameterDirection.ReturnValue;

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebNegativeUnlockMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return await Descp;

        }
    }
}