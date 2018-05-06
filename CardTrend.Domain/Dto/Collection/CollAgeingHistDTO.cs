using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Collection
{
   public class CollAgeingHistDTO
    {
       public string AcctNo { get; set; }
       public string CmpyName { get; set; }
       public string Ageing { get; set; }
       public string Category { get; set; }
       public decimal TxnAmt { get; set; }
       public decimal OutstandingAmt { get; set; }
       public string BillingDate { get; set; }
       public string DueDate { get; set; }
       public string GraceDueDate { get; set; }
       public decimal LatestPaymentReceived { get; set; }
       public string LatestPaymentDate { get; set; }
    }
}
