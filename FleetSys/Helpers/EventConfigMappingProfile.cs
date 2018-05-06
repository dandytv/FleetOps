using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.EventConfiguration;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class EventConfigMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<WebNtfyEvtConfDetailDTO, LookupParameters>()
                .ForMember(d => d.EventTypeId, m => m.MapFrom(src => Convert.ToString(src.Id)))
                .ForMember(d => d.SelectedEventType, m => m.MapFrom(src => src.Type))
                .ForMember(d => d.DetailedDescp, m => m.MapFrom(src => src.Descp))
                .ForMember(d => d.SelectedRefTo, m => m.MapFrom(src => src.RefTo))
                .ForMember(d => d.SelectedStatus, m => m.MapFrom(src => src.Status))
                .ForMember(d => d.LastUpdated, m => m.MapFrom(src => src.UpdateDate))
                .ForMember(d => d.UpdatedBy, m => m.MapFrom(src => src.UpdateBy))
                ;
            this.CreateMap<NtfyEventConfDTO, LookupParameters>()
                .ForMember(d => d.BitmapAmt, m => m.MapFrom(src => Convert.ToString(src.ParamInd)))
                .ForMember(d => d.EvtPlanDetailId, m => m.MapFrom(src => Convert.ToString(src.EventPlanDetailId)))
                .ForMember(d => d.EventScheduleId, m => m.MapFrom(src => Convert.ToString(src.EventScheduleId)))
                .ForMember(d => d.EventTypeId, m => m.MapFrom(src => Convert.ToString(src.EventTypeId)))
                .ForMember(d => d.EventPlanId, m => m.MapFrom(src => Convert.ToString(src.EventPlanId)))
                .ForMember(d => d.DetailedDescp, m => m.MapFrom(src => src.Descp))
                .ForMember(d => d.type, m => m.MapFrom(src => src.Type))
                .ForMember(d => d.TypeDesc, m => m.MapFrom(src => src.TypeDescp))
                .ForMember(d => d.SelectedPriority, m => m.MapFrom(src => src.Severity))
                .ForMember(d => d.SelectedOwner, m => m.MapFrom(src => src.OwnerType))
                .ForMember(d => d.SelectedStatus, m => m.MapFrom(src => src.Sts))
                .ForMember(d => d.SelectedRefTo, m => m.MapFrom(src => src.Refto))
                .ForMember(d => d.RefKey, m => m.MapFrom(src => src.Refkey))
                .ForMember(d => d.CompanyName, m => m.MapFrom(src => src.CmpyName))
                .ForMember(d => d.MaxOccur, m => m.MapFrom(src => Convert.ToString(src.MaxOccur)))
                .ForMember(d => d.SelectedFrequency, m => m.MapFrom(src => src.OccurPeriodType))
                .ForMember(d => d.MinIntVal, m => m.MapFrom(src => Convert.ToString(src.MinIntVal)))
                .ForMember(d => d.MaxIntVal, m => m.MapFrom(src => Convert.ToString(src.MaxIntVal)))
                .ForMember(d => d.MinMoneyVal, m => m.MapFrom(src => src.MinMoneyVal.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.MinMoneyVal)) : ""))
                .ForMember(d => d.MaxMoneyVal, m => m.MapFrom(src => src.MaxMoneyVal.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.MaxMoneyVal)) : ""))
                .ForMember(d => d.MinDateVal, m => m.MapFrom(src => NumberExtensions.DateConverter(src.MinDateVal)))
                .ForMember(d => d.MaxDateVal, m => m.MapFrom(src => NumberExtensions.DateConverter(src.MaxDateVal)))
                .ForMember(d => d.MinTimeVal, m => m.MapFrom(src => Convert.ToString(src.MinTimeVal)))
                .ForMember(d => d.MaxTimeVal, m => m.MapFrom(src => Convert.ToString(src.MaxTimeVal)))
                .ForMember(d => d.PeriodInterval, m => m.MapFrom(src => Convert.ToString(src.PeriodInterval)))
                .ForMember(d => d.ApplyAllInd, m => m.MapFrom(src => NumberExtensions.BoolConverter(src.AllAppyInd)))
                .ForMember(d => d.DefaultInd, m => m.MapFrom(src => NumberExtensions.BoolConverter(src.DefaultInd)))
                .ForMember(d => d.LastUpdated, m => m.MapFrom(src => NumberExtensions.DateConverter(src.LastUpdDate)))
                .ForMember(d => d.UpdatedBy, m => m.MapFrom(src => src.UserId))
                .ForMember(d => d.ParamInd, m => m.MapFrom(src => Convert.ToString(src.ParamInd)))
                .ForMember(d => d.NotifyInd, m => m.MapFrom(src => NumberExtensions.ConvertInt(src.EvtTypeChannelInd)))
               ;
            this.CreateMap<LookupParameters, NtfyEventConfDTO>()
                 .ForMember(d => d.EventScheduleId, m => m.MapFrom(src => Convert.ToInt64(src.EventScheduleId)))
                 .ForMember(d => d.EventTypeId, m => m.MapFrom(src => Convert.ToInt64(src.EventTypeId)))
                 .ForMember(d => d.EventPlanId, m => m.MapFrom(src => Convert.ToInt64(src.EventPlanId)))
                 .ForMember(d => d.ShortDescp, m => m.MapFrom(src => src.ShortDescp))
                 .ForMember(d => d.OwnerType, m => m.MapFrom(src => src.SelectedOwner))
                 .ForMember(d => d.Sts, m => m.MapFrom(src => src.SelectedStatus))
                    //.ForMember(d => d.Sts, m => m.MapFrom(src => src.SelectedStatus))
                 .ForMember(d => d.Refto, m => m.MapFrom(src => src.SelectedRefTo))
                 .ForMember(d => d.Refkey, m => m.MapFrom(src => src.RefKey))
                 .ForMember(d => d.MaxOccur, m => m.MapFrom(src => Convert.ToInt32(src.MaxOccur)))
                 .ForMember(d => d.Frequency, m => m.MapFrom(src => src.SelectedFrequency))
                 .ForMember(d => d.UserId, m => m.MapFrom(src => src.UserId))
                 .ForMember(d => d.EvtTypeChannelInd, m => m.MapFrom(src => Convert.ToString(src.NotifyInd)))
                 .ForMember(d => d.DefaultInd, m => m.MapFrom(src => src.DefaultInd == true ? "Y" : "N"))
                 .ForMember(d => d.ProductItems, m => m.MapFrom(src => CustomMapperExtensions.MapIEnumerableToList(src.ProductItems)))
                 .ForMember(d => d.eventRcpts, m => m.MapFrom(src => CustomMapperExtensions.MapIEnumerableToList(src._EventRcptList)))
                 ;
            this.CreateMap<EventRcptDTO, EventRcptList>()
                .ForMember(d => d.ChannelInd, m => m.MapFrom(src => src.ChannelInd))
                .ForMember(d => d.ContactName, m => m.MapFrom(src => src.ContactName))
                .ForMember(d => d.ContactNo, m => m.MapFrom(src => src.ContactNo))
                .ForMember(d => d.LangInd, m => m.MapFrom(src => src.LangInd))
                .ForMember(d => d.Id, m => m.MapFrom(src => src.Id))
               ;
        }

    }
}