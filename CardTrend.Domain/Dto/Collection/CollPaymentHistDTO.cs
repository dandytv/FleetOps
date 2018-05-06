using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Collection
{
   public class CollPaymentHistDTO
    {
       public DateTime StatementDate { get; set; }
       public DateTime DueDate { get; set; }
       public DateTime TransactionDate { get; set; }
       public DateTime PostingDate { get; set; }
       public string TransactionDescription { get; set; }
       public decimal TransactionAmount { get; set; }
       public string ApprovalCode { get; set; }
    }
}
