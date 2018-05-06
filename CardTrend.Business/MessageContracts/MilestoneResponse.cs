using CardTrend.Business.MessageBase;
using CardTrend.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Business.MessageContracts
{
    public class MilestoneResponse : ResponseBase
    {
        public IList<MilestoneDTO> milestoneDTOLst { get; set; }
        public MilestoneResponse()
        {
            milestoneDTOLst = new List<MilestoneDTO>();
        }
        
    }
}
