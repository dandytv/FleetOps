using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Collection
{
   public class CollPaymentHstList
    {
       public CollPaymentHstList()
       {
           CollPaymentHsts = new List<CollPaymentHistDTO>();
       }
       public IList<CollPaymentHistDTO> CollPaymentHsts { get; set; }
       public Int64 TotalNoOfRecs { get; set; }
    }
}
