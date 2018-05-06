using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.PinMailer;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
   public class PinMailerBatchResponse : ResponseBase
    {
       public PinMailerBatchResponse()
       {
           pinMailerBatchs = new List<PinMailerBatchList>();
           pinMailerBatchViews = new List<PinMailerBatchView>();
       }
       //public IList<PinMailerBatchDTO> pinMailerBatchs { get; set; }
       public IList<PinMailerBatchList> pinMailerBatchs { get; set; }
       public IList<PinMailerBatchView> pinMailerBatchViews { get; set; }
    }
}
