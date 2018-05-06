using CardTrend.Business.MessageBase;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class SecurityOpResponse : ResponseBase
    {
        public SecurityOpResponse()
        {
            webModules = new List<WebModule>();
            webPages = new List<WebPage>();
            webPageSections = new List<WebPageSection>();
        }
        public List<WebModule> webModules { get; set; }
        public List<WebPage> webPages { get; set; }
        public List<WebPageSection> webPageSections { get; set; }
    }
}
