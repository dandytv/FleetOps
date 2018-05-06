using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FleetSys.ViewModel
{
    public class CollectionTaskListViewModel
    {
        [DisplayName("Collect No")]
        public string EventId { get; set; }
        [DisplayName("Account No")]
        public string AcctNo { get; set; }
        [DisplayName("Company Name")]
        public string CmpyName1 { get; set; }

        [DisplayName("Sales Territory")]
        public string SelectedSalesTerritory { get; set; }
        public IEnumerable<SelectListItem> SalesTerritory { get; set; }

        [DisplayName("Collection Amount")]
        public string AccumAgeingAmt { get; set; }
        [DisplayName("Grace Due Date")]
        public string GraceDueDate { get; set; }
        [DisplayName("Age Code")]
        public string CycAge { get; set; }

        public string Priority { get; set; }
        [DisplayName("Account Status")]
        public string AccountSts { get; set; }
        [DisplayName("Collect Status")]
        public string SelectedCollectionSts { get; set; }
        public IEnumerable<SelectListItem> Collectionsts { get; set; }

        [DisplayName("Recall Date")]
        public string RecallDate { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }

        //search
        public string RecallFromDate { get; set; }
        public string RecallToDate { get; set; }
        public string CreationFromDate { get; set; }
        public string CreationToDate { get; set; }

          [DisplayName("Owner")]
        public string SelectedOwner { get; set; }
        public IEnumerable<SelectListItem> Owner { get; set; }

        [DisplayName("Corporate Code")]
        public string SelectedCorpCode { get; set; }
        public IEnumerable<SelectListItem> CorpCode { get; set; }

        [DisplayName("Corporate Account")]
        public string CorpAcct { get; set; }

        [DisplayName("Corporate Name")]
        public string CorpName { get; set; }

        //Threshold Limit
        [DisplayName("Permanent Credit Limit")]
        public string PermCreditLimit { get; set; }

        [DisplayName("Temporary Credit Limit")]
        public string TempCreditLimit { get; set; }

        [DisplayName("% Usage")]
        public string PercentageUsage { get; set; }

        [DisplayName("Available Balance")]
        public string AvailBalance { get; set; }

        [DisplayName("Pukal Account Indicator")]
        public string PukalAcctInd { get; set; }
    }
}