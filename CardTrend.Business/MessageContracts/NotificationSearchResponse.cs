using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.EventConfiguration;
using CardTrend.Domain.Dto.NotifSearch;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class NotificationSearchResponse : ResponseBase
    {
       public NotificationSearchResponse()
       {
           lookupParameters = new List<LookupParameters>();
           eventRcpts = new List<EventRcptList>();
       }
       public IList<LookupParameters> lookupParameters { get; set; }
      // public IList<EventRcptDTO> eventRcpts { get; set; }
       public IList<EventRcptList> eventRcpts { get; set; }
    }
}
