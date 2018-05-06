using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.WebDto
{
   public class WebPageSectionDTO
    {
        public int Level { get; set; }
        public string CtrlId { get; set; }
        public string PageId { get; set; }
        public string CtrlType { get; set; }
        public string URL { get; set; }
        public string Descp { get; set; }
        public int Len { get; set; }
        public string Fill { get; set; }
        public int MaxRow { get; set; }
        public int RefType { get; set; }
        public int Sts { get; set; }
        public string Section { get; set; }
        public string SectionId { get; set; }
        public string ModuleId { get; set; }
        public int SectionStatus { get; set; }
    }
}
