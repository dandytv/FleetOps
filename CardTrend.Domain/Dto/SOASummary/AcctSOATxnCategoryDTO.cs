using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.SOASummary
{
   public class AcctSOATxnCategoryDTO
    {
       public int TransactionCode { get; set; }
       public string TransactionEventCode { get; set; }
       public string TransactionDesc { get; set; }
       public int TotalCount { get; set; }
       public decimal TotalAmount { get; set; }
       public decimal TotalItemQuantity { get; set; }
       public decimal TotalItemAmount { get; set; }
       public DateTime StatementDate { get; set; }
       public string CompanyName { get; set; }
       public Int64 AccountNo { get; set; }
       public string BasicCard { get; set; }
    }
}
