using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.MultiplePayment
{
   public class MultipleTxnRecordDTO
    {
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
