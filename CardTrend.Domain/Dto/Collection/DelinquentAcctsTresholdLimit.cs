using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Collection
{
   public class DelinquentAcctsTresholdLimit
    {
       public DelinquentAcctsTresholdLimit()
       {
           delinquentAcctsTresholdLimits = new List<DelinquentAcctsTresholdLimitDTO>();
       }
       public Int64 tOtalNoOfRecs { get; set; }
       public IList<DelinquentAcctsTresholdLimitDTO> delinquentAcctsTresholdLimits { get; set; }
    }
}
