using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Fraud
{
   public class FraudCardDTO
    {
        public string SelectedCardNo { get; set; }
        public List<string> CardNo { get; set; }
        public string AcctNo { get; set; }
        public string EventId { get; set; }
    }
}
