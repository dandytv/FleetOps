using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Account
{
   public class CreditLimitHistoryDTO
    {
       public string CreditLimit { get; set; }
       public string PaymentMode { get; set; }
       public string PaymentTerms { get; set; }
       public string SalesTerritory { get; set; }
       public DateTime MaintenanceDateTime { get; set; }
       public string QuantitativeRating { get; set; }
       public string QualitativeRating { get; set; }
       public string AccountNo { get; set; }
       public string DepositType { get; set; }
       public string Field { get; set; }
       public string From { get; set; }
       public string To { get; set; }
       public string UserId { get; set; }
       public DateTime CreationDate { get; set; }
    }
}
