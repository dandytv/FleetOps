using System;
using CCMS.ModelSector;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModelSector.Global_Resources;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ModelSector
{
    public class NonFleetTxn
    {
        [DisplayName("Txn Id")]
       // [Display(Name = "txnid", ResourceType = typeof(locale))]
        public string TxnId { get; set; }

        public CardnAccNo _CardnAccNo { get; set; }

        [DisplayName("Txn Cd")]
        //[Display(Name = "txncd", ResourceType = typeof(locale))]
       // [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string SelectedTxnCd { get; set; }
        public IEnumerable<SelectListItem> TxnCd { get; set; }
        [DisplayName("Debit Credit Indicator ")]
        public string DbCrInd { get; set; }
        [DisplayName("Txn Date")]
        //[Display(Name = "txndate", ResourceType = typeof(locale))]
        public string TxnDate { get; set; }
        [DisplayName("Affiliate")]
        public string SelectedAffiliate { get; set; }
        public IEnumerable<SelectListItem> Affiliate { get; set; }
        [DisplayName("Txn Amount")]
        //[Display(Name = "totalamount", ResourceType = typeof(locale))]
      //  [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        //decimalvalidationbug
        public string  TotAmnt { get; set; }
        public string DisplayTotAmnt { get; set; }

        [DisplayName("Billing Amount")]
        //decimalvalidationbug
        public string BillingTxnAmt { get; set; }

        [DisplayName("Total Point")]
       // [Display(Name = "totalpoints", ResourceType = typeof(locale))]
       // [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        //decimalvalidationbug
        public string Totpts { get; set; }
        [DisplayName("Account")]
        public string Account { get; set; }
        [DisplayName("Description")]
        //[Display(Name = "descp", ResourceType = typeof(locale))]
        public string Descp { get; set; }

        [Display(Name = "invoicenumber", ResourceType = typeof(locale))]
        public string InvoiceNo { get; set; }

        [Display(Name = "collectionid", ResourceType = typeof(locale))]
        public string CollectionId { get; set; }

        [Display(Name = "referencetype", ResourceType = typeof(locale))]
        public string  RefType { get; set; }

        [Display(Name = "referenceid", ResourceType = typeof(locale))]
        public string RefId { get; set; }

        [DisplayName("User Id")]
       // [Display(Name = "userid", ResourceType = typeof(locale))]
        public string UserId { get; set; }


       // [Display(Name = "creationdate", ResourceType = typeof(locale))]
        public string CreationDate { get; set; }

        [Display(Name = "endorsedby", ResourceType = typeof(locale))]
        public string EndorsedBy { get; set; }

        [Display(Name = "endorseddate", ResourceType = typeof(locale))]
        public string EndorsedDate { get; set; }

        [DisplayName("Withheld Unsettle Id")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public Int32 WithHeldUnsettleId { get; set; }

        [DisplayName("Dealer")]
        public string DeftBusnLocation { get; set; }

        [DisplayName("Deft Terminal Id")]
        public string DeftTermId { get; set; }

        [DisplayName("Cheque No")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int? CheqNo { get; set; }


        [DisplayName("Status")]
        public string StsDescp { get; set; }

        [DisplayName("Status")]
        public string SelectedSts { get; set; }
        public IEnumerable<SelectListItem> Sts { get; set; }
        [DisplayName("Approval Code")]
        public string AppvCd { get; set; }
        [DisplayName("Remarks")]
        public string remarks { get; set; }
    }
}
