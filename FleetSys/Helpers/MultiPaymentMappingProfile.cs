using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.MerchantMultiAdjustment;
using CardTrend.Domain.Dto.MultiplePayment;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class MultiPaymentMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<MultiPaymentDTO, MultiPayment>()
               .ForMember(d => d.Sts, m => m.Ignore())
               .ForMember(d => d.Owner, m => m.Ignore())
               .ForMember(d => d.IssueingBank, m => m.Ignore())
               .ForMember(d => d.BatchId, m => m.MapFrom(src => src.BatchId.ToString()))
               .ForMember(d => d.RefNo, m => m.MapFrom(src => src.RefNo))
               .ForMember(d => d.ChequeNo, m => m.MapFrom(src => src.ChequeNo.ToString()))
               .ForMember(d => d.BillingAmt, m => m.MapFrom(src => decimal.Round(src.BatchTotalAmt, 2, MidpointRounding.AwayFromZero)))
               .ForMember(d => d.CreationDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.CreationDate)))
               .ForMember(d => d.SelectedOwner, m => m.MapFrom(src => src.Owner))
               .ForMember(d => d.SelectedTxnCode, m => m.MapFrom(src => src.TxnCdDescp))
               .ForMember(d => d.TxnCnt, m => m.MapFrom(src => src.TxnCnt.ToString()))
               .ForMember(d => d.SelectedSts, m => m.MapFrom(src => src.AppvSts))
               .ForMember(d => d.TxnId, m => m.MapFrom(src => src.TxnId))
               .ForMember(d => d.TxnDate, m => m.MapFrom(src => src.TxnDate.HasValue ? NumberExtensions.DateConverter(src.TxnDate) : ""))
               .ForMember(d => d.DueDate, m => m.MapFrom(src => src.DueDate.HasValue ? NumberExtensions.DateConverter(src.DueDate) : ""))
               .ForMember(d => d.ChequeAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.ChequeAmt))))
               .ForMember(d => d.SelectedIssueingBank, m => m.MapFrom(src => src.IssuingBank))
               .ForMember(d => d.SlipNo, m => m.MapFrom(src => src.SlipNo))
               .ForMember(d => d.SelectedPaymentType, m => m.MapFrom(src => src.PymtType))
               .ForMember(d => d.SelectedGLSettlement, m => m.MapFrom(src => src.SettleVal))
               .ForMember(d => d.MultipleTxnRecord, m => m.MapFrom(src => CustomMapperExtensions.MapIEnumerableToList(src.MultipleTxnRecordList)))
               ;
            this.CreateMap<MultiPayment, MultiPaymentDTO>()
               .ForMember(d => d.Sts, m => m.Ignore())
               .ForMember(d => d.Owner, m => m.Ignore())
               .ForMember(d => d.ChequeNo, m => m.Ignore())
               .ForMember(d => d.ChequeAmt, m => m.MapFrom(src => src.ChequeAmt))
               .ForMember(d => d.RefKey, m => m.MapFrom(src => Convert.ToString(src.ChequeNo)))
               ;
            this.CreateMap<MultiPaymentGLCodeDTO, MultiPayment>()
              .ForMember(d => d.GLTxnCode, m => m.MapFrom(src => src.GLAcctNo))
              .ForMember(d => d.GLDescp, m => m.MapFrom(src => src.Descp))
              .ForMember(d => d.GLCodeDescp, m => m.MapFrom(src => src.TxnType))
              ;
        }
    }
}