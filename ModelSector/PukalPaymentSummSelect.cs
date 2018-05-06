using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSector
{
   public class PukalPaymentSummSelect
    {
       public string TxnCd { get; set; }
       public string TxnDate { get; set; }
       public string AcctOfficeCd { get; set; }
       public string RefCd { get; set; }
       public string Owner { get; set; }
       public int CycStmtId { get; set; }
       public string StmtDate { get; set; }
       public decimal ChequeNo { get; set; }
       public string ChequeAmt {get;set;}
       public string GLSettlement { get; set; }
       public string Sts { get; set; }
       public string SlipNo { get; set; }
       public string IssBank { get; set; }
    }
}
