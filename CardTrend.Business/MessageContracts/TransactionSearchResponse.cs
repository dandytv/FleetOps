using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.TransactionSearch;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class TransactionSearchResponse : ResponseBase
    {
        public TransactionSearchResponse()
        {
            transactionSearches = new List<AcctPostedTxnSearch>();
            merchPostedTxnSearches = new List<MerchPostedTxnSearch>();
            objectDetail = new ObjectDetail();
        }
        public IList<AcctPostedTxnSearch> transactionSearches { get; set; }
        public IList<MerchPostedTxnSearch> merchPostedTxnSearches { get; set; }
        public ObjectDetail objectDetail { get; set; }
    }
}
