using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.PukaAcct
{
   public class PukalSedutDTO
    {
       public Int64 AcctNo { get; set; }
       public string CmpyName1 { get; set; }
       public DateTime? ActivationDate { get; set; }
       public DateTime? TerminationDate { get; set; }
       public decimal PukalAmt { get; set; }
       public DateTime? StmtDate { get; set; }
       public string Sts { get; set; }
       public string UserId { get; set; }
    }
}
