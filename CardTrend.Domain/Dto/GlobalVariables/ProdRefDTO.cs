using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.GlobalVariables
{
   public class ProdRefDTO
    {
       public ProdRefDTO()
       {
           ProductItems = new List<ProductListItemDTO>();
       }
       public Int64? ProdId { get; set; }
       public string ProdCd { get; set; }
       public string ProdDescp { get; set; }

       //public string ProductCode { get; set; }
       public string ProductName { get; set; }
       public string ShortDescription { get; set; }
       public string ProductCategory { get; set; }
       public string ProductType { get; set; }
       public decimal? UnitPrice { get; set; }
       public int? BillingPlan { get; set; }
       public string UpdateDate { get; set; }
       public string EffDate { get; set; }
       public string EffEndDate { get; set; }
       public string Flag { get; set; }
       public string UserId { get; set; }
       public string UpdatedOn { get; set; }
       public IEnumerable<ProductListItemDTO> ProductItems { get; set; }
    }
}
