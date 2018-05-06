using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.SOASummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class GetAcctSOATxnListResponse : ResponseBase
    {
        public IList<AcctSOATxnDTO> accountSOATxnLst { get; set; }
        public GetAcctSOATxnListResponse()
        {
            accountSOATxnLst = new List<AcctSOATxnDTO>();
        }
    }
}
