using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Utilities.DAL;
using System.Data;
using System.Data.SqlClient;
using FleetOps.Models;
using System.Web.Mvc;
using CCMS.ModelSector;
using ModelSector;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using System.Configuration;

namespace FleetSys.Models
{
    public class AccountOps : BaseClass
    {
        #region "General Info"
        public async Task<GeneralInfoModel> FtGeneralInfoForm(Int64 AcctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@AcctNo", AcctNo);
                var Reader = await objDataEngine.ExecuteCommandAsync("WebAcctGeneralInfoSelect", CommandType.StoredProcedure, Parameters);

                while (Reader.Read())
                {
                    var AcctGeneralInfo = new GeneralInfoModel
                    {
                        AcctNo = Convert.ToString(Reader["AcctNo"]),
                        SelectedAccountType = Convert.ToString(Reader["AccountType"]),
                        SelectedPlasticType = Convert.ToString(Reader["PlasticType"]),
                        AccountName = Convert.ToString(Reader["AccountName"]),
                        CmpyRegsNo = Convert.ToString(Reader["CmpyRegsNo"]),
                        SelectedCompanyType = Convert.ToString(Reader["CmpyType"]),
                        RegsDate = DateConverter(Reader["RegsDate"]),
                        SelectedSIC = Convert.ToString(Reader["SIC"]),
                        SapNo = Convert.ToString(Reader["CustomerNo"]),
                        BlockedDate = DateConverter(Reader["BlockedDate"]),
                        SelectedCorpName = Convert.ToString(Reader["CorpCd"]),
                        SelectedClientClass = Convert.ToString(Reader["TermsofPayment"]),
                        selectedClientType = Convert.ToString(Reader["CustomerGroup"]),
                        SelectedBusnEstablishment = Convert.ToString(Reader["BusnEstablishment"]),
                        SourceCode = Convert.ToString(Reader["SrcCd"]),
                        SourceRefNo = Convert.ToString(Reader["SrcRefNo"]),
                        SelectedAcctSts = Convert.ToString(Reader["Sts"]),
                        TerminatedDate = DateConverter(Reader["TerminatedDate"]),
                        SelectedReasonCode = Convert.ToString(Reader["ReasonCd"]),
                        OvrStatusTaggedByUserId = Convert.ToString(Reader["OverrideStsUserId"]),
                        SelectedOvrStatus = Convert.ToString(Reader["OverrideSts"]),
                        OvrExpDate = DateConverter(Reader["OverrideStsExpiry"]),
                        OvrStartDate = DateConverter(Reader["OverrideStsStart"]),
                        ApplId = Convert.ToString(Reader["ApplId"]),
                        ApplRef = Convert.ToString(Reader["ApplRef"]),
                        CaptDate = DateConverter(Reader["CaptDate"]),
                        Remarks = Convert.ToString(Reader["Remarks"]),
                        WebUserId = Convert.ToString(Reader["WebUserId"]),
                        LoyaltyCardNo = Convert.ToString(Reader["LoyaltyCardNo"]),
                        WebPassword = Convert.ToString(Reader["WebPassword"]),
                        SelectedBusnCategory = Convert.ToString(Reader["Industry"]),
                        SelectedSaleTerritory = Convert.ToString(Reader["SalesGroup"]),
                        SelectedPaymentTerm = Convert.ToString(Reader["PymtTerms"]),
                        ApplCreationDate = Convert.ToString(Reader["CreationDate"]),
                        TaxId = Convert.ToString(Reader["TaxId"]),
                        CutOff = Convert.ToString(Reader["CutOff"]),
                        SelectedCurrentStatus = Convert.ToString(Reader["Sts"]),
                        SelectedLangId = Convert.ToString(Reader["LangId"]),
                        CompanyEmbName = Convert.ToString(Reader["CmpyEmbName"]),
                        ContactPerson = Convert.ToString(Reader["FamilyName"]),
                        AuthSignatory = Convert.ToString(Reader["AuthName"]),
                        SelectedTradingArea = Convert.ToString(Reader["TradingArea"])
                    };
                    if (AcctGeneralInfo.LoyaltyCardNo == "0")
                    {
                        AcctGeneralInfo.LoyaltyCardNo = null;
                    }
                    return AcctGeneralInfo;
                }
                return new GeneralInfoModel();
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

        }


        /// <summary>
        /// Together with the AcceptAllCertifications method right
        /// below this causes to bypass errors caused by SLL-Errors.
        /// </summary>
        public static void IgnoreBadCertificates()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
        }

        /// <summary>
        /// In Short: the Method solves the Problem of broken Certificates.
        /// Sometime when requesting Data and the sending Webserverconnection
        /// is based on a SSL Connection, an Error is caused by Servers whoes
        /// Certificate(s) have Errors. Like when the Cert is out of date
        /// and much more... So at this point when calling the method,
        /// this behaviour is prevented
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certification"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns>true</returns>
        private static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public String LoadPointBalance(string RequestorId, string Token, String AcctNo)
        {
            string result = null;
            String Uri = ConfigurationManager.AppSettings["pointBalanceUrl"] + String.Format("?RequestorId={0}&Token={1}&AcctNo={2}", RequestorId, Token, AcctNo);
            IgnoreBadCertificates();
            HttpWebRequest _Request = HttpWebRequest.Create(Uri) as HttpWebRequest;
            _Request.Method = "GET";
            _Request.KeepAlive = true;
            _Request.Accept = "application/json";
            using (WebResponse _Response = _Request.GetResponse())
            {
                var sr = new StreamReader(_Response.GetResponseStream());
                result = sr.ReadToEnd();
                sr.Close();
            }
            var _Serializer = new JavaScriptSerializer();
            var TokenData = _Serializer.Deserialize<PointBalance>(result);
            String PointBalance = TokenData.PointBal;
            return PointBalance;
        }


