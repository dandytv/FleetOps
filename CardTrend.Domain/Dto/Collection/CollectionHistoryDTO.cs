using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Collection
{
   public class CollectionHistoryDTO
    {
       public int CollectNo { get; set; }
       public string Priority { get; set; }
       public string CollectStatus { get; set; }
       public string UserId { get; set; }
       public string CloseDate { get; set; }
       public string CreationDate { get; set; }
    }
}
