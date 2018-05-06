using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.PinMailer;
using CardTrend.Domain.Dto.PukaAcct;
using CardTrend.Domain.Dto.ReportViewer;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class PukalAcctMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<ReportViewer, ReportViewerDTO>()
                .ForMember(d => d.ColName, m => m.MapFrom(src => src.ColName))
                .ForMember(d => d.RptType, m => m.MapFrom(src => src.RptType))
                ;
            this.CreateMap<PinMailerBatchDTO, PinMailerBatchList>()
                .ForMember(d => d.BatchID, m => m.MapFrom(src => NumberExtensions.ConvertToInt(src.BatchId)))
                .ForMember(d => d.CreationDate, m => m.MapFrom(src => NumberExtensions.DateTimeConverter(src.CreationDate)))
                .ForMember(d => d.Count, m => m.MapFrom(src => NumberExtensions.ConvertToInt(src.Count)))
                ;
            this.CreateMap<PinMailerBatchViewDTO, PinMailerBatchView>()
                .ForMember(d => d.CardNo, m => m.MapFrom(src => src.CardNo.ToString()))
                .ForMember(d => d.CardCreationDate, m => m.MapFrom(src => NumberExtensions.DateTimeConverter(src.CardCreationDate)))
                ;
            this.CreateMap<PukalPaymentDTO, PukalAcctBatchList>()
                .ForMember(d => d.ChequeAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.ChequeAmt))))
                .ForMember(d => d.AreaCode, m => m.MapFrom(src => src.AcctOfficeCd + ":" + src.AcctOfficeCdDescp))
                .ForMember(d => d.StatementDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.StmtDate)))
                .ForMember(d => d.CreationDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.CreationDate)))
                ;
            this.CreateMap<PukalSedutDTO, WebPukalSedutList>()
                .ForMember(d => d.ActivationDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.ActivationDate)))
                .ForMember(d => d.TerminationDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.TerminationDate)))
                .ForMember(d => d.PukalAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.PukalAmt))))
                .ForMember(d => d.StmtDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.StmtDate)))
                ;
            this.CreateMap<PukalAcctBatchDTO, PukalAcctBatchView>()
                .ForMember(d => d.CompanyName, m => m.MapFrom(src => src.CmpyName))
                .ForMember(d => d.SalesAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.SalesAmt))))
                .ForMember(d => d.SedutAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.SedutAmt))))
                .ForMember(d => d.PaymentAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.PaymentAmt))))
                ;
                  
        }
    }
}