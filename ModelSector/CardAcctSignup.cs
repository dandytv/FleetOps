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


namespace CCMS.ModelSector
{

    public class AccountDetails
    {

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "AcctNo1Lbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string AcctNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CorporateNameLbl")]
        public string CorporateName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SourceCodeLbl")]
        public string SourceCode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SourceRefNoLbl")]
        public string SourceRefNo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedReasonCodeDdl")]
        public string SelectedStatus { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CreditLimitLbl")]
        public string CreditLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "TransLitreLimitLbl")]
        public string TransLitreLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "TransAmtLimitLbl")]
        public string TransAmtLimit { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CycleNonDateLbl")]
        public string CycleNonDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "Statement Type")]
        public string SelectedStatementType { get; set; }
        public IEnumerable<SelectListItem> StatementType { get; set; }
        public MerchpersonalBankingDtl PersonalBankingDtl { get; set; }
        public ApplicationDts ApplicationDetails { get; set; }
    }

    public class AcctSignUp
    {
        public string[] Excelheader()
        {
            return new string[] { "Application Id", "Acct No", "Company Name", "Corporate Account", "Credit Limit", "Pending Reasons", "User Id", "Creation Date", "Approved Date" };
        }
        public string[] ExcelBody()
        {
            return new string[] { ApplicationId, AcctNo, CompanyName, SelectedCorporateAcct, ShownCreditLimit, PendingReasons, CreationDatenUserid.UserId, CreationDatenUserid.CreationDate, ApprovedDate };
        }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "ApplicationIdLbl")]
        public string ApplicationId { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "ApplicationRefLbl")]
        public string ApplicationRef { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "ApprovedDateLbl")]
        public string ApprovedDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "ReceiveDateLbl")]
        public string ReceiveDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "RejectedDateLbl")]
        public string RejectedDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "AppvCdLbl")]
        public string AppvCd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "AcctNoLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string AcctNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CorporateNameLbl")]
        public string CorporateName { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CompanyRegnNoLbl")]
        [StringLength(15, ErrorMessage = "Maximum length is 15 characters")]
        public string CompanyRegnNo { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CompanyNameLbl")]
        [StringLength(50, ErrorMessage = "Maximum lengthis 50 characters")]
        public string CompanyName { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CompanyLegalNameLbl")]
        [Required]
        [StringLength(50, ErrorMessage = "Maximum lengthis 50 characters")]
        public string CompanyLegalName { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedCorporateAcctDdl")]
        public string SelectedCorporateAcct { get; set; }
        public IEnumerable<SelectListItem> CorporateAcct { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "ShortNameLbl")]
        public string ShortName { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "PersonInChargeLbl")]
        public string PersonInCharge { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "PositionLbl")]
        public string Position { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "selectedCompanyTypeDdl")]
        public string selectedCompanyType { get; set; }
        public IEnumerable<SelectListItem> CompanyType { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "selectedPlasticTypeDdl")]
        public string selectedPlasticType { get; set; }
        public IEnumerable<SelectListItem> PlasticType { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CompanyRegnNameLbl")]
        public string CompanyRegnName { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CompanyEmbNameLbl")]
        [StringLength(26, ErrorMessage = "Maximum lengthis 26 characters")]
        public string CompanyEmbName { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CompanyRegnDateLbl")]
        public string CompanyRegnDate { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "ContactPersonLbl")]
        [StringLength(100, ErrorMessage = "Maximum lengthis 100 characters")]
        public string ContactPerson { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedBusinessCategoryDdl")]
        public string SelectedBusinessCategory { get; set; }
        public IEnumerable<SelectListItem> BusinessCategory { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedNatureOfBusinessDdl")]
        public string SelectedNatureOfBusiness { get; set; }
        public IEnumerable<SelectListItem> NatureOfBusiness { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedCycleNoDdl")]
        public string SelectedCycleNo { get; set; }
        public IEnumerable<SelectListItem> CycleNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CreditLimitLbl")]
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string CreditLimit { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CreditLimitLbl")]
        [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
        public string ShownCreditLimit { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedStatementTypeDdl")]
        public string SelectedStatementType { get; set; }
        public IEnumerable<SelectListItem> StatementType { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SSelectedStatusDdl")]
        public string SelectedStatus { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }

        public CreationDatenUserId CreationDatenUserid { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "lastUpdateDateLbl")]
        public string lastUpdateDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedRegionDdl")]
        public string SelectedRegion { get; set; }
        public IEnumerable<SelectListItem> Region { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "VehiclePerformanceReportLbl")]
        public string VehiclePerformanceReport { get; set; }


        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "OfficePhoneLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string OfficePhone { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "MobileNoLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string MobileNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "OfficeFaxLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string OfficeFax { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "EmailAddressLbl")]
        [RegularExpression((@"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$"), ErrorMessage = "Not a valid Email Address")]
        public string emailAddress { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedBillingTypeDdl")]
        public string SelectedBillingType { get; set; }
        public IEnumerable<SelectListItem> BillingType { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedInvoicePrefDdl")]
        public string SelectedInvoicePref { get; set; }
        public IEnumerable<SelectListItem> InvoicePref { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "LoyaltyCardNoLbl")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string LoyaltyCardNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "LoyaltyFullNameLbl")]
        public string LoyaltyFullName { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "LoyaltyIcNoLbl")]
        public string LoyaltyIcNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "LoyaltyContactNoLbl")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Invalid Phone Number.")]
        public string LoyaltyContactNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "LoyaltyeBusnLbl")]
        public bool LoyaltyeBusn { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "EntityIdLbl")]
        public string EntityId { get; set; }

        public bool DirCreated { get; set; }

        public string Files { get; set; }
        public string PendingReasons { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "InvoiceBillingIndicatorLbl")]
        public bool InvoiceBillingIndicator { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "PymtIndLbl")]
        public bool PymtInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "InvoiceIndCopyLbl")]
        public bool InvoiceIndCopy { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "VehiclePerformanceReportIndLbl")]
        public bool VehiclePerformanceReportInd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedCostCentreDdl")]
        public string SelectedCostCentre { get; set; }
        public IEnumerable<SelectListItem> CostCentre { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SapNoLbl")]
        public string SapNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedTaxCategoryDdl")]
        public string SelectedTaxCategory { get; set; }
        public IEnumerable<SelectListItem> TaxCategory { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "WithholdingTaxIndLbl")]
        public bool WithholdingTaxInd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedLangIdDdl")]
        public string SelectedLangId { get; set; }
        public IEnumerable<SelectListItem> LangId { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "WebsiteLbl")]
        public string website { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedClientClassDdl")]
        public string SelectedClientClass { get; set; }
        public IEnumerable<SelectListItem> ClientClass { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedClientTypeDdl")]
        public string SelectedClientType { get; set; }
        public IEnumerable<SelectListItem> ClientType { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedPaymentModeDdl")]
        public string SelectedPaymentMode { get; set; }
        public IEnumerable<SelectListItem> PaymentMode { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedReasonCodeDdl")]
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "AuthSignatoryLbl")]
        public string AuthSignatory { get; set; }
    }

    public class ReferenceInfo
    {
        [DisplayName("Reference Key")]
        public string ReferenceKey { get; set; }
        [DisplayName("Reference Type")]
        public string SelectedReferenceType { get; set; }
        public IEnumerable<SelectListItem> ReferenceType { get; set; }
        public string RefKey { get; set; }
        [DisplayName("Reference Type")]
        public string SelectedRefType { get; set; }
        public IEnumerable<SelectListItem> RefType { get; set; }
        [DisplayName("Reference Code")]
        public string RefCd { get; set; }
    }

    public class ApplicationDts
    {
        [DisplayName("Application ID")]
        public string ApplicationId { get; set; }
        [DisplayName("Application Reference")]
        public string ApplicationReference { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
        [DisplayName("Approval Date")]
        public string ApprovalDate { get; set; }
        [DisplayName("Transfered Date")]
        public string TransferredDate { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
    }
    public class FinancialInfo
    {

        [DisplayName("Credit Allowance Factor")]
        public string CreditAllowanceFactor { get; set; }

        public MerchpersonalBankingDtl _BankingDtl { get; set; }

        [DisplayName("Customer No")]
        public string CustomerNo { get; set; }

        [DisplayName("Banker")]
        public string Banker { get; set; }


        [DisplayName("Cash Deposit")]
        public string CashDeposit { get; set; }

        [DisplayName("Source Code")]
        public string SourceCode { get; set; }

        [DisplayName("State")]
        public string selectedState { get; set; }
        public IEnumerable<SelectListItem> State { get; set; }

        [DisplayName("Industry Code")]
        public string IndustryCode { get; set; }

        [DisplayName("Approved By")]
        public string ApprovedBy { get; set; }

        [DisplayName("Approved Date")]
        public string ApprovedDate { get; set; }

        [DisplayName("Endorsed Date")]
        public string EndorsedDate { get; set; }


        [DisplayName("Endorsed By")]
        public string EndorsedBy { get; set; }

        [DisplayName("Transferred Date")]
        public string TransferredDate { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }
    }
    public class VelocityLimitnList
    {
        public ReferenceInfo _ReferenceInfo { get; set; }

        [DisplayName("Velocity Type")]
        public string SelectedVelocityType { get; set; }
        public IEnumerable<SelectListItem> VelocityType { get; set; }

        [DisplayName("Control Type")]
        public string SelectedControlType { get; set; }
        public IEnumerable<SelectListItem> ControlType { get; set; }

        [DisplayName("Product Code")]
        public string ProductCode { get; set; }

        [DisplayName("Litre Limits")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public Int64 LimitLitres { get; set; }

        [DisplayName("Amount Limits")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public Int64 AmountLimits { get; set; }

        [DisplayName("Counter Limits")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public Int64 CounterLimits { get; set; }

        public CreationDatenUserId _CreationDatenUserId { get; set; }

        [Display(Name = "lastupdatedate", ResourceType = typeof(locale))]
        public string lastUpdateDate { get; set; }
    }
    public class CardDetailsnSelect
    {
        public ReferenceInfo _ReferenceInfo { get; set; }

        [DisplayName("Card No")]
        [StringLength(19, MinimumLength = 16, ErrorMessage = "Minimum Length is 16,Maximum Length is 19")]
        public string CardNo { get; set; }

        public GhostCardModel CardModel { get; set; }

        [DisplayName("Emboss Name")]
        public string EmbossName { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [DisplayName("Status")]
        public string SelectedStatus { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }

        [DisplayName("National Identity No")]
        public string NationalIdentityNo { get; set; }

        [DisplayName("Identity No. Type")]
        public string SelectedNationalIdentityType { get; set; }
        public IEnumerable<SelectListItem> NationalIdentityType { get; set; }

        [DisplayName("Alternate National Identity No")]
        public string AlternateNationalIdentityNo { get; set; }

        [DisplayName("Alternate Identity No. Type")]
        public string SelectedAlternateNationalIdentitype { get; set; }
        public IEnumerable<SelectListItem> AlternateNationalIdentitype { get; set; }

        [DisplayName("Nationality")]
        public string SelectedNationality { get; set; }
        public IEnumerable<SelectListItem> Nationality { get; set; }

        [DisplayName("Transaction Litre Limit")]
        public string TransactionLiterLimit { get; set; }

        [DisplayName("Transaction Amount Limit")]
        public string TransactionAmountLimit { get; set; }

        [DisplayName("Passport ID")]
        public string PassportId { get; set; }

        [DisplayName("Passport Issued")]
        public string PassportIssued { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DisplayName("Date of Birth")]
        public string DOB { get; set; }

        [DisplayName("Annual Income")]
        public string SelectedAnnualIncome { get; set; }
        public IEnumerable<SelectListItem> AnnualIncome { get; set; }

        [Required]
        [Display(Name = "occupation", ResourceType = typeof(locale))]
        public string selectedPosition { get; set; }
        public IEnumerable<SelectListItem> Position { get; set; }

        [Display(Name = "License", ResourceType = typeof(locale))]
        public string License { get; set; }
        public IEnumerable<SelectListItem> Drivinglicense { get; set; }

        [DisplayName("Vehicle Registration No")]
        public string VehicleRegNo { get; set; }

        [DisplayName("Driver Code")]
        public string DriverCode { get; set; }
    }

    public class VehicleDetails
    {
        public ReferenceInfo _ReferenceInfo { get; set; }
        [DisplayName("Vehicle Registration No")]
        public string VehicleRegnNo { get; set; }
        [DisplayName("Vehicle Maker")]
        public string VehicleMaker { get; set; }
        [DisplayName("Vehicle Model")]
        public string VehicleModel { get; set; }
        [DisplayName("Vehicle Registration Date")]
        public string VehicleRegnDate { get; set; }
        [DisplayName("Vehicle Year")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string VehicleYear { get; set; }
        [DisplayName("Vehicle Color")]
        public string VehicleColor { get; set; }
        [DisplayName("Odometer Reading")]
        public string OdoMeterreading { get; set; }
        [DisplayName("Odometer Update")]
        public string OdoMeterUpdate { get; set; }
        [DisplayName("Road Tax Expirt Date")]
        public string RoadTaxExpiryDate { get; set; }
        [DisplayName("Road Tax Amount")]
        public string RoadTaxAmount { get; set; }
        [DisplayName("Renewal Period")]
        public string RenewalPeriod { get; set; }
        [DisplayName("Insurance No")]
        public string InsuranceNo { get; set; }
        [DisplayName("Policy No")]
        public string PolicyNo { get; set; }
        [DisplayName("Policy Expiry Date")]
        public string PolicyExpiryDate { get; set; }
        [DisplayName("Premium Amount")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public Int64 PremiumAmount { get; set; }
        [DisplayName("Policy Amount")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public Int64 PolicyAmount { get; set; }
        [DisplayName("Vehicle Service Date")]
        public string VehicleServiceDate { get; set; }
    }

    public class ProductAcceptance
    {
        [DisplayName("Card No")]
        [StringLength(19, MinimumLength = 16, ErrorMessage = "Minimum Length is 16,Maximum Length is 19")]
        public string CardNo { get; set; }
        [DisplayName("Product Group")]
        public string ProductGroup { get; set; }
    }
    public class LocationAcceptance
    {
        public ReferenceInfo _ReferenceInfo { get; set; }

        [DisplayName("Merchant ID")]
        public string MerchantId { get; set; }
    }

    public class CreationDatenUserId
    {
        [Display(Name = "creationdate", ResourceType = typeof(locale))]
        public string CreationDate { get; set; }
        [Display(Name = "userid", ResourceType = typeof(locale))]
        public string UserId { get; set; }
    }
    public class ContactDetailsnList
    {
        public ReferenceInfo _ReferenceInfo { get; set; }
        [DisplayName("Contact No")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string ContactNo { get; set; }
        public CreationDatenUserId CreationDatenUserid { get; set; }
    }
    public class TemporaryCreditnList
    {
        [DisplayName("Account No")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string AcctNo { get; set; }
        [DisplayName("Date From")]
        public string FromDate { get; set; }
        [DisplayName("Date To")]
        public string ToDate { get; set; }
        [DisplayName("Credit Limit")]
        public string CreditLimit { get; set; }
        [DisplayName("Status")]
        public string SelectedStatus { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        public CreationDatenUserId CreationDatenUserid { get; set; }
    }
    public class MiscInfo
    {
        [DisplayName("Company Type")]
        public string SelectedCompanyType { get; set; }
        public IEnumerable<SelectListItem> CompanyType { get; set; }
        [DisplayName("Company Registration Name")]
        [StringLength(100, ErrorMessage = "Maximum lengthis 100 characters")]
        public string CompanyRegnName { get; set; }
        [DisplayName("Trade No")]
        public string TradeNo { get; set; }
        [DisplayName("Trade Id")]
        public string TaxId { get; set; }

        [DisplayName("Business Category")]
        public string SelectedBusinessCategory { get; set; }
        public IEnumerable<SelectListItem> BusinessCategory { get; set; }
        [DisplayName("Business Registration Date")]
        public string BusnRegnDate { get; set; }
        [DisplayName("Place of Registration")]
        public string PlaceofRegn { get; set; }
        [DisplayName("Legal Date")]
        public string LegalDate { get; set; }

        [DisplayName("Authorized Signatory")]
        public string AuthorizedSignatory { get; set; }
        [DisplayName("Authorized Full Name")]
        public string AuthorizedFullName { get; set; }
        [Display(Name = "occupation", ResourceType = typeof(locale))]
        public string selectedPosition { get; set; }
        public IEnumerable<SelectListItem> Position { get; set; }
        [DisplayName("Authorized Date")]
        public string AuthorizedDate { get; set; }
        [DisplayName("Smiles Driver FullName")]
        public string SmilesDriverFullName { get; set; }
        [DisplayName("Smiles ICNo.")]
        public string SmilesICNo { get; set; }
        [DisplayName("Smiles IC Type")]
        public string selectedSmilesICType { get; set; }
        public IEnumerable<SelectListItem> SmilesICType { get; set; }
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }
        [DisplayName("eBusiness Type")]
        public string selectedeBusinessType { get; set; }
        public IEnumerable<SelectListItem> eBusnType { get; set; }
    }

    public class Milestone
    {
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "UserIdLbl")]
        public string UserId { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedReasonCdDdl")]
        public string selectedReasonCd { get; set; }
        public IEnumerable<SelectListItem> ReasonCd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "RemarksLbl")]
        [StringLength(500, ErrorMessage = "Maximum length is 500 characters")]
        public string Remarks { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "RecallDateLbl")]
        public string RecallDate { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedStatusDdl")]
        public string selectedStatus { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedOwnerDdl")]
        public string selectedOwner { get; set; }
        public IEnumerable<SelectListItem> Owner { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedPriorityDdl")]
        public string selectedPriority { get; set; }
        public IEnumerable<SelectListItem> Priority { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "RefKeyLbl")]
        public Int64 RefKey { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "RefNoLbl")]
        public string RefNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "IDLbl")]
        public Int64 ID { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "WorkflowcdLbl")]
        public string workflowcd { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CreationDateLbl")]
        public string CreationDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "LastUpdDateLbl")]
        public string LastUpdDate { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "AprId")]
        public Int64 aprId { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "DescpLbl")]
        public string Descp { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CreditLimitLbl")]
        public string CreditLimit { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SecurityAmtLbl")]
        public string SecurityAmt { get; set; }

        [Required]
        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedTaskNoDdl")]
        public string SelectedTaskNo { get; set; }
        public IEnumerable<SelectListItem> TaskNo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "TaskDescpLbl")]
        public string TaskDescp { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "AcctNo1Lbl")]
        public string AcctNo { get; set; }

        public string PrevId { get; set; }
        public int Ind { get; set; }
        public string Url { get; set; }
        public string validationSP { get; set; }
        public string ActionSP { get; set; }
        public int SLADay { get; set; }

        public string CompanyName { get; set; }

        public string BatchID { get; set; }

        public string TxnCount { get; set; }

        public string TxnAmt { get; set; }

        public string AuthType { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "CardNumberLbl")]
        public string CardNumber { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "RequestByLbl")]
        public string RequestBy { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "RequestValueLbl")]
        public string RequestValue { get; set; }
    }

}
