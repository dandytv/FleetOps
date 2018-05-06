using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.CardHolder
{
   public class SkdsDTO
    {
       public string TxnId { get; set; }
       public string accountNo { get; set; }
       public string applId { get; set; }
       public string SKDSNo { get; set; }
       public decimal? Quota { get; set; }
       public DateTime? QuotaFromDate { get; set; }
       public DateTime? QuotaToDate { get; set; }
       public DateTime? EffFromDate { get; set; }
       public DateTime? EffToDate { get; set; }
       public string Reference { get; set; }
       public string Status { get; set; }
       public string Remarks { get; set; }
       public DateTime? LastSubsidyDate { get; set; }
       public DateTime? LastUpdateDate { get; set; }
       public string UserId { get; set; }
       public DateTime? CreationDate { get; set; }
       public string SubsidyLevel { get; set; }
    }
}
