using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class GeneralInfoResponse : ResponseBase
    {
        public GeneralInfoResponse()
        {
            generalInfo = new GeneralInfoModel();
        }
        public GeneralInfoModel generalInfo { get; set; }

    }
}
