using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.EventConfiguration
{
   public class WebNtfyEvtConfDetailDTO
    {
       public Int64 Id { get; set; }
       public string Type { get; set; }
       public string ShortDescp { get; set; }
       public string Descp { get; set; }
       public string RefTo { get; set; }
       public string RefKey { get; set; }
       public string Status { get; set; }
       public string UpdateDate { get; set; }
       public string UpdateBy { get; set; }
    }
}
