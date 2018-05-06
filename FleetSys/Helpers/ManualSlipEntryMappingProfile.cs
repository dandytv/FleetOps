using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.ManualSlipEntry;
using CCMS.ModelSector;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class ManualSlipEntryMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<ManualSlipEntryDTO, ManualSlipEntry>()
               .ForMember(d => d.BusnLocation, m => m.MapFrom(src => src.Dealer))
               .ForMember(d => d.SelectedTermId, m => m.MapFrom(src => src.TerminalId))
               .ForMember(d => d.SiteId, m => m.MapFrom(src => src.SiteId))
               .ForMember(d => d.InvoiceNo, m => m.MapFrom(src => src.InvoiceNo))
               .ForMember(d => d.SettleDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.SettleDate)))
               .ForMember(d => d.TotalCnt, m => m.MapFrom(src => src.TotalCount))
               .ForMember(d => d.DisplayTotalAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TotalAmount))))
               .ForMember(d => d.Descp, m => m.MapFrom(src => src.Description))
               .ForMember(d => d.SelectedSts, m => m.MapFrom(src => src.Status))
               .ForMember(d => d.TxnDescp, m => m.MapFrom(src => src.TxnDescription))
               .ForMember(d => d._CreationDatenUserId, m => m.ResolveUsing(model => new CreationDatenUserId() { UserId = model.UserId, CreationDate = CardTrend.Common.Extensions.NumberExtensions.DateConverter(model.CreationDate) }))
               ;
            this.CreateMap<MerchManualTxnDTO, ManualSlipEntry>()
                .ForMember(d => d.TxnCd, m => m.Ignore())
                .ForMember(d => d.VATCd, m => m.Ignore())
                .ForMember(d => d.TermId, m => m.Ignore())
                .ForMember(d => d.Sts, m => m.Ignore())
                .ForMember(d => d.ProdCd, m => m.Ignore())
                .ForMember(d => d.SelectedProdCd, m => m.MapFrom(src => src.ProdCd))
                .ForMember(d => d.Quantity, m => m.MapFrom(src => src.Quantity.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.000}", src.Quantity.Value)) : "0.000"))
                .ForMember(d => d.ProdAmt, m => m.MapFrom(src => src.ProdAmt.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.ProdAmt.Value)) : "0.00"))
                .ForMember(d => d.UnitPrice, m => m.MapFrom(src => src.UnitPrice.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.UnitPrice.Value)) : "0.00"))
                .ForMember(d => d.VATRate, m => m.MapFrom(src => src.VATRate.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.VATRate.Value)) : "0.00"))
                .ForMember(d => d.SelectedVATCd, m => m.MapFrom(src => src.VATCd))
                .ForMember(d => d.SelectedSts, m => m.MapFrom(src => src.Sts))
                .ForMember(d => d.SelectedTermId, m => m.MapFrom(src => src.TerminalId))
                .ForMember(d => d.SelectedTxnCd, m => m.MapFrom(src => src.TxnCd))
                .ForMember(d => d.RcptNo, m => m.MapFrom(src => src.ReceiptNo))
                .ForMember(d => d.BusnLocation, m => m.MapFrom(src => src.Dealer))
                .ForMember(d => d.AppvCd, m => m.MapFrom(src => src.AuthNo))
                .ForMember(d => d.TxnDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.TransactionDate)))
                .ForMember(d => d.AuthCardNo, m => m.MapFrom(src => src.DriverCard))
                .ForMember(d => d.DisplayTxnAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TxnAmount))))
                .ForMember(d => d.VATAmt, m => m.MapFrom(src => src.VATAmt.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.VATAmt.Value)) : "0.00"))
                .ForMember(d => d.Descp, m => m.MapFrom(src => src.Description))
                .ForMember(d => d.ProdDesc, m => m.MapFrom(src => src.Description))
                .ForMember(d => d.TotalAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TxnAmount))))
                .ForMember(d => d.DisplayTotalAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TxnAmount))))
                .ForMember(d => d.AuthNo, m => m.MapFrom(src => src.AuthNo))
                .ForMember(d => d.Odometer, m => m.MapFrom(src => src.OdometerReading))
                .ForMember(d => d.ArrayCnt, m => m.MapFrom(src => src.ArrayCount))
                .ForMember(d => d.Stans, m => m.MapFrom(src => src.Stan))
                .ForMember(d => d.Rrn, m => m.MapFrom(src => Convert.ToString(src.Rrn)))
                .ForMember(d => d._CreationDatenUserId, m => m.ResolveUsing(model => new CreationDatenUserId() { UserId = model.UserId, CreationDate = NumberExtensions.DateConverter(model.CreationDate) }))
                ;

            this.CreateMap<ManualSlipEntry, MerchManualTxnDTO>()
               .ForMember(d => d.TxnCd, m => m.MapFrom(src => src.SelectedTxnCd))
               .ForMember(d => d.Dealer, m => m.MapFrom(src => src.BusnLocation))
               .ForMember(d => d.TerminalId, m => m.MapFrom(src => src.SelectedTermId))
               .ForMember(d => d.ReceiptNo, m => m.MapFrom(src => src.RcptNo))
               .ForMember(d => d.VATCd, m => m.MapFrom(src => src.SelectedVATCd))
               .ForMember(d => d.ProdAmt, m => m.MapFrom(src => NumberExtensions.ConvertDecimalToDb(src.ProdAmt)))
               .ForMember(d => d.ProdCd, m => m.MapFrom(src => src.SelectedProdCd))
               .ForMember(d => d.InvoiceNo, m => m.MapFrom(src => Convert.ToInt32(src.InvoiceNo)))
               .ForMember(d => d.Stan, m => m.MapFrom(src => Convert.ToInt32(src.Stans)))
               .ForMember(d => d.Rrn, m => m.MapFrom(src => Convert.ToInt64(src.Rrn)))
               .ForMember(d => d.CardExpiry, m => m.MapFrom(src => src.CardExpire))
               .ForMember(d => d.DriverCard, m => m.MapFrom(src => src.AuthCardNo))
               .ForMember(d => d.VATAmt, m => m.MapFrom(src => NumberExtensions.ConvertDecimalToDb(src.VATAmt)))
               .ForMember(d => d.TxnDate, m => m.MapFrom(src => NumberExtensions.ConvertDatetimeDB(src.TxnDate)))
               .ForMember(d => d.ArrayCount, m => m.MapFrom(src => Convert.ToInt32(src.ArrayCnt)))
               .ForMember(d => d.Quantity, m => m.MapFrom(src => NumberExtensions.ConvertDecimalToDb(src.Quantity)))
               .ForMember(d => d.TotalAmt, m => m.MapFrom(src => src.DisplayTotalAmt))
               .ForMember(d => d.Description, m => m.MapFrom(src => src.Descp))
               .ForMember(d => d.OdometerReading, m => m.MapFrom(src => src.Odometer))
               .ForMember(d => d.Sts, m => m.MapFrom(src => src.SelectedSts))
               ;

            this.CreateMap<ManualSlipEntryBatchDetailDTO, ManualSlipEntry>()
                .ForMember(d => d.BusnLocation, m => m.MapFrom(src => src.Dealer))
                .ForMember(d => d.TermId, m => m.Ignore())
                .ForMember(d => d.Sts, m => m.Ignore())
                .ForMember(d => d.SelectedTermId, m => m.MapFrom(src => src.TermId))
                .ForMember(d => d.BatchId, m => m.MapFrom(src => src.BatchId))
                .ForMember(d => d.TxnCd, m => m.Ignore())
                .ForMember(d => d.SelectedTxnCd, m => m.MapFrom(src => src.TxnCd))
                .ForMember(d => d.InvoiceNo, m => m.MapFrom(src => src.InvoiceNo))
                .ForMember(d => d.SettleDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.SettleDate)))
                .ForMember(d => d.TotalCnt, m => m.MapFrom(src => src.Cnt))
                .ForMember(d => d.DisplayTotalAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Amt))))
                .ForMember(d => d.OrigBatchNo, m => m.MapFrom(src => Convert.ToInt32(src.OrigBatchNo)))
                .ForMember(d => d.SelectedSts, m => m.MapFrom(src => src.Sts))
                .ForMember(d => d.Sts, m => m.MapFrom(s => CustomMapperExtensions.MapIEnumerableToList(s.StsList)))
                ;

            this.CreateMap<ManualSlipEntry, ManualSlipEntryBatchDetailDTO>()
               .ForMember(d => d.Dealer, m => m.MapFrom(src => src.BusnLocation))
               .ForMember(d => d.TermId, m => m.Ignore())
               .ForMember(d => d.Sts, m => m.Ignore())
               .ForMember(d => d.TxnCd, m => m.Ignore())
               .ForMember(d => d.TermId, m => m.MapFrom(src => src.SelectedTermId))
               .ForMember(d => d.TxnCd, m => m.MapFrom(src => src.SelectedTxnCd))
               .ForMember(d => d.InvoiceNo, m => m.MapFrom(src => Convert.ToString(src.InvoiceNo)))
               .ForMember(d => d.BatchId, m => m.MapFrom(src => src.BatchId.ToString()))
               .ForMember(d => d.OrigBatchNo, m => m.MapFrom(src => src.OrigBatchNo))
               .ForMember(d => d.SettleDate, m => m.MapFrom(src => NumberExtensions.DateConverterDB(src.SettleDate)))
               .ForMember(d => d.Sts, m => m.MapFrom(src => src.SelectedSts))
               ;
            this.CreateMap<ManualTxnDTO, ManualSlipEntry>()
            .ForMember(d => d.TxnCd, m => m.Ignore())
            .ForMember(d => d.BusnLocation, m => m.MapFrom(src => src.Dealer))
            .ForMember(d => d.SelectedTermId, m => m.MapFrom(src => src.Termid))
            .ForMember(d => d.SelectedTxnCd, m => m.MapFrom(src => src.TxnCd))
            .ForMember(d => d.InvoiceNo, m => m.MapFrom(src => src.InvoiceNo))
            ;
            this.CreateMap<ManualTxnProductDTO, ManualTxnProduct>()
            .ForMember(d => d.BatchId, m => m.MapFrom(src => Convert.ToString(src.BatchId)))
            .ForMember(d => d.SelectedProdCd, m => m.MapFrom(src => src.Prod))
            .ForMember(d => d.Quantity, m => m.MapFrom(src => src.Quantity.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.000}", src.Quantity.Value)) : "0.000"))
            .ForMember(d => d.ProdAmt, m => m.MapFrom(src => src.ProdAmount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.ProdAmount.Value)) : "0.00"))
            .ForMember(d => d.ProdDesc, m => m.MapFrom(src => src.ProdDescription))
            .ForMember(d => d.LastUpdDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.LastUpdateDate)))
            .ForMember(d => d.VATAmt, m => m.MapFrom(src => src.VATAmt.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.VATAmt.Value)) : "0.00"))
            .ForMember(d => d.VATRate, m => m.MapFrom(src => src.VATRate.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.VATRate.Value)) : "0.00"))
            .ForMember(d => d.SelectedVATCd, m => m.MapFrom(src => src.VATCd))
            .ForMember(d => d._CreationDatenUserId, m => m.ResolveUsing(model => new CreationDatenUserId() { UserId = model.UserId, CreationDate = CardTrend.Common.Extensions.NumberExtensions.DateConverter(model.CreationDate) }))
            ;
        }
    }
}