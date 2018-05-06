using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Account
{
  public class BillingItemDTO
    {
      public string Level { get; set; }
      public string Descp { get; set; }
      public DateTime? TxnDate { get; set; }
      public string CloseAt { get; set; }
      public decimal? BillingAmt { get; set; }
      public decimal? SettledAmt { get; set; }
      public string Differ { get; set; }
      public string DueDate { get; set; }
      public string SettledDate { get; set; }
      public string Sts { get; set; }
      public string UserId { get; set; }
      public string CreationDate { get; set; }
      public decimal? TARBalance { get; set; }
      public decimal? TotalBillingTxnAmt { get; set; }
      public decimal? TotalSettledAmt { get; set; }
      public string AcctNo { get; set; }
      public int? TxnCd { get; set; }
      public Int64 TxnId { get; set; }
      public Int64 RefId { get; set; }
      public Int64? XrefRefId { get; set; }

    }
}
