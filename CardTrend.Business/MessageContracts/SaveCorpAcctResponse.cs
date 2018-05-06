using CardTrend.Business.MessageBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class SaveCorpAcctResponse : ResponseBase
    {
        public string desp { get; set; }
        public int flag { get; set; } 
    }
}
