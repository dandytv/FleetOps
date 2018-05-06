using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.WebDto
{
   public class UserAccessLevelDTO
    {
       public byte Lvl { get; set; }
       public string ModuleId { get; set; }
       public Int64? PageId { get; set; }
       public string AccessLevel { get; set; }
       public string Url { get; set; }
       public int? Sts { get; set; }
       public string ShortDescp { get; set; }
       public string Descp { get; set; }
    }
}
