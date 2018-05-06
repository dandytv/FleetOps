using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Corporate
{
   public class CorporateDTO
    {
       public string CorporateCode { get; set; }
       public string ComplexAcctInd { get; set; }
       public string CorporateName { get; set; }
       public decimal TradeLimit { get; set; }
       public string InvoiceCenter { get; set; }
       public string PaymentCenter { get; set; }
       public string PersonInCharge { get; set; }
       public string UserId { get; set; }
    }
}
