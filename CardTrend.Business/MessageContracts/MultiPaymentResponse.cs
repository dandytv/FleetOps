using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.MultiplePayment;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class MultiPaymentResponse : ResponseBase
    {
        public MultiPaymentResponse()
        {
            multiPayment = new MultiPayment();
            multiPayments = new List<MultiPayment>();
        }
        public MultiPayment multiPayment { get; set; }
        public IList<MultiPayment> multiPayments { get; set; }

    }
}
