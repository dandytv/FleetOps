using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSector
{
   public class CollAgeingHistViewModel
    {
        public string Ageing { get; set; }
        public string Category { get; set; }
        public string TxnAmt { get; set; }
        public string OutstandingAmt { get; set; }
        public string BillingDate { get; set; }
        public string DueDate { get; set; }
        public string GraceDueDate { get; set; }
        public string LatestPaymentReceived { get; set; }
        public string LatestPaymentDate { get; set; }
    }
}
