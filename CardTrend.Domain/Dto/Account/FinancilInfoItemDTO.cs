using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Account
{
    public class FinancilInfoItemDTO
    {
        public string TxnId { get; set; }
        public string CardNo { get; set; }
        public decimal? TxnAmt { get; set; }
        public string BookingDate { get; set; }
        public string TxnDate { get; set; }
        public string DueDate { get; set; }
        public string Descp { get; set; }
        public string CreationDate { get; set; }
        public string DriverCardNo { get; set; }
        public string RcptNo { get; set; }
        public int? TxnCd { get; set; }
        public string ShortDescp { get; set; }
        public string SiteId { get; set; }
        public string UserId { get; set; }

    }
}
