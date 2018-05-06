using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Applicant
{
   public class CardFinancialInfoDTO
    {
       public decimal? TxnLimit { get; set; }
       public decimal? LitLimit { get; set; }
       public byte? PinExceedCnt { get; set; }
       public string PinAttempted { get; set; }
       public DateTime? PinTriedUpdDate { get; set; }
    }
}
