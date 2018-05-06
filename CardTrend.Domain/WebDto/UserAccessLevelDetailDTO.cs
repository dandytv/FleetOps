using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.WebDto
{
    public class UserAccessLevelDetailDTO
    {
        public int? Lvl { get; set; }
        public Int64? ModuleId { get; set; }
        public string SectionName { get; set; }
        public string SectionStatus { get; set; }
        public Int64? ControlId { get; set; }
        public Int64? SectionId { get; set; }
        public Int64? PageId { get; set; }
        public string Url { get; set; }
        public string ControlType { get; set; }
        public string CtrlSts { get; set; }
        public string ControlDescription { get; set; }
        public string PageDescription { get; set; }
        public string Sts { get; set; }
    }
}
