using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Account
{
   public class TempCreditCtrlDTO
    {
       public decimal? CreditLimit { get; set; }
       public DateTime? EffDateFrom { get; set; }
       public DateTime? EffDateTo { get; set; }
       public string UserId { get; set; }
       public DateTime? CreationDate { get; set; }
    }
}
