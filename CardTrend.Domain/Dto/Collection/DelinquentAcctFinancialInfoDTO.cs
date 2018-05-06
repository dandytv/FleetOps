using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Collection
{
   public class DelinquentAcctFinancialInfoDTO
    {
       public int PaymentTerm { get; set; }
       public Byte DunningCode { get; set; }
       public decimal PermanentCreditLimit { get; set; }
       public decimal TemporaryCreditLimit { get; set; }
       public decimal TotalTar { get; set; }
       public decimal OutstandingAmount { get; set; }
       public decimal OverdueAmount { get; set; }
       public int AgeCode { get; set; }
       public int DelinquentDays { get; set; }
    }
}
