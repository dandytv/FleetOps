using CCMS.ModelSector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSector.Fraud
{
   public class FraudCardDetailsViewModel
    {
        [DisplayName("Account No")]
        public string AcctNo { get; set; }
        public string EventId { get; set; }

        [DisplayName("Average Sales")]
        public string CardAvgSales { get; set; }
        public string CardAvgSalesDisplay { get; set; }

        public string CardMonth1Date { get; set; }
        public string CardMonth1Amt { get; set; }

        public string CardMonth2Date { get; set; }
        public string CardMonth2Amt { get; set; }

        public string CardMonth3Date { get; set; }
        public string CardMonth3Amt { get; set; }

        public string CardMonth4Date { get; set; }
        public string CardMonth4Amt { get; set; }

        public string CardMonth5Date { get; set; }
        public string CardMonth5Amt { get; set; }
        public string CardMonth6Date { get; set; }
        public string CardMonth6Amt { get; set; }

        public string SingleTxn { get; set; }
        public string LitLimit { get; set; }
        public string DailyTxn { get; set; }
        public string DailyLitre { get; set; }
        public int? DailyCnt { get; set; }
        public string MonthlyTxn { get; set; }
        public string MonthlyLitre { get; set; }
        public int? MonthlyCnt { get; set; }

        public List<FraudCards> FraudCards { get; set; }
    }
}
