using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Fraud
{
   public class FraudCustomerDetailsDTO
    {
        public Int64 AccountNo { get; set; }
        public Int64 EventID { get; set; }
        public string CompanyName { get; set; }
        public string AccountType { get; set; }
        public string ClientType { get; set; }
        public int? AgingDays { get; set; }
        public decimal? AvgSales { get; set; }
        public string AvgSalesDisplay { get; set; }
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
        public string SubsidyNo { get; set; }
    }
}
