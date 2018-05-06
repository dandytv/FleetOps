using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Merchant
{
   public class MerchPostedTxnSearchDTO
    {
       public string Dealer { get; set; }
       public string TermBatch { get; set; }
       public string TxnDate { get; set; }
       public string CardNo { get; set; }
       public string TxnDescp { get; set; }
       public string TxnAmt { get; set; }
       public string TermId { get; set; }
       public string AuthNo { get; set; }
       public string AuthCardNo { get; set; }
       public string PrcsDate { get; set; }
       public string TxnId { get; set; }
    }
}
