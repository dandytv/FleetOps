using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.SOASummary
{
   public class AcctSOASummaryDetailDTO
    {
       public Int64 CompanyId { get; set; }
       public string CompanyName { get; set; }
       public string BasicCard { get; set; }
       public string CycleDate { get; set; }
       public string LastAgeCode { get; set; }
       public decimal CreditLimit { get; set; }
       public decimal OpeningBalance { get; set; }
       public decimal MTDDebits { get; set; }
       public decimal AvailableCreditLimit { get; set; }
       public decimal CurrentBalance { get; set; }
       public decimal MTDCredits { get; set; }
       public decimal TotalMinimunPayment { get; set; }
       public decimal CurrentDueMinimunPayment { get; set; }
       public decimal PastDueMinimunPayment { get; set; }
       public string PaymentDueDate { get; set; }
       public string LastPaymentDate { get; set; }
       public decimal LastPaymentAmount { get; set; }
    }
}
