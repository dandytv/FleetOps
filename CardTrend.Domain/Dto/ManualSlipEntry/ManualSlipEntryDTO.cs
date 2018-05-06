using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.ManualSlipEntry
{
    public class ManualSlipEntryDTO
    {
        public string Dealer { get; set; }
        public string TerminalId { get; set; }
        public string SiteId { get; set; }
        public string TxnId { get; set; }
        public int BatchId { get; set; }
        public int InvoiceNo { get; set; }
        public DateTime SettleDate { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
        public string TxnDescription { get; set; }
        public string SettleId { get; set; }
    }
}
