using CardTrend.Business.MessageBase;
using CardTrend.Domain.WebDto;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.Business.MessageContracts
{
    public class UserAccessResponse : ResponseBase
    {
        public UserAccessResponse()
        {
            RefLibLst = new List<SelectListItem>();
            userTitles = new List<GetUserTitleDto>();
            userAccesses = new List<UserAccess>();
            userAccess = new UserAccess();
        }
        public IList<SelectListItem> RefLibLst { get; set; }
        public IList<GetUserTitleDto> userTitles { get; set; }
        public UserAccess userAccess { get; set; }
        public IList<UserAccess> userAccesses { get; set; }
    }
}
