using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.PukaAcct
{
   public class PukalPaymentDTO
    {
       public int BatchId { get; set; }
       public string RefCd { get; set; }
       public string RefCdDescp { get; set; }
       public string AcctOfficeCd { get; set; }
       public string AcctOfficeCdDescp { get; set; }
       public DateTime StmtDate { get; set; }
       public Int64 ChequeNo { get; set; }
       public decimal ChequeAmt { get; set; }
       public string SlipNo { get; set; }
       public string IssBank { get; set; }
       public DateTime CreationDate { get; set; }
       public string Sts { get; set; }
       public string StsDescp { get; set; }
       public string Owner { get; set; }
    }
}
