using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.GlobalVariables
{
   public class RebatePlanDetailDTO
    {
       public int PlanId { get; set; }
       public string Descp { get; set; }
       public string Type { get; set; }
       public DateTime? EffectiveDate { get; set; }
       public DateTime? ExpiredDate { get; set; }
       public DateTime? PlansUpdateDate { get; set; }
       public decimal MinPurchAmt { get; set; }
       public decimal SubseqPurchAmt { get; set; }
       public decimal SubseqBillingAmt { get; set; }
       public string BillingPlanUserId { get; set; }
       public DateTime? BillingPlanLastUpdate { get; set; }
       public string PlanUserId { get; set; }
    }
}
