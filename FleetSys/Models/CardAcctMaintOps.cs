using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using FleetOps.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using FleetOps.Models;
using FleetOps.ViewModel;
using ModelSector;
using CCMS.ModelSector;
using System.Threading.Tasks;
using FleetSys.Models;

namespace FleetOps.Models
{
    public class CardAcctMaintOps : BaseClass
    {
        int RcptNo;

        #region"General Info"

        public async Task<GeneralInfoModel> GetGeneralInfo(int issNo, string acctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", issNo);
            Parameters[1] = new SqlParameter("@AcctNo", Convert.ToInt64(acctNo));
            var Reader = await objDataEngine.ExecuteCommandAsync("WebAcctGeneralInfoSelect", CommandType.StoredProcedure, Parameters);

            while (Reader.Read())
            {

                var _generalInfo = new GeneralInfoModel
                {
                    AcctNo = Convert.ToString(Reader["AcctNo"]),
                    PlasticType =await WebGetPlasticType("I"),
                    SelectedPlasticType = Convert.ToString(Reader["PlasticType"]),
                    AccountName = Convert.ToString(Reader["AccountName"]),
                    CmpyRegsNo = Convert.ToString(Reader["CmpyRegsNo"]),
                    RegsDate = DateConverter(Reader["RegsDate"]),
                    SIC =await BaseClass.WebGetMerchType("S"),
                    SelectedSIC = Convert.ToString(Reader["SIC"]),
                    SapNo = Convert.ToString(Reader["CustomerNo"]),
                    CorpName =await BaseClass.WebGetCorpCd(),
                    BlockedDate = DateTimeConverter(Reader["BlockedDate"]),
                    SelectedCorpName = Convert.ToString(Reader["CorpCd"]),
                    ClientClass =await BaseClass.WebGetRefLib("TermsofPayment"),
                    SelectedClientClass = Convert.ToString(Reader["TermsofPayment"]),
                    CustomerGroup =await BaseClass.WebGetRefLib("ClientType"),
                    SelectedCustomerGroup = Convert.ToString(Reader["CustomerGroup"]),
                    BusnEstablishment =await BaseClass.WebGetRefLib("BusnEst"),
                    SelectedBusnEstablishment = Convert.ToString(Reader["BusnEstablishment"]),
                    SourceCode = Convert.ToString(Reader["SrcCd"]),
                    SourceRefNo = Convert.ToString(Reader["SrcRefNo"]),
                    CurrentStatus =await BaseClass.WebGetRefLib("AcctSts"),
                    SelectedCurrentStatus = Convert.ToString(Reader["Sts"]),
                    CreationDate = BaseClass.DateConverter(Reader["CreationDate"]),
                    TerminatedDate = BaseClass.DateConverter(Reader["TerminatedDate"]),
                    ReasonCode =await BaseClass.WebGetRefLib("ReasonCd"),
                    SelectedReasonCode = Convert.ToString(Reader["ReasonCd"]),
                    OvrStatusTaggedByUserId = Convert.ToString(Reader["OverrideStsUserId"]),
                    OvrStatus =await BaseClass.WebGetRefLib("OverrideSts"),
                    SelectedOvrStatus = Convert.ToString(Reader["OverrideSts"]),
                    OvrExpDate = BaseClass.DateConverter(Reader["OverrideStsExpiry"]),
                    ApplId = Convert.ToString(Reader["ApplId"]),
                    ApplRef = Convert.ToString(Reader["ApplRef"]),
                    ApplCreationDate = BaseClass.DateConverter(Reader["CaptDate"]),
                    Remarks = Convert.ToString(Reader["Remarks"]),
                    WebUserId = Convert.ToString(Reader["WebUserId"]),
                    LoyaltyCardNo = Convert.ToString(Reader["LoyaltyCardNo"]),
                    EntityId = Convert.ToString(Reader["EntityId"]),
                    AuditId = Convert.ToString(Reader["AuditId"]),
                    WebPassword = Convert.ToString(Reader["WebPassword"]),
                    CustSvcId = Convert.ToString(Reader["ReconAcct"]),
                    BusnCategory =await BaseClass.WebGetRefLib("BusnCategory"),
                    SelectedBusnCategory = Convert.ToString("Industry"),
                    TaxId = Convert.ToString(Reader["TaxId"]),
                    SaleTerritory =await BaseClass.WebGetRefLib("SaleTerritory"),
                    SelectedSaleTerritory = Convert.ToString(Reader["SalesGroup"]),
                    SelectedAccountType = Convert.ToString(Reader["AccountType"]),
                    AccountType =await BaseClass.WebGetRefLib("BankAcctType"),
                    CompanyType =await BaseClass.WebGetRefLib("CmpyType"),
                    SelectedCompanyType = Convert.ToString(Reader["CmpyType"]),
                    PaymentTerms = Convert.ToString(Reader["PymtTerms"])
                };

                objDataEngine.CloseConnection();
                return _generalInfo;

            }
            return new GeneralInfoModel();
        }
        public async Task<MsgRetriever> SaveGeneralInfoResult(GeneralInfoModel _generalInfo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[36];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_generalInfo.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _generalInfo.AcctNo);
            Parameters[2] = String.IsNullOrEmpty(_generalInfo.CmpyRegsNo) ? new SqlParameter("@CmpyRegsNo", DBNull.Value) : new SqlParameter("@CmpyRegsNo", _generalInfo.CmpyRegsNo);
            Parameters[3] = new SqlParameter("@RegsDate", ConvertDatetimeDB(_generalInfo.RegsDate));
            Parameters[4] = String.IsNullOrEmpty(_generalInfo.AccountName) ? new SqlParameter("@CmpyName1", DBNull.Value) : new SqlParameter("@CmpyName1", _generalInfo.AccountName);
            Parameters[5] = String.IsNullOrEmpty(_generalInfo.SelectedSIC) ? new SqlParameter("@Sic", DBNull.Value) : new SqlParameter("@Sic", _generalInfo.SelectedSIC);
            Parameters[6] = String.IsNullOrEmpty(_generalInfo.SapNo) ? new SqlParameter("@SAPNo", DBNull.Value) : new SqlParameter("@SAPNo", _generalInfo.SapNo);
            Parameters[7] = String.IsNullOrEmpty(_generalInfo.SelectedCorpName) ? new SqlParameter("@CorpCd", DBNull.Value) : new SqlParameter("@CorpCd", _generalInfo.SelectedCorpName);
            Parameters[8] = String.IsNullOrEmpty(_generalInfo.SelectedClientClass) ? new SqlParameter("@ClientClass", DBNull.Value) : new SqlParameter("@ClientClass", _generalInfo.SelectedClientClass);
            Parameters[9] = String.IsNullOrEmpty(_generalInfo.SelectedCustomerGroup) ? new SqlParameter("@ClientType", DBNull.Value) : new SqlParameter("@ClientType", _generalInfo.SelectedCustomerGroup);
            Parameters[10] = String.IsNullOrEmpty(_generalInfo.SelectedBusnEstablishment) ? new SqlParameter("@BusnEstablishment", DBNull.Value) : new SqlParameter("@BusnEstablishment", _generalInfo.SelectedBusnEstablishment);
            Parameters[11] = String.IsNullOrEmpty(_generalInfo.SourceCode) ? new SqlParameter("@SrcCd", DBNull.Value) : new SqlParameter("@SrcCd", _generalInfo.SourceCode);
            Parameters[12] = String.IsNullOrEmpty(_generalInfo.SourceRefNo) ? new SqlParameter("@SrcRefNo", DBNull.Value) : new SqlParameter("@SrcRefNo", _generalInfo.SourceRefNo);
            Parameters[13] = String.IsNullOrEmpty(_generalInfo.SelectedCurrentStatus) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _generalInfo.SelectedCurrentStatus);
            Parameters[14] = new SqlParameter("@CreationDate", BaseClass.DateConverterDB(_generalInfo.CreationDate));
            Parameters[15] = new SqlParameter("@TerminatedDate", BaseClass.DateConverterDB(_generalInfo.TerminatedDate));
            Parameters[16] = String.IsNullOrEmpty(_generalInfo.SelectedReasonCode) ? new SqlParameter("@ReasonCd", DBNull.Value) : new SqlParameter("@ReasonCd", _generalInfo.SelectedReasonCode);
            Parameters[17] = String.IsNullOrEmpty(_generalInfo.OvrStatusTaggedByUserId) ? new SqlParameter("@OverrideStsUserId", DBNull.Value) : new SqlParameter("@OverrideStsUserId", _generalInfo.OvrStatusTaggedByUserId);
            Parameters[18] = String.IsNullOrEmpty(_generalInfo.SelectedOvrStatus) ? new SqlParameter("@OverrideSts", DBNull.Value) : new SqlParameter("@OverrideSts", _generalInfo.SelectedOvrStatus);
            Parameters[19] = new SqlParameter("@OverrideStsExp", BaseClass.DateConverterDB(_generalInfo.OvrExpDate));
            Parameters[20] = String.IsNullOrEmpty(_generalInfo.ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", _generalInfo.ApplId);
            Parameters[21] = String.IsNullOrEmpty(_generalInfo.ApplRef) ? new SqlParameter("@ApplRefNo", DBNull.Value) : new SqlParameter("@ApplRefNo", _generalInfo.ApplRef);
            Parameters[22] = new SqlParameter("@CaptDate", BaseClass.DateConverterDB(_generalInfo.ApplCreationDate));
            Parameters[23] = String.IsNullOrEmpty(_generalInfo.Remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", _generalInfo.Remarks);
            Parameters[24] = String.IsNullOrEmpty(_generalInfo.WebUserId) ? new SqlParameter("@WebUserId", DBNull.Value) : new SqlParameter("@WebUserId", _generalInfo.WebUserId);
            Parameters[25] = String.IsNullOrEmpty(_generalInfo.LoyaltyCardNo) ? new SqlParameter("@LoyaltyCardNo", DBNull.Value) : new SqlParameter("@LoyaltyCardNo", _generalInfo.LoyaltyCardNo);
            Parameters[26] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);

            Parameters[27] = String.IsNullOrEmpty(_generalInfo.WebPassword) ? new SqlParameter("@WebPw", DBNull.Value) : new SqlParameter("@WebPw", _generalInfo.WebPassword);
            Parameters[28] = String.IsNullOrEmpty(_generalInfo.CustSvcId) ? new SqlParameter("@ReconAcct", DBNull.Value) : new SqlParameter("@ReconAcct", _generalInfo.CustSvcId);
            Parameters[29] = String.IsNullOrEmpty(_generalInfo.SelectedBusnCategory) ? new SqlParameter("@BusnCategory", DBNull.Value) : new SqlParameter("@BusnCategory", _generalInfo.SelectedBusnCategory);
            Parameters[30] = String.IsNullOrEmpty(_generalInfo.SelectedSaleTerritory) ? new SqlParameter("@SaleTerritory", DBNull.Value) : new SqlParameter("@SaleTerritory", _generalInfo.SelectedSaleTerritory);
            Parameters[31] = String.IsNullOrEmpty(_generalInfo.SelectedAccountType) ? new SqlParameter("@AcctType", DBNull.Value) : new SqlParameter("@AcctType", _generalInfo.SelectedAccountType);
            Parameters[32] = String.IsNullOrEmpty(_generalInfo.CutOff) ? new SqlParameter("@CutOff", DBNull.Value) : new SqlParameter("@CutOff", _generalInfo.CutOff);
            Parameters[35] = String.IsNullOrEmpty(_generalInfo.TaxId) ? new SqlParameter("@TaxId", DBNull.Value) : new SqlParameter("@TaxId", _generalInfo.TaxId);

            Parameters[33] = String.IsNullOrEmpty(_generalInfo.PaymentTerms) ? new SqlParameter("@PymtTerms", DBNull.Value) : new SqlParameter("@PymtTerms", _generalInfo.PaymentTerms);



            Parameters[34] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[34].Direction = ParameterDirection.ReturnValue;


            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebAcctGeneralInfoMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;

        }
        public static IEnumerable<SelectListItem> CustomDate()
        {
            string currentDateTime;
            var SelectDatelist = new List<SelectListItem>();

            for (int i = 1; i <= 7; i++)
            {
                currentDateTime = System.DateTime.Now.AddDays(i).ToShortDateString();
                SelectDatelist.Add(new SelectListItem
                {
                    Text = currentDateTime,
                    Value = currentDateTime
                });

            };
            return SelectDatelist;

        }


        public async Task<MsgRetriever> MailMerge(string AcctNo)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[4];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = new SqlParameter("@AcctNo", AcctNo);
            Parameters[2] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[3] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[3].Direction = ParameterDirection.ReturnValue;

            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("RptMailMergeMaint‏‏", CommandType.StoredProcedure, Parameters);
            var Result = ConvertInt(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return Descp;
        }


        #endregion

