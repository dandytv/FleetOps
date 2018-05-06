using CardTrend.Business.MessageBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.Business.MessageContracts
{
   public class GetDataVersionResponse : ResponseBase
    {
        public IList<SelectListItem> dataVersionLst { get; set; }
        public GetDataVersionResponse()
        {
            dataVersionLst = new List<SelectListItem>();
        }
    }
}
