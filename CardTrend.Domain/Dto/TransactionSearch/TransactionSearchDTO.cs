using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.TransactionSearch
{
   public class TransactionSearchDTO
    {
       public string StatementDate { get; set; }
       public string TxnDate { get; set; }
       public string AcctNo { get; set; }
       public string CardNo { get; set; }
       public string TermBatch { get; set; }
       public string TaxInvoiceNo { get; set; }
       public string TxnDescp { get; set; }
       public string BillingAmt { get; set; }
       public decimal? TxnAmt { get; set; }

       public string Dealer { get; set; }
       public string TermId { get; set; }
       public string AuthNo { get; set; }
       public string AppvCd { get; set; }
       public Int64? Rrn { get; set; }
       public int? Stan { get; set; }
       public string VATNo { get; set; }
       public string AuthCardNo { get; set; }
       public string PrcsDate { get; set; }
       public string TxnId { get; set; }
       public string ReceiptNo { get; set; }
       public int? BatchNo { get; set; }
       public string VehRegsNo { get; set; }
       public string DriverName { get; set; }
       public string SiteId { get; set; }
       public decimal? ProductQty { get; set; }
       public decimal? Qty { get; set; }
       public decimal? ProductAmt { get; set; }
       public decimal? VATAmt { get; set; }
       public decimal? BaseAmt { get; set; }
       public string VATCd { get; set; }
       public decimal? VATRate { get; set; }
       public string ProductDescp { get; set; }

    }
}