        #region"Financial Info"
        public async Task<FinancialInfoModel> GetFinancialInfo(int issNo, int acctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", issNo);
            Parameters[1] = new SqlParameter("@AcctNo", acctNo);
            var Reader =await objDataEngine.ExecuteCommandAsync("WebAcctFinInfoSelect", CommandType.StoredProcedure, Parameters);
            while (Reader.Read())
            {
                var _financialInfo = new FinancialInfoModel
                {
                    AcctNo = Convert.ToString(Reader["AcctNo"]),
                    TaxId = Convert.ToString(Reader["TaxId"]),
                    LatePaymtCharge = BaseClass.BoolConverter(Reader["StopLPC"]),
                    DunningCd =await BaseClass.WebGetRefLib("Dunning"),
                    SelectedDunningCd = Convert.ToString(Reader["DunCd"]),
                    CredtAllowanceFact = BaseClass.ConverterDecimal(Reader["AllowanceFactor"]),
                    AccrdInterest = BaseClass.ConverterDecimal(Reader["AccruedInterestAmt"]),
                    AccrdCrdtUsg = BaseClass.ConverterDecimal(Reader["AccruedCreditUsageAmt"]),
                    PromPaymtRebate = BaseClass.ConverterDecimal(Reader["PromptPaymtRebate"]),
                    PPRGracePeriod = ConvertInt(Reader["PromptPaymtRebateTerms"]),
                    pprExpiry =DateConverter(Reader["PromptPaymtRebateExpiry"]),
                    LitreLimitPerTxn = BaseClass.ConverterDecimal(Reader["LitLimitPerTxn"]),
                    AmtLimitPerTxn = BaseClass.ConverterDecimal(Reader["AmtLimitPerTxn"]),
                    CycNo =await BaseClass.WebGetCycle("I"),
                    SelectedCycNo = Convert.ToString(Reader["CycNo"]),
                    StmtType =await BaseClass.WebGetRefLib("BillingType"),
                    SelectedStmtType = Convert.ToString(Reader["StmtType"]),
                    StmtInd =await BaseClass.WebGetRefLib("StmtInd"),
                    SelectedStmtInd = Convert.ToString(Reader["StmtInd"]),
                    StmtDate = DateConverter(Reader["StmtDate"]),
                    PaymtMethod =await BaseClass.WebGetRefLib("PaymtMethod"),
                    SelectedPaymtMethod = Convert.ToString(Reader["PaymtMethod"]),
                    PaymtTerm = ConvertInt(Reader["PymtTerms"]),
                    GracePeriod = ConvertInt(Reader["GracePeriod"]),
                    DirectDebitInd = BaseClass.BoolConverter(Reader["DirectDebitInd"]),
                    BankAcctType =await BaseClass.WebGetRefLib("BankAcctType"),
                    SelectedBankAcctType = Convert.ToString(Reader["BankAcctType"]),
                    BankName =await BaseClass.WebGetRefLib("Bank"),
                    selectedBankName = Convert.ToString(Reader["BankName"]),
                    BankAcctNo = Convert.ToString(Reader["BankAcctNo"]),
                    BankBranchCD = Convert.ToString(Reader["BankBranchCd"]),
                    PayeeCd = Convert.ToString(Reader["PayeeCd"]),
                    //WebAcctFinInfoMaint‏
                    //  VATRate = BaseClass.ConverterDecimal(Reader["VATRate"]),
                    TaxCategory =await BaseClass.WebGetRefLib("TaxCategory"),
                    SelectedTaxCategory = Convert.ToString(Reader["TaxCategory"]),
                    WriteoffDate =DateConverter(Reader["WriteOffDate"]),
                    LastPaymtType = Convert.ToString(Reader["LastPaymtRecvType"]),
                    LastPaymtReceived = BaseClass.ConverterDecimal(Reader["LastPaymtRecvAmt"]),
                    LastPaymtDate = DateConverter(Reader["LastPaymtDate"]),
                    InvoiceBillingIndicator = BaseClass.BoolConverter(Reader["InvBillInd"]),
                    PayAdviceBillingIndicator = BaseClass.BoolConverter(Reader["PymtInd"]),
                    // InvoiceIndCopy = BaseClass.BoolConverter(Reader["InvoiceCopyInd"]),
                    VehiclePerformanceReportInd = BaseClass.BoolConverter(Reader["VehPerfRptInd"]),
                    AssessmentType =await BaseClass.WebGetRefLib("AssessmentType"),
                    SelectedAssessmentType = Convert.ToString(Reader["SecuredCreditLine"]),
                    RiskCategory =await BaseClass.WebGetRefLib("RiskCategory"),
                    SelectedRiskCategory = Convert.ToString(Reader["RiskCategory"]),
                    CreditLimit = ConverterDecimal(Reader["CreditLimit"]),
                    WithholdingTaxInd = BaseClass.BoolConverter(Reader["Ewt"]),

                    _skds = new SKDS
                    {
                        SKDSNo = Convert.ToString(Reader["SKDSNo"]),
                        SKDSLitreQuota = BaseClass.ConverterDecimal(Reader["SKDSQuota"]),
                        EffFromDate =DateConverter(Reader["SKDSFromDate"]),
                        EffToDate =DateConverter(Reader["SKDSToDate"]),
                        SKDSRate = BaseClass.ConverterDecimal(Reader["SKDSRate"]),
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

                objDataEngine.CloseConnection();
                return _financialInfo;
            }
            return new FinancialInfoModel();
        }
        public async Task<MsgRetriever> SaveFinancialInfoResult(FinancialInfoModel _financialInfo, string AccNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[40];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AccNo);
            Parameters[2] = String.IsNullOrEmpty(_financialInfo.TaxId) ? new SqlParameter("@TaxId", DBNull.Value) : new SqlParameter("@TaxId", _financialInfo.TaxId);
            Parameters[3] = new SqlParameter("@LatePaymtInd", ConvertBoolDB(_financialInfo.LatePaymtCharge));
            Parameters[4] = new SqlParameter("@DunCd", ConvertIntToDb(_financialInfo.SelectedDunningCd));
            Parameters[5] = new SqlParameter("@AllowanceFactor", ConvertDecimalToDb(_financialInfo.CredtAllowanceFact));
            Parameters[6] = new SqlParameter("@AccruedInterestAmt", ConvertDecimalToDb(_financialInfo.AccrdInterest));
            Parameters[7] = new SqlParameter("@AccruedCreditUsageAmt", ConvertDecimalToDb(_financialInfo.AccrdCrdtUsg));
            Parameters[8] = new SqlParameter("@PromptPaymtRebate", ConvertDecimalToDb(_financialInfo.PromPaymtRebate));
            Parameters[9] = new SqlParameter("@PromptPaymtExp", DateConverterDB(_financialInfo.pprExpiry));
            Parameters[10] = new SqlParameter("@PromptPaymtRebateTerms", ConvertIntToDb(_financialInfo.PPRGracePeriod));
            Parameters[11] = new SqlParameter("@LitLimitPerTxn", ConvertDecimalToDb(_financialInfo.LitreLimitPerTxn));
            Parameters[12] = new SqlParameter("@AmtLimitPerTxn", ConvertDecimalToDb(_financialInfo.AmtLimitPerTxn));
            Parameters[13] = new SqlParameter("@CycNo", ConvertIntToDb(_financialInfo.SelectedCycNo));
            Parameters[14] = String.IsNullOrEmpty(_financialInfo.SelectedStmtType) ? new SqlParameter("@StmtType", DBNull.Value) : new SqlParameter("@StmtType", _financialInfo.SelectedStmtType);
            Parameters[15] = String.IsNullOrEmpty(_financialInfo.SelectedStmtInd) ? new SqlParameter("@StmtInd", DBNull.Value) : new SqlParameter("@StmtInd", _financialInfo.SelectedStmtInd);
            Parameters[16] = new SqlParameter("@StmtDate", DateConverterDB(_financialInfo.StmtDate));
            Parameters[17] = String.IsNullOrEmpty(_financialInfo.SelectedPaymtMethod) ? new SqlParameter("@PaymtMethod", DBNull.Value) : new SqlParameter("@PaymtMethod", _financialInfo.SelectedPaymtMethod);
            Parameters[18] = new SqlParameter("@PaymtTerms", ConvertIntToDb(_financialInfo.PaymtTerm));
            Parameters[19] = new SqlParameter("@GracePeriod", ConvertIntToDb(_financialInfo.GracePeriod));
            Parameters[20] = new SqlParameter("@DirectDebitInd", ConvertBoolDB(_financialInfo.DirectDebitInd));
            Parameters[21] = String.IsNullOrEmpty(_financialInfo.SelectedBankAcctType) ? new SqlParameter("@BankAcctType", DBNull.Value) : new SqlParameter("@BankAcctType", _financialInfo.SelectedBankAcctType);
            Parameters[22] = String.IsNullOrEmpty(_financialInfo.selectedBankName) ? new SqlParameter("@BankName", DBNull.Value) : new SqlParameter("@BankName", _financialInfo.selectedBankName);
            Parameters[23] = String.IsNullOrEmpty(_financialInfo.BankAcctNo) ? new SqlParameter("@BankAcctNo", DBNull.Value) : new SqlParameter("@BankAcctNo", _financialInfo.BankAcctNo);
            Parameters[24] = String.IsNullOrEmpty(_financialInfo.BankBranchCD) ? new SqlParameter("@BankBranchCd", DBNull.Value) : new SqlParameter("@BankBranchCd", _financialInfo.BankBranchCD);
            Parameters[25] = String.IsNullOrEmpty(_financialInfo.SelectedTaxCategory) ? new SqlParameter("@TaxCategory", DBNull.Value) : new SqlParameter("@TaxCategory", _financialInfo.SelectedTaxCategory);
            Parameters[26] = new SqlParameter("@WriteOffDate", BaseClass.DateConverterDB(_financialInfo.WriteoffDate));
            Parameters[27] = String.IsNullOrEmpty(_financialInfo.LastPaymtType) ? new SqlParameter("@LastPaymtRecvType", DBNull.Value) : new SqlParameter("@LastPaymtRecvType", _financialInfo.LastPaymtType);
            Parameters[28] = new SqlParameter("@LastPaymtRecvAmt", ConvertDecimalToDb(_financialInfo.LastPaymtReceived));
            Parameters[29] = new SqlParameter("@LastPaymtRecvDate", BaseClass.DateConverterDB(_financialInfo.LastPaymtDate));

            Parameters[30] = new SqlParameter("@InvBillInd", ConvertBoolDB(_financialInfo.InvoiceBillingIndicator));
            Parameters[31] = new SqlParameter("@PymtInd ", ConvertBoolDB(_financialInfo.PayAdviceBillingIndicator));

            Parameters[32] = new SqlParameter("@VehPerfRptInd ", ConvertBoolDB(_financialInfo.VehiclePerformanceReportInd));

            Parameters[33] = String.IsNullOrEmpty(_financialInfo.SelectedRiskCategory) ? new SqlParameter("@RiskCategory", DBNull.Value) : new SqlParameter("@RiskCategory", _financialInfo.SelectedRiskCategory);
            Parameters[34] = String.IsNullOrEmpty(_financialInfo.SelectedAssessmentType) ? new SqlParameter("@AssessmentType", DBNull.Value) : new SqlParameter("@AssessmentType", _financialInfo.SelectedAssessmentType);
            Parameters[35] = new SqlParameter("@CreditLimit", ConvertDecimalToDb(_financialInfo.CreditLimit));
            Parameters[36] = String.IsNullOrEmpty(_financialInfo.PayeeCd) ? new SqlParameter("@PayeeCd", DBNull.Value) : new SqlParameter("@PayeeCd", _financialInfo.PayeeCd);

            Parameters[37] = new SqlParameter("@Ewt", ConvertBoolDB(_financialInfo.WithholdingTaxInd));
            Parameters[38] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[39] = new SqlParameter("@RETURN_VALUE", SqlDbType.TinyInt);
            Parameters[39].Direction = ParameterDirection.ReturnValue;

            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebAcctFinInfoMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;

        }


        #endregion


        #region "UpToDateBal"
        public async Task<UpToDateBal> GetUpToBal(string acctNo)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            var parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            parameters[1] = new SqlParameter("@AcctNo", acctNo);
            var reader =await objDataEngine.ExecuteCommandAsync("WebAcctUTDBalanceSelect", CommandType.StoredProcedure, parameters);
            while (reader.Read())
            {
                var uptodatebal = new UpToDateBal
                {
                    AcctType =await BaseClass.WebGetPlasticType("I"),
                    SelectedAccountType = Convert.ToString(reader["Account Type"]),
                    CreditLimit = BaseClass.ConverterDecimal(reader["CreditLimit"]),
                    OpeningBal = BaseClass.ConverterDecimal(reader["Opening Balance"]),
                    InstantAmt = BaseClass.ConverterDecimal(reader["Instant Amount"]),
                    UnpostedAmt = BaseClass.ConverterDecimal(reader["Unposted Amount"]),
                    ClosingBal = BaseClass.ConverterDecimal(reader["Closing Balance"]),
                };

                objDataEngine.CloseConnection();
                return uptodatebal;

            }



            return new UpToDateBal();
        }
        #endregion

        #region "VelocityLimits"

