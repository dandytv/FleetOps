using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.ManualSlipEntry
{
   public class MerchManualTxnDTO
    {
        public string Dealer { get; set; }
        public string TerminalId { get; set; }
        public string SiteId { get; set; }
        public string SettleId { get; set; }
        public string TxnDetailId { get; set; }
        public string ProdCd { get; set; }
        public decimal? ProdAmt { get; set; }
        public string CardExpiry { get; set; }
        public int TxnCd { get; set; }
        public int BatchId { get; set; }
        public int InvoiceNo { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string CardNo { get; set; }
        public string DriverCard { get; set; }
        public decimal TxnAmount { get; set; }
        public string Description { get; set; }
        public string AuthResp { get; set; }
        public string DriverCd { get; set; }
        public string AuthCardExp { get; set; }
        public string AuthNo { get; set; }
        public int? OdometerReading { get; set; }
        public int? Stan { get; set; }
        public int? ArrayCount { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? TotalAmt { get; set; }
        public Int64 Rrn { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? TxnDate { get; set; }
        public string UserId { get; set; }
        public string TxnId { get; set; }
        public string Sts { get; set; }
        public Int64 Ids { get; set; }
        public string VATNo { get; set; }
        public string VATCd { get; set; }
        public decimal? VATAmt { get;set; }
        public decimal? VATRate { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
