using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.MultipleAdjustment
{
   public class TxnMultipleAdjustmentDTO
    {
       public int BatchId { get; set; }
       public int TxnNo { get; set; }
       public string Rrn { get; set; }
       public decimal TotalTxnAmt { get; set; }
       public DateTime CreationDate { get; set; }
       public string TxnCd { get; set; }
       public string ChequeAmt { get; set; }
       public string UserId { get; set; }
       public string Sts { get; set; }
       public string Owner { get; set; }
    }
}