        public async Task<List<VeloctyLimitListMaintModel>> GetCustAcctVelocityList(VeloctyLimitListMaintModel custAcctVelocity)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            var parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            parameters[1] = String.IsNullOrEmpty(custAcctVelocity._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", custAcctVelocity._CardnAccNo.AccNo);
            parameters[2] = String.IsNullOrEmpty(custAcctVelocity._CardnAccNo.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", custAcctVelocity._CardnAccNo.CardNo);
            parameters[3] = String.IsNullOrEmpty(custAcctVelocity.ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", custAcctVelocity.ApplId);
            parameters[4] = String.IsNullOrEmpty(custAcctVelocity.AppcId) ? new SqlParameter("@AppcId", DBNull.Value) : new SqlParameter("@AppcId", custAcctVelocity.AppcId);

            var execResult =await objDataEngine.ExecuteCommandAsync("WebVelocityLimitListSelect", CommandType.StoredProcedure, parameters);

            var veloctyLimitListMaint = new List<VeloctyLimitListMaintModel>();



            while (execResult.Read())
            {
                veloctyLimitListMaint.Add(new VeloctyLimitListMaintModel
                {
                    VelocityIndDescp = Convert.ToString(execResult["Velocity Indicator"]),
                    ProdCdDescp = Convert.ToString(execResult["Product Code"]),
                    velocityCounter = Convert.ToString(execResult["Counter"]),
                    ddlVelocityLimit = ConverterDecimal(execResult["Velocity Amount"]),
                    ddlVelocityLitre = ConverterDecimal(execResult["Velocity Litre"]),
                    SpentCnt = Convert.ToInt32(execResult["Spent Counter"]),
                    SpentLimit = ConverterDecimal(execResult["Spent Amount"]),
                    SpentLitre = ConverterDecimal(execResult["Spent Litre"]),
                    LastUpdateDate = DateConverter(execResult["Last Update Date"]),
                    UserId = Convert.ToString(execResult["User Id"]),
                    CreationDate = BaseClass.DateConverter(execResult["Creation Date"]),
                    SelectedVelocityInd = Convert.ToString(execResult["VelocityInd"]),
                    SelectedProdCd = Convert.ToString(execResult["Product"]),
                    //  CostCentre = Convert.ToString(execResult["CostCentre"])
                });
            };
            objDataEngine.CloseConnection();
            return veloctyLimitListMaint;
        }


        public async Task<VeloctyLimitListMaintModel> GetCustAcctVelocityDetail(VeloctyLimitListMaintModel _VelocityDetail)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[7];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_VelocityDetail._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _VelocityDetail._CardnAccNo.AccNo);
            Parameters[2] = String.IsNullOrEmpty(_VelocityDetail._CardnAccNo.CardNo) ? new SqlParameter("@CardNo ", DBNull.Value) : new SqlParameter("@CardNo ", _VelocityDetail._CardnAccNo.CardNo);
            Parameters[3] = String.IsNullOrEmpty(_VelocityDetail.ApplId) ? new SqlParameter("@ApplId ", DBNull.Value) : new SqlParameter("@ApplId ", _VelocityDetail.ApplId);
            Parameters[4] = String.IsNullOrEmpty(_VelocityDetail.AppcId) ? new SqlParameter("@AppcId ", DBNull.Value) : new SqlParameter("@AppcId ", _VelocityDetail.AppcId);
            Parameters[5] = String.IsNullOrEmpty(_VelocityDetail.SelectedVelocityInd) ? new SqlParameter("@VelocityInd ", DBNull.Value) : new SqlParameter("@VelocityInd ", _VelocityDetail.SelectedVelocityInd);
            Parameters[6] = String.IsNullOrEmpty(_VelocityDetail.SelectedProdCd) ? new SqlParameter("@ProdCd ", DBNull.Value) : new SqlParameter("@ProdCd ", _VelocityDetail.SelectedProdCd);

            var execResult =await objDataEngine.ExecuteCommandAsync("WebVelocityLimitSelect", CommandType.StoredProcedure, Parameters);

            var _GetVelDetail = new VeloctyLimitListMaintModel();

            while (execResult.Read())
            {

                _GetVelDetail.SelectedVelocityInd = Convert.ToString(execResult["VelocityInd"]);
                _GetVelDetail.SelectedProdCd = Convert.ToString(execResult["Product"]);
                _GetVelDetail.CntrLimit = Convert.ToInt32(execResult["Counter"]);
                _GetVelDetail.ddlVelocityLimit = ConverterDecimal(execResult["Velocity Amount"]);
                _GetVelDetail.ddlVelocityLitre = ConverterDecimal(execResult["Velocity Litre"]);
                _GetVelDetail.LastUpdateDate = Convert.ToString(execResult["LastUpdDate"]);
                _GetVelDetail.UserId = Convert.ToString(execResult["UserId"]);
                _GetVelDetail.CreationDate = Convert.ToString(execResult["CreationDate"]);
                //_GetVelDetail.CostCentre = Convert.ToString(execResult["CostCentre"]);
            };
            objDataEngine.CloseConnection();
            return _GetVelDetail;

        }
        public async Task<MsgRetriever> SaveCustAcctVelocity(VeloctyLimitListMaintModel _VelocityLimitList, string Func)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[14];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(Func) ? new SqlParameter("@Func", DBNull.Value) : new SqlParameter("@Func", Func);
            Parameters[2] = String.IsNullOrEmpty(_VelocityLimitList._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _VelocityLimitList._CardnAccNo.AccNo);
            Parameters[3] = String.IsNullOrEmpty(_VelocityLimitList._CardnAccNo.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _VelocityLimitList._CardnAccNo.CardNo);
            Parameters[4] = String.IsNullOrEmpty(_VelocityLimitList.CostCentre) ? new SqlParameter("@CostCentre", DBNull.Value) : new SqlParameter("@CostCentre", _VelocityLimitList.CostCentre);
            Parameters[5] = String.IsNullOrEmpty(_VelocityLimitList.ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", _VelocityLimitList.ApplId);
            Parameters[6] = String.IsNullOrEmpty(_VelocityLimitList.AppcId) ? new SqlParameter("@AppcId", DBNull.Value) : new SqlParameter("@AppcId", _VelocityLimitList.AppcId);
            Parameters[7] = String.IsNullOrEmpty(_VelocityLimitList.SelectedVelocityInd) ? new SqlParameter("@VelocityInd", DBNull.Value) : new SqlParameter("@VelocityInd", _VelocityLimitList.SelectedVelocityInd);
            Parameters[8] = String.IsNullOrEmpty(_VelocityLimitList.SelectedProdCd) ? new SqlParameter("@ProdCd", DBNull.Value) : new SqlParameter("@ProdCd", _VelocityLimitList.SelectedProdCd);
            Parameters[9] = new SqlParameter("@VelocityLimit", ConvertDecimalToDb(_VelocityLimitList.VelocityLimit));
            Parameters[10] = new SqlParameter("@VelocityCnt", ConvertIntToDb(_VelocityLimitList.CntrLimit));
            Parameters[11] = new SqlParameter("@VelocityLitre", ConvertDecimalToDb(_VelocityLimitList.VelocityLitre));
            Parameters[12] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@userId", DBNull.Value) : new SqlParameter("@userId", this.GetUserId);
            Parameters[13] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[13].Direction = ParameterDirection.ReturnValue;

            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebVelocityLimitMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;
        }
        public async Task<MsgRetriever> DelCustAcctVelocity(string AcctNo, string CardNo, string ApplId, string AppcId, string VelocityInd, string ProdCd)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[8];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
            Parameters[2] = String.IsNullOrEmpty(CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", CardNo);
            Parameters[3] = String.IsNullOrEmpty(ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", ApplId);
            Parameters[4] = String.IsNullOrEmpty(AppcId) ? new SqlParameter("@AppcId", DBNull.Value) : new SqlParameter("@AppcId", AppcId);
            Parameters[5] = String.IsNullOrEmpty(VelocityInd) ? new SqlParameter("@VelocityInd", DBNull.Value) : new SqlParameter("@VelocityInd", VelocityInd);
            Parameters[6] = String.IsNullOrEmpty(ProdCd) ? new SqlParameter("@ProdCd", DBNull.Value) : new SqlParameter("@ProdCd", ProdCd);
            //    Parameters[7] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
            Parameters[7] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[7].Direction = ParameterDirection.ReturnValue;

            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebVelocityLimitDelete", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;
        }

        #endregion

        #region "CardHolder"

        public async Task<List<SearchResult1>> GetBusinessLocations(string merchantId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(merchantId) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", merchantId);


            var execResult =await objDataEngine.ExecuteCommandAsync("WebLocationAcceptanceBySearch", CommandType.StoredProcedure, Parameters);
            var SearchResults = new List<SearchResult1>();

            while (execResult.Read())
            {
                SearchResults.Add(new SearchResult1
                {
                    Descp = Convert.ToString(execResult["Descp"]),
                    Object = Convert.ToString(execResult["Dealer"]),
                });
            }
            objDataEngine.CloseConnection();
            return SearchResults;
        }

        #region PersonInfo
        public async Task<PersonInfoModel> GetPersonInfo(int issNo, string entityId)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", issNo);
            Parameters[1] = String.IsNullOrEmpty(entityId) ? new SqlParameter("@EntityId", DBNull.Value) : new SqlParameter("@EntityId", entityId);
            var Reader =await objDataEngine.ExecuteCommandAsync("WebEntitySelect", CommandType.StoredProcedure, Parameters);

            while (Reader.Read())
            {

                var _PersonInfo = new PersonInfoModel
                {
                    title =await BaseClass.WebGetRefLib("Title"),
                    SelectedTitle = Convert.ToString(Reader["Title"]),
                    FirstName = Convert.ToString(Reader["First Name"]),
                    LastName = Convert.ToString(Reader["Last Name"]),
                    IdNo = Convert.ToString(Reader["NewIc"]),
                    IdType =await BaseClass.WebGetRefLib("IcType"),
                    SelectedIdType = Convert.ToString(Reader["NewIcType"]),
                    AltIdNo = Convert.ToString(Reader["AlternateIc"]),
                    AltIdType =await BaseClass.WebGetRefLib("IcType"),
                    SelectedAltIdType = Convert.ToString(Reader["AlternateIcType"]),
                    gender =await BaseClass.WebGetRefLib("Gender"),
                    Selectedgender = Convert.ToString(Reader["Gender"]),
                    DOB = Convert.ToString(Reader["DOB"]),
                    AnnualIncome = BaseClass.ConverterDecimal(Reader["Income"]),
                    Occupation =await BaseClass.WebGetRefLib("Occupation"),
                    SelectedOccupation = Convert.ToString(Reader["Occupation"]),
                    DeptId =await BaseClass.WebGetRefLib("Dept"),
                    SelectedDeptId = Convert.ToString(Reader["DeptId"]),
                    DrivingLicense = Convert.ToString(Reader["DrivingLic"])

                };

                objDataEngine.CloseConnection();
                return _PersonInfo;

            }

            return new PersonInfoModel()
            {
                title =await BaseClass.WebGetRefLib("Title"),
                IdType =await BaseClass.WebGetRefLib("IcType"),
                AltIdType =await BaseClass.WebGetRefLib("IcType"),
                gender =await BaseClass.WebGetRefLib("Gender"),
                Occupation =await BaseClass.WebGetRefLib("Occupation"),
                DeptId =await BaseClass.WebGetRefLib("Dept")
            };


        }
        public async Task<MsgRetriever> SavePersonInfo(PersonInfoModel _personInfo, string entityId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
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

            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebEntityMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;
        }


        #endregion

        #endregion

