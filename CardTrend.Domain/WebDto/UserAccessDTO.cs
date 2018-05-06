using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.WebDto
{
  public class UserAccessDTO
    {
      public string UserId { get; set; }
      public string Name { get; set; }
      public string Sts { get; set; }
      public string Password { get; set; }
      public string MapUserId { get; set; }
      public string ContactNo { get; set; }
      public string EmailAddr { get; set; }
      public string Title { get; set; }
      public string DeptId { get; set; }
      public string PrivilegeCd { get; set; }
      public string AccessInd { get; set; }
      public DateTime? LastLogin { get; set; }
      public DateTime? CreationDate { get; set; }
      public string CreateBy { get; set; }
      public string ChangePassInd { get; set; }
      public string AccessTmpl { get; set; }
    }
}
