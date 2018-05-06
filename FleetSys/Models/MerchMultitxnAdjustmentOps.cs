using CCMS.ModelSector;
using FleetOps.Models;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Utilities.DAL;

namespace FleetSys.Models
{
    public class MerchMultitxnAdjustmentOps : BaseClass
    {
        public string RetCd { get; set; }
        public string BatchId { get; set; }
        public async Task<MsgRetriever> WebMerchantMultiTxnAdjustmentMaint(TxnAdjustment _MultiPayment)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                DataTable dt = new DataTable();
                dt.Columns.Add("Acctno", typeof(long));
                dt.Columns.Add("Cardno");
                dt.Columns.Add("TxnAmt", typeof(decimal)); 
                dt.Columns.Add("Descp"); 
                dt.Columns.Add("InvoiceNo");
                dt.Columns.Add("AppvCd"); 
                dt.Columns.Add("DeftBusnlocation");
                dt.Columns.Add("DeftTermId");
                dt.Columns.Add("Owner");
                dt.Columns.Add("Sts"); 
                dt.Columns.Add("TxnDate");
                dt.Columns.Add("DueDate"); 
                dt.Columns.Add("TxnCd"); 
                dt.Columns.Add("TxnId");

                foreach (var item in _MultiPayment.MultipleTxnRecord)
                {
                    DataRow dr = dt.NewRow();
                    dr["DeftBusnlocation"] = item.MerchantAcctNo;
                    if (!string.IsNullOrEmpty(item.TxnAmt))
                    {
                        dr["TxnAmt"] = ConvertDecimalToDb(item.TxnAmt);
                    }
                    else
                    {
                        dr["TxnAmt"] = DBNull.Value;
                    }
                    if(String.IsNullOrEmpty(item.Descp)){
                        dr["Descp"] = DBNull.Value;
                    }else{
                        dr["Descp"] = item.Descp;
                    } 
                    //dr["ChequeNo"] = _MultiPayment.CheqNo;
                    //dr["Owner"] = _MultiPayment.SelectedOwner;
                    dr["TxnCd"] = _MultiPayment.SelectedTxnCd;
                    dr["TxnDate"] = ConvertDatetimeDB( _MultiPayment.TxnDate);
                    dr["DueDate"] = ConvertDatetimeDB(_MultiPayment.DueDate);
                    dr["Owner"] = _MultiPayment.SelectedOwner;
                    if (String.IsNullOrEmpty(_MultiPayment.InvoiceNo))
                    {
                        dr["InvoiceNo"] = DBNull.Value;
                    }
                    else
                    {
                        dr["InvoiceNo"] = _MultiPayment.InvoiceNo;
                    }
                    if (String.IsNullOrEmpty(item.TxnId))
                    {
                        dr["TxnId"] = DBNull.Value;
                    }
                    else
                    {
                        dr["TxnId"] =item.TxnId;
                    }
                    dt.Rows.Add(dr);
                }
                SqlParameter[] Parameters = new SqlParameter[10];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@UserId", GetUserId);
                Parameters[2] = new SqlParameter("@Adjustment", dt);
                Parameters[3] = new SqlParameter("@CheqAmt",ConvertDecimalToDb(_MultiPayment.ChequeAmt));
                Parameters[4] = String.IsNullOrEmpty(_MultiPayment.InvoiceNo) ? new SqlParameter("@RcptNo", DBNull.Value) : new SqlParameter("@RcptNo", _MultiPayment.InvoiceNo);
                Parameters[5] = String.IsNullOrEmpty(_MultiPayment.BatchId) ? new SqlParameter("@BatchId", DBNull.Value) : new SqlParameter("@BatchId", _MultiPayment.BatchId);
                Parameters[6] = new SqlParameter("@RetCd", SqlDbType.VarChar, 25);
                Parameters[6].Direction = ParameterDirection.Output;
                Parameters[7] = String.IsNullOrEmpty(_MultiPayment.SelectedOwner) ? new SqlParameter("@Owner", DBNull.Value) : new SqlParameter("@Owner", _MultiPayment.SelectedOwner);
                Parameters[8] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[8].Direction = ParameterDirection.ReturnValue;

                Parameters[9] = new SqlParameter("@BatchOut", SqlDbType.VarChar, 25);
                Parameters[9].Direction = ParameterDirection.Output;

