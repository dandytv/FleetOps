using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.GlobalVariables
{
   public class ProdGroupRefDTO
    {
       public string ProductGroup { get; set; }
       public string Description { get; set; }
       public string ProductCode { get; set; }
       public string ProductName { get; set; }
       public string ProductCategory { get; set; }
       public string ProductType { get; set; }
       public DateTime UpdateDate { get; set; }
       public string UserId { get; set; }
    }
}
