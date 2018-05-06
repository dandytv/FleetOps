using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.ManualSlipEntry
{
    public class ManualTxnDTO
    {
        public string Dealer { get; set; }
        public string Termid { get; set; }
        public int TxnCd { get; set; }
        public int InvoiceNo { get; set; }
    }
}
