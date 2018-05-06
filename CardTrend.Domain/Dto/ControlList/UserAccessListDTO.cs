using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.ControlList
{
    public class UserAccessListDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string DeptId { get; set; }
        public string AccessInd { get; set; }
        public string AccessTmpl { get; set; }
    }
}
