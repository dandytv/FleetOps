using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.ManualSlipEntry
{
   public class ManualTxnProductDTO
    {
       public int? BatchId { get; set; }
       public string Prod { get; set; }
       public decimal? Quantity { get; set; }
       public decimal? ProdAmount { get; set; }
       public string ProdDescription { get; set; }
       public DateTime? CreationDate { get; set; }
       public string UserId { get; set; }
       public DateTime? LastUpdateDate { get; set; }
       public string SettleId { get; set; }
       public string TxnId { get; set; }
       public string TxnDetailId { get; set; }
       public decimal? VATAmt { get; set; }
       public string VATCd { get; set; }
       public decimal? VATRate { get; set; }
    }
}
