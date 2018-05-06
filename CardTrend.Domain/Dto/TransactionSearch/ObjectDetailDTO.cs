using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.TransactionSearch
{
   public class ObjectDetailDTO
    {
        public string Flag { get; set; }
        public string Preifix { get; set; }
        public Int64? AcctNo { get; set; }
        public Int64? CardNo { get; set; }
        public string BusnLocation { get; set; }
        public string MerchAcctNo { get; set; }
    }
}
