using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.Corporate;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class CorporateMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<Corporate, CorporateDTO>()
                .ForMember(d => d.CorporateCode, m => m.MapFrom(src => src.CorpCd))
                .ForMember(d => d.ComplexAcctInd, m => m.MapFrom(src => src.ComplexInd == true ? "Y" : "N"))
                .ForMember(d => d.CorporateName, m => m.MapFrom(src => src.CorpName))
                .ForMember(d => d.TradeLimit, m => m.MapFrom(src => Convert.ToDecimal(src.TradeCreditLimit)))
                .ForMember(d => d.InvoiceCenter, m => m.MapFrom(src => src.InvBillInd == true ? "Y" : "N"))
                .ForMember(d => d.PaymentCenter, m => m.MapFrom(src => src.PymtInd == true ? "Y" : "N"))
                .ForMember(d => d.PersonInCharge, m => m.MapFrom(src => src.PIC))
                ;
            this.CreateMap<CorporateDTO, Corporate>()
                .ForMember(d => d.CorpCd, m => m.MapFrom(src => src.CorporateCode))
                .ForMember(d => d.ComplexInd, m => m.MapFrom(src => NumberExtensions.BoolConverter(src.ComplexAcctInd)))
                .ForMember(d => d.CorpName, m => m.MapFrom(src => src.CorporateName))
                .ForMember(d => d.DispalyTradeCreditLimit, m => m.MapFrom(src => NumberExtensions.ConverterDecimal(src.TradeLimit.ToString("0.##"))))
                .ForMember(d => d.InvBillInd, m => m.MapFrom(src => NumberExtensions.BoolConverter(src.InvoiceCenter)))
                .ForMember(d => d.PymtInd, m => m.MapFrom(src => NumberExtensions.BoolConverter(src.PaymentCenter)))
                .ForMember(d => d.PIC, m => m.MapFrom(src => src.PersonInCharge))
                ;
        }
    }
}