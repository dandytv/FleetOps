using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelSector;
using CCMS.ModelSector;
using FleetOps.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace FleetOps.Models
{
    public class SAPMaint : BaseClass
    {
        public async Task<SAP> SAPGetAccount(string SapNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = String.IsNullOrEmpty(SapNo) ? new SqlParameter("@sapNo", DBNull.Value) : new SqlParameter("@sapNo", SapNo);

            var execResult = objDataEngine.ExecuteCommand("WebSAPAcctSelect", CommandType.StoredProcedure, Parameters);
            var _SAPAccount = new SAP();

            while (execResult.Read())
            {
                _SAPAccount.Id = Convert.ToString(execResult["Id"]);
                _SAPAccount.SapNo = Convert.ToString(execResult["SAPNo"]);
                _SAPAccount.SourceRefNo = Convert.ToString(execResult["SourceRefNo"]);
                _SAPAccount.RefKey = Convert.ToString(execResult["RefKey"]);
                _SAPAccount.RefTo = Convert.ToString(execResult["RefTo"]);
                _SAPAccount.AccountName = Convert.ToString(execResult["AccountName"]);
                _SAPAccount.SelectedAccountGroup = Convert.ToString(execResult["AccountGroup"]);
                _SAPAccount.TaxId = Convert.ToString(execResult["TaxId"]);
                _SAPAccount.SelectedWithHoldingTax = Convert.ToString(execResult["WithholdingTax"]);
                _SAPAccount.PayeeName = Convert.ToString(execResult["PayeeName"]);
                _SAPAccount.SelectedPayeeCd = Convert.ToString(execResult["PayeeCd"]);
                _SAPAccount.OldRefKey = Convert.ToString(execResult["OldRefKey"]);
                _SAPAccount.Remarks = Convert.ToString(execResult["Remarks"]);
                _SAPAccount.CreatedBy = Convert.ToString(execResult["CreatedBy"]);
                _SAPAccount.CreationDate = Convert.ToString(execResult["CreationDate"]);
                _SAPAccount.UserId = Convert.ToString(execResult["UserId"]);
                _SAPAccount.LastUpdate = Convert.ToString(execResult["LastUpdateDate"]);
                _SAPAccount.AccountGroup = await BaseClass.WebGetRefLib("City");
                _SAPAccount.PayeeCd = await BaseClass.WebGetRefLib("City");
                _SAPAccount.WithHoldingTax = await BaseClass.WebGetRefLib("City");
            }

            objDataEngine.CloseConnection();
            return _SAPAccount;
        }

        public async Task<SAP_GeneralInfo> SAPGetGeneralInfo(string SapNo)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = String.IsNullOrEmpty(SapNo) ? new SqlParameter("@sapNo", DBNull.Value) : new SqlParameter("@sapNo", SapNo);

            var execResult = objDataEngine.ExecuteCommand("WebSAPGeneralInfoSelect", CommandType.StoredProcedure, Parameters);
            var _SAP = new SAP_GeneralInfo();

            while (execResult.Read())
            {
                _SAP.CompanyCd = Convert.ToString(execResult["CmpyCd"]);
                _SAP.ReconAccountNo = Convert.ToString(execResult["ReconAcctNo"]);
                _SAP.selectedDistributionChannel = Convert.ToString(execResult["DistChannel"]);
                _SAP.DistributionChannel = await BaseClass.WebGetRefLib("City");
                _SAP.SelectedDivision = Convert.ToString(execResult["Division"]);
                _SAP.Division = await BaseClass.WebGetRefLib("City");
                _SAP.selectedSalesOrganisation = Convert.ToString(execResult["SalesOrganization"]);
                _SAP.SalesOrganization = await BaseClass.WebGetRefLib("City");
                _SAP.selectedCustomerClass = Convert.ToString(execResult["CustomerClass"]);
                _SAP.CustoemrClass = await BaseClass.WebGetRefLib("City");
                _SAP.AuthCd = Convert.ToString(execResult["Authorization"]);
                _SAP.SearchName = Convert.ToString(execResult["SearchTerm1"]);
                _SAP.SortKey = Convert.ToString(execResult["SortKey"]);
                _SAP.Location = Convert.ToString(execResult["Location"]);
                _SAP.SelectedCountryCd = Convert.ToString(execResult["CountryCd"]);
                _SAP.CountryCd = await BaseClass.WebGetRefLib("City");
                _SAP.CurrencyCd = await BaseClass.WebGetRefLib("City");
                _SAP.SelectedCurrencyCd = Convert.ToString(execResult["CurrencyCd"]);
                _SAP.SelectedCashManagementGroup = Convert.ToString(execResult["CashMgmtGroup"]);
                _SAP.CashManagementGroup = await BaseClass.WebGetRefLib("City");
            }
            objDataEngine.CloseConnection();
            return _SAP;
        }
        public async Task<SAP_Cao> SAPGetCAO(string SapNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = String.IsNullOrEmpty(SapNo) ? new SqlParameter("@sapNo", DBNull.Value) : new SqlParameter("@sapNo", SapNo);

            var execResult = objDataEngine.ExecuteCommand("WebSAPCAOSelect", CommandType.StoredProcedure, Parameters);
            var _SAP = new SAP_Cao();

            while (execResult.Read())
            {

                _SAP.CreditLimit = Convert.ToString(execResult["CreditLimit"]);
                _SAP.NIRD = Convert.ToString(execResult["NIRD"]);
                _SAP.SelectedCreditControlArea = Convert.ToString(execResult["CreditControlArea"]);
                _SAP.SelectedRiskCategory = Convert.ToString(execResult["RiskCategory"]);
                _SAP.SecurityCreditAmount = Convert.ToString(execResult["SecureCreditAmt"]);
                _SAP.SelectedPaymentTerms = Convert.ToString(execResult["PaymtTerm"]);
                _SAP.Authorization = Convert.ToString(execResult["Authorization"]);
                _SAP.PaymentHistoryRecord = Convert.ToString(execResult["PaymtHistRec"]);
                _SAP.CreditControlArea = await BaseClass.WebGetRefLib("City");
                _SAP.PaymentMethodSupplier = await BaseClass.WebGetRefLib("City");
                _SAP.PaymentTerms = await BaseClass.WebGetRefLib("City");
                _SAP.RiskCategory = await BaseClass.WebGetRefLib("City");
            }
            return _SAP;
        }
        public async Task<SAP_SalesTerritory> SAPSalesTerritory(string SapNo)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = String.IsNullOrEmpty(SapNo) ? new SqlParameter("@sapNo", DBNull.Value) : new SqlParameter("@sapNo", SapNo);

            var execResult = objDataEngine.ExecuteCommand("WebSAPSalesTerritorySelect", CommandType.StoredProcedure, Parameters);
            var _SAP = new SAP_SalesTerritory();

            while (execResult.Read())
            {

                _SAP.SelectedSalesTerritoryCd = Convert.ToString(execResult["SalesTerritoryCd"]);
                _SAP.SalesTerritoryCd = await BaseClass.WebGetRefLib("City");
                _SAP.CustomerPricingProc = Convert.ToString(execResult["CustPricingProc"]);
                _SAP.SelectedCustoemrStatsGroup = Convert.ToString(execResult["CustStatisticGroup"]);
                _SAP.CustomerStatsGroup = await BaseClass.WebGetRefLib("City");
                _SAP.SelectedSalesDistrict = Convert.ToString(execResult["CustStatisticGroup"]);
                _SAP.SalesDistrict = await BaseClass.WebGetRefLib("City");
                _SAP.SalesOfficer = Convert.ToString(execResult["SalesOfficer"]);
                _SAP.SelectedSalesGroup = Convert.ToString(execResult["SalesGroup"]);
                _SAP.SalesGroup = await BaseClass.WebGetRefLib("City");
                _SAP.ShippingConditions = Convert.ToString(execResult["ShippingCondition"]);
                _SAP.MaxPartialDelivery = Convert.ToString(execResult["MaxPartialDelivery"]);
                _SAP.SelectedTaxClass = Convert.ToString(execResult["TaxClass"]);
                _SAP.TaxClass = await BaseClass.WebGetRefLib("City");
                _SAP.OrderCombiation = Convert.ToString(execResult["OrderCombination"]);
            }
            return _SAP;

        }


        public async Task<SAP_SOAInfo> SAPSOAInfo(string SapNo)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[1];
            Parameters[0] = String.IsNullOrEmpty(SapNo) ? new SqlParameter("@sapNo", DBNull.Value) : new SqlParameter("@sapNo", SapNo);

            var execResult = objDataEngine.ExecuteCommand("WebSAPSOAInfoSelect", CommandType.StoredProcedure, Parameters);
            var _SAP = new SAP_SOAInfo();

            while (execResult.Read())
            {

                _SAP.SelectedAccountType = Convert.ToString(execResult["AcctType"]);
                _SAP.AccountType = await BaseClass.WebGetRefLib("City");
                _SAP.HandlingFee = Convert.ToString(execResult["HandlingFee"]);
                _SAP.SelectedTxnCategory = Convert.ToString(execResult["AcctTxnType"]);
                _SAP.TxnCategory = await BaseClass.WebGetRefLib("City");
                _SAP.priceShieldHours = Convert.ToString(execResult["PriceShieldHours"]);
            }
            return _SAP;
        }

        public async Task<MsgRetriever> WebSAPAcctMaint(SAP _SAP)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[17];
            SqlCommand cmd = new SqlCommand();

            Parameters[1] = String.IsNullOrEmpty(_SAP.SapNo) ? new SqlParameter("@SapNo", DBNull.Value) : new SqlParameter("@SapNo", _SAP.SapNo);
            Parameters[2] = String.IsNullOrEmpty(_SAP.SourceRefNo) ? new SqlParameter("@SourceRefNo", DBNull.Value) : new SqlParameter("@SourceRefNo", _SAP.SourceRefNo);
            Parameters[3] = String.IsNullOrEmpty(_SAP.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", _SAP.RefKey);
            Parameters[4] = String.IsNullOrEmpty(_SAP.RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", _SAP.RefTo);
            Parameters[5] = String.IsNullOrEmpty(_SAP.AccountName) ? new SqlParameter("@AcctName", DBNull.Value) : new SqlParameter("@AcctName", _SAP.AccountName);
            Parameters[6] = String.IsNullOrEmpty(_SAP.SelectedAccountGroup) ? new SqlParameter("@AcctGroup", DBNull.Value) : new SqlParameter("@AcctGroup", _SAP.SelectedAccountGroup);
            Parameters[7] = String.IsNullOrEmpty(_SAP.TaxId) ? new SqlParameter("@TaxId", DBNull.Value) : new SqlParameter("@TaxId", _SAP.TaxId);
            Parameters[8] = String.IsNullOrEmpty(_SAP.SelectedWithHoldingTax) ? new SqlParameter("@Wht", DBNull.Value) : new SqlParameter("@Wht", _SAP.SelectedWithHoldingTax);
            Parameters[9] = String.IsNullOrEmpty(_SAP.PayeeName) ? new SqlParameter("@PayeeName", DBNull.Value) : new SqlParameter("@PayeeName", _SAP.PayeeName);

            Parameters[10] = String.IsNullOrEmpty(_SAP.SelectedPayeeCd) ? new SqlParameter("@PayeeCd", DBNull.Value) : new SqlParameter("@PayeeCd", _SAP.SelectedPayeeCd);

            Parameters[11] = String.IsNullOrEmpty(_SAP.OldRefKey) ? new SqlParameter("@PrevRefKey", DBNull.Value) : new SqlParameter("@PrevRefKey", _SAP.OldRefKey);

            Parameters[12] = String.IsNullOrEmpty(_SAP.Remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", _SAP.Remarks);

            Parameters[13] = String.IsNullOrEmpty(_SAP.CreatedBy) ? new SqlParameter("@CreatedBy", DBNull.Value) : new SqlParameter("@CreatedBy", _SAP.CreatedBy);

            Parameters[14] = String.IsNullOrEmpty(_SAP.CreationDate) ? new SqlParameter("@CreationDate", DBNull.Value) : new SqlParameter("@CreationDate", BaseClass.ConvertDatetimeDB(_SAP.CreationDate));

            Parameters[15] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);

            Parameters[16] = new SqlParameter("@LastUpdDate", ConvertDatetimeDB(DateTime.Now.ToString()));


            Parameters[0] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[0].Direction = ParameterDirection.ReturnValue;


            var Cmd = objDataEngine.ExecuteWithReturnValue("[WebSAPAcctMaint]", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return await Descp;

        }



        public async Task<MsgRetriever> WebSAPGeneralInfoMaint(SAP_GeneralInfo _SAP, string SapNo)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[15];
            SqlCommand cmd = new SqlCommand();

            Parameters[0] = String.IsNullOrEmpty(SapNo) ? new SqlParameter("@SapNo", DBNull.Value) : new SqlParameter("@SapNo", SapNo);
            Parameters[1] = String.IsNullOrEmpty(_SAP.ReconAccountNo) ? new SqlParameter("@ReconAcctNo", DBNull.Value) : new SqlParameter("@ReconAcctNo", _SAP.ReconAccountNo);

            Parameters[2] = String.IsNullOrEmpty(_SAP.selectedDistributionChannel) ? new SqlParameter("@DistChannel", DBNull.Value) : new SqlParameter("@DistChannel", _SAP.selectedDistributionChannel);
            Parameters[3] = String.IsNullOrEmpty(_SAP.CompanyCd) ? new SqlParameter("@CmpyCd", DBNull.Value) : new SqlParameter("@CmpyCd", _SAP.CompanyCd);
            Parameters[4] = String.IsNullOrEmpty(_SAP.SelectedDivision) ? new SqlParameter("@Division", DBNull.Value) : new SqlParameter("@Division", _SAP.SelectedDivision);
            Parameters[5] = String.IsNullOrEmpty(_SAP.selectedSalesOrganisation) ? new SqlParameter("@SaleOrg", DBNull.Value) : new SqlParameter("@SaleOrg", _SAP.selectedSalesOrganisation);
            Parameters[6] = String.IsNullOrEmpty(_SAP.selectedCustomerClass) ? new SqlParameter("@CustClass", DBNull.Value) : new SqlParameter("@CustClass", _SAP.selectedCustomerClass);
            Parameters[7] = String.IsNullOrEmpty(_SAP.AuthCd) ? new SqlParameter("@Autho", DBNull.Value) : new SqlParameter("@Autho", _SAP.AuthCd);
            Parameters[8] = String.IsNullOrEmpty(_SAP.SearchName) ? new SqlParameter("@SearchTerm1", DBNull.Value) : new SqlParameter("@SearchTerm1", _SAP.SearchName);
            Parameters[9] = String.IsNullOrEmpty(_SAP.SortKey) ? new SqlParameter("@SortKey", DBNull.Value) : new SqlParameter("@SortKey", _SAP.SortKey);

            Parameters[10] = String.IsNullOrEmpty(_SAP.Location) ? new SqlParameter("@Location", DBNull.Value) : new SqlParameter("@Location", _SAP.Location);

            Parameters[11] = String.IsNullOrEmpty(_SAP.SelectedCountryCd) ? new SqlParameter("@CtryCd", DBNull.Value) : new SqlParameter("@CtryCd", _SAP.SelectedCountryCd);
            Parameters[12] = String.IsNullOrEmpty(_SAP.SelectedCurrencyCd) ? new SqlParameter("@CurrCd", DBNull.Value) : new SqlParameter("@CurrCd", _SAP.SelectedCurrencyCd);
            Parameters[13] = String.IsNullOrEmpty(_SAP.SelectedCashManagementGroup) ? new SqlParameter("@CashMgmtGroup", DBNull.Value) : new SqlParameter("@CashMgmtGroup", _SAP.SelectedCashManagementGroup);

            Parameters[14] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[14].Direction = ParameterDirection.ReturnValue;


            var Cmd = objDataEngine.ExecuteWithReturnValue("[WebSAPGeneralInfoMaint]", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return await Descp;
        }

        public async Task<MsgRetriever> WebSAPSalesTerritoryMaint(SAP_SalesTerritory _SAP, string SapNo)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[11];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = String.IsNullOrEmpty(SapNo) ? new SqlParameter("@SapNo", DBNull.Value) : new SqlParameter("@SapNo", SapNo);
            Parameters[1] = String.IsNullOrEmpty(_SAP.SelectedSalesTerritoryCd) ? new SqlParameter("@SalesTerritoryCd", DBNull.Value) : new SqlParameter("@SalesTerritoryCd", _SAP.SelectedSalesTerritoryCd);
            Parameters[2] = String.IsNullOrEmpty(_SAP.CustomerPricingProc) ? new SqlParameter("@CustPricingProc", DBNull.Value) : new SqlParameter("@CustPricingProc", _SAP.CustomerPricingProc);
            Parameters[3] = String.IsNullOrEmpty(_SAP.SelectedCustoemrStatsGroup) ? new SqlParameter("@CustStatisticGroup", DBNull.Value) : new SqlParameter("@CustStatisticGroup", _SAP.SelectedCustoemrStatsGroup);
            Parameters[4] = String.IsNullOrEmpty(_SAP.SelectedSalesDistrict) ? new SqlParameter("@SalesDistrict", DBNull.Value) : new SqlParameter("@SalesDistrict", _SAP.SelectedSalesDistrict);
            Parameters[5] = String.IsNullOrEmpty(_SAP.SalesOfficer) ? new SqlParameter("@SalesOffice", DBNull.Value) : new SqlParameter("@SalesOffice", _SAP.SalesOfficer);
            Parameters[6] = String.IsNullOrEmpty(_SAP.ShippingConditions) ? new SqlParameter("@ShippingCondition", DBNull.Value) : new SqlParameter("@ShippingCondition", _SAP.ShippingConditions);
            Parameters[7] = String.IsNullOrEmpty(_SAP.MaxPartialDelivery) ? new SqlParameter("@MaxPartialDelivery", DBNull.Value) : new SqlParameter("@MaxPartialDelivery", _SAP.MaxPartialDelivery);
            Parameters[8] = String.IsNullOrEmpty(_SAP.OrderCombiation) ? new SqlParameter("@OrderCombination", DBNull.Value) : new SqlParameter("@OrderCombination", _SAP.OrderCombiation);
            Parameters[9] = String.IsNullOrEmpty(_SAP.SelectedTaxClass) ? new SqlParameter("@TaxClass", DBNull.Value) : new SqlParameter("@TaxClass", _SAP.SelectedTaxClass);
            Parameters[10] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[10].Direction = ParameterDirection.ReturnValue;
            var Cmd = objDataEngine.ExecuteWithReturnValue("[WebSAPSalesTerritoryMaint]", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return await Descp;
        }
        public async Task<MsgRetriever> WebSAPCAOMaint(SAP_Cao _CAO, string SapNo)
        //[WebSAPCAOMaint]
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[13];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = String.IsNullOrEmpty(SapNo) ? new SqlParameter("@SapNo", DBNull.Value) : new SqlParameter("@SapNo", SapNo);
            Parameters[1] = String.IsNullOrEmpty(_CAO.CreditLimit) ? new SqlParameter("@CrLimit", DBNull.Value) : new SqlParameter("@CrLimit", _CAO.CreditLimit);

            Parameters[2] = String.IsNullOrEmpty(_CAO.NIRD) ? new SqlParameter("@NIRD", DBNull.Value) : new SqlParameter("@NIRD", ConvertToDatetimeDB(_CAO.NIRD));
            Parameters[3] = String.IsNullOrEmpty(_CAO.SelectedCreditControlArea) ? new SqlParameter("@CrCtrlArea", DBNull.Value) : new SqlParameter("@CrCtrlArea", _CAO.SelectedCreditControlArea);
            Parameters[4] = String.IsNullOrEmpty(_CAO.SelectedRiskCategory) ? new SqlParameter("@RiskCategory", DBNull.Value) : new SqlParameter("@RiskCategory", _CAO.SelectedRiskCategory);
            Parameters[5] = String.IsNullOrEmpty(_CAO.SecurityCreditAmount) ? new SqlParameter("@SecureCreditAmt", DBNull.Value) : new SqlParameter("@SecureCreditAmt", _CAO.SecurityCreditAmount);
            Parameters[6] = String.IsNullOrEmpty(_CAO.SelectedPaymentTerms) ? new SqlParameter("@PaymtTerm", DBNull.Value) : new SqlParameter("@PaymtTerm", _CAO.SelectedPaymentTerms);

            Parameters[7] = String.IsNullOrEmpty(_CAO.DBLInvoice) ? new SqlParameter("@DBLInvoiceNo", DBNull.Value) : new SqlParameter("@DBLInvoiceNo", _CAO.DBLInvoice);
            Parameters[8] = String.IsNullOrEmpty(_CAO.SelectedPaymentMethodSupplier) ? new SqlParameter("@PaymtMethodSupplier", DBNull.Value) : new SqlParameter("@PaymtMethodSupplier", _CAO.SelectedPaymentMethodSupplier);
            Parameters[9] = String.IsNullOrEmpty(_CAO.Authorization) ? new SqlParameter("@Authorization", DBNull.Value) : new SqlParameter("@Authorization", _CAO.Authorization);
            Parameters[10] = new SqlParameter("@InterestFlag", BaseClass.ConvertBoolDB(_CAO.InterestFlag));
            Parameters[11] = String.IsNullOrEmpty(_CAO.PaymentHistoryRecord) ? new SqlParameter("@PaymtHistRec", DBNull.Value) : new SqlParameter("@PaymtHistRec", _CAO.PaymentHistoryRecord);

            Parameters[12] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[12].Direction = ParameterDirection.ReturnValue;


            var Cmd = objDataEngine.ExecuteWithReturnValue("[WebSAPCAOMaint]", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return await Descp;
        }
        public async Task<MsgRetriever> WebSAPSOAInfoMaint(SAP_SOAInfo _SAP, string SapNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[10];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = String.IsNullOrEmpty(SapNo) ? new SqlParameter("@SapNo", DBNull.Value) : new SqlParameter("@SapNo", SapNo);
            Parameters[1] = String.IsNullOrEmpty(_SAP.SelectedAccountType) ? new SqlParameter("@AcctType", DBNull.Value) : new SqlParameter("@AcctType", _SAP.SelectedAccountType);
            Parameters[2] = String.IsNullOrEmpty(_SAP.SelectedTxnCategory) ? new SqlParameter("@TxnCategory", DBNull.Value) : new SqlParameter("@TxnCategory", _SAP.SelectedTxnCategory);
            Parameters[3] = new SqlParameter("@LPCFlag", BaseClass.ConvertBoolDB(_SAP.LatePayementChargeFlag));
            Parameters[4] = new SqlParameter("@HandlingFeeFlag", BaseClass.ConvertBoolDB(_SAP.HandlingFeeFlag));
            Parameters[5] = new SqlParameter("@PayeeCardFlag", BaseClass.ConvertBoolDB(_SAP.PayeeCardFlag));
            Parameters[6] = new SqlParameter("@EWTFlag", BaseClass.ConvertBoolDB(_SAP.EWTFlag));
            Parameters[7] = String.IsNullOrEmpty(_SAP.priceShieldHours) ? new SqlParameter("@PriceShieldHours", DBNull.Value) : new SqlParameter("@PriceShieldHours", _SAP.priceShieldHours);
            Parameters[8] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[8].Direction = ParameterDirection.ReturnValue;
            Parameters[9] = new SqlParameter("@AcctTxnType", DBNull.Value);
            var Cmd = objDataEngine.ExecuteWithReturnValue("[WebSAPSOAInfoMaint]", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return await Descp;
        }
    }
}