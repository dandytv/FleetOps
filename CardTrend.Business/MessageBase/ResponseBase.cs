using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageBase
{
   public class ResponseBase
    {
       public object Data { get; set; }
       public string Message { get; set; }
       public ResponseStatus Status { get; set; }
    }
}
