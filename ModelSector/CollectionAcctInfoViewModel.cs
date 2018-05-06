using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSector
{
   public class CollectionAcctInfoViewModel
    {
        [DisplayName("Account Number")]
        public string AcctNo { get; set; }
        [DisplayName("Company Name")]
        public string CmpyName { get; set; }
        [DisplayName("Client Type")]
        public string ClientType { get; set; }
        [DisplayName("Corporate Code")]
        public string CorpCode { get; set; }
        [DisplayName("Corporate Name")]
        public string CorpName { get; set; }
        [DisplayName("Sales Territory")]
        public string SalesTerritory { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
        [DisplayName("Blocked Date")]
        public string BlockedDate { get; set; }
        public string TempReinstatementDateFrom { get; set; }
        public string TempReinstatementDateTo { get; set; }
        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }
        [DisplayName("Occupation")]
        public string Occupation { get; set; }
        [DisplayName("Office Phone")]
        public string OfficePhone { get; set; }
        [DisplayName("Mobile No")]
        public string MobileNo { get; set; }
        [DisplayName("Email Address")]
        public string EmailAddr { get; set; }
    }
}
