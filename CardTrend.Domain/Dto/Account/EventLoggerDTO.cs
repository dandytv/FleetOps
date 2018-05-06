using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.Domain.Dto.Account
{
   public class EventLoggerDTO
    {
       public string EventType { get; set; }
       public string Status { get; set; }
       public string ReferenceKey { get; set; }
       public string RefDocument { get; set; }
       public string AcctNo { get; set; }
       public string CardNo { get; set; }
       public string ReasonCd { get; set; }
       public string SysInd { get; set; }
       public string EventId { get; set; }
       public string Descp { get; set; }
       public string Module { get; set; }
       public string Indicator { get; set; }
       public string UserId { get; set; }
       public DateTime? EventDate { get; set; }
       public DateTime? ClosedDate { get; set; }
       public DateTime? Reminder { get; set; }
       public DateTime? CreationDate { get; set; }
       //public IEnumerable<SelectListItem> EventTypeLst { get; set; }
       //public IEnumerable<SelectListItem> ReasonCdLst { get; set; }
    }
}
