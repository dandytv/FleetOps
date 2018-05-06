using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using ModelSector.Global_Resources;

namespace ModelSector
{
    //class CorpCardAcctMaint
    //{
       public class CorporateAccountInfo
       {
           [DisplayName("Corporate Code")]
           public string SelectedCorporateCd { get; set; }
           public IEnumerable<SelectListItem> CorporateCd { get; set; }

           [DisplayName("Corporate Name")]
           public string CorporateName { get; set; }

           [DisplayName("Trade Credit Limit")]
           [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
           public string SelectedTradeCreditLimit { get; set; }
           public IEnumerable<SelectListItem> TradeCreditLimit { get; set; }

           [DisplayName("Contact Name")]
           public string ContactName { get; set; }

           [DisplayName("Contact No")]
           public string ContactNo { get; set; }

           public UserIdandCreationDate _UserIdandCreationDate { get; set; }
       }

       public class CorpAddressList
       { 
                  
           public string ReferenceKey { get; set; }

           [DisplayName("Address Type")]
           public string SelectedAddressType{ get; set; }
           public IEnumerable<SelectListItem> AddressType { get; set; }
           
           [DisplayName("Mailing Indicator")]
           public Boolean MailingIndicator { get; set; }
           
           [DisplayName("Address1")]
           public string Address1 { get; set; }
           
           [DisplayName("Address2")]
           public string Address2 { get; set; }
           
           [DisplayName("Address3")]
           public string Address3 { get; set; }

           [DisplayName("State")]
           public string SelectedState { get; set; }
           public IEnumerable<SelectListItem> State { get; set; }

           [DisplayName("City")]
           public string City { get; set; }
           
           [DisplayName("Postal Code")]
           public string PostalCode { get; set; }
           
           [DisplayName("Country")]
           public string SelectedCountry { get; set; }
           public IEnumerable<SelectListItem> Country { get; set; }

           public UserIdandCreationDate _UserIdandCreationDate { get; set; }

       }

       public class CorpContactList
       { 
           public string ReferenceKey { get; set; }

           [DisplayName("Contact Type")]
           public string ContactType{ get; set; }

           [DisplayName("Contact No")]
           public string ContactNo { get; set; }

           public UserIdandCreationDate _UserIdandCreationDate { get; set; }

       }

       public class UserIdandCreationDate
       {
           [DisplayName("Creation Date")]
           public DateTime CreationDate { get; set; }

           [DisplayName("User Id")]
           public string UserId { get; set; }
       }
    }
//}
