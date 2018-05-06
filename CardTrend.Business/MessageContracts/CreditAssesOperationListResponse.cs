using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.Corporate;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class CreditAssesOperationListResponse : ResponseBase
    {
        public IList<CreditAssesOperation> creditAssesOperationLst { get; set; }
        public CreditAssesOperationListResponse()
        {
            creditAssesOperationLst = new List<CreditAssesOperation>();
        }
    }
}
