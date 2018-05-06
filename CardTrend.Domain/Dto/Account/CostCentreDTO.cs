using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Account
{
    public class CostCentreDTO
    {
        public string RefTo { get; set; }
        public string RefKey { get; set; }
        public string CostCentre { get; set; }
        public string Descp { get; set; }
        public string PersonInCharge { get; set; }
    }
}
