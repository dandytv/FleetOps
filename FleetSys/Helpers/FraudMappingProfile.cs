using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.Fraud;
using CCMS.ModelSector;
using ModelSector.Fraud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class FraudMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<FraudCaseDTO, FraudCaseListViewModel>()
            .ForMember(d => d.EventId, m => m.MapFrom(src => src.CaseNo))
            .ForMember(d => d.RefKey, m => m.MapFrom(src => src.AccountNo))
            .ForMember(d => d.e_Descp, m => m.MapFrom(src => src.Status))
            .ForMember(d => d.CloseDate, m => m.MapFrom(src => src.CloseDate))
            .ForMember(d => d.LastUpdDate, m => m.MapFrom(src => src.UpdateDate.HasValue ? NumberExtensions.DateConverter(src.UpdateDate) : ""))
            .ForMember(d => d.CreationDate, m => m.MapFrom(src => src.CreationDate))
            .ForMember(d => d.UserId, m => m.MapFrom(src => src.UpdatedBy))
            .ForMember(d => d.et_Descp, m => m.MapFrom(src => src.FraudType))
            .ForMember(d => d.CmpyName1, m => m.MapFrom(src => src.CompanyName))
            ;
            this.CreateMap<FraudCaseDTO, FraudCaseListViewModel>()
            .ForMember(d => d.EventId, m => m.MapFrom(src => src.CaseNo))
            .ForMember(d => d.RefKey, m => m.MapFrom(src => src.AccountNo))
            .ForMember(d => d.e_Descp, m => m.MapFrom(src => src.Status))
            .ForMember(d => d.CloseDate, m => m.MapFrom(src => src.CloseDate))
            .ForMember(d => d.LastUpdDate, m => m.MapFrom(src => src.UpdateDate.HasValue ? NumberExtensions.DateConverter(src.UpdateDate) : ""))
            .ForMember(d => d.CreationDate, m => m.MapFrom(src => src.CreationDate))
            .ForMember(d => d.UserId, m => m.MapFrom(src => src.UpdatedBy))
            .ForMember(d => d.et_Descp, m => m.MapFrom(src => src.FraudType))
            .ForMember(d => d.CmpyName1, m => m.MapFrom(src => src.CompanyName))
            ;
            this.CreateMap<WebFraudDetailDTO, FraudMainViewModel>()
               .ForMember(d => d.FraudCustomerDetailsViewModel, m => m.ResolveUsing(model => new FraudCustomerDetailsViewModel() { AcctNo = model.AcctNo, EventId = model.EventId }))
               .ForMember(d => d.FraudCardDetailsViewModel, m => m.ResolveUsing(model => new FraudCardDetailsViewModel() { AcctNo = model.AcctNo, EventId = model.EventId }))
               .ForMember(d => d.FraudIncidentsViewModel, m => m.ResolveUsing(model => new FraudIncidentsViewModel()
               {
                   SelectedReportedBy = model.ReportedBy,
                   SelectedReportedVia = model.ReportChannel,
                   SelectedReportedViaDescp = model.ChannelDescp,
                   IncidentDate = NumberExtensions.DateConverter(model.IncidentDate),
                   DisputedAmt = NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", model.DisputeAmt)),
                   SelectedNatureOfIncident = model.IncidentType,
                   OtherNatureOfIncident = model.IncidentTypeDescp,
                   Description = model.Descp,
                   SelectedInvestigatedBy = model.InvestigatedBy,
                   InvestigationDate = NumberExtensions.DateConverter(model.InvestigationDate),
                   InvestigationVenue = model.InvestigationVenue,
                   CaseBackground = model.CaseBackGround,
                   InvestigationProcess = model.InvestigationProcess,
                   Findings = model.Findings,
                   ActionTaken = model.ActionTaken,
                   MitigationPlan = model.Recommendation,
                   Conclusion = model.Conclusion,
                   CurrentStatus = model.StsDescp,
                   SelectedNextStatus = model.Sts,
                   Remarks = model.Remarks,
                   PreparedByName1 = model.PreparedByName1,
                   PreparedByPosition1 = model.PreparedByPosition1,
                   PreparedByName2 = model.PreparedByName2,
                   PreparedByPosition2 = model.PreparedByPosition2,
                   ReviewdByName1 = model.ReviewerName1,
                   ReviewdByPosition1 = model.ReviewerPosition1,
                   ReviewdByName2 = model.ReviewerName2,
                   ReviewdByPosition2 = model.ReviewerPosition2,
                   EndorsedByName1 = model.EndorsedByName1,
                   EndorsedByPosition1 = model.EndorsedByPosition1,
                   EndorsedByName2 = model.EndorsedByName2,
                   EndorsedByPosition2 = model.EndorsedByPosition2,
                   ApprovedByName1 = model.ApprovedByName1,
                   ApprovedByPosition1 = model.ApprovedByPosition1,
                   ApprovedByName2 = model.ApprovedByName2,
                   ApprovedByPosition2 = model.ApprovedByPosition2,
               }))
               ;
            this.CreateMap<FraudIncidentsViewModel, WebFraudDetailDTO>()
                .ForMember(d => d.ReportedBy, m => m.MapFrom(src => src.SelectedReportedBy))
                .ForMember(d => d.ReportVia, m => m.MapFrom(src => src.SelectedReportedVia))
                .ForMember(d => d.IncidentDate, m => m.MapFrom(src => NumberExtensions.DateConverterDB(src.IncidentDate)))
                .ForMember(d => d.DisputeAmt, m => m.MapFrom(src => Convert.ToDecimal(src.DisputedAmt)))
                .ForMember(d => d.NatureOfIncident, m => m.MapFrom(src => src.SelectedNatureOfIncident))
                .ForMember(d => d.OtherNatureOfIncident, m => m.MapFrom(src => src.OtherNatureOfIncident))
                .ForMember(d => d.Descp, m => m.MapFrom(src => src.Description))
                .ForMember(d => d.InvestigatedBy, m => m.MapFrom(src => src.SelectedInvestigatedBy))
                .ForMember(d => d.InvestigationDate, m => m.MapFrom(src => NumberExtensions.DateConverterDB(src.InvestigationDate)))
                .ForMember(d => d.InvestigationVenue, m => m.MapFrom(src => src.InvestigationVenue))
                .ForMember(d => d.Recommendation, m => m.MapFrom(src => src.MitigationPlan))
                .ForMember(d => d.Sts, m => m.MapFrom(src => src.SelectedNextStatus))
                .ForMember(d => d.ReviewerName1, m => m.MapFrom(src => src.ReviewdByName1))
                .ForMember(d => d.ReviewerPosition1, m => m.MapFrom(src => src.ReviewdByPosition1))
                .ForMember(d => d.ReviewerName2, m => m.MapFrom(src => src.ReviewdByName2))
                .ForMember(d => d.ReviewerPosition2, m => m.MapFrom(src => src.ReviewdByPosition2))
                ;
            this.CreateMap<FraudCardDetailDTO, FraudCardDetailsViewModel>()
                .ForMember(d => d.FraudCards, m => m.MapFrom(src => CustomMapperExtensions.MapIEnumerableToList(src.FraudCards)))
                .ForMember(d => d.CardMonth1Date, m => m.MapFrom(src => string.IsNullOrEmpty(src.Month1Date) ? "N/A" : src.Month1Date))
                .ForMember(d => d.CardMonth2Date, m => m.MapFrom(src => string.IsNullOrEmpty(src.Month2Date) ? "N/A" : src.Month2Date))
                .ForMember(d => d.CardMonth3Date, m => m.MapFrom(src => string.IsNullOrEmpty(src.Month3Date) ? "N/A" : src.Month3Date))
                .ForMember(d => d.CardMonth4Date, m => m.MapFrom(src => string.IsNullOrEmpty(src.Month4Date) ? "N/A" : src.Month4Date))
                .ForMember(d => d.CardMonth5Date, m => m.MapFrom(src => string.IsNullOrEmpty(src.Month5Date) ? "N/A" : src.Month5Date))
                .ForMember(d => d.CardMonth6Date, m => m.MapFrom(src => string.IsNullOrEmpty(src.Month6Date) ? "N/A" : src.Month6Date))
                .ForMember(d => d.CardMonth1Amt, m => m.MapFrom(src => src.Month1Amount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Month1Amount.Value)) : "0.00"))
                .ForMember(d => d.CardMonth2Amt, m => m.MapFrom(src => src.Month2Amount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Month2Amount.Value)) : "0.00"))
                .ForMember(d => d.CardMonth3Amt, m => m.MapFrom(src => src.Month3Amount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Month3Amount.Value)) : "0.00"))
                .ForMember(d => d.CardMonth4Amt, m => m.MapFrom(src => src.Month4Amount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Month4Amount.Value)) : "0.00"))
                .ForMember(d => d.CardMonth5Amt, m => m.MapFrom(src => src.Month5Amount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Month5Amount.Value)) : "0.00"))
                .ForMember(d => d.CardMonth6Amt, m => m.MapFrom(src => src.Month6Amount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Month6Amount.Value)) : "0.00"))
                .ForMember(d => d.CardAvgSales, m => m.MapFrom(src => src.AvgSales.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.AvgSales.Value)) : ""))
                .ForMember(d => d.CardAvgSalesDisplay, m => m.MapFrom(src => src.CardAvgSalesDisplay))
                .ForMember(d => d.LitLimit, m => m.MapFrom(src => src.LitLimit.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.LitLimit)) : ""))
                .ForMember(d => d.SingleTxn, m => m.MapFrom(src => src.SingleTxn.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.SingleTxn)) : ""))
                .ForMember(d => d.DailyTxn, m => m.MapFrom(src => src.DailyTxn.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.DailyTxn)) : ""))
                .ForMember(d => d.DailyLitre, m => m.MapFrom(src => src.DailyLitre.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.DailyLitre)) : ""))
                .ForMember(d => d.MonthlyLitre, m => m.MapFrom(src => src.MonthlyLitre.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.MonthlyLitre)) : ""))
                .ForMember(d => d.MonthlyTxn, m => m.MapFrom(src => src.MonthlyTxn.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.MonthlyTxn)) : ""))
                ;
            this.CreateMap<FraudCardDTO, FraudCards>();
            this.CreateMap<FraudCards, FraudCardDTO>();
            this.CreateMap<FraudTxnDisputeDTO, FraudTxnDisputeViewModel>()
                .ForMember(d => d.TxnDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.TxnDate)))
                .ForMember(d => d.BSNLocationName, m => m.MapFrom(src => src.BusnName))
                .ForMember(d => d.BSNLocation, m => m.MapFrom(src => src.BusnLocation))
                .ForMember(d => d.TxnDesp, m => m.MapFrom(src => src.Descp))
                .ForMember(d => d.TxnAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.BillingTxnAmt))))
                .ForMember(d => d.AuthCardNo, m => m.MapFrom(src => Convert.ToString(src.AuthCardNo)))
                .ForMember(d => d.TxnId, m => m.MapFrom(src => Convert.ToString(src.TxnId)))
                .ForMember(d => d.VehRegNo, m => m.MapFrom(src => src.VehRegsNo))
                .ForMember(d => d.RRn, m => m.MapFrom(src => Convert.ToString(src.Rrn)))
                .ForMember(d => d.Stan, m => m.MapFrom(src => Convert.ToString(src.Stan)))
                ;
            this.CreateMap<FraudCustomerDetailsDTO, FraudCustomerDetailsViewModel>()
                .ForMember(d => d.AcctNo, m => m.MapFrom(src => Convert.ToString(src.AccountNo)))
                .ForMember(d => d.CmpyName1, m => m.MapFrom(src => src.CompanyName))
                .ForMember(d => d.EventId, m => m.MapFrom(src => Convert.ToString(src.EventID)))
                .ForMember(d => d.AgeingDays, m => m.MapFrom(src => Convert.ToString(src.AgingDays)))
                .ForMember(d => d.Month1Amt, m => m.MapFrom(src => src.Month1Amount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Month1Amount.Value)) : ""))
                .ForMember(d => d.Month2Amt, m => m.MapFrom(src => src.Month2Amount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Month2Amount.Value)) : ""))
                .ForMember(d => d.Month3Amt, m => m.MapFrom(src => src.Month3Amount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Month3Amount.Value)) : ""))
                .ForMember(d => d.Month4Amt, m => m.MapFrom(src => src.Month4Amount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Month4Amount.Value)) : ""))
                .ForMember(d => d.Month5Amt, m => m.MapFrom(src => src.Month5Amount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Month5Amount.Value)) : ""))
                .ForMember(d => d.Month6Amt, m => m.MapFrom(src => src.Month6Amount.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Month6Amount.Value)) : ""))
                .ForMember(d => d.AvgSales, m => m.MapFrom(src => src.AvgSales.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.AvgSales.Value)) : ""))
                .ForMember(d => d.AvgSalesDisplay, m => m.MapFrom(src => src.AvgSalesDisplay))
          ;
        }
    }
}