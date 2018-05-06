using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.NotifSearch
{
    public class NtfyEventConfSearchDTO
    {
        public Int64 EventScheduleId { get; set; }
        public Int64 EvtId { get; set; }
        public string ShortDescp { get; set; }
        public string Type { get; set; }
        public string TypeDescp { get; set; }
        public string Severity { get; set; }
        public string OwnerType { get; set; }
        public string Sts { get; set; }
        public string Refto { get; set; }
        public string Descp { get; set; }
        public string EvtDescp { get; set; }
        public string Refkey { get; set; }
        public string CmpyName { get; set; }
        public int? MaxOccur { get; set; }
        public string OccurPeriodType { get; set; }
        public int? MinIntVal { get; set; }
        public int? MaxIntVal { get; set; }
        public decimal? MinMoneyVal { get; set; }
        public decimal? MaxMoneyVal { get; set; }
        public DateTime? MinDateVal { get; set; }
        public DateTime? MaxDateVal { get; set; }
        public TimeSpan? MinTimeVal { get; set; }
        public TimeSpan? MaxTimeVal { get; set; }
        public string VarCharVal { get; set; }
        public string PeriodType { get; set; }
        public int? PeriodInterval { get; set; }
        public DateTime? LastUpdDate { get; set; }
        public string UserId { get; set; }
        public Int64 ChannelInd { get; set; }
        public Int64 ParamInd { get; set; }
        public DateTime? SentDate { get; set; }
    }
}
