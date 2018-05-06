using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.NotifSearch;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class NotificationSearchMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<NtfyEventConfSearchDTO, LookupParameters>()
                .ForMember(d => d.BitmapAmt, m => m.MapFrom(src => Convert.ToString(src.ParamInd)))
                .ForMember(d => d.EventScheduleId, m => m.MapFrom(src => Convert.ToString(src.EventScheduleId)))
                .ForMember(d => d.Id, m => m.MapFrom(src => Convert.ToString(src.EvtId)))
                .ForMember(d => d.DetailedDescp, m => m.MapFrom(src => src.EvtDescp))
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
                .ForMember(d => d.MinMoneyVal, m => m.MapFrom(src => src.MinMoneyVal.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.MinMoneyVal)) : "0.00"))
                .ForMember(d => d.MaxMoneyVal, m => m.MapFrom(src => src.MaxMoneyVal.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.MaxMoneyVal)) : "0.00"))
                .ForMember(d => d.MinDateVal, m => m.MapFrom(src => NumberExtensions.DateConverter(src.MinDateVal)))
                .ForMember(d => d.MaxDateVal, m => m.MapFrom(src => NumberExtensions.DateConverter(src.MaxDateVal)))
                .ForMember(d => d.MinTimeVal, m => m.MapFrom(src => Convert.ToString(src.MinTimeVal)))
                .ForMember(d => d.MaxTimeVal, m => m.MapFrom(src => Convert.ToString(src.MaxTimeVal)))
                .ForMember(d => d.PeriodInterval, m => m.MapFrom(src => Convert.ToString(src.PeriodInterval)))
                .ForMember(d => d.LastUpdated, m => m.MapFrom(src => NumberExtensions.DateConverter(src.LastUpdDate)))
                .ForMember(d => d.UpdatedBy, m => m.MapFrom(src => src.UserId))
                .ForMember(d => d.ParamInd, m => m.MapFrom(src => Convert.ToString(src.ParamInd)))
                .ForMember(d => d.SentDate, m => m.MapFrom(src => src.SentDate.HasValue ? NumberExtensions.DateConverter(src.SentDate) : ""))
            ;
            this.CreateMap<NtfyEventSearchDTO, LookupParameters>()
                .ForMember(d => d.Id, m => m.MapFrom(src => Convert.ToString(src.EvtId)))
                .ForMember(d => d.SeletedEventInd, m => m.MapFrom(src => src.EvtTypeInd))
                .ForMember(d => d.ShortDescp, m => m.MapFrom(src => src.EvtShorDescp))
                .ForMember(d => d.SelectedReason, m => m.MapFrom(src => src.EvtReason))
                .ForMember(d => d.SelectedRefTo, m => m.MapFrom(src => src.Refto))
                .ForMember(d => d.RefKey, m => m.MapFrom(src => src.RefKey))
                .ForMember(d => d.SelectedOwner, m => m.MapFrom(src => src.OwnerType))
                .ForMember(d => d.CreationDate, m => m.MapFrom(src => src.CreationDate))
                .ForMember(d => d.CompanyName, m => m.MapFrom(src => src.CmpyName))
                .ForMember(d => d.Channel, m => m.MapFrom(src => src.Channel))
                ; 
        }
    }
}