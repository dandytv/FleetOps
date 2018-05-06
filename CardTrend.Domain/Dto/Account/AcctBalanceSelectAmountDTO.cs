using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Account
{
   public class AcctBalanceSelectAmountDTO
    {
       public Int64? Ids { get; set; }
       public decimal? BillingAmt { get; set; }
       public Int64? TxnId { get; set; }
       public Int64? CardNo { get; set; }
       public decimal? TxnAmt { get; set; }
       public DateTime? TxnDate { get; set; }
       public DateTime? DueDate { get; set; }
       public string Descp { get; set; }
       public string UserId { get; set; }
       public string BusnLocation { get; set; }
       public string TermId { get; set; }
       public Int64? InvoiceNo { get; set; }
       public string TxnInd { get; set; }
       public Int16? Mti { get; set; }
       public DateTime? CreationDate { get; set; }
       public Int32? TxnCd { get; set; }
       public Int32? PrcsCd { get; set; }
       public string Rrn { get; set; }
       public string Sts { get; set; }
    }
}
