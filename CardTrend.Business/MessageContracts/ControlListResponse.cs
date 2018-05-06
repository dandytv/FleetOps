using CardTrend.Business.MessageBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.Business.MessageContracts
{
    public class ControlListResponse : ResponseBase
    {
        public ControlListResponse()
        {
            RefLibLst = new List<SelectListItem>();
        }
        public IList<SelectListItem> RefLibLst { get; set; }
    }
}
