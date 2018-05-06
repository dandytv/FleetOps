using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ModelSector
{
    public class Prepaid
    {
        public Int64 TxnId { get; set; }
        public IEnumerable<SelectListItem> RefTo { get; set; }
        [Display(Name = "RefTo")]
        public string SelectedRefTo { get; set; }
        public string LinkTxnId { get; set; }
        [Display(Name = " Account No")]
        [Required]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string AcctNo { get; set; }
        public string XRefDoc { get; set; }
        public string TxnDate { get; set; }
        public string TxnAmount { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        [Display(Name = "Status")]
        public string SelectedStatus { get; set; }
        public string UserId { get; set; }
        public string CreationDate { get; set; }
        [Display(Name = " Document No")]
        [Required]
        public string DocNo { get; set; }
        [Display(Name = "From Date")]
        public string FromDate { get; set; }
        [Display(Name = "To Date")]
        public string ToDate { get; set; }
        public string CardNo { get; set; }
        public string TxnAmt { get; set; }



        public string POBalance { get; set; }
    }

    public class PurchaseOrder
    {
        [Display(Name="Doc No")]
        public string DocNo { get; set; }
          [Display(Name = "Txn Id")]
        public Int64? TxnId { get; set; }
        public string RefTo { get; set; }
        public string LinkTxnId { get; set; }
          [Display(Name = "Account No")]
        public Int64 AcctNo { get; set; }
        public string XRefDoc { get; set; }
        [Display(Name = "Transaction Date")]
        public string TxnDate { get; set; }
        [Display(Name = "Transaction Amount")]
        public string TxnAmt { get; set; }
        [Display(Name = "Amount Balance")]
        public string Balance { get; set; }
        
        [Display(Name = "Status")]
        public string SelectedStatus { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }

        public string UserId { get; set; }
          [Display(Name = "Creation Date")]
        public string CreationDate { get; set; }
        public string Remarks { get; set; }
        
        [Display(Name = "PO No")]
        public string RefKey { get; set; }
          [Display(Name = "Effective From")]
        public string EffectiveFromDate { get; set; }
    }
    public class DeliveryAdvice
    {
        public string CardNo { get; set; }
         [Display(Name = "Txn Id")]
        public Int64? TxnId { get; set; }
        public string RefTo { get; set; }
        [Display(Name="Ref Key")]
        public string RefKey { get; set; }
        public string PoTxnId { get; set; }
        public string DaTxnId { get; set; }
         [Display(Name = "Acct No")]
        public Int64 AcctNo { get; set; }
        public string XrefDoc { get; set; }
         [Display(Name = "Txn Date")]
        public string TxnDate { get; set; }
        [Display(Name = "Effective Date From")]
        public string EffDateFrom { get; set; }
         [Display(Name = "Txn Amount")]
        public string TxnAmt { get; set; }
         [Display(Name = "Reload Amount")]
        public string ReloadAmt { get; set; }
        [Display(Name = "Status")]
        public string SelectedStatus { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
         [Display(Name = "Creation Date")]
        public string CreationDate { get; set; }
        public string Remarks { get; set; }
        public string DANoRefKey { get; set; }
         [Display(Name = "DA Balance")]
        public string DABal { get; set; }
        [Display(Name="Doc No")]
        public string DocNo { get; set; }
         [Display(Name = "Parent Txn Id")]
        public string ParentTxnId { get; set; }
         [Display(Name = "User Id")]
        public string UserId { get; set; }
    }
    public class PrepaidCardnAcct {

        public Int64? TxnId { get; set; }
        public string AcctNo { get; set; }
        public string CardNo { get; set; }
        public string ReloadAmt { get; set; }
          [Display(Name = "Status")]
        public string SelectedStatus { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        public string Remarks { get; set; }
        public string ReloadDate { get; set; }
        public string UserId { get; set; }
        public string CreationDate { get; set; }
        public Int64? ParentTxnId { get; set; }
        public string BusinessLocation { get; set; }

        public string TxnDate { get; set; }

        public string EffDateFrom { get; set; }

        public string TxnAmt { get; set; }
    }
}