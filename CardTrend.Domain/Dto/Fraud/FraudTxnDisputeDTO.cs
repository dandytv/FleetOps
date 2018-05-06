using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Fraud
{
    public class FraudTxnDisputeDTO
    {
        public DateTime? TxnDate { get; set; }
        public string BusnName { get; set; }
        public string BusnLocation { get; set; }
        public string Descp { get; set; }
        public decimal BillingTxnAmt { get; set; }
        public string TermId { get; set; }
        public Int64? AuthCardNo { get; set; }
        public Int64? TxnId { get; set; }
        public string VehRegsNo { get; set; }
        public Int64? Rrn { get; set; }
        public int? Stan { get; set; }
    }
}
