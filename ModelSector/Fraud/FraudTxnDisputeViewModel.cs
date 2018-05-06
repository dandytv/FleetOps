using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ModelSector.Fraud
{
   public class FraudTxnDisputeViewModel
    {
        public string EventId { get; set; }
        public string AcctNo { get; set; }
        [DisplayName("Card No")]
        public string SelectedCardNo { get; set; }
        [DisplayName("Transaction Category")]
        public string SelectedTxnCategory { get; set; }
        public IEnumerable<SelectListItem> TxnCategory { get; set; }
        [DisplayName("Transaction Code")]
        public string SelectedTxnCd { get; set; }
        public IEnumerable<SelectListItem> TxnCd { get; set; }
        [DisplayName("Transaction Date")]
        public string TxnDate { get; set; }
        [DisplayName("Transaction Description")]
        public string TxnDesp { get; set; }
        [DisplayName("Transaction Id")]
        public string TxnId { get; set; }
        [DisplayName("From Date")]
        public string FromDate { get; set; }
        [DisplayName("To Date")]
        public string ToDate { get; set; }
        [DisplayName("Auth Card No")]
        public string AuthCardNo { get; set; }
        public string VehRegNo { get; set; }
        public string Stan { get; set; }
        public string RRn { get; set; }
        public string BSNLocation { get; set; }
        public string BSNLocationName { get; set; }
        [DisplayName("Terminal Id")]
        public string TermId { get; set; }
        [DisplayName("Txn Amount")]
        public string TxnAmt { get; set; }
    }
}
