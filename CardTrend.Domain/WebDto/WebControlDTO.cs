using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.WebDto
{
   public class WebControlDTO
    {
        public string CtrlId { get; set; }
        public int Sts { get; set; }
        public string CtrlDesp { get; set; }
        public int Level { get; set; }
        public string PageId { get; set; }
        public string ShortName { get; set; }
        public string ModuleId { get; set; }
        public string SectionId { get; set; }
    }
}
