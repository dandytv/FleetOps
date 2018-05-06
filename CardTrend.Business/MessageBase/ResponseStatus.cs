using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageBase
{
    public enum ResponseStatus
    {
        [EnumMember]
        Success = 1,

        [EnumMember]
        Failure = 2,

        [EnumMember]
        Exception = 3
    }
}
