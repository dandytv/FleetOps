using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class SaveAcctSignUpResponse : ResponseBase
    {
        public SaveAcctSignUpResponse()
        {
            returnValue = new ReturnObject();
        }
        public string desp { get; set; }
        public int flag { get; set; }
        public ReturnObject returnValue;
    }
}
