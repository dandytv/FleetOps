using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.SOASummary;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class SOASummaryMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<AcctSOASummaryDetailDTO, AcctSOA>()
                .ForMember(d => d.CompanyID, m => m.MapFrom(src => Convert.ToString(src.CompanyId)))
                .ForMember(d => d.SelectedStmtDate, m => m.MapFrom(src => src.CycleDate))
                .ForMember(d => d.LastAgeCd, m => m.MapFrom(src => src.LastAgeCode))
                .ForMember(d => d.CreditLimit, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.CreditLimit))))
                .ForMember(d => d.OpeningBal, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.OpeningBalance))))
                .ForMember(d => d.MTDDebits, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.MTDCredits))))
                .ForMember(d => d.AvaiCredLimits, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.AvailableCreditLimit))))
                .ForMember(d => d.CurrBalance, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.CurrentBalance))))
                .ForMember(d => d.MTDCreds, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.MTDCredits))))
                .ForMember(d => d.TotMinimumPymt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TotalMinimunPayment))))
                .ForMember(d => d.CrrtDueMinimumOymt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.CurrentDueMinimunPayment))))
                .ForMember(d => d.PastDueMinimumPymt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.PastDueMinimunPayment))))
                .ForMember(d => d.PymtDueDate, m => m.MapFrom(src => src.PaymentDueDate))
                .ForMember(d => d.LastPymtDate, m => m.MapFrom(src => src.LastPaymentDate))
                .ForMember(d => d.LastPymtAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.LastPaymentAmount))))
                ;
            this.CreateMap<AcctSOASummaryDTO, AcctSOA>()
                .ForMember(d => d.Month, m => m.MapFrom(src => src.Mth))
                .ForMember(d => d.SelectedStmtDate, m => m.MapFrom(src => src.StatementDate))
                .ForMember(d => d.ClseBalance, m => m.MapFrom(src => NumberExtensions.ConverterDecimal(src.ClosingBalance.ToString())))
                .ForMember(d => d.MinimumPayment, m => m.MapFrom(src => NumberExtensions.ConverterDecimal(src.MinimunPayment.ToString())))
                .ForMember(d => d.Debits, m => m.MapFrom(src => NumberExtensions.ConverterDecimal(src.Debits.ToString())))
                .ForMember(d => d.Credits, m => m.MapFrom(src => decimal.Round(src.Credits, 2, MidpointRounding.AwayFromZero)))
                .ForMember(d => d.Sales, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Sales))))
                .ForMember(d => d.DBAdjust, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.DBadjustment))))
                .ForMember(d => d.Charges, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Charges))))
                .ForMember(d => d.Payment, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Payment))))
                .ForMember(d => d.CRAdjust, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.CRAdjustment))))
                .ForMember(d => d.age, m => m.MapFrom(src => src.Age))
                .ForMember(d => d.Rchq, m => m.MapFrom(src => src.RChq))
                ;

            this.CreateMap<AcctSOATxnCategoryDTO, AcctSOA>()
              .ForMember(d => d.TxnCode, m => m.MapFrom(src => Convert.ToString(src.TransactionCode)))
              .ForMember(d => d.TxnEventCd, m => m.MapFrom(src => src.TransactionEventCode))
              .ForMember(d => d.TxnDesc, m => m.MapFrom(src => src.TransactionDesc))
              .ForMember(d => d.TotalCount, m => m.MapFrom(src => Convert.ToString(src.TotalCount)))
              .ForMember(d => d.TotalAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TotalAmount))))
              .ForMember(d => d.TotalItemQty, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.000}", src.TotalItemQuantity))))
              .ForMember(d => d.TotalItemAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TotalItemAmount))))
              .ForMember(d => d.SelectedStmtDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.StatementDate)))
              .ForMember(d => d.AcctNo, m => m.MapFrom(src => Convert.ToString(src.AccountNo)))
              ;
            this.CreateMap<AcctSOATxnDTO, AcctSOA>()
             .ForMember(d => d.TxnDate, m => m.MapFrom(src => src.TransactionDate))
             .ForMember(d => d.txnTime, m => m.MapFrom(src => src.TransactionTime))
             .ForMember(d => d.TxnCode, m => m.MapFrom(src => Convert.ToString(src.TxnCode)))
             .ForMember(d => d.TxnAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TransactionAmount))))
             .ForMember(d => d.Amt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Amount))))
             .ForMember(d => d.DBAName, m => m.MapFrom(src => src.TradingNameDescription))
             .ForMember(d => d.MCC, m => m.MapFrom(src => src.Mcc))
             .ForMember(d => d.RRn, m => m.MapFrom(src => src.RRN))
             ;

        }
    }
}