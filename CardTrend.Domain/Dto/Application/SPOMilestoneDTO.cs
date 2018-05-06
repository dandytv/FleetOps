using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Application
{
   public class SPOMilestoneDTO
    {
        public int EventId { get; set; }
        public string WorkflowCd { get; set; }
        public string ReqType { get; set; }
        public string ReqValue { get; set; }
        public int TaskNo { get; set; }
        public string TaskDescp { get; set; }
        public Int64 AcctNo { get; set; }
        public Int64 CardNo { get; set; }
        public string CmpyName { get; set; }
        public string Sts { get; set; }
        public DateTime ReqDate { get; set; }
        public DateTime LastUpdDate { get; set; }
        public string ReqBy { get; set; }
    }
}
