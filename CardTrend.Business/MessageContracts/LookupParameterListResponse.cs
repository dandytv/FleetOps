using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.GlobalVariables;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class LookupParameterListResponse : ResponseBase
    {
        public LookupParameters lookupParameter { get; set; }
        public IList<LookupParameters> lookupParameters { get; set; }
        public IList<TmplDisplayer> TmplDisplays { get; set; }
        public LookupParameterListResponse()
        {
            lookupParameters = new List<LookupParameters>();
            TmplDisplays = new List<TmplDisplayer>();
            lookupParameter = new LookupParameters();
        }
    }
}
