using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Application
{
   public class WebMilestoneApplDto
    {
        public string TaskNo { get; set; }
        public string TaskDescp { get; set; }
        public string StsDescp { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastUpdDate { get; set; }
        public string ReasonCd { get; set; }
        public string Remarks { get; set; }
        public string CmpyName1 { get; set; }
        public string RecallDate { get; set; }
        public Int64 RefKey { get; set; }
        public string Priority { get; set; }
    }
}
