using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Account
{
   public class PaymentTxnDTO
    {
       public string TxnType { get; set; }
       public string AccountNo { get; set; }
       public string CardNo { get; set; }
       public DateTime? TxnDate { get; set; }
       public DateTime? DueDate { get; set; }
       public DateTime? BookingDate { get; set; }
       public decimal? TxnAmount { get; set; }
       public decimal? BillingAmount { get; set; }
       public decimal? Pts { get; set; }
       public string TxnDescription { get; set; }
       public string ChequeNo { get; set; }
       public string AppvCd { get; set; }
       public string Status { get; set; }
       public string Sts { get; set; }
       public string UserId { get; set; }
       public string Owner { get; set; }
       public int? TxnCd { get; set; }
       public string TxnId { get; set; }
       public int? WUId { get; set; }
       public DateTime? CreationDate { get; set; }
       public string AppvRemarks { get; set; }
    }
}
