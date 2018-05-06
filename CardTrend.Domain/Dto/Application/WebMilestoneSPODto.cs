using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Application
{
   public class WebMilestoneSPODto
    {
        public int Id { get; set; }
        public string TaskNo { get; set; }
        public string TaskDescp { get; set; }
        public string StsDescp { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastUpdDate { get; set; }
        public Int32 RefKey { get; set; }
        public string RequestType { get; set; }
        public string CardNo { get; set; }
        public string AcctNo { get; set; }
        public string ReqVal { get; set; }
        public string WorkflowCd { get; set; }
        public string CmpyName { get; set; }
        public string RequestBy { get; set; }
        public string Sts { get; set; }

    }
}
