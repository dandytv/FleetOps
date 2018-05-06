using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Account
{
   public class ProductDiscountDTO
    {
       public Int64? Id { get; set; }
       public string Datakey1 { get; set; }
       public string ProdGroup { get; set; }
       public string ProductDescp { get; set; }
       public string ProdDiscType { get; set; }
       public string ProdDiscDescp { get; set; }
       public int? PlanId { get; set; }
       public DateTime? EffDate { get; set; }
       public string UserId { get; set; }
       public DateTime? CreationDate { get; set; }
       public string Remarks { get; set; }
       public DateTime? EffEndDate { get; set; }
       public string OnlineInd { get; set; }
    }
}
