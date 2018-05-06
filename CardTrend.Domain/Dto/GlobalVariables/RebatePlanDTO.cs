using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.GlobalVariables
{
   public class RebatePlanDTO
    {
       public RebatePlanDTO()
       {
           ProductItems = new List<ProductListItemDTO>();
       }
       public int PlanId { get; set; }
       public string Descp { get; set; }
       public Int64 Type { get; set; }
       public DateTime EffectiveDate { get; set; }
       public DateTime ExpiredDate { get; set; }
       public DateTime Plan_UpdateDate { get; set; }
       public decimal TierValue { get; set; }
       public decimal BasisValue { get; set; }
       public decimal BillValue { get; set; }
       public DateTime PlanDetail_UpdateDate { get; set; }
       public string UserId { get; set; }
       public IEnumerable<ProductListItemDTO> ProductItems { get; set; }
    }
}
