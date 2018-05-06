using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.MultipleAdjustment;
using CardTrend.Domain.Dto.MultiplePayment;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class MultipleAdjMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<TxnAdjustment, TxnAdjustmentDTO>()
                .ForMember(d => d.Owner, m => m.Ignore())
                .ForMember(d => d.Sts, m => m.Ignore())
                .ForMember(d => d.IssueingBank, m => m.Ignore())
                .ForMember(d => d.TxnCd, m => m.Ignore())
                .ForMember(d => d.ChequeAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.ChequeAmt))))
                .ForMember(d => d.RefId, m => m.MapFrom(src => Convert.ToInt64(src.RefId)))
                .ForMember(d => d.ChequeNo, m => m.MapFrom(src => Convert.ToInt64(src.ChequeNo)))
                .ForMember(d => d.TxnCd, m => m.MapFrom(src => Convert.ToInt64(src.SelectedTxnCd)))
                .ForMember(d => d.AdjTxnCd, m => m.MapFrom(src => Convert.ToInt32(src.SelectedAdjTxnCode)))
                .ForMember(d => d.TxnDate, m => m.MapFrom(src => NumberExtensions.DateConverterDB(src.TxnDate)))
                .ForMember(d => d.DueDate, m => m.MapFrom(src => NumberExtensions.DateConverterDB(src.DueDate)))
                .ForMember(d => d.UserId, m => m.MapFrom(src => src.UserId))
                .ForMember(d => d.IssueingBank, m => m.MapFrom(src => src.SelectedIssueingBank))
                .ForMember(d => d.Owner, m => m.MapFrom(src => src.SelectedOwner))
                .ForMember(d => d.SelectedGLSettlement, m => m.MapFrom(src => src.SelectedGLSettlement))
                .ForMember(d => d.CreationDate, m => m.MapFrom(src => NumberExtensions.DateConverterDB(src.CreationDate)))
                .ForMember(d => d.BatchId, m => m.MapFrom(src => Convert.ToInt32(src.BatchId)))
                .ForMember(d => d.multipleTxnRecord, m => m.MapFrom(src => CustomMapperExtensions.MapIEnumerableToList(src.MultipleTxnRecord)))
                ;

            this.CreateMap<TxnAdjustmentDTO, TxnAdjustment>()
                .ForMember(d => d.Owner, m => m.Ignore())
                .ForMember(d => d.Sts, m => m.Ignore())
                .ForMember(d => d.TxnCode, m => m.Ignore())
                .ForMember(d => d.IssueingBank, m => m.Ignore())
                .ForMember(d => d.TxnCd, m => m.Ignore())
                .ForMember(d => d.TxnDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.TxnDate)))
                .ForMember(d => d.ChequeAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.ChequeAmt))))
                .ForMember(d => d.BillingTxnAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.BillingAmount))))
                .ForMember(d => d.WithHeldUnsettleId, m => m.MapFrom(src => NumberExtensions.ConvertInt(src.WUId)))
                .ForMember(d => d.CreationDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.CreationDate)))
                .ForMember(d => d.BatchId, m => m.MapFrom(src => Convert.ToString(src.BatchId)))
                .ForMember(d => d.SelectedOwner, m => m.MapFrom(src => src.Owner))
                .ForMember(d => d.SelectedSts, m => m.MapFrom(src => src.Sts))
                .ForMember(d => d.SelectedAdjTxnCode, m => m.MapFrom(src => src.TxnCd.ToString()))
                .ForMember(d => d.SelectedTxnType, m => m.MapFrom(src => src.TxnType))
                .ForMember(d => d.SelectedPaymentType, m => m.MapFrom(src => src.PymtType))
                .ForMember(d => d.MultipleTxnRecord, m => m.MapFrom(src => CustomMapperExtensions.MapIEnumerableToList(src.multipleTxnRecord)))
                ;
           this.CreateMap<TxnMultipleAdjustmentDTO, TxnAdjustment>()
                 .ForMember(d => d.BatchId, m => m.MapFrom(src => Convert.ToString(src.BatchId)))
                 .ForMember(d => d.TxnNo, m => m.MapFrom(src => Convert.ToString(src.TxnNo)))
                 .ForMember(d => d.ChequeAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.ChequeAmt))))
                 .ForMember(d => d.DisplayTotAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TotalTxnAmt))))
                 .ForMember(d => d.CreationDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.CreationDate)))
                 .ForMember(d => d.CreatedBy, m => m.MapFrom(src => src.UserId))
                 .ForMember(d => d.SelectedTxnCode, m => m.MapFrom(src => src.TxnCd))
                 .ForMember(d => d.SelectedOwner, m => m.MapFrom(src => src.Owner))
                 .ForMember(d => d.SelectedSts, m => m.MapFrom(src => src.Sts))
                 .ForMember(d => d.ChequeNo, m => m.MapFrom(src => src.Rrn))
                 ;
           this.CreateMap<GLCodeDTO, MultiPayment>()
                .ForMember(d => d.GLTxnCode, m => m.MapFrom(src => src.GLAcctNo))
                .ForMember(d => d.GLDescp, m => m.MapFrom(src => src.TxnType))
                .ForMember(d => d.GLCodeDescp, m => m.MapFrom(src => src.Descp))
                ;
        }
    }
}