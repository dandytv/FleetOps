using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto;
using CCMS.ModelSector;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class ApplicationMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<Milestone, MilestoneDTO>()
                .ForMember(d => d.TaskNo, m => m.Ignore())
                .ForMember(d => d.ReasonCd, m => m.Ignore())
                .ForMember(d => d.Priority, m => m.Ignore())
                .ForMember(d => d.WorkflowCd, m => m.MapFrom(src => src.workflowcd))
                .ForMember(d => d.StsDescp, m => m.MapFrom(src => src.selectedStatus))
                .ForMember(d => d.CreationDate, m => m.MapFrom(src => NumberExtensions.DateTimeConverter2(src.CreationDate)))
                .ForMember(d => d.TaskNo, m => m.MapFrom(src => src.SelectedTaskNo))
                .ForMember(d => d.ReasonCd, m => m.MapFrom(src => src.selectedReasonCd))
                .ForMember(d => d.Priority, m => m.MapFrom(src => src.selectedPriority))
                .ForMember(d => d.Id, m => m.MapFrom(src => src.ID))
                .ForMember(d => d.BatchId, m => m.MapFrom(src => src.BatchID))
                .ForMember(d => d.RequestType, m => m.MapFrom(src => src.Descp))
                .ForMember(d => d.ReqVal, m => m.MapFrom(src => src.RequestValue))
                .ForMember(d => d.Sts, m => m.MapFrom(src => src.selectedStatus))
                ;

            this.CreateMap<MilestoneDTO, PukalApproval>()
               .ForMember(d => d.ID, m => m.MapFrom(src => src.Id))
               .ForMember(d => d.Refkey, m => m.MapFrom(src => src.RefKey.ToString()))
               .ForMember(d => d.ChequeAmount, m => m.MapFrom(src => src.ChequeAmt))
               .ForMember(d => d.CreationDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.CreationDate)))
               .ForMember(d => d.LastUpdDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.LastUpdDate)))
               .ForMember(d => d.StmtDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.StmtDate)))
               ;
            this.CreateMap<MilestoneDTO, PukalApproval>()
              .ForMember(d => d.ID, m => m.MapFrom(src => src.Id))
              .ForMember(d => d.Refkey, m => m.MapFrom(src => src.RefKey.ToString()))
              .ForMember(d => d.TaskNo, m => m.MapFrom(src => src.TaskNo))
              .ForMember(d => d.TaskDescp, m => m.MapFrom(src => src.TaskDescp))
              .ForMember(d => d.Priority, m => m.MapFrom(src => src.Priority))
              .ForMember(d => d.CreationDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.CreationDate)))
              .ForMember(d => d.LastUpdDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.LastUpdDate)))
              .ForMember(d => d.ChequeAmount, m => m.MapFrom(src => src.ChequeAmt))
              .ForMember(d => d.StmtDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.StmtDate)))
              ;
            // end application
        }
    }
}