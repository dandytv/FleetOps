using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.WebDto
{
   public class WebModuleDTO
    {
        public int Level { get; set; }
        public string ModuleId { get; set; }
        public string PageId { get; set; }
        public string CtrlId { get; set; }
        public string ShortDescp { get; set; }
        public string Descp { get; set; }
        public int Sts { get; set; }
        public int HourBitmap { get; set; }
    }
}