        #region "Addresses List"
        /*
        public List<AddrListMaintModel> GetAddressList(string RefTo, string RefKey)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = new SqlParameter("@RefTo", String.IsNullOrEmpty(RefTo) ? "" : RefTo);
            Parameters[2] = new SqlParameter("@RefKey", String.IsNullOrEmpty(RefKey) ? "" : RefKey);

            var execResult = await objDataEngine.ExecuteCommandAsync("WebAddressListSelect", CommandType.StoredProcedure, Parameters);
            var _AddrListMaintModel = new List<AddrListMaintModel>();

            while (execResult.Read())
            {
                _AddrListMaintModel.Add(new AddrListMaintModel
                {

                    SelectedAddrType = Convert.ToString(execResult["Address Type"]),
                    MainMailingInd = BoolConverter(execResult["Main Mailing"]),
                    addr1 = Convert.ToString(execResult["Address1"]),
                    addr2 = Convert.ToString(execResult["Address2"]),
                    addr3 = Convert.ToString(execResult["Address3"]),
                    Selectedstate = Convert.ToString(execResult["StateCd"]),
                    PostalCode = Convert.ToString(execResult["ZipCd"]),
                    SelectedCountry = Convert.ToString(execResult["Country"]),
                    selectedregion = Convert.ToString(execResult["Region"]),
                    SelectedRefCd = Convert.ToString(execResult["RefCd"])
                });
            };
            return _AddrListMaintModel;

        }
        public AddrListMaintModel GetAddressDetail(string RefTo, string RefKey, string RefCd)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[4];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = new SqlParameter("@RefTo", String.IsNullOrEmpty(RefTo) ? "" : RefTo);
            Parameters[2] = new SqlParameter("@RefKey", String.IsNullOrEmpty(RefKey) ? "" : RefKey);
            Parameters[3] = new SqlParameter("@RefCd", String.IsNullOrEmpty(RefCd) ? "" : RefCd);

            var execResult = await objDataEngine.ExecuteCommandAsync("WebAddressSelect", CommandType.StoredProcedure, Parameters);

           var _GetAddress = new AddrListMaintModel();

               while (execResult.Read())            
               {
                _GetAddress.SelectedAddrType = Convert.ToString(execResult["Address Type"]);
                _GetAddress.addrtype = BaseClass.WebGetRefLib("Address");
                _GetAddress.MainMailingInd = BoolConverter(execResult["Main Mailing"]);
                _GetAddress.addr1 = Convert.ToString(execResult["Address1"]);
                _GetAddress.addr2 = Convert.ToString(execResult["Address2"]);
                _GetAddress.addr3 = Convert.ToString(execResult["Address3"]);
                _GetAddress.State = BaseClass.WebGetRefLib("State");
                _GetAddress.Selectedstate = Convert.ToString(execResult["StateCd"]);
                _GetAddress.PostalCode = Convert.ToString(execResult["ZipCd"]);
                _GetAddress.Country = BaseClass.WebGetRefLib("Country");
                _GetAddress.SelectedCountry = Convert.ToString(execResult["Country"]);
                _GetAddress.region = BaseClass.WebGetRefLib("RegionCd");
                _GetAddress.selectedregion = Convert.ToString(execResult["Region"]);
                _GetAddress.SelectedRefCd = Convert.ToString(execResult["RefCd"]);                        
            };
            return _GetAddress;

        }
        public MsgRetriever SaveAddressList(AddrListMaintModel _AddrListMaint, string RefTo, string RefCd )
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[14];
            Parameters[0] = new SqlParameter("@Func", GetIssNo);
            Parameters[1] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[2] = String.IsNullOrEmpty(RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", RefTo);
            Parameters[3] = String.IsNullOrEmpty(_AddrListMaint.SelectedRefCd) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", _AddrListMaint.SelectedRefCd);
            Parameters[4] = String.IsNullOrEmpty(RefCd) ? new SqlParameter("@RefCd", DBNull.Value) : new SqlParameter("@RefCd", RefCd);
            Parameters[5] = String.IsNullOrEmpty(_AddrListMaint.addr1) ? new SqlParameter("@Street1", DBNull.Value) : new SqlParameter("@Street1", _AddrListMaint.addr1);
            Parameters[6] = String.IsNullOrEmpty(_AddrListMaint.addr2) ? new SqlParameter("@Street2", DBNull.Value) : new SqlParameter("@Street2 ", _AddrListMaint.addr2);
            Parameters[7] = String.IsNullOrEmpty(_AddrListMaint.addr3) ? new SqlParameter("@Street3", DBNull.Value) : new SqlParameter("@Street3 ", _AddrListMaint.addr3);
            Parameters[8] = String.IsNullOrEmpty(_AddrListMaint.Selectedstate) ? new SqlParameter("@State", DBNull.Value) : new SqlParameter("@State ", _AddrListMaint.Selectedstate);
            Parameters[9] = String.IsNullOrEmpty(_AddrListMaint.PostalCode) ? new SqlParameter("@ZipCd", DBNull.Value) : new SqlParameter("@ZipCd ", _AddrListMaint.PostalCode);
            Parameters[10] = String.IsNullOrEmpty(_AddrListMaint.SelectedCountry) ? new SqlParameter("@Ctry", DBNull.Value) : new SqlParameter("@Ctry ", _AddrListMaint.SelectedCountry);
            Parameters[11] = String.IsNullOrEmpty(_AddrListMaint.UserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId ", _AddrListMaint.UserId);
            Parameters[12] = new SqlParameter("@MailInd",ConvertBoolDB(_AddrListMaint.MainMailingInd));

            Parameters[13] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[13].Direction = ParameterDirection.ReturnValue;

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebAddressMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            return Descp;

        }
        public MsgRetriever DelAddress(string RefTo, string RefKey, string RefCd,string userId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[6];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = new SqlParameter("@RefTo", String.IsNullOrEmpty(RefTo) ? "" : RefTo);
            Parameters[2] = new SqlParameter("@RefKey", String.IsNullOrEmpty(RefKey) ? "" : RefKey);
            Parameters[3] = new SqlParameter("@RefCd", String.IsNullOrEmpty(RefCd) ? "" : RefCd);
            Parameters[4] = new SqlParameter("@UserId", String.IsNullOrEmpty(userId) ? "" : userId);
            Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[5].Direction = ParameterDirection.ReturnValue;
            var Cmd = objDataEngine.ExecuteWithReturnValue("WebAddressDelete", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);

            var Descp = GetMessageCode(Result);


            return Descp;

        }
        */
        #endregion


