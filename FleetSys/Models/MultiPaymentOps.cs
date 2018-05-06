using CCMS.ModelSector;
using Utilities.DAL;
using FleetOps.Models;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace FleetSys.Models
{
    public class MultiPaymentOps : BaseClass
    {
        public string BatchId { get; set; }
        public async Task<MsgRetriever> WebMultiPaymentMaint(TxnAdjustment _MultiPayment)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                DataTable dt = new DataTable();

                dt.Columns.Add("TxnId");
                dt.Columns.Add("AcctNo", typeof(long));
                dt.Columns.Add("PymtAmt", typeof(decimal));
                dt.Columns.Add("Descp");

                foreach (var item in _MultiPayment.MultipleTxnRecord)
                {
                    DataRow dr = dt.NewRow();
                    dr["AcctNo"] = item.AcctNo;
                    if (!string.IsNullOrEmpty(item.TxnAmt))
                    {
                        dr["PymtAmt"] = ConvertDecimalToDb(item.TxnAmt);
                    }
                    else
                    {
                        dr["PymtAmt"] = DBNull.Value;
                    }
                    dr["Descp"] = item.TxnDescp;
                    dr["TxnId"] = item.TxnId;
                    dt.Rows.Add(dr);
                }

                SqlParameter[] Parameters = new SqlParameter[16];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@UserId", GetUserId);
                Parameters[2] = new SqlParameter("@Payment", dt);
                // Parameters[2].SqlDbType = SqlDbType.Structured;
                Parameters[3] = new SqlParameter("@TtlPymt", _MultiPayment.ChequeAmt);
                Parameters[4] = String.IsNullOrEmpty(_MultiPayment.BatchId) ? new SqlParameter("@BatchId", DBNull.Value) : new SqlParameter("@BatchId", _MultiPayment.BatchId);
                Parameters[5] = new SqlParameter("@RefKey", _MultiPayment.RefId);
                Parameters[6] = new SqlParameter("@ChequeNo", _MultiPayment.ChequeNo);
                Parameters[7] = new SqlParameter("@TxnCd", _MultiPayment.SelectedTxnCode);
                Parameters[8] = new SqlParameter("@TxnDate", DateConverterDB(_MultiPayment.TxnDate));
                Parameters[9] = new SqlParameter("@DueDate", DateConverterDB(_MultiPayment.DueDate));
                Parameters[10] = String.IsNullOrEmpty(_MultiPayment.SlipNo) ? new SqlParameter("@SlipNo", DBNull.Value) : new SqlParameter("@SlipNo", _MultiPayment.SlipNo);
                Parameters[11] = new SqlParameter("@IssueingBankCd", _MultiPayment.SelectedIssueingBank);
                Parameters[12] = new SqlParameter("@Owner", _MultiPayment.SelectedOwner);
                Parameters[13] = new SqlParameter("@Batch", SqlDbType.VarChar, 25);
                Parameters[13].Direction = ParameterDirection.Output;
                Parameters[14] = String.IsNullOrEmpty(_MultiPayment.SelectedGLSettlement) ? new SqlParameter("@SettleVal", DBNull.Value) : new SqlParameter("@SettleVal", _MultiPayment.SelectedGLSettlement);
                Parameters[15] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[15].Direction = ParameterDirection.ReturnValue;
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebMultiPaymentMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                this.BatchId = Convert.ToString(Cmd.Parameters["@Batch"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<List<MultiPayment>> WebMultiPaymentListSelect()
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);



            try
            {

                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[1];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebMultiPaymentListSelect", CommandType.StoredProcedure, Parameters);
                var _List = new List<MultiPayment>();
                while (execResult.Read())
                {
                    _List.Add(new MultiPayment
                    {
                        BatchId = Convert.ToString(execResult["BatchId"]),
                        //TxnId = Convert.ToString(execResult["Txn No"]),
                         RefNo= Convert.ToString(execResult["RefNo"]),
                        ChequeNo = Convert.ToString(execResult["ChequeNo"]),
                        BillingAmt = ConverterDecimal(execResult["BatchTotalAmt"]),
                        CreationDate = Convert.ToString(execResult["CreationDate"]),
                        SelectedOwner = Convert.ToString(execResult["Owner"]),
                        SelectedTxnCode = Convert.ToString(execResult["TxnCdDescp"]),
                        TxnCnt = Convert.ToString(execResult["Txn Cnt"]),
                        //TxnAmt = Convert.ToString(execResult["SubTotalAmt"]),
                        SelectedSts = Convert.ToString(execResult["AppvSts"])
                    });
                }
                return _List;
            }
            finally
            {

                objDataEngine.CloseConnection();
            }


        }

        public async Task<MultiPayment> WebMultiPaymentSelect(string BatchId, MultiPayment _MultiPymt)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[4];
                Parameters[0] = new SqlParameter("@BatchId", BatchId);
                Parameters[1] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[2] = String.IsNullOrEmpty(_MultiPymt.ChequeAmt) ? new SqlParameter("@TxnAmt", DBNull.Value) : new SqlParameter("@TxnAmt", SqlDbType.Decimal);
                Parameters[3] = String.IsNullOrEmpty(_MultiPymt.ChequeNo) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", _MultiPymt.ChequeNo);// SqlDbType.VarChar, 12
                //Parameters[3].Direction = ParameterDirection.Output;

                var execResult = await objDataEngine.ExecuteCommandAsync("WebMultiPaymentSelect", CommandType.StoredProcedure, Parameters);
                var _Payment = new MultiPayment();
                var txnList = new List<MultipleTxnRecord>();
                while (execResult.Read())
                {
                    _Payment.TxnId = Convert.ToString(execResult["TxnId"]);
                    _Payment.TxnDate = DateConverter(execResult["TxnDate"]);
                    _Payment.DueDate = DateConverter(execResult["DueDate"]);
                    _Payment.RefNo = Convert.ToString(execResult["RefNo"]);
                    _Payment.SelectedTxnCode = Convert.ToString(execResult["TxnCd"]);
                    _Payment.ChequeNo = Convert.ToString(execResult["ChequeNo"]);
                    _Payment.ChequeAmt = ConverterDecimal(execResult["ChequeAmt"]);
                    _Payment.CreationDate = DateConverter(execResult["CreationDate"]);
                    _Payment.SelectedOwner = Convert.ToString(execResult["Owner"]);
                    _Payment.SelectedIssueingBank = Convert.ToString(execResult["IssuingBank"]);
                    _Payment.SlipNo = Convert.ToString(execResult["SlipNo"]);
                    _Payment.SelectedSts = Convert.ToString(execResult["Sts"]);
                    _Payment.SelectedPaymentType = Convert.ToString(execResult["PymtType"]);
                    _Payment.SelectedGLSettlement = Convert.ToString(execResult["SettleVal"]);
                    txnList.Add(new MultipleTxnRecord
                    {
                        AcctNo = ConvertInt(execResult["AcctNo"]),
                        //CardNo = Convert.ToString(execResult["CardNo"]),
                        TxnAmt = ConverterDecimal(execResult["TxnAmt"]),
                        //BookingDate = DateConverter(execResult["BookingDate"]),
                        //Pts = Convert.ToString(execResult["Pts"]),
                        TxnDescp = Convert.ToString(execResult["Descp"]),
                        //AppvCd = Convert.ToString(execResult["AppvCd"]),
                        SelectedSts = Convert.ToString(execResult["Sts"]),
                        //WithHeldUnsettleId = Convert.ToString(execResult["WithheldUnsettleId"]),
                        ChequeNo = Convert.ToString(execResult["ChequeNo"]),
                        CreationDate = DateConverter(execResult["CreationDate"]),
                        SelectedOwner = Convert.ToString(execResult["Owner"]),
                        TxnId = Convert.ToString(execResult["TxnId"]),
                        AcctName = Convert.ToString(execResult["AccountName"])
                    });
                }
                if (txnList.Any())
                {
                    _Payment.MultipleTxnRecord = txnList;
                }
                return _Payment;
            }
            finally
            {

                objDataEngine.CloseConnection();
            }

        }

        //public MultiPayment WebGetGLCode(MultiPayment _Multipayment)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
        //    objDataEngine.InitiateConnection();
        //    SqlParameter[] Parameters = new SqlParameter[1];
        //    Parameters[0] = String.IsNullOrEmpty(_Multipayment.SelectedTxnCode) ? new SqlParameter("@TxnCd", DBNull.Value) : new SqlParameter("@TxnCd", _Multipayment.SelectedTxnCode);

        //    var execResult = objDataEngine.ExecuteCommand("WebGetGLCode", CommandType.StoredProcedure, Parameters);

        //    while (execResult.Read())
        //    {
        //        var _GLCode = new MultiPayment

        //        {
        //            GLTxnCode = Convert.ToString(execResult["AcctTxnCd"]),
        //            GLDescp = Convert.ToString(execResult["GLTxnDescp"]),
        //            GLCodeDescp = Convert.ToString(execResult["Descp"])

        //        };
        //        objDataEngine.CloseConnection();
        //        return _GLCode;
        //    }
        //    return new MultiPayment();
        //}

        public async Task<List<MultiPayment>> WebGetGLCode(MultiPayment _multi)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@TxnCd", ConvertLongToDb( _multi.SelectedTxnCode));
                Parameters[1] = String.IsNullOrEmpty(_multi.SelectedGLSettlement) ? new SqlParameter("@SettleVal", DBNull.Value) : new SqlParameter("@SettleVal", _multi.SelectedGLSettlement);
                Parameters[2] = new SqlParameter("@AcctNo",  ConvertLongToDb(_multi.AcctNo));
                var execResult = await objDataEngine.ExecuteCommandAsync("WebGetGLCode", CommandType.StoredProcedure, Parameters);
                var _List = new List<MultiPayment>();
                while (execResult.Read())
                {
                    _List.Add(new MultiPayment
                    {
                        GLTxnCode = Convert.ToString(execResult["GLAcctNo"]),
                        GLDescp = Convert.ToString(execResult["TxnType"]),
                        GLCodeDescp = Convert.ToString(execResult["Descp"])
                    });
                }
                return _List;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

        }

        public async Task<IEnumerable<SelectListItem>> WebGetPymtTxnCd(string GlSettlementCd, string TxnCat)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@TxnCat", TxnCat);
                Parameters[2] = String.IsNullOrEmpty(GlSettlementCd) ? new SqlParameter("@settleval", DBNull.Value) : new SqlParameter("@settleval", GlSettlementCd);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebGetPymtTxnCd", CommandType.StoredProcedure, Parameters);
                var _List = new List<SelectListItem>();
                while (execResult.Read())
                {
                    _List.Add(new SelectListItem
                    {
                        Text = Convert.ToString(execResult["Descp"]),
                        Value = Convert.ToString(execResult["TxnCd"])
                    });
                }
                return _List;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

        }
    }
}