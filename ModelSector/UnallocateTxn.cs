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
  public  class UnsettleTxn
  {

        [DisplayName("Account No")]
        public string AcctNo { get; set; }

        [DisplayName("Batch Id")]
        public string BatchId { get; set; }
      
        [DisplayName("Record Type")]
        public string RecType { get; set; }

        [DisplayName("Cheque No")]
        [Range(0,double.MaxValue)]
        public string CheqNo { get; set; }

        [DisplayName("Payee Name ")]
        public string PayeeName { get; set; }

        [DisplayName("Txn Amount")]
        public string TxnAmt { get; set; }
        [DisplayName("Txn Amount")]
        public string STxnAmt { get; set; }

        [DisplayName("Txn Code")]
        public int? TxnCd { get; set; }

        [DisplayName("TxnId")]
        public string TxnId { get; set; }
      
        [DisplayName("Last Update Date")]
        public string LastUpdDate { get; set; }

        [DisplayName("Creation Date")]
        public string creationDate { get; set; }

        [DisplayName("Txn Date")]
        public string TxnDate { get; set; }

        [DisplayName("Booking Date")]
        public string BookingDate { get; set; }

        [DisplayName("Status")]
        public string SelectedSts { get; set; }
        public IEnumerable<SelectListItem> Sts { get; set; }

        [DisplayName("Description")]
        public string Descp { get; set; }

        [DisplayName("UserId")]
        public string UserId { get; set; }
    }
}