        #region "Location Acceptance List"
        public async Task<List<LocationAcceptListModel>> GetLocationAcceptList(string AcctNo, CardnAccNo _CardnAccNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo ", AcctNo);
            Parameters[2] = String.IsNullOrEmpty(_CardnAccNo.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo ", _CardnAccNo.CardNo);

            var execResult =await objDataEngine.ExecuteCommandAsync("WebLocationAcceptanceListSelect", CommandType.StoredProcedure, Parameters);
            var _LocationAcceptList = new List<LocationAcceptListModel>();

            while (execResult.Read())
            {
                _LocationAcceptList.Add(new LocationAcceptListModel
                {
                    MerchantId = Convert.ToString(execResult["Dealer"]),
                    DBAName = Convert.ToString(execResult["DBA Name"]),
                    s_state = Convert.ToString(execResult["DBA City"]),
                    UserId = Convert.ToString(execResult["User Id"]),
                    CreationDate = Convert.ToString(execResult["Creation Date"]),
                    SiteId = Convert.ToString(execResult["SiteId"])


                }
                );
            }

            objDataEngine.CloseConnection();
            return _LocationAcceptList;
        }
        public async Task<LocationAcceptListModel> GetLocationAcceptDetail(string AcctNo, string BusnLoc)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo ", AcctNo);
            Parameters[2] = String.IsNullOrEmpty(BusnLoc) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation ", BusnLoc);

            var execResult =await objDataEngine.ExecuteCommandAsync("WebLocationAcceptanceSelect", CommandType.StoredProcedure, Parameters);

            var _GetLocationAccept = new LocationAcceptListModel();

            while (execResult.Read())
            {
                _GetLocationAccept.UserId = Convert.ToString(execResult["User Id"]);
                _GetLocationAccept.CreationDate = DateConverter(execResult["Creation Date"]);
                _GetLocationAccept.BusnLoc = Convert.ToString(execResult["BusnLocation"]);
            };
            objDataEngine.CloseConnection();
            return _GetLocationAccept;

        }
        public async Task<MsgRetriever> SaveLocationAcceptance(LocationAcceptListModel _LocationAcceptList)
        {
            MsgRetriever Descp = new MsgRetriever();
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            if (_LocationAcceptList.SelectedStates[0] != "")
            {
                foreach (var x in _LocationAcceptList.SelectedStates)
                {
                    objDataEngine.InitiateConnection();
                    SqlParameter[] Parameters = new SqlParameter[7];
                    Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                    Parameters[1] = String.IsNullOrEmpty(_LocationAcceptList._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo ", _LocationAcceptList._CardnAccNo.AccNo);
                    Parameters[2] = String.IsNullOrEmpty(_LocationAcceptList._CardnAccNo.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _LocationAcceptList._CardnAccNo.CardNo);
                    Parameters[3] = new SqlParameter("@State", Convert.ToString(x));
                    Parameters[4] = new SqlParameter("@BusnLocation", DBNull.Value);
                    Parameters[5] = new SqlParameter("@UserId", this.GetUserId);

                    Parameters[6] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                    Parameters[6].Direction = ParameterDirection.ReturnValue;

                    var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebLocationAcceptanceMaint", CommandType.StoredProcedure, Parameters);
                    var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                    Descp =await GetMessageCode(Result);
                    objDataEngine.CloseConnection();
                }
                objDataEngine.CloseConnection();
                return Descp;
            }

            else
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[6];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_LocationAcceptList._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _LocationAcceptList._CardnAccNo.AccNo);
                Parameters[2] = String.IsNullOrEmpty(_LocationAcceptList._CardnAccNo.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _LocationAcceptList._CardnAccNo.CardNo);
                Parameters[3] = new SqlParameter("@State", null);
                Parameters[4] = String.IsNullOrEmpty(_LocationAcceptList.BusnLoc) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation ", _LocationAcceptList.BusnLoc);
                Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[5].Direction = ParameterDirection.ReturnValue;

                var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebLocationAcceptanceMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                Descp =await GetMessageCode(Result);

                objDataEngine.CloseConnection();
                return Descp;
            }


        }


        public async Task<MsgRetriever> DeleteLocationAcceptance(string AcctNo, string BusnLocation, string CardNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[6];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo ", AcctNo);
            Parameters[2] = String.IsNullOrEmpty(CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", CardNo);
            Parameters[3] = String.IsNullOrEmpty(BusnLocation) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", BusnLocation);
            Parameters[4] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[5].Direction = ParameterDirection.ReturnValue;

            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebLocationAcceptanceDelete", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            MsgRetriever Descp =await GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return Descp;
        }
        #endregion

        #region "Temporary Credit Control"
        public async Task<TempCreditCtrlModel> GetTempCreditLimitDetail(string acctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = new SqlParameter("@AcctNo", acctNo);

            var execResult =await objDataEngine.ExecuteCommandAsync("WebTempCreditLimitSelect", CommandType.StoredProcedure, Parameters);

            var _TempCreditCtrlModel = new TempCreditCtrlModel();

            while (execResult.Read())
            {
                _TempCreditCtrlModel.AllowCreditLimit = ConverterDecimal(execResult["CreditLimit"]);
                _TempCreditCtrlModel.ExpDate = DateConverter(execResult["TxnDate"]);
                _TempCreditCtrlModel.UserId = Convert.ToString(execResult["UserId"]);
                _TempCreditCtrlModel.CreationDate = DateConverter(execResult["CreationDate"]);
            };
            objDataEngine.CloseConnection();

            return _TempCreditCtrlModel;


        }


        public async Task<MsgRetriever> SaveTempCreditCtrl(TempCreditCtrlModel _tempCredit, string acctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[7];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = new SqlParameter("@AcctNo", acctNo);
            Parameters[2] = String.IsNullOrEmpty(_tempCredit.AllowCreditLimit) ? new SqlParameter("@CreditLimit", DBNull.Value) : new SqlParameter("@CreditLimit ", _tempCredit.AllowCreditLimit);
            Parameters[3] = String.IsNullOrEmpty(_tempCredit.ExpDate) ? new SqlParameter("@EffDateFrom", DBNull.Value) : new SqlParameter("@EffDateFrom ", DateConverterDB(_tempCredit.ExpDate));
            Parameters[4] = String.IsNullOrEmpty(_tempCredit.ExpDate) ? new SqlParameter("@EffDateTo", DBNull.Value) : new SqlParameter("@EffDateTo ", DateConverterDB(_tempCredit.ExpDate));
            Parameters[5] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[6] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[6].Direction = ParameterDirection.ReturnValue;

            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebTempCreditLimitMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;

        }


        #endregion


        #region "Vehicle"
        //public List<VehiclesListModel> GetVehicleList(string AcctNo, string ApplId)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
        //    objDataEngine.InitiateConnection();
        //    SqlParameter[] Parameters = new SqlParameter[3];
        //    Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
        //    Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
        //    Parameters[2] = String.IsNullOrEmpty(ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", ApplId);

        //    var execResult = await objDataEngine.ExecuteCommandAsync("WebVehicleListSelect", CommandType.StoredProcedure, Parameters);
        //    var _GetVehicleList = new List<VehiclesListModel>();

        //    while (execResult.Read())
        //    {
        //        _GetVehicleList.Add(new VehiclesListModel 
        //        {
        //            CardNo = Convert.ToString(execResult["CardNo"]),
        //            SelectedCardType = Convert.ToString(execResult["Card Type"]),
        //            pin = Convert.ToString(execResult["PIN"]),
        //            VehRegtNo = Convert.ToString(execResult["VRN"]),
        //            VehRegDate = Convert.ToString(execResult["Registered Date"]),
        //            SelectedVehMaker = Convert.ToString(execResult["Vehicle Maker"]),
        //            SelectedSts = Convert.ToString(execResult["Status"]),
        //            XrefCardNo = Convert.ToString(execResult["Xref CardNo"]),
        //            OdoMeterReading = Convert.ToString(execResult["Odometer Reading"]),
        //            OdoMeterUpdate = Convert.ToString(execResult["Odometer Update"]),
        //            PolicyExpDate = Convert.ToString(execResult["Card Expiry"]),
        //            RoadTaxExpDate = Convert.ToString(execResult["RoadTax Expiry"]),
        //            SelectedVehType = Convert.ToString(execResult["Vehicle Type"]),
        //            SelectedVehColor = Convert.ToString(execResult["Vehicle Color"]),
        //            SelectedVehModel = Convert.ToString(execResult["Vehicle Model"]),
        //            CardTerminated = DateConverter(execResult["Card Terminated"])
        //        });
        //    };
        //    return _GetVehicleList;

        //}
        //public VehiclesListModel GetVehicleDetail(VehiclesListModel _VehiclesListModel)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
        //    objDataEngine.InitiateConnection();
        //    SqlParameter[] Parameters = new SqlParameter[3];
        //    Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
        //    Parameters[1] = String.IsNullOrEmpty(_VehiclesListModel.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo ", _VehiclesListModel.CardNo);
        //    Parameters[2] = String.IsNullOrEmpty(_VehiclesListModel.VehRegtNo) ? new SqlParameter("@VRN", DBNull.Value) : new SqlParameter("@VRN ", _VehiclesListModel.VehRegtNo);

        //    var execResult = await objDataEngine.ExecuteCommandAsync("WebVehicleSelect", CommandType.StoredProcedure, Parameters);

        //    _VehiclesListModel = new VehiclesListModel();

        //    while (execResult.Read())
        //    {
        //        _VehiclesListModel.CardNo = Convert.ToString(execResult["CardNo"]);
        //        _VehiclesListModel.VehRegtNo = Convert.ToString(execResult["VehRegsNo"]);
        //        _VehiclesListModel.SkdsInd =BaseClass.BoolConverter(execResult["SKDSInd"]);// 87hide
        //        _VehiclesListModel.SkdsQuota = ConverterDecimal(execResult["SKDS Quota"]);//767 hide
        //        _VehiclesListModel.SelectedVehModel = Convert.ToString(execResult["Vehicle Model"]);
        //        _VehiclesListModel.SelectedVehMaker = Convert.ToString(execResult["Vehicle Maker"]);
        //        _VehiclesListModel.VehRegDate = Convert.ToString(execResult["VehRegsDate"]);
        //        _VehiclesListModel.SelectedVehType = Convert.ToString(execResult["VehType"]);
        //        _VehiclesListModel.SelectedVehColor = Convert.ToString(execResult["Color"]);
        //        _VehiclesListModel.OdoMeterReading = Convert.ToString(execResult["CurrOdoReading"]);
        //        _VehiclesListModel.OdoMeterUpdate = Convert.ToString(execResult["OdoLastUpd"]);
        //        _VehiclesListModel.RoadTaxExpDate = Convert.ToString(execResult["RoadTaxExpiry"]);
        //        _VehiclesListModel.SelectedSts = Convert.ToString(execResult["Sts"]);


        //    };
        //    return _VehiclesListModel;

        //}
        //public MsgRetriever SaveVehicleList(VehiclesListModel _VehiclesListModel)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
        //    objDataEngine.InitiateConnection();
        //    SqlParameter[] Parameters = new SqlParameter[12];
        //    Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
        //    Parameters[1] = String.IsNullOrEmpty(_VehiclesListModel.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo ", _VehiclesListModel.CardNo);
        //    Parameters[2] = String.IsNullOrEmpty(_VehiclesListModel.VehRegtNo) ? new SqlParameter("@VRN", DBNull.Value) : new SqlParameter("@VRN ", _VehiclesListModel.VehRegtNo);
        //    Parameters[3] = new SqlParameter("@SKDSInd", _VehiclesListModel.SkdsInd); 
        //    Parameters[4] = new SqlParameter("@SKDSQuota", ConverterDecimal(_VehiclesListModel.SkdsQuota));
        //    Parameters[5] = String.IsNullOrEmpty(_VehiclesListModel.SelectedVehMaker) ? new SqlParameter("@Manufacturer", DBNull.Value) : new SqlParameter("@Manufacturer ", _VehiclesListModel.SelectedVehMaker);
        //    Parameters[6] = String.IsNullOrEmpty(_VehiclesListModel.SelectedVehModel) ? new SqlParameter("@Model", DBNull.Value) : new SqlParameter("@Model ", _VehiclesListModel.SelectedVehModel);
        //    Parameters[7] = String.IsNullOrEmpty(_VehiclesListModel.VehRegDate) ? new SqlParameter("@VehRegsDate", DBNull.Value) : new SqlParameter("@VehRegsDate ", _VehiclesListModel.VehRegDate);
        //    Parameters[8] = String.IsNullOrEmpty(_VehiclesListModel.SelectedVehType) ? new SqlParameter("@VehType", DBNull.Value) : new SqlParameter("@VehType ", _VehiclesListModel.SelectedVehType);
        //    Parameters[9] = String.IsNullOrEmpty(_VehiclesListModel.SelectedVehColor) ? new SqlParameter("@Color", DBNull.Value) : new SqlParameter("@Color ", _VehiclesListModel.SelectedVehColor);
        //    Parameters[10] = new SqlParameter("@RoadTaxExpiry", BaseClass.DateConverterDB(_VehiclesListModel.RoadTaxExpDate));
        //    Parameters[11] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
        //    Parameters[11].Direction = ParameterDirection.ReturnValue;
        //    //String.IsNullOrEmpty(_VehiclesListModel.SelectedVehColor) ? new SqlParameter("@Color", DBNull.Value) : new SqlParameter("@Color ", _VehiclesListModel.SelectedVehColor);
        //    var Cmd = objDataEngine.ExecuteWithReturnValueAsync("WebVehicleMaint", CommandType.StoredProcedure, Parameters);
        //    var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
        //    var Descp = GetMessageCode(Result);

        //    return Descp;
        //}
        #endregion

        #region"Change Status"
        public async Task<ChangeStatus> GetChangedAcctStsDetail(string id, string refCd)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[6];
            var _GetChangeStatus = new ChangeStatus();

            if (refCd == "ACCT")
            {
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(id) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", id);
                Parameters[2] = new SqlParameter("@CardNo", DBNull.Value);
                Parameters[3] = new SqlParameter("@MerchAcctNo", DBNull.Value);
                Parameters[4] = new SqlParameter("@BusnLocation", DBNull.Value);
                Parameters[5] = new SqlParameter("@AppcId", DBNull.Value);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebChangeStatusSelect", CommandType.StoredProcedure, Parameters);

                while (execResult.Read())
                {
                    _GetChangeStatus.CurrentStatus =await BaseClass.WebGetRefLib("CardSts");
                    _GetChangeStatus.SelectedCurrentStatus = Convert.ToString(execResult["Sts"]);
                    _GetChangeStatus.RefType =await BaseClass.WebGetRefLib("EventType");
                    _GetChangeStatus.SelectedRefType = Convert.ToString(execResult["EventType"]);
                    _GetChangeStatus.ReasonCode =await BaseClass.WebGetRefLib("ReasonCd", "64");
                    _GetChangeStatus.SelectedReasonCode = Convert.ToString(execResult["ReasonCd"]);
                    _GetChangeStatus.Remarks = Convert.ToString(execResult["Remarks"]);
                    _GetChangeStatus.ChangeStatusTo =await BaseClass.WebGetRefLib("AcctSts", "");
                };

            }
            else if (refCd == "CARD")
            {
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@AcctNo", DBNull.Value);
                Parameters[2] = String.IsNullOrEmpty(id) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", id);
                Parameters[3] = new SqlParameter("@MerchAcctNo", DBNull.Value);
                Parameters[4] = new SqlParameter("@BusnLocation", DBNull.Value);
                Parameters[5] = new SqlParameter("@AppcId", DBNull.Value);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebChangeStatusSelect", CommandType.StoredProcedure, Parameters);

                while (execResult.Read())
                {
                    _GetChangeStatus.CurrentStatus =await BaseClass.WebGetRefLib("CardSts", "1");
                    _GetChangeStatus.SelectedCurrentStatus = Convert.ToString(execResult["Sts"]);
                    _GetChangeStatus.RefType =await BaseClass.WebGetRefLib("EventType");
                    _GetChangeStatus.SelectedRefType = Convert.ToString(execResult["EventType"]);
                    _GetChangeStatus.ReasonCode =await BaseClass.WebGetRefLib("ReasonCd", "32");
                    _GetChangeStatus.SelectedReasonCode = Convert.ToString(execResult["ReasonCd"]);
                    _GetChangeStatus.Remarks = Convert.ToString(execResult["Remarks"]);
                    _GetChangeStatus.ChangeStatusTo =await BaseClass.WebGetRefLib("CardSts", "1");
                };
            }
            else if (refCd == "MERCH")
            {
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@AcctNo", DBNull.Value);
                Parameters[2] = new SqlParameter("@CardNo", DBNull.Value);
                Parameters[3] = String.IsNullOrEmpty(id) ? new SqlParameter("@MerchAcctNo", DBNull.Value) : new SqlParameter("@MerchAcctNo", id);
                Parameters[4] = new SqlParameter("@BusnLocation", DBNull.Value);
                Parameters[5] = new SqlParameter("@AppcId", DBNull.Value);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebChangeStatusSelect", CommandType.StoredProcedure, Parameters);

                while (execResult.Read())
                {
                    _GetChangeStatus.CurrentStatus =await BaseClass.WebGetRefLib("MerchAcctSts");
                    _GetChangeStatus.SelectedCurrentStatus = Convert.ToString(execResult["Sts"]);
                    _GetChangeStatus.RefType =await BaseClass.WebGetRefLib("EventType");
                    _GetChangeStatus.SelectedRefType = Convert.ToString(execResult["EventType"]);
                    _GetChangeStatus.ReasonCode =await BaseClass.WebGetRefLib("MerchReasonCd");
                    _GetChangeStatus.SelectedReasonCode = Convert.ToString(execResult["ReasonCd"]);
                    _GetChangeStatus.Remarks = Convert.ToString(execResult["Remarks"]);
                    _GetChangeStatus.ChangeStatusTo =await BaseClass.WebGetRefLib("MerchAcctSts");
                };
            }
            else if (refCd == "BUSN")
            {
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@AcctNo", DBNull.Value);
                Parameters[2] = new SqlParameter("@CardNo", DBNull.Value);
                Parameters[3] = new SqlParameter("@MerchAcctNo", DBNull.Value);
                Parameters[4] = String.IsNullOrEmpty(id) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", id);
                Parameters[5] = new SqlParameter("@AppcId", DBNull.Value);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebChangeStatusSelect", CommandType.StoredProcedure, Parameters);

                while (execResult.Read())
                {
                    _GetChangeStatus.CurrentStatus =await BaseClass.WebGetRefLib("MerchAcctSts");
                    _GetChangeStatus.SelectedCurrentStatus = Convert.ToString(execResult["Sts"]);
                    _GetChangeStatus.RefType =await BaseClass.WebGetRefLib("EventType");
                    _GetChangeStatus.SelectedRefType = Convert.ToString(execResult["EventType"]);
                    _GetChangeStatus.ReasonCode =await BaseClass.WebGetRefLib("MerchReasonCd");
                    _GetChangeStatus.SelectedReasonCode = Convert.ToString(execResult["ReasonCd"]);
                    _GetChangeStatus.Remarks = Convert.ToString(execResult["Remarks"]);
                    _GetChangeStatus.ChangeStatusTo =await BaseClass.WebGetRefLib("MerchAcctSts");
                };

            }
            else if (refCd == "APPC")
            {
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@AcctNo", DBNull.Value);
                Parameters[2] = new SqlParameter("@CardNo", DBNull.Value);
                Parameters[3] = new SqlParameter("@MerchAcctNo", DBNull.Value);
                Parameters[4] = new SqlParameter("@BusnLocation", DBNull.Value);
                Parameters[5] = String.IsNullOrEmpty(id) ? new SqlParameter("@AppcId", DBNull.Value) : new SqlParameter("@AppcId", id);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebChangeStatusSelect", CommandType.StoredProcedure, Parameters);

                while (execResult.Read())
                {
                    _GetChangeStatus.CurrentStatus =await BaseClass.WebGetRefLib("AppcSts");
                    _GetChangeStatus.SelectedCurrentStatus = Convert.ToString(execResult["Sts"]);
                    _GetChangeStatus.RefType =await BaseClass.WebGetRefLib("EventType");
                    _GetChangeStatus.SelectedRefType = Convert.ToString(execResult["EventType"]);
                    _GetChangeStatus.ReasonCode =await BaseClass.WebGetRefLib("ReasonCd");
                    _GetChangeStatus.SelectedReasonCode = Convert.ToString(execResult["ReasonCd"]);
                    _GetChangeStatus.Remarks = Convert.ToString(execResult["Remarks"]);
                    _GetChangeStatus.ChangeStatusTo =await BaseClass.WebGetRefLib("AppcSts");
                };
            }
            objDataEngine.CloseConnection();
            return _GetChangeStatus;


        }
        public async Task<MsgRetriever> SaveChangedAcctSts(ChangeStatus _ChangeStatus)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[12];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_ChangeStatus._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _ChangeStatus._CardnAccNo.AccNo);
            Parameters[2] = String.IsNullOrEmpty(_ChangeStatus._CardnAccNo.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _ChangeStatus._CardnAccNo.CardNo);
            Parameters[3] = String.IsNullOrEmpty(_ChangeStatus.MerchAcctNo) ? new SqlParameter("@MerchAcctNo", DBNull.Value) : new SqlParameter("@MerchAcctNo", _ChangeStatus.MerchAcctNo);
            Parameters[4] = String.IsNullOrEmpty(_ChangeStatus.BusnLocation) ? new SqlParameter("@BusnLocation", DBNull.Value) : new SqlParameter("@BusnLocation", _ChangeStatus.BusnLocation);
            Parameters[5] = string.IsNullOrEmpty(_ChangeStatus.AppcId) ? new SqlParameter("@AppcId", DBNull.Value) : new SqlParameter("@AppcId", _ChangeStatus.AppcId);
            Parameters[6] = String.IsNullOrEmpty(_ChangeStatus.SelectedRefType) ? new SqlParameter("@EventType", DBNull.Value) : new SqlParameter("@EventType", _ChangeStatus.SelectedRefType);
            Parameters[7] = String.IsNullOrEmpty(_ChangeStatus.SelectedChangeStatusTo) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _ChangeStatus.SelectedChangeStatusTo);
            Parameters[8] = String.IsNullOrEmpty(_ChangeStatus.SelectedReasonCode) ? new SqlParameter("@ReasonCd", DBNull.Value) : new SqlParameter("@ReasonCd", _ChangeStatus.SelectedReasonCode);
            Parameters[9] = String.IsNullOrEmpty(_ChangeStatus.Remarks) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _ChangeStatus.Remarks);
            Parameters[10] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
            Parameters[11] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[11].Direction = ParameterDirection.ReturnValue;


            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebChangeStatusMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;
        }
        #endregion

        #region "AcctGuarantee"
        public async Task<List<AcctGuarantee>> GetAcctGuaranteeList(AcctGuarantee _AcctGuarantee)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
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
                    BankCd =await WebGetRefLib("Bank"),
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
            objDataEngine.CloseConnection();
            return _AcctGuaranteeList;

        }
        public async Task<AcctGuarantee> GetAcctGuaranteeDetail(AcctGuarantee _AcctGuarantee)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
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
                _AcctGuarantee.BankCd =await BaseClass.WebGetRefLib("Bank");
                _AcctGuarantee.SelectedBankCd = Convert.ToString(execResult["BankName"]);
                _AcctGuarantee.BankAcctNo = Convert.ToString(execResult["BankAcctNo"]);
                _AcctGuarantee.BankBranchCd = Convert.ToString(execResult["BankBranchCd"]);
                _AcctGuarantee.EffFromDate = DateConverter(execResult["EffFromDate"]);
                _AcctGuarantee.EffToDate = DateConverter(execResult["EffToDate"]);
                _AcctGuarantee.Remarks = Convert.ToString(execResult["Remarks"]);
                _AcctGuarantee.UserId = Convert.ToString(execResult["UserId"]);
            };
            objDataEngine.CloseConnection();
            return _getAcctGuarantee;

        }
        public async Task<MsgRetriever> SaveAcctGuarantee(AcctGuarantee _AcctGuarantee)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
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

            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebAcctGuaranteeMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;

        }

        #endregion

        #region "AcctPostedTxnSearch"
        public async Task<List<AcctPostedTxnSearch>> GetAcctPostedTxnSearch(AcctPostedTxnSearch _acctPostedTxnSearch)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
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
                    //  SelectedTxnCd = ConvertInt(execResult["TxnCd"]),
                    TxnDesp = Convert.ToString(execResult["Txn Descp"]),
                    TxnAmt = ConverterDecimal(execResult["Txn Amt"]),
                    Dealer = Convert.ToString(execResult["Dealer"]),
                    //TermId = Convert.ToString(execResult["TermId"]),
                    //ApproveCd = Convert.ToString(execResult["AppvCd"]),
                    AuthCardNo = Convert.ToString(execResult["AuthCardNo"]),
                    PrcsDate = DateConverter(execResult["PrcsDate"]),
                    TxnId = Convert.ToString(execResult["Txn Id"]),
                    InvoicDt = DateConverter(execResult["InvoiceDate"]),
                    SiteId = Convert.ToString(execResult["SiteId"]),
                    RecieptId = Convert.ToString(execResult["Receipt No"]),
                    Batch = Convert.ToString(execResult["BatchNo"]),
                    VehRegNo = Convert.ToString(execResult["VehRegsNo"]),
                    DriverName = Convert.ToString(execResult["Driver Name"]),
                    TotalTxnAmt = Convert.ToString(execResult["TotalTxnAmt"]),
                    Quantity=ConverterDecimal(execResult["Qty"])
                });
            };
            objDataEngine.CloseConnection();
            return _AcctPostedTxnSearch;

        }



        //public MsgRetriever SaveAcctPostedTxnSearch(AcctPostedTxnSearch _acctPostedTxnSearch)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
        //    objDataEngine.InitiateConnection();
        //    SqlParameter[] Parameters = new SqlParameter[7];
        //    Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
        //    Parameters[1] = String.IsNullOrEmpty(_acctPostedTxnSearch.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _acctPostedTxnSearch.AcctNo);
        //    Parameters[2] = String.IsNullOrEmpty(_acctPostedTxnSearch.SelectedCardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _acctPostedTxnSearch.SelectedCardNo);
        //    Parameters[3] = String.IsNullOrEmpty(_acctPostedTxnSearch.SelectedTxnCategory) ? new SqlParameter("@TxnCategory", DBNull.Value) : new SqlParameter("@TxnCategory", _acctPostedTxnSearch.SelectedTxnCategory);
        //    Parameters[4] = new SqlParameter("@TxnCd", _acctPostedTxnSearch.SelectedTxnCd);
        //    Parameters[5] = String.IsNullOrEmpty(_acctPostedTxnSearch.TxnDate) ? new SqlParameter("@TxnDate", DBNull.Value) : new SqlParameter("@TxnDate", _acctPostedTxnSearch.TxnDate);
        //    Parameters[6] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
        //    Parameters[6].Direction = ParameterDirection.ReturnValue;

        //    var Cmd = objDataEngine.ExecuteWithReturnValueAsync("WebAcctPostedTxnSearch", CommandType.StoredProcedure, Parameters);
        //    var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
        //    var Descp =await GetMessageCode(Result);

        //    objDataEngine.CloseConnection();
        //    return Descp;

        //}
        #endregion


        #region "Event Logger"
        public async Task<List<EventLogger>> GetEventSearch(EventLogger _eventLog)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.Admin, DBType.Maint);
            objDataEngine.InitiateConnection();

            SqlParameter[] Parameters = new SqlParameter[4];
            Parameters[0] = String.IsNullOrEmpty(_eventLog.SelectedModule) ? new SqlParameter("@Module", DBNull.Value) : new SqlParameter("@Module", _eventLog.SelectedModule);
            Parameters[1] = String.IsNullOrEmpty(_eventLog.AccountNo) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", _eventLog.AccountNo);
            Parameters[2] = String.IsNullOrEmpty(_eventLog.SelectedEventType) ? new SqlParameter("@EventType", DBNull.Value) : new SqlParameter("@EventType", _eventLog.SelectedEventType);
            Parameters[3] = new SqlParameter("@EventDate", DateConverterDB(_eventLog.EventDate));
            var execResult = await objDataEngine.ExecuteCommandAsync("WebEventSearch", CommandType.StoredProcedure, Parameters);
            var EventLoggerSearch = new List<EventLogger>();

            while (execResult.Read())
            {
                EventLoggerSearch.Add(new EventLogger
                {
                    SelectedEventType = Convert.ToString(execResult["EventType"]),
                    SelectedEventSts = Convert.ToString(execResult["Status"]),
                    AccountNo = Convert.ToString(execResult["AcctNo"]),
                    CardNo = Convert.ToString(execResult["CardNo"]),
                    SelectedReasonCode = Convert.ToString(execResult["ReasonCd"]),
                    ClosedDate = DateConverter(execResult["ClosedDate"]),
                    ReminderDatetime = DateConverter(execResult["RecallDate"]),
                    sysInd = Convert.ToString(execResult["SysInd"]),
                    EventId = Convert.ToString(execResult["EventId"]),
                    Description = Convert.ToString(execResult["Descp"]),
                    UserId = Convert.ToString(execResult["UserId"])
                });
            };
            objDataEngine.CloseConnection();
            return EventLoggerSearch;
        }

        public async Task<List<EventLogger>> GetEventlist(EventLogger _eventLog)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@Module", "I");
            Parameters[1] = String.IsNullOrEmpty(_eventLog.AccountNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _eventLog.AccountNo);
            var execResult = await objDataEngine.ExecuteCommandAsync("WebEventListSelect", CommandType.StoredProcedure, Parameters);
            var EventLoggerList = new List<EventLogger>();

            while (execResult.Read())
            {
                EventLoggerList.Add(new EventLogger
                {
                    EventId = Convert.ToString(execResult["Event Id"]),
                    SelectedEventType = Convert.ToString(execResult["Event Type"]),
                    RefKey = Convert.ToString(execResult["Reference Key"]),
                    Description = Convert.ToString(execResult["Description"]),
                    SelectedReasonCode = Convert.ToString(execResult["Reason"]),
                    ReminderDatetime = DateConverter(execResult["Reminder"]),
                    ClosedDate = DateConverter(execResult["Closed"]),
                    UserId = Convert.ToString(execResult["CreatedBy"]),
                    CreationDate = DateConverter(execResult["CreationDate"])
                });
            };
            objDataEngine.CloseConnection();
            return EventLoggerList;

        }

        public async Task<EventLogger> GetEventLoggerDetail(EventLogger _eventLogger, string eventID)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = String.IsNullOrEmpty(_eventLogger.SelectedModule) ? new SqlParameter("@Module", DBNull.Value) : new SqlParameter("@Module", _eventLogger.SelectedModule);

            Parameters[1] = String.IsNullOrEmpty(_eventLogger.EventId) ? new SqlParameter("@EventId", DBNull.Value) : new SqlParameter("@EventId", _eventLogger.EventId);


            var execResult = await objDataEngine.ExecuteCommandAsync("WebEventSelect", CommandType.StoredProcedure, Parameters);
            while (execResult.Read())
            {

                var _eventlogger = new EventLogger
                {

                    EventId = Convert.ToString(execResult["Event Id"]),
                    SelectedEventType = Convert.ToString(execResult["EventType"]),
                    RefKey = Convert.ToString(execResult["Reference Key"]),
                    AccountNo = Convert.ToString(execResult["AcctNo"]),
                    Description = Convert.ToString(execResult["Descp"]),
                    SelectedReasonCode = Convert.ToString(execResult["ReasonCd"]),
                    ReminderDatetime = Convert.ToString(execResult["Reminder"]),
                    refDoc = Convert.ToString(execResult["Ref Document"]),
                    ClosedDate = DateConverter(execResult["ClsDate"]),
                    CreationDate = Convert.ToString(execResult["CreationDate"]),
                    SelectedEventSts = Convert.ToString(execResult["Sts"])
                };
                if (_eventLogger.SelectedModule == "I")
                {
                    _eventlogger.EventType =await WebGetRefLib("EventType");
                    _eventlogger.ReasonCd =await WebGetRefLib("ReasonCd");
                }
                else
                {
                    _eventlogger.EventType =await WebGetRefLib("MerchEventType");
                    _eventlogger.ReasonCd =await WebGetRefLib("MerchReasonCd");
                }

                objDataEngine.CloseConnection();
                return _eventlogger;
            };
            objDataEngine.CloseConnection();
            return new EventLogger();
        }
        public async Task<MsgRetriever> SaveEventMaint(EventLogger _Logger)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[13];
            Parameters[0] = new SqlParameter("@Module", "I");
            Parameters[1] = String.IsNullOrEmpty(_Logger.EventId) ? new SqlParameter("@EventId", DBNull.Value) : new SqlParameter("@EventId", _Logger.EventId);
            Parameters[2] = String.IsNullOrEmpty(_Logger.SelectedEventType) ? new SqlParameter("@EventType", DBNull.Value) : new SqlParameter("@EventType", _Logger.SelectedEventType);
            Parameters[3] = String.IsNullOrEmpty(_Logger.AccountNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _Logger.AccountNo);
            Parameters[4] = String.IsNullOrEmpty(_Logger.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", _Logger.RefKey);
            Parameters[5] = String.IsNullOrEmpty(_Logger.Description) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _Logger.Description);
            Parameters[6] = String.IsNullOrEmpty(_Logger.SelectedReasonCode) ? new SqlParameter("@ReasonCd", DBNull.Value) : new SqlParameter("@ReasonCd", _Logger.SelectedReasonCode);

            Parameters[7] = new SqlParameter("@ReminderDate", BaseClass.ConvertDatetimeDB(_Logger.ReminderDatetime));
            Parameters[8] = String.IsNullOrEmpty(_Logger.refDoc) ? new SqlParameter("@XrefDoc", DBNull.Value) : new SqlParameter("@XrefDoc", _Logger.refDoc);
            Parameters[9] = new SqlParameter("@ClsDate", DateConverterDB(_Logger.ClosedDate));
            Parameters[10] = String.IsNullOrEmpty(_Logger.SelectedEventSts) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _Logger.SelectedEventSts);
            Parameters[11] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);

            Parameters[12] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[12].Direction = ParameterDirection.ReturnValue;

            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebEventMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;

        }





        #endregion

        #region "Event Detail"
        public async Task<List<EventDetails>> GetEventDetaillist(EventDetails _eventDetails)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
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
            objDataEngine.CloseConnection();
            return EventDetailList;

        }

        public async Task<EventDetails> GetWebEventDetailSelect(EventDetails _eventDetails)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
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

                objDataEngine.CloseConnection();
                return _eventlogger;
            };
            return new EventDetails();
        }


        public async Task<MsgRetriever> SaveEventDetailMaint(EventDetails _EventDetails)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[6];
            Parameters[0] = new SqlParameter("@Module", "I");
            Parameters[1] = String.IsNullOrEmpty(_EventDetails.EventId) ? new SqlParameter("@EventId", DBNull.Value) : new SqlParameter("@EventId", _EventDetails.EventId);
            Parameters[2] = String.IsNullOrEmpty(_EventDetails.Description) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _EventDetails.Description);
            Parameters[3] = new SqlParameter("@ReminderDate", BaseClass.DateConverterDB(_EventDetails.ReminderDatetime));
            Parameters[4] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[5].Direction = ParameterDirection.ReturnValue;
            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebEventDetailMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;

        }





        #endregion

        #region "TxnPayment"
        public async Task<List<PaymentTxn>> GetPaymentTxnList(string AccNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AccNo);

            var execResult = await objDataEngine.ExecuteCommandAsync("WebTxnPaymentListSelect", CommandType.StoredProcedure, Parameters);
            var _paymentTxn = new List<PaymentTxn>();
            while (execResult.Read())
            {
                _paymentTxn.Add(new PaymentTxn
                {
                    SelectedTxnType = Convert.ToString(execResult["Txn Type"]),
                    AcctNo = Convert.ToString(execResult["Account No"]),
                    CardNo = Convert.ToString(execResult["Card No"]),
                    TxnDate = DateConverter(execResult["Txn Date"]),
                    TotAmnt = ConverterDecimal(execResult["Txn Amount"]),
                    displayTotAmnt = ConverterDecimal(execResult["Txn Amount"]),
                    BillingTxnAmt = ConverterDecimal(execResult["Billing Amount"]),
                    Descp = Convert.ToString(execResult["Txn Description"]),
                    StsDescp = Convert.ToString(execResult["Status"]),
                    SelectedSts = Convert.ToString(execResult["Sts"]),
                    UserId = Convert.ToString(execResult["User Id"]),
                    SelectedPyTxnCd = Convert.ToString(execResult["TxnCd"]),
                    PyTxnId = Convert.ToString(execResult["Txn Id"]),
                    WithHeldUnsettleId = Convert.ToInt32(execResult["WU Id"]),
                    CreationDate = DateConverter(execResult["Creation Date"]),


                });
            };
            return _paymentTxn;
        }

        public async Task<PaymentTxn> GetPaymentTxnDetail(PaymentTxn _PaymentTxn)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_PaymentTxn.PyTxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _PaymentTxn.PyTxnId);

            var execResult = await objDataEngine.ExecuteCommandAsync("WebTxnPaymentSelect", CommandType.StoredProcedure, Parameters);

            var _GetPaymentTxn = new PaymentTxn();
            while (execResult.Read())
            {
                _GetPaymentTxn.PyTxnId = Convert.ToString(execResult["TxnId"]);
                _GetPaymentTxn.TxnDate = DateConverter(execResult["TxnDate"]);
                _GetPaymentTxn.SelectedPyTxnCd = Convert.ToString(execResult["TxnCd"]);
                _GetPaymentTxn.CardNo = Convert.ToString(execResult["CardNo"]);
                _GetPaymentTxn.TotAmnt = ConverterDecimal(execResult["TxnAmt"]);
                _GetPaymentTxn.DueDt = DateConverter(execResult["DueDate"]);
                _GetPaymentTxn.BookingDt = DateConverter(execResult["BookingDate"]);
                _GetPaymentTxn.Totpts = ConverterDecimal(execResult["Pts"]);
                _GetPaymentTxn.Descp = Convert.ToString(execResult["Descp"]);
                _GetPaymentTxn.SelectedAppvCd = Convert.ToString(execResult["AppvCd"]);
                _GetPaymentTxn.SelectedSts = Convert.ToString(execResult["Sts"]);
                _GetPaymentTxn.UserId = Convert.ToString(execResult["UserId"]);
                _GetPaymentTxn.WithHeldUnsettleId = Convert.ToInt32(execResult["WithheldUnsettleId"]);
                _GetPaymentTxn.CheqNo = Convert.ToString(execResult["ChequeNo"]);
                _GetPaymentTxn.CreationDate = DateConverter(execResult["CreationDate"]);

            };
            return _GetPaymentTxn;

        }
        public async Task<MsgRetriever> SavePaymentTxn(PaymentTxn _PaymentTxn)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[18];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_PaymentTxn.PyTxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _PaymentTxn.PyTxnId);
            Parameters[2] = String.IsNullOrEmpty(_PaymentTxn.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _PaymentTxn.AcctNo);
            Parameters[3] = String.IsNullOrEmpty(_PaymentTxn.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _PaymentTxn.CardNo);
            Parameters[4] = String.IsNullOrEmpty(_PaymentTxn.SelectedPyTxnCd) ? new SqlParameter("@TxnCd", DBNull.Value) : new SqlParameter("@TxnCd", _PaymentTxn.SelectedPyTxnCd);
            Parameters[5] = new SqlParameter("@TxnDate", ConvertDatetimeDB(_PaymentTxn.TxnDate));
            Parameters[6] = new SqlParameter("@BookingDate", ConvertDatetimeDB(_PaymentTxn.BookingDt));
            Parameters[7] = new SqlParameter("@DueDate", ConvertDatetimeDB(_PaymentTxn.DueDt));
            Parameters[8] = new SqlParameter("@TxnAmt", ConvertDecimalToDb(_PaymentTxn.TotAmnt));
            Parameters[9] = new SqlParameter("@Pts", ConvertDecimalToDb(_PaymentTxn.Totpts));
            Parameters[10] = String.IsNullOrEmpty(_PaymentTxn.Descp) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _PaymentTxn.Descp);
            Parameters[11] = String.IsNullOrEmpty(_PaymentTxn.SelectedAppvCd) ? new SqlParameter("@AppvCd", DBNull.Value) : new SqlParameter("@AppvCd", _PaymentTxn.SelectedAppvCd);
            Parameters[12] = new SqlParameter("@CheqNo", ConvertIntToDb(_PaymentTxn.CheqNo));
            Parameters[13] = String.IsNullOrEmpty(_PaymentTxn.SelectedSts) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _PaymentTxn.SelectedSts);
            Parameters[14] = new SqlParameter("@RcptNo", SqlDbType.VarChar, 19);
            Parameters[14].Direction = ParameterDirection.Output;
            Parameters[15] = new SqlParameter("@RetCd", SqlDbType.VarChar, 19);
            Parameters[15].Direction = ParameterDirection.Output;
            Parameters[16] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[17] = new SqlParameter("@RETURN_VALUE", SqlDbType.VarChar, 19);
            Parameters[17].Direction = ParameterDirection.ReturnValue;
            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebTxnPaymentMaint", CommandType.StoredProcedure, Parameters);
            var Result = ConvertInt(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);
            return Descp;

        }
        #endregion

        #region"BillingItem"
        public async Task<List<BillingItem>> GetBillingItemTxnList(int TxnId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
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
            objDataEngine.CloseConnection();
            return _bi;
        }
        public async Task<List<BillingItem>> GetBillingItemSettlementList(int TxnId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
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
            objDataEngine.CloseConnection();
            return _bi;
        }
        public async Task<List<BillingItem>> SearchBillingItem(BillingItem bi)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[6];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(bi._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", bi._CardnAccNo.AccNo);
            Parameters[2] = new SqlParameter("@FromDate", DateConverterDB(bi.FromDate));
            Parameters[3] = new SqlParameter("@ToDate", DateConverterDB(bi.ToDate));
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
            objDataEngine.CloseConnection();
            return _bi;
        }
        #endregion
        #region "AcctDepositInfo"
        public async Task<List<CreditAssesOperation>> GetAcctDepositInfoList(string applid = null, string acctNo = null, string corpCd = null)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[4];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(applid) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", applid);
            Parameters[2] = String.IsNullOrEmpty(acctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", acctNo);
            Parameters[3] = String.IsNullOrEmpty(corpCd) ? new SqlParameter("@CorpCd", DBNull.Value) : new SqlParameter("@CorpCd", corpCd);
            var execResult = await objDataEngine.ExecuteCommandAsync("WebAcctDepositInfoListSelect", CommandType.StoredProcedure, Parameters);
            var _acctDeptInfo = new List<CreditAssesOperation>();
            while (execResult.Read())
            {
                _acctDeptInfo.Add(new CreditAssesOperation
                {
                    SelectedDirectDebitInd = Convert.ToString(execResult["DirectDebitInd"]),
                    SelectedDepositType = Convert.ToString(execResult["DepositType"]),
                    //  SelectedBankAcctType = Convert.ToString(execResult["BankAcctType"]),
                    SelectedBankName = Convert.ToString(execResult["BankName"]),
                    //    BankAcctNo = Convert.ToString(execResult["BankAcctNo"]),
                    DepositAmt = ConverterDecimal(execResult["DepositAmt"]),
                    Txnid = Convert.ToString(execResult["TxnId"]),
                    UserId = Convert.ToString(execResult["UserId"]),
                    Creationdt = Convert.ToString(execResult["CreationDate"]),
                    DepositFromDate = DateConverter(execResult["DepositFromDate"]),
                    DepositToDate = DateConverter(execResult["DepositToDate"]),
                    BgSerialNo = Convert.ToString(execResult["BGSerialNo"])


                });
            };
            return _acctDeptInfo;
        }
        public async Task<CreditAssesOperation> GetGetAcctDepositInfoDetail(string TxnId, string applid = null, string acctNo = null, string corpCd = null)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
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
            };
            return _GetAcctDeptInfo;

        }
        public async Task<MsgRetriever> SaveAcctDepositInfo(CreditAssesOperation _AcctDeptInfo, string acctNo = null, string applId = null, string corpCd = null)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[19];
            SqlCommand cmd = new SqlCommand();

            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(applId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", applId);
            Parameters[2] = String.IsNullOrEmpty(acctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", acctNo);
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
            Parameters[18] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[18].Direction = ParameterDirection.ReturnValue;

            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebAcctDepositInfoMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;


        }





        #endregion

        #region "Acct Subsidy"

        public async Task<List<SKDS>> GetAcctSubsidyInfoList(string acctNo = null, string skdsNo = null)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
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
                });
            };
            return SkdsSearchInfo;
        }


        public async Task<MsgRetriever> SaveAcctSubsidyTag(List<SKDS> skds, string AcctNo, string SKDSNo)
        {

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("VehRegsNo");
            dataTable.Columns.Add("CardNo");
            dataTable.Columns.Add("Sts");
            //dataTable.Columns.Add("SKDSNo");
            //dataTable.Columns.Add("EffFromDate");
            //dataTable.Columns.Add("EffToDate");

            DataRow dr = dataTable.NewRow();
            for (int i = 0; i < skds.Count; i++)
            {
                dr["VehRegsNo"] = skds[i].VehRegsNo;
                dr["CardNo"] = skds[i].CardNo;
                dr["Sts"] = skds[i].SelectedSts;
                //dr["SKDSNo"] = skds[i].SKDSNo;
                //dr["EffFromDate"] = skds[i].EffFromDate;
                //dr["EffToDate"] = skds[i].EffToDate;
                dataTable.Rows.Add(dr);
                dr = dataTable.NewRow();
            }

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[6];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
            Parameters[2] = String.IsNullOrEmpty(SKDSNo) ? new SqlParameter("@SKDSNo", DBNull.Value) : new SqlParameter("@SKDSNo", SKDSNo);
            Parameters[3] = new SqlParameter("@Tbl", dataTable);
            Parameters[4] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[4].Direction = ParameterDirection.ReturnValue;
            Parameters[5] = new SqlParameter("@UserId", System.Web.HttpContext.Current.User.Identity.Name);
            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebAcctSubsidyTagMaint", CommandType.StoredProcedure, Parameters);
            var Result = ConvertInt(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);
            return Descp;

        }
        #endregion


        public async Task<List<FinancilInfoItemsList>> TxnInstantListSelect(string AcctNo)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);

            var execResult = await objDataEngine.ExecuteCommandAsync("[WebTxnInstantListSelect]", CommandType.StoredProcedure, Parameters);

            var _FinInfoList = new List<FinancilInfoItemsList>();
            while (execResult.Read())
            {

                _FinInfoList.Add(new FinancilInfoItemsList
                {
                    TxnId = Convert.ToString(execResult["TxnId"]),
                    Descp = Convert.ToString(execResult["Descp"]),
                    TxnDate = Convert.ToString(execResult["TxnDate"]),
                    DueDate = Convert.ToString(execResult["DueDate"]),
                    BookingDate = Convert.ToString(execResult["BookingDate"]),
                    CardNo = Convert.ToString(execResult["CardNo"]),
                    CreationDate = Convert.ToString(execResult["CreationDate"]),
                    DriverCardNo = Convert.ToString(execResult["Driver CardNo"]),
                    Lbe = Convert.ToString(execResult["LBE"]),
                    RcptNo = Convert.ToString(execResult["RcptNo"]),
                    ShortDescp = Convert.ToString(execResult["ShortDescp"]),
                    SiteId = Convert.ToString(execResult["SiteId"]),
                    UserId = Convert.ToString(execResult["UserId"]),
                    TxnAmt = Convert.ToDecimal(execResult["TxnAmt"]).ToString("0.00"),
                    TxnCd = Convert.ToString(execResult["TxnCd"])
                });
            };
            objDataEngine.CloseConnection();
            return _FinInfoList;

        }


        public async Task<List<FinancilInfoItemsList>> TxnInstantUnpostedTxnList(string AcctNo)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);

            var execResult = await objDataEngine.ExecuteCommandAsync("[WebTxnUnpostedListSelect]", CommandType.StoredProcedure, Parameters);

            var _FinInfoList = new List<FinancilInfoItemsList>();
            while (execResult.Read())
            {

                _FinInfoList.Add(new FinancilInfoItemsList
                {
                    TxnId = Convert.ToString(execResult["TxnId"]),
                    Descp = Convert.ToString(execResult["Descp"]),
                    TxnDate = Convert.ToString(execResult["TxnDate"]),
                    DueDate = Convert.ToString(execResult["DueDate"]),
                    BookingDate = Convert.ToString(execResult["BookingDate"]),
                    CardNo = Convert.ToString(execResult["CardNo"]),
                    CreationDate = Convert.ToString(execResult["CreationDate"]),
                    Lbe = Convert.ToString(execResult["LBE"]),
                    ShortDescp = Convert.ToString(execResult["ShortDescp"]),
                    UserId = Convert.ToString(execResult["UserId"]),
                    TxnAmt = ConverterDecimal(execResult["BillingTxnAmt"]),
                    TxnCd = Convert.ToString(execResult["TxnCd"])
                });
            };
            objDataEngine.CloseConnection();
            return _FinInfoList;
        }
        #region "Account Users"
        public async Task<List<AccountUser>> GetAccountUsers(string AcctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);
            objDataEngine.InitiateConnection();
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("AcctNo", DBNull.Value) : new SqlParameter("AcctNo", AcctNo);

            var execResult = await objDataEngine.ExecuteCommandAsync("WebUserPrivilegeListSelect", CommandType.StoredProcedure, parameters);
            var _userList = new List<AccountUser>();
            while (execResult.Read())
            {
                _userList.Add(new AccountUser
                {
                    PrivilegeCd = Convert.ToString(execResult["PrivilegeCd"]),
                    Username = Convert.ToString(execResult["EmailAddr"]),
                    Status = Convert.ToString(execResult["sts"])
                });
            }
            objDataEngine.CloseConnection();
            return _userList;
        }
        public async Task<MsgRetriever> RensendActivationEmail(ResendAccountMail _Resend)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Web);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[6];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = new SqlParameter("@Name", DBNull.Value);
            Parameters[2] = String.IsNullOrEmpty(_Resend.UserId) ? new SqlParameter("@Email", DBNull.Value) : new SqlParameter("@Email", _Resend.UserId);
            Parameters[3] = String.IsNullOrEmpty(_Resend.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _Resend.AcctNo);
            Parameters[4] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@CreatedBy", DBNull.Value) : new SqlParameter("@CreatedBy", this.GetUserId);
            Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[5].Direction = ParameterDirection.ReturnValue;

            var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebCheckUserMaint‏", CommandType.StoredProcedure, Parameters);
            var Result = ConvertInt(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return Descp;

        }




        public async Task<MsgRetriever> ResetPasswordCounter(string AcctNo, string UserId)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Web);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = new SqlParameter("@UserId", UserId);
            Parameters[1] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[1].Direction = ParameterDirection.ReturnValue;

            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebResetUserCountSelect‏‏‏", CommandType.StoredProcedure, Parameters);
            var Result = ConvertInt(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return Descp;
        }


        #endregion



        public async Task<MsgRetriever> WebResendToken(string AdminEmail)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Web);
            objDataEngine.InitiateConnection();

            var token = this.AutoHashing(this.PasswordGenerator()).Replace(" ", "");

            SqlParameter[] Parameters = new SqlParameter[4];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(AdminEmail) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", AdminEmail);
            Parameters[2] = new SqlParameter("@Token", token);
            Parameters[3] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[3].Direction = ParameterDirection.ReturnValue;

            var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebAcctResendToken", CommandType.StoredProcedure, Parameters);
            var Result = ConvertInt(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return Descp;

        }

        #region "Product Discount"


        public async Task<List<ProductDiscount>> WebProductDiscountListSelect(string AcctNo, string DiscType)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
            Parameters[1] = String.IsNullOrEmpty(DiscType) ? new SqlParameter("@ProdDiscType", DBNull.Value) : new SqlParameter("@ProdDiscType", DiscType);

            var execResult = await objDataEngine.ExecuteCommandAsync("WebProductDiscountListSelect", CommandType.StoredProcedure, Parameters);

            var _ProductDiscount = new List<ProductDiscount>();
            while (execResult.Read())
            {

                _ProductDiscount.Add(new ProductDiscount
                {
                    Id = Convert.ToString(execResult["Id"]),
                    SelectedProdCd = Convert.ToString(execResult["ProdCd"]),
                    SelectedProdDiscType = Convert.ToString(execResult["ProdDiscType"]),
                    ProdDiscDescp = Convert.ToString(execResult["ProdDiscDescp"]),
                    SelectedPlanId = Convert.ToString(execResult["PlanId"]),
                    EffDateFrom = DateConverter(execResult["EffDate"]),
                    CreatedBy = Convert.ToString(execResult["UserId"]),
                    CreationDate = DateConverter(execResult["CreationDate"]),
                    ProdCdDescp = Convert.ToString(execResult["ProductDescp"]),
                    Remarks = Convert.ToString(execResult["Remarks"])
                });
            };
            objDataEngine.CloseConnection();
            return _ProductDiscount;

        }

        public async Task<ProductDiscount> WebProductDiscountSelect(string AcctNo, string DiscType, string Id)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
            Parameters[1] = String.IsNullOrEmpty(DiscType) ? new SqlParameter("@ProdDiscType", DBNull.Value) : new SqlParameter("@ProdDiscType", DiscType);
            Parameters[2] = String.IsNullOrEmpty(Id) ? new SqlParameter("@Id", DBNull.Value) : new SqlParameter("@Id", ConvertLongToDb(Id));
            var execResult = await objDataEngine.ExecuteCommandAsync("WebProductDiscountSelect", CommandType.StoredProcedure, Parameters);
            var _ProductDiscount = new ProductDiscount();
            while (execResult.Read())
            {
                _ProductDiscount = new ProductDiscount
                {
                    SelectedProdCd = Convert.ToString(execResult["ProdCd"]),
                    EffDateFrom = DateConverter(execResult["EffDate"]),
                    SelectedPlanId = Convert.ToString(execResult["PlanId"]),
                    CreatedBy = Convert.ToString(execResult["UserId"]),
                    CreationDate = DateConverter(execResult["CreationDate"]),
                    Remarks = Convert.ToString(execResult["Remarks"]),
                  //  CompanyName = Convert.ToString(execResult["CmpyName"])
                };
            };
            return _ProductDiscount;
        }

        public async Task<MsgRetriever> ProductDiscountMaint(ProductDiscount _Discount, string AcctNo, string Func)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[10];
            SqlCommand cmd = new SqlCommand();
            if (Func == "N")
            {
                Parameters[0] = new SqlParameter("@Id", DBNull.Value);
                Parameters[1] = new SqlParameter("@flag", "N");
            }
            else
            {
                Parameters[0] = String.IsNullOrEmpty(_Discount.Id) ? new SqlParameter("@Id", DBNull.Value) : new SqlParameter("@Id", ConvertIntToDb(_Discount.Id));
                Parameters[1] = new SqlParameter("@flag", "E");
            }

            Parameters[2] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
            Parameters[3] = String.IsNullOrEmpty(_Discount.SelectedProdDiscType) ? new SqlParameter("@ProdDiscType", DBNull.Value) : new SqlParameter("@ProdDiscType", _Discount.SelectedProdDiscType);
            Parameters[4] = String.IsNullOrEmpty(_Discount.SelectedProdCd) ? new SqlParameter("@ProdCd", DBNull.Value) : new SqlParameter("@ProdCd", _Discount.SelectedProdCd);
            Parameters[5] = String.IsNullOrEmpty(_Discount.EffDateFrom) ? new SqlParameter("@EffDate", DBNull.Value) : new SqlParameter("@EffDate", ConvertDatetimeDB(_Discount.EffDateFrom));
            Parameters[6] = String.IsNullOrEmpty(_Discount.SelectedPlanId) ? new SqlParameter("@PlanId", DBNull.Value) : new SqlParameter("@PlanId", _Discount.SelectedPlanId);
            Parameters[7] = String.IsNullOrEmpty(_Discount.Remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", _Discount.Remarks);
            Parameters[8] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);

            Parameters[9] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[9].Direction = ParameterDirection.ReturnValue;

            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebProductDiscountMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;

        }

        public async Task<MsgRetriever> ProductDiscountDelete(ProductDiscount _Discount, string AcctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[7];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = String.IsNullOrEmpty(_Discount.Id) ? new SqlParameter("@Id", DBNull.Value) : new SqlParameter("@Id", ConvertLongToDb(_Discount.Id));
            Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
            Parameters[2] = String.IsNullOrEmpty(_Discount.SelectedProdDiscType) ? new SqlParameter("@ProdDiscType", DBNull.Value) : new SqlParameter("@ProdDiscType", _Discount.SelectedProdDiscType);
            Parameters[3] = String.IsNullOrEmpty(_Discount.SelectedProdCd) ? new SqlParameter("@ProdCd", DBNull.Value) : new SqlParameter("@ProdCd", _Discount.SelectedProdCd);
            Parameters[4] = String.IsNullOrEmpty(_Discount.EffDateFrom) ? new SqlParameter("@EffDate", DBNull.Value) : new SqlParameter("@EffDate", ConvertDatetimeDB(_Discount.EffDateFrom));
            Parameters[5] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
            Parameters[6] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[6].Direction = ParameterDirection.ReturnValue;

            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebProductDiscountDelete", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return Descp;


        }


        #endregion

        #region "Points Adjustment"

        public async Task<List<PointAdjustment>> WebPointAdjustmentListSelect(string AcctNo)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
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
                    CardNo = Convert.ToString(execResult["Card No"]),
                    Points = ConverterDecimal(execResult["Points"]),
                    TxnDate = DateTimeConverter(execResult["Txn Date"]),
                    TxnDescription = Convert.ToString(execResult["Txn Description"]),
                    SelectedStatus = Convert.ToString(execResult["Status"]),
                    SelectedTxnCd = Convert.ToString(execResult["TxnCd"]),
                    WUId = Convert.ToString(execResult["WU Id"]),
                    CreationDate = DateConverter(execResult["Creation Date"]),
                    TxnId = Convert.ToString(execResult["Txn Id"])
                });
            };
            objDataEngine.CloseConnection();
            return _PointAdjustment;

        }

        public async Task<PointAdjustment> WebPointAdjustmentSelect(string TxnId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
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
        public async Task<MsgRetriever> WebPointAdjustmentMaint(PointAdjustment _Adj, string AcctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
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
            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebPointAdjustmentMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return Descp;
        }
        #endregion
        #region"Cost Centre"

        public async Task<List<CostCentre>> WebCostCentreListSelect(CostCentre CostCentre)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = String.IsNullOrEmpty(CostCentre.RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", CostCentre.RefTo);
            Parameters[1] = String.IsNullOrEmpty(CostCentre.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", CostCentre.RefKey);
            Parameters[2] = new SqlParameter("@IssNo", GetIssNo);

            var execResult = await objDataEngine.ExecuteCommandAsync("[WebCostCentreListSelect]", CommandType.StoredProcedure, Parameters);

            var _CostCentre = new List<CostCentre>();
            while (execResult.Read())
            {
                _CostCentre.Add(new CostCentre
                {
                    SelectedCostCentre = Convert.ToString(execResult["CostCentre"]),
                    Descp = Convert.ToString(execResult["Descp"]),
                    PersoninCharge = Convert.ToString(execResult["PersoninCharge"])
                });
            };
            objDataEngine.CloseConnection();
            return _CostCentre;
        }

        public async Task<List<CostCentre>> WebCostCentreSearch(CostCentre CostCentre)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[4];
            Parameters[0] = String.IsNullOrEmpty(CostCentre.RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", CostCentre.RefTo);
            Parameters[1] = String.IsNullOrEmpty(CostCentre.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", CostCentre.RefKey);
            Parameters[2] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[3] = String.IsNullOrEmpty(CostCentre.SelectedCostCentre) ? new SqlParameter("@CostCentre", DBNull.Value) : new SqlParameter("@CostCentre", CostCentre.SelectedCostCentre);


            var execResult = await objDataEngine.ExecuteCommandAsync("[WebCostCentreSearch]", CommandType.StoredProcedure, Parameters);
            var _CostCentre = new List<CostCentre>();
            while (execResult.Read())
            {
                _CostCentre.Add(new CostCentre{
                SelectedCostCentre = Convert.ToString(execResult["CostCentre"]),
                Descp = Convert.ToString(execResult["Descp"]),
                PersoninCharge = Convert.ToString(execResult["PersoninCharge"]),
                });
            };
            return _CostCentre;
        }


        public async Task<CostCentre> WebCostCentreSelect(CostCentre CostCentre)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[4];
            Parameters[0] = String.IsNullOrEmpty(CostCentre.RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", CostCentre.RefTo);
            Parameters[1] = String.IsNullOrEmpty(CostCentre.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", CostCentre.RefKey);
            Parameters[2] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[3] = String.IsNullOrEmpty(CostCentre.SelectedCostCentre) ? new SqlParameter("@CostCentre", DBNull.Value) : new SqlParameter("@CostCentre", CostCentre.SelectedCostCentre);

            var execResult = await objDataEngine.ExecuteCommandAsync("[WebCostCentreSelect]", CommandType.StoredProcedure, Parameters);
            var _CostCentre = new CostCentre();
            while (execResult.Read())
            {
                    _CostCentre.SelectedCostCentre = Convert.ToString(execResult["CostCentre"]);
                    _CostCentre.Descp = Convert.ToString(execResult["Descp"]);
                    _CostCentre.PersoninCharge = Convert.ToString(execResult["PersoninCharge"]);
            };
            return _CostCentre;
        }
        public async Task<MsgRetriever> WebCostCentreMaint(CostCentre _CostCentre)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[7];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_CostCentre.RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", _CostCentre.RefTo);
            Parameters[2] = String.IsNullOrEmpty(_CostCentre.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@refKey", _CostCentre.RefKey);
            Parameters[3] = String.IsNullOrEmpty(_CostCentre.SelectedCostCentre) ? new SqlParameter("@CostCentre", DBNull.Value) : new SqlParameter("@CostCentre", _CostCentre.SelectedCostCentre);
            Parameters[4] = String.IsNullOrEmpty(_CostCentre.Descp) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _CostCentre.Descp);
            Parameters[5] = String.IsNullOrEmpty(_CostCentre.PersoninCharge) ? new SqlParameter("@PersonInCharge", DBNull.Value) : new SqlParameter("@PersonInCharge", _CostCentre.PersoninCharge);
            Parameters[6] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[6].Direction = ParameterDirection.ReturnValue;
            var Cmd =await objDataEngine.ExecuteWithReturnValueAsync("WebCostCentreMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp =await GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return Descp;
        }
        #endregion
    }
}