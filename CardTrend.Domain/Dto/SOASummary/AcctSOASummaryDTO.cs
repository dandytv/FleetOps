using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.SOASummary
{
   public class AcctSOASummaryDTO
    {
       public string Mth { get; set; }
       public string StatementDate { get; set; }
       public decimal ClosingBalance { get; set; }
       public decimal MinimunPayment { get; set; }
       public decimal Debits { get; set; }
       public decimal Credits { get; set; }
       public decimal Sales { get; set; }
       public decimal DBadjustment { get; set; }
       public decimal Charges { get; set; }
       public decimal Payment { get; set; }
       public decimal CRAdjustment { get; set; }
       public string Age { get; set; }
       public string RChq { get; set; }
       public string Lpay { get; set; }
       public string Rv { get; set; }
       public string Dun { get; set; }
    }
}
