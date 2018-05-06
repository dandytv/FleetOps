using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.EventConfiguration
{
   public class NtfyEventConfDTO
    {
       public Int64? EventScheduleId { get; set; }
       public string Frequency { get; set; }
       public Int64? EventTypeId { get; set; }
       public Int64? EventPlanId { get; set; }
       public Int64? EventPlanDetailId { get; set; }
       public string ShortDescp { get; set; }
       public string Type { get; set; }
       public string TypeDescp { get; set; }
       public string Severity { get; set; }
       public string OwnerType { get; set; }
       public string Sts { get; set; }
       public string Descp { get; set; }
       public string Refto { get; set; }
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
       public string AllAppyInd { get; set; }
       public DateTime? LastUpdDate { get; set; }
       public string UserId { get; set; }
       public Int64? ParamInd { get; set; }
       public string DefaultInd { get; set; }
       public string EvtTypeChannelInd { get; set; }
       public IEnumerable<ProductListItemDTO> ProductItems { get; set; }
       public IEnumerable<EventRcptDTO> eventRcpts { get; set; }
    }
}
