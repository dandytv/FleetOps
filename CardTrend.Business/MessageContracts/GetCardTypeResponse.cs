using CardTrend.Business.MessageBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.Business.MessageContracts
{
    public class GetCardTypeResponse : ResponseBase
    {
        public IList<SelectListItem> CardTypeLst { get; set; }
        public GetCardTypeResponse()
        {
            CardTypeLst = new List<SelectListItem>();
        }
    }
}
