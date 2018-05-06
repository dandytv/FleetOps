using CardTrend.Business.MessageBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
   public class LoginResponse : ResponseBase
    {
       public string strReturnCode { get; set; }
    }
}
