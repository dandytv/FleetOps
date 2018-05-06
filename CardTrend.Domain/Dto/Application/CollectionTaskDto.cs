using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Application
{
   public class CollectionTaskDto
    {
       public string EventId { get; set; }
       public string AcctNo { get; set; }
       public string CmpyName1 { get; set; }
       public string SaleTerritory { get; set; }
       public decimal CollectionAmt { get; set; }
       public DateTime GraceDueDate { get; set; }
    }
}
