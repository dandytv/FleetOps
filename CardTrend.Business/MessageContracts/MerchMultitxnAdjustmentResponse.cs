using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.MerchantMultiAdjustment;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class MerchMultitxnAdjustmentResponse : ResponseBase
    {
        public MerchMultitxnAdjustmentResponse()
        {
            txtAdjustments = new List<TxnAdjustment>();
            txnAdjustmentDetail = new TxnAdjustment();
            multiPaymentGLCodes = new List<MultiPayment>();
        }
        public List<TxnAdjustment> txtAdjustments { get; set; }
        public TxnAdjustment txnAdjustmentDetail { get; set; }
        public IList<MultiPayment> multiPaymentGLCodes { get; set; }
    }
}
