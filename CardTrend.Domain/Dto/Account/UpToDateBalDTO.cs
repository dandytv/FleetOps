using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.Domain.Dto.Account
{
   public class UpToDateBalDTO
    {
       public string AccountType { get; set; }
       public string ComplexInd { get; set; }
       public decimal? CreditLimit { get; set; }
       public decimal? TempCreditLimit { get; set; }
       public decimal? OpeningBalance { get; set; }
       public decimal? InstantAmt { get; set; }
       public decimal? ClosingBalance { get; set; }
       public decimal? TotalCreditLimit { get; set; }
       public decimal? OnlineAmt { get; set; }
       public decimal? AvailableCredit { get; set; }
       public decimal? QuotaLimit { get; set; }
       public decimal? QuotaUsage { get; set; }
       public decimal? OfflineAmt { get; set; }
       public decimal? BatchAmt { get; set; }
       public IList<SelectListItem> AcctType { get; set; }
    }
}
