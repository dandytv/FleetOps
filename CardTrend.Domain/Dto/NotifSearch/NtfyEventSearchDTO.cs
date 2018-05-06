using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.NotifSearch
{
   public class NtfyEventSearchDTO
    {
       public Int64 EvtId { get; set; }
       public string EvtTypeInd { get; set; }
       public string EvtShorDescp { get; set; }
       public string EvtReason { get; set; }
       public string Refto { get; set; }
       public string RefKey { get; set; }
       public string OwnerType { get; set; }
       public string CreationDate { get; set; }
       public string CmpyName { get; set; }
       public string Channel { get; set; }
    }
}
