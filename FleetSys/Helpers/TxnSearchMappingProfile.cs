using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.TransactionSearch;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class TxnSearchMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<TransactionSearchDTO, AcctPostedTxnSearch>()
              .ForMember(d => d.InvoicDt, m => m.MapFrom(src => src.StatementDate))
              .ForMember(d => d.SelectedCardNo, m => m.MapFrom(src => src.CardNo))
              .ForMember(d => d.TxnDesp, m => m.MapFrom(src => src.TxnDescp))
              .ForMember(d => d.TxnAmt, m => m.MapFrom(src => src.TxnAmt.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TxnAmt)) : ""))
              .ForMember(d => d.RecieptId, m => m.MapFrom(src => src.ReceiptNo))
              .ForMember(d => d.Batch, m => m.MapFrom(src => src.BatchNo.HasValue ? src.BatchNo.Value.ToString() : ""))
              .ForMember(d => d.VehRegNo, m => m.MapFrom(src => src.VehRegsNo))
              .ForMember(d => d.Quantity, m => m.MapFrom(src => src.Qty.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Qty)) : ""))
              .ForMember(d => d.ProductAmt, m => m.MapFrom(src => src.ProductAmt.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.ProductAmt)) : ""))
              .ForMember(d => d.VATAmt, m => m.MapFrom(src => src.VATAmt.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.VATAmt)) : ""))
              .ForMember(d => d.BaseAmt, m => m.MapFrom(src => src.BaseAmt.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.BaseAmt)) : ""))
              .ForMember(d => d.VATRate, m => m.MapFrom(src => src.VATRate.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.VATRate)) : ""))
              .ForMember(d => d.RRn, m => m.MapFrom(src => Convert.ToString(src.Rrn)))
              .ForMember(d => d.Stan, m => m.MapFrom(src => Convert.ToString(src.Stan)))
              .ForMember(d => d.ApproveCd, m => m.MapFrom(src => src.AppvCd))
              ;
            this.CreateMap<TransactionSearchDTO, MerchPostedTxnSearch>()
             .ForMember(d => d.SelectedDealer, m => m.MapFrom(src => src.Dealer))
             .ForMember(d => d.cardNo, m => m.MapFrom(src => src.CardNo))
             .ForMember(d => d.TxnDesp, m => m.MapFrom(src => src.TxnDescp))
             .ForMember(d => d.TxnAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.BillingAmt))))
             .ForMember(d => d.ProductQty, m => m.MapFrom(src => src.ProductQty.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.ProductQty)) : ""))
             .ForMember(d => d.ProductAmt, m => m.MapFrom(src => src.ProductAmt.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.ProductAmt)) : ""))
             .ForMember(d => d.VATAmt, m => m.MapFrom(src => src.VATAmt.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.VATAmt)) : ""))
             .ForMember(d => d.BaseAmt, m => m.MapFrom(src => src.BaseAmt.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.BaseAmt)) : ""))
             .ForMember(d => d.VATRate, m => m.MapFrom(src => src.VATRate.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.VATRate)) : ""))
             .ForMember(d => d.ProductDescp, m => m.MapFrom(src => src.ProductDescp))
            ;
            this.CreateMap<ObjectDetailDTO, ObjectDetail>()
                .ForMember(d => d.AcctNo, m => m.MapFrom(src => src.AcctNo.HasValue ? src.AcctNo.Value.ToString() : null))
                .ForMember(d => d.CardNo, m => m.MapFrom(src => src.CardNo.HasValue ? src.CardNo.Value.ToString() : null))
                ;
        }
    }
}