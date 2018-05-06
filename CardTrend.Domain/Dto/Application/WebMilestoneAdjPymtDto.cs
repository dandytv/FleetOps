using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Application
{
   public class WebMilestoneAdjPymtDto
    {
        public string TaskNo { get; set; }
        public string TaskDescp { get; set; }
        public string StsDescp { get; set; }
        public DateTime CreationDate { get; set; }
        public string LastUpdDate { get; set; }
        public int Id { get; set; }
        public string BatchId { get; set; }
        public Int64 RefKey { get; set; }
        public string Priority { get; set; }
    }
}
