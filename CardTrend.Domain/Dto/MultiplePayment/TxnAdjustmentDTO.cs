using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.MultiplePayment
{
   public class TxnAdjustmentDTO
    {
       public TxnAdjustmentDTO()
       {
           multipleTxnRecord = new List<MultipleTxnRecordDTO>();
       }
       public DateTime TxnDate { get; set; }
       public DateTime DueDate { get; set; }
       public int TxnCd { get; set; }
       public int AdjTxnCd { get; set; }
       public string InvoiceNo { get; set; }
       public string ChequeNo { get; set; }
       public string ChequeAmt { get; set; }
       public int BatchId { get; set; }
       public string UserId { get; set; }
       public string Owner { get; set; }
       public string SelectedGLSettlement { get; set; }
       public string SlipNo { get; set; }
       public string IssueingBank { get; set; }
       public Int64? RefId { get; set; }
       //
       public string TxnType { get; set; }
       public string AccountNo { get; set; }
       public string CardNo { get; set; }
       public decimal TxnAmount { get; set; }
       public decimal BillingAmount { get; set; }
       public string TxnDescription { get; set; }
       public string Status { get; set; }
       public string Sts { get; set; }
       public Int64? WUId { get; set; }
       public DateTime CreationDate { get; set; }
       public string AppvCd { get; set; }
       public string Dealer { get; set; }
       public string TermId { get; set; }
       public Int64? TxnId { get; set; }
       public string AccountName { get; set; }
       public string PymtType { get; set; }
       public List<MultipleTxnRecordDTO> multipleTxnRecord { get; set; }
    }
}
