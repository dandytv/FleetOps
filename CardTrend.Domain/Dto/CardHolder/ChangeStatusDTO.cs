using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.Domain.Dto.CardHolder
{
   public class ChangeStatusDTO
    {
       public string Sts { get; set; }
       public string EventType { get; set; }
       public string ReasonCd { get; set; }
       public string Remarks { get; set; }
       public string AcctNo { get; set; }
       public string CardNo { get; set; }
       public string MerchAcctNo { get; set; }
       public string BusnLocation { get; set; }
       public string AppcId { get; set; }
       public IEnumerable<SelectListItem> CurrentStatus { get; set; }
       public IEnumerable<SelectListItem> ChangeStatusTo { get; set; }
       public IEnumerable<SelectListItem> ReasonCode { get; set; }
       public IEnumerable<SelectListItem> RefType { get; set; }
    }
}
