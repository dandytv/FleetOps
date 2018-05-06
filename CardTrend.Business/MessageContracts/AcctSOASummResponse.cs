using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.SOASummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class AcctSOASummResponse : ResponseBase
    {
        public IList<AcctSOASummaryDTO> AcctSOASummaryLst { get; set; }
        public AcctSOASummResponse()
        {
            AcctSOASummaryLst = new List<AcctSOASummaryDTO>();
        }
    }
}
