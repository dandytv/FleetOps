using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ModelSector
{
    public class AcctSOA
    {
        [Display(Name="Account No.")]
        public string AcctNo { get; set; }

        [Display(Name = "Statement Date")]
        public string SelectedStmtDate { get; set; }
        public IEnumerable<SelectListItem> StmtDate { get; set; }

        public string Category { get; set; }
        public string BillingTxnAmt { get; set; }
        //public string TxnCd { get; set; }
        public string TxnCdDescp { get; set; }
        public string PrcsId { get; set; }

        [Display(Name="Company Name")]
        public string CompanyName { get; set; }

        [Display(Name="Credit Limit")]
        public string CreditLimit { get; set; }

        public string StmtNo { get; set; }
        public string MDTCANo { get; set; }
        public string Quota { get; set; }

        [Display(Name="Opening Balance")]
        public string OpeningBal { get; set; }

        public string CurrtPurchse { get; set; }
        public string RevovingIntrstChrge { get; set; }
        public string LatePaymentCharge { get; set; }
        public string JonFee { get; set; }
        public string AnnFee { get; set; }
        public string CardReplaceFee { get; set; }
        public string RetrieveFeeReversal { get; set; }
        public string ExcessLimitFee { get; set; }
        public string Topup { get; set; }
        public string PymtThroughBranchesCash { get; set; }
        public string PymtThroughBranchesCheq { get; set; }
        public string RejectedPymtFee { get; set; }
        public string DebitMiscAdjAR { get; set; }
        public string DebitAdjCardReplacementFee { get; set; }
        public string CreditAdjtoLatePymtChrge { get; set; }
        public string CreditAdjforDisc { get; set; }
        public string CreditAdjforSubsClaims { get; set; }
        public string CardholderDisTrans { get; set; }
        public string RepostDisputedTrans { get; set; }
        public string ClseBalance { get; set; }
        public string Month { get; set; }
        public string MinimumPayment { get; set; }
        public string Debits { get; set; }
        public string Credits { get; set; }
        public string Sales { get; set; }
        public string DBAdjust { get; set; }
        public string Charges { get; set; }
        public string Payment { get; set; }
        public string CRAdjust { get; set; }
        public string age { get; set; }
        public string Rchq { get; set; }
        public string Lpay { get; set; }
        public string Rv { get; set; }
        public string Dun { get; set; }

        [Display(Name="Payment Due Date")]
        public string PymtDueDate { get; set; }

        [Display(Name="Past Due Min Payment")]
        public string PastDueMinimumPymt { get; set; }

        [Display(Name="Current Due Min Payment")]
        public string CrrtDueMinimumOymt { get; set; }

        [Display(Name="Total Min Payment")]
        public string TotMinimumPymt { get; set; }

        [Display(Name="Last Age Code")]
        public string LastAgeCd { get; set; }

        [Display(Name="MTD Debits")]
        public string MTDDebits { get; set; }

        [Display(Name="Available Credit Limit")]
        public string AvaiCredLimits { get; set; }

        [Display(Name="Current Balance")]
        public string CurrBalance { get; set; }

        [Display(Name="MTD Credits")]
        public string MTDCreds { get; set; }

        [Display(Name="Last Payment Date")]
        public string LastPymtDate { get; set; }

        [Display(Name="Last Payment Amount")]
        public string LastPymtAmt { get; set; }

        [Display(Name="Basic Card")]
        public string BasicCard { get; set; }

        [Display(Name="Company ID")]
        public string CompanyID { get; set; }

        public string TxnCode { get; set; }
        public string TxnEventCd { get; set; }
        public string TxnDesc { get; set; }
        public string TotalCount { get; set; }
        public string TotalAmt { get; set; }
        public string TotalItemQty { get; set; }
        public string TotalItemAmt { get; set; }
        public string CardHolderNo { get; set; }
        public string DriverCardNo { get; set; }
        public string TxnDate { get; set; }
        public string txnTime { get; set; }
        public string PostDate { get; set; }
        public string TxnAmt { get; set; }
        public string Amt { get; set; }
        public string ChqRefNo { get; set; }
        public string MerchantID { get; set; }
        public string MerchantName { get; set; }
        public string DBAName { get; set; }
        public string MCC { get; set; }
        public string RRn { get; set; }
        public string Curr { get; set; }
    }
}
