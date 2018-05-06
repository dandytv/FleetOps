using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Collection
{
   public class DelinquentAcctsTresholdLimitDTO
    {
       public Int64 AcctNo { get; set; }
       public string CompanyName { get; set; }
       public string CorpAccount { get; set; }
       public string CorporateName { get; set; }
       public string SaleTerritory { get; set; }
       public decimal CreditLimit { get; set; }
       public decimal TempCreditLimit { get; set; }
       public decimal Usage { get; set; }
       public decimal AvailBal { get; set; }
       public string PukalAccountInd { get; set; }
    }
}
