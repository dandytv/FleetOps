using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Collection
{
   public class CollectionTaskDTO
    {
       public string SaleTerritory { get; set; }
       public string Owner { get; set; }
       public string RecallDate { get; set; }
       public DateTime? ToRecallDate { get; set; }
       public string CreationDate { get; set; }
       public DateTime? ToCreationDate { get; set; }
       public int EventId { get; set; }
       public Int64 AcctNo { get; set; }
       public string accountNoSelected { get; set; }
       public string CmpyName1 { get; set; }
       public string CorpCd { get; set; }
       public string CorpName { get; set; }
       public decimal CollectionAmt { get; set; }
       public string GraceDueDate { get; set; }
       public int CycAge { get; set; }
       public string Priority { get; set; }
       public string AccountSts { get; set; }
       public string Collectionsts { get; set; }

    }
}
