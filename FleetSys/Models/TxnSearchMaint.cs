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
    public class TxnSearchMaint:BaseClass
    {


        public async Task<List<AcctPostedTxnSearch>> WebAcctTxnSearch(TxnSearchModel _acctPostedTxnSearch)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[7];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_acctPostedTxnSearch.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _acctPostedTxnSearch.AcctNo);
                Parameters[2] = String.IsNullOrEmpty(_acctPostedTxnSearch.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _acctPostedTxnSearch.CardNo);
                Parameters[3] = String.IsNullOrEmpty(_acctPostedTxnSearch.SelectedTxnCategory) ? new SqlParameter("@TxnCategory",DBNull.Value) : new SqlParameter("@TxnCategory", _acctPostedTxnSearch.SelectedTxnCategory);
                Parameters[4] = String.IsNullOrEmpty(_acctPostedTxnSearch.SelectedTxnCd) ? new SqlParameter("@TxnCd", DBNull.Value) : new SqlParameter("@TxnCd", _acctPostedTxnSearch.SelectedTxnCd);
                Parameters[5] = new SqlParameter("@FromDate", ConvertDatetimeDB(_acctPostedTxnSearch.FromDate));
                Parameters[6] = new SqlParameter("@ToDate", ConvertDatetimeDB(_acctPostedTxnSearch.ToDate));
                var execResult = await objDataEngine.ExecuteCommandAsync("WebAcctTxnSearch", CommandType.StoredProcedure, Parameters);
                var _AcctPostedTxnSearch = new List<AcctPostedTxnSearch>();
                while (execResult.Read())
                {
                    _AcctPostedTxnSearch.Add(new AcctPostedTxnSearch
                    {
                        InvoicDt = DateConverter(execResult["StatementDate"]),
                        TxnDate = Convert.ToString(execResult["TxnDate"]),
                        AcctNo = Convert.ToString(execResult["AcctNo"]),
                        SelectedCardNo = Convert.ToString(execResult["CardNo"]),
                        TaxInvoiceNo = Convert.ToString(execResult["TaxInvoiceNo"]),
                        TxnDesp = Convert.ToString(execResult["Txn Descp"]),
                        TxnAmt = ConverterDecimal(execResult["Txn Amt"]),
                        Dealer = Convert.ToString(execResult["Dealer"]),
                        VATNo = Convert.ToString(execResult["VAT No"]),
                        AuthCardNo = Convert.ToString(execResult["AuthCardNo"]),
                        PrcsDate = DateConverter(execResult["PrcsDate"]),
                        TxnId = Convert.ToString(execResult["Txn Id"]),
                        RecieptId = Convert.ToString(execResult["Receipt No"]),
                        Batch = Convert.ToString(execResult["BatchNo"]),
                        VehRegNo = Convert.ToString(execResult["VehRegsNo"]),
                        DriverName = Convert.ToString(execResult["Driver Name"]),
                        SiteId = Convert.ToString(execResult["SiteId"]),
                        Quantity = Convert.ToString(execResult["Qty"]),
                        ProductAmt = ConverterDecimal(execResult["ProductAmt"]),
                        VATAmt = ConverterDecimal(execResult["VAT Amt"]),
                        BaseAmt = ConverterDecimal(execResult["Base Amt"]),
                        VATCd = Convert.ToString(execResult["VATCd"]),
                        VATRate = ConverterDecimal(execResult["VATRate"]),
                        ProductDescp = Convert.ToString(execResult["ProductDescp"]),
                        RRn=Convert.ToString(execResult["Rrn"]),
                        Stan= Convert.ToString(execResult["Stan"]),
                         
                        //TermId = Convert.ToString(execResult["TermId"]),
                        //TotalTxnAmt = ConverterDecimal(execResult["TotalTxnAmt"]),
                        ApproveCd = Convert.ToString(execResult["AppvCd"]),
                        //RRn = Convert.ToString(execResult["RRn"]),
                        //Stan = Convert.ToString(execResult["Stan"]),
                    });
                };
                return _AcctPostedTxnSearch;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        public async Task<List<MerchPostedTxnSearch>> WebMerchTxnSearch(TxnSearchModel _model)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[7];
                Parameters[0] = new SqlParameter("@AcqNo", this.GetAcqNo);
                Parameters[1] = String.IsNullOrEmpty(_model.BusnLocation) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", _model.BusnLocation);
                Parameters[2] = String.IsNullOrEmpty(_model.SelectedMerchTxnCd) ? new SqlParameter("@TxnCd", DBNull.Value) : new SqlParameter("@TxnCd", _model.SelectedMerchTxnCd);
                Parameters[3] = String.IsNullOrEmpty(_model.MerchAcctNo) ? new SqlParameter("@MerchAcctNo", DBNull.Value) : new SqlParameter("@MerchAcctNo", _model.MerchAcctNo);
                Parameters[4] = String.IsNullOrEmpty(_model.MerchFromDate) ? new SqlParameter("@FrmTxnDate", DBNull.Value) : new SqlParameter("@FrmTxnDate", DateConverterDB(_model.MerchFromDate));
                Parameters[5] = String.IsNullOrEmpty(_model.MerchToDate) ? new SqlParameter("@ToTxnDate", DBNull.Value) : new SqlParameter("@ToTxnDate", DateConverterDB(_model.MerchToDate));
                Parameters[6] = String.IsNullOrEmpty(_model.SelectedTxnCategory) ? new SqlParameter("@TxnCat", DBNull.Value) : new SqlParameter("@TxnCat", _model.SelectedTxnCategory);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebMerchTxnSearch", CommandType.StoredProcedure, Parameters);
                var _MerchPostedTxnSearch = new List<MerchPostedTxnSearch>();
                while (execResult.Read())
                {
                    _MerchPostedTxnSearch.Add(new MerchPostedTxnSearch
                    {
                        SelectedDealer = Convert.ToString(execResult["Dealer"]),
                        TermBatch = Convert.ToString(execResult["TermBatch"]),
                        TxnDate = Convert.ToString(execResult["TxnDate"]),
                        cardNo = Convert.ToString(execResult["CardNo"]),
                        TxnDesp = Convert.ToString(execResult["TxnDescp"]),
                        TxnAmt = ConverterDecimal(execResult["BillingAmt"]),
                        TermId = Convert.ToString(execResult["TermId"]),
                        AuthNo = Convert.ToString(execResult["AuthNo"]),
                        AuthCardNo = Convert.ToString(execResult["AuthCardNo"]),
                        PrcsDate = Convert.ToString(execResult["PrcsDate"]),
                        TxnId = Convert.ToString(execResult["TxnId"]),
                        ProductQty= Convert.ToString(execResult["ProductQty"]),
                        ProductAmt= ConverterDecimal(execResult["ProductAmt"]),
                        VATAmt= ConverterDecimal(execResult["VAT Amt"]),
                        BaseAmt=ConverterDecimal(execResult["Base Amt"]),
                        VATCd= Convert.ToString(execResult["VATCd"]),
                        VATRate= ConverterDecimal(execResult["VATRate"]),
                        ProductDescp= Convert.ToString(execResult["ProductDescp"])
                    });
                };
                return _MerchPostedTxnSearch;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }


    }
}