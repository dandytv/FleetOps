using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Fraud
{
    public class FraudCardDetailDTO
    {
        public FraudCardDetailDTO()
        {
            FraudCards = new List<FraudCardDTO>();
        }
        public Int64 CardNo { get; set; }
        public decimal? AvgSales { get; set; }
        public string CardAvgSalesDisplay {get;set;}
        public decimal? Month1Amount { get; set; }
        public string Month1Date { get; set; }
        public decimal? Month2Amount { get; set; }
        public string Month2Date { get; set; }
        public decimal? Month3Amount { get; set; }
        public string Month3Date { get; set; }
        public decimal? Month4Amount { get; set; }
        public string Month4Date { get; set; }
        public decimal? Month5Amount { get; set; }
        public string Month5Date { get; set; }
        public decimal? Month6Amount { get; set; }
        public string Month6Date { get; set; }
        public decimal? SingleTxn { get; set; }
        public decimal? LitLimit { get; set; }
        public decimal? DailyTxn { get; set; }
        public decimal? DailyLitre { get; set; }
        public int? DailyCnt { get; set; }
        public decimal? MonthlyTxn { get; set; }
        public decimal? MonthlyLitre { get; set; }
        public int? MonthlyCnt { get; set; }
        public List<FraudCardDTO> FraudCards { get; set; }
    }
}
