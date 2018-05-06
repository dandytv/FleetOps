using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.EventConfiguration
{
   public class EventRcptDTO
    {
        public Int64 Id { get; set; }
        public string EventScheduleId { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public Int64 ChannelInd { get; set; }
        public string LangInd { get; set; }
        public Int64 ContentId { get; set; }
    }
}
