using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.MultipleAdjustment;
using CardTrend.Domain.Dto.MultiplePayment;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class MultipleTxnOpResponse : ResponseBase
    {
        public MultipleTxnOpResponse()
        {
            txtAdjustments = new List<TxnAdjustment>();
            txnAdjustment = new TxnAdjustment();
        }
        public TxnAdjustment txnAdjustment { get; set; }
        public IList<TxnAdjustment> txtAdjustments { get; set; }

    }
}
