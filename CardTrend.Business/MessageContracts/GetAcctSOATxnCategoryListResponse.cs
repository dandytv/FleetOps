using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.SOASummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class GetAcctSOATxnCategoryListResponse : ResponseBase
    {
        public IList<AcctSOATxnCategoryDTO> acctSOATxnCategory { get; set; }
        public GetAcctSOATxnCategoryListResponse()
        {
            acctSOATxnCategory = new List<AcctSOATxnCategoryDTO>();
        }
    }
}
