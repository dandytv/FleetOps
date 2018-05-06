using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto
{
   public class ReturnObject
    {
       public string RetCd { get; set; }
       public string BatchId { get; set; }
       public string ChequeNo { get; set; }
       public string BusnLocation { get; set; }
       public string ApplId { get; set; }
       public string AppcId { get; set; }
       public string DocPath { get; set; }
       public string EntityId { get; set; }
       public string TxnDetailId { get; set; }
       public string TxnId { get; set; }
       public string SettleId { get; set; }
       public string NewcardNo { get; set; }
       public Int64 NewEventSchId { get; set; }
    }
}
