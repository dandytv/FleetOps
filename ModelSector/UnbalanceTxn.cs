using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CCMS.ModelSector;

namespace ModelSector 
{
    public class UnbalanceTxnSearch
    {
        [DisplayName("Rec Type")]
        public string SelectedRecType { get; set; }
        public IEnumerable<SelectListItem> RecType { get; set; }
        [DisplayName("Txn Id")]
        public string TxnId { get; set; }
        [DisplayName("Txn Date")]
        public string TxnDate { get; set; }
        public string DisplayTxnDate { get; set; }
        [DisplayName("Due Date")]
        public string DueDate { get; set; }
        [DisplayName("Billed Amount")]
        public string BilledAmt { get; set; }
        [DisplayName("Settled Amount")]
        public string SettledAmt { get; set; }
        [DisplayName("Booked Amount")]
        public string BookingAmt { get; set; }
        [DisplayName("Description")]
        public string Descp { get; set; }
        [DisplayName("Account No")]
        public string AcctNo { get; set; }
        [DisplayName("Reference")]
        public string Ref { get; set; }
        [DisplayName("Txn Cd")]
        public string selectedTxnCd { get; set; }
        public IEnumerable<SelectListItem> TxnCd { get; set; }
        [DisplayName("Txn Amount")]
        public string TxnAmount { get; set; }
        public string DisplayTxnAmount { get; set; }

        [DisplayName("UnbalanceTxn Amount")]
        public string UnallocatedAmount { get; set; }


        [DisplayName("LBE")]
        public string LBE { get; set; }

        [DisplayName("SrcTxnId")]
        public string SrcTxnId { get; set; }

        [DisplayName("SrcTxnSequence")]
        public string SrcTxnSequence { get; set; }
        [DisplayName("TxnSequence")]
        public string TxnSequence { get; set; }

        [DisplayName("TgtTxnId")]
        public string TgtTxnId { get; set; }


        [DisplayName("Out StandingAmt")]
        public string OutStandingAmt { get; set; }

         [DisplayName("Orignal Amount")]
        public decimal OrigAmt { get; set; }

        [DisplayName("RE")]
         public int re { get; set; }
        [DisplayName("CNT")]
         public int Cnt { get; set; }
        [DisplayName("ERR")]
         public int Err { get; set; }
        [DisplayName("Prcs Id")]
         public int PrcsId { get; set; }
         [DisplayName("Prcs Date")]
         public string PrcsDate { get; set; }
        [DisplayName("Prcs Name")]
         public string PrcsName { get; set; }
        

        
    }
}
