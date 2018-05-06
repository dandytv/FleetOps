using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ModelSector.Fraud
{
   public class FraudCustomerDetailsViewModel
    {
        public string EventId { get; set; }

        public string SubsidyNo { get; set; }

        //iac_Account / GeneralInfoModel
        [DisplayName("Account No")]
        public string AcctNo { get; set; }

        [DisplayName("Company Name")]
        public string CmpyName1 { get; set; }

        [DisplayName("Plastic Type")]
        public string AccountType { get; set; }

        [DisplayName("Card No")]
        public string SelectedCardNo { get; set; }
        public IEnumerable<SelectListItem> CardNo { get; set; }

        [DisplayName("Ageing Days")]
        public string AgeingDays { get; set; }

        [DisplayName("Client Type")]
        public string ClientType { get; set; }

        [DisplayName("Average Sales")]
        public string AvgSales { get; set; }
        public string AvgSalesDisplay { get; set; }

        public string Month1Date { get; set; }
        public string Month1Amt { get; set; }

        public string Month2Date { get; set; }
        public string Month2Amt { get; set; }

        public string Month3Date { get; set; }
        public string Month3Amt { get; set; }

        public string Month4Date { get; set; }
        public string Month4Amt { get; set; }

        public string Month5Date { get; set; }
        public string Month5Amt { get; set; }

        public string Month6Date { get; set; }
        public string Month6Amt { get; set; }
    }
}
