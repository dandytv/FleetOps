using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Dealer
{
   public class MerchProductPrizeDTO
    {
       public string BusnLocation { get; set; }
       public string ProdCd { get; set; }
       public DateTime? StartDate { get; set; }
       public DateTime? EndDate { get; set; }
       public string Product { get; set; }
       public decimal? Price { get; set; }
       public DateTime? CreationDate { get; set; }
    }
}
