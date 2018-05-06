using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.MerchantMultiAdjustment
{
   public class MerchantMultiTxnAdjustmentDTO
    {
       public Int64 BatchId { get; set; }
       public DateTime CreationDate { get; set; }
       public int TxnCode { get; set; }
       public int InvoiceNo { get; set; }
       public int TxnCnt { get; set; }
       public decimal TxnAmount { get; set; }
       public string Owner { get; set; }
       public string Status { get; set; }
    }
}
