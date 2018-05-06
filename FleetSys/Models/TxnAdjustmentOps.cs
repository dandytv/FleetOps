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
    public class TxnAdjustmentOps : BaseClass
    {
        int RcptNo, RetCd;

        public async Task<List<TxnAdjustment>> GetTxnAdjustmentList(string AcctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebTxnAdjustmentListSelect", CommandType.StoredProcedure, Parameters);
                var _TxnAdjustmentList = new List<TxnAdjustment>();
                var _TxnAdjustmentInfo = new List<TxnAdjustment>();

                while (execResult.Read())
                {
                    _TxnAdjustmentList.Add(new TxnAdjustment
                    {
                        RefType = Convert.ToString(execResult["Txn Type"]),
                        TxnDate = DateConverter(execResult["Txn Date"]),
                        TotAmnt = ConverterDecimal(execResult["Txn Amount"]),
                        DisplayTotAmt = ConverterDecimal(execResult["Txn Amount"]),
                        BillingTxnAmt = ConverterDecimal(execResult["Billing Amount"]),
                        Descp = Convert.ToString(execResult["Txn Description"]),
                        StsDescp = Convert.ToString(execResult["Status"]),
                        SelectedSts = Convert.ToString(execResult["Sts"]),
                        UserId = Convert.ToString(execResult["User Id"]),//R
                        SelectedTxnCd = Convert.ToString(execResult["TxnCd"]),
                        TxnId = Convert.ToString(execResult["Txn Id"]),//R
                        WithHeldUnsettleId = ConvertInt(execResult["WU Id"]),//R
                        CreationDate = DateConverter(execResult["Creation Date"]),//R
                        AppvRemarks = Convert.ToString(execResult["AppvRemarks"]),
                        _CardnAccNo = new CardnAccNo
                        {
                            AccNo = Convert.ToString(execResult["Account No"]),
                            CardNo = Convert.ToString(execResult["Card No"])
                        }

                    });
                };
                return _TxnAdjustmentList;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<TxnAdjustment> GetTxnAdjustmentDetail(TxnAdjustment _TxnAdjustment)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_TxnAdjustment.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _TxnAdjustment.TxnId);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebTxnAdjustmentSelect", CommandType.StoredProcedure, Parameters);

                var _GetTxnAdjustmentDetail = new TxnAdjustment();
                _GetTxnAdjustmentDetail._CardnAccNo = new CardnAccNo();
                while (execResult.Read())
                {
                    _GetTxnAdjustmentDetail.TxnId = Convert.ToString(execResult["TxnId"]);
                    _GetTxnAdjustmentDetail.TxnDate = DateConverter(execResult["TxnDate"]);
                    _GetTxnAdjustmentDetail.DueDate = DateConverter(execResult["DueDate"]);
                    _GetTxnAdjustmentDetail.SelectedTxnCd = Convert.ToString(execResult["TxnCd"]);
                    _GetTxnAdjustmentDetail._CardnAccNo.CardNo = Convert.ToString(execResult["CardNo"]);
                    _GetTxnAdjustmentDetail.TotAmnt = ConverterDecimal(execResult["TxnAmt"]);
                    _GetTxnAdjustmentDetail.Totpts = ConverterDecimal(execResult["Pts"]);
                    _GetTxnAdjustmentDetail.Descp = Convert.ToString(execResult["Descp"]);
                    _GetTxnAdjustmentDetail.AppvCd = Convert.ToString(execResult["AppvCd"]);
                    _GetTxnAdjustmentDetail.SelectedSts = Convert.ToString(execResult["Sts"]);
                    _GetTxnAdjustmentDetail.UserId = Convert.ToString(execResult["UserId"]);
                    _GetTxnAdjustmentDetail.WithHeldUnsettleId = ConvertInt(execResult["WithheldUnsettleId"]);
                    _GetTxnAdjustmentDetail.CreationDate = DateConverter(execResult["CreationDate"]);
                    _GetTxnAdjustmentDetail.SelectedOwner = Convert.ToString(execResult["Owner"]);
                };

                //if (string.IsNullOrEmpty(_GetTxnAdjustmentDetail.SelectedSts))
                //{
                //    _GetTxnAdjustmentDetail.SelectedSts = "H";
                //}
                return _GetTxnAdjustmentDetail;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<MsgRetriever> SaveTxnAdjustment(TxnAdjustment _TxnAdjustment)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[19];

                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_TxnAdjustment.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _TxnAdjustment.TxnId);
                Parameters[2] = String.IsNullOrEmpty(_TxnAdjustment._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _TxnAdjustment._CardnAccNo.AccNo);
                Parameters[3] = String.IsNullOrEmpty(_TxnAdjustment._CardnAccNo.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _TxnAdjustment._CardnAccNo.CardNo);
                Parameters[4] = String.IsNullOrEmpty(_TxnAdjustment.SelectedTxnCd) ? new SqlParameter("@TxnCd", DBNull.Value) : new SqlParameter("@TxnCd", _TxnAdjustment.SelectedTxnCd);

                Parameters[5] = new SqlParameter("@TxnDate", ConvertDatetimeDB(_TxnAdjustment.TxnDate));
                Parameters[6] = String.IsNullOrEmpty(_TxnAdjustment.DueDate) ? new SqlParameter("@DueDate", DBNull.Value) : new SqlParameter("@DueDate", ConvertDatetimeDB(_TxnAdjustment.DueDate));
                Parameters[7] = new SqlParameter("@TxnAmt", ConvertDecimalToDb(_TxnAdjustment.TotAmnt));
                Parameters[8] = new SqlParameter("@Pts", ConvertDecimalToDb(_TxnAdjustment.Totpts));
                Parameters[9] = String.IsNullOrEmpty(_TxnAdjustment.Descp) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _TxnAdjustment.Descp);
                Parameters[10] = String.IsNullOrEmpty(_TxnAdjustment.AppvCd) ? new SqlParameter("@AppvCd", DBNull.Value) : new SqlParameter("@AppvCd", _TxnAdjustment.AppvCd);
                Parameters[11] = new SqlParameter("@CheqNo", ConvertLongToDb(_TxnAdjustment.ChequeNo));
                Parameters[12] = String.IsNullOrEmpty(_TxnAdjustment.SelectedSts) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _TxnAdjustment.SelectedSts);

                Parameters[13] = new SqlParameter("@RcptNo", SqlDbType.BigInt);
                Parameters[13].Direction = ParameterDirection.Output;
                Parameters[14] = new SqlParameter("@RetCd", SqlDbType.BigInt);
                Parameters[14].Direction = ParameterDirection.Output;
                Parameters[15] = String.IsNullOrEmpty(System.Web.HttpContext.Current.User.Identity.Name) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", System.Web.HttpContext.Current.User.Identity.Name);

                Parameters[16] = String.IsNullOrEmpty(_TxnAdjustment.SelectedOwner) ? new SqlParameter("@Owner", DBNull.Value) : new SqlParameter("@Owner", _TxnAdjustment.SelectedOwner);
                Parameters[17] = String.IsNullOrEmpty(_TxnAdjustment.DeftBusnLocation) ? new SqlParameter("@Dealer", DBNull.Value) : new SqlParameter("@Dealer", _TxnAdjustment.DeftBusnLocation);
                Parameters[18] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[18].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebTxnAdjustmentMaint", CommandType.StoredProcedure, Parameters);
                var Result = ConvertInt(Cmd.Parameters["@RETURN_VALUE"].Value);
                this.RcptNo = ConvertInt(Cmd.Parameters[13].Value);
                this.RetCd = ConvertInt(Cmd.Parameters[14].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<MsgRetriever> DelTxnAdjustment(TxnAdjustment _TxnAdjustment)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[5];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_TxnAdjustment.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _TxnAdjustment.TxnId);
                Parameters[2] = String.IsNullOrEmpty(_TxnAdjustment._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _TxnAdjustment._CardnAccNo.AccNo);
                Parameters[3] = String.IsNullOrEmpty(_TxnAdjustment._CardnAccNo.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _TxnAdjustment._CardnAccNo.CardNo);
                Parameters[4] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[4].Direction = ParameterDirection.ReturnValue;
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebTxnAdjustmentDelete", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
    }
}