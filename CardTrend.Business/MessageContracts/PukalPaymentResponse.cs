using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.PukaAcct;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class PukalPaymentResponse : ResponseBase
    {
        public PukalPaymentResponse()
        {
            pukalPayments = new List<PukalAcctBatchList>();
            pukalSeduts = new List<WebPukalSedutList>();
            pukalAcctBatches = new List<PukalAcctBatchView>();
        }
        public IList<PukalAcctBatchList> pukalPayments { get; set; }
        public IList<WebPukalSedutList> pukalSeduts { get; set; }
        public IList<PukalAcctBatchView> pukalAcctBatches { get; set; }
    }
}
