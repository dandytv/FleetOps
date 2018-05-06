using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.SOASummary
{
   public class AcctSOATxnDTO
    {
       public string CardHolderNo { get; set; }
       public string DriverCardNo { get; set; }
       public string MerchantName { get; set; }
       public string TransactionDate { get; set; }
       public string TransactionTime { get; set; }
       public string PostDate { get; set; }
       public int? TxnCode { get; set; }
       public string Curr { get; set; }
       public decimal TransactionAmount { get; set; }
       public decimal Amount { get; set; }
       public string ChqRefNo { get; set; }
       public string MerchantID { get; set; }
       public string TradingNameDescription { get; set; }
       public string Mcc { get; set; }
       public string RRN { get; set; }
    }
}
