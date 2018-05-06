using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Merchant
{
   public class EServiceDTO
    {
       public string BusnLocation { get; set; }
       public string SiteId { get; set; }
       public string BusnName { get; set; }
       public string BankName { get; set; }
       public string BankAcctNo { get; set; }
       public string TradingName { get; set; }
       public DateTime? PrcsDate { get; set; }
       public string LocalDate { get; set; }
       public string LocalTime { get; set; }
       public Int64? CardNo { get; set; }
       public Int64? Rrn { get; set; }
       public decimal? Qty { get; set; }
       public decimal? BillingAmt { get; set; }
       public decimal? MDR { get; set; }
       public decimal? VATAmt { get; set; }
       public decimal? NetAmt { get; set; }
       public string Multiplier { get; set; }
    }
}
