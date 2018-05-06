using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.Domain.Dto.ManualSlipEntry
{
  public  class ManualSlipEntryBatchDetailDTO
    {
      public ManualSlipEntryBatchDetailDTO()
      {
          StsList = new List<SelectListItem>();
      }
      public string Dealer { get; set; }
      public string TermId { get; set; }
      public string SiteId { get; set; }
      public string SettleId { get; set; }
      public int BatchId { get; set; }
      public int TxnCd { get; set; }
      public int InvoiceNo { get; set; }
      public DateTime SettleDate { get; set; }
      public int Cnt { get; set; }
      public decimal Amt { get; set; }
      public string Descp { get; set; }
      public int? OrigBatchNo { get; set; }
      public string Sts { get; set; }
      public string UserId { get; set; }
      public IEnumerable<SelectListItem> StsList { get; set; }
    }
}
