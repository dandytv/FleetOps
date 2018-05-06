using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.PukaAcct
{
   public class PukalAcctBatchDTO
    {
       public long AcctNo { get; set; }
       public string CmpyName { get; set; }
       public int SalesAmt { get; set; }
       public decimal SedutAmt { get; set; }
       public decimal PaymentAmt { get; set; }
    }
}
