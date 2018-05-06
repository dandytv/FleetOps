using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Application
{
    public class IssContactDTO
    {
        public string RefTo { get; set; }
        public string RefKey { get; set; }
        public string RefCd { get; set; }
        public string ContactType { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public string ContactStatus { get; set; }
        public string Function { get; set; }
        public string Status { get; set; }
        public string Occupation { get; set; }
        public string EmailAddr { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UserId { get; set; }
    }
}
