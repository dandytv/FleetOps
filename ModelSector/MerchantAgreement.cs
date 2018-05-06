using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModelSector.Global_Resources;

namespace CCMS.ModelSector
{


    public class MerchantDtl
    {
        [DisplayName("Account Name")]
        public string AccountName { get; set; }
        public MerchTaxInfo taxInfo { get; set; }
        public MerchOwnership Ownership { get; set; }
        public MerchStatus Status { get; set; }
    }


    public class MerchTaxInfo
    {
        [Display(Name = "taxid", ResourceType = typeof(locale))]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public Int64 TaxID { get; set; }
        [Display(Name = "registeredname", ResourceType = typeof(locale))]
        public string RegisteredName { get; set; }
    }

    public class MerchOwnership
    {
        public IEnumerable<SelectListItem> Ownership { get; set; }
        [Display(Name = "ownership", ResourceType = typeof(locale))]
        public string selectedOwnership { get; set; }

        [Display(Name = "ownership", ResourceType = typeof(locale))]
        public string AgreementNo { get; set; }


        [Display(Name = "agrmntdate", ResourceType = typeof(locale))]
        public DateTime AgreementDate { get; set; }
        [DisplayName("Cancel Date")]
        public DateTime CancelDate { get; set; }
        [Display(Name = "coregsno", ResourceType = typeof(locale))]
        [DisplayName("CoRegs No")]
        public string CoRegNo { get; set; }
        [DisplayName("CoRegs Name")]
        public string CoRegName { get; set; }
        [DisplayName("site Id")]
        public string SiteId { get; set; }
        public IEnumerable<SelectListItem> CorporateCd { get; set; }
        [Display(Name = "corporatecode", ResourceType = typeof(locale))]
        public string selectedCorporateCd { get; set; }
        public IEnumerable<SelectListItem> Establishment { get; set; }
        [Display(Name = "establishment", ResourceType = typeof(locale))]
        public string selectedEstablishment { get; set; }
        public string MCC { get; set; }

        [Display(Name = "pic", ResourceType = typeof(locale))]
        public string PersonInCharge { get; set; }
        [DisplayName("Payee Name")]
        public string PayeeName { get; set; }
        [Display(Name = "sic", ResourceType = typeof(locale))]
        public string SIC { get; set; }

    }
    public class MerchStatus
    {
        [Display(Name = "creationdate", ResourceType = typeof(locale))]
        public DateTime CreationDate { get; set; }


        [Display(Name = "createdby", ResourceType = typeof(locale))]
        public string CreatedBy { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }


        [Display(Name = "status", ResourceType = typeof(locale))]
        public string selectedStatus { get; set; }

        public IEnumerable<SelectListItem> ReasonCode { get; set; }
        [Display(Name = "reasoncd", ResourceType = typeof(locale))]
        public string SelectedReasonCode { get; set; }


    }

    public class MerchEntityDtl
    {
        [Display(Name = "entityid", ResourceType = typeof(locale))]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int EntityId { get; set; }
        public PersonlParticulars PersonalParticulars { get; set; }
        public EmploymentDtl EmploymentDetail { get; set; }
        public MerchpersonalBankingDtl BankingDtl { get; set; }
    }



    public class PersonlParticulars
    {
        [Display(Name = "Name", ResourceType = typeof(locale))]
        public string Name { get; set; }
        [Display(Name = "gender", ResourceType = typeof(locale))]
        public string Gender { get; set; }



        public IEnumerable<SelectListItem> MaritalStatus { get; set; }
         [Display(Name = "maritalstatus", ResourceType = typeof(locale))]
        public string selectedMarital { get; set; }

        public IEnumerable<SelectListItem> DOBDay { get; set; }
        [DisplayName("Day")]
        public string selectedDOBDay { get; set; }
        [DisplayName("Date of Birth")]
        public string DateoBirth { get; set; }
        public IEnumerable<SelectListItem> DOBMonth { get; set; }
        [DisplayName("Month")]
        public string selectedDOBMonth { get; set; }

        public IEnumerable<SelectListItem> DOBYear { get; set; }
        [DisplayName("Year")]
        public string selectedDOBYear { get; set; }



        public IEnumerable<SelectListItem> BloodGroup { get; set; }
        [DisplayName("Blood Group")]



        [Display(Name = "bloodgroup", ResourceType = typeof(locale))]
        public string selectedBloodGroup { get; set; }

        [Display(Name = "newic", ResourceType = typeof(locale))]

        public string NewIC { get; set; }
        [Display(Name = "oldic", ResourceType = typeof(locale))]
        public string OldIC { get; set; }
         [Display(Name = "title", ResourceType = typeof(locale))]
        public string Title { get; set; }


        [Display(Name = "passportno", ResourceType = typeof(locale))]
        public string PassportNo { get; set; }

        [Display(Name = "License", ResourceType = typeof(locale))]
        public string License { get; set; }

    }



    public class EmploymentDtl
    {
        public IEnumerable<SelectListItem> Department { get; set; }
        [Display(Name = "department", ResourceType = typeof(locale))]
        public string selectedDepartment { get; set; }
        public IEnumerable<SelectListItem> Occupation { get; set; }

