using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.ControlList
{
   public class GetProductDTO
    {
       public string ProdCd { get; set; }
       public string Descp { get; set; }
       public decimal? PricePerUnit { get; set; }
    }
}
