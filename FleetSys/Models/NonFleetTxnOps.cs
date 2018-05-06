using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using Utilities.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using FleetOps.Models;
using FleetOps.ViewModel;
using ModelSector;
using CCMS.ModelSector;
using System.Threading.Tasks;

namespace FleetOps.Models
{
    public class NonFleetTxnOps : BaseClass
    {
        public List<NonFleetTxn> GetNonFleetTxnList(NonFleetTxn _NonFleetTxn)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[1];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);

                var execResult = objDataEngine.ExecuteCommand("WebNonFleetTxnListSelect", CommandType.StoredProcedure, Parameters);
                var _nonFleetTxn = new List<NonFleetTxn>();
                while (execResult.Read())
                {
                    _nonFleetTxn.Add(new NonFleetTxn
                    {
                        SelectedTxnCd = Convert.ToString(execResult["Txn Code"]),
                        Descp = Convert.ToString(execResult["Txn Description"]),
                        TotAmnt = ConverterDecimal(execResult["Txn Amount"]),
                        DisplayTotAmnt = ConverterDecimal(execResult["Txn Amount"]),
                        TxnDate = DateConverter(execResult["Txn Date"]),
                        DbCrInd = Convert.ToString(execResult["DbCr"]),
                        Account = Convert.ToString(execResult["Acct"]),
                        UserId = Convert.ToString(execResult["User Id"]),
                        TxnId = Convert.ToString(execResult["Txn Id"]),

                    });
                };
                return _nonFleetTxn;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public NonFleetTxn GetNonFleetTxnDetail(NonFleetTxn _NonFleetTxn, string id)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {

                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@TxnId", String.IsNullOrEmpty(_NonFleetTxn.TxnId) ? "" : _NonFleetTxn.TxnId);

                var execResult = objDataEngine.ExecuteCommand("WebNonFleetTxnSelect", CommandType.StoredProcedure, Parameters);

                var _GetNonFleetTxnDetail = new NonFleetTxn();
                _GetNonFleetTxnDetail._CardnAccNo = new CardnAccNo();
                while (execResult.Read())
                {
                    // _GetNonFleetTxnDetail.TxnCd = BaseClass.WebGetTxnCode(id);
                    _GetNonFleetTxnDetail.SelectedTxnCd = Convert.ToString(execResult["Txn Code"]);
                    _GetNonFleetTxnDetail.Descp = Convert.ToString(execResult["Txn Description"]);
                    _GetNonFleetTxnDetail.TotAmnt = ConverterDecimal(execResult["Txn Amount"]);
                    _GetNonFleetTxnDetail.TxnDate = DateConverter(execResult["Txn Date"]);
                    _GetNonFleetTxnDetail.UserId = Convert.ToString(execResult["User Id"]);
                    _GetNonFleetTxnDetail.TxnId = Convert.ToString(execResult["Txn Id"]);
                    //  _GetNonFleetTxnDetail.Affiliate = BaseClass.WebGetAffiliate();
                    _GetNonFleetTxnDetail.SelectedAffiliate = Convert.ToString(execResult["Affiliate"]);
                    _GetNonFleetTxnDetail.remarks = Convert.ToString(execResult["Remarks"]);
                };
                return _GetNonFleetTxnDetail;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<MsgRetriever> SaveNonFleetTxn(NonFleetTxn _NonFleetTxn)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[10];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_NonFleetTxn.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _NonFleetTxn.TxnId);
                Parameters[2] = String.IsNullOrEmpty(_NonFleetTxn.SelectedTxnCd) ? new SqlParameter("@TxnCd", DBNull.Value) : new SqlParameter("@TxnCd", _NonFleetTxn.SelectedTxnCd);
                Parameters[3] = new SqlParameter("@TxnAmt", ConvertDecimalToDb(_NonFleetTxn.TotAmnt));
                Parameters[4] = new SqlParameter("@TxnDate", DateConverterDB(_NonFleetTxn.TxnDate));
                Parameters[5] = String.IsNullOrEmpty(_NonFleetTxn.SelectedAffiliate) ? new SqlParameter("@Affiliate", DBNull.Value) : new SqlParameter("@Affiliate", _NonFleetTxn.SelectedAffiliate);
                Parameters[6] = String.IsNullOrEmpty(_NonFleetTxn.Descp) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _NonFleetTxn.Descp);
                Parameters[7] = String.IsNullOrEmpty(_NonFleetTxn.remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", _NonFleetTxn.remarks);
                Parameters[8] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
                Parameters[9] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[9].Direction = ParameterDirection.ReturnValue;

                var Cmd = objDataEngine.ExecuteWithReturnValue("WebNonFleetTxnMaint", CommandType.StoredProcedure, Parameters);
                var Result = ConvertInt(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = GetMessageCode(Result);

                return await Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<MsgRetriever> DelNonFleetTxn(NonFleetTxn _NonFleetTxnDel)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[5];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_NonFleetTxnDel.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _NonFleetTxnDel.TxnId);
                Parameters[2] = String.IsNullOrEmpty(_NonFleetTxnDel._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _NonFleetTxnDel._CardnAccNo.AccNo);
                Parameters[3] = String.IsNullOrEmpty(_NonFleetTxnDel._CardnAccNo.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _NonFleetTxnDel._CardnAccNo.CardNo);
                Parameters[4] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[4].Direction = ParameterDirection.ReturnValue;

                var Cmd = objDataEngine.ExecuteWithReturnValue("WebTxnAdjustmentDelete", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = GetMessageCode(Result);

                return await Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

    }
}