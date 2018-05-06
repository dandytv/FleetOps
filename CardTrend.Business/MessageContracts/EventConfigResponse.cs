using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.EventConfiguration;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class EventConfigResponse : ResponseBase
    {
        public EventConfigResponse()
        {
            lookupParameters = new List<LookupParameters>();
            eventRcpts = new List<EventRcptDTO>();
        }
        public List<LookupParameters> lookupParameters { get; set; }
        public List<EventRcptDTO> eventRcpts { get; set; }
        public string CmpyName { get; set; }
    }
}
