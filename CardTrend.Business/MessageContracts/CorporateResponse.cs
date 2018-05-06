using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto.Corporate;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class CorporateResponse : ResponseBase
    {
        public CorporateResponse()
        {
            coporate = new Corporate();
            corporates = new List<Corporate>();
            generalInfoes = new List<GeneralInfoModel>();
        }
        public Corporate coporate { get; set; }
        public IList<Corporate> corporates { get; set; }
        public IList<GeneralInfoModel> generalInfoes { get; set; }
    }
}
