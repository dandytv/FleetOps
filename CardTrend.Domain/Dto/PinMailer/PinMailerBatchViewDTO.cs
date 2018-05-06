using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.PinMailer
{
   public class PinMailerBatchViewDTO
    {
       public Int64 SeqNo { get; set; }
       public string Sts { get; set; }
       public Int64 CardNo { get; set; }
       public DateTime CardCreationDate { get; set; }
       public string CompName { get; set; }
       public string DriverName { get; set; }
       public string PIC { get; set; }
       public string Address { get; set; }
       public string StsDescp { get; set; }
    }
}
