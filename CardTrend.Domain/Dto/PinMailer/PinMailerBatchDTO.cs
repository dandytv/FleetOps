using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.PinMailer
{
   public class PinMailerBatchDTO
    {
       public int BatchId { get; set; }
       public DateTime CreationDate { get; set; }
       public string Sts { get; set; }
       public int Count { get; set; }
    }
}