        [Display(Name = "occupation", ResourceType = typeof(locale))]
        public string selectedOccupation { get; set; }

        [Display(Name = "income", ResourceType = typeof(locale))]
        public string Income { get; set; }
    }


    public class MerchpersonalBankingDtl
    {
        [DisplayName ("Bank Account No")]
        public string BankAcctNo { get; set; }
        [DisplayName("Bank Account Name")]
        public string BankAcctName { get; set; }
        [DisplayName("Bank Account Type")]
        public string selectedBankAccountType { get; set; }
        public IEnumerable<SelectListItem> BankAcctType { get; set; }
        [DisplayName("Bank Direct Debit Indicator")]
        public string BankDirectDebitInd { get; set; }
        [DisplayName("Branch")]
        public string Branch { get; set; }
        // ReSharper restore InconsistentNaming
    }


    public class CICnMCC
    {

        public string MerchCategoryCd { get; set; }
        public string CategoryCd { get; set; }
        public string Descp { get; set; }
    }

    public class MerchantContacts
    {
        [Required]

        [Display(Name = "contactname", ResourceType = typeof(locale))]
        public string ContactName { get; set; }

        public IEnumerable<SelectListItem> ContactType { get; set; }

        [Required]
        [Display(Name = "contacttype", ResourceType = typeof(locale))]
        public string selectedContactType { get; set; }



        [Required]
        [Display(Name = "occupation", ResourceType = typeof(locale))]
        public string selectedPosition { get; set; }
        public IEnumerable<SelectListItem> Position { get; set; }

        [DisplayName ("contactno")]
        [Range(11, 12)]
        [Required]
        public string ContactNo { get; set; }


        public IEnumerable<SelectListItem> ContactStatus { get; set; }
        [DisplayName("Contact Status")]
        [Required]
        public string selectedContactStatus { get; set; }


            [DisplayName("emailaddr")]
        [Required]
        public string EmailAddress { get; set; }

    }

    public class MerchAddress
    {
        public IEnumerable<SelectListItem> AddressType { get; set; }
        [Display(Name = "addresstype", ResourceType = typeof(locale))]
        public string selectedAddressType { get; set; }
        
        public string ReferenceKey { get; set; }

        [DisplayName("Mailing Indicator")]
        public bool MailingIndicator { get; set; }

        [Display(Name = "contactname", ResourceType = typeof(locale))]
        public bool ContactName { get; set; }

        [Display(Name = "Address", ResourceType = typeof(locale))]
        public string Street1 { get; set; }

        [Display(Name = "District", ResourceType = typeof(locale))]
        public string Street2 { get; set; }

        [Display(Name = "City", ResourceType = typeof(locale))]
        public string Street3 { get; set; }

        [Display(Name = "city", ResourceType = typeof(locale))]
        public string City { get; set; }

        [Display(Name = "postalcode", ResourceType = typeof(locale))]
        public string PostalCode { get; set; }

        public IEnumerable<SelectListItem> State { get; set; }
        [Display(Name = "state", ResourceType = typeof(locale))]
        public string selectedState { get; set; }

        public IEnumerable<SelectListItem> countryCd { get; set; }
        [Display(Name = "ctrycd", ResourceType = typeof(locale))]
        public string selectedCountryCd { get; set; }

        [Display(Name="User Id", ResourceType=typeof(locale))]
        public string UserId { get; set; }

        [DisplayName("Last Update Date")]
        public DateTime LastUptDate { get; set; }

        public string Prefix { get; set; }
    }

    public class OtherInfo
    {
        public long AvgMnthlySales { get; set; }
        public long EstAnnualSales { get; set; }

    }

    public class Dba
    {
        public string Name { get; set; }
        public IEnumerable<SelectListItem> City { get; set; }
        public string SelectedCity { get; set; }
        public IEnumerable<SelectListItem> State { get; set; }
        public string SelectedState { get; set; }
        public IEnumerable<SelectListItem> Region { get; set; }
        public string SelectedRegion { get; set; }
    }
    public class ChildMerch
    {
        public string MerchNo { get; set; }
        public string MerchName { get; set; }
        public string EntityType { get; set; }
        public string AccountNo { get; set; }
        public MerchTaxInfo TaxInfo { get; set; }
        public MerchOwnership Ownership { get; set; }
        public CICnMCC CicnMcc { get; set; }
        public MerchStatus MerchantStatus { get; set; }
        public MerchpersonalBankingDtl BankingDtl { get; set; }
        public OtherInfo OtherInfo { get; set; }
        public Dba Dba { get; set; }
        public IEnumerable<SelectListItem> DealerCd { get; set; }
        [DisplayName("Dealer Code")]
        public string SelectedDealerCd { get; set; }
        public IEnumerable<SelectListItem> MerchantType { get; set; }
        [DisplayName("Dealer Code")]
        public string SelectedMerchantType { get; set; }
    }

    public class iss_WebResources
    {
        public string AppName { get; set; }
        public string ResxName { get; set; }
        public string Descp { get; set; }
        public string ResxType { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
