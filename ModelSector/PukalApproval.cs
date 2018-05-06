using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSector
{
    public class PukalApproval
    {
        public Int64 ID { get; set; }
        public string Refkey { get; set; }
        public int TaskNo { get; set; }
        public string TaskDescp { get; set; }
        public string Priority { get; set; }
        public string CreationDate { get; set; }
        public string LastUpdDate { get; set; }
        public string Sts { get; set; }
        public string StsDescp { get; set; }
        public string RefCd { get; set; }
        public string AreaCode { get; set; }
        public Int64 ChequeNo { get; set; }
        public decimal ChequeAmount { get; set; }
        public string StmtDate { get; set; }
    }
}
