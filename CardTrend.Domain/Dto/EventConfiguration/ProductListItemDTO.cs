using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.EventConfiguration
{
   public class ProductListItemDTO
    {
        public string UnitPrice { get; set; }
        public string EffectiveFrom { get; set; }
        public string ExpiryDate { get; set; }
        public string LastUpdated { get; set; }
        public string UserId { get; set; }
        public string ProdId { get; set; }
        public string ProductCode { get; set; }
        public string MinPurchaseAmt { get; set; }
        public string SubSeqPurchaseAmt { get; set; }
        public string SubSeqBillingAmt { get; set; }
        public string BillingPlanUserId { get; set; }
        public string BillingPlanLastUpdate { get; set; }
        public string SelectedType { get; set; }
        ////////////////////////////////////////////////////
        public string BitmapAmt { get; set; }
        public string MinIntVal { get; set; }
        public string MaxIntVal { get; set; }
        public string MinMoneyVal { get; set; }
        public string MaxMoneyVal { get; set; }
        public string MinDateVal { get; set; }
        public string MaxDateVal { get; set; }
        public string MinTimeVal { get; set; }
        public string MaxTimeVal { get; set; }
        public string VarCharVal { get; set; }
        public string PeriodType { get; set; }
        public string PeriodInterval { get; set; }
        public string EvtPlanDetailId { get; set; }

    }
}
