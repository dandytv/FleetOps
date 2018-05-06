using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CCMS.ModelSector;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.Corporate;
using ModelSector.Helpers;
namespace ModelSector
{ 
    public class GeneralInfoModel
    { 
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "AcctNoLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = @"Numbers only")]
        public string AcctNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedPlasticTypeDdl")]
        public string SelectedPlasticType { get; set; }
        public IEnumerable<SelectListItem> PlasticType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "AccountNameLbl")]
        public string AccountName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CmpyRegsNoLbl")]
        [StringLength(50, ErrorMessage = "Maximum lengthis 50 characters")]
        public string CmpyRegsNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "RegsDateLbl")]
        public string RegsDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedSICDdl")]
        public string SelectedSIC { get; set; }
        public IEnumerable<SelectListItem> SIC { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SapNoLbl")]
        public string SapNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedCorpNameDdl")]
        public string SelectedCorpName { get; set; }
        public IEnumerable<SelectListItem> CorpName { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedClientClassDdl")]
        public string SelectedClientClass { get; set; }
        public IEnumerable<SelectListItem> ClientClass { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedCustomerGroupDdl")]
        public string SelectedCustomerGroup { get; set; }
        public IEnumerable<SelectListItem> CustomerGroup { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedBusnEstablishmentDdl")]
        public string SelectedBusnEstablishment { get; set; }
        public IEnumerable<SelectListItem> BusnEstablishment { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SourceCodeLbl")]
        public string SourceCode { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SourceRefNoLbl")]
        public string SourceRefNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedCurrentStatusDdl")]
        public string SelectedCurrentStatus { get; set; }
        //public IEnumerable<SelectListItem> CurrentStatus { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "CreationDateLbl")]
        public string CreationDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "TerminatedDateLbl")]
        public string TerminatedDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "BlockedDateLbl")]
        public string BlockedDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedReasonCodeDdl")]
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCode { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "OvrStatusTaggedByUserIdLbl")]
        public string OvrStatusTaggedByUserId { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedOvrStatusDdl")]
        public string SelectedOvrStatus { get; set; }
        public IEnumerable<SelectListItem> OvrStatus { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "OvrStartDateLbl")]
        public string OvrStartDate { get; set; }
        public string OvrExpDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "ApplIdLbl")]
        public string ApplId { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "ApplRefLbl")]
        public string ApplRef { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "ApplCreationDateLbl")]
        public string ApplCreationDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "ApplTransfferedDateLbl")]
        public string ApplTransfferedDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "RemarksLbl")]
        public string Remarks { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "WebUserIdLbl")]
        public string WebUserId { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "LoyaltyCardNoLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string LoyaltyCardNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "EntityIdLbl")]
        public string EntityId { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "AuditIdLbl")]
        public string AuditId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "AdminEmailLbl")]
        public string AdminEmail { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "ResendEmailLbl")]
        public string ResendEmail { get; set; }

        public string CmpyName1 { get; set; }
        public IEnumerable<SelectListItem> CompanyType { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedCompanyTypeDdl")]
        public string SelectedCompanyType { get; set; }

        public string CaptDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "WebPasswordLbl")]
        public string WebPassword { get; set; }
        public string CustSvcId { get; set; }
        public IEnumerable<SelectListItem> BusnCategory { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedBusnCategoryDdl")]
        public string SelectedBusnCategory { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "TaxIdLbl")]
        public string TaxId { get; set; }

        public IEnumerable<SelectListItem> SaleTerritory { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedSaleTerritoryDdl")]
        public string SelectedSaleTerritory { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedAccountTypeDdl")]
        public string SelectedAccountType { get; set; }
        public IEnumerable<SelectListItem> AccountType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CutOffLbl")]
        public string CutOff { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedPaymentTermDdl")]
        [Range(0, 255)]
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string SelectedPaymentTerm { get; set; }
        public IEnumerable<SelectListItem> PaymentTerm { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedAcctStsDdl")]
        public string SelectedAcctSts { get; set; }
        //public IEnumerable<SelectListItem> AcctSts { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "selectedClientTypeDdl")]
        public string selectedClientType { get; set; }
        public IEnumerable<SelectListItem> ClientType { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedLangIdDdl")]
        public string SelectedLangId { get; set; }
        public IEnumerable<SelectListItem> LangId { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CompanyEmbNameLbl")]
        [StringLength(26, ErrorMessage = "Maximum lengthis 26 characters")]
        public string CompanyEmbName { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "ContactPersonLbl")]
        public string ContactPerson { get; set; }

        public string CorpId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "PointBalanceLbl")]
        public string PointBalance { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "AuthSignatoryLbl")]
        public string AuthSignatory { get; set; }

        public IEnumerable<SelectListItem> TradingArea { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedTradingAreaDdl")]
        public string SelectedTradingArea { get; set; }
    }
    public class FinancialInfoModel
    {
        public IEnumerable<SelectListItem> TaxCategory { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedTaxCategoryDdl")]
        public string SelectedTaxCategory { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "AcctNoLbl")]
        public string AcctNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "TaxIdLbl")]
        public string TaxId { get; set; }

        [DisplayName("Late Payment Charge")]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedTaxCategoryDdl")]
        public bool LatePaymtCharge { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedDunningCdDdl")]
        [RegularExpression(@"^([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])$", ErrorMessage = "Dunning Code limit is 255")]
        public string SelectedDunningCd { get; set; }
        public IEnumerable<SelectListItem> DunningCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CredtAllowanceFactLbl")]
        public string CredtAllowanceFact { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "AccrdInterestLbl")]
        public string AccrdInterest { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "AccrdCrdtUsgLbl")]
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string AccrdCrdtUsg { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "PromPaymtRebateLbl")]
        public string PromPaymtRebate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "PPRGracePeriodLbl")]
        [RegularExpression(@"^([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])$", ErrorMessage = "PPR Grace Period limit is 255")]
        public int? PPRGracePeriod { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "pprExpiryLbl")]
        public string pprExpiry { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "LitreLimitPerTxnLbl")]
        public string LitreLimitPerTxn { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:#####.##$}")]

        [DisplayNameLocalizedAttribute("CardtrendAccount", "AmtLimitPerTxnLbl")]

        public string AmtLimitPerTxn { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedCycNoDdl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string SelectedCycNo { get; set; }
        public IEnumerable<SelectListItem> CycNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedStmtTypeDdl")]
        public string SelectedStmtType { get; set; }
        public IEnumerable<SelectListItem> StmtType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedStmtIndDdl")]
        public string SelectedStmtInd { get; set; }
        public IEnumerable<SelectListItem> StmtInd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "StmtDateLbl")]
        public string StmtDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedPaymtMethodDdl")]

        public string SelectedPaymtMethod { get; set; }
        public IEnumerable<SelectListItem> PaymtMethod { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "PaymtTermLbl")]
        [RegularExpression(@"^([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])$", ErrorMessage = "Payment Term limit is 255")]
        public int? PaymtTerm { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "GracePeriodLbl")]
        [RegularExpression(@"^([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])$", ErrorMessage = "Grace Period limit is 255")]
        public int? GracePeriod { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "DirectDebitIndLbl")]
        public bool DirectDebitInd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedBankAcctTypeDdl")]
        public string SelectedBankAcctType { get; set; }
        public IEnumerable<SelectListItem> BankAcctType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "selectedBankNameDdl")]
        public string selectedBankName { get; set; }
        public IEnumerable<SelectListItem> BankName { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "BankAcctNoLbl")]
        public string BankAcctNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "BankBranchCDLbl")]
        public string BankBranchCD { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "VATRateLbl")]
        public string VATRate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "WriteoffDateLbl")]
        public string WriteoffDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "LastPaymtTypeLbl")]
        public string LastPaymtType { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "LastPaymtReceivedLbl")]
        public string LastPaymtReceived { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "LastPaymtDateLbl")]
        public string LastPaymtDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "UserIdLbl")]
        public string UserId { get; set; }
        public SKDS _skds { get; set; }
        public CreditAssesOperation _creditAssesOperation { get; set; }
        public UpToDateBal _upToDateBal { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "InvoiceBillingIndicatorLbl")]
        public bool InvoiceBillingIndicator { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "PayAdviceBillingIndicatorLbl")]
        public bool PayAdviceBillingIndicator { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "InvoiceIndCopyLbl")]
        public bool InvoiceIndCopy { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "VehiclePerformanceReportIndLbl")]
        public bool VehiclePerformanceReportInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "WithholdingTaxIndLbl")]
        public bool WithholdingTaxInd { get; set; }
        public IEnumerable<SelectListItem> ProdRebateType { get; set; }
        public string SelectedProdRebateType { get; set; }
        public string ProdRebateRate { get; set; }
        public IEnumerable<SelectListItem> RiskCategory { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedRiskCategoryDdl")]
        public string SelectedRiskCategory { get; set; }
        public IEnumerable<SelectListItem> AssessmentType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedAssessmentTypeDdl")]
        public string SelectedAssessmentType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CreditLimitLbl")]
        public string CreditLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "PayeeCdLbl")]
        public string PayeeCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedAssignCollectorDdl")]
        public string SelectedAssignCollector { get; set; }
        public IEnumerable<SelectListItem> AssignCollector { get; set; }
    }
    public class CreditLimitHistory
    {
        public string From { get; set; }
        public string To { get; set; }
        public string AcctNo { get; set; }
        public string DepositType { get; set; }
        public string Field { get; set; }
        public string UserId { get; set; }
        public string CreationDate { get; set; }
    }

    public class CreditAssesOperation
    {
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedTerritoryCdDdl")]
        public string SelectedTerritoryCd { get; set; }
        public IEnumerable<SelectListItem> TerritoryCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedRiskCategryDdl")]
        public string SelectedRiskCategry { get; set; }
        public IEnumerable<SelectListItem> RiskCategory { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedAssesmtTypeDdl")]
        public string SelectedAssesmtType { get; set; } //change
        public IEnumerable<SelectListItem> AssesmtType { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedBankAcctTypeDdl")]
        public string SelectedBankAcctType { get; set; }
        public IEnumerable<SelectListItem> BankAcctType { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "selectedBankNameDdl")]
        public string SelectedBankName { get; set; }
        public IEnumerable<SelectListItem> BankName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "BankAcctNoLbl")]
        public string BankAcctNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "BankBranchCodeLbl")]
        public string BankBranchCode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "ApprovedCreditLimitLbl")]
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string CreditLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "LineOfCredtProviderLbl")]
        public string LineOfCredtProvider { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "DepositExpLbl")]
        public string DepositExp { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "DirectDebitIndLbl")]
        public bool DirectDebitInd { get; set; }
        public string SelectedDirectDebitInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedDepositTypeDdl")]
        public string SelectedDepositType { get; set; }
        public IEnumerable<SelectListItem> DepositType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedStatusDdl")]
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SecurityAmtLbl")]
        public string SecurityAmt { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "DepositAmtLbl")]
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string DepositAmt { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "ValidityDateLbl")]
        public string ValidityDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "NRIDLbl")]
        public string NRID { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedPaymentModeDdl")]
        public string SelectedPaymentMode { get; set; }
        public IEnumerable<SelectListItem> PaymentMode { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedPaymentTermDdl")]
        [Range(0, 255)]
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string SelectedPaymentTerm { get; set; }
        public IEnumerable<SelectListItem> PaymentTerm { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "GracePeriodLbl")]
        [Range(0, 255)]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? GracePeriod { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "DepositFromDateLbl")]
        public string DepositFromDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "DepositToDateLbl")]
        public string DepositToDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "DepositBankAcctNoLbl")]
        public string DepositBankAcctNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "DepositBankBranchCDLbl")]
        public string DepositBankBranchCD { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "TxnAmtLimitLbl")]
        public string TxnAmtLimit { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "TxnLitLimitLbl")]
        public string TxnLitLimit { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "AppvUserIdEDPLbl")]
        public string AppvUserIdEDP { get; set; }

	    [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedAppvStsEDPDdl")]
        public string SelectedAppvStsEDP { get; set; }
        public IEnumerable<SelectListItem> AppvStsEDP { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "AppvDateEDPLbl")]
        public string AppvDateEDP { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "AppvUserIdCredOffLbl")]
        public string AppvUserIdCredOff { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedAppvStsCredOffDdl")]
        public string SelectedAppvStsCredOff { get; set; }
        public IEnumerable<SelectListItem> AppvStsCredOff { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "AppvDateEDPLbl")]
        public string AppvDateCredOff { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "AppvUserIdCredOffLbl")]
        public string AppvUserIdBackOff { get; set; }


		[DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedAppvStsCredOffDdl")]
        public string SelectedAppvStsBackOff { get; set; }
        public IEnumerable<SelectListItem> AppvStsBackOff { get; set; }

	    [DisplayNameLocalizedAttribute("CardtrendAccount", "AppvDateEDPLbl")]
        public string AppvDateBackOff { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "AppvUserIdCredOffLbl")]
        public string AppvUserIdQAOff { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedAppvStsCredOffDdl")]
        public string SelectedAppvStsQAOff { get; set; }
        public IEnumerable<SelectListItem> AppvStsQAOff { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "AppvDateEDPLbl")]
        public string AppvDateQAOff { get; set; }
        public string DocPath { get; set; }
		[DisplayNameLocalizedAttribute("CardtrendAccount", "UserIdLbl")]
        public string UserId { get; set; }
		[DisplayNameLocalizedAttribute("CardtrendAccount", "TxnidLbl")]
        public string Txnid { get; set; }
		[DisplayNameLocalizedAttribute("CardtrendAccount", "CreationdtLbl")]
        public string Creationdt { get; set; }
		[DisplayNameLocalizedAttribute("CardtrendAccount", "SAPRefNoLbl")]
        public string SAPRefNo { get; set; }
        [StringLength(1000, ErrorMessage = "Maximum length is 1000 characters")]
	    [DisplayNameLocalizedAttribute("CardtrendAccount", "RemarksLbl")]
        public string remarks { get; set; }
        public string BgSerialNo { get; set; }
        public string acctNo { get; set; }
        public string ApplId { get; set; }

	    [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedQuantitativeDdl")]
        public string SelectedQuantitative { get; set; }
        public IEnumerable<SelectListItem> Quantitative { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedQualitativeDdl")]
        public string SelectedQualitative { get; set; }
        public IEnumerable<SelectListItem> Qualitative { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "SaleProposalLbl")]
        [StringLength(150, ErrorMessage = "Maximum length is 150 characters")]
        public string SaleProposal { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "RecommendCreditLimitLbl")]
        public string RecommendCreditLimit { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "ProposeCreditLimitLbl")]
        public string ProposeCreditLimit { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "RecommendSecurityAmtLbl")]
        public string RecommendSecurityAmt { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "ProposeSecurityAmtLbl")]
        public string ProposeSecurityAmt { get; set; }
        public IList<RemarkHistory> remarkHistory { set; get; }

        public IEnumerable<SelectListItem> TradingArea { get; set; }
		[DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedTradingAreaDdl")]
        public string SelectedTradingArea { get; set; }
    }
    public class UpToDateBal
    {
		[DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedAccountTypeDdl")]
        public string SelectedAccountType { get; set; }
        public IEnumerable<SelectListItem> AcctType { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "CreditLimitLbl")]
        public string CreditLimit { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "OpeningBalLbl")]
        public string OpeningBal { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "InstantAmtLbl")]
        public string InstantAmt { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "UnpostedAmtLbl")]
        public string UnpostedAmt { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "ClosingBalLbl")]
        public string ClosingBal { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "OpeningPtsBalLbl")]
        public string OpeningPtsBal { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "InstantPtsLbl")]
        public string InstantPts { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "UnpostedPtsLbl")]
        public string UnpostedPts { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "TempCreditLimitLbl")]
        public string TempCreditLimit { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "TotalCreditLimitLbl")]
        public string TotalCreditLimit { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "OnlineAmtLbl")]
        public string OnlineAmt { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "BatchAmtLbl")]
        public string BatchAmt { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "OfflineAmtLbl")]
        public string OfflineAmt { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "AvailableCredLimitLbl")]
        public string AvailableCredLimit { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "QuotaUsageLbl")]
        public string QuotaUsage { get; set; }

	    [DisplayNameLocalizedAttribute("CardtrendAccount", "QuotaLimitLbl")]
        public string QuotaLimit { get; set; }
    }
    public class RemarkHistory
    {
        public string Content { get; set; }
        public string UserId { get; set; }
        public string CreationDate { get; set; }
        public string TxnId { get; set; }
    }
    public class SKDS
    {
        public CardnAccNo _CardnAccNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "ApplIdLbl")]
        public string ApplId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "TransactionIdLbl")]
        public string TxnId { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SKDSNoLbl")]
        [StringLength(20, ErrorMessage = "Maximum length is 20 characters")]
        public string SKDSNo { get; set; }
		
		[DisplayNameLocalizedAttribute("CardtrendAccount", "SksdDespLbl")]
        public string SksdDesp { get; set; }
		[DisplayNameLocalizedAttribute("CardtrendAccount", "VehRegsNoLbl")]
        public string VehRegsNo { get; set; }
        //[DisplayName(@"Skds Effective Date")]
        //public string SkdsEffDt { get; set; }
		[DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedSkdsAcctSubDdl")]
        public string SelectedSkdsAcctSub { get; set; }
        public IEnumerable<SelectListItem> SkdsAcctSub { get; set; }
        [Required]
		[DisplayNameLocalizedAttribute("CardtrendAccount", "QuotaFromDateLbl")]
        public string QuotaFromDate { get; set; }
        [Required]
		[DisplayNameLocalizedAttribute("CardtrendAccount", "QuotaToDateLbl")]
        public string QuotaToDate { get; set; }
		[DisplayNameLocalizedAttribute("CardtrendAccount", "LastSubsidyDateLbl")]
        public string LastSubsidyDate { get; set; }
        [Required]
		[DisplayNameLocalizedAttribute("CardtrendAccount", "EffFromDateLbl")]
        public string EffFromDate { get; set; }
        [Required]
		[DisplayNameLocalizedAttribute("CardtrendAccount", "EffToDateLbl")]
        public string EffToDate { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "RemarksLbl")]
        public string Remarks { get; set; }
		[DisplayNameLocalizedAttribute("CardtrendAccount", "UserIdLbl")]
        public string UserId { get; set; }
		[DisplayNameLocalizedAttribute("CardtrendAccount", "SKDSLitreQuotaLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string SKDSLitreQuota { get; set; }
		
        [Required]
		[DisplayNameLocalizedAttribute("CardtrendAccount", "RefferenceLbl")]
        public string Refference { get; set; }		
		[DisplayNameLocalizedAttribute("CardtrendAccount", "LastUpdDateLbl")]
        public string LastUpdDate { get; set; }
		[DisplayNameLocalizedAttribute("CardtrendAccount", "CreationDateLbl")]
        public string CreationDate { get; set; }
		[DisplayNameLocalizedAttribute("CardtrendAccount", "SKDSRateLbl")]
        public string SKDSRate { get; set; }		
		[DisplayNameLocalizedAttribute("CardtrendAccount", "SKDSQuotaLbl")]
        public string SKDSQuota { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "SKDSDocXrefLbl")]
        public string SKDSDocXref { get; set; }
		[DisplayNameLocalizedAttribute("CardtrendAccount", "SKDSRefLbl")]
        public string SKDSRef { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedStsDdl")]
        public string SelectedSts { get; set; }
        public IEnumerable<SelectListItem> Sts { get; set; }
		[DisplayNameLocalizedAttribute("CardtrendAccount", "CardStsLbl")]
        public string CardSts { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "CardNoLbl")]
        public string CardNo { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedCategoryDdl")]
        public string SelectedCategory { get; set; }
        public IEnumerable<SelectListItem> Category { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedSubsidyTypeDdl")]
        public string SelectedSubsidyType { get; set; }
        public IEnumerable<SelectListItem> SubsidyType { get; set; }

		[DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedSubsidyLevelDdl")]
        public string SelectedSubsidyLevel { get; set; }
        public IEnumerable<SelectListItem> SubsidyLevel { get; set; }

    }

    public class VeloctyLimitListMaintModel
    {
        public CardnAccNo _CardnAccNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "RefKeyLbl")]
        public string RefKey { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "VelocityCounterLbl")]
        public string velocityCounter { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedProdCdDdl")]
        public string SelectedProdCd { get; set; }
        public string ProdCdDescp { get; set; }
        public IEnumerable<SelectListItem> ProdCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "VelocityLitreLbl")]
        public string VelocityLitre { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "DdlVelocityLitreLbl")]
        public Int64 ddlVelocityLitre { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "VelocityLimitLbl")]
        public string VelocityLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "DdlVelocityLimitLbl")]
        public string ddlVelocityLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CntrLimitLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? CntrLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SpentCntLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? SpentCnt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SpentLimitLbl")]
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string SpentLimit { get; set; }
        public string SpentAmt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SpentLitreLbl")]
        public string SpentLitre { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "LastTransactionDateLbl")]
        public string LastUpdateDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "UserIdLbl")]
        public string UserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CreationDateLbl")]
        public string CreationDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "ApplicationIdLbl")]
        public string ApplId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "AppcIdLbl")]
        public string AppcId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedVelocityIndDdl")]
        public string SelectedVelocityInd { get; set; }
        public IEnumerable<SelectListItem> VelocityInd { get; set; }
        public string VelocityIndDescp { get; set; }
        public string veVelocityCnt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedCtrlTypeDdl")]
        public string SelectedCtrlType { get; set; }
        public IEnumerable<SelectListItem> CtrlType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CostCentreLbl")]
        public string CostCentre { get; set; }
        public string CostCentreDescription { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedCorpCdDdl")]
        public string SelectedCorpCd { get; set; }
        public IEnumerable<SelectListItem> CorpCd { get; set; }

    }
    public class CardHolderInfoModel
    {
        public string[] ExcelHeader
        {

            get
            {
                return new string[] { "CardNo", "Emboss Name", "Current Status", "Card Expiry", "XRef CardNo", "CardType", "Pin Indicator", "Vehicle Reg.No", "SKDS No", " Driver Code", "Full Name", "Blocked Date", "Terminated Date", "Cost Centre" };
            }
        }
        public string[] ExcelBody()
        {
            return new string[] { CardNo, EmbossName, SelectedCurrentStatus, CardExpiry, XRefCardNo.ToString(), SelectedCardType, SelectedPINInd, vehRegNo, SelectedSKDSNo, DriverCd, FullName, BlockedDate, TerminatedDate, SelectedCostCentre };
        }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CardNoLbl")]
        [MinLength(16)]
        [MaxLength(19)]
        public string CardNo { get; set; }
        public string AcctNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "MemberSinceLbl")]
        public string MemberSince { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CreationDateLbl")]
        public string CreationDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "XrefCardNoLbl")]
        public string XRefCardNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "OldCardNoLbl")]
        public string oldCardNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "selectedPlasticTypeDdl")]
        public string SelectedPlasticType { get; set; }
        public IEnumerable<SelectListItem> PlasticType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedCardTypeDdl")]
        public string SelectedCardType { get; set; }
        public IEnumerable<SelectListItem> CardType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "FullNameLbl")]
        public string FullName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedCurrentStatusDdl")]
        public string SelectedCurrentStatus { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "EmbossNameLbl")]
        public string EmbossName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedReasonCdDdl")]
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedPINIndDdl")]
        public string SelectedPINInd { get; set; }
        public IEnumerable<SelectListItem> PINInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "PWLbl")]
        public string PVV { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "PINOffsetLbl")]
        public string PINOffset { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedPushAlertIndDdl")]
        public bool SelectedPushAlertInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedDialogueIndDdl")]
        public string SelectedDialogueInd { get; set; }
        public IEnumerable<SelectListItem> DialogueInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "VehRegNoLbl")]
        public string vehRegNo { get; set; }
        public IEnumerable<SelectListItem> VehicleModel { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedVehicleModelDdl")]
        public string SelectedVehicleModel { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "DriverCdLbl")]
        public string DriverCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "DriverNameLbl")]
        public string DriverName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "LocIndLbl")]
        public bool LocInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedLocCheckFlagChk")]
        public bool SelectedLocCheckFlag { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedAnnualFeeDdl")]
        public string SelectedAnnualFee { get; set; }
        public IEnumerable<SelectListItem> AnnualFee { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedJonFeeDdl")]
        public string SelectedJonFee { get; set; }
        public IEnumerable<SelectListItem> JonFee { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "MaxCountLimitLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int MaxCountLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "AmtLimitLbl")]
        public string AmtLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedFuelCheckFlagChk")]
        public bool SelectedFuelCheckFlag { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedFuelIndDdl")]
        public bool SelectedFuelInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedRenewalIndDdl")]
        public string SelectedRenewalInd { get; set; }
        public IEnumerable<SelectListItem> RenewalInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "FuelLitreLbl")]
        public string FuelLitre { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "PINExceedCntLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? PINExceedCnt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "PINAttemptedLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? PINAttempted { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "EntityIdLbl")]
        public string EntityId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CardExpiryLbl")]
        public string CardExpiry { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "TerminatedDateLbl")]
        public string TerminatedDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "BlockedDateLbl")]
        public string BlockedDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SkdsQuotaLbl")]
        public string SKDSQuota { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedSKDSIndChk")]
        public bool SelectedSKDSInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedSKDSNoDdl")]
        public string SelectedSKDSNo { get; set; }
        public IEnumerable<SelectListItem> SKDSNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "OdometerIndicatorLbl")]
        public bool OdometerIndicator { get; set; }
        public PersonInfoModel _PersoninfoModel { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedProductUtilizationDdl")]
        public string SelectedProductUtilization { get; set; }
        public IEnumerable<SelectListItem> ProductUtilization { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "PrimaryCardLbl")]
        public bool PrimaryCard { get; set; }
        public string SelectedAnnualFeeCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedCostCentreDdl")]
        public string SelectedCostCentre { get; set; }
        public IEnumerable<SelectListItem> CostCentre { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedBranchCdDdl")]
        public string SelectedBranchCd { get; set; }
        public IEnumerable<SelectListItem> BranchCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedDivisionCodeDdl")]
        public string SelectedDivisionCode { get; set; }
        public IEnumerable<SelectListItem> DivisionCode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedDeptCdDdl")]
        public string SelectedDeptCd { get; set; }
        public IEnumerable<SelectListItem> DeptCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedProductGroupDdl")]
        public string SelectedProductGroup { get; set; }
        public IEnumerable<SelectListItem> ProductGroup { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedCardMediaDdl")]
        public string SelectedCardMedia { get; set; }
        public IEnumerable<SelectListItem> CardMedia { get; set; }
    }

    public class PersonInfoModel
    {
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "EntityIdLbl")]
        public string EntityId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedTitleDdl")]
        public string SelectedTitle { get; set; }
        public IEnumerable<SelectListItem> title { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "FirstNameLbl")]
        public string FirstName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "LastNameLbl")]
        public string LastName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "IdNoLbl")]
        public string IdNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedIdTypeDdl")]
        public string SelectedIdType { get; set; }
        public IEnumerable<SelectListItem> IdType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "AltIdNoLbl")]
        public string AltIdNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedAltIdTypeDdl")]
        public string SelectedAltIdType { get; set; }
        public IEnumerable<SelectListItem> AltIdType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedgenderDdl")]
        public string Selectedgender { get; set; }
        public IEnumerable<SelectListItem> gender { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "DOBLbl")]
        public string DOB { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "AnnualIncomeLbl")]
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string AnnualIncome { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedOccupationDdl")]
        public string SelectedOccupation { get; set; }
        public IEnumerable<SelectListItem> Occupation { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedDeptIdDdl")]
        public string SelectedDeptId { get; set; }
        public IEnumerable<SelectListItem> DeptId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "DrivingLicenseLbl")]
        public string DrivingLicense { get; set; }
    }
    public class VehiclesListModel
    {
        public string[] ToCsv()
        {
            return new string[] { CardNo, VehRegtNo, SelectedVehModel, VehRegDate, SelectedVehType, OdoMeterReading, OdoMeterUpdate, SelectedSts, PolicyExpDate, XrefCardNo, SelectedCardType, SkdsQuota };
        }
        public string[] CsvHeader()
        {
            return new string[] { "Card No", "Vehicle Regn.No", "Vehicle Model", "Vehicle Regn.Date", "Vehicle Type", "Odometer Reading", "Odometer Update", "Status", "Policy Expiry Date", "XRef Card No", "Card Type", "SKDS Quota" };
        }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CardNoLbl")]
        [RegularExpression(@"^[0-9]{16,19}$", ErrorMessage = "Card No Range = 16 to 19 digit")]
        public string CardNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedCardTypeDdl")]
        public string SelectedCardType { get; set; }
        public IEnumerable<SelectListItem> CardType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CardTerminatedLbl")]
        public string CardTerminated { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CardExpiryLbl")]
        public string CardExpiry { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "VehRegtNoLbl")]
        public string VehRegtNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedVehMakerDdl")]
        public string SelectedVehMaker { get; set; }
        public IEnumerable<SelectListItem> VehMaker { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedVehModelDdl")]
        public string SelectedVehModel { get; set; }
        public IEnumerable<SelectListItem> VehModel { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "VehRegDateLbl")]
        public string VehRegDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedVehYrDdl")]
        public string SelectedVehYr { get; set; }
        public IEnumerable<SelectListItem> VehYr { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedVehTypeDdl")]
        public string SelectedVehType { get; set; }
        public IEnumerable<SelectListItem> VehType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedVehColorDdl")]
        public string SelectedVehColor { get; set; }
        public IEnumerable<SelectListItem> VehColor { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "OdoMeterReadingLbl")]
        public string OdoMeterReading { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "OdoMeterUpdateLbl")]
        public string OdoMeterUpdate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "RoadTaxExpDateLbl")]
        public string RoadTaxExpDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "RoadTaxAmtLbl")]
        public string RoadTaxAmt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "RenewalPeriodLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? RenewalPeriod { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "InsurerCdLbl")]
        public string InsurerCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "PremiumAmtLbl")]
        public string PremiumAmt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "PinLbl")]
        public string pin { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "PolicyNoLbl")]
        public string PolicyNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "PolicyExpDateLbl")]
        public string PolicyExpDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "PolicyAmtLbl")]
        public string PolicyAmt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedCardStsLbl")]
        public string SelectedSts { get; set; }
        public IEnumerable<SelectListItem> Sts { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SkdsIndLbl")]
        public bool SkdsInd { get; set; }
        public string RawSKDSInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SkdsQuotaLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string SkdsQuota { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "VehicleServiceDateLbl")]
        public string VehicleServiceDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "XrefCardNoLbl")]
        [RegularExpression(@"^[0-9]{16,19}$", ErrorMessage = "XrefCardNo Range = 16 to 19 digit")]
        public string XrefCardNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "DescpriptionLbl")]
        public string Descp { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "AppcCardId")]
        public string AppcId { get; set; }
    }

    public class ProdAcceptListModel
    {
        [DisplayName("Reference Key")]
        public string RefKey { get; set; }
        [DisplayName("Product Group")]
        public string ProdGroup { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }

    }

    public class LocationAcceptListModel
    {
        public CardnAccNo _CardnAccNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "BusnLocationLbl")]
        public string MerchantId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "UserIdLbl")]
        public string UserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "CreationDateLbl")]
        public string CreationDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedStatesDdl")]
        public List<string> SelectedStates { get; set; }
        public IEnumerable<SelectListItem> State { get; set; }
        public string s_state { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "BusnLocationLbl")]
        public string BusnLoc { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedBusnLocationsDdl")]
        public List<string> SelectedBusnLocations { get; set; }
        public IEnumerable<SelectListItem> BusnLocations { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "DBANameLbl")]
        public string DBAName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SiteIdLbl")]
        public string SiteId { get; set; }
        public string busnName { get; set; }
    }

    public class AddrListMaintModel
    {
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "RefKeyLbl")]
        public string SelectedRefCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedAddrTypeDdl")]
        public string SelectedAddrType { get; set; }
        public IEnumerable<SelectListItem> addrtype { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "MainMailingIndLbl")]
        public bool MainMailingInd { get; set; }
        public string SelectedMailInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "AddressLbl")]
        [StringLength(100, ErrorMessage = "Maximum length is 100 characters")]
        public string Address { get; set; }
        [StringLength(100, ErrorMessage = "Maximum length is 100 characters")]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "DistrictLbl")]
        public string District { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CityLbl")]
        [StringLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string City { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedstateDdl")]
        public string Selectedstate { get; set; }
        public IEnumerable<SelectListItem> State { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "PostalCodeLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        [StringLength(5, ErrorMessage = "Maximum length is 5 characters")]
        public string PostalCode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedRegionDdl")]
        public string selectedregion { get; set; }
        public IEnumerable<SelectListItem> region { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedCountryDdl")]
        public string SelectedCountry { get; set; }
        public IEnumerable<SelectListItem> Country { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "UserIdLbl")]
        public string UserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CreationDateLbl")]
        public string CreationDate { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "Address1Lbl")]
        [StringLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string Address1 { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "Address2Lbl")]
        [StringLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string Address2 { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "Address3Lbl")]
        [StringLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string Address3 { get; set; }//changed       
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "Address4Lbl")]
        [StringLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string Address4 { get; set; }//changed
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "Address5Lbl")]
        [StringLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string Address5 { get; set; }//changed
        public string RefKey { get; set; }
        public string RefTo { get; set; }
    }

    public class TempCreditCtrlModel
    {
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "ExpDateLbl")]
        public string ExpDate { get; set; }
        public string ExpDateTo { get; set; }
        public string ExpDateFrom { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "AllowCreditLimitLbl")]
        public string AllowCreditLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "UserIdLbl")]
        public string UserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CreationDateLbl")]
        public string CreationDate { get; set; }
        public string EventType { get; set; }
        public string EventDate { get; set; }
        public string Status { get; set; }
        public string CardNo { get; set; }
        public string ReasonCd { get; set; }
        public string CloseDate { get; set; }
        public string RecallDate { get; set; }
        public string SystemInd { get; set; }
        public string EventId { get; set; }

    }

    public class MiscellaneousInfoModel
    {
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "ApplicationIdLbl")]
        public string ApplId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "AuthNameLbl")]
        public string AuthName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedDesignationDdl")]
        public string SelectedDesignation { get; set; }
        public IEnumerable<SelectListItem> Designation { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "AuthDateLbl")]
        public string AuthDate { get; set; }
    }


    public class ContactLstModel
    {
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "RefKeyLbl")]
        public string RefKey { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedContactTypeDdl")]
        public string SelectedContactType { get; set; }
        public IEnumerable<SelectListItem> ContactType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "RefCdLbl")]
        public string RefCd { get; set; }
        public string RefTo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "ContactNoLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        [StringLength(16, ErrorMessage = "Maximum lengthis 16 characters")]
        public string ContactNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "ContactNameLbl")]
        [StringLength(50, ErrorMessage = "Maximum lengthis 50 characters")]
        public string ContactName { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "EmailAddrLbl")]
        [RegularExpression((@"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$"), ErrorMessage = "Not a valid Email Address")]
        [StringLength(80, ErrorMessage = "Maximum lengthis 80 characters")]
        public string EmailAddr { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedStsDdl")]
        public string SelectedSts { get; set; }
        public IEnumerable<SelectListItem> Sts { get; set; }
        public string RawStatus { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "UserIdLbl")]
        public string UserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CreationDateLbl")]
        public string CreationDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedOccupationDdl")]
        public string SelectedOccupation { get; set; }
        public IEnumerable<SelectListItem> Occupation { get; set; }
        public string RawOccupation { get; set; }
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string Fax { get; set; }
    }

    public class ChangeStatus
    {
        public CardnAccNo _CardnAccNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedCurrentStatusDdl")]
        public string SelectedCurrentStatus { get; set; }
        public IEnumerable<SelectListItem> CurrentStatus { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedChangeStatusToDdl")]
        public string SelectedChangeStatusTo { get; set; }
        public IEnumerable<SelectListItem> ChangeStatusTo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedReasonCodeDdl")]
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "RemarksLbl")]
        public string Remarks { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "SelectedRefTypeDdl")]
        public string SelectedRefType { get; set; }
        public IEnumerable<SelectListItem> RefType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "RefIdLbl")]
        public string RefId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "MerchAcctNoLbl")]
        public string MerchAcctNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "BusnLocationLbl")]
        public string BusnLocation { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendCardHolder", "AppcIdLbl")]
        public string AppcId { get; set; }
    }
    public class AcctGuarantee
    {
        public CardnAccNo _CardnAccNo { get; set; }
        [DisplayName("Application Id")]
        public string ApplId { get; set; }
        [DisplayName("Page")]
        public char Page { get; set; }
        [DisplayName("Deposit Type")]
        public string DepositType { get; set; }
        [DisplayName("Deposit Amount")]
        //decimalvalidationbug
        public string DepositAmt { get; set; }
        [DisplayName("Bank Name")]
        public string SelectedBankCd { get; set; }
        public IEnumerable<SelectListItem> BankCd { get; set; }
        [DisplayName("Bank Account No")]
        public string BankAcctNo { get; set; }
        [DisplayName("Bank Branch Cd")]
        public string BankBranchCd { get; set; }
        [DisplayName("From Date")]
        public string EffFromDate { get; set; }
        [DisplayName("To Date")]
        public string EffToDate { get; set; }
        [DisplayName("Last Update Date")]
        public string LastUpdDate { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        [DisplayName("Txn Id")]
        public string TxnId { get; set; }

    }

    public class AcctPostedTxnSearch
    {
        public string[] ExcelHeader
        {
            get
            {

                return new string[] { "Statement Date", "Transaction Date", "Posting Date", "Account No.", "Card No.", "Auth Card No.", "Txn Description", "Vehicle Reg. No.", "Stan", "Approve Code", "RRN",
                    "VAT No.", "Dealer", "Txn Id", "Total Amount","Product Description","Quantity","Product Amount", "VAT Amount","VAT Code","VAT Rate" };
            }

        }
        public string[] ExcelBody()
        {
            return new string[] { InvoicDt, TxnDate, this.PrcsDate, AcctNo, this.SelectedCardNo, this.AuthCardNo, this.TxnDesp, this.VehRegNo, this.Stan, this.ApproveCd, this.RRn, this.VATNo, this.Dealer, this.TxnId, this.TxnAmt, this.ProductDescp, this.Quantity, this.ProductAmt, this.VATAmt, this.VATCd, this.VATRate };
        }


        [DisplayName("Account No")]
        public string AcctNo { get; set; }
        [DisplayName("Card No")]
        public string SelectedCardNo { get; set; }
        public IEnumerable<SelectListItem> CardNo { get; set; }
        [DisplayName("Transaction Category")]
        public string SelectedTxnCategory { get; set; }
        public IEnumerable<SelectListItem> TxnCategory { get; set; }
        [DisplayName("Transaction Code")]
        public string SelectedTxnCd { get; set; }
        public IEnumerable<SelectListItem> TxnCd { get; set; }
        [DisplayName("Transaction Date")]
        public string TxnDate { get; set; }
        [DisplayName("Transaction Description")]
        public string TxnDesp { get; set; }
        [DisplayName("Transaction Amount")]
        public string TxnAmt { get; set; }
        [DisplayName("Dealer")]
        public string Dealer { get; set; }
        [DisplayName("Term Id")]
        public string TermId { get; set; }
        [DisplayName("Approve Code")]
        public string ApproveCd { get; set; }
        [DisplayName("Authorised Card No")]
        public string AuthCardNo { get; set; }
        [DisplayName("Prcs Date")]
        public string PrcsDate { get; set; }
        [DisplayName("Transaction Id")]
        public string TxnId { get; set; }
        [Required]
        [DisplayName("From Date")]
        public string FromDate { get; set; }
        [Required]
        [DisplayName("To Date")]
        public string ToDate { get; set; }
        [DisplayName("Invoice Date")]
        public string InvoicDt { get; set; }
        [DisplayName("Receipt ID")]
        public string RecieptId { get; set; }

        public string Batch { get; set; }
        public string VehRegNo { get; set; }
        public string DriverName { get; set; }
        public string SiteId { get; set; }
        public string TotalTxnAmt { get; set; }
        public string Quantity { get; set; }

        public string TaxInvoiceNo { get; set; }
        public string VATAmt { get; set; }
        public string BaseAmt { get; set; }
        //public string AppvCd { get; set; }
        public string RRn { get; set; }
        public string ProductDescp { get; set; }
        public string Stan { get; set; }
        public string VATNo { get; set; }
        public string ProductAmt { get; set; }
        public string VATCd { get; set; }
        public string VATRate { get; set; }

    }
    public class MerchPostedTxnSearch
    {


        public string[] ExcelHeader
        {
            get
            {
                return new string[] { "Dealer", "Term Batch", "Txn Date", "Card No", "Txn Description", "Txn Amount", "Term Id", "Auth No", "Auth Card No", "Process Date", "Txn Id", "Product Description", "Quantity", "Product Amount", "VAT Amount", "Base Amount", "VAT Code", "VAT Rate" };
            }
        }
        public string[] ExcelBody()
        {
            return new string[] { SelectedDealer, TermBatch, TxnDate, cardNo, TxnDesp, TxnAmt, TermId, AuthNo, AuthCardNo, PrcsDate, TxnId, ProductDescp, this.ProductQty, this.ProductAmt, this.VATAmt, this.BaseAmt, this.VATCd, this.VATRate };
        }

        [DisplayName("Account No")]
        public string AcctNo { get; set; }
        [DisplayName("Business Location")]
        public string BusnLocation { get; set; }
        [DisplayName("Transaction Code")]
        public string SelectedTxnCd { get; set; }
        public IEnumerable<SelectListItem> TxnCd { get; set; }
        [DisplayName("Transaction Description")]
        public string TxnDesp { get; set; }
        [Required]
        [DisplayName("Transaction Date")]
        public string TxnDate { get; set; }
        [DisplayName("Dealer")]
        public string SelectedDealer { get; set; }
        public IEnumerable<SelectListItem> Dealer { get; set; }
        [DisplayName("Term Batch")]
        public string TermBatch { get; set; }
        [DisplayName("Card No")]
        public string cardNo { get; set; }
        [DisplayName("Transaction Amount")]
        public string TxnAmt { get; set; }
        [DisplayName("Billing Amount")]
        public string BillingAmt { get; set; }

        [DisplayName("Term Id")]
        public string TermId { get; set; }
        [DisplayName("Authorised  No")]
        public string AuthNo { get; set; }
        [DisplayName("Authorised Card No")]
        public string AuthCardNo { get; set; }
        [DisplayName("Prcs Date")]
        public string PrcsDate { get; set; }
        [DisplayName("Transaction Id")]
        public string TxnId { get; set; }
        public string ProductQty { get; set; }
        public string ProductAmt { get; set; }
        public string VATAmt { get; set; }
        public string BaseAmt { get; set; }
        public string VATCd { get; set; }
        public string VATRate { get; set; }
        public string ProductDescp { get; set; }

    }



    public enum TxnSearchType
    {
        Acct,
        Merch
    }

    public class TxnSearchModel
    {
        public TxnSearchType SearchType { get; set; }
        [DisplayName("Account No")]
        public string AcctNo { get; set; }
        [DisplayName("Merchant Account No")]
        public string MerchAcctNo { get; set; }
        [DisplayName("Card No")]
        public string CardNo { get; set; }
        [DisplayName("Transaction Category")]
        public string SelectedTxnCategory { get; set; }
        public IEnumerable<SelectListItem> TxnCategory { get; set; }
        [DisplayName("Transaction Code")]
        public string SelectedTxnCd { get; set; }
        public IEnumerable<SelectListItem> TxnCd { get; set; }
        [Required]
        [DisplayName("From Date")]
        public string FromDate { get; set; }
        [Required]
        [DisplayName("To Date")]
        public string ToDate { get; set; }
        [DisplayName("Statement Date")]//Refer: MYPDBFPM-159
        public string SelectedStatementDate { get; set; }
        public IEnumerable<SelectListItem> StatementDate { get; set; }
        [DisplayName("Business Location")]
        public string BusnLocation { get; set; }
        [DisplayName("Merchant Transaction Code")]
        public string SelectedMerchTxnCd { get; set; }
        public IEnumerable<SelectListItem> MerchTxnCd { get; set; }
        [Required]
        public string MerchFromDate { get; set; }
        [Required]
        public string MerchToDate { get; set; }
    }

    public class PaymentTxn
    {
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "PyTxnIdLbl")]
        public string PyTxnId { get; set; }
        public CardnAccNo _CardnAccNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedPyTxnCdDdl")]
        public string SelectedPyTxnCd { get; set; }
        public IEnumerable<SelectListItem> PyTxnCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedTxnTypeDdl")]
        public string SelectedTxnType { get; set; }
        public IEnumerable<SelectListItem> TxnType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "DbCrIndLbl")]
        public string DbCrInd { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "TxnDateLbl")]
        public string TxnDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedAffiliateDdl")]
        public string SelectedAffiliate { get; set; }
        public IEnumerable<SelectListItem> Affiliate { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "TotAmntLbl")]
        public string TotAmnt { get; set; }
        public string displayTotAmnt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "BillingTxnAmtLbl")]
        public string BillingTxnAmt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "TotptsLbl")]
        public string Totpts { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "AccountLbl")]
        public string Account { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "DescpLbl")]
        public string Descp { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "InvoiceNoLbl")]
        public string InvoiceNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CollectionIdLbl")]
        public string CollectionId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "RefTypeLbl")]
        public string RefType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "RefIdLbl")]
        public string RefId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "UserIdLbl")]
        public string UserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "AcctNoLbl")]
        public string AcctNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CardNoLbl")]
        public string CardNo { get; set; }
        public string CreationDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "EndorsedByLbl")]
        public string EndorsedBy { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "EndorsedDateLbl")]
        public string EndorsedDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "BookingDtLbl")]
        public string BookingDt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "MovementDtLbl")]
        public string MovementDt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "DueDtLbl")]
        public string DueDt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "BalDateLbl")]
        public string BalDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "TARBalLbl")]
        public string TARBal { get; set; }
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "WithHeldUnsettleIdLbl")]
        public Int32 WithHeldUnsettleId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "DeftBusnLocationLbl")]
        public string DeftBusnLocation { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "DeftTermIdLbl")]
        public string DeftTermId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CheqNoLbl")]
        public string CheqNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedStatusDdl")]
        public string SelectedSts { get; set; }
        public string StsDescp { get; set; }
        public IEnumerable<SelectListItem> Sts { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedAppvCdDdl")]
        public string SelectedAppvCd { get; set; }
        public IEnumerable<SelectListItem> AppvCd { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedOwnerDdl")]
        public string selectedOwner { get; set; }
        public IEnumerable<SelectListItem> Owner { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "RemarksLbl")]
        public string remarks { get; set; }
        public string AppRemarks { get; set; }
        public string TxnId { get; set; }
        public string DueDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedTxnCdDdl")]
        public string SelectedTxnCd { get; set; }
    }

    public class BillingItem
    {
        [DisplayName("Txn Id")]
        public Int64? TxnId { get; set; }
        public CardnAccNo _CardnAccNo { get; set; }
        public CreationDatenUserId _CreationDatenUserId { get; set; }
        [DisplayName("From Date")]
        public string FromDate { get; set; }
        [DisplayName("To Date")]
        public string ToDate { get; set; }
        [DisplayName("Txn Category")]
        public string SelectedTxnCategory { get; set; }
        public IEnumerable<SelectListItem> TxnCategory { get; set; }
        [DisplayName("Status")]
        public string SelectedSts { get; set; }
        public IEnumerable<SelectListItem> Sts { get; set; }
        public string ClosedDate { get; set; }
        [DisplayName("Txn Date")]
        public string TxnDate { get; set; }
        [DisplayName("Booking Date")]
        public string BookingDate { get; set; }
        [DisplayName("Prcs Date")]
        public string PrcsDate { get; set; }
        [DisplayName("Billing Txn Amount")]
        public string BillingTxnAmt { get; set; }
        public string DisplayBillingTxnAmt { get; set; }
        [DisplayName("Description")]
        public string Descp { get; set; }
        [DisplayName("Dealer")]
        public string BusnLocation { get; set; }
        [DisplayName("Terminal Id")]
        public string TermId { get; set; }
        public string TarBalance { get; set; }
        [DisplayName("Settled Amount")]
        public string SettledAmt { get; set; }
        public string DisplaySettledAmt { get; set; }
        [DisplayName("Settled Date")]
        public string SettledDate { get; set; }
        [DisplayName("Document Ref No")]
        public string DocRefNo { get; set; }
        [DisplayName("Txn Code")]
        public string TxnCd { get; set; }
        [DisplayName("Ref Id")]
        public string RefId { get; set; }
        [DisplayName("Due Date")]
        public string DueDate { get; set; }
        public string Level { get; set; }
        public string TotalTxnAmount { get; set; }
        public string TotalSettledAmt { get; set; }
    }

    public class OnlineTransaction
    {
        public string Ids { get; set; }
        public string BillingAmt { get; set; }
        public string TxnId { get; set; }
        public string Sts { get; set; }
        public string CardNo { get; set; }
        public string TxnAmt { get; set; }
        public string TxnDate { get; set; }
        public string DueDate { get; set; }
        public string Descp { get; set; }
        public string UserId { get; set; }
        public string BusnLocation { get; set; }
        public string TermId { get; set; }
        public Int64? InvoiceNo { get; set; }
        public string TxnInd { get; set; }
        public Int32? Mti { get; set; }
        public Int32? PrcsCd { get; set; }
        public string CreationDate { get; set; }
        public string Rrn { get; set; }
        public Int32? TxnCd { get; set; }
    }

    public class FinancilInfoItemsList
    {
        public string TxnId { get; set; }
        public string Lbe { get; set; }
        public string CardNo { get; set; }
        public string TxnAmt { get; set; }
        public string BookingDate { get; set; }
        public string TxnDate { get; set; }
        public string DueDate { get; set; }
        public string Descp { get; set; }
        public string UserId { get; set; }
        public string CreationDate { get; set; }
        public string RcptNo { get; set; }
        public string TxnCd { get; set; }
        public string ShortDescp { get; set; }
        public string SiteId { get; set; }
        public string DriverCardNo { get; set; }
    }

    public class AccountUser
    {
        public string AcctNo { get; set; }
        public string CompanyName { get; set; }
        public string Username { get; set; }
        public string PrivilegeCd { get; set; }
        public string Status { get; set; }
    }

    public class ResendAccountMail
    {
        public string AcctNo { get; set; }
        public string CorpCd { get; set; }
        public string UserId { get; set; }
        public string ContentId { get; set; }
        public string Name { get; set; }
        public string Bcc { get; set; }
        public string Cc { get; set; }
    }
    public class ProductDiscount
    {
        public IEnumerable<SelectListItem> ProdCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedProdCdDdl")]
        public string SelectedProdCd { get; set; }
        public string ProdCdDescp { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "EffDateFromLbl")]
        public string EffDateFrom { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "EffDateToLbl")]
        public string EffDateTo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CreatedByLbl")]
        public string CreatedBy { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CreationDateLbl")]
        public string CreationDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "TxnidLbl")]
        public string TxnId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "TxnCdLbl")]
        public string TxnCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedDiscPlanDdl")]
        public string SelectedDiscPlan { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedRebatePlanDdl")]
        public string SelectedRebatePlan { get; set; }
        public IEnumerable<SelectListItem> DiscPlan { get; set; }
        public IEnumerable<SelectListItem> RebatePlan { get; set; }
        public IEnumerable<SelectListItem> ProdDiscType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedProdDiscTypeDdl")]
        public string SelectedProdDiscType { get; set; }
        public string ProdDiscDescp { get; set; }
        public IEnumerable<SelectListItem> PlanId { get; set; }//webgetplan
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedPlanIdDdl")]
        public string SelectedPlanId { get; set; }
        public string Remarks { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CompanyNameLbl")]
        public string CompanyName { get; set; }
        public string Id { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "OnlineIndicatorLbl")]
        public bool OnlineIndicator { get; set; }
    }

    public class PointAdjustment
    {

        [Display(Name = "Txn Type")]
        public string TxnType { get; set; }
        [Display(Name = "Account No")]
        public string AccountNo { get; set; }
        [Display(Name = "Card No")]
        public string CardNo { get; set; }
        public string Points { get; set; }
        [Display(Name = "User Id")]
        public string UserId { get; set; }
        [Display(Name = "Txn Date")]
        public string TxnDate { get; set; }
        [Display(Name = "Due Date")]
        public string DueDate { get; set; }
        [Display(Name = "Booking Date")]
        public string BookingDate { get; set; }
        [Display(Name = "Txn Amount")]
        public string TxnAmt { get; set; }
        [Display(Name = "Cheque No")]
        public int ChequeNo { get; set; }
        [Display(Name = "Approval Code")]
        public string ApprvCd { get; set; }
        [Display(Name = "Receipt No")]
        public string ReceiptNo { get; set; }
        public string ReturnCd { get; set; }
        [Display(Name = "Withhold Unsettle Id")]
        public string WithHeldUnsettleId { get; set; }
        [Display(Name = "Txn Description")]
        public string TxnDescription { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        [Display(Name = "Status")]
        public string SelectedStatus { get; set; }
        [Display(Name = "Txn Code")]
        public string SelectedTxnCd { get; set; }
        [Display(Name = "Txn Id")]
        public IEnumerable<SelectListItem> TxnCd { get; set; }
        public string TxnId { get; set; }
        public string WUId { get; set; }
        [Display(Name = "Creation Date")]
        public string CreationDate { get; set; }
    }
    public class CostCentre
    {
        public string[] XlsHeader()
        {
            return new string[] { "Cost Centre", "Description", "Person in Charge" };
        }
        public string[] XlsBody()
        {
            return new string[] { SelectedCostCentre, Descp, PersoninCharge };
        }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedCostCentreDdl")]
        public string SelectedCostCentre { get; set; }
        public IEnumerable<SelectListItem> CostCentreX { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "PersoninChargeLbl")]
        public string PersoninCharge { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "DescpLbl")]
        public string Descp { get; set; }
        public string RefKey { get; set; }
        public string RefTo { get; set; }
    }

    public class Pukal
    {
        [Display(Name = "Company ID")]
        public string CompanyId { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Registration Date")]
        public string RegDate { get; set; }
        [Display(Name = "Termination Flag")]
        public string TerminationFlag { get; set; }
        public IEnumerable<SelectListItem> TermFlag { get; set; }
        [Display(Name = "AG Code")]
        public string SelectedAcctOfficeCode { get; set; }
        public IEnumerable<SelectListItem> AcctOfficeCode { get; set; }
        [Display(Name = "Warrant Department")]
        public string WarrantDepartment { get; set; }
        [Display(Name = "Warrant PTJ")]
        public string WarrantPtj { get; set; }
        [Display(Name = "Vote Code")]
        public string VoteCode { get; set; }
        [Display(Name = "Sort Key")]
        public string SortKey { get; set; }
        [Display(Name = "PRG")]
        public string Prg { get; set; }
        public string Project { get; set; }
        [Display(Name = "Updated By")]
        public string UpdatetedBy { get; set; }
        public IEnumerable<SelectListItem> BulkEntity { get; set; }
        [Display(Name = "Bulk Entity(Ref Cd)")]
        public string SelectedRefCd { get; set; }
        public string SelectedBulkEntity { get; set; }
        public IEnumerable<SelectListItem> RefCd { get; set; }

        [Display(Name = "Confirmation Regn. Flag")]
        public bool ConfirmationRegnFlag { get; set; }
        [Display(Name = "Termination Date")]
        public string TerminationDate { get; set; }

        [Display(Name = "Charge Department")]
        public string ChargeDepartment { get; set; }

        [Display(Name = "Charge PTJ")]
        public string ChargePtj { get; set; }

        [Display(Name = "AG Object Code")]
        public string SelectedAgObjectCode { get; set; }
        public IEnumerable<SelectListItem> AgObjectCode { get; set; }
        [Display(Name = "Last Updated")]
        public string LastUpdateDate { get; set; }
        [Display(Name = "Prg/Act/Amanah")]
        public string ProgramCd { get; set; }
        [Display(Name = "Project Code")]
        public string RefTo { get; set; }
        public string RefKey { get; set; }

        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Display(Name = "Registration Date")]
        public string creationDate { get; set; }
        public string createdBy { get; set; }

        [Display(Name = "Activation Date")]
        public string ActivationDate { get; set; }

        [Display(Name = "Last Updated")]
        public string LastUpdated { get; set; }

        [Display(Name = "Project/Setia")]
        public string ProjCd { get; set; }

        public int acctNo { get; set; }
        [Display(Name = "Check Digit Indicator")]
        public bool checkIndicator { get; set; }
    }

    public class PointBalance
    {
        public string AcctNo { get; set; }
        public string PointBal { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescp { get; set; }

    }

}
