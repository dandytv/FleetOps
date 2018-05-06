using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.MerchantMultiAdjustment;
using CardTrend.Domain.Dto.TxnAdjustment;
using CCMS.ModelSector;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class TxnAdjustmentMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<TxnAdjustmentDTO, TxnAdjustment>()
              .ForMember(d => d.TxnCd, m => m.Ignore())
              .ForMember(d => d.Sts, m => m.Ignore())
              .ForMember(d => d.Owner, m => m.Ignore())
              .ForMember(d => d.TxnCode, m => m.Ignore())
              .ForMember(d => d.PaymentType, m => m.Ignore())
              .ForMember(d => d.TxnId, m => m.MapFrom(src => src.TxnId))
              .ForMember(d => d.TxnDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.TxnDate)))
              .ForMember(d => d.DueDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.DueDate)))
              .ForMember(d => d.SelectedTxnCd, m => m.MapFrom(src => Convert.ToString(src.TxnCd)))
              .ForMember(d => d._CardnAccNo, m => m.ResolveUsing(model => new CardnAccNo { CardNo = model.CardNo }))
              .ForMember(d => d.TotAmnt, m => m.MapFrom(src => src.TxnAmount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TxnAmount)) : null))
              .ForMember(d => d.Totpts, m => m.MapFrom(src => src.Pts.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Pts)) : null))
              .ForMember(d => d.Descp, m => m.MapFrom(src => src.TxnDescription))
              .ForMember(d => d.RefType, m => m.MapFrom(src => src.TxnType))
              .ForMember(d => d.SelectedSts, m => m.MapFrom(src => src.Sts))
              .ForMember(d => d.WithHeldUnsettleId, m => m.MapFrom(src => NumberExtensions.ConvertInt(src.WUId)))
              .ForMember(d => d.CreationDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.CreationDate)))
              .ForMember(d => d.SelectedOwner, m => m.MapFrom(src => src.Owner))
              .ForMember(d => d.StsDescp, m => m.MapFrom(src => src.Status))
              .ForMember(d => d.Descp, m => m.MapFrom(src => src.AppvRemarks))
              .ForMember(d => d.BillingTxnAmt, m => m.MapFrom(src => src.BillingAmount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.BillingAmount)) : null))
              .ForMember(d => d.DisplayTotAmt, m => m.MapFrom(src => src.TxnAmount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TxnAmount)) : null))
              ;
            this.CreateMap<TxnAdjustmentDetailDTO, TxnAdjustment>()
            .ForMember(d => d.Owner, m => m.Ignore())
            .ForMember(d => d.Sts, m => m.Ignore())
            .ForMember(d => d.TxnCode, m => m.Ignore())
            .ForMember(d => d.IssueingBank, m => m.Ignore())
            .ForMember(d => d.TxnCd, m => m.Ignore())
            .ForMember(d => d.SelectedTxnCd, m => m.MapFrom(src => src.TxnCd.ToString()))
            .ForMember(d => d.SelectedTxnType, m => m.MapFrom(src => src.TxnType))
            .ForMember(d => d.TxnDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.TxnDate)))
            .ForMember(d => d.SelectedOwner, m => m.MapFrom(src => src.Owner))
            .ForMember(d => d.SelectedSts, m => m.MapFrom(src => src.ApprovalStatus))
            .ForMember(d => d.StsDescp, m => m.MapFrom(src => src.ApprovalDesc))
            .ForMember(d => d.MultipleTxnRecord, m => m.MapFrom(src => CustomMapperExtensions.MapIEnumerableToList(src.multipleTxnRecord)))
            ;
            this.CreateMap<MerchantMultiTxnAdjustmentDTO, TxnAdjustment>()
            .ForMember(d => d.Owner, m => m.Ignore())
            .ForMember(d => d.TxnCode, m => m.Ignore())
            .ForMember(d => d.CreationDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.CreationDate)))
            .ForMember(d => d.SelectedAdjTxnCode, m => m.MapFrom(src => src.TxnCode.ToString()))
            .ForMember(d => d.InvoiceNo, m => m.MapFrom(src => src.InvoiceNo.ToString()))
            .ForMember(d => d.TxnCount, m => m.MapFrom(src => src.TxnCnt.ToString()))
            .ForMember(d => d.BillingTxnAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TxnAmount))))
            .ForMember(d => d.SelectedOwner, m => m.MapFrom(src => src.Owner))
            .ForMember(d => d.SelectedSts, m => m.MapFrom(src => src.Status))
            ;
        }
    }
}