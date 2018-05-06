using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using ModelSector.Global_Resources;
using ModelSector.Helpers;

namespace ModelSector
{
    public class MA_GeneralInfo
    {
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "AcctNoLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string AcctNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedBusnModelDdl")]
        public string SelectedBusnModel { get; set; }
        public IEnumerable<SelectListItem> BusnModel { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "AgrmntNoLbl")]
        public string AgrmntNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "AgrmntDtLbl")]
        public string AgrmntDt { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "BusnNameLbl")]
        public string BusnName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedBusnSizeDdl")]
        public string SelectedBusnSize { get; set; }
        public IEnumerable<SelectListItem> BusnSize { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedAffiliatedWithCorpCodeDdl")]
        public string SelectedAffiliatedWithCorpCode { get; set; }
        public IEnumerable<SelectListItem> AffiliatedWithCorpCode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "selectedCurrentStatusDdl")]        
        public string SelectedCurrentStatus { get; set; }
        public IEnumerable<SelectListItem> CurrentStatus { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SourceCdLbl")] 
        public string SourceCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SourceRefNoLbl")] 
        public string SourceRefNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "FloorLimitLbl")] 
        public string FloorLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedCycleNoDdl")] 
        public string SelectedCycleNo { get; set; }
        public IEnumerable<SelectListItem> CycleNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CycleDateLbl")] 
        public string CycleDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "BankAcctNameLbl")] 
        public string BankAcctName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "BankAcctNoLbl")] 
        public string BankAcctNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "selectedBankAccountTypeDdl")] 
        public string selectedBankAccountType { get; set; }
        public IEnumerable<SelectListItem> BankAcctType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "BankDirectDebitIndChk")] 
        public bool BankDirectDebitInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "RemarksLbl")] 
        public string Remarks { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CreationDateLbl")] 
        public string CreationDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "UserIdLbl")] 
        public string UserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "ApprovalDateLbl")] 
        public string ApprovalDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "ApprovedByUserIdLbl")] 
        public string ApprovedByUserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "AutoDebitIndChk")] 
        public bool AutoDebitInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "PICLbl")] 
        public string PIC { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedOwnershipDdl")]
        public string SelectedOwnership { get; set; }
        public IEnumerable<SelectListItem> Ownership { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CoRegNoLbl")] 
        public string CoRegNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CoRegNameLbl")] 
        [StringLength(100, ErrorMessage = "Maximum length is 100 characters")]
        public string CoRegName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CoRegDateLbl")] 
        public string CoRegDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "PayeeNameLbl")] 
        [StringLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string PayeeName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedBankNameDdl")] 
        [StringLength(10, ErrorMessage = "Maximum length is 10 characters")]
        public string SelectedBankName { get; set; }
        public IEnumerable<SelectListItem> BankName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedBankBranchCdDdl")] 
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        [StringLength(6, ErrorMessage = "Maximum length is 6 characters")]
        public string SelectedBankBranchCd { get; set; }
        public IEnumerable<SelectListItem> BankBranchCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedBusnEstDdl")] 
        public string SelectedBusnEst { get; set; }
        public IEnumerable<SelectListItem> BusnEst { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "MsfLbl")] 
        public string Msf { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SAPNoLbl")] 
        public string SAPNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "EnttIdLbl")] 
        public string EnttId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "TaxIdLbl")] 
        public string TaxId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "WithholdingTaxIndLbl")] 
        public string WithholdingTaxInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "WithholdingTaxRateLbl")] 
        public string WithholdingTaxRate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedReasonCodeDdl")] 
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCd { get; set; }
        public string SAPName { get; set; }
        public string BusinessLocation { get; set; }
    }

    public class ContactList
    {
        public string RefType { get; set; }
        public string RefTo { get; set; }
        [DisplayName("Reference Key")]
        public string RefKey { get; set; }
        [DisplayName("Reference Code")]
        public string RefCd { get; set; }

        [DisplayName("ContactType")]
        public string SelectedContactType { get; set; }
        public IEnumerable<SelectListItem> ContactType { get; set; }
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }

        [DisplayName("Contact Status")]
        public string SelectedContactStatus { get; set; }
        public IEnumerable<SelectListItem> ContactStatus { get; set; }
        public string RawStatus { get; set; }

        [DisplayName("Function")]
        public string SelectedJobOccupation { get; set; }
        public IEnumerable<SelectListItem> JobOccupation { get; set; }
        public string RawOccupation { get; set; }

        [DisplayName("Email Address")]
        [RegularExpression((@"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$"), ErrorMessage = "Not a valid Email Address")]
        public string EmailAddr { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
    }

    public class AddressList
    {
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedMailingIndicatorDdl")]
        public bool SelectedMailingIndicator { get; set; }
        public IEnumerable<SelectListItem> MailingIndicator { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedAddressTypeDdl")]
        public string selectedAddressType { get; set; }
        public IEnumerable<SelectListItem> AddressType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "Address1Lbl")]
        public string Address1 { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "Address2Lbl")]
        public string Address2 { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "Address3Lbl")]
        public string Address3 { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedCountryDdl")]
        public string selectedCountry { get; set; }
        public IEnumerable<SelectListItem> country { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedRegionDdl")]
        public string selectedRegion { get; set; }
        public IEnumerable<SelectListItem> Region { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedStateDdl")]
        public string selectedState { get; set; }
        public IEnumerable<SelectListItem> state { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedCityDdl")]
        public string selectedCity { get; set; }
        public IEnumerable<SelectListItem> City { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "PostalCodeLbl")]
        public string PostalCode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "RefcdLbl")]
        public string Refcd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "UserIdLbl")]
        public string UserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CreationDateLbl")]
        public string CreationDate { get; set; }

    }
    public class MerchProductPrize
    {
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "EffDateFromLbl")]
        public string EffDateFrom { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "EffDateToLbl")]
        public string EffDateTo { get; set; }
        public string Descp { get; set; }
        public string CreationDate { get; set; }
        public string Price { get; set; }
        public IEnumerable<SelectListItem> ProdCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedProdCdDdl")]
        public string SelectedProdCd { get; set; }
        public string BusnLocation { get; set; }
    }

    public class eService
    {
        public string No { get; set; }
        public string PostingDate { get; set; }
        public string TxnDate { get; set; }
        public string TxnTime { get; set; }
        public string CardNo { get; set; }
        public string RRN { get; set; }
        public int Quantity { get; set; }
        public string Amount { get; set; }
        public string MDR { get; set; }
        public string GST { get; set; }
        public string NetAmount { get; set; }
        public string VatAmount { get; set; }
        public IEnumerable<SelectListItem> TxnType { get; set; }
        public string SelectedTxnType { get; set; }
        public string BusnLocation { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public IEnumerable<SelectListItem> TxnDateRange { get; set; }
        public string SelectedTxnDateRange { get; set; }
        public string Multiplier { get; set; }
        public string Siteid { get; set; }
        public string BusnName { get; set; }
        public string bankName { get; set; }
        public string BankAcctNo { get; set; }
        public string TradingName { get; set; }
    }
    public class MerchChangeOwnership
    {
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "FromMerchantIdLbl")] 
        public string FromMerchantId { get; set; }
        public string ToMerchantId { get; set; }
        public string CreationDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CutoffDateLbl")] 
        public string CutoffDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "FloatAcctIndLbl")] 
        public bool FloatAcctInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CurrentSiteIdLbl")] 
        public string CurrentSiteId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "NewSiteIdLbl")] 
        public string NewSiteId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CutOffTimeLbl")] 
        public string CutOffTime { get; set; }
        public string BusnName { get; set; }
        public string TaxId { get; set; }
        public string DBAName { get; set; }
        public IEnumerable<SelectListItem> DBAState { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedDBAStateDdl")] 
        public string SelectedDBAState { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CoRegNoLbl")] 
        public string CoRegNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "CoRegNameLbl")] 
        public string CoRegName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "DealerNameLbl")] 
        public string DealerName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "DealerContactLbl")] 
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string DealerContact { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "PayeeNameLbl")] 
        public string PayeeName { get; set; }
        public IEnumerable<SelectListItem> BankName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedBankNameDdl")] 
        public string SelectedBankName { get; set; }
        public IEnumerable<SelectListItem> BankAcctType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedBankAcctTypeDdl")] 
        public string SelectedBankAcctType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SelectedBankBranchCdDdl")] 
        public string BankBranchCd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "BankAccountNoLbl")] 
        public string BankAcctNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMechSignUp", "SAPNoLbl")] 
        public string SAPNo { get; set; }
        public bool MaskedFlag { get; set; }
    }
}
