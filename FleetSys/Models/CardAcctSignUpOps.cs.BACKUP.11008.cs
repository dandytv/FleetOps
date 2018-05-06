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
using System.IO;
namespace FleetOps.Models
{
    public class CardAcctSignUpOps : BaseClass
    {
        public string ApplId { get; set; }
        public string EntityId { get; set; }
        public string DocPath { get; set; }

        #region "Application General Info"
        public List<AcctSignUp> GetAcctSignUpList(string _ApplicationId, string page)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = new SqlParameter("@ApplId", DBNull.Value);
            Parameters[2] = new SqlParameter("@Page", DBNull.Value);


            var execResult = objDataEngine.ExecuteCommand("WebApplListSelect", CommandType.StoredProcedure, Parameters);
            var _AcctSignUpList = new List<AcctSignUp>();

            while (execResult.Read())
            {
                _AcctSignUpList.Add(new AcctSignUp
                {
                    ApplicationId = Convert.ToString(execResult["ApplId"]),
                    AcctNo = Convert.ToString(execResult["Account No"]),
                    CompanyName = Convert.ToString(execResult["Company Name"]),
                    //CorporateAcct = BaseClass.WebGetRefLib("CorpCd"),
                    SelectedCorporateAcct = Convert.ToString(execResult["Corporate Account"]),
                    ApplicationRef = Convert.ToString(execResult["ApplRef"]),
                    CreditLimit = BaseClass.ConverterDecimal(execResult["Credit Limit"]),
                    ShownCreditLimit = ConverterDecimal(execResult["Credit Limit"]),
                    PendingReasons = Convert.ToString(execResult["Pending Reason"]),
                    ApprovedDate = DateConverter(execResult["Approved Date"]),
                    ReceiveDate = DateConverter(execResult["Received Date"]),
                    RejectedDate = DateConverter(execResult["Rejected Date"]),

                    CreationDatenUserid = new CreationDatenUserId
                    {
                        CreationDate = Convert.ToString(execResult["Creation Date"]),
                        UserId = Convert.ToString(execResult["User Id"]),
                    }
                });
            };
            objDataEngine.CloseConnection();
            return _AcctSignUpList;
        }
        public AcctSignUp GetApplicationGeneralInfo(string _applid)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = new SqlParameter("@ApplId", _applid);
            var Reader = objDataEngine.ExecuteCommand("WebApplGeneralInfoSelect", CommandType.StoredProcedure, Parameters);
            while (Reader.Read())
            {
                var _AcctSignUp = new AcctSignUp
                {
                    CycleNo = BaseClass.WebGetCycle("I"),
                    SelectedCycleNo = Convert.ToString(Reader["CycNo"]),
                    PlasticType = BaseClass.WebPlasticTypeSelect(),
                    selectedPlasticType = Convert.ToString(Reader["PlasticType"]),
                    AcctNo = Convert.ToString(Reader["AcctNo"]),
                    CorporateAcct = BaseClass.WebGetCorpCd(),
                    SelectedCorporateAcct = Convert.ToString(Reader["CorpCd"]),
                    ApplicationRef = Convert.ToString(Reader["Reference"]),
                    CompanyLegalName = Convert.ToString(Reader["CmpyLegalName"]),
                    CompanyName = Convert.ToString(Reader["CmpyName"]),
                    CompanyEmbName = Convert.ToString(Reader["CmpyEmbName"]),
                    ContactPerson = Convert.ToString(Reader["ContactPerson"]),
                    Position = BaseClass.WebGetRefLib("Occupation"),
                    selectedPosition = Convert.ToString(Reader["Position"]),
                    OfficePhone = Convert.ToString(Reader["OfficePhone"]),
                    MobileNo = Convert.ToString(Reader["MobileNo"]),
                    OfficeFax = Convert.ToString(Reader["OfficeFax"]),
                    emailAddress = Convert.ToString(Reader["EmailAddr"]),
                    CompanyType = BaseClass.WebGetRefLib("CmpyType"),
                    selectedCompanyType = Convert.ToString(Reader["CmpyType"]),
                    CompanyRegnNo = Convert.ToString(Reader["CmpyRegsNo"]),
                    CompanyRegnDate = DateConverter(Reader["CmpyRegsDate"]),
                    NatureOfBusiness = Convert.ToString(Reader["NatureOfBusn"]),
                    BillingType = BaseClass.WebGetRefLib("BillingType"),
                    SelectedBillingType = Convert.ToString(Reader["BillMethod"]),
                    InvoicePref = BaseClass.WebGetRefLib("InvPrefer"),
                    SelectedInvoicePref = Convert.ToString(Reader["InvoicePref"]),
                    LoyaltyCardNo = Convert.ToString(Reader["LoyaltyCardNo"]),
                    LoyaltyFullName = Convert.ToString(Reader["LoyaltyFullName"]),
                    LoyaltyIcNo = Convert.ToString(Reader["LoyaltyIcNo"]),
                    LoyaltyContactNo = Convert.ToString(Reader["LoyaltyContactNo"]),
                    LoyaltyeBusn = BaseClass.BoolConverter(Reader["LoyaltyeBusn"]),
                    BusinessCategory = BaseClass.WebGetRefLib("BusnCategory"),
                    SelectedBusinessCategory = Convert.ToString(Reader["BusnCategory"]),
                    EntityId = Convert.ToString(Reader["EntityId"]),
                    Files = Convert.ToString(Reader["DocPath"]),
                    InvoiceBillingIndicator = BaseClass.BoolConverter(Reader["InvBillInd"]),
                    PymtInd = BaseClass.BoolConverter(Reader["PymtInd"]),
                    //InvoiceIndCopy = BaseClass.BoolConverter(Reader["InvoiceCopyInd"]),
                    VehiclePerformanceReportInd = BaseClass.BoolConverter(Reader["VehPerfRptInd"]),

                };

                objDataEngine.CloseConnection();
                return _AcctSignUp;

            }
            objDataEngine.CloseConnection();
            return new AcctSignUp();
        }
        public MsgRetriever SaveApplicationGeneralInfoResult(AcctSignUp acctSU)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[37];
            SqlCommand cmd = new SqlCommand();
            if (!Directory.Exists(ConfigurationManager.AppSettings["ApplicationDirectory"]))
            {
                Directory.CreateDirectory(ConfigurationManager.AppSettings["ApplicationDirectory"]);
            }
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(acctSU.selectedPlasticType) ? new SqlParameter("@PlasticType", DBNull.Value) : new SqlParameter("@PlasticType", acctSU.selectedPlasticType);
            Parameters[2] = String.IsNullOrEmpty(acctSU.SelectedCycleNo) ? new SqlParameter("@CycNo", DBNull.Value) : new SqlParameter("@CycNo", acctSU.SelectedCycleNo);
            Parameters[3] = String.IsNullOrEmpty(acctSU.ApplicationId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", acctSU.ApplicationId);
            Parameters[4] = String.IsNullOrEmpty(acctSU.AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", acctSU.AcctNo);

            Parameters[5] = String.IsNullOrEmpty(acctSU.SelectedCorporateAcct) ? new SqlParameter("@CorpCd", DBNull.Value) : new SqlParameter("@CorpCd", acctSU.SelectedCorporateAcct);
            Parameters[6] = String.IsNullOrEmpty(acctSU.ApplicationRef) ? new SqlParameter("@ApplRef ", DBNull.Value) : new SqlParameter("@ApplRef ", acctSU.ApplicationRef);
            Parameters[7] = String.IsNullOrEmpty(acctSU.CompanyLegalName) ? new SqlParameter("@CmpyLegalName", DBNull.Value) : new SqlParameter("@CmpyLegalName", acctSU.CompanyLegalName);
            Parameters[8] = String.IsNullOrEmpty(acctSU.CompanyName) ? new SqlParameter("@CmpyName", DBNull.Value) : new SqlParameter("@CmpyName", acctSU.CompanyName);
            Parameters[9] = String.IsNullOrEmpty(acctSU.CompanyEmbName) ? new SqlParameter("@CmpyEmbName", DBNull.Value) : new SqlParameter("@CmpyEmbName", acctSU.CompanyEmbName);

            Parameters[10] = String.IsNullOrEmpty(acctSU.ContactPerson) ? new SqlParameter("@ContactPerson", DBNull.Value) : new SqlParameter("@ContactPerson", acctSU.ContactPerson);
            Parameters[11] = String.IsNullOrEmpty(acctSU.selectedPosition) ? new SqlParameter("@Position", DBNull.Value) : new SqlParameter("@Position", acctSU.selectedPosition);
            Parameters[12] = String.IsNullOrEmpty(acctSU.SelectedBusinessCategory) ? new SqlParameter("@BusnCategory", DBNull.Value) : new SqlParameter("@BusnCategory", acctSU.SelectedBusinessCategory);
            Parameters[13] = String.IsNullOrEmpty(acctSU.OfficePhone) ? new SqlParameter("@OfficePhone", DBNull.Value) : new SqlParameter("@OfficePhone", acctSU.OfficePhone);
            Parameters[14] = String.IsNullOrEmpty(acctSU.MobileNo) ? new SqlParameter("@MobileNo", DBNull.Value) : new SqlParameter("@MobileNo", acctSU.MobileNo);

            Parameters[15] = String.IsNullOrEmpty(acctSU.OfficeFax) ? new SqlParameter("@OfficeFax", DBNull.Value) : new SqlParameter("@OfficeFax", acctSU.OfficeFax);
            Parameters[16] = String.IsNullOrEmpty(acctSU.emailAddress) ? new SqlParameter("@EmailAddr", DBNull.Value) : new SqlParameter("@EmailAddr", acctSU.emailAddress);
            Parameters[17] = String.IsNullOrEmpty(acctSU.selectedCompanyType) ? new SqlParameter("@CmpyType", DBNull.Value) : new SqlParameter("@CmpyType", acctSU.selectedCompanyType);
            Parameters[18] = String.IsNullOrEmpty(acctSU.CompanyRegnNo) ? new SqlParameter("@CmpyRegsNo", DBNull.Value) : new SqlParameter("@CmpyRegsNo", acctSU.CompanyRegnNo);
            Parameters[19] = new SqlParameter("@CmpyRegsDate", BaseClass.DateConverterDB(acctSU.CompanyRegnDate));

            Parameters[20] = String.IsNullOrEmpty(acctSU.NatureOfBusiness) ? new SqlParameter("@NatureOfBusn", DBNull.Value) : new SqlParameter("@NatureOfBusn", acctSU.NatureOfBusiness);
            Parameters[21] = String.IsNullOrEmpty(acctSU.SelectedBillingType) ? new SqlParameter("@BillingMethod", DBNull.Value) : new SqlParameter("@BillingMethod", acctSU.SelectedBillingType);
            Parameters[22] = String.IsNullOrEmpty(acctSU.SelectedInvoicePref) ? new SqlParameter("@InvoicePref", DBNull.Value) : new SqlParameter("@InvoicePref", acctSU.SelectedInvoicePref);
            Parameters[23] = new SqlParameter("@InvBillInd ", ConvertBoolDB(acctSU.InvoiceBillingIndicator));
            Parameters[24] = new SqlParameter("@PymtInd", ConvertBoolDB(acctSU.PymtInd));
            Parameters[25] = new SqlParameter("@VehPerfRptInd ", ConvertBoolDB(acctSU.VehiclePerformanceReportInd));
            Parameters[26] = String.IsNullOrEmpty(acctSU.LoyaltyCardNo) ? new SqlParameter("@LoyaltyCardNo", DBNull.Value) : new SqlParameter("@LoyaltyCardNo", acctSU.LoyaltyCardNo);
            Parameters[27] = String.IsNullOrEmpty(acctSU.LoyaltyFullName) ? new SqlParameter("@LoyaltyFullName", DBNull.Value) : new SqlParameter("@LoyaltyFullName", acctSU.LoyaltyFullName);
            Parameters[28] = String.IsNullOrEmpty(acctSU.LoyaltyIcNo) ? new SqlParameter("@LoyaltyIcNo ", DBNull.Value) : new SqlParameter("@LoyaltyIcNo ", acctSU.LoyaltyIcNo);
            Parameters[29] = String.IsNullOrEmpty(acctSU.LoyaltyContactNo) ? new SqlParameter("@LoyaltyContactNo ", DBNull.Value) : new SqlParameter("@LoyaltyContactNo ", acctSU.LoyaltyContactNo);
            Parameters[30] = new SqlParameter("@LoyaltyeBusn", ConvertBoolDB(acctSU.LoyaltyeBusn));
            Parameters[31] = String.IsNullOrEmpty(acctSU.EntityId) ? new SqlParameter("@EntityId ", DBNull.Value) : new SqlParameter("@EntityId", acctSU.EntityId);
            Parameters[32] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[33] = new SqlParameter("oApplId", SqlDbType.VarChar, 19);
            Parameters[33].Direction = ParameterDirection.Output;

            Parameters[34] = new SqlParameter("oEntityId", SqlDbType.VarChar, 19);
            Parameters[34].Direction = ParameterDirection.Output;
            Parameters[35] = new SqlParameter("@DocPath", SqlDbType.VarChar, 150);
            Parameters[35].Direction = ParameterDirection.Output;
            Parameters[36] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[36].Direction = ParameterDirection.ReturnValue;


            var Cmd = objDataEngine.ExecuteWithReturnValue("WebApplGeneralInfoMaint", CommandType.StoredProcedure, Parameters);
            var Result = ConvertInt(Cmd.Parameters["@RETURN_VALUE"].Value);
            this.ApplId = Convert.ToString(Cmd.Parameters[33].Value);
            this.EntityId = Convert.ToString(Cmd.Parameters[34].Value);
            this.DocPath = Convert.ToString(Cmd.Parameters[35].Value);
            var Descp = GetMessageCode(Result);

            if (Descp.flag == 0)
            {
                if (Descp.desp.Contains("updated"))
                {
                    Descp.desp = Descp.desp;
                }
                else
                {
                    string tempObj = this.ApplId;
                    int temInt = Descp.flag;
                    if (this.AutoGenerateFolder(temInt, this.DocPath))
                    {
                        return Descp;
                    }
                    else
                    {

                        Descp.flag = 1;
                        Descp.desp = Descp.desp + ", Directory Creaction Failed ";
                    }
                }

            }
            else
            {
                Descp.flag = 1;
                Descp.desp = Descp.desp;

            }
            objDataEngine.CloseConnection();
            return Descp;



        }
        #endregion

        #region"Financial Info"
        public FinancialInfoModel GetFinancialInfo(int issNo, string acctNo)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", issNo);
            Parameters[1] = new SqlParameter("@ApplNo", acctNo);
            var Reader = objDataEngine.ExecuteCommand("WebAcctFinInfoSelect", CommandType.StoredProcedure, Parameters);
            while (Reader.Read())
            {
                var _financialInfo = new FinancialInfoModel
                {
                    TaxId = Convert.ToString(Reader["TaxId"]),
                    LatePaymtCharge = BaseClass.BoolConverter(Reader["LatePaymtInd"]),
                    SelectedDunningCd = Convert.ToString(Reader["DunCd"]),
                    CredtAllowanceFact = BaseClass.ConverterDecimal(Reader["AllowanceFactor"]),
                    AccrdInterest = BaseClass.ConverterDecimal(Reader["AccruedInterestAmt"]),
                    AccrdCrdtUsg = BaseClass.ConverterDecimal(Reader["AccruedCreditUsageAmt"]),
                    PromPaymtRebate = BaseClass.ConverterDecimal(Reader["PromptPaymtRebate"]),
                    PPRGracePeriod = Convert.ToInt32(Reader["PromptPaymtRebateTerms"]),
                    LitreLimitPerTxn = BaseClass.ConverterDecimal(Reader["LitLimitPerTxn"]),
                    AmtLimitPerTxn = BaseClass.ConverterDecimal(Reader["AmtLimitPerTxn"]),
                    CycNo = BaseClass.WebGetCycle("I"),
                    SelectedCycNo = Convert.ToString(Reader["CycNo"]),
                    StmtType = BaseClass.WebGetRefLib("BillingType"),
                    SelectedStmtType = Convert.ToString(Reader["StmtType"]),
                    StmtInd = BaseClass.WebGetRefLib("StmtInd"),
                    SelectedStmtInd = Convert.ToString(Reader["StmtInd"]),
                    StmtDate = BaseClass.DateConverter(Reader["StmtDate"]),
                    PaymtMethod = BaseClass.WebGetRefLib("PaymtMethod"),
                    SelectedPaymtMethod = Convert.ToString(Reader["PaymtMethod"]),
                    PaymtTerm = Convert.ToInt32(Reader["PymtTerms"]),
                    GracePeriod = Convert.ToInt32(Reader["GracePeriod"]),
                    DirectDebitInd = BaseClass.BoolConverter(Reader["DirectDebitInd"]),
                    BankAcctType = BaseClass.WebGetRefLib("BankAcctType"),
                    SelectedBankAcctType = Convert.ToString(Reader["BankAcctType"]),
                    selectedBankName = Convert.ToString(Reader["BankName"]),
                    BankName = BaseClass.WebGetRefLib("Bank"),
                    BankAcctNo = Convert.ToString(Reader["BankAcctNo"]),
                    BankBranchCD = Convert.ToString(Reader["BankBranchCd"]),
                    VATRate = BaseClass.ConverterDecimal(Reader["VATRate"]),
                    WriteoffDate = BaseClass.DateConverter(Reader["WriteOffDate"]),
                    LastPaymtType = Convert.ToString(Reader["LastPaymtRecvType"]),
                    LastPaymtReceived = BaseClass.ConverterDecimal(Reader["LastPaymtRecvAmt"]),
                    LastPaymtDate = BaseClass.DateConverter(Reader["LastPaymtDate"])
                };

                objDataEngine.CloseConnection();
                return _financialInfo;

            }
            return new FinancialInfoModel();

        }
        //public MsgRetriever SaveFinancialInfoResult(FinancialInfoModel _financialInfo, string acctNo)
        //{
        //    var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
        //    objDataEngine.InitiateConnection();
        //    SqlParameter[] Parameters = new SqlParameter[30];
        //    SqlCommand cmd = new SqlCommand();
        //    Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
        //    Parameters[1] = new SqlParameter("@AcctNo", String.IsNullOrEmpty(acctNo) ? "" : acctNo);
        //    Parameters[2] = String.IsNullOrEmpty(_financialInfo.TaxId) ? new SqlParameter("@TaxId", DBNull.Value) : new SqlParameter("@TaxId", _financialInfo.TaxId);
        //    Parameters[3] = new SqlParameter("@LatePaymtInd", _financialInfo.LatePaymtCharge);
        //    Parameters[4] = new SqlParameter("@DunCd", ConvertIntToDb(_financialInfo.SelectedDunningCd));
        //    Parameters[5] = new SqlParameter("@AllowanceFactor", ConvertDecimalToDb(_financialInfo.CredtAllowanceFact));
        //    Parameters[6] = new SqlParameter("@AccruedInterestAmt", ConvertDecimalToDb(_financialInfo.AccrdInterest));
        //    Parameters[7] = new SqlParameter("@AccruedCreditUsageAmt", ConvertDecimalToDb(_financialInfo.AccrdCrdtUsg));
        //    Parameters[8] = new SqlParameter("@PromptPaymtRebate", ConvertDecimalToDb(_financialInfo.PromPaymtRebate));
        //    Parameters[9] = new SqlParameter("@PromptPaymtRebateTerms", ConvertIntToDb(_financialInfo.PPRGracePeriod));
        //    Parameters[10] = new SqlParameter("@LitLimitPerTxn", ConvertDecimalToDb(_financialInfo.LitreLimitPerTxn));
        //    Parameters[11] = new SqlParameter("@AmtLimitPerTxn", ConvertDecimalToDb(_financialInfo.AmtLimitPerTxn));
        //    Parameters[12] = new SqlParameter("@CycNo", ConvertIntToDb(_financialInfo.SelectedCycNo));
        //    Parameters[13] = String.IsNullOrEmpty(_financialInfo.SelectedStmtType) ? new SqlParameter("@StmtType", DBNull.Value) : new SqlParameter("@StmtType", _financialInfo.SelectedStmtType);
        //    Parameters[14] = String.IsNullOrEmpty(_financialInfo.SelectedStmtInd) ? new SqlParameter("@StmtInd", DBNull.Value) : new SqlParameter("@StmtInd", _financialInfo.SelectedStmtInd);
        //    Parameters[15] = new SqlParameter("@StmtDate", DateConverterDB(_financialInfo.StmtDate));
        //    Parameters[16] = String.IsNullOrEmpty(_financialInfo.SelectedPaymtMethod) ? new SqlParameter("@PaymtMethod", DBNull.Value) : new SqlParameter("@PaymtMethod", _financialInfo.SelectedPaymtMethod);
        //    Parameters[17] = String.IsNullOrEmpty(_financialInfo.SelectedPaymtMethod) ? new SqlParameter("@PaymtTerms", DBNull.Value) : new SqlParameter("@PaymtTerms", _financialInfo.SelectedPaymtMethod);
        //    Parameters[18] = new SqlParameter("@GracePeriod", ConvertIntToDb(_financialInfo.GracePeriod));
        //    Parameters[19] = new SqlParameter("@DirectDebitInd", ConvertBoolDB(_financialInfo.DirectDebitInd));
        //    Parameters[20] = String.IsNullOrEmpty(_financialInfo.SelectedBankAcctType) ? new SqlParameter("@BankAcctType", DBNull.Value) : new SqlParameter("@BankAcctType", _financialInfo.SelectedBankAcctType);
        //    Parameters[21] = String.IsNullOrEmpty(_financialInfo.BankName) ? new SqlParameter("@BankName", DBNull.Value) : new SqlParameter("@BankName", _financialInfo.BankName);
        //    Parameters[22] = String.IsNullOrEmpty(_financialInfo.BankAcctNo) ? new SqlParameter("@BankAcctNo", DBNull.Value) : new SqlParameter("@BankAcctNo", _financialInfo.BankAcctNo);
        //    Parameters[23] = String.IsNullOrEmpty(_financialInfo.BankBranchCD) ? new SqlParameter("@BankBranchCd", DBNull.Value) : new SqlParameter("@BankBranchCd", _financialInfo.BankBranchCD);
        //    Parameters[24] = new SqlParameter("@VATRate", ConvertDecimalToDb(_financialInfo.VATRate));
        //    Parameters[25] = new SqlParameter("@WriteOffDate", BaseClass.DateConverterDB(_financialInfo.WriteoffDate));
        //    Parameters[26] = new SqlParameter("@LastPaymtRecvType", String.IsNullOrEmpty(_financialInfo.LastPaymtType) ? "" : _financialInfo.LastPaymtType);
        //    Parameters[27] = new SqlParameter("@LastPaymtRecvAmt", ConvertDecimalToDb(_financialInfo.LastPaymtReceived));
        //    Parameters[28] = new SqlParameter("@LastPaymtRecvDate", DateConverterDB(_financialInfo.LastPaymtDate));
        //    Parameters[29] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
        //    Parameters[29].Direction = ParameterDirection.ReturnValue;


        //    var Cmd = objDataEngine.ExecuteWithReturnValue("WebAcctFinInfoMaint", CommandType.StoredProcedure, Parameters);
        //    var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
        //    var Descp = GetMessageCode(Result);

        //    objDataEngine.CloseConnection();
        //    return Descp;

        //}
        #endregion

        #region "Credit Assement Operation"

        public CreditAssesOperation GetCAOGeneralInfo(string acctNo = null, string appId = null)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(acctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", acctNo);
            Parameters[2] = String.IsNullOrEmpty(appId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", appId);

            var Reader = objDataEngine.ExecuteCommand("WebAcctCAOSelect", CommandType.StoredProcedure, Parameters);

            while (Reader.Read())
            {

                var _caoMaint = new CreditAssesOperation
                {
                    CreditLimit = ConverterDecimal(Reader["CreditLimit"]),
                    PaymentMode = WebGetRefLib("PaymtMethod"),
                    SelectedPaymentMode = Convert.ToString(Reader["PymtMode"]),
                    PaymentTerm = BaseClass.ConvertInt(Reader["PymtTerms"]),
                    TxnAmtLimit = BaseClass.ConverterDecimal(Reader["TxnAmtLimit"]),
                    TxnLitLimit = BaseClass.ConverterDecimal(Reader["TxnLitLimit"]),
                    TerritoryCd = BaseClass.WebGetRefLib("SaleTerritory"),
                    SelectedTerritoryCd = Convert.ToString(Reader["SaleTerritory"]),
                    RiskCategory = BaseClass.WebGetRefLib("RiskCategory"),
                    SelectedRiskCategry = Convert.ToString(Reader["RiskCategory"]),
                    AssesmtType = BaseClass.WebGetRefLib("AssessmentType"),
                    SelectedAssesmtType = Convert.ToString(Reader["AssessmentType"]),
                    SecurityAmt = BaseClass.ConverterDecimal(Reader["SecurityAmt"]),
                    DirectDebitInd = BaseClass.BoolConverter(Reader["DirectDebitInd"]),
                    DepositType = BaseClass.WebGetRefLib("DepositType"),
                    SelectedDepositType = Convert.ToString(Reader["DepositType"]),
                    DepositExp = DateConverter(Reader["DepositExp"]),
                    BankAcctType = BaseClass.WebGetRefLib("BankAcctType"),
                    SelectedBankAcctType = Convert.ToString(Reader["BankAcctType"]),
                    BankName = BaseClass.WebGetRefLib("Bank"),
                    SelectedBankName = Convert.ToString(Reader["BankName"]),
                    BankAcctNo = Convert.ToString(Reader["BankAcctNo"]),
                    BankBranchCode = Convert.ToString(Reader["BankBranchCd"]),
                    DepositAmt = ConverterDecimal(Reader["DepositAmt"]),
                    ValidityDate = DateConverter(Reader["ValidityDate"]),
                    NRID = DateConverter(Reader["NRID"]),
                    ReasonCd = WebGetCAOReasonCd(),
                    SelectedReasonCd = Convert.ToString(Reader["ReasonCd"]),
                    AppvUserIdQAOff = Convert.ToString(Reader["AppvUserId1"]),
                    AppvStsQAOff = BaseClass.WebGetRefLib("ApplSts"),
                    SelectedAppvStsQAOff = Convert.ToString(Reader["AppvSts1"]),
                    AppvDateQAOff = DateConverter(Reader["AppvDate1"]),

                    AppvUserIdBackOff = Convert.ToString(Reader["AppvUserId2"]),
                    AppvStsBackOff = BaseClass.WebGetRefLib("ApplSts"),
                    SelectedAppvStsBackOff = Convert.ToString(Reader["AppvSts2"]),
                    AppvDateBackOff = DateConverter(Reader["AppvDate2"]),


                    //AppvUserIdCredOff = Convert.ToString(Reader["AppvUserId3"]),
                    //AppvStsCredOff = BaseClass.WebGetRefLib("ApplSts"),
                    //SelectedAppvStsCredOff = Convert.ToString(Reader["AppvSts3"]),
                    //AppvDateCredOff = DateConverter(Reader["AppvDate3"]),

                    AppvUserIdEDP = Convert.ToString(Reader["AppvUserId4"]),
                    AppvStsEDP = BaseClass.WebGetRefLib("ApplSts"),
                    SelectedAppvStsEDP = Convert.ToString(Reader["AppvSts4"]),
                    AppvDateEDP = DateConverter(Reader["AppvDate4"]),


                    DocPath = Convert.ToString(Reader["DocPath"])
                };

                objDataEngine.CloseConnection();
                return _caoMaint;
            }

            return new CreditAssesOperation
            {
                PaymentMode = BaseClass.WebGetRefLib("PaymtMethod"),
                RiskCategory = BaseClass.WebGetRefLib("RiskCategory"),
                AssesmtType = BaseClass.WebGetRefLib("AssessmentType"),
                DepositType = BaseClass.WebGetRefLib("DepositType"),
                BankAcctType = BaseClass.WebGetRefLib("BankAcctType"),
                BankName = BaseClass.WebGetRefLib("Bank"),
                AppvStsEDP = BaseClass.WebGetRefLib("ApplSts"),
                AppvStsCredOff = BaseClass.WebGetRefLib("ApplSts"),
                AppvStsBackOff = BaseClass.WebGetRefLib("ApplSts"),
                AppvStsQAOff = BaseClass.WebGetRefLib("ApplSts"),
                TerritoryCd = BaseClass.WebGetRefLib("SaleTerritory"),
                ReasonCd = BaseClass.WebGetRefLib("ReasonCd"),
            };
        }

        public MsgRetriever SaveCreditAssessmentOperation(CreditAssesOperation _CAO, string acctNo = null, string applId = null)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[36];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(acctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", acctNo);
            Parameters[2] = String.IsNullOrEmpty(applId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", applId);
            Parameters[3] = new SqlParameter("@CreditLimit", ConvertDecimalToDb(_CAO.CreditLimit));
            Parameters[4] = String.IsNullOrEmpty(_CAO.SelectedPaymentMode) ? new SqlParameter("@PymtMode", DBNull.Value) : new SqlParameter("@PymtMode", _CAO.SelectedPaymentMode);

            Parameters[5] = new SqlParameter("@PymtTerms", ConvertIntToDb(_CAO.PaymentTerm));
            Parameters[6] = new SqlParameter("@TxnLimit", ConvertDecimalToDb(_CAO.TxnAmtLimit));
            Parameters[7] = new SqlParameter("@LitLimit", ConvertDecimalToDb(_CAO.TxnLitLimit));
            Parameters[8] = String.IsNullOrEmpty(_CAO.SelectedTerritoryCd) ? new SqlParameter("@SaleTerritory", DBNull.Value) : new SqlParameter("@SaleTerritory", _CAO.SelectedTerritoryCd);
            Parameters[9] = String.IsNullOrEmpty(_CAO.SelectedRiskCategry) ? new SqlParameter("@RiskCategory", DBNull.Value) : new SqlParameter("@RiskCategory", _CAO.SelectedRiskCategry);

            Parameters[10] = String.IsNullOrEmpty(_CAO.SelectedAssesmtType) ? new SqlParameter("@AssessmentType", DBNull.Value) : new SqlParameter("@AssessmentType", _CAO.SelectedAssesmtType);
            Parameters[11] = new SqlParameter("@DirectDebitInd", ConvertBoolDB(_CAO.DirectDebitInd));
            Parameters[12] = String.IsNullOrEmpty(_CAO.SelectedDepositType) ? new SqlParameter("@DepositType", DBNull.Value) : new SqlParameter("@DepositType", _CAO.SelectedDepositType);
            Parameters[13] = new SqlParameter("@DepositExp", DateConverter(_CAO.DepositExp));
            Parameters[14] = String.IsNullOrEmpty(_CAO.SelectedBankAcctType) ? new SqlParameter("@BankAcctType", DBNull.Value) : new SqlParameter("@BankAcctType", _CAO.SelectedBankAcctType);

            Parameters[15] = String.IsNullOrEmpty(_CAO.SelectedBankName) ? new SqlParameter("@BankCd", DBNull.Value) : new SqlParameter("@BankCd", _CAO.SelectedBankName);
            Parameters[16] = String.IsNullOrEmpty(_CAO.BankAcctNo) ? new SqlParameter("@BankAcctNo", DBNull.Value) : new SqlParameter("@BankAcctNo", _CAO.BankAcctNo);
            Parameters[17] = String.IsNullOrEmpty(_CAO.BankBranchCode) ? new SqlParameter("@BankBranchCd", DBNull.Value) : new SqlParameter("@BankBranchCd", _CAO.BankBranchCode);
            Parameters[18] = new SqlParameter("@DepositAmt", ConvertDecimalToDb(_CAO.DepositAmt));
            Parameters[19] = new SqlParameter("@ValidityDate", DateConverterDB(_CAO.ValidityDate));

            Parameters[20] = new SqlParameter("@NRID", DateConverterDB(_CAO.NRID));
            Parameters[21] = String.IsNullOrEmpty(_CAO.SelectedReasonCd) ? new SqlParameter("@ReasonCd", DBNull.Value) : new SqlParameter("@ReasonCd", _CAO.SelectedReasonCd);
            Parameters[22] = String.IsNullOrWhiteSpace(_CAO.AppvUserIdQAOff) ? new SqlParameter("@AppvUserId1", DBNull.Value) : new SqlParameter("@AppvUserId1", _CAO.AppvUserIdQAOff);
            Parameters[23] = String.IsNullOrWhiteSpace(_CAO.SelectedAppvStsQAOff) ? new SqlParameter("@AppvSts1", DBNull.Value) : new SqlParameter("@AppvSts1", _CAO.SelectedAppvStsQAOff);
            Parameters[24] = new SqlParameter("@AppvDate1", DateConverterDB(_CAO.AppvDateQAOff));

            Parameters[25] = String.IsNullOrWhiteSpace(_CAO.AppvUserIdBackOff) ? new SqlParameter("@AppvUserId2", DBNull.Value) : new SqlParameter("@AppvUserId2", _CAO.AppvUserIdBackOff);
            Parameters[26] = String.IsNullOrWhiteSpace(_CAO.SelectedAppvStsBackOff) ? new SqlParameter("@AppvSts2", DBNull.Value) : new SqlParameter("@AppvSts2", _CAO.SelectedAppvStsBackOff);
            Parameters[27] = new SqlParameter("@AppvDate2", DateConverterDB(_CAO.AppvDateBackOff));
            Parameters[28] = String.IsNullOrWhiteSpace(_CAO.AppvUserIdCredOff) ? new SqlParameter("@AppvUserId3", DBNull.Value) : new SqlParameter("@AppvUserId3", _CAO.AppvUserIdCredOff);
            Parameters[29] = String.IsNullOrWhiteSpace(_CAO.SelectedAppvStsCredOff) ? new SqlParameter("@AppvSts3", DBNull.Value) : new SqlParameter("@AppvSts3", _CAO.SelectedAppvStsCredOff);

            Parameters[30] = new SqlParameter("@AppvDate3", DateConverterDB(_CAO.AppvDateCredOff));
            Parameters[31] = String.IsNullOrWhiteSpace(_CAO.AppvUserIdEDP) ? new SqlParameter("@AppvUserId4", DBNull.Value) : new SqlParameter("@AppvUserId4", _CAO.AppvUserIdEDP);
            Parameters[32] = String.IsNullOrWhiteSpace(_CAO.SelectedAppvStsEDP) ? new SqlParameter("@AppvSts4", DBNull.Value) : new SqlParameter("@AppvSts4", _CAO.SelectedAppvStsEDP);
            Parameters[33] = new SqlParameter("@AppvDate4", DateConverterDB(_CAO.AppvDateEDP));
            Parameters[34] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[35] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[35].Direction = ParameterDirection.ReturnValue;







            var Cmd = objDataEngine.ExecuteWithReturnValue("WebAcctCAOMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;


        }

        #endregion

        #region "UpToDateBal"
        public UpToDateBal GetUpToBal(string issNo, string acctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", issNo);
            Parameters[1] = new SqlParameter("@AcctNo", acctNo);
            var Reader = objDataEngine.ExecuteCommand("WebAcctUpToDateBalanceSelect", CommandType.StoredProcedure, Parameters);
            while (Reader.Read())
            {
                var _uptodatebal = new UpToDateBal
                {
                    AcctType = BaseClass.WebGetRefLib("AcctType"),
                    SelectedAcctType = Convert.ToString(Reader["Account Type"]),
                    CreditLimit = BaseClass.ConverterDecimal(Reader["CreditLimit"]),
                    OpeningBal = BaseClass.ConverterDecimal(Reader["Opening Balance"]),
                    InstantAmt = BaseClass.ConverterDecimal(Reader["Instant Amount"]),
                    UnpostedAmt = BaseClass.ConverterDecimal(Reader["Unposted Amount"]),
                    ClosingBal = BaseClass.ConverterDecimal(Reader["Closing Balance"]),
                };

                objDataEngine.CloseConnection();
                return _uptodatebal;

            }



            return new UpToDateBal();
        }
        #endregion

        #region "VelocityLimits"

        public List<VeloctyLimitListMaintModel> GetCustAcctVelocityList(VeloctyLimitListMaintModel _CustAcctVelocity)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[5];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_CustAcctVelocity._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _CustAcctVelocity._CardnAccNo.AccNo);
            Parameters[2] = String.IsNullOrEmpty(_CustAcctVelocity._CardnAccNo.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _CustAcctVelocity._CardnAccNo.CardNo);
            Parameters[3] = String.IsNullOrEmpty(_CustAcctVelocity.ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", _CustAcctVelocity.ApplId);
            Parameters[4] = String.IsNullOrEmpty(_CustAcctVelocity.AppcId) ? new SqlParameter("@AppcId", DBNull.Value) : new SqlParameter("@AppcId", _CustAcctVelocity.AppcId);

            var execResult = objDataEngine.ExecuteCommand("WebVelocityLimitListSelect", CommandType.StoredProcedure, Parameters);

            var _VeloctyLimitListMaint = new List<VeloctyLimitListMaintModel>();



            while (execResult.Read())
            {
                _VeloctyLimitListMaint.Add(new VeloctyLimitListMaintModel
                {
                    VelocityIndDescp = Convert.ToString(execResult["Velocity Indicator"]),
                    ProdCdDescp = Convert.ToString(execResult["Product Code"]),
                    CntrLimit = Convert.ToInt32(execResult["Counter"]),
                    ddlVelocityLimit = ConverterDecimal(execResult["Velocity Amount"]),
                    ddlVelocityLitre = ConverterDecimal(execResult["Velocity Litre"]),
                    LastUpdateDate = DateConverter(execResult["Last Update Date"]),
                    UserId = Convert.ToString(execResult["User Id"]),
                    CreationDate = DateConverter(execResult["Creation Date"]),
                    SelectedVelocityInd = Convert.ToString(execResult["VelocityInd"]),
                    SelectedProdCd = Convert.ToString(execResult["Product"]),
                    SpentCnt = Convert.ToInt32(execResult["Spent Counter"]),
                    SpentLimit = ConverterDecimal(execResult["Spent Amount"]),
                    SpentLitre = ConverterDecimal(execResult["Spent Litre"]),

                });



            };
            objDataEngine.CloseConnection();
            return _VeloctyLimitListMaint;
        }
        public VeloctyLimitListMaintModel GetCustAcctVelocityDetail(VeloctyLimitListMaintModel _VelocityDetail)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[7];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_VelocityDetail._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _VelocityDetail._CardnAccNo.AccNo);
            Parameters[2] = String.IsNullOrEmpty(_VelocityDetail._CardnAccNo.CardNo) ? new SqlParameter("@CardNo ", DBNull.Value) : new SqlParameter("@CardNo ", _VelocityDetail._CardnAccNo.CardNo);
            Parameters[3] = String.IsNullOrEmpty(_VelocityDetail.ApplId) ? new SqlParameter("@ApplId ", DBNull.Value) : new SqlParameter("@ApplId ", _VelocityDetail.ApplId);
            Parameters[4] = String.IsNullOrEmpty(_VelocityDetail.AppcId) ? new SqlParameter("@AppcId ", DBNull.Value) : new SqlParameter("@AppcId ", _VelocityDetail.AppcId);
            Parameters[5] = String.IsNullOrEmpty(_VelocityDetail.SelectedVelocityInd) ? new SqlParameter("@VelocityInd ", DBNull.Value) : new SqlParameter("@VelocityInd ", _VelocityDetail.SelectedVelocityInd);
            Parameters[6] = String.IsNullOrEmpty(_VelocityDetail.SelectedProdCd) ? new SqlParameter("@ProdCd ", DBNull.Value) : new SqlParameter("@ProdCd ", _VelocityDetail.SelectedProdCd);

            var execResult = objDataEngine.ExecuteCommand("WebVelocityLimitSelect", CommandType.StoredProcedure, Parameters);

            var _GetVelDetail = new VeloctyLimitListMaintModel();

            while (execResult.Read())
            {
                // _GetVelDetail.VelocityInd = BaseClass.WebGetRefLib("VelocityInd"); 
                _GetVelDetail.SelectedVelocityInd = Convert.ToString(execResult["VelocityInd"]);
                //_GetVelDetail.ProdCd = BaseClass.WebProductGroupSelect(1);
                _GetVelDetail.SelectedProdCd = Convert.ToString(execResult["Product"]);
                _GetVelDetail.CntrLimit = Convert.ToInt32(execResult["Counter"]);
                _GetVelDetail.VelocityLimit = ConverterDecimal(execResult["Velocity Amount"]);
                _GetVelDetail.VelocityLitre = ConverterDecimal(execResult["Velocity Litre"]);
                _GetVelDetail.CreationDate = DateConverter(execResult["CreationDate"]);
                _GetVelDetail.LastUpdateDate = DateConverter(execResult["LastUpdDate"]);
                _GetVelDetail.UserId = Convert.ToString(execResult["UserId"]);

            };
            objDataEngine.CloseConnection();
            return _GetVelDetail;

        }
        public MsgRetriever SaveCustAcctVelocity(VeloctyLimitListMaintModel _VelocityLimitList, string applId, string Func)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[14];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
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

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebVelocityLimitMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;
        }
        public MsgRetriever DelCustAcctVelocity(string acctNo, string cardNo, string applId, string appcId, string VelInd, string ProdCd)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[8];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(acctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", acctNo);
            Parameters[2] = String.IsNullOrEmpty(cardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", cardNo);
            Parameters[3] = String.IsNullOrEmpty(applId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", applId);
            Parameters[4] = String.IsNullOrEmpty(appcId) ? new SqlParameter("@AppcId", DBNull.Value) : new SqlParameter("@AppcId", appcId);
            Parameters[5] = String.IsNullOrEmpty(VelInd) ? new SqlParameter("@VelocityInd", DBNull.Value) : new SqlParameter("@VelocityInd", VelInd);
            Parameters[6] = String.IsNullOrEmpty(ProdCd) ? new SqlParameter("@ProdCd", DBNull.Value) : new SqlParameter("@ProdCd", ProdCd);
            Parameters[7] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[7].Direction = ParameterDirection.ReturnValue;


            var Cmd = objDataEngine.ExecuteWithReturnValue("WebVelocityLimitDelete", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;
        }

        #endregion

        #region "CardHolder"

        public List<CardHolderInfoModel> GetCardHolderList(string AcctNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = new SqlParameter("@AcctNo", String.IsNullOrEmpty(AcctNo) ? "" : AcctNo);

            var execResult = objDataEngine.ExecuteCommand("WebCardListSelect", CommandType.StoredProcedure, Parameters);
            var _CardHolderInfo = new List<CardHolderInfoModel>();

            while (execResult.Read())
            {
                _CardHolderInfo.Add(new CardHolderInfoModel
                {
                    CardNo = Convert.ToString(execResult["CardNo"]),
                    EmbossName = Convert.ToString(execResult["Emboss Name"]),
                    SelectedCurrentStatus = Convert.ToString(execResult["Status"]),
                    CardExpiry = Convert.ToString(execResult["Card Expiry"]),
                    XRefCardNo = ConvertLong(execResult["Xref CardNo"]),
                    SelectedCardType = Convert.ToString(execResult["Card Type"]),
                    SelectedPINInd = Convert.ToString(execResult["PIN"]),
                    vehRegNo = Convert.ToString(execResult["VRN"]),
                    SKDSQuota = ConverterDecimal(execResult["SKDS Quota"]),
                    SelectedSKDSNo = Convert.ToString(execResult["SKDSNo"]),
                    DriverCd = Convert.ToString(execResult["Driver"]),
                    FullName = Convert.ToString(execResult["Full Name"]),
                    TerminatedDate = Convert.ToString(execResult["Card Terminated"])
                });
            }
            objDataEngine.CloseConnection();
            return _CardHolderInfo;


        }
        public CardHolderInfoModel GetCardHolderDetail(string CardNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = new SqlParameter("@AcctNo", String.IsNullOrEmpty(CardNo) ? "" : CardNo);

            var execResult = objDataEngine.ExecuteCommand("WebCardSelect", CommandType.StoredProcedure, Parameters);

            var _GetCardHolder = new CardHolderInfoModel();

            while (execResult.Read())
            {
                _GetCardHolder.CreationDate = System.DateTime.Now.ToShortDateString();
                _GetCardHolder.CardType = BaseClass.WebGetRefLib("CardType");
                _GetCardHolder.PINInd = BaseClass.WebGetRefLib("PinInd");
                _GetCardHolder.CurrentStatus = BaseClass.WebGetRefLib("CardSts");
                _GetCardHolder.PlasticType = BaseClass.WebPlasticTypeSelect();
                _GetCardHolder.JonFee = BaseClass.WebGetFeeCd("JON");
                _GetCardHolder.AnnualFee = BaseClass.WebGetFeeCd("ANN");
                _GetCardHolder.RenewalInd = BaseClass.WebGetRefLib("RenewalInd");

                _GetCardHolder.CardNo = Convert.ToString(execResult["CardNo"]);
                _GetCardHolder.EmbossName = Convert.ToString(execResult["Emboss Name"]);
                _GetCardHolder.SelectedCurrentStatus = Convert.ToString(execResult["Status"]);
                _GetCardHolder.CardExpiry = Convert.ToString(execResult["Card Expiry"]);
                _GetCardHolder.XRefCardNo = ConvertLong(execResult["Xref CardNo"]);
                _GetCardHolder.SelectedCardType = Convert.ToString(execResult["Card Type"]);
                _GetCardHolder.SelectedPINInd = Convert.ToString(execResult["PIN"]);
                _GetCardHolder.vehRegNo = Convert.ToString(execResult["VRN"]);
                //_GetCardHolder.SKDSQuota = BaseClass.DecimalConverter(execResult["SKDS Quota"]);
                _GetCardHolder.DriverCd = Convert.ToString(execResult["Driver"]);
                _GetCardHolder.FullName = Convert.ToString(execResult["Full Name"]);
                _GetCardHolder.TerminatedDate = Convert.ToString(execResult["Card Terminated"]);
            };
            objDataEngine.CloseConnection();
            return _GetCardHolder;

        }

        public List<SearchResult> GetBusinessLocations(string merchantId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = new SqlParameter("@BusnLocation", merchantId);

            var execResult = objDataEngine.ExecuteCommand("WebLocationAcceptanceBySearch", CommandType.StoredProcedure, Parameters);
            var SearchResults = new List<SearchResult>();

            while (execResult.Read())
            {
                SearchResults.Add(new SearchResult
                {
                    Descp = Convert.ToString(execResult["Descp"]),
                    Object = Convert.ToString(execResult["Dealer"]),
                });
            }
            objDataEngine.CloseConnection();
            return SearchResults;
        }

        public CardHolderInfoModel GetCardHolder(string cardNo)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = new SqlParameter("@CardNo", cardNo);
            var Reader = objDataEngine.ExecuteCommand("WebCardGeneralInfoSelect", CommandType.StoredProcedure, Parameters);

            while (Reader.Read())
            {

                var _cardHolderGeneralInfo = new CardHolderInfoModel
                {
                    RenewalInd = BaseClass.WebGetRefLib("RenewalInd"),
                    EmbossName = Convert.ToString(Reader["EmbName"]),
                    CardType = BaseClass.WebGetCardType(),
                    AcctNo = Convert.ToString(Reader["AcctNo"]),
                    SelectedCardType = Convert.ToString(Reader["CardType"]),
                    CurrentStatus = BaseClass.WebGetRefLib("CardSts"),
                    SelectedCurrentStatus = Convert.ToString(Reader["Sts"]),
                    ReasonCd = BaseClass.WebGetRefLib("ReasonCd", "32"),
                    SelectedReasonCd = Convert.ToString(Reader["ReasonCd"]),
                    CreationDate = Convert.ToString(Reader["CreationDate"]),
                    MemberSince = Convert.ToString(Reader["MemSince"]),
                    CardExpiry = Convert.ToString(Reader["CardExpiry"]),
<<<<<<< HEAD
                   XRefCardNo = ConvertLong(Reader["XRef CardNo"]),
                   oldCardNo=ConvertLong(Reader["Old CardNo"]),
=======
                  // XRefCardNo = ConvertLong(Reader["XRef CardNo"]),
                  // oldCardNo=ConvertLong(Reader["Old CardNo"]),
>>>>>>> 39cce31bf2724bf075ce3dd49f45b7def49fead7
                    PINInd = BaseClass.WebGetRefLib("PINInd"),
                    SelectedPINInd = Convert.ToString(Reader["PINInd"]),
                    vehRegNo = Convert.ToString(Reader["VehRegsNo"]),
                    SelectedSKDSInd = BoolConverter(Reader["SKDSInd"]),
                    SelectedSKDSNo = Convert.ToString(Reader["SKDSNo"]),
                    SKDSQuota = BaseClass.ConverterDecimal(Reader["SKDSQuota"]),
                    DriverCd = Convert.ToString(Reader["DriverCd"]),
                    //FullName = Convert.ToString(Reader["Full Name"]),
                    TerminatedDate = BaseClass.DateConverter(Reader["TerminatedDate"]),
                    PVV = Convert.ToString(Reader["PVV"]),
                    PINOffset = Convert.ToString(Reader["PINoffSet"]),
                    DialogueInd = BaseClass.WebGetRefLib("DialogueInd"),
                    SelectedDialogueInd = Convert.ToString(Reader["DialogueInd"]),
                    SelectedPushAlertInd = BaseClass.BoolConverter(Reader["PushAlertInd"]),
                    LocInd = BaseClass.BoolConverter(Reader["LocationInd"]),
                    SelectedLocCheckFlag = BaseClass.BoolConverter(Reader["LocationCheckFlag"]),
                    MaxCountLimit = ConvertInt(Reader["LocationMaxCnt"]),
                    AmtLimit = BaseClass.ConverterDecimal(Reader["LocationMaxAmt"]),
                    SelectedFuelCheckFlag = BaseClass.BoolConverter(Reader["FuelCheckFlag"]),
                    FuelLitre = BaseClass.ConverterDecimal(Reader["FuelLitPerKM"]),
                    PINExceedCnt = BaseClass.ConvertInt(Reader["PINExceedCnt"]),
                    PINAttempted = BaseClass.ConvertInt(Reader["PINAttempted"]),
                    AnnualFee = BaseClass.WebGetFeeCd("ANN"),
                    SelectedAnnualFee = Convert.ToString(Reader["AnnlFeeCd"]),
                    JonFee = BaseClass.WebGetFeeCd("JON"),
                    SelectedJonFee = Convert.ToString(Reader["JoiningFeeCd"]),
                    SelectedRenewalInd = Convert.ToString(Reader["RenewalInd"]),
                    EntityId = Convert.ToString(Reader["EntityId"]),
                    OdometerIndicator = BaseClass.BoolConverter(Reader["OdometerInd"])
                };

                objDataEngine.CloseConnection();
                return _cardHolderGeneralInfo;
            }
            objDataEngine.CloseConnection();
            return new CardHolderInfoModel();
        }
        public MsgRetriever SaveCardHolderInfo(CardHolderInfoModel _cardHolder)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[24];
            SqlCommand cmd = new SqlCommand();

            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_cardHolder.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _cardHolder.CardNo);
            Parameters[2] = String.IsNullOrEmpty(_cardHolder.EmbossName) ? new SqlParameter("@EmbName", DBNull.Value) : new SqlParameter("@EmbName", _cardHolder.EmbossName);
            Parameters[3] = String.IsNullOrEmpty(_cardHolder.SelectedCurrentStatus) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _cardHolder.SelectedCurrentStatus);
            Parameters[4] = String.IsNullOrEmpty(_cardHolder.TerminatedDate) ? new SqlParameter("@TerminatedDate", DBNull.Value) : new SqlParameter("@TerminatedDate", _cardHolder.TerminatedDate);
            Parameters[5] = String.IsNullOrEmpty(_cardHolder.vehRegNo) ? new SqlParameter("@VehRegsNo", DBNull.Value) : new SqlParameter("@VehRegsNo", _cardHolder.vehRegNo);
            Parameters[6] = String.IsNullOrEmpty(_cardHolder.DriverCd) ? new SqlParameter("@DriverCd", DBNull.Value) : new SqlParameter("@DriverCd", _cardHolder.DriverCd);
            Parameters[7] = new SqlParameter("@SKDSInd", ConvertBoolDB(_cardHolder.SelectedSKDSInd));
            Parameters[8] = new SqlParameter("@SKDSQuota", ConvertDecimalToDb(_cardHolder.SKDSQuota));
            Parameters[9] = String.IsNullOrEmpty(_cardHolder.SelectedSKDSNo) ? new SqlParameter("@SKDSNo", DBNull.Value) : new SqlParameter("@SKDSNo", _cardHolder.SelectedSKDSNo);
            Parameters[10] = String.IsNullOrEmpty(_cardHolder.SelectedDialogueInd) ? new SqlParameter("@DialogueInd", DBNull.Value) : new SqlParameter("@DialogueInd", _cardHolder.SelectedDialogueInd);
            Parameters[11] = new SqlParameter("@PushAlertInd", ConvertBoolDB(_cardHolder.SelectedPushAlertInd));
            Parameters[12] = new SqlParameter("@LocationInd", ConvertBoolDB(_cardHolder.LocInd));
            Parameters[13] = new SqlParameter("@LocationCheckFlag", ConvertBoolDB(_cardHolder.SelectedLocCheckFlag));
            Parameters[14] = new SqlParameter("@LocationMaxCnt", ConvertIntToDb(_cardHolder.MaxCountLimit));
            Parameters[15] = new SqlParameter("@LocationMaxAmt", ConvertDecimalToDb(_cardHolder.AmtLimit));
            Parameters[16] = new SqlParameter("@FuelCheckFlag", ConvertBoolDB(_cardHolder.SelectedFuelCheckFlag));
            Parameters[17] = new SqlParameter("@FuelLitPerKM", ConvertDecimalToDb(_cardHolder.FuelLitre));
            Parameters[18] = String.IsNullOrEmpty(_cardHolder.SelectedAnnualFee) ? new SqlParameter("@AnnlFee", DBNull.Value) : new SqlParameter("@AnnlFee", _cardHolder.SelectedAnnualFee);
            Parameters[19] = String.IsNullOrEmpty(_cardHolder.SelectedJonFee) ? new SqlParameter("@JoiningFee", DBNull.Value) : new SqlParameter("@JoiningFee", _cardHolder.SelectedJonFee);
            Parameters[20] = String.IsNullOrEmpty(_cardHolder.SelectedRenewalInd) ? new SqlParameter("@RenewalInd", DBNull.Value) : new SqlParameter("@RenewalInd ", _cardHolder.SelectedRenewalInd);
            Parameters[21] = new SqlParameter("@UserId  ", this.GetUserId);
            Parameters[22] = new SqlParameter("@OdometerInd", ConvertBoolDB(_cardHolder.OdometerIndicator));

            Parameters[23] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[23].Direction = ParameterDirection.ReturnValue;

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebCardGeneralInfoMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;





        }

        #endregion

        #region "Addresses List"
        public List<AddrListMaintModel> GetAddressList(string RefTo, string RefKey)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = new SqlParameter("@RefTo", String.IsNullOrEmpty(RefTo) ? "" : RefTo);
            Parameters[2] = new SqlParameter("@RefKey", String.IsNullOrEmpty(RefKey) ? "" : RefKey);


            var execResult = objDataEngine.ExecuteCommand("WebAddressListSelect", CommandType.StoredProcedure, Parameters);

            var _AddrListMaintModel = new List<AddrListMaintModel>();

            while (execResult.Read())
            {
                _AddrListMaintModel.Add(new AddrListMaintModel
                {
                    SelectedAddrType = Convert.ToString(execResult["Address Type"]),
                    MainMailingInd = BoolConverter(execResult["Main Mailing"]),
                    SelectedMailInd = Convert.ToString(execResult["Main Mailing"]),
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
            objDataEngine.CloseConnection();
            return _AddrListMaintModel;

        }
        public AddrListMaintModel GetAddressDetail(string RefTo, string RefKey, string RefCd)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[4];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = new SqlParameter("@RefTo", String.IsNullOrEmpty(RefTo) ? "" : RefTo);
            Parameters[2] = new SqlParameter("@RefKey", String.IsNullOrEmpty(RefKey) ? "" : RefKey);
            Parameters[3] = new SqlParameter("@RefCd", String.IsNullOrEmpty(RefCd) ? "" : RefCd);

            var execResult = objDataEngine.ExecuteCommand("WebAddressSelect", CommandType.StoredProcedure, Parameters);

            var _GetAddress = new AddrListMaintModel();

            while (execResult.Read())
            {
                _GetAddress.SelectedAddrType = Convert.ToString(execResult["RefCd"]);// humairah: take the refcode value coz address col return the description of the address 
                _GetAddress.MainMailingInd = BoolConverter(execResult["Main Mailing"]);
                _GetAddress.addr1 = Convert.ToString(execResult["Address1"]);
                _GetAddress.addr2 = Convert.ToString(execResult["Address2"]);
                _GetAddress.addr3 = Convert.ToString(execResult["Address3"]);
                _GetAddress.Selectedstate = Convert.ToString(execResult["StateCd"]);
                _GetAddress.PostalCode = Convert.ToString(execResult["ZipCd"]);
                _GetAddress.SelectedCountry = Convert.ToString(execResult["Country"]);
                _GetAddress.selectedregion = Convert.ToString(execResult["Region"]);
                _GetAddress.SelectedRefCd = Convert.ToString(execResult["RefCd"]);
            };
            objDataEngine.CloseConnection();
            return _GetAddress;

        }
        public MsgRetriever SaveAddressList(AddrListMaintModel _AddrListMaint, string RefTo, string RefCd, string RefKey)
        { //@RefCd  to save address is the selected address type
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[14];
            Parameters[0] = new SqlParameter("@Func", DBNull.Value);
            Parameters[1] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[2] = String.IsNullOrEmpty(RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", RefTo);
            Parameters[3] = String.IsNullOrEmpty(RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", RefKey);
            Parameters[4] = String.IsNullOrEmpty(_AddrListMaint.SelectedAddrType) ? new SqlParameter("@RefCd", DBNull.Value) : new SqlParameter("@RefCd", _AddrListMaint.SelectedAddrType);
            Parameters[5] = String.IsNullOrEmpty(_AddrListMaint.addr1) ? new SqlParameter("@Street1", DBNull.Value) : new SqlParameter("@Street1", _AddrListMaint.addr1);
            Parameters[6] = String.IsNullOrEmpty(_AddrListMaint.addr2) ? new SqlParameter("@Street2", DBNull.Value) : new SqlParameter("@Street2", _AddrListMaint.addr2);
            Parameters[7] = String.IsNullOrEmpty(_AddrListMaint.addr3) ? new SqlParameter("@Street3", DBNull.Value) : new SqlParameter("@Street3", _AddrListMaint.addr3);
            Parameters[8] = String.IsNullOrEmpty(_AddrListMaint.Selectedstate) ? new SqlParameter("@State", DBNull.Value) : new SqlParameter("@State", _AddrListMaint.Selectedstate);
            Parameters[9] = String.IsNullOrEmpty(_AddrListMaint.PostalCode) ? new SqlParameter("@ZipCd", DBNull.Value) : new SqlParameter("@ZipCd", _AddrListMaint.PostalCode);
            Parameters[10] = String.IsNullOrEmpty(_AddrListMaint.SelectedCountry) ? new SqlParameter("@Ctry", DBNull.Value) : new SqlParameter("@Ctry", _AddrListMaint.SelectedCountry);
            Parameters[11] = new SqlParameter("@MailInd", ConvertBoolDB(_AddrListMaint.MainMailingInd));
            Parameters[12] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[13] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[13].Direction = ParameterDirection.ReturnValue;

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebAddressMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;

        }
        public MsgRetriever DelAddress(string RefTo, string RefKey, string RefCd)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[6];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", RefTo);
            Parameters[2] = String.IsNullOrEmpty(RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", RefKey);
            Parameters[3] = String.IsNullOrEmpty(RefCd) ? new SqlParameter("@RefCd", DBNull.Value) : new SqlParameter("@RefCd", RefCd);
            Parameters[4] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[5].Direction = ParameterDirection.ReturnValue;
            var Cmd = objDataEngine.ExecuteWithReturnValue("WebAddressDelete", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);

            var Descp = GetMessageCode(Result);


            objDataEngine.CloseConnection();
            return Descp;

        }
        #endregion

        #region "Contacts List"
        public List<ContactLstModel> GetContactlist(string RefTo, string RefKey)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = new SqlParameter("@RefTo", String.IsNullOrEmpty(RefTo) ? "" : RefTo);
            Parameters[2] = new SqlParameter("@RefKey", String.IsNullOrEmpty(RefKey) ? "" : RefKey);

            var execResult = objDataEngine.ExecuteCommand("WebContactListSelect", CommandType.StoredProcedure, Parameters);
            var _ContactLstModel = new List<ContactLstModel>();

            while (execResult.Read())
            {
                _ContactLstModel.Add(new ContactLstModel
                {
                    RefTo = Convert.ToString(execResult["RefTo"]),
                    RefCd = Convert.ToString(execResult["RefCd"]),
                    SelectedContactType = Convert.ToString(execResult["ContactType"]),
                    ContactName = Convert.ToString(execResult["Contact Name"]),
                    ContactNo = Convert.ToString(execResult["Contact No"]),
                    SelectedSts = Convert.ToString(execResult["Contact Status"]),
                    RawStatus = Convert.ToString(execResult["Status"]),
                    SelectedOccupation = Convert.ToString(execResult["Job Occupation"]),
                    RawOccupation = Convert.ToString(execResult["Occupation"]),
                    EmailAddr = Convert.ToString(execResult["EmailAddr"]),
                    CreationDate = Convert.ToString(execResult["CreationDate"]),
                    UserId = Convert.ToString(execResult["UserId"])

                });
            };
            objDataEngine.CloseConnection();
            return _ContactLstModel;

        }
        public ContactLstModel GetContactDetail(string RefTo, string RefKey, string RefCd)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[4];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", RefTo);
            Parameters[2] = String.IsNullOrEmpty(RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", RefKey);
            Parameters[3] = String.IsNullOrEmpty(RefCd) ? new SqlParameter("@RefCd", DBNull.Value) : new SqlParameter("@RefCd", RefCd);

            var execResult = objDataEngine.ExecuteCommand("WebContactSelect", CommandType.StoredProcedure, Parameters);
            var _Contact = new ContactLstModel();
            while (execResult.Read())
            {
                _Contact.RefKey = Convert.ToString(execResult["RefTo"]);
                _Contact.RefCd = Convert.ToString(execResult["RefCd"]);
                _Contact.SelectedContactType = Convert.ToString(execResult["ContactType"]);
                _Contact.ContactName = Convert.ToString(execResult["Contact Name"]);
                _Contact.ContactNo = Convert.ToString(execResult["Contact No"]);
                _Contact.SelectedSts = Convert.ToString(execResult["Status"]);
                _Contact.SelectedOccupation = Convert.ToString(execResult["Occupation"]);
                _Contact.EmailAddr = Convert.ToString(execResult["EmailAddr"]);
                _Contact.UserId = Convert.ToString(execResult["UserId"]);
                _Contact.CreationDate = DateConverter(execResult["CreationDate"]);


            };
            objDataEngine.CloseConnection();
            return _Contact;

        }
        public MsgRetriever SaveContactsList(ContactLstModel _ContactList, string RefTo, string Func)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[12];
            Parameters[0] = String.IsNullOrEmpty(Func) ? new SqlParameter("@Func", DBNull.Value) : new SqlParameter("@Func", Func);
            Parameters[1] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[2] = String.IsNullOrEmpty(RefTo) ? new SqlParameter("@RefTo", DBNull.Value) : new SqlParameter("@RefTo", RefTo);
            Parameters[3] = String.IsNullOrEmpty(_ContactList.RefKey) ? new SqlParameter("@RefKey", DBNull.Value) : new SqlParameter("@RefKey", _ContactList.RefKey);
            Parameters[4] = String.IsNullOrEmpty(_ContactList.SelectedContactType) ? new SqlParameter("@RefCd", DBNull.Value) : new SqlParameter("@RefCd", _ContactList.SelectedContactType);
            Parameters[5] = String.IsNullOrEmpty(_ContactList.ContactName) ? new SqlParameter("@ContactName", DBNull.Value) : new SqlParameter("@ContactName", _ContactList.ContactName);
            Parameters[6] = String.IsNullOrEmpty(_ContactList.SelectedOccupation) ? new SqlParameter("@Occupation", DBNull.Value) : new SqlParameter("@Occupation", _ContactList.SelectedOccupation);
            Parameters[7] = String.IsNullOrEmpty(_ContactList.ContactNo) ? new SqlParameter("@ContactNo", DBNull.Value) : new SqlParameter("@ContactNo", _ContactList.ContactNo);
            Parameters[8] = String.IsNullOrEmpty(_ContactList.SelectedSts) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _ContactList.SelectedSts);
            Parameters[9] = String.IsNullOrEmpty(_ContactList.EmailAddr) ? new SqlParameter("@EmailAddr", DBNull.Value) : new SqlParameter("@EmailAddr", _ContactList.EmailAddr);
            Parameters[10] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
            Parameters[11] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[11].Direction = ParameterDirection.ReturnValue;

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebContactMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;
        }
        public MsgRetriever DelContact(string RefTo, string RefKey, string RefCd)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[5];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = new SqlParameter("@RefTo", String.IsNullOrEmpty(RefTo) ? "" : RefTo);
            Parameters[2] = new SqlParameter("@RefKey", String.IsNullOrEmpty(RefKey) ? "" : RefKey);
            Parameters[3] = new SqlParameter("@RefCd", String.IsNullOrEmpty(RefCd) ? "" : RefCd);
            Parameters[4] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[4].Direction = ParameterDirection.ReturnValue;

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebContactDelete", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;

        }
        #endregion

        #region "Vehicle"
        public List<VehiclesListModel> GetVehicleList(string AcctNo, string ApplId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(AcctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", AcctNo);
            Parameters[2] = String.IsNullOrEmpty(ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", ApplId);

            var execResult = objDataEngine.ExecuteCommand("WebVehicleListSelect", CommandType.StoredProcedure, Parameters);
            var _GetVehicleList = new List<VehiclesListModel>();

            while (execResult.Read())
            {
                _GetVehicleList.Add(new VehiclesListModel
                {
                    AppcId = Convert.ToString(execResult["Appc Id"]),
                    CardNo = Convert.ToString(execResult["CardNo"]),//
                    SelectedCardType = Convert.ToString(execResult["Card Type"]),//
                    pin = Convert.ToString(execResult["PIN"]),//
                    VehRegtNo = Convert.ToString(execResult["VRN"]),//
                    VehRegDate = Convert.ToString(execResult["Registered Date"]),//
                    SelectedVehMaker = Convert.ToString(execResult["Vehicle Maker"]),//
                    SelectedSts = Convert.ToString(execResult["Status"]),//
                    XrefCardNo = Convert.ToString(execResult["Xref CardNo"]),//
                    OdoMeterReading = Convert.ToString(execResult["Odometer Reading"]),//
                    OdoMeterUpdate = Convert.ToString(execResult["Odometer Update"]),//
                    PolicyExpDate = Convert.ToString(execResult["Card Expiry"]),
                    RoadTaxExpDate = Convert.ToString(execResult["RoadTax Expiry"]),
                    SelectedVehType = Convert.ToString(execResult["Vehicle Type"]),
                    SelectedVehColor = Convert.ToString(execResult["Vehicle Color"]),
                    SelectedVehModel = Convert.ToString(execResult["Vehicle Model"]),
                    CardTerminated = DateConverter(execResult["Card Terminated"])
                });
            };
            objDataEngine.CloseConnection();
            return _GetVehicleList;

        }
        public VehiclesListModel GetVehicleDetail(VehiclesListModel _VehiclesListModel)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[4];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_VehiclesListModel.AppcId) ? new SqlParameter("@AppcId", DBNull.Value) : new SqlParameter("@AppcId", _VehiclesListModel.AppcId);
            Parameters[2] = String.IsNullOrEmpty(_VehiclesListModel.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _VehiclesListModel.CardNo);
            Parameters[3] = String.IsNullOrEmpty(_VehiclesListModel.VehRegtNo) ? new SqlParameter("@VRN", DBNull.Value) : new SqlParameter("@VRN", _VehiclesListModel.VehRegtNo);

            var execResult = objDataEngine.ExecuteCommand("WebVehicleSelect", CommandType.StoredProcedure, Parameters);

            _VehiclesListModel = new VehiclesListModel();

            while (execResult.Read())
            {
                _VehiclesListModel.CardNo = Convert.ToString(execResult["CardNo"]);
                _VehiclesListModel.VehRegtNo = Convert.ToString(execResult["VehRegsNo"]);
                _VehiclesListModel.SkdsInd = BaseClass.BoolConverter(execResult["SKDSInd"]);//hide
                _VehiclesListModel.SkdsQuota = ConverterDecimal(execResult["SKDS Quota"]);//hide
                _VehiclesListModel.SelectedVehMaker = Convert.ToString(execResult["Vehicle Maker"]);
                _VehiclesListModel.SelectedVehModel = Convert.ToString(execResult["Vehicle Model"]);
                _VehiclesListModel.VehRegDate = Convert.ToString(execResult["VehRegsDate"]);
                _VehiclesListModel.SelectedVehType = Convert.ToString(execResult["VehType"]);
                _VehiclesListModel.SelectedVehColor = Convert.ToString(execResult["Color"]);
                _VehiclesListModel.OdoMeterReading = Convert.ToString(execResult["CurrOdoReading"]);
                _VehiclesListModel.OdoMeterUpdate = Convert.ToString(execResult["OdoLastUpd"]);
                _VehiclesListModel.RoadTaxExpDate = Convert.ToString(execResult["RoadTaxExpiry"]);
                _VehiclesListModel.SelectedSts = Convert.ToString(execResult["Sts"]);
                _VehiclesListModel.CardExpiry = Convert.ToString(execResult["Card Expiry"]);
                _VehiclesListModel.XrefCardNo = Convert.ToString(execResult["XRefCardNo"]);
                _VehiclesListModel.CardTerminated = Convert.ToString(execResult["Card Terminated"]);

            };
            objDataEngine.CloseConnection();
            return _VehiclesListModel;

        }
        public MsgRetriever SaveVehicleList(VehiclesListModel _VehiclesListModel, string AppcId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[14];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = new SqlParameter("@AppcId", AppcId);
            Parameters[2] = String.IsNullOrEmpty(_VehiclesListModel.CardNo) ? new SqlParameter("@CardNo", DBNull.Value) : new SqlParameter("@CardNo", _VehiclesListModel.CardNo);
            Parameters[3] = String.IsNullOrEmpty(_VehiclesListModel.VehRegtNo) ? new SqlParameter("@VRN", DBNull.Value) : new SqlParameter("@VRN", _VehiclesListModel.VehRegtNo);
            Parameters[4] = new SqlParameter("@SKDSInd", ConvertBoolDB(_VehiclesListModel.SkdsInd));
            Parameters[5] = new SqlParameter("@SKDSQuota", ConvertDecimalToDb(_VehiclesListModel.SkdsQuota));
            Parameters[6] = String.IsNullOrEmpty(_VehiclesListModel.SelectedVehMaker) ? new SqlParameter("@Manufacturer", DBNull.Value) : new SqlParameter("@Manufacturer", _VehiclesListModel.SelectedVehMaker);
            Parameters[7] = String.IsNullOrEmpty(_VehiclesListModel.SelectedVehModel) ? new SqlParameter("@Model", DBNull.Value) : new SqlParameter("@Model", _VehiclesListModel.SelectedVehModel);
            Parameters[8] = new SqlParameter("@VehRegsDate", DateConverterDB(_VehiclesListModel.VehRegDate));
            Parameters[9] = String.IsNullOrEmpty(_VehiclesListModel.SelectedVehType) ? new SqlParameter("@VehType", DBNull.Value) : new SqlParameter("@VehType", _VehiclesListModel.SelectedVehType);
            Parameters[10] = String.IsNullOrEmpty(_VehiclesListModel.SelectedVehColor) ? new SqlParameter("@Color", DBNull.Value) : new SqlParameter("@Color", _VehiclesListModel.SelectedVehColor);
            Parameters[11] = new SqlParameter("@RoadTaxExpiry", DateConverterDB(_VehiclesListModel.RoadTaxExpDate));
            Parameters[12] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[13] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[13].Direction = ParameterDirection.ReturnValue;

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebVehicleMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;
        }
        #endregion

        #region "MiscellaneousInfo"
        public MiscellaneousInfoModel GetMiscellaneousInfoDetail(int ApplId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = new SqlParameter("@ApplId", ApplId);

            var execResult = objDataEngine.ExecuteCommand("WebApplMiscInfoSelect", CommandType.StoredProcedure, Parameters);
            var _misInfo = new MiscellaneousInfoModel();

            while (execResult.Read())
            {
                _misInfo.Designation = BaseClass.WebGetRefLib("Occupation");
                _misInfo.AuthName = Convert.ToString(execResult["AuthName"]);
                _misInfo.SelectedDesignation = Convert.ToString(execResult["Designation"]);
                _misInfo.AuthDate = Convert.ToString(execResult["AuthDate"]);

            };
            objDataEngine.CloseConnection();
            return _misInfo;

        }
        public MsgRetriever SaveMiscellaneousInfo(MiscellaneousInfoModel _miscellaneousInfo)
        {

            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[6];
            SqlCommand cmd = new SqlCommand();
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_miscellaneousInfo.ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", _miscellaneousInfo.ApplId);
            Parameters[2] = String.IsNullOrEmpty(_miscellaneousInfo.AuthName) ? new SqlParameter("@AuthName", DBNull.Value) : new SqlParameter("@AuthName", _miscellaneousInfo.AuthName);
            Parameters[3] = String.IsNullOrEmpty(_miscellaneousInfo.SelectedDesignation) ? new SqlParameter("@Designation", DBNull.Value) : new SqlParameter("@Designation", _miscellaneousInfo.SelectedDesignation);
            Parameters[4] = new SqlParameter("@AuthDate", DateConverterDB(_miscellaneousInfo.AuthDate));
            Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[5].Direction = ParameterDirection.ReturnValue;

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebApplMiscInfoMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;


        }
        #endregion

        #region"SKDS"
        public List<SKDS> GetSKDSList(SKDS _SKDS)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[4];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_SKDS._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _SKDS._CardnAccNo.AccNo);
            Parameters[2] = String.IsNullOrEmpty(_SKDS.ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", _SKDS.ApplId);
            Parameters[3] = new SqlParameter("@Page", DBNull.Value);

            var execResult = objDataEngine.ExecuteCommand("WebAcctSubsidyListSelect", CommandType.StoredProcedure, Parameters);
            var _SkdsList = new List<SKDS>();

            while (execResult.Read())
            {
                _SkdsList.Add(new SKDS
                {
                    TxnId = Convert.ToString(execResult["TxnId"]),
                    SKDSNo = Convert.ToString(execResult["SKDSNo"]),
                    SKDSLitreQuota = ConverterDecimal(execResult["Quota"]),
                    EffFromDate = DateConverter(execResult["From Date"]),
                    EffToDate = DateConverter(execResult["To Date"]),
                    Refference = Convert.ToString(execResult["Reference"]),
                    Remarks = Convert.ToString(execResult["Remarks"]),
                    LastUpdDate = DateConverter(execResult["Last Update Date"]),
                    UserId = Convert.ToString(execResult["User Id"]),
                    CreationDate = DateConverter(execResult["Creation Date"]),
                    SelectedSts = Convert.ToString(execResult["Status"])
                });
            };
            objDataEngine.CloseConnection();
            return _SkdsList;
        }
        public SKDS GetSKDSDetail(SKDS _SKDS)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[4];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_SKDS._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _SKDS._CardnAccNo.AccNo);
            Parameters[2] = String.IsNullOrEmpty(_SKDS.ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", _SKDS.ApplId);
            Parameters[3] = String.IsNullOrEmpty(_SKDS.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _SKDS.TxnId);

            var execResult = objDataEngine.ExecuteCommand("WebAcctSubsidySelect", CommandType.StoredProcedure, Parameters);

            var SKDSDetail = new SKDS();
            while (execResult.Read())
            {
                SKDSDetail.TxnId = Convert.ToString(execResult["TxnId"]);
                SKDSDetail.SKDSNo = Convert.ToString(execResult["SKDSNo"]);
                SKDSDetail.SKDSLitreQuota = ConverterDecimal(execResult["Quota"]);
                SKDSDetail.QuotaFromDate = DateConverter(execResult["QuotaFromDate"]);
                SKDSDetail.QuotaToDate = DateConverter(execResult["QuotaToDate"]);
                SKDSDetail.EffFromDate = DateConverter(execResult["EffFromDate"]);
                SKDSDetail.EffToDate = DateConverter(execResult["EffToDate"]);
                SKDSDetail.Refference = Convert.ToString(execResult["Ref"]);
                SKDSDetail.LastSubsidyDate = DateConverter(execResult["LastSubsidyDate"]);
                SKDSDetail.Remarks = Convert.ToString(execResult["Remarks"]);
                SKDSDetail.LastUpdDate = DateConverter(execResult["LastUpdDate"]);
                SKDSDetail.UserId = Convert.ToString(execResult["UserId"]);
                SKDSDetail.CreationDate = DateConverter(execResult["CreationDate"]);
                SKDSDetail.SelectedSts = Convert.ToString(execResult["Sts"]);

            };
            objDataEngine.CloseConnection();
            return SKDSDetail;

        }
        public MsgRetriever SaveSKDS(SKDS _SKDS)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[16];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(_SKDS._CardnAccNo.AccNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", _SKDS._CardnAccNo.AccNo);
            Parameters[2] = String.IsNullOrEmpty(_SKDS.ApplId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", _SKDS.ApplId);
            Parameters[3] = String.IsNullOrEmpty(_SKDS.TxnId) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _SKDS.TxnId);
            Parameters[4] = String.IsNullOrEmpty(_SKDS.SKDSNo) ? new SqlParameter("@SKDSNo", DBNull.Value) : new SqlParameter("@SKDSNo", _SKDS.SKDSNo);
            Parameters[5] = new SqlParameter("@QuotaFromDate", DateConverterDB(_SKDS.QuotaFromDate));
            Parameters[6] = new SqlParameter("@QuotaToDate", DateConverterDB(_SKDS.QuotaToDate));
            Parameters[7] = new SqlParameter("@Quota", ConvertDecimalToDb(ConverterDecimal(_SKDS.SKDSLitreQuota)));
            Parameters[8] = String.IsNullOrEmpty(_SKDS.Refference) ? new SqlParameter("@Ref", DBNull.Value) : new SqlParameter("@Ref", _SKDS.Refference);

            Parameters[9] = new SqlParameter("@LastSubsidyDate", DateConverterDB(_SKDS.LastSubsidyDate));

            Parameters[10] = new SqlParameter("@EffFromDate", DateConverterDB(_SKDS.EffFromDate));
            Parameters[11] = new SqlParameter("@EffToDate", DateConverterDB(_SKDS.EffToDate));
            Parameters[12] = String.IsNullOrEmpty(_SKDS.Remarks) ? new SqlParameter("@Remarks ", DBNull.Value) : new SqlParameter("@Remarks ", _SKDS.Remarks);
            Parameters[13] = String.IsNullOrEmpty(_SKDS.SelectedSts) ? new SqlParameter("@Sts ", DBNull.Value) : new SqlParameter("@Sts ", _SKDS.SelectedSts);
            Parameters[14] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[15] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[15].Direction = ParameterDirection.ReturnValue;

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebAcctSubsidyMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;
        }
        #endregion

        #region "AcctDepositInfo"
        public List<CreditAssesOperation> GetAcctDepositInfoList(string applid = null, string acctNo = null)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(applid) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", applid);
            Parameters[2] = String.IsNullOrEmpty(acctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", acctNo);
            var execResult = objDataEngine.ExecuteCommand("WebAcctDepositInfoListSelect", CommandType.StoredProcedure, Parameters);
            var _acctDeptInfo = new List<CreditAssesOperation>();
            while (execResult.Read())
            {
                _acctDeptInfo.Add(new CreditAssesOperation
                {
                    SelectedDirectDebitInd = Convert.ToString(execResult["DirectDebitInd"]),
                    SelectedDepositType = Convert.ToString(execResult["DepositType"]),
                    SelectedBankAcctType = Convert.ToString(execResult["BankAcctType"]),
                    SelectedBankName = Convert.ToString(execResult["BankName"]),
                    BankAcctNo = Convert.ToString(execResult["BankAcctNo"]),
                    DepositAmt = ConverterDecimal(execResult["DepositAmt"]),
                    Txnid = Convert.ToString(execResult["TxnId"]),
                    UserId = Convert.ToString(execResult["UserId"]),
                    Creationdt = Convert.ToString(execResult["CreationDate"]),

                });
            };
            return _acctDeptInfo;
        }
        public CreditAssesOperation GetAcctDepositInfoDetail(string applid = null, string acctNo = null, string txnid = null)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[4];
            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(applid) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", applid);
            Parameters[2] = String.IsNullOrEmpty(acctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", acctNo);
            Parameters[3] = String.IsNullOrEmpty(txnid) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", txnid);
            var execResult = objDataEngine.ExecuteCommand("WebAcctDepositInfoSelect", CommandType.StoredProcedure, Parameters);

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
                _GetAcctDeptInfo.NRID = DateConverter(execResult["NIRD"]);
                _GetAcctDeptInfo.remarks = Convert.ToString(execResult["Remarks"]);
                _GetAcctDeptInfo.Txnid = Convert.ToString(execResult["TxnId"]);
                _GetAcctDeptInfo.UserId = Convert.ToString(execResult["UserId"]);
                _GetAcctDeptInfo.Creationdt = DateConverter(execResult["CreationDate"]);
            };
            return _GetAcctDeptInfo;

        }
        public MsgRetriever SaveAcctDepositInfo(CreditAssesOperation _AcctDeptInfo, string acctNo = null, string applId = null)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[18];
            SqlCommand cmd = new SqlCommand();

            Parameters[0] = new SqlParameter("@IssNo", this.GetIssNo);
            Parameters[1] = String.IsNullOrEmpty(applId) ? new SqlParameter("@ApplId", DBNull.Value) : new SqlParameter("@ApplId", applId);
            Parameters[2] = String.IsNullOrEmpty(acctNo) ? new SqlParameter("@AcctNo", DBNull.Value) : new SqlParameter("@AcctNo", acctNo);
            Parameters[3] = String.IsNullOrEmpty(_AcctDeptInfo.Txnid) ? new SqlParameter("@TxnId", DBNull.Value) : new SqlParameter("@TxnId", _AcctDeptInfo.Txnid);
            Parameters[4] = new SqlParameter("@DepositInd", ConvertBoolDB(_AcctDeptInfo.DirectDebitInd));
            Parameters[5] = String.IsNullOrEmpty(_AcctDeptInfo.SelectedDepositType) ? new SqlParameter("@DepositType", DBNull.Value) : new SqlParameter("@DepositType", _AcctDeptInfo.SelectedDepositType);
            Parameters[6] = new SqlParameter("@ValidityDate", DateConverterDB(_AcctDeptInfo.ValidityDate));
            Parameters[7] = String.IsNullOrEmpty(_AcctDeptInfo.SelectedBankAcctType) ? new SqlParameter("@BankAcctType", DBNull.Value) : new SqlParameter("@BankAcctType", _AcctDeptInfo.SelectedBankAcctType);
            Parameters[8] = String.IsNullOrEmpty(_AcctDeptInfo.SelectedBankName) ? new SqlParameter("@BankName", DBNull.Value) : new SqlParameter("@BankName", _AcctDeptInfo.SelectedBankName);
            Parameters[9] = String.IsNullOrEmpty(_AcctDeptInfo.BankAcctNo) ? new SqlParameter("@BankAcctNo", DBNull.Value) : new SqlParameter("@BankAcctNo", _AcctDeptInfo.BankAcctNo);
            Parameters[10] = new SqlParameter("@DepositAmt", ConvertDecimalToDb(_AcctDeptInfo.DepositAmt));
            Parameters[11] = String.IsNullOrEmpty(_AcctDeptInfo.BankBranchCode) ? new SqlParameter("@BGSerialNo", DBNull.Value) : new SqlParameter("@BGSerialNo", _AcctDeptInfo.BankBranchCode);
            Parameters[12] = new SqlParameter("@EffFromDate", DateConverterDB(_AcctDeptInfo.DepositFromDate));
            Parameters[13] = new SqlParameter("@EffToDate", DateConverterDB(_AcctDeptInfo.DepositToDate));
            Parameters[14] = String.IsNullOrEmpty(_AcctDeptInfo.NRID) ? new SqlParameter("@NIRD", DBNull.Value) : new SqlParameter("@NIRD", DateConverterDB(_AcctDeptInfo.NRID));
            Parameters[15] = String.IsNullOrEmpty(_AcctDeptInfo.remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", (_AcctDeptInfo.remarks));
            Parameters[16] = new SqlParameter("@UserId", this.GetUserId);
            Parameters[17] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
            Parameters[17].Direction = ParameterDirection.ReturnValue;

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebAcctDepositInfoMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = GetMessageCode(Result);

            objDataEngine.CloseConnection();
            return Descp;


        }


        #endregion
    }
}