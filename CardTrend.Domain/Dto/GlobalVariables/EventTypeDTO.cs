using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.GlobalVariables
{
   public class EventTypeDTO
    {
       public EventTypeDTO()
       {
           ProductItems = new List<ProductListItemDTO>();
       }
       public Int64? EvtTypeID { get; set; }
       public Int64? EvtPlanId { get; set; }
       public Int64? EvtPlanDetailId { get; set; }
       public string ShortDescription { get; set; }
       public string Type { get; set; }
       public string Severity { get; set; }
       public string Scope { get; set; }
       public string Status { get; set; }
       public string ApplyAllInd { get; set; }
       public string FullDescription { get; set; }
       public Int64? BitmapAmt { get; set; }
       public int? TotalOccurs { get; set; }
       public string SetFrequencyType { get; set; }
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
       public Int64 NtfyInd { get; set; }
       public DateTime? UpdateOn { get; set; }
       public string Updateby { get; set; }
       public string DefaultInd { get; set; }
       public IEnumerable<ProductListItemDTO> ProductItems { get; set; }
    }
}