                //// Parameters[2].SqlDbType = SqlDbType.Structured
                //Parameters[5] = new SqlParameter("@RefKey", _MultiPayment.RefId);
                //Parameters[6] = new SqlParameter("@ChequeNo", _MultiPayment.ChequeNo);
                //Parameters[7] = new SqlParameter("@TxnCd", _MultiPayment.SelectedTxnCode);
                //Parameters[8] = new SqlParameter("@TxnDate", DateConverterDB(_MultiPayment.TxnDate));
                //Parameters[9] = new SqlParameter("@DueDate", DateConverterDB(_MultiPayment.DueDate));
                //Parameters[10] = String.IsNullOrEmpty(_MultiPayment.SlipNo) ? new SqlParameter("@SlipNo", DBNull.Value) : new SqlParameter("@SlipNo", _MultiPayment.SlipNo);
                //Parameters[11] = new SqlParameter("@IssueingBankCd", _MultiPayment.SelectedIssueingBank);
                //Parameters[12] = new SqlParameter("@Owner", _MultiPayment.SelectedOwner);
                //Parameters[13] = new SqlParameter("@Batch", SqlDbType.VarChar, 25);
                //Parameters[13].Direction = ParameterDirection.Output;
                //Parameters[14] = String.IsNullOrEmpty(_MultiPayment.SelectedGLSettlement) ? new SqlParameter("@SettleVal", DBNull.Value) : new SqlParameter("@SettleVal", _MultiPayment.SelectedGLSettlement);
                //Parameters[15] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                //Parameters[15].Direction = ParameterDirection.ReturnValue;
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebMerchantMultiTxnAdjustmentMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                this.RetCd = Convert.ToString(Cmd.Parameters["@RetCd"].Value);
                this.BatchId = Convert.ToString(Cmd.Parameters["@BatchOut"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<List<TxnAdjustment>> WebMerchantMultiTxnAdjustmentListSelect()
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[1];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebMerchantMultiTxnAdjustmentListSelect", CommandType.StoredProcedure, Parameters);
                var _List = new List<TxnAdjustment>();
                while (execResult.Read())
                {
                    _List.Add(new TxnAdjustment
                    {
                        BatchId = Convert.ToString(execResult["Batch Id"]),
                        CreationDate = DateConverter(execResult["Creation Date"]),
                        SelectedAdjTxnCode = Convert.ToString(execResult["Txn Code"]),
                        InvoiceNo = Convert.ToString(execResult["Invoice No"]),
                        TxnCount = Convert.ToString(execResult["Txn Cnt"]),
                        BillingTxnAmt = ConverterDecimal(execResult["Txn Amount"]),
                        SelectedOwner = Convert.ToString(execResult["Owner"]),
                        SelectedSts = Convert.ToString(execResult["Status"]),
                    });
                }
                return _List;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TxnAdjustment> WebMerchantMultiTxnAdjustmentSelect(string InvoiceNo, string BatchId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@OrigBatchId", BatchId);
                Parameters[1] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[2] = String.IsNullOrEmpty(InvoiceNo) ? new SqlParameter("@InvoiceNo", DBNull.Value) : new SqlParameter("@InvoiceNo", InvoiceNo);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebMerchantMultiTxnAdjustmentSelect", CommandType.StoredProcedure, Parameters);
                var _Adj = new TxnAdjustment();
                var txnList = new List<MultipleTxnRecord>();
                while (execResult.Read())
                {
                    _Adj.SelectedTxnCd = Convert.ToString(execResult["TxnCd"]);
                    _Adj.SelectedTxnType = Convert.ToString(execResult["Txn Type"]);
                    _Adj.TxnDate = DateConverter(execResult["TxnDate"]);
                    _Adj.ChequeAmt = ConverterDecimal(execResult["Cheque Amt"]);
                    _Adj.SelectedOwner = Convert.ToString(execResult["Owner"]);
                    _Adj.SelectedSts = Convert.ToString(execResult["Sts"]);
                    _Adj.GroupingBatchId = Convert.ToString(execResult["Grouping BatchId"]);
                    _Adj.BatchId = Convert.ToString(execResult["BatchId"]);
                    _Adj.InvoiceNo = Convert.ToString(execResult["Invoice No"]);
                    _Adj.SelectedSts = Convert.ToString(execResult["Approval Status"]);
                    _Adj.StsDescp = Convert.ToString(execResult["Approval Desc"]);
                    txnList.Add(new MultipleTxnRecord
                    {
                        TxnAmt = ConverterDecimal(execResult["Amt"]),
                        MerchantAcctNo = Convert.ToString(execResult["Merchant No"]),
                        AcctName = Convert.ToString(execResult["Merchant Name"]),
                        TxnId=Convert.ToString(execResult["Ids"]),
                        Descp = Convert.ToString(execResult["Description"])
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
                SqlParameter[] Parameters = new SqlParameter[1];
                Parameters[0] = String.IsNullOrEmpty(_multi.SelectedAdjTxnCode) ? new SqlParameter("@TxnCd", DBNull.Value) : new SqlParameter("@TxnCd", _multi.SelectedAdjTxnCode);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebGetGlCode", CommandType.StoredProcedure, Parameters);
                var _List = new List<MultiPayment>();
                while (execResult.Read())
                {
                    _List.Add(new MultiPayment
                    {
                        GLTxnCode = Convert.ToString(execResult["AcctTxnCd"]),
                        GLDescp = Convert.ToString(execResult["GLTxnDescp"]),
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
    }
}