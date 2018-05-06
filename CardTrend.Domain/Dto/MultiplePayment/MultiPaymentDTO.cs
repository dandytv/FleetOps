using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.MultiplePayment
{
  public class MultiPaymentDTO
    {
      public MultiPaymentDTO()
      {
          MultipleTxnRecordList = new List<MultipleTxnRecordDTO>();
      }
      public int BatchId { get; set; }
      public string CreationDate { get; set; }
      public string TxnCdDescp { get; set; }
      public int TxnCnt { get; set; }
      public string RefNo { get; set; }
      public decimal ChequeAmt { get; set; }
      public string RefKey { get; set; }
      public int ChequeNo { get; set; }
      public decimal BatchTotalAmt { get; set; }
      public string Owner { get; set; }
      public string AppvSts { get; set; }

      public int TxnCd { get; set; }
      public string TxnId { get; set; }
      public DateTime? TxnDate { get; set; }
      public string AcctNo { get; set; }
      public decimal TxnAmt { get; set; }
      public DateTime? DueDate { get; set; }
      public string Descp { get; set; }
      public string IssuingBank { get; set; }
      public string SlipNo { get; set; }
      public string Sts { get; set; }
      public string Rrn { get; set; }
      public string AccountName { get; set; }
      public string PymtType { get; set; }
      public string SettleVal { get; set; }
      public IEnumerable<MultipleTxnRecordDTO> MultipleTxnRecordList { get; set; }
    }
}
