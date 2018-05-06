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
using ModelSector.Helpers;

namespace ModelSector
{
   public class TxnAdjustment 
    {
        public string[] Excelheader()
        {
            return new string[] { "Ref Type", "Card No", "Txn Date", "Display Total Amount", "Description", "Status", "userId", "Txn Id", "Creation Date" };
        }
        public string[] ExcelBody()
        {
            return new string[] { RefType, _CardnAccNo.CardNo,TxnDate,DisplayTotAmt,Descp, StsDescp, UserId,TxnId, CreationDate};
        }

       [Display(Name = "transactionid", ResourceType = typeof(locale))]
       public string TxnId { get; set; }
       public CardnAccNo _CardnAccNo { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedTxnCdDdl")]
       [Required(ErrorMessage="Please Select Txn Code")]
       public string SelectedTxnCd { get; set; }
       public IEnumerable<SelectListItem> TxnCd { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "TxnDateLbl")]
       [Required(ErrorMessage="Please Select The Txn Date")]
       public string TxnDate { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "TotAmntLbl")]
       [Required(ErrorMessage="Please Fill In The Txn Amount")]
       public string TotAmnt { get; set; }
       public string DisplayTotAmt { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "BillingTxnAmtLbl")]
       public string BillingTxnAmt { get; set; }
       [Display(Name = "totalpoints", ResourceType = typeof(locale))]
       public string Totpts { get; set; }
       [Display(Name = "descp", ResourceType = typeof(locale))]
       public string Descp { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "InvoiceNoLbl")]
       public string InvoiceNo { get; set; }

       [Display(Name = "collectionid", ResourceType = typeof(locale))]
       public string CollectionId { get; set; }

       [Display(Name = "referencetype", ResourceType = typeof(locale))]
       public string RefType { get; set; }

       [Display(Name = "referenceid", ResourceType = typeof(locale))]
       public string RefId { get; set; }

       [Display(Name = "userid", ResourceType = typeof(locale))]
       public string UserId { get; set; }

       [Display(Name = "creationdate", ResourceType = typeof(locale))]
       public string CreationDate { get; set; }

       [Display(Name = "endorsedby", ResourceType = typeof(locale))]
       public string EndorsedBy { get; set; }

       [Display(Name = "endorseddate", ResourceType = typeof(locale))]
       public string EndorsedDate { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "WithHeldUnsettleIdLbl")]
       public int? WithHeldUnsettleId { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "DeftBusnLocationLbl")]
       public string DeftBusnLocation { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "DeftTermIdLbl")]
       public string DeftTermId { get; set; }
       [Required]
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "ChequeNoLbl")]
       [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
       public string ChequeNo { get; set; }
       public string StsDescp { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedStsDdl")]
       public string SelectedSts { get; set; }
       public IEnumerable<SelectListItem> Sts { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "AppvCdLbl")]
       public string AppvCd { get; set; }
       [Required]
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedOwnerDdl")]
       public string SelectedOwner { get; set; }
       public IEnumerable<SelectListItem> Owner { get; set; }
       [Required]
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "DueDateLbl")]
       public string DueDate { get; set; }

       public string AppvRemarks { get; set; }
       public List<MultipleTxnRecord> MultipleTxnRecord { get; set; }

       public string SelectedTxnType { get; set; }

       public string AcctNo { get; set; }

       public string CardNo { get; set; }

       [Required]
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "ChequeAmtLbl")]
       public string ChequeAmt { get; set; }

       public string RcptNo { get; set; }

       public string RetCd { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedTxnCodeDdl")]
       public string SelectedTxnCode { get; set; }
       public IEnumerable<SelectListItem> TxnCode { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedTxnCodeDdl")]
       public string SelectedAdjTxnCode { get; set; }
       public IEnumerable<SelectListItem> AdjTxnCode { get; set; }
       public string BatchId { get; set; }
       public string TxnNo { get; set; }
       public string CreatedBy { get; set; }

       public string dealer { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SlipNoLbl")]
       public string SlipNo { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedIssueingBankDdl")]
       public string SelectedIssueingBank { get; set; }
       public IEnumerable<SelectListItem> IssueingBank { get; set; }

       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedPaymentTypeDdl")]
       public string SelectedPaymentType { get; set; }
       public IEnumerable<SelectListItem> PaymentType { get; set; }

       public string ShortDescp { get; set; }

       public string TxnCodeQuery { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAcctSignUp", "SelectedGLSettlementDdl")]
       public string SelectedGLSettlement { get; set; }
       public IEnumerable<SelectListItem> GLSettlement { get; set; }
       public string TxnCount { get; set; }
       public string MerchantNo { get; set; }
       public string MerchantName { get; set; }
       public string GroupingBatchId { get; set; }

    }

   public class MultipleTxnRecord
   {
       [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
       public Int32 AcctNo { get; set; }

       public string CardNo { get; set; }
       public string TxnAmt { get; set; }
       public string TxnDescp { get; set; }
       public string InvoiceNo { get; set; }
       public string AppvCd { get; set; }
       public string DeftBusnLocation { get; set; }
       public string DeftTermId { get; set; }
       public string SelectedOwner { get; set; }
       public string SelectedStsDescp { get; set; }
       public string SelectedSts { get; set; }
       public string PaymentAmt { get; set; }
       public string Id { get; set; }
       public string ChequeNo { get; set; }

       public string BookingDate { get; set; }

       public string Pts { get; set; }

       public string WithHeldUnsettleId { get; set; }

       public string CreationDate { get; set; }

       public string Descp { get; set; }

       public string TxnId { get; set; }

       public string AcctName { get; set; }
       public string MerchantAcctNo { get; set; }

   }

}

