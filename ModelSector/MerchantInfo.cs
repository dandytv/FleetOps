using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CCMS.ModelSector;
using ModelSector.Helpers;

namespace ModelSector
{

    public class MerchantInfo
    {
        public AccountDetails _AccountDetails { get; set; }

    }

    public class MerchantDetails
    {
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "AcctNoLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string AcctNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "AutoDebitIndLbl")]
        public bool AutoDebitInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "MerchantNoLbl")]
        public string MerchantNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "BusinessNameLbl")]
        public string BusinessName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "selectedCurrentStatusDdl")]
        public string selectedCurrentStatus { get; set; }
        public IEnumerable<SelectListItem> CurrentStatus { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "EntityIdLbl")]
        public string EntityId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SiteIdLbl")]
        public string SiteId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "TaxIdLbl")]
        public string TaxId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "TxnCodeDdl")]
        public IEnumerable<SelectListItem> TxnCode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedTaxCodeDdl")]
        public string SelectedTaxCode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "TaxRegistrationNameLbl")]
        public string TaxRegistrationName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CoRegNoLbl")]
        public string CoRegNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CoRegNameLbl")]
        [StringLength(100, ErrorMessage = "Maximum lengthis 100 characters")]
        public string CoRegName { get; set; }
        
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CoRegDateLbl")]
        public string CoRegDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "VATRateLbl")]
        public string VATRate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "AgreementNoLbl")]
        public string AgreementNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "AgreementDateLbl")]
        public string AgreementDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "PersonInChargeLbl")]
        public string PersonInCharge { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "selectedOwnershsipDdl")]
        public string selectedOwnershsip { get; set; }
        public IEnumerable<SelectListItem> Ownership { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "OwnershipTrsfDateLbl")]
        public string OwnershipTrsfDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "OwnershipToLbl")]
        public string OwnershipTo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedMCCDdl")]
        public string SelectedMCC { get; set; }
        public IEnumerable<SelectListItem> MCC { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedSICDdl")]
        public string SelectedSIC { get; set; }
        public IEnumerable<SelectListItem> SIC { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "DBANameLbl")]
        [StringLength(50, ErrorMessage = "Maximum lengthis 50 characters")]
        public string DBAName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectDBACityDdl")]
        public string SelectDBACity { get; set; }
        public IEnumerable<SelectListItem> DBACity { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectDBARegionDdl")]
        public string SelectDBARegion { get; set; }
        public IEnumerable<SelectListItem> DBARegion { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectDBAStateDdl")]
        public string SelectDBAState { get; set; }
        public IEnumerable<SelectListItem> DBAState { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "PayeeNameLbl")]
        [StringLength(50, ErrorMessage = "Maximum lengthis 50 characters")]
        public string PayeeName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedBankAcctNameDdl")]
        [StringLength(10, ErrorMessage = "Maximum lengthis 10 characters")]
        public string SelectedBankAcctName { get; set; }
        public IEnumerable<SelectListItem> BankAcctName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "BankAccountNoLbl")]
        public string BankAccountNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "selectedBankAcctTypeDdl")]
        public string selectedBankAcctType { get; set; }
        public IEnumerable<SelectListItem> BankAcctType { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedBankBranchCodeDdl")]
        public string SelectedBankBranchCode { get; set; }
        public IEnumerable<SelectListItem> BankBranchCode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "BankDirectDebitIndicatorLbl")]
        public string BankDirectDebitIndicator { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "UserIDLbl")]
        public string UserID { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CreationDateLbl")]
        public string CreationDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "LastUpdDateLbl")]
        public string LastUpdDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CancelDateLbl")]
        public string CancelDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "EndorsedByUserIdLbl")]
        public string EndorsedByUserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "EndorsedDateLbl")]
        public string EndorsedDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "BusnSizeLbl")]
        public string BusnSize { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "WithholdingTaxIndLbl")]
        public bool WithholdingTaxInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "WithholdingTaxRateLbl")]
        public string WithholdingTaxRate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedcycNoDdl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string SelectedcycNo { get; set; }
        public IEnumerable<SelectListItem> cycNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "PartnerRefNoLbl")]
        public string PartnerRefNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedReasonCodeDdl")]
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "StmtPrintIndLbl")]
        public bool StmtPrintInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "BusnLocLbl")]
        [Required]
        public string BusnLoc { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedPaymtModeDdl")]
        public string SelectedPaymtMode { get; set; }
        public IEnumerable<SelectListItem> PaymtMode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "WithholdPaymtFlagLbl")]
        public bool WithholdPaymtFlag { get; set; }
        public string PaymentTrans { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "PaymentTermsLbl")]
        public string PaymentTerms { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "TopupLimitLbl")]
        public string TopupLimit { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "TopupAmtLbl")]
        public string TopupAmt { get; set; }
        public string TaxCategory { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedDBARegionDdl")]
        public string SelectedDBARegion { get; set; }
        public IEnumerable<SelectListItem> DBARegions { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedAreaCodeDdl")]
        public string SelectedAreaCode { get; set; }
        public IEnumerable<SelectListItem> AreaCodes { get; set; }
    }
    public class CardRangeAcceptance
    {
        [DisplayName("Merchant No")]
        public string MerchantNo { get; set; }
        [DisplayName("Card Range")]
        public string selectedCardRange { get; set; }
        public IEnumerable<SelectListItem> CardRange { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Creation Date")]
        public string creationDate { get; set; }

    }

    public class MerchantAcctMaint
    {
        [DisplayName("Account No")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string AcctNo { get; set; }
        [DisplayName("Reference Type")]
        public string SelectedRefType { get; set; }
        public IEnumerable<SelectListItem> RefType { get; set; }
        [DisplayName("Reference Key")]
        public string RefKey { get; set; }
        [DisplayName("Old Data")]
        public string OldData { get; set; }
        [DisplayName("New Data")]
        public string NewData { get; set; }
        [DisplayName("User ID")]
        public string UserId { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }

    }

    public class BusnLocTerminal
    {
        public string BusnLocation { get; set; }
        [DisplayName("Terminal Id")]
        [StringLength(8, ErrorMessage = "Maximum lengthis 8 characters")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string TermId { get; set; }
        [DisplayName("Status")]
        public string SelectedStatus { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        public string RawStatus { get; set; }
        [DisplayName("Deployment Date")]
        public string DeployDate { get; set; }
        [DisplayName("Replaced By Terminal Id")]
        public string Replacement { get; set; }
        [DisplayName("Replaced Date")]
        public string ReplacedDate { get; set; }
        [DisplayName("Reason")]
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCd { get; set; }
        [DisplayName("IPEK")]
        public string IPEK { get; set; }
        [DisplayName("Settle From Time")]
        public string SettlementStart { get; set; }
        [DisplayName("Settle To Time")]
        public string SettlementEnd { get; set; }
        [DisplayName("Last Batch Id")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? LastBatchId { get; set; }
        [DisplayName("Settle Transaction Id")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? SettleTxnId { get; set; }
        [DisplayName("Device Model")]
        public string SelectedProdType { get; set; }
        public IEnumerable<SelectListItem> ProdType { get; set; }
        [DisplayName("Serial No")]
        public string SerialNo { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
        [DisplayName("UserId")]
        public string UserId { get; set; }
        [DisplayName("Last Update Date")]
        public string LastUpdDate { get; set; }
        [DisplayName("Sale Territory")]
        public string SaleTerritory { get; set; }
        //[DisplayName("Device Model")]
        //public string DeviceModel { get; set; }
        [DisplayName("Terminal Type")]
        public string SelectedTermType { get; set; }
        public IEnumerable<SelectListItem> TermType { get; set; }
        public string Descp { get; set; }
    }



}
