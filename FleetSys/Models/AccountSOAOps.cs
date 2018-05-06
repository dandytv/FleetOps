using FleetOps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CCMS.ModelSector;
using ModelSector;
using Utilities.DAL;
using System.Data.SqlClient;
using System.Data;

namespace FleetSys.Models
{
    public class AccountSOAOps : BaseClass
    {
        /*
        public async Task<AcctSOA> WebAcctSOASummSelect(AcctSOA _AcctSOA)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[1];
                Parameters[0] = String.IsNullOrEmpty(_AcctSOA.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _AcctSOA.AcctNo);

                var Reader = await objDataEngine.ExecuteCommandAsync("WebAcctSOASummSelect", CommandType.StoredProcedure, Parameters);

                while (Reader.Read())
                {

                    var _AcctSOASelect = new AcctSOA
                    {
                        CompanyID = Convert.ToString(Reader["Company Id"]),
                        CompanyName = Convert.ToString(Reader["Company Name"]),
                        BasicCard = Convert.ToString(Reader["Basic Card"]),
                        SelectedStmtDate = Convert.ToString(Reader["Cycle Date"]),
                        LastAgeCd = Convert.ToString(Reader["Last Age Code"]),
                        CreditLimit = ConverterDecimal(Reader["Credit Limit"]),
                        OpeningBal = ConverterDecimal(Reader["Opening Balance"]),
                        MTDDebits = ConverterDecimal(Reader["MTD Debits"]),
                        AvaiCredLimits = ConverterDecimal(Reader["Available Credit Limit"]),
                        CurrBalance = ConverterDecimal(Reader["Current Balance"]),
                        MTDCreds = ConverterDecimal(Reader["MTD Credits"]),
                        TotMinimumPymt = Convert.ToString(Reader["Total Minimun Payment"]),
                        CrrtDueMinimumOymt = ConverterDecimal(Reader["Current Due Minimun Payment"]),
                        PastDueMinimumPymt = Convert.ToString(Reader["Past Due Minimun Payment"]),
                        PymtDueDate = Convert.ToString(Reader["Payment DueDate"]),
                        LastPymtDate = Convert.ToString(Reader["Last Payment Date"]),
                        LastPymtAmt = ConverterDecimal(Reader["Last Payment Amount"])
                    };
                    return _AcctSOASelect;
                }
                return new AcctSOA();
            }
            finally
            {
                objDataEngine.CloseConnection();
                
            }
        }
        */
        /*
        public async Task<List<AcctSOA>> WebAcctSOASummList(AcctSOA _AcctSOA)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[1];
                Parameters[0] = String.IsNullOrEmpty(_AcctSOA.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _AcctSOA.AcctNo);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebAcctSOASummList", CommandType.StoredProcedure, Parameters);

                var _SOAList = new List<AcctSOA>();
                while (execResult.Read())
                {

                    _SOAList.Add(new AcctSOA
                    {
                        Month = Convert.ToString(execResult["Mth"]),
                        SelectedStmtDate = Convert.ToString(execResult["Statement Date"]),
                        ClseBalance = Convert.ToString(execResult["Closing Balance"]),
                        MinimumPayment = Convert.ToString(execResult["Minimun Payment"]),
                        Debits = Convert.ToString(execResult["Debits"]),
                        Credits = Convert.ToString(execResult["Credits"]),
                        Sales = Convert.ToString(execResult["Sales"]),
                        DBAdjust = Convert.ToString(execResult["DB adjustment"]),
                        Charges = Convert.ToString(execResult["Charges"]),
                        Payment = Convert.ToString(execResult["Payment"]),
                        CRAdjust = Convert.ToString(execResult["CR Adjustment"]),
                        age = Convert.ToString(execResult["Age"]),
                        Rchq = Convert.ToString(execResult["RChq"]),
                        Lpay = Convert.ToString(execResult["Lpay"]),
                        Rv = Convert.ToString(execResult["Rv"]),
                        Dun = Convert.ToString(execResult["Dun"])
                    });
                };
                return _SOAList;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
          

        }
        */
        /*
        public async Task<List<AcctSOA>> WebAcctSOATxnCategoryList(AcctSOA _AcctSOA)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = String.IsNullOrEmpty(_AcctSOA.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _AcctSOA.AcctNo);
                Parameters[1] = String.IsNullOrEmpty(_AcctSOA.SelectedStmtDate) ? new SqlParameter("@StmtDate", DBNull.Value) : new SqlParameter("@StmtDate", _AcctSOA.SelectedStmtDate);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebAcctSOATxnCategoryList", CommandType.StoredProcedure, Parameters);

                var _SOATxnCategoryList = new List<AcctSOA>();
                while (execResult.Read())
                {

                    _SOATxnCategoryList.Add(new AcctSOA
                    {
                        TxnCode = Convert.ToString(execResult["Transaction Code"]),
                        TxnEventCd = Convert.ToString(execResult["Transaction Event Code"]),
                        TxnDesc = Convert.ToString(execResult["Transaction Desc"]),
                        TotalCount = Convert.ToString(execResult["Total Count"]),
                        TotalAmt = ConverterDecimal(execResult["Total Amount"]),
                        TotalItemQty =Convert.ToString(execResult["Total Item Quantity"]),
                        TotalItemAmt = ConverterDecimal(execResult["Total Item Amount"]),
                        SelectedStmtDate = Convert.ToString(execResult["Statement Date"]),
                        CompanyName = Convert.ToString(execResult["Company Name"]),
                        AcctNo = Convert.ToString(execResult["Account No"]),
                    });
                };
                return _SOATxnCategoryList;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
         */
        public async Task<List<AcctSOA>> WebAcctSOATxnList(AcctSOA _AcctSOA)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = String.IsNullOrEmpty(_AcctSOA.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _AcctSOA.AcctNo);
                Parameters[1] = String.IsNullOrEmpty(_AcctSOA.SelectedStmtDate) ? new SqlParameter("@StmtDate", DBNull.Value) : new SqlParameter("@StmtDate", _AcctSOA.SelectedStmtDate);
                Parameters[2] = String.IsNullOrEmpty(_AcctSOA.TxnCode) ? new SqlParameter("@TxnCd", DBNull.Value) : new SqlParameter("@TxnCd", _AcctSOA.TxnCode);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebAcctSOATxnList", CommandType.StoredProcedure, Parameters);

