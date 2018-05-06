using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.CardHolder
{
   public class PersonalInfoDTO
    {
       public string Title { get; set; }
       public string LastName { get; set; }
       public string FirstName { get; set; }
       public string NewIcType { get; set; }
       public string NewIc { get; set; }
       public string OldIcType { get; set; }
       public string OldIc { get; set; }
       public string AlternateIcType { get; set; }
       public string AlternateIc { get; set; }
       public string Gender { get; set; }
       public DateTime? DOB { get; set; }
       public int? Income { get; set; }
       public string IncomeBK { get; set; }
       public string Occupation { get; set; }
       public string DeptId { get; set; }
       public string DrivingLic { get; set; }
       public string EntityId { get; set; }
    }
}
