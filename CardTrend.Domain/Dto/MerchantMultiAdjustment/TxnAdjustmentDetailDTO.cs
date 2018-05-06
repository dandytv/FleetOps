using CardTrend.Domain.Dto.MultiplePayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.MerchantMultiAdjustment
{
   public class TxnAdjustmentDetailDTO
    {
       public Int64 Ids { get; set; }
       public string Description { get; set; }
       public int TxnCd { get; set; }
       public string TxnType { get; set; }
       public string MerchantNo { get; set; }
       public string MerchantName { get; set; }
       public DateTime TxnDate { get; set; }
       public string ChequeAmt { get; set; }
       public string Owner { get; set; }
       public decimal Amt { get; set; }
       public string Sts { get; set; }
       public int GroupingBatchId { get; set; }
       public Int64 BatchId { get; set; }
       public int InvoiceNo { get; set; }
       public string ApprovalStatus { get; set; }
       public string ApprovalDesc { get; set; }
       public List<MultipleTxnRecordDTO> multipleTxnRecord { get; set; }
    }
}