                var _SOATxnList = new List<AcctSOA>();
                while (execResult.Read())
                {

                    _SOATxnList.Add(new AcctSOA
                    {
                        //CompanyName = Convert.ToString(execResult["Company Name"]),
                        CardHolderNo = Convert.ToString(execResult["CardHolder No"]),
                        DriverCardNo = Convert.ToString(execResult["Driver Card No"]),
                        TxnDate = Convert.ToString(execResult["Transaction Date"]),
                        txnTime = Convert.ToString(execResult["Transaction Time"]),
                        PostDate = Convert.ToString(execResult["Post Date"]),
                        TxnCode = Convert.ToString(execResult["Txn Code"]),
                        Curr = Convert.ToString(execResult["Curr"]),
                        TxnAmt = ConverterDecimal(execResult["Transaction Amount"]),
                        Amt = ConverterDecimal(execResult["Amount"]),
                        ChqRefNo = Convert.ToString(execResult["Chq Ref No"]),
                        MerchantID = Convert.ToString(execResult["Merchant ID"]),
                        MerchantName = Convert.ToString(execResult["Merchant Name"]),
                        DBAName = Convert.ToString(execResult["Trading Name Description"]),
                        MCC = Convert.ToString(execResult["Mcc"]),
                        RRn = Convert.ToString(execResult["RRN"]),

                    });
                };
                return _SOATxnList;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
    }
}