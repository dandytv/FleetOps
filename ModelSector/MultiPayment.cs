using ModelSector.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ModelSector
{
   public class MultiPayment
    {
        public IEnumerable<SelectListItem> TxnCode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMultiPayment", "SelectedTxnCodeDdl")]
        public string SelectedTxnCode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMultiPayment", "TxnDateLbl")]
        public string TxnDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMultiPayment", "DueDateLbl")]
        public string DueDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMultiPayment", "ChequeAmtLbl")]
        public string ChequeAmt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendMultiPayment", "ChequeNoLbl")]
        public string ChequeNo { get; set; }

         public object ReceiptId { get; set; }

         public object RetCd { get; set; }

         public string BatchId { get; set; }

         public string AcctNo { get; set; }

         public string TxnType { get; set; }

         public string TxnDescp { get; set; }

         public string TxnAmt { get; set; }

         public string BillingAmt { get; set; }

         public string Descp { get; set; }

         public IEnumerable<SelectListItem> Sts { get; set; }
         public string SelectedSts { get; set; }

         public string UserId { get; set; }

         public string CreationDate { get; set; }

         public string TxnId { get; set; }

         public string TxnCnt { get; set; }
         [DisplayNameLocalizedAttribute("CardtrendMultiPayment", "SelectedOwnerDdl")]
         public string SelectedOwner { get; set; }
         public IEnumerable<SelectListItem> Owner { get; set; }

         public string RefCd { get; set; }

         public string SelectedTxnType { get; set; }
         public IEnumerable<MultipleTxnRecord> MultipleTxnRecord { get; set; }
         [DisplayNameLocalizedAttribute("CardtrendMultiPayment", "SelectedIssueingBankDdl")]
         public string SelectedIssueingBank { get; set; }
         public IEnumerable<SelectListItem> IssueingBank { get; set; }

         public string txnCd { get; set; }
         [DisplayNameLocalizedAttribute("CardtrendMultiPayment", "SlipNoLbl")]
         public string SlipNo { get; set; }

         public string GLTxnCode { get; set; }

         public string GLDescp { get; set; }

         public string GLCodeDescp { get; set; }

         [DisplayName("")]
         public string GLCode { get; set; }
         [DisplayNameLocalizedAttribute("CardtrendMultiPayment", "SelectedPaymentTypeDdl")]
         public string SelectedPaymentType { get; set; }
         public IEnumerable<SelectListItem> PaymentType { get; set; }
         [DisplayNameLocalizedAttribute("CardtrendMultiPayment", "SelectedAdjTxnCodeDdl")]
         public string SelectedAdjTxnCode { get; set; }
         public IEnumerable<SelectListItem> AdjTxnCode { get; set; }
         [DisplayNameLocalizedAttribute("CardtrendMultiPayment", "SelectedGLSettlementDdl")]
         public string SelectedGLSettlement { get; set; }
         public IEnumerable<SelectListItem> GLSettlement { get; set; }

         public string RefNo { get; set; }
    }


}