        public async Task<MsgRetriever> ftGeneralInfoMaint(GeneralInfoModel _generalInfo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[43];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@AcctNo", _generalInfo.AcctNo);
                Parameters[2] = String.IsNullOrEmpty(_generalInfo.CmpyRegsNo) ? new SqlParameter("@CmpyRegsNo", DBNull.Value) : new SqlParameter("@CmpyRegsNo", _generalInfo.CmpyRegsNo);
                Parameters[3] = new SqlParameter("@RegsDate", ConvertDatetimeDB(_generalInfo.RegsDate));
                Parameters[4] = String.IsNullOrEmpty(_generalInfo.AccountName) ? new SqlParameter("@CmpyName1", DBNull.Value) : new SqlParameter("@CmpyName1", _generalInfo.AccountName);
                Parameters[5] = String.IsNullOrEmpty(_generalInfo.SelectedSIC) ? new SqlParameter("@Sic", DBNull.Value) : new SqlParameter("@Sic", _generalInfo.SelectedSIC);
                Parameters[6] = String.IsNullOrEmpty(_generalInfo.SapNo) ? new SqlParameter("@SAPNo", DBNull.Value) : new SqlParameter("@SAPNo", _generalInfo.SapNo);
                Parameters[7] = String.IsNullOrEmpty(_generalInfo.SelectedCorpName) ? new SqlParameter("@CorpCd", DBNull.Value) : new SqlParameter("@CorpCd", _generalInfo.SelectedCorpName);
                Parameters[8] = String.IsNullOrEmpty(_generalInfo.SelectedClientClass) ? new SqlParameter("@ClientClass", DBNull.Value) : new SqlParameter("@ClientClass", _generalInfo.SelectedClientClass);
                Parameters[9] = String.IsNullOrEmpty(_generalInfo.selectedClientType) ? new SqlParameter("@ClientType", DBNull.Value) : new SqlParameter("@ClientType", _generalInfo.selectedClientType);
                Parameters[10] = String.IsNullOrEmpty(_generalInfo.SelectedBusnEstablishment) ? new SqlParameter("@BusnEstablishment", DBNull.Value) : new SqlParameter("@BusnEstablishment", _generalInfo.SelectedBusnEstablishment);
                Parameters[11] = String.IsNullOrEmpty(_generalInfo.SourceCode) ? new SqlParameter("@SrcCd", DBNull.Value) : new SqlParameter("@SrcCd", _generalInfo.SourceCode);
                Parameters[12] = String.IsNullOrEmpty(_generalInfo.SourceRefNo) ? new SqlParameter("@SrcRefNo", DBNull.Value) : new SqlParameter("@SrcRefNo", _generalInfo.SourceRefNo);
                Parameters[13] = String.IsNullOrEmpty(_generalInfo.SelectedAcctSts) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _generalInfo.SelectedAcctSts);
                Parameters[14] = new SqlParameter("@CreationDate", DateConverterDB(_generalInfo.CreationDate));
                Parameters[15] = new SqlParameter("@TerminatedDate", DateConverterDB(_generalInfo.TerminatedDate));
                Parameters[16] = String.IsNullOrEmpty(_generalInfo.SelectedReasonCode) ? new SqlParameter("@ReasonCd", DBNull.Value) : new SqlParameter("@ReasonCd", _generalInfo.SelectedReasonCode);
                Parameters[17] = String.IsNullOrEmpty(_generalInfo.OvrStatusTaggedByUserId) ? new SqlParameter("@OverrideStsUserId", DBNull.Value) : new SqlParameter("@OverrideStsUserId", _generalInfo.OvrStatusTaggedByUserId);
                Parameters[18] = String.IsNullOrEmpty(_generalInfo.SelectedOvrStatus) ? new SqlParameter("@OverrideSts", DBNull.Value) : new SqlParameter("@OverrideSts", _generalInfo.SelectedOvrStatus);
                Parameters[19] = new SqlParameter("@OverrideStsExp", DateConverterDB(_generalInfo.OvrExpDate));
                Parameters[20] = new SqlParameter("@ApplId", _generalInfo.ApplId);
                Parameters[21] = String.IsNullOrEmpty(_generalInfo.ApplRef) ? new SqlParameter("@ApplRefNo", DBNull.Value) : new SqlParameter("@ApplRefNo", _generalInfo.ApplRef);
                Parameters[22] = new SqlParameter("@CaptDate", DateConverterDB(_generalInfo.CaptDate));
                Parameters[23] = String.IsNullOrEmpty(_generalInfo.Remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", _generalInfo.Remarks);
                Parameters[24] = String.IsNullOrEmpty(_generalInfo.WebUserId) ? new SqlParameter("@WebUserId", DBNull.Value) : new SqlParameter("@WebUserId", _generalInfo.WebUserId);
                Parameters[25] = String.IsNullOrEmpty(_generalInfo.LoyaltyCardNo) ? new SqlParameter("@LoyaltyCardNo", DBNull.Value) : new SqlParameter("@LoyaltyCardNo", _generalInfo.LoyaltyCardNo); //new SqlParameter("@LoyaltyCardNo", _generalInfo.LoyaltyCardNo);
                Parameters[26] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
                Parameters[27] = String.IsNullOrEmpty(_generalInfo.WebPassword) ? new SqlParameter("@WebPw", DBNull.Value) : new SqlParameter("@WebPw", _generalInfo.WebPassword);
                Parameters[28] = String.IsNullOrEmpty(_generalInfo.SelectedBusnCategory) ? new SqlParameter("@BusnCategory", DBNull.Value) : new SqlParameter("@BusnCategory", _generalInfo.SelectedBusnCategory);
                Parameters[29] = String.IsNullOrEmpty(_generalInfo.SelectedSaleTerritory) ? new SqlParameter("@SaleTerritory", DBNull.Value) : new SqlParameter("@SaleTerritory", _generalInfo.SelectedSaleTerritory);
                Parameters[30] = String.IsNullOrEmpty(_generalInfo.SelectedAccountType) ? new SqlParameter("@AcctType", DBNull.Value) : new SqlParameter("@AcctType", _generalInfo.SelectedAccountType);
                Parameters[31] = String.IsNullOrEmpty(_generalInfo.CutOff) ? new SqlParameter("@CutOff", DBNull.Value) : new SqlParameter("@CutOff", _generalInfo.CutOff); //new SqlParameter("@CutOff", _generalInfo.CutOff);
                Parameters[32] = String.IsNullOrEmpty(_generalInfo.TaxId) ? new SqlParameter("@TaxId", DBNull.Value) : new SqlParameter("@TaxId", _generalInfo.TaxId);
                Parameters[33] = String.IsNullOrEmpty(_generalInfo.SelectedPaymentTerm) ? new SqlParameter("@PymtTerms", DBNull.Value) : new SqlParameter("@PymtTerms", _generalInfo.SelectedPaymentTerm);
                Parameters[34] = String.IsNullOrEmpty(_generalInfo.CustSvcId) ? new SqlParameter("@ReconAcct", DBNull.Value) : new SqlParameter("@ReconAcct", _generalInfo.CustSvcId);
                Parameters[35] = new SqlParameter("@OverrideStsStart", DateConverterDB(_generalInfo.OvrStartDate));
                Parameters[36] = String.IsNullOrEmpty(_generalInfo.SelectedLangId) ? new SqlParameter("@LangId", DBNull.Value) : new SqlParameter("@LangId", _generalInfo.SelectedLangId);
                Parameters[37] = String.IsNullOrEmpty(_generalInfo.CompanyEmbName) ? new SqlParameter("@CmpyEmbName", DBNull.Value) : new SqlParameter("@CmpyEmbName", _generalInfo.CompanyEmbName);
                Parameters[38] = String.IsNullOrEmpty(_generalInfo.SelectedCompanyType) ? new SqlParameter("@CmpyType", DBNull.Value) : new SqlParameter("@CmpyType", _generalInfo.SelectedCompanyType);
                Parameters[39] = String.IsNullOrEmpty(_generalInfo.ContactPerson) ? new SqlParameter("@ContactPerson", DBNull.Value) : new SqlParameter("@ContactPerson", _generalInfo.ContactPerson);
                Parameters[40] = String.IsNullOrEmpty(_generalInfo.AuthSignatory) ? new SqlParameter("@AuthName", DBNull.Value) : new SqlParameter("@AuthName", _generalInfo.AuthSignatory);
                
                Parameters[41] = String.IsNullOrEmpty(_generalInfo.SelectedTradingArea) ? new SqlParameter("@TradingArea", DBNull.Value) : new SqlParameter("@TradingArea", _generalInfo.SelectedTradingArea);

                Parameters[42] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[42].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebAcctGeneralInfoMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }



        }
        #endregion
        #region"Financial Info"
        public async Task<FinancialInfoModel> FtFinancialInfoForm(int AcctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@AcctNo", AcctNo);//new SqlParameter("@AcctNo", AcctNo);
                var Reader = await objDataEngine.ExecuteCommandAsync("WebAcctFinInfoSelect", CommandType.StoredProcedure, Parameters);
                while (Reader.Read())
                {
                    var _financialInfo = new FinancialInfoModel
                    {
                        AcctNo = Convert.ToString(Reader["AcctNo"]),
                        TaxId = Convert.ToString(Reader["TaxId"]),
                        LatePaymtCharge = BoolConverter(Reader["StopLPC"]),
                        SelectedDunningCd = Convert.ToString(Reader["DunCd"]),
                        CredtAllowanceFact = ConverterDecimal(Reader["AllowanceFactor"]),
                        AccrdInterest = ConverterDecimal(Reader["AccruedInterestAmt"]),
                        AccrdCrdtUsg = ConverterDecimal(Reader["AccruedCreditUsageAmt"]),
                        PromPaymtRebate = ConverterDecimal(Reader["PromptPaymtRebate"]),
                        PPRGracePeriod = ConvertInt(Reader["PromptPaymtRebateTerms"]),
                        pprExpiry = DateConverter(Reader["PromptPaymtRebateExpiry"]),
                        LitreLimitPerTxn = ConverterDecimal(Reader["LitLimitPerTxn"]),
                        AmtLimitPerTxn = ConverterDecimal(Reader["AmtLimitPerTxn"]),
                        SelectedCycNo = Convert.ToString(Reader["CycNo"]),
                        SelectedStmtType = Convert.ToString(Reader["StmtType"]),
                        SelectedStmtInd = Convert.ToString(Reader["StmtInd"]),
                        StmtDate = DateConverter(Reader["StmtDate"]),
                        SelectedPaymtMethod = Convert.ToString(Reader["PaymtMethod"]),
                        PaymtTerm = ConvertInt(Reader["PymtTerms"]),
                        GracePeriod = ConvertInt(Reader["GracePeriod"]),
                        DirectDebitInd = BoolConverter(Reader["DirectDebitInd"]),
                        SelectedBankAcctType = Convert.ToString(Reader["BankAcctType"]),
                        selectedBankName = Convert.ToString(Reader["BankName"]),
                        BankAcctNo = Convert.ToString(Reader["BankAcctNo"]),
                        BankBranchCD = Convert.ToString(Reader["BankBranchCd"]),
                        PayeeCd = Convert.ToString(Reader["PayeeCd"]),
                        SelectedTaxCategory = Convert.ToString(Reader["TaxCategory"]),
                        WriteoffDate = DateConverter(Reader["WriteOffDate"]),
                        LastPaymtType = Convert.ToString(Reader["LastPaymtRecvType"]),
                        LastPaymtReceived = ConverterDecimal(Reader["LastPaymtRecvAmt"]),
                        LastPaymtDate = DateConverter(Reader["LastPaymtDate"]),
                        InvoiceBillingIndicator = BoolConverter(Reader["InvBillInd"]),
                        PayAdviceBillingIndicator = BoolConverter(Reader["PymtInd"]),
                        VehiclePerformanceReportInd = BoolConverter(Reader["VehPerfRptInd"]),
                        SelectedAssessmentType = Convert.ToString(Reader["SecuredCreditLine"]),
                        SelectedRiskCategory = Convert.ToString(Reader["RiskCategory"]),
                        CreditLimit = ConverterDecimal(Reader["CreditLimit"]),
                        WithholdingTaxInd = BoolConverter(Reader["Ewt"]),
                        SelectedAssignCollector= Convert.ToString(Reader["Owner"]),

                        _skds = new SKDS
                        {
                            SKDSNo = Convert.ToString(Reader["SKDSNo"]),
                            SKDSLitreQuota = ConverterDecimal(Reader["SKDSQuota"]),
                            EffFromDate = DateConverter(Reader["SKDSFromDate"]),
                            EffToDate = DateConverter(Reader["SKDSToDate"]),
                            SKDSRate = ConverterDecimal(Reader["SKDSRate"]),
                            SKDSDocXref = Convert.ToString(Reader["SKDSRef"]),
                        }
                    };
                    _financialInfo.ProdRebateType = new List<SelectListItem>(){
                 new SelectListItem{
                  Text="Type 1",Value="1"
                 },
                 new SelectListItem{
                  Text="Type 2",Value="2"
                 }
                };

                    if (string.IsNullOrEmpty(_financialInfo.SelectedStmtInd))
                    {
                        _financialInfo.SelectedStmtInd = "0";
                    }
                    return _financialInfo;
                }
                return new FinancialInfoModel();
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion
        #region "CostCentre"

        //public async Task<List<CostCentre>> FtCostCentreListSelect(CostCentre CostCentre)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
        //    try
        //    {
        //        objDataEngine.InitiateConnection();
        //        SqlParameter[] Parameters = new SqlParameter[3];
        //        Parameters[0] = String.IsNullOrEmpty(CostCentre.RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", CostCentre.RefTo);
        //        Parameters[1] = String.IsNullOrEmpty(CostCentre.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", CostCentre.RefKey);
        //        Parameters[2] = new SqlParameter("@IssNo", GetIssNo);

        //        var execResult = await objDataEngine.ExecuteCommandAsync("WebCostCentreListSelect", CommandType.StoredProcedure, Parameters);

        //        var _CostCentre = new List<CostCentre>();
        //        while (execResult.Read())
        //        {
        //            _CostCentre.Add(new CostCentre
        //            {
        //                SelectedCostCentre = Convert.ToString(execResult["CostCentre"]),
        //                Descp = Convert.ToString(execResult["Descp"]),
        //                PersoninCharge = Convert.ToString(execResult["PersoninCharge"])
        //            });
        //        };
        //        return _CostCentre;
        //    }
        //    finally
        //    {
        //        objDataEngine.CloseConnection();
        //    }

        //}

        public async Task<List<CostCentre>> WebCostCentreSearch(CostCentre CostCentre)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[4];
                Parameters[0] = String.IsNullOrEmpty(CostCentre.RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", CostCentre.RefTo);
                Parameters[1] = String.IsNullOrEmpty(CostCentre.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", CostCentre.RefKey);
                Parameters[2] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[3] = String.IsNullOrEmpty(CostCentre.SelectedCostCentre) ? new SqlParameter("@CostCentre", DBNull.Value) : new SqlParameter("@CostCentre", CostCentre.SelectedCostCentre);


                var execResult = await objDataEngine.ExecuteCommandAsync("WebCostCentreSearch", CommandType.StoredProcedure, Parameters);
                var _CostCentre = new List<CostCentre>();
                while (execResult.Read())
                {
                    _CostCentre.Add(new CostCentre
                    {
                        SelectedCostCentre = Convert.ToString(execResult["CostCentre"]),
                        Descp = Convert.ToString(execResult["Descp"]),
                        PersoninCharge = Convert.ToString(execResult["PersoninCharge"]),
                    });
                };
                return _CostCentre;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

        }

        //public async Task<CostCentre> WebCostCentreSelect(CostCentre CostCentre)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

        //    try
        //    {
        //        objDataEngine.InitiateConnection();
        //        SqlParameter[] Parameters = new SqlParameter[4];
        //        Parameters[0] = String.IsNullOrEmpty(CostCentre.RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", CostCentre.RefTo);
        //        Parameters[1] = String.IsNullOrEmpty(CostCentre.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", CostCentre.RefKey);
        //        Parameters[2] = new SqlParameter("@IssNo", GetIssNo);
        //        Parameters[3] = String.IsNullOrEmpty(CostCentre.SelectedCostCentre) ? new SqlParameter("@CostCentre", DBNull.Value) : new SqlParameter("@CostCentre", CostCentre.SelectedCostCentre);

        //        var execResult = await objDataEngine.ExecuteCommandAsync("WebCostCentreSelect", CommandType.StoredProcedure, Parameters);
        //        var _CostCentre = new CostCentre();
        //        while (execResult.Read())
        //        {
        //            _CostCentre.SelectedCostCentre = Convert.ToString(execResult["CostCentre"]);
        //            _CostCentre.Descp = Convert.ToString(execResult["Descp"]);
        //            _CostCentre.PersoninCharge = Convert.ToString(execResult["PersoninCharge"]);
        //        };
        //        return _CostCentre;
        //    }
        //    finally
        //    {
        //        objDataEngine.CloseConnection();
        //    }
        //}
        //public async Task<MsgRetriever> WebCostCentreMaint(CostCentre _CostCentre)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);

        //    try
        //    {
        //        await objDataEngine.InitiateConnectionAsync();
        //        SqlParameter[] Parameters = new SqlParameter[8];
        //        SqlCommand cmd = new SqlCommand();
        //        Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
        //        Parameters[1] = String.IsNullOrEmpty(_CostCentre.RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", _CostCentre.RefTo);
        //        Parameters[2] = String.IsNullOrEmpty(_CostCentre.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@refKey", _CostCentre.RefKey);
        //        Parameters[3] = String.IsNullOrEmpty(_CostCentre.SelectedCostCentre) ? new SqlParameter("@CostCentre", DBNull.Value) : new SqlParameter("@CostCentre", _CostCentre.SelectedCostCentre);
        //        Parameters[4] = String.IsNullOrEmpty(_CostCentre.Descp) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _CostCentre.Descp);
        //        Parameters[5] = String.IsNullOrEmpty(_CostCentre.PersoninCharge) ? new SqlParameter("@PersonInCharge", DBNull.Value) : new SqlParameter("@PersonInCharge", _CostCentre.PersoninCharge);
        //        Parameters[6] = new SqlParameter("@UserId", this.GetUserId);// String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@userId", DBNull.Value) : new SqlParameter("@userId", this.GetUserId);
        //        Parameters[7] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
        //        Parameters[7].Direction = ParameterDirection.ReturnValue;
        //        var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebCostCentreMaint", CommandType.StoredProcedure, Parameters);
        //        var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
        //        var Descp = await GetMessageCode(Result);
        //        return Descp;
        //    }
        //    finally
        //    {
        //        objDataEngine.CloseConnection();
        //    }
        //}
        #endregion
        #region "VelocityLimits"

        public async Task<MsgRetriever> ftCustAcctVelocityMaint(VeloctyLimitListMaintModel _VelocityLimitList, string Func)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[15];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(Func) ? new SqlParameter("@Func", DBNull.Value) : new SqlParameter("@Func", Func);
                Parameters[2] = String.IsNullOrEmpty(_VelocityLimitList._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _VelocityLimitList._CardnAccNo.AccNo);
                Parameters[3] = String.IsNullOrEmpty(_VelocityLimitList._CardnAccNo.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _VelocityLimitList._CardnAccNo.CardNo);
                Parameters[4] = String.IsNullOrEmpty(_VelocityLimitList.CostCentre) ? new SqlParameter("@CostCentre", DBNull.Value) : new SqlParameter("@CostCentre", _VelocityLimitList.CostCentre);
                Parameters[5] = String.IsNullOrEmpty(_VelocityLimitList.ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", _VelocityLimitList.ApplId);
                Parameters[6] = String.IsNullOrEmpty(_VelocityLimitList.AppcId) ? new SqlParameter("@AppcId", DBNull.Value) : new SqlParameter("@AppcId", _VelocityLimitList.AppcId);
                Parameters[7] = String.IsNullOrEmpty(_VelocityLimitList.SelectedVelocityInd) ? new SqlParameter("@VelocityInd", DBNull.Value) : new SqlParameter("@VelocityInd", _VelocityLimitList.SelectedVelocityInd);
                Parameters[8] = new SqlParameter("@ProdCd", 0);
                Parameters[9] = new SqlParameter("@VelocityLimit", ConvertDecimalToDb(_VelocityLimitList.VelocityLimit));
                Parameters[10] = new SqlParameter("@VelocityCnt", ConvertIntToDb(_VelocityLimitList.CntrLimit));
                Parameters[11] = new SqlParameter("@VelocityLitre", ConvertIntToDb(_VelocityLimitList.VelocityLitre));//String.IsNullOrEmpty(_VelocityLimitList.VelocityLitre) ? new SqlParameter("@VelocityLitre", DBNull.Value) : new SqlParameter("@VelocityLitre", _VelocityLimitList.VelocityLitre);
                Parameters[12] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@userId", DBNull.Value) : new SqlParameter("@userId", this.GetUserId);
                Parameters[13] = String.IsNullOrEmpty(_VelocityLimitList.SelectedCorpCd) ? new SqlParameter("@CorpCd", DBNull.Value) : new SqlParameter("@CorpCd", _VelocityLimitList.SelectedCorpCd);
                Parameters[14] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[14].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebVelocityLimitMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        public async Task<MsgRetriever> DelCustAcctVelocity(string AcctNo, string CardNo, string ApplId, string AppcId, string VelocityInd, string ProdCd, string CostCenter, string corpCd)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[10];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
                Parameters[2] = String.IsNullOrEmpty(CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", CardNo);
                Parameters[3] = String.IsNullOrEmpty(ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", ApplId);
                Parameters[4] = String.IsNullOrEmpty(AppcId) ? new SqlParameter("@AppcId", DBNull.Value) : new SqlParameter("@AppcId", AppcId);
                Parameters[5] = String.IsNullOrEmpty(VelocityInd) ? new SqlParameter("@VelocityInd", DBNull.Value) : new SqlParameter("@VelocityInd", VelocityInd);
                Parameters[6] = new SqlParameter("@ProdCd", 0);
                Parameters[7] = String.IsNullOrEmpty(CostCenter) ? new SqlParameter("@CostCentre", DBNull.Value) : new SqlParameter("@CostCentre", CostCenter);
                Parameters[8] = String.IsNullOrEmpty(corpCd) ? new SqlParameter("@CorpCd", DBNull.Value) : new SqlParameter("@CorpCd", corpCd);
                //    Parameters[7] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
                Parameters[9] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[9].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebVelocityLimitDelete", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);


                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        #endregion
        #region "AcctGuarantee"

        public async Task<List<AcctGuarantee>> GetAcctGuaranteeList(AcctGuarantee _AcctGuarantee)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[4];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_AcctGuarantee._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _AcctGuarantee._CardnAccNo.AccNo);
                Parameters[2] = String.IsNullOrEmpty(_AcctGuarantee.ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", _AcctGuarantee.ApplId);
                Parameters[3] = char.IsWhiteSpace(_AcctGuarantee.Page) ? new SqlParameter("@Page", DBNull.Value) : new SqlParameter("@Page", _AcctGuarantee.Page);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebAcctGuaranteeListSelect", CommandType.StoredProcedure, Parameters);
                var _AcctGuaranteeList = new List<AcctGuarantee>();

                while (execResult.Read())
                {
                    _AcctGuaranteeList.Add(new AcctGuarantee
                    {
                        DepositType = Convert.ToString(execResult["Deposit Type"]),
                        DepositAmt = ConverterDecimal(execResult["Deposit Amount"]),
                        BankCd = await WebGetRefLib("Bank"),
                        SelectedBankCd = Convert.ToString(execResult["Bank Name"]),
                        BankAcctNo = Convert.ToString(execResult["Bank Account No"]),
                        BankBranchCd = Convert.ToString(execResult["Bank Branch Cd"]),
                        EffFromDate = DateConverter(execResult["From Date"]),
                        EffToDate = DateConverter(execResult["To Date"]),
                        LastUpdDate = DateConverter(execResult["Last Update Date"]),
                        UserId = Convert.ToString(execResult["User Id"]),
                        CreationDate = DateConverter(execResult["Creation Date"]),
                        Remarks = Convert.ToString(execResult["Remarks"])

                    });
                };
                return _AcctGuaranteeList;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        public async Task<AcctGuarantee> FtAcctGuaranteeDetail(AcctGuarantee _AcctGuarantee)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[4];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_AcctGuarantee._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _AcctGuarantee._CardnAccNo.AccNo);
                Parameters[2] = String.IsNullOrEmpty(_AcctGuarantee.ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", _AcctGuarantee.ApplId);
                Parameters[3] = String.IsNullOrEmpty(_AcctGuarantee.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _AcctGuarantee.TxnId);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebAcctGuaranteeSelect", CommandType.StoredProcedure, Parameters);
                var _getAcctGuarantee = new AcctGuarantee();

                while (execResult.Read())
                {
                    _AcctGuarantee.DepositType = Convert.ToString(execResult["DepositType"]);
                    _AcctGuarantee.DepositAmt = ConverterDecimal(execResult["DepositAmt"]);
                    _AcctGuarantee.BankCd = await WebGetRefLib("Bank");
                    _AcctGuarantee.SelectedBankCd = Convert.ToString(execResult["BankName"]);
                    _AcctGuarantee.BankAcctNo = Convert.ToString(execResult["BankAcctNo"]);
                    _AcctGuarantee.BankBranchCd = Convert.ToString(execResult["BankBranchCd"]);
                    _AcctGuarantee.EffFromDate = DateConverter(execResult["EffFromDate"]);
                    _AcctGuarantee.EffToDate = DateConverter(execResult["EffToDate"]);
                    _AcctGuarantee.Remarks = Convert.ToString(execResult["Remarks"]);
                    _AcctGuarantee.UserId = Convert.ToString(execResult["UserId"]);
                };
                return _getAcctGuarantee;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }


        }

        public async Task<MsgRetriever> FtAcctGuaranteeMaint(AcctGuarantee _AcctGuarantee)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[13];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_AcctGuarantee._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _AcctGuarantee._CardnAccNo.AccNo);
                Parameters[2] = String.IsNullOrEmpty(_AcctGuarantee.ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", _AcctGuarantee.ApplId);
                Parameters[3] = String.IsNullOrEmpty(_AcctGuarantee.DepositType) ? new SqlParameter("@DepositType", DBNull.Value) : new SqlParameter("@DepositType", _AcctGuarantee.DepositType);
                Parameters[4] = new SqlParameter("@DepositAmt", _AcctGuarantee.DepositAmt);
                Parameters[5] = String.IsNullOrEmpty(_AcctGuarantee.SelectedBankCd) ? new SqlParameter("@BankName", DBNull.Value) : new SqlParameter("@BankName", _AcctGuarantee.SelectedBankCd);
                Parameters[6] = String.IsNullOrEmpty(_AcctGuarantee.BankAcctNo) ? new SqlParameter("@BankAcctNo", DBNull.Value) : new SqlParameter("@BankAcctNo", _AcctGuarantee.BankAcctNo);
                Parameters[7] = String.IsNullOrEmpty(_AcctGuarantee.BankBranchCd) ? new SqlParameter("@BankBranchCd", DBNull.Value) : new SqlParameter("@BankBranchCd", _AcctGuarantee.BankBranchCd);
                Parameters[8] = new SqlParameter("@EffFromDate", DateConverterDB(_AcctGuarantee.EffFromDate));
                Parameters[9] = new SqlParameter("@EffToDate", DateConverterDB(_AcctGuarantee.EffToDate));
                Parameters[10] = String.IsNullOrEmpty(_AcctGuarantee.Remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", _AcctGuarantee.Remarks);
                Parameters[11] = String.IsNullOrEmpty(_AcctGuarantee.UserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", _AcctGuarantee.UserId);
                Parameters[12] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[12].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebAcctGuaranteeMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();

            }


        }

        #endregion
        #region "AcctPostedTxnSearch"

        public async Task<List<AcctPostedTxnSearch>> FtAcctPostedTxnSearch(AcctPostedTxnSearch _acctPostedTxnSearch)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[7];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_acctPostedTxnSearch.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _acctPostedTxnSearch.AcctNo);
                Parameters[2] = String.IsNullOrEmpty(_acctPostedTxnSearch.SelectedCardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _acctPostedTxnSearch.SelectedCardNo);
                Parameters[3] = String.IsNullOrEmpty(_acctPostedTxnSearch.SelectedTxnCategory) ? new SqlParameter("@TxnCategory", DBNull.Value) : new SqlParameter("@TxnCategory", _acctPostedTxnSearch.SelectedTxnCategory);
                Parameters[4] = String.IsNullOrEmpty(_acctPostedTxnSearch.SelectedTxnCd) ? new SqlParameter("@TxnCd", DBNull.Value) : new SqlParameter("@TxnCd", _acctPostedTxnSearch.SelectedTxnCd);
                Parameters[5] = new SqlParameter("@FromDate", ConvertDatetimeDB(_acctPostedTxnSearch.FromDate));
                Parameters[6] = new SqlParameter("@ToDate", ConvertDatetimeDB(_acctPostedTxnSearch.ToDate));

                var execResult = await objDataEngine.ExecuteCommandAsync("WebAcctPostedTxnSearch", CommandType.StoredProcedure, Parameters);
                var _AcctPostedTxnSearch = new List<AcctPostedTxnSearch>();

                while (execResult.Read())
                {
                    _AcctPostedTxnSearch.Add(new AcctPostedTxnSearch
                    {
                        TxnDate = Convert.ToString(execResult["TxnDate"]),
                        SelectedCardNo = Convert.ToString(execResult["CardNo"]),
                        TxnDesp = Convert.ToString(execResult["Txn Descp"]),
                        TxnAmt = ConverterDecimal(execResult["Txn Amt"]),
                        Dealer = Convert.ToString(execResult["Dealer"]),
                        TermId = Convert.ToString(execResult["TermId"]),
                        AuthCardNo = Convert.ToString(execResult["AuthCardNo"]),
                        PrcsDate = DateConverter(execResult["PrcsDate"]),
                        TxnId = Convert.ToString(execResult["Txn Id"]),
                        InvoicDt = DateConverter(execResult["StatementDate"]),
                        SiteId = Convert.ToString(execResult["SiteId"]),
                        RecieptId = Convert.ToString(execResult["Receipt No"]),
                        Batch = Convert.ToString(execResult["BatchNo"]),
                        VehRegNo = Convert.ToString(execResult["VehRegsNo"]),
                        DriverName = Convert.ToString(execResult["Driver Name"]),
                        TotalTxnAmt = ConverterDecimal(execResult["TotalTxnAmt"]),
                        Quantity = ConverterDecimal(execResult["Qty"]),
                        TaxInvoiceNo = Convert.ToString(execResult["TaxInvoiceNo"]),
                        VATAmt = ConverterDecimal(execResult["VAT Amt"]),
                        BaseAmt = ConverterDecimal(execResult["Base Amt"]),
                        ApproveCd = Convert.ToString(execResult["AppvCd"]),
                        RRn = Convert.ToString(execResult["RRn"]),
                        ProductDescp = Convert.ToString(execResult["ProductDescp"]),
                        Stan = Convert.ToString(execResult["Stan"]),
                        VATNo = Convert.ToString(execResult["VAT No"]),
                        ProductAmt = ConverterDecimal(execResult["ProductAmt"]),
                        VATCd = Convert.ToString(execResult["VATCd"]),
                        VATRate = ConverterDecimal(execResult["VATRate"]),
                    });
                };
                return _AcctPostedTxnSearch;
            }
            finally
            {
                objDataEngine.CloseConnection();

            }


        }

        #endregion
        #region "Event Logger"

        public async Task<MsgRetriever> FtEventMaint(EventLogger _Logger)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[13];
                Parameters[0] = new SqlParameter("@Module", "I");
                Parameters[1] = String.IsNullOrEmpty(_Logger.EventId) ? new SqlParameter("@EventId", DBNull.Value) : new SqlParameter("@EventId", _Logger.EventId);
                Parameters[2] = String.IsNullOrEmpty(_Logger.SelectedEventType) ? new SqlParameter("@EventType", DBNull.Value) : new SqlParameter("@EventType", _Logger.SelectedEventType);
                Parameters[3] = String.IsNullOrEmpty(_Logger.acctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _Logger.acctNo);
                Parameters[4] = String.IsNullOrEmpty(_Logger.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", _Logger.RefKey);
                Parameters[5] = String.IsNullOrEmpty(_Logger.Description) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _Logger.Description);
                Parameters[6] = String.IsNullOrEmpty(_Logger.SelectedReasonCode) ? new SqlParameter("@ReasonCd", DBNull.Value) : new SqlParameter("@ReasonCd", _Logger.SelectedReasonCode);

                Parameters[7] = new SqlParameter("@ReminderDate", ConvertDatetimeDB(_Logger.ReminderDatetime));
                Parameters[8] = String.IsNullOrEmpty(_Logger.refDoc) ? new SqlParameter("@XrefDoc", DBNull.Value) : new SqlParameter("@XrefDoc", _Logger.refDoc);
                Parameters[9] = new SqlParameter("@ClsDate", DateConverterDB(_Logger.ClosedDate));
                Parameters[10] = String.IsNullOrEmpty(_Logger.SelectedEventSts) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _Logger.SelectedEventSts);
                Parameters[11] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);

                Parameters[12] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[12].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebEventMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }


        }

        #endregion
        #region "Event Detail"

        public async Task<List<EventDetails>> FtEventDetaillist(EventDetails _eventDetails)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[4];
                Parameters[0] = new SqlParameter("@Module", "I");
                Parameters[1] = String.IsNullOrEmpty(_eventDetails.AccountNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _eventDetails.AccountNo);
                Parameters[2] = String.IsNullOrEmpty(_eventDetails.BusnLoc) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", _eventDetails.BusnLoc);
                Parameters[3] = String.IsNullOrEmpty(_eventDetails.EventId) ? new SqlParameter("@EventId", DBNull.Value) : new SqlParameter("@EventId", _eventDetails.EventId);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebEventDetailListSelect", CommandType.StoredProcedure, Parameters);
                var EventDetailList = new List<EventDetails>();

                while (execResult.Read())
                {
                    EventDetailList.Add(new EventDetails
                    {
                        EventId = Convert.ToString(execResult["Event Id"]),
                        SeqNo = Convert.ToString(execResult["SeqNo"]),
                        Description = Convert.ToString(execResult["Description"]),
                        ReminderDatetime = Convert.ToString(execResult["Reminder"]),
                        CreationBy = Convert.ToString(execResult["CreatedBy"]),
                        CreationDate = Convert.ToString(execResult["CreationDate"]),

                    });
                };
                return EventDetailList;
            }
            finally
            {
                objDataEngine.CloseConnection();

            }


        }

        public async Task<EventDetails> GetWebEventDetailSelect(EventDetails _eventDetails)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[4];
                Parameters[0] = new SqlParameter("@Module", "I");
                Parameters[1] = String.IsNullOrEmpty(_eventDetails.AccountNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _eventDetails.AccountNo);
                Parameters[2] = String.IsNullOrEmpty(_eventDetails.BusnLoc) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", _eventDetails.BusnLoc);
                Parameters[3] = String.IsNullOrEmpty(_eventDetails.EventId) ? new SqlParameter("@EventId", DBNull.Value) : new SqlParameter("@EventId", _eventDetails.EventId);


                var execResult = await objDataEngine.ExecuteCommandAsync("WebEventDetailSelect", CommandType.StoredProcedure, Parameters);
                while (execResult.Read())
                {

                    var _eventlogger = new EventDetails
                    {

                        EventId = Convert.ToString(execResult["Event Id"]),
                        SeqNo = Convert.ToString(execResult["SeqNo"]),
                        Description = Convert.ToString(execResult["Description"]),
                        ReminderDatetime = Convert.ToString(execResult["Reminder"]),
                        CreationBy = Convert.ToString(execResult["CreatedBy"]),
                        CreationDate = Convert.ToString(execResult["CreationDate"])

                    };
                    return _eventlogger;
                };
                return new EventDetails();
            }
            finally
            {
                objDataEngine.CloseConnection();

            }
        }
        public async Task<MsgRetriever> SaveEventDetailMaint(EventDetails _EventDetails)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[6];
                Parameters[0] = new SqlParameter("@Module", "I");
                Parameters[1] = String.IsNullOrEmpty(_EventDetails.EventId) ? new SqlParameter("@EventId", DBNull.Value) : new SqlParameter("@EventId", _EventDetails.EventId);
                Parameters[2] = String.IsNullOrEmpty(_EventDetails.Description) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _EventDetails.Description);
                Parameters[3] = new SqlParameter("@ReminderDate", DateConverterDB(_EventDetails.ReminderDatetime));
                Parameters[4] = new SqlParameter("@UserId", this.GetUserId);
                Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[5].Direction = ParameterDirection.ReturnValue;
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebEventDetailMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();

            }


        }

        #endregion
        #region "PaymentTxns"

        public async Task<MsgRetriever> FtPaymentTxnMaint(PaymentTxn _PaymentTxn)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[19];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_PaymentTxn.PyTxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _PaymentTxn.PyTxnId);
                Parameters[2] = String.IsNullOrEmpty(_PaymentTxn.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _PaymentTxn.AcctNo);
                Parameters[3] = new SqlParameter("@CardNo", DBNull.Value);// : new SqlParameter("@CardNo", _PaymentTxn.CardNo); //  String.IsNullOrEmpty(_PaymentTxn.CardNo) ?
                Parameters[4] = String.IsNullOrEmpty(_PaymentTxn.SelectedTxnCd) ? new SqlParameter("@TxnCd", DBNull.Value) : new SqlParameter("@TxnCd", _PaymentTxn.SelectedTxnCd);
                Parameters[5] = new SqlParameter("@TxnDate", ConvertDatetimeDB(_PaymentTxn.TxnDate));
                Parameters[6] = new SqlParameter("@BookingDate", ConvertDatetimeDB(_PaymentTxn.BookingDt));
                Parameters[7] = new SqlParameter("@DueDate", ConvertDatetimeDB(_PaymentTxn.DueDt));
                Parameters[8] = new SqlParameter("@TxnAmt", ConvertDecimalToDb(_PaymentTxn.TotAmnt));
                Parameters[9] = new SqlParameter("@Pts", ConvertDecimalToDb(_PaymentTxn.Totpts));
                Parameters[10] = String.IsNullOrEmpty(_PaymentTxn.Descp) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _PaymentTxn.Descp);
                Parameters[11] = String.IsNullOrEmpty(_PaymentTxn.SelectedAppvCd) ? new SqlParameter("@AppvCd", DBNull.Value) : new SqlParameter("@AppvCd", _PaymentTxn.SelectedAppvCd);
                Parameters[12] = new SqlParameter("@CheqNo", ConvertInt(_PaymentTxn.CheqNo));
                Parameters[13] = String.IsNullOrEmpty(_PaymentTxn.SelectedSts) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _PaymentTxn.SelectedSts);
                Parameters[14] = new SqlParameter("@RcptNo", SqlDbType.VarChar, 19);
                Parameters[14].Direction = ParameterDirection.Output;
                Parameters[15] = new SqlParameter("@RetCd", SqlDbType.VarChar, 19);
                Parameters[15].Direction = ParameterDirection.Output;
                Parameters[16] = new SqlParameter("@UserId", this.GetUserId);
                Parameters[17] = String.IsNullOrEmpty(_PaymentTxn.selectedOwner) ? new SqlParameter("@Owner", DBNull.Value) : new SqlParameter("@Owner", _PaymentTxn.selectedOwner);
                Parameters[18] = new SqlParameter("@RETURN_VALUE", SqlDbType.VarChar, 19);
                Parameters[18].Direction = ParameterDirection.ReturnValue;
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebTxnPaymentMaint", CommandType.StoredProcedure, Parameters);
                var Result = ConvertInt(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();

            }



        }

        #endregion
        #region"BillingItem"

        public async Task<List<BillingItem>> FtBillingItemTxnList(int TxnId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@TxnId", TxnId);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebBillingItemTransactionListSelect", CommandType.StoredProcedure, Parameters);
                var _bi = new List<BillingItem>();

                while (execResult.Read())
                {
                    _bi.Add(new BillingItem
                    {
                        TxnId = ConvertInt(execResult["TxnId"]),
                        TxnDate = DateConverter(execResult["TxnDate"]),
                        BookingDate = DateConverter(execResult["BookingDate"]),
                        PrcsDate = DateConverter(execResult["PrcsDate"]),
                        BillingTxnAmt = Convert.ToDecimal(execResult["BillingTxnAmt"]).ToString("0.00"),
                        DisplayBillingTxnAmt = Convert.ToDecimal(execResult["BillingTxnAmt"]).ToString("0.00"),
                        Descp = Convert.ToString(execResult["Descp"]),
                        BusnLocation = Convert.ToString(execResult["BusnLocation"]),
                        TermId = Convert.ToString(execResult["TermId"]),

                        _CardnAccNo = new CardnAccNo
                        {
                            CardNo = Convert.ToString(execResult["CardNo"])
                        },

                        _CreationDatenUserId = new CreationDatenUserId
                        {
                            UserId = Convert.ToString(execResult["UserId"])
                        }
                    });
                };
                return _bi;
            }
            finally
            {
                objDataEngine.CloseConnection();

            }

        }

        public async Task<List<BillingItem>> FtBillingItemSettlementList(int TxnId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@TxnId", TxnId);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebBillingItemSettlementListSelect", CommandType.StoredProcedure, Parameters);

                var _bi = new List<BillingItem>();
                while (execResult.Read())
                {

                    _bi.Add(new BillingItem
                    {
                        TxnId = ConvertInt(execResult["TxnSeq"]),
                        SettledDate = DateConverter(execResult["SettledDate"]),
                        SettledAmt = Convert.ToString(execResult["SettledAmt"]),
                        DisplaySettledAmt = Convert.ToDecimal(execResult["SettledAmt"]).ToString("0.00"),
                        PrcsDate = DateConverter(execResult["PrcsDate"]),
                        RefId = Convert.ToString(execResult["SourceRefId"]),

                        _CreationDatenUserId = new CreationDatenUserId
                        {
                            UserId = Convert.ToString(execResult["UserId"]),
                            CreationDate = DateConverter(execResult["CreationDate"])
                        }
                    });

                };
                return _bi;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<List<BillingItem>> SearchBillingItem(BillingItem bi)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[6];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(bi._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", bi._CardnAccNo.AccNo);
                Parameters[2] = new SqlParameter("@FromDate", ConvertDatetimeDB(bi.FromDate));
                Parameters[3] = new SqlParameter("@ToDate", ConvertDatetimeDB(bi.ToDate));
                Parameters[4] = String.IsNullOrEmpty(bi.SelectedTxnCategory) ? new SqlParameter("@TxnCategory", DBNull.Value) : new SqlParameter("@TxnCategory", bi.SelectedTxnCategory);
                Parameters[5] = String.IsNullOrEmpty(bi.SelectedSts) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", bi.SelectedSts);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebBillingAllItemSearch", CommandType.StoredProcedure, Parameters);

                var _bi = new List<BillingItem>();
                while (execResult.Read())
                {

                    _bi.Add(new BillingItem
                    {
                        Level = Convert.ToString(execResult["Level"]),
                        TxnId = ConvertInt(execResult["TxnId"]),
                        ClosedDate = DateConverter(execResult["CloseAt"]),
                        Descp = Convert.ToString(execResult["Descp"]),
                        TxnDate = DateConverter(execResult["TxnDate"]),
                        DueDate = DateConverter(execResult["DueDate"]),
                        DisplayBillingTxnAmt = ConverterDecimal(execResult["BillingAmt"]),
                        SettledAmt = ConverterDecimal(execResult["SettledAmt"]),
                        DisplaySettledAmt = ConverterDecimal(execResult["SettledAmt"]),
                        SettledDate = DateConverter(execResult["SettledDate"]),
                        //DocRefNo = Convert.ToString(execResult["DocRefNo"]),
                        TxnCd = Convert.ToString(execResult["TxnCd"]),
                        RefId = Convert.ToString(execResult["RefId"]),
                        SelectedSts = Convert.ToString(execResult["Sts"]),
                        TarBalance = ConverterDecimal(execResult["TAR Balance"]),
                        TotalTxnAmount = ConverterDecimal(execResult["TotalBillingTxnAmt"]),
                        TotalSettledAmt = ConverterDecimal(execResult["TotalSettledAmt"]),

                        _CreationDatenUserId = new CreationDatenUserId
                        {
                            UserId = Convert.ToString(execResult["UserId"]),
                            CreationDate = DateConverter(execResult["CreationDate"])
                        }
                    });

                };
                return _bi;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

        }

        #endregion
        #region "AcctDepositInfo"

        public async Task<CreditAssesOperation> FtAcctDepositInfoDetail(string TxnId, string applid = null, string acctNo = null, string corpCd = null)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[5];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(applid) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", applid);
                Parameters[2] = String.IsNullOrEmpty(acctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", acctNo);
                Parameters[3] = String.IsNullOrEmpty(TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", TxnId);
                Parameters[4] = String.IsNullOrEmpty(corpCd) ? new SqlParameter("@CorpCd", DBNull.Value) : new SqlParameter("@CorpCd", corpCd);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebAcctDepositInfoSelect", CommandType.StoredProcedure, Parameters);

                var _GetAcctDeptInfo = new CreditAssesOperation();
                while (execResult.Read())
                {
                    _GetAcctDeptInfo.DirectDebitInd = BoolConverter(execResult["DirectDebitInd"]);
                    _GetAcctDeptInfo.SelectedDepositType = Convert.ToString(execResult["DepositType"]);
                    _GetAcctDeptInfo.ValidityDate = DateConverter(execResult["ValidityDate"]);
                    _GetAcctDeptInfo.SelectedBankAcctType = Convert.ToString(execResult["BankAcctType"]);
                    _GetAcctDeptInfo.SelectedBankName = Convert.ToString(execResult["BankName"]);
                    _GetAcctDeptInfo.BankAcctNo = Convert.ToString(execResult["BankAcctNo"]);
                    _GetAcctDeptInfo.DepositAmt = ConverterDecimal(execResult["DepositAmt"]);
                    _GetAcctDeptInfo.BankBranchCode = Convert.ToString(execResult["BGSerialNo"]);
                    _GetAcctDeptInfo.DepositFromDate = DateConverter(execResult["EffFromDate"]);
                    _GetAcctDeptInfo.DepositToDate = DateConverter(execResult["EffToDate"]);
                    _GetAcctDeptInfo.NRID = Convert.ToString(execResult["NIRD"]);
                    _GetAcctDeptInfo.remarks = Convert.ToString(execResult["Remarks"]);
                    _GetAcctDeptInfo.Txnid = Convert.ToString(execResult["TxnId"]);
                    _GetAcctDeptInfo.UserId = Convert.ToString(execResult["UserId"]);
                    _GetAcctDeptInfo.Creationdt = Convert.ToString(execResult["CreationDate"]);
                    _GetAcctDeptInfo.SecurityAmt = Convert.ToString(execResult["SecurityDeposit"]);
                    _GetAcctDeptInfo.SAPRefNo = Convert.ToString(execResult["SAPRefNo"]);
                    _GetAcctDeptInfo.remarkHistory = await GetSecDepRemarksListSelect(acctNo, "secdep", Convert.ToString(execResult["TxnId"]));                 
                };
                return _GetAcctDeptInfo;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<IList<RemarkHistory>> GetSecDepRemarksListSelect(string accountNo, string eventType,string txnId)
        {
            IList<RemarkHistory> remarks = new List<RemarkHistory>();
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = String.IsNullOrEmpty(accountNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", accountNo);
                Parameters[1] = String.IsNullOrEmpty(eventType) ? new SqlParameter("@EventType", DBNull.Value) : new SqlParameter("eventType", eventType);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebSecDepRemarksListSelect", CommandType.StoredProcedure, Parameters);
                while(execResult.Read())
                {
                    remarks.Add(new RemarkHistory {
                                    Content = Convert.ToString(execResult["RemarkHistory"]),
                                    UserId = Convert.ToString(execResult["CreatedBy"]),
                                    CreationDate = Convert.ToString(execResult["CreationDate"]),
                                    TxnId = Convert.ToString(execResult["TxnId"])});
                }
                if (eventType.ToLower() != "cao")
                    return remarks.Where(p => p.TxnId.ToLower().Contains(txnId)).ToList();
                else
                    return remarks;
            }catch( Exception ex)
            {
                System.Exception conEx = new System.Exception(ex.Message);
                objDataEngine.CloseConnection();
                throw conEx;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<MsgRetriever> AcctDepositInfoMaint(CreditAssesOperation _AcctDeptInfo, string applId = null, string corpCd = null)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[21];
                SqlCommand cmd = new SqlCommand();

                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(applId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", applId);
                Parameters[2] = String.IsNullOrEmpty(_AcctDeptInfo.acctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _AcctDeptInfo.acctNo);
                Parameters[3] = String.IsNullOrEmpty(corpCd) ? new SqlParameter("@CorpCd", DBNull.Value) : new SqlParameter("@CorpCd", corpCd);
                Parameters[4] = String.IsNullOrEmpty(_AcctDeptInfo.Txnid) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _AcctDeptInfo.Txnid);
                Parameters[5] = new SqlParameter("@DepositInd", ConvertBoolDB(_AcctDeptInfo.DirectDebitInd));
                Parameters[6] = String.IsNullOrEmpty(_AcctDeptInfo.SelectedDepositType) ? new SqlParameter("@DepositType", DBNull.Value) : new SqlParameter("@DepositType", _AcctDeptInfo.SelectedDepositType);
                Parameters[7] = new SqlParameter("@ValidityDate", ConvertDatetimeDB(_AcctDeptInfo.ValidityDate));
                Parameters[8] = String.IsNullOrEmpty(_AcctDeptInfo.SelectedBankAcctType) ? new SqlParameter("@BankAcctType", DBNull.Value) : new SqlParameter("@BankAcctType", _AcctDeptInfo.SelectedBankAcctType);
                Parameters[9] = String.IsNullOrEmpty(_AcctDeptInfo.SelectedBankName) ? new SqlParameter("@BankName", DBNull.Value) : new SqlParameter("@BankName", _AcctDeptInfo.SelectedBankName);
                Parameters[10] = String.IsNullOrEmpty(_AcctDeptInfo.BankAcctNo) ? new SqlParameter("@BankAcctNo", DBNull.Value) : new SqlParameter("@BankAcctNo", _AcctDeptInfo.BankAcctNo);
                Parameters[11] = new SqlParameter("@DepositAmt", ConvertDecimalToDb(_AcctDeptInfo.DepositAmt));
                Parameters[12] = String.IsNullOrEmpty(_AcctDeptInfo.BankBranchCode) ? new SqlParameter("@BGSerialNo", DBNull.Value) : new SqlParameter("@BGSerialNo", _AcctDeptInfo.BankBranchCode);
                Parameters[13] = new SqlParameter("@EffFromDate", ConvertDatetimeDB(_AcctDeptInfo.DepositFromDate));
                Parameters[14] = new SqlParameter("@EffToDate", ConvertDatetimeDB(_AcctDeptInfo.DepositToDate));
                Parameters[15] = String.IsNullOrEmpty(_AcctDeptInfo.NRID) ? new SqlParameter("@NIRD", DBNull.Value) : new SqlParameter("@NIRD", DateConverterDB(_AcctDeptInfo.NRID));
                Parameters[16] = String.IsNullOrEmpty(_AcctDeptInfo.remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", (_AcctDeptInfo.remarks));
                Parameters[17] = new SqlParameter("@UserId", this.GetUserId);
                Parameters[18] = String.IsNullOrEmpty(_AcctDeptInfo.SecurityAmt) ? new SqlParameter("@SecurityDepositAmt", DBNull.Value) : new SqlParameter("@SecurityDepositAmt", _AcctDeptInfo.SecurityAmt);
                Parameters[19] = String.IsNullOrEmpty(_AcctDeptInfo.SAPRefNo) ? new SqlParameter("@SAPRefNo", DBNull.Value) : new SqlParameter("@SAPRefNo", _AcctDeptInfo.SAPRefNo);
                Parameters[20] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[20].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebAcctDepositInfoMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }catch(Exception ex)
            {
                objDataEngine.CloseConnection();
                return null;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion
        #region "point adjustment"
        public async Task<List<PointAdjustment>> WebPointAdjustmentListSelect(string AcctNo)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
                Parameters[1] = new SqlParameter("@IssNo", GetIssNo);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebPointAdjustmentListSelect", CommandType.StoredProcedure, Parameters);

                var _PointAdjustment = new List<PointAdjustment>();
                while (execResult.Read())
                {
                    _PointAdjustment.Add(new PointAdjustment
                    {
                        TxnType = Convert.ToString(execResult["Txn Type"]),
                        AccountNo = Convert.ToString(execResult["Account No"]),//add
                        CardNo = Convert.ToString(execResult["Card No"]),
                        Points = ConverterDecimal(execResult["Points"]),
                        TxnAmt = ConverterDecimal(execResult["TxnAmt"]),//add
                        UserId = Convert.ToString(execResult["User Id"]),//add
                        TxnDate = DateTimeConverter(execResult["Txn Date"]),
                        TxnDescription = Convert.ToString(execResult["Txn Description"]),
                        SelectedStatus = Convert.ToString(execResult["Status"]),
                        SelectedTxnCd = Convert.ToString(execResult["TxnCd"]),
                        WUId = Convert.ToString(execResult["WU Id"]),
                        CreationDate = DateConverter(execResult["Creation Date"]),
                        TxnId = Convert.ToString(execResult["Txn Id"])
                    });
                };
                return _PointAdjustment;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        public async Task<PointAdjustment> WebPointAdjustmentSelect(string TxnId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", TxnId);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebPointAdjustmentSelect", CommandType.StoredProcedure, Parameters);
                var _PointAdjustment = new PointAdjustment();
                while (execResult.Read())
                {
                    _PointAdjustment = new PointAdjustment
                    {
                        TxnDate = DateConverter(execResult["TxnDate"]),
                        DueDate = DateConverter(execResult["DueDate"]),
                        SelectedTxnCd = Convert.ToString(execResult["TxnCd"]),
                        CardNo = Convert.ToString(execResult["CardNo"]),
                        Points = ConverterDecimal(execResult["Pts"]),
                        TxnDescription = Convert.ToString(execResult["Descp"]),
                        ApprvCd = Convert.ToString(execResult["AppvCd"]),
                        SelectedStatus = Convert.ToString(execResult["Sts"]),
                        UserId = Convert.ToString(execResult["UserId"]),
                        WithHeldUnsettleId = Convert.ToString(execResult["WithheldUnsettleId"]),
                        CreationDate = DateConverter(execResult["CreationDate"])
                    };
                };
                return _PointAdjustment;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        public async Task<MsgRetriever> WebPointAdjustmentMaint(PointAdjustment _Adj, string AcctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[18];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_Adj.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _Adj.TxnId);
                Parameters[2] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
                Parameters[3] = String.IsNullOrEmpty(_Adj.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _Adj.CardNo);
                Parameters[4] = String.IsNullOrEmpty(_Adj.SelectedTxnCd) ? new SqlParameter("@TxnCd", DBNull.Value) : new SqlParameter("@TxnCd", _Adj.SelectedTxnCd);
                Parameters[5] = String.IsNullOrEmpty(_Adj.TxnDate) ? new SqlParameter("@TxnDate", DBNull.Value) : new SqlParameter("@TxnDate", ConvertDatetimeDB(_Adj.TxnDate));
                Parameters[6] = String.IsNullOrEmpty(_Adj.BookingDate) ? new SqlParameter("@BookingDate", DBNull.Value) : new SqlParameter("@BookingDate", ConvertDatetimeDB(_Adj.BookingDate));
                Parameters[7] = String.IsNullOrEmpty(_Adj.DueDate) ? new SqlParameter("@DueDate", DBNull.Value) : new SqlParameter("@DueDate", ConvertDatetimeDB(_Adj.DueDate));
                Parameters[8] = String.IsNullOrEmpty(_Adj.TxnAmt) ? new SqlParameter("@TxnAmt", DBNull.Value) : new SqlParameter("@TxnAmt", _Adj.TxnAmt);
                Parameters[9] = String.IsNullOrEmpty(_Adj.Points) ? new SqlParameter("@Pts", DBNull.Value) : new SqlParameter("@Pts", _Adj.Points);
                Parameters[10] = String.IsNullOrEmpty(_Adj.TxnDescription) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _Adj.TxnDescription);
                Parameters[11] = new SqlParameter("@CheqNo", _Adj.ChequeNo);
                Parameters[12] = String.IsNullOrEmpty(_Adj.SelectedStatus) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _Adj.SelectedStatus);
                Parameters[13] = new SqlParameter("@RcptNo", _Adj.ReceiptNo);
                Parameters[13].Direction = ParameterDirection.Output;
                Parameters[13].Size = 10;
                Parameters[14] = new SqlParameter("@retCd", _Adj.ReturnCd);
                Parameters[14].Direction = ParameterDirection.Output;
                Parameters[14].Size = 10;
                Parameters[15] = new SqlParameter("@UserId", this.GetUserId);
                Parameters[16] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[16].Direction = ParameterDirection.ReturnValue;
                Parameters[17] = String.IsNullOrEmpty(_Adj.ApprvCd) ? new SqlParameter("@AppvCd", DBNull.Value) : new SqlParameter("@AppvCd", _Adj.ApprvCd);
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebPointAdjustmentMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion "point adjustment"
        #region "Acct Subsidy"

        public async Task<List<SKDS>> GetAcctSubsidyInfoList(string acctNo = null, string skdsNo = null)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(acctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", acctNo);
                Parameters[2] = String.IsNullOrEmpty(skdsNo) ? new SqlParameter("@SKDSNo", DBNull.Value) : new SqlParameter("@SKDSNo", skdsNo);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebAcctSubsidyTagListSelect", CommandType.StoredProcedure, Parameters);
                var SkdsSearchInfo = new List<SKDS>();
                while (execResult.Read())
                {
                    SkdsSearchInfo.Add(new SKDS
                    {
                        VehRegsNo = Convert.ToString(execResult["VehRegsNo"]),
                        CardNo = Convert.ToString(execResult["CardNo"]),
                        CardSts = Convert.ToString(execResult["CardStatus"]),
                        SelectedSts = Convert.ToString(execResult["Status"]),
                        SKDSNo = Convert.ToString(execResult["SKDSNo"]),
                        EffFromDate = Convert.ToString(execResult["EffectiveDate"]),
                        EffToDate = Convert.ToString(execResult["EndDate"]),
                        SKDSQuota = ConverterDecimal(execResult["SKDSQuota"]),
                    });
                };
                return SkdsSearchInfo;
            }
            finally
            {
                objDataEngine.CloseConnection();
                ;
            }
        }


        public async Task<MsgRetriever> SaveAcctSubsidyTag(List<SKDS> SKDS, string AcctNo, string SKDSNo)
        {

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("VehRegsNo");
            dataTable.Columns.Add("CardNo");
            dataTable.Columns.Add("SKDSQuota");
            dataTable.Columns.Add("Sts");
            //dataTable.Columns.Add("SKDSNo");
            //dataTable.Columns.Add("EffFromDate");
            //dataTable.Columns.Add("EffToDate");
            DataRow dr = dataTable.NewRow();
            for (int i = 0; i < SKDS.Count; i++)
            {
                dr["VehRegsNo"] = SKDS[i].VehRegsNo;
                dr["CardNo"] = SKDS[i].CardNo;
                dr["SKDSQuota"] = SKDS[i].SKDSQuota;
                dr["Sts"] = SKDS[i].SelectedSts;
                //dr["SKDSNo"] = SKDS[i].SKDSNo;
                //dr["EffFromDate"] = SKDS[i].EffFromDate;
                //dr["EffToDate"] = SKDS[i].EffToDate;
                dataTable.Rows.Add(dr);
                dr = dataTable.NewRow();
            }

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[6];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
                Parameters[2] = String.IsNullOrEmpty(SKDSNo) ? new SqlParameter("@SKDSNo", DBNull.Value) : new SqlParameter("@SKDSNo", SKDSNo);
                Parameters[3] = new SqlParameter("@Tbl", dataTable);
                Parameters[4] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[4].Direction = ParameterDirection.ReturnValue;
                Parameters[5] = new SqlParameter("@UserId", System.Web.HttpContext.Current.User.Identity.Name);
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebAcctSubsidyTagMaint", CommandType.StoredProcedure, Parameters);
                var Result = ConvertInt(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }


        }
        #endregion
        #region PersonInfo
        public async Task<PersonInfoModel> GetPersonInfo(int issNo, string entityId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", issNo);
                Parameters[1] = String.IsNullOrEmpty(entityId) ? new SqlParameter("@EntityId", DBNull.Value) : new SqlParameter("@EntityId", entityId);
                var Reader = await objDataEngine.ExecuteCommandAsync("WebEntitySelect", CommandType.StoredProcedure, Parameters);

                while (Reader.Read())
                {

                    var _PersonInfo = new PersonInfoModel
                    {
                        title = await BaseClass.WebGetRefLib("Title"),
                        SelectedTitle = Convert.ToString(Reader["Title"]),
                        FirstName = Convert.ToString(Reader["First Name"]),
                        LastName = Convert.ToString(Reader["Last Name"]),
                        IdNo = Convert.ToString(Reader["NewIc"]),
                        IdType = await BaseClass.WebGetRefLib("IcType"),
                        SelectedIdType = Convert.ToString(Reader["NewIcType"]),
                        AltIdNo = Convert.ToString(Reader["AlternateIc"]),
                        AltIdType = await BaseClass.WebGetRefLib("IcType"),
                        SelectedAltIdType = Convert.ToString(Reader["AlternateIcType"]),
                        gender = await BaseClass.WebGetRefLib("Gender"),
                        Selectedgender = Convert.ToString(Reader["Gender"]),
                        DOB = Convert.ToString(Reader["DOB"]),
                        AnnualIncome = BaseClass.ConverterDecimal(Reader["Income"]),
                        Occupation = await BaseClass.WebGetRefLib("Occupation"),
                        SelectedOccupation = Convert.ToString(Reader["Occupation"]),
                        DeptId = await BaseClass.WebGetRefLib("Dept"),
                        SelectedDeptId = Convert.ToString(Reader["DeptId"]),
                        DrivingLicense = Convert.ToString(Reader["DrivingLic"])

                    };
                    return _PersonInfo;
                }
                return new PersonInfoModel()
            {
                title = await BaseClass.WebGetRefLib("Title"),
                IdType = await BaseClass.WebGetRefLib("IcType"),
                AltIdType = await BaseClass.WebGetRefLib("IcType"),
                gender = await BaseClass.WebGetRefLib("Gender"),
                Occupation = await BaseClass.WebGetRefLib("Occupation"),
                DeptId = await BaseClass.WebGetRefLib("Dept")
            };
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        public async Task<MsgRetriever> SavePersonInfo(PersonInfoModel _personInfo, string entityId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[16];
                SqlCommand cmd = new SqlCommand();

                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(entityId) ? new SqlParameter("@EntityId", DBNull.Value) : new SqlParameter("@EntityId", entityId);
                Parameters[2] = String.IsNullOrEmpty(_personInfo.SelectedTitle) ? new SqlParameter("@Title", DBNull.Value) : new SqlParameter("@Title", _personInfo.SelectedTitle);
                Parameters[3] = String.IsNullOrEmpty(_personInfo.FirstName) ? new SqlParameter("@FirstName", DBNull.Value) : new SqlParameter("@FirstName", _personInfo.FirstName);
                Parameters[4] = String.IsNullOrEmpty(_personInfo.LastName) ? new SqlParameter("@LastName", DBNull.Value) : new SqlParameter("@LastName", (_personInfo.LastName));
                Parameters[5] = String.IsNullOrEmpty(_personInfo.SelectedIdType) ? new SqlParameter("@NewIcType", DBNull.Value) : new SqlParameter("@NewIcType", (_personInfo.SelectedIdType));
                Parameters[6] = String.IsNullOrEmpty(_personInfo.IdNo) ? new SqlParameter("@NewIc", DBNull.Value) : new SqlParameter("@NewIc", (_personInfo.IdNo));
                Parameters[7] = String.IsNullOrEmpty(_personInfo.SelectedAltIdType) ? new SqlParameter("@OldIcType", DBNull.Value) : new SqlParameter("@OldIcType", (_personInfo.SelectedAltIdType));
                Parameters[8] = String.IsNullOrEmpty(_personInfo.AltIdNo) ? new SqlParameter("@OldIc", DBNull.Value) : new SqlParameter("@OldIc", (_personInfo.AltIdNo));
                Parameters[9] = String.IsNullOrEmpty(_personInfo.Selectedgender) ? new SqlParameter("@Gender", DBNull.Value) : new SqlParameter("@Gender", (_personInfo.Selectedgender));
                Parameters[10] = new SqlParameter("@DOB", ConvertDatetimeDB(_personInfo.DOB));
                Parameters[11] = new SqlParameter("@Income", ConvertDecimalToDb(_personInfo.AnnualIncome));
                Parameters[12] = String.IsNullOrEmpty(_personInfo.SelectedOccupation) ? new SqlParameter("@Occupation", DBNull.Value) : new SqlParameter("@Occupation", (_personInfo.SelectedOccupation));
                Parameters[13] = String.IsNullOrEmpty(_personInfo.SelectedDeptId) ? new SqlParameter("@DeptId", DBNull.Value) : new SqlParameter("@DeptId", (_personInfo.SelectedDeptId));
                Parameters[14] = String.IsNullOrEmpty(_personInfo.DrivingLicense) ? new SqlParameter("@DrivingLic", DBNull.Value) : new SqlParameter("@DrivingLic", (_personInfo.DrivingLicense));
                //    Parameters[15] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
                Parameters[15] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[15].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebEntityMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }


        #endregion
        #region "Account Users"
        public async Task<List<AccountUser>> GetAccountUsers(string AcctNo, String CorpCd = null)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("AcctNo", DBNull.Value) : new SqlParameter("AcctNo", ConvertLongToDb(AcctNo));
                parameters[2] = String.IsNullOrEmpty(CorpCd) ? new SqlParameter("CorpCd", DBNull.Value) : new SqlParameter("CorpCd", CorpCd);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebUserPrivilegeListSelect", CommandType.StoredProcedure, parameters);
                var _userList = new List<AccountUser>();
                AccountUser User;
                while (execResult.Read())
                {
                    User = new AccountUser();

                    User.PrivilegeCd = Convert.ToString(execResult["PrivilegeCd"]);
                    User.Username = Convert.ToString(execResult["EmailAddr"]);


                    User.Status = Convert.ToString(execResult["sts"]);

                    if (!string.IsNullOrEmpty(CorpCd))
                    {
                        User.AcctNo = Convert.ToString(execResult["AcctNo"]);
                        User.CompanyName = Convert.ToString(execResult["CmpyName"]);
                    }
                    _userList.Add(User);
                }
                return _userList;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<MsgRetriever> RensendActivationEmail(ResendAccountMail _Resend)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Web);

            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[6];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_Resend.UserId) ? new SqlParameter("@Email", DBNull.Value) : new SqlParameter("@Email", _Resend.UserId);
                Parameters[2] = String.IsNullOrEmpty(_Resend.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _Resend.AcctNo);
                Parameters[3] = String.IsNullOrEmpty(_Resend.CorpCd) ? new SqlParameter("@CorpCd", DBNull.Value) : new SqlParameter("@CorpCd", _Resend.CorpCd);
                Parameters[4] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@CreatedBy", DBNull.Value) : new SqlParameter("@CreatedBy", this.GetUserId);
                Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[5].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebCheckUserMaint‏", CommandType.StoredProcedure, Parameters);
                var Result = ConvertInt(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
                ;
            }
        }

        public async Task<MsgRetriever> ResetPasswordCounter(string AcctNo, string UserId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Web);

            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[2];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@UserId", UserId);
                Parameters[1] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[1].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebResetUserCountSelect‏‏‏", CommandType.StoredProcedure, Parameters);
                var Result = ConvertInt(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        #endregion
        #region "Product Discount"


        public async Task<List<ProductDiscount>> WebProductDiscountListSelect(string AcctNo, string DiscType, string RefTo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@Refkey", DBNull.Value) : new SqlParameter("@Refkey", AcctNo);
                Parameters[1] = String.IsNullOrEmpty(DiscType) ? new SqlParameter("@ProdDiscType", DBNull.Value) : new SqlParameter("@ProdDiscType", DiscType);
                Parameters[2] = String.IsNullOrEmpty(RefTo) ? new SqlParameter("@Refto", DBNull.Value) : new SqlParameter("@Refto", RefTo);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebProductDiscountListSelect", CommandType.StoredProcedure, Parameters);
                var _ProductDiscount = new List<ProductDiscount>();
                while (execResult.Read())
                {
                    _ProductDiscount.Add(new ProductDiscount
                    {
                        Id = Convert.ToString(execResult["Id"]),
                        SelectedProdCd = Convert.ToString(execResult["ProdGroup"]),
                        SelectedProdDiscType = Convert.ToString(execResult["ProdDiscType"]),
                        ProdDiscDescp = Convert.ToString(execResult["ProdDiscDescp"]),
                        SelectedPlanId = Convert.ToString(execResult["PlanId"]),
                        EffDateFrom = DateConverter(execResult["EffDate"]),
                        CreatedBy = Convert.ToString(execResult["UserId"]),
                        CreationDate = DateConverter(execResult["CreationDate"]),
                        ProdCdDescp = Convert.ToString(execResult["ProductDescp"]),
                        Remarks = Convert.ToString(execResult["Remarks"]),
                        EffDateTo = DateConverter(execResult["EffEndDate"]),
                    });
                };
                return _ProductDiscount;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        //public async Task<ProductDiscount> WebProductDiscountSelect(string AcctNo, string DiscType, string Id, string RefTo)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
        //    try
        //    {
        //        objDataEngine.InitiateConnection();
        //        SqlParameter[] Parameters = new SqlParameter[4];
        //        Parameters[0] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@Refkey", DBNull.Value) : new SqlParameter("@Refkey", AcctNo);
        //        Parameters[1] = String.IsNullOrEmpty(DiscType) ? new SqlParameter("@ProdDiscType", DBNull.Value) : new SqlParameter("@ProdDiscType", DiscType);
        //        Parameters[2] = String.IsNullOrEmpty(Id) ? new SqlParameter("@Id", DBNull.Value) : new SqlParameter("@Id", ConvertLongToDb(Id));
        //        Parameters[3] = String.IsNullOrEmpty(RefTo) ? new SqlParameter("@Refto", DBNull.Value) : new SqlParameter("@Refto", RefTo);
        //        var execResult = await objDataEngine.ExecuteCommandAsync("WebProductDiscountSelect", CommandType.StoredProcedure, Parameters);
        //        var _ProductDiscount = new ProductDiscount();
        //        while (execResult.Read())
        //        {
        //            _ProductDiscount = new ProductDiscount
        //            {
        //                Id = Convert.ToString(execResult["Id"]),
        //                SelectedProdCd = Convert.ToString(execResult["ProdCd"]),
        //                EffDateFrom = DateConverter(execResult["EffDate"]),
        //                SelectedPlanId = Convert.ToString(execResult["PlanId"]),
        //                CreatedBy = Convert.ToString(execResult["UserId"]),
        //                CreationDate = DateConverter(execResult["CreationDate"]),
        //                Remarks = Convert.ToString(execResult["Remarks"]),
        //                SelectedProdDiscType = Convert.ToString(execResult["ProdDiscType"]),
        //                EffDateTo = DateConverter(execResult["EffEndDate"]),
        //                OnlineIndicator = BoolConverter(execResult["OnlineInd"])
        //                //  CompanyName = Convert.ToString(execResult["CmpyName"])
        //            };
        //        };
        //        return _ProductDiscount;
        //    }
        //    finally
        //    {
        //        objDataEngine.CloseConnection();
        //    }

        //}

        //public async Task<MsgRetriever> ProductDiscountMaint(ProductDiscount _Discount, string AcctNo, string Func, string RefTo)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
        //    try
        //    {
        //        await objDataEngine.InitiateConnectionAsync();
        //        SqlParameter[] Parameters = new SqlParameter[13];
        //        SqlCommand cmd = new SqlCommand();
        //        if (Func == "N")
        //        {
        //            Parameters[0] = new SqlParameter("@Id", DBNull.Value);
        //            Parameters[1] = new SqlParameter("@flag", "N");
        //        }
        //        else
        //        {
        //            Parameters[0] = String.IsNullOrEmpty(_Discount.Id) ? new SqlParameter("@Id", DBNull.Value) : new SqlParameter("@Id", ConvertIntToDb(_Discount.Id));
        //            Parameters[1] = new SqlParameter("@flag", "E");
        //        }

        //        Parameters[2] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@Refkey", DBNull.Value) : new SqlParameter("@Refkey", AcctNo);
        //        Parameters[3] = String.IsNullOrEmpty(RefTo) ? new SqlParameter("@Refto", DBNull.Value) : new SqlParameter("@Refto", RefTo);//new SqlParameter("@RefTo", "ACCT");
        //        Parameters[4] = String.IsNullOrEmpty(_Discount.SelectedProdDiscType) ? new SqlParameter("@ProdDiscType", DBNull.Value) : new SqlParameter("@ProdDiscType", _Discount.SelectedProdDiscType);
        //        Parameters[5] = String.IsNullOrEmpty(_Discount.SelectedProdCd) ? new SqlParameter("@ProdCd", DBNull.Value) : new SqlParameter("@ProdCd", _Discount.SelectedProdCd);
        //        Parameters[6] = new SqlParameter("@EffDate", ConvertDatetimeDB(_Discount.EffDateFrom));//String.IsNullOrEmpty(_Discount.EffDateFrom) ? new SqlParameter("@EffDate", DBNull.Value) : 

        //        Parameters[7] = String.IsNullOrEmpty(_Discount.SelectedPlanId) ? new SqlParameter("@PlanId", DBNull.Value) : new SqlParameter("@PlanId", _Discount.SelectedPlanId);
        //        Parameters[8] = String.IsNullOrEmpty(_Discount.Remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", _Discount.Remarks);
        //        Parameters[9] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
        //        Parameters[10] = new SqlParameter("@EffEndDate", ConvertDatetimeDB(_Discount.EffDateTo));//String.IsNullOrEmpty(_Discount.EffDateTo) ? new SqlParameter("@EffEndDate", DBNull.Value) : 
        //        Parameters[11] = new SqlParameter("@OnlineInd", ConvertBoolDB(_Discount.OnlineIndicator));
        //        Parameters[12] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
        //        Parameters[12].Direction = ParameterDirection.ReturnValue;

        //        var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebProductDiscountMaint", CommandType.StoredProcedure, Parameters);
        //        var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
        //        var Descp = await GetMessageCode(Result);
        //        return Descp;
        //    }
        //    finally
        //    {
        //        objDataEngine.CloseConnection();
        //    }
        //}
        public async Task<MsgRetriever> ProductDiscountDelete(ProductDiscount _Discount, string AcctNo, string RefTo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);

            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[9];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = String.IsNullOrEmpty(_Discount.Id) ? new SqlParameter("@Id", DBNull.Value) : new SqlParameter("@Id", ConvertLongToDb(_Discount.Id));
                Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@Refkey", DBNull.Value) : new SqlParameter("@Refkey", AcctNo);
                Parameters[2] = String.IsNullOrEmpty(_Discount.SelectedProdDiscType) ? new SqlParameter("@ProdDiscType", DBNull.Value) : new SqlParameter("@ProdDiscType", _Discount.SelectedProdDiscType);
                Parameters[3] = String.IsNullOrEmpty(_Discount.SelectedProdCd) ? new SqlParameter("@ProdCd", DBNull.Value) : new SqlParameter("@ProdCd", _Discount.SelectedProdCd);
                Parameters[4] = String.IsNullOrEmpty(_Discount.EffDateFrom) ? new SqlParameter("@EffDate", DBNull.Value) : new SqlParameter("@EffDate", ConvertDatetimeDB(_Discount.EffDateFrom));
                Parameters[5] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
                Parameters[6] = String.IsNullOrEmpty(RefTo) ? new SqlParameter("@Refto", DBNull.Value) : new SqlParameter("@Refto", RefTo);//new SqlParameter("@RefTo", "ACCT");
                Parameters[7] = String.IsNullOrEmpty(_Discount.EffDateTo) ? new SqlParameter("EffEndDate", DBNull.Value) : new SqlParameter("EffEndDate", _Discount.EffDateTo);
                Parameters[8] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[8].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebProductDiscountDelete", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }


        #endregion
        #region "Pukal"
        public async Task<MsgRetriever> WebPukalMaint(Pukal pukal, string RefKey)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[19];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@RefTo", "Acct");//"Acct"
                Parameters[2] = String.IsNullOrEmpty(RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", RefKey);
                Parameters[3] = String.IsNullOrEmpty(pukal.SelectedRefCd) ? new SqlParameter("@RefCd", DBNull.Value) : new SqlParameter("@RefCd", pukal.SelectedRefCd);
                //Parameters[4] = new SqlParameter("@TerminationDate", DateConverterDB(pukal.TerminationDate));//String.IsNullOrEmpty(pukal.TerminationDate) ? new SqlParameter("@TerminationDate", DBNull.Value) : 
                Parameters[4] = String.IsNullOrEmpty(pukal.SelectedAcctOfficeCode) ? new SqlParameter("@AcctOfficeCd", DBNull.Value) : new SqlParameter("@AcctOfficeCd", pukal.SelectedAcctOfficeCode);
                Parameters[5] = String.IsNullOrEmpty(pukal.WarrantDepartment) ? new SqlParameter("@WarrantDept", DBNull.Value) : new SqlParameter("@WarrantDept", pukal.WarrantDepartment);
                Parameters[6] = String.IsNullOrEmpty(pukal.ChargeDepartment) ? new SqlParameter("@ChargeDept", DBNull.Value) : new SqlParameter("@ChargeDept", pukal.ChargeDepartment);
                Parameters[7] = String.IsNullOrEmpty(pukal.WarrantPtj) ? new SqlParameter("@WarrantPTJ", DBNull.Value) : new SqlParameter("@WarrantPTJ", pukal.WarrantPtj);
                Parameters[8] = String.IsNullOrEmpty(pukal.ChargePtj) ? new SqlParameter("@ChargePTJ", DBNull.Value) : new SqlParameter("@ChargePTJ", pukal.ChargePtj);
                Parameters[9] = String.IsNullOrEmpty(pukal.VoteCode) ? new SqlParameter("@VoteCd", DBNull.Value) : new SqlParameter("@VoteCd", pukal.VoteCode);
                Parameters[10] = String.IsNullOrEmpty(pukal.SelectedAgObjectCode) ? new SqlParameter("@ObjCd", DBNull.Value) : new SqlParameter("@ObjCd", pukal.SelectedAgObjectCode);
                Parameters[11] = String.IsNullOrEmpty(pukal.SortKey) ? new SqlParameter("@SortKey", DBNull.Value) : new SqlParameter("@SortKey", pukal.SortKey);
                Parameters[12] = String.IsNullOrEmpty(pukal.ProgramCd) ? new SqlParameter("@ProgramCd", DBNull.Value) : new SqlParameter("@ProgramCd", pukal.ProgramCd);
                Parameters[13] = String.IsNullOrEmpty(pukal.ProjCd) ? new SqlParameter("@ProjCd", DBNull.Value) : new SqlParameter("@ProjCd", pukal.ProjCd);
                Parameters[14] = String.IsNullOrEmpty(GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", GetUserId);
                if (pukal.checkIndicator)
                    Parameters[15] = new SqlParameter("@CheckDigitInd","Y");
                else
                    Parameters[15] = new SqlParameter("@CheckDigitInd", "N");
                Parameters[16] = new SqlParameter("@ActivationDate", DateConverterDB(pukal.ActivationDate));
                Parameters[17] = new SqlParameter("@TerminationFlag", pukal.TerminationFlag);
                Parameters[18] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[18].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebPukalMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<Pukal> WebPukalSelect(int RefKey, String RefTo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@RefKey", RefKey);
                Parameters[2] = String.IsNullOrEmpty(RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", RefTo);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebPukalSelect", CommandType.StoredProcedure, Parameters);
                var _GetPukalDetail = new Pukal();

                while (execResult.Read())
                {
                    _GetPukalDetail.acctNo = ConvertInt(RefKey);
                    _GetPukalDetail.RefKey = Convert.ToString(execResult["Refkey"]);
                    _GetPukalDetail.SelectedRefCd = Convert.ToString(execResult["RefCd"]);
                    _GetPukalDetail.TerminationDate = DateConverter(execResult["TerminationDate"]); //Convert.ToString(execResult["TerminationDate"]);
                    _GetPukalDetail.TerminationFlag = Convert.ToString(execResult["TerminationFlag"]);
                    _GetPukalDetail.SelectedAcctOfficeCode = Convert.ToString(execResult["AcctOfficeCd"]);
                    _GetPukalDetail.WarrantDepartment = Convert.ToString(execResult["WarrantDept"]);
                    _GetPukalDetail.ChargeDepartment = Convert.ToString(execResult["ChargeDept"]);
                    _GetPukalDetail.WarrantPtj = Convert.ToString(execResult["WarrantPTJ"]);
                    _GetPukalDetail.ChargePtj = Convert.ToString(execResult["ChargePTJ"]);
                    _GetPukalDetail.VoteCode = Convert.ToString(execResult["VoteCD"]);
                    _GetPukalDetail.SortKey = Convert.ToString(execResult["SortKey"]);
                    _GetPukalDetail.ProgramCd = Convert.ToString(execResult["ProgramCd"]);
                    _GetPukalDetail.ProjCd = Convert.ToString(execResult["ProjCd"]);
                    _GetPukalDetail.SelectedAgObjectCode = Convert.ToString(execResult["ObjCd"]);
                    _GetPukalDetail.UserId = Convert.ToString(execResult["UserId"]);
                    _GetPukalDetail.creationDate = DateConverter(execResult["CreationDate"]);
                    _GetPukalDetail.createdBy = Convert.ToString(execResult["CreatedBy"]);
                    _GetPukalDetail.LastUpdated = DateConverter(execResult["LastUpdDate"]);
                    _GetPukalDetail.ActivationDate = DateConverter(execResult["ActivationDate"]);
                    _GetPukalDetail.CompanyName = Convert.ToString(execResult["CmpyName1"]);
                    _GetPukalDetail.CompanyId = Convert.ToString(execResult["Refkey"]);
                    _GetPukalDetail.checkIndicator = execResult["CheckDigitInd"].ToString().ToLower() == "y"?true : false;
                };
                if (_GetPukalDetail.acctNo == 0)
                {
                    _GetPukalDetail.TerminationFlag = "N";
                    _GetPukalDetail.checkIndicator = true;
                }

                return _GetPukalDetail;
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion "Pukal"
        #region "AcctMilestone"
        public async Task<List<Milestone>> WebAcctMilestoneListSelect(Milestone _milestone)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[4];
                Parameters[0] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
                Parameters[1] = String.IsNullOrEmpty(_milestone.workflowcd) ? new SqlParameter("@WorkflowCd", DBNull.Value) : new SqlParameter("@WorkflowCd", _milestone.workflowcd);
                Parameters[2] = String.IsNullOrEmpty(_milestone.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _milestone.AcctNo);
                Parameters[3] = new SqlParameter("@Ind", _milestone.Ind);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebAcctMilestoneListSelect", CommandType.StoredProcedure, Parameters);
                var getMilestone = new List<Milestone>();

                while (execResult.Read())
                {
                    var _ms = new Milestone();
                    _ms.AcctNo = Convert.ToString(execResult["AcctNo"]);
                    _ms.RefKey = Convert.ToInt64(execResult["RefKey"]);
                    _ms.SelectedTaskNo = Convert.ToString(execResult["TaskNo"]);
                    _ms.selectedPriority = Convert.ToString(execResult["Priority"]);
                    _ms.selectedReasonCd = Convert.ToString(execResult["ReasonCd"]);
                    _ms.Remarks = Convert.ToString(execResult["Remarks"]);
                    _ms.RecallDate = Convert.ToString(execResult["RecallDate"]);
                    _ms.selectedStatus = Convert.ToString(execResult["Sts"]);
                    _ms.Descp = Convert.ToString(execResult["StsDescp"]);
                    _ms.CreationDate = Convert.ToString(execResult["CreationDate"]);
                    _ms.LastUpdDate = Convert.ToString(execResult["LastUpdDate"]);
                    _ms.TaskDescp = Convert.ToString(execResult["TaskDescp"]);
                    //if (_milestone.workflowcd == "Adj" || _milestone.workflowcd == "Pymt")
                    //{
                    //    _ms.AcctNo = Convert.ToString(execResult["AcctNo"]);
                    //}
                    getMilestone.Add(_ms);
                };
                return getMilestone;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion
    }
}