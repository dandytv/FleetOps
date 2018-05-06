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

namespace FleetSys.Models
{
    public class MultipleTxnOps : BaseClass
    {
        public string BatchId { get; set; }
        public int RetCd { get; set; }
        public int ChequeNo { get; set; }
        #region "MultipleTxn Adj"
        public async Task<MsgRetriever> ftMultipleAdjMaint(TxnAdjustment _MultipleTxn)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();

                DataTable dt = new DataTable();

                dt.Columns.Add("Acctno");
                dt.Columns.Add("Cardno", typeof(string));//
                dt.Columns.Add("TxnAmt");
                dt.Columns.Add("Descp");
                dt.Columns.Add("InvoiceNo");
                dt.Columns.Add("AppvCd");
                dt.Columns.Add("DeftBusnLocation");
                dt.Columns.Add("DeftTermId");
                dt.Columns.Add("Owner");
                dt.Columns.Add("Sts");
                dt.Columns.Add("TxnDate");
                dt.Columns.Add("DueDate");
                dt.Columns.Add("TxnCd");
                dt.Columns.Add("TxnId");
                //dt.Columns.Add("TxnId")
                foreach (var item in _MultipleTxn.MultipleTxnRecord)
                {
                    DataRow dr = dt.NewRow();
                    dr["Acctno"] = item.AcctNo;
                    dr["Cardno"] = item.CardNo;
                    if (!string.IsNullOrEmpty(item.TxnAmt))
                    {
                        dr["TxnAmt"] = ConvertDecimalToDb(item.TxnAmt);
                    }
                    else
                    {
                        dr["TxnAmt"] = DBNull.Value;
                    }
                    dr["Descp"] = item.TxnDescp;
                    dr["InvoiceNo"] = item.InvoiceNo;
                    dr["AppvCd"] = item.AppvCd;
                    dr["DeftBusnLocation"] = item.DeftBusnLocation;
                    dr["DeftTermId"] = item.DeftTermId;
                    dr["Owner"] = _MultipleTxn.SelectedOwner;
                    dr["Sts"] = item.SelectedSts;
                    dr["TxnDate"] = ConvertDatetimeDB(_MultipleTxn.TxnDate);
                    dr["DueDate"] = DBNull.Value;
                    dr["TxnCd"] = _MultipleTxn.SelectedAdjTxnCode;
                    dr["TxnId"] = item.TxnId;
                    //dr["RcptNo"] = DBNull.Value;
                    dt.Rows.Add(dr);
                }

                SqlParameter[] Parameters = new SqlParameter[11];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                //Parameters[1] = !string.IsNullOrEmpty(_MultipleTxn.CheqNo) ? new SqlParameter("@CheqNo", _MultipleTxn.CheqNo) : new SqlParameter("@CheqNo", DBNull.Value);
                Parameters[1] = !string.IsNullOrEmpty(_MultipleTxn.ChequeAmt) ? new SqlParameter("@CheqAmt", ConvertDecimalToDb(_MultipleTxn.ChequeAmt)) : new SqlParameter("@CheqAmt", DBNull.Value);
                Parameters[2] = new SqlParameter("@UserId", GetUserId);
                Parameters[3] = new SqlParameter("@Adjustment", dt);
                Parameters[4] = String.IsNullOrEmpty(_MultipleTxn.ChequeNo) ? new SqlParameter("@RcptNo", DBNull.Value) : new SqlParameter("@RcptNo", _MultipleTxn.ChequeNo);//


                //Parameters[5] = new SqlParameter("@RcptNo", SqlDbType.VarChar, 19);
                //Parameters[5].Direction = ParameterDirection.Output;
                Parameters[5] = new SqlParameter("@Batch", SqlDbType.VarChar, 25);
                Parameters[5].Direction = ParameterDirection.Output;
                Parameters[6] = new SqlParameter("@RetCd", SqlDbType.Int);
                Parameters[6].Direction = ParameterDirection.Output;
                Parameters[7] = new SqlParameter("@RcptOut", SqlDbType.Int);
                Parameters[7].Direction = ParameterDirection.Output;
                Parameters[8] = string.IsNullOrEmpty(_MultipleTxn.BatchId) ? new SqlParameter("@BatchId", DBNull.Value) : new SqlParameter("@BatchId", _MultipleTxn.BatchId);
                Parameters[9] = string.IsNullOrEmpty(_MultipleTxn.SelectedOwner) ? new SqlParameter("@Owner", DBNull.Value) : new SqlParameter("@Owner", _MultipleTxn.SelectedOwner);
                Parameters[10] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[10].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebMultiTxnAdjustmentMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                this.BatchId = Convert.ToString(Cmd.Parameters["@Batch"].Value);
                this.RetCd = ConvertInt(Cmd.Parameters["@RetCd"].Value);
                this.ChequeNo = ConvertInt(Cmd.Parameters["@RcptOut"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

        }
        public async Task<List<TxnAdjustment>> WebMultiTxnAdjustmentListSelect()
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[1];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebMultiTxnAdjustmentListSelect", CommandType.StoredProcedure, Parameters);
                var _List = new List<TxnAdjustment>();
                while (execResult.Read())
                {
                    _List.Add(new TxnAdjustment
                    {
                        BatchId = Convert.ToString(execResult["Batchid"]),
                        TxnNo = Convert.ToString(execResult["Txn No"]),
                        ChequeAmt = ConverterDecimal(execResult["Cheque Amt"]),
                        DisplayTotAmt = ConverterDecimal(execResult["Total Txn Amt"]),
                        CreationDate = Convert.ToString(execResult["CreationDate"]),
                        CreatedBy = Convert.ToString(execResult["User Id"]),
                        SelectedTxnCode = Convert.ToString(execResult["TxnCd"]),
                        SelectedOwner = Convert.ToString(execResult["Owner"]),
                        SelectedSts = Convert.ToString(execResult["Sts"]),
                        ChequeNo = Convert.ToString(execResult["Rrn"])
                    });
                }
                return _List;
            }
            finally
            {

                objDataEngine.CloseConnection();
            }

        }

        public async Task<TxnAdjustment> WebMultiTxnAdjustmentSelect(string BatchId, TxnAdjustment _Txn)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@BatchId", BatchId);
                Parameters[1] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[2] = String.IsNullOrEmpty(_Txn.ChequeNo) ? new SqlParameter("@Rrn", DBNull.Value) : new SqlParameter("@Rrn", _Txn.ChequeNo);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebMultiTxnAdjustmentSelect", CommandType.StoredProcedure, Parameters);
                var _Adj = new TxnAdjustment();
                var txnList = new List<MultipleTxnRecord>();
                while (execResult.Read())
                {
                    _Adj.TxnDate = DateConverter(execResult["Txn Date"]);
                    _Adj.ChequeAmt = ConverterDecimal(execResult["Cheque Amt"]);
                    _Adj.ChequeNo = Convert.ToString(execResult["ChequeNo"]);
                    _Adj.BillingTxnAmt = Convert.ToString(execResult["Billing Amount"]);
                    _Adj.UserId = Convert.ToString(execResult["User Id"]);
                    _Adj.WithHeldUnsettleId = ConvertInt(execResult["WU Id"]);
                    _Adj.CreationDate = Convert.ToString(execResult["Creation Date"]);
                    _Adj.BatchId = Convert.ToString(execResult["BatchId"]);
                    _Adj.SelectedOwner = Convert.ToString(execResult["Owner"]);
                    _Adj.SelectedSts = Convert.ToString(execResult["Sts"]);
                    //_Adj.SelectedPaymentType = Convert.ToString(execResult["PymtType"]);
                    _Adj.SelectedAdjTxnCode = Convert.ToString(execResult["TxnCd"]);
                    //_Adj.SelectedAdjTxnCode = Convert.ToString(execResult["TxnCd"]);
                    _Adj.SelectedTxnType = Convert.ToString(execResult["Txn Type"]);

                    _Adj.SelectedPaymentType = Convert.ToString(execResult["PymtType"]);

                    //_Adj.SelectedAdjTxnCode = Request.QueryString["TxnCd"];
                    //_Adj.ShortDescp = Convert.ToString(execResult["ShortDescp"]);

                    txnList.Add(new MultipleTxnRecord
                    {
                        AcctNo = ConvertInt(execResult["Account No"]),
                        CardNo = Convert.ToString(execResult["Card No"]),
                        TxnAmt = ConverterDecimal(execResult["Txn Amount"]),
                        TxnDescp = Convert.ToString(execResult["Txn Description"]),
                        AppvCd = Convert.ToString(execResult["AppvCd"]),
                        DeftBusnLocation = Convert.ToString(execResult["Dealer"]),
                        DeftTermId = Convert.ToString(execResult["TermId"]),
                        SelectedOwner = Convert.ToString(execResult["Owner"]),
                        SelectedStsDescp = Convert.ToString(execResult["Status"]),
                        SelectedSts = Convert.ToString(execResult["Sts"]),
                        TxnId = Convert.ToString(execResult["TxnId"]),
                        AcctName = Convert.ToString(execResult["AccountName"])
                    });
                }
                if (txnList.Any())
                {
                    _Adj.MultipleTxnRecord = txnList;
                }
                return _Adj;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

        }

        public async Task<List<MultiPayment>> WebGetGLCode(MultiPayment _multi)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = String.IsNullOrEmpty(_multi.SelectedAdjTxnCode) ? new SqlParameter("@TxnCd", DBNull.Value) : new SqlParameter("@TxnCd", _multi.SelectedAdjTxnCode);
                Parameters[1] = new SqlParameter("@SettleVal",1000);
                Parameters[2] = String.IsNullOrEmpty(_multi.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _multi.AcctNo);
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

        #endregion

        #region "MultiplePaymentTxn"
        //public async Task<List<MultiPayment>> WebMultiPaymentTxnList()
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
        //    objDataEngine.InitiateConnection();
        //    SqlParameter[] Parameters = new SqlParameter[1];
        //    Parameters[0] = new SqlParameter("@IssNo", GetIssNo);

        //    var execResult = await objDataEngine.ExecuteCommandAsync("WebMultiPaymentListSelect", CommandType.StoredProcedure, Parameters);
        //    var _List = new List<MultiPayment>();

        //    while (execResult.Read())
        //    {
        //        _List.Add(new MultiPayment
        //            {
        //                BatchId = Convert.ToString(execResult["BatchId"]),
        //                AcctNo = Convert.ToString(execResult["AcctNo"]),
        //                TxnDate = Convert.ToString(execResult["TxnDate"]),
        //                TxnType = Convert.ToString(execResult["TxnType"]),
        //                TxnDescp = Convert.ToString(execResult[""])
        //            })
        //    }
        //}
        #endregion
    }
}