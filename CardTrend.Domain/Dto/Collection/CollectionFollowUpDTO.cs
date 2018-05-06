using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Collection
{
   public class CollectionFollowUpDTO
    {
       public string Status { get; set; }
       public string StatusDescp { get; set; }
       public string Priority { get; set; }
       public string PriorityDescp { get; set; }
       public DateTime? EventCreationDate { get; set; }
       public DateTime? DetailCreationDate { get; set; }
       public DateTime? RecallDate { get; set; }
       public DateTime? LastUpdDate { get; set; }
       public string CreatedBy { get; set; }
       public string Remarks { get; set; }
    }
}
