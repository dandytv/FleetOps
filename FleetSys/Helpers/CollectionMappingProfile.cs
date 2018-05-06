using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.Collection;
using ModelSector;
using ModelSector.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class CollectionMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<CollectionTaskListViewModel, CollectionTaskDTO>()
                .ForMember(d => d.CorpCd, m => m.MapFrom(src => src.SelectedCorpCode))
                .ForMember(d => d.SaleTerritory, m => m.MapFrom(src => src.SelectedSalesTerritory))
                .ForMember(d => d.Collectionsts, m => m.MapFrom(src => src.SelectedCollectionSts))
                .ForMember(d => d.Owner, m => m.MapFrom(src => src.SelectedOwner))
                .ForMember(d => d.RecallDate, m => m.MapFrom(src => src.RecallFromDate))
                .ForMember(d => d.ToRecallDate, m => m.MapFrom(src => NumberExtensions.DateConverterDB(src.RecallToDate)))
                .ForMember(d => d.CreationDate, m => m.MapFrom(src => src.CreationFromDate))
                .ForMember(d => d.ToRecallDate, m => m.MapFrom(src => NumberExtensions.DateConverterDB(src.CreationToDate)))
                .ForMember(d => d.accountNoSelected, m => m.MapFrom(src => src.AcctNo))
                .ForMember(d => d.CmpyName1, m => m.MapFrom(src => src.CmpyName1))
                .ForMember(d => d.CorpName, m => m.MapFrom(src => src.CorpName))
                .ForMember(d => d.CollectionAmt, m => m.MapFrom(src => src.AccumAgeingAmt))
                .ForMember(d => d.GraceDueDate, m => m.MapFrom(src => src.GraceDueDate))
                .ForMember(d => d.CycAge, m => m.MapFrom(src => Convert.ToInt32(src.CycAge)))
                .ForMember(d => d.Priority, m => m.MapFrom(src => src.Priority))
                .ForMember(d => d.AccountSts, m => m.MapFrom(src => src.AccountSts))
                .ForMember(d => d.Collectionsts, m => m.MapFrom(src => src.SelectedCollectionSts))
                ;
            this.CreateMap<CollectionTaskDTO, CollectionTaskListViewModel>()
                .ForMember(d => d.Owner, m => m.Ignore())
                .ForMember(d => d.Collectionsts, m => m.Ignore())
                .ForMember(d => d.SelectedCorpCode, m => m.MapFrom(src => src.CorpCd))
                .ForMember(d => d.EventId, m => m.MapFrom(src => Convert.ToString(src.EventId)))
                .ForMember(d => d.AcctNo, m => m.MapFrom(src => Convert.ToString(src.AcctNo)))
                .ForMember(d => d.SelectedSalesTerritory, m => m.MapFrom(src => src.SaleTerritory))
                .ForMember(d => d.AccumAgeingAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.CollectionAmt))))
                .ForMember(d => d.GraceDueDate, m => m.MapFrom(src => src.GraceDueDate))
                .ForMember(d => d.CycAge, m => m.MapFrom(src => Convert.ToString(src.CycAge)))
                .ForMember(d => d.SelectedCollectionSts, m => m.MapFrom(src => src.Collectionsts))
                .ForMember(d => d.SelectedOwner, m => m.MapFrom(src => src.Owner))
                .ForMember(d => d.CorpAcct, m => m.MapFrom(src => src.CorpName))
                ;
            this.CreateMap<CollPaymentHistDTO, CollPaymentHistViewModel>()
                .ForMember(d => d.StatementDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.StatementDate)))
                .ForMember(d => d.DueDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.DueDate)))
                .ForMember(d => d.TxnDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.TransactionDate)))
                .ForMember(d => d.PostingDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.PostingDate)))
                .ForMember(d => d.TxnDesc, m => m.MapFrom(src => src.TransactionDescription))
                .ForMember(d => d.TxnAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}",src.TransactionAmount))))
                .ForMember(d => d.ApprovalCode, m => m.MapFrom(src => src.ApprovalCode))
                ;
            this.CreateMap<DelinquentAcctsTresholdLimitDTO, CollectionTaskListViewModel>()
               .ForMember(d => d.Collectionsts, m => m.Ignore())
               .ForMember(d => d.Owner, m => m.Ignore())
               .ForMember(d => d.AcctNo, m => m.MapFrom(src => Convert.ToString(src.AcctNo)))
               .ForMember(d => d.CmpyName1, m => m.MapFrom(src => src.CompanyName))
               .ForMember(d => d.CorpAcct, m => m.MapFrom(src => src.CorpAccount))
               .ForMember(d => d.CorpName, m => m.MapFrom(src => src.CorporateName))
               .ForMember(d => d.SelectedSalesTerritory, m => m.MapFrom(src => src.SaleTerritory))
               .ForMember(d => d.PermCreditLimit, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.CreditLimit))))
               .ForMember(d => d.TempCreditLimit, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TempCreditLimit))))
               .ForMember(d => d.PercentageUsage, m => m.MapFrom(src => src.Usage == null ? string.Empty : NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.Usage))))
               .ForMember(d => d.AvailBalance, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.AvailBal))))
               .ForMember(d => d.PukalAcctInd, m => m.MapFrom(src => src.PukalAccountInd))
               ;
            this.CreateMap<CollectionHistoryDTO, CollectionHistoryViewModel>()
              .ForMember(d => d.CollectionNo, m => m.MapFrom(src => Convert.ToString(src.CollectNo)))
              .ForMember(d => d.Priority, m => m.MapFrom(src => src.Priority))
              .ForMember(d => d.CollectSts, m => m.MapFrom(src => src.CollectStatus))
              .ForMember(d => d.UserId, m => m.MapFrom(src => src.UserId))
              .ForMember(d => d.CloseDate, m => m.MapFrom(src => src.CloseDate))
              .ForMember(d => d.CreationDate, m => m.MapFrom(src => src.CreationDate))
              ;
            this.CreateMap<DelinquentAcctFinancialInfoDTO, CollInfoViewModel>()
              .ForMember(d => d.PaymentTerm, m => m.MapFrom(src => Convert.ToString(src.PaymentTerm)))
              .ForMember(d => d.DunningCode, m => m.MapFrom(src => Convert.ToString(src.DunningCode)))
              .ForMember(d => d.PermanentCreditLimit, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.PermanentCreditLimit))))
              .ForMember(d => d.TempCreditLimit, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TemporaryCreditLimit))))
              .ForMember(d => d.TotalTAR, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TotalTar))))
              .ForMember(d => d.OutstandingAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.OutstandingAmount))))
              .ForMember(d => d.OverdueAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.OverdueAmount))))
              .ForMember(d => d.AgeCode, m => m.MapFrom(src => Convert.ToString(src.AgeCode)))
              .ForMember(d => d.DelinquentDays, m => m.MapFrom(src => Convert.ToString(src.DelinquentDays)))
              ;
            this.CreateMap<CollectionFollowUpDTO, CollectionFollowUpViewModel>()
              .ForMember(d => d.SelectedCollectionSts, m => m.MapFrom(src => src.Status))
              .ForMember(d => d.SelectedPriority, m => m.MapFrom(src => src.Priority))
              .ForMember(d => d.CreationDate, m => m.MapFrom(src => NumberExtensions.DateTimeConverter(src.EventCreationDate)))
              .ForMember(d => d.RecallDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.RecallDate)))
              .ForMember(d => d.LastUpdate, m => m.MapFrom(src => NumberExtensions.DateTimeConverter(src.LastUpdDate)))
              .ForMember(d => d.UserId, m => m.MapFrom(src => src.CreatedBy))
              .ForMember(d => d.NoteCreationDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.DetailCreationDate)))
              ;
            this.CreateMap<DelinquentAcctInfoDTO, CollectionAcctInfoViewModel>()
              .ForMember(d => d.AcctNo, m => m.MapFrom(src => Convert.ToString(src.AccountNumber)))
              .ForMember(d => d.CmpyName, m => m.MapFrom(src => src.CompanyName))
              .ForMember(d => d.ClientType, m => m.MapFrom(src => src.ClientType))
              .ForMember(d => d.CorpCode, m => m.MapFrom(src => src.CorporateCode))
              .ForMember(d => d.CorpName, m => m.MapFrom(src => src.CorporateName))
              .ForMember(d => d.SalesTerritory, m => m.MapFrom(src => src.SalesTerritory))
              .ForMember(d => d.CreationDate, m => m.MapFrom(src => src.CreationDate.HasValue ? NumberExtensions.DateConverter(src.CreationDate) : ""))
              .ForMember(d => d.BlockedDate, m => m.MapFrom(src => src.BlockedDate.HasValue ? NumberExtensions.DateConverter(src.BlockedDate) : ""))
              .ForMember(d => d.TempReinstatementDateFrom, m => m.MapFrom(src => src.TempReinstatementFrom))
              .ForMember(d => d.TempReinstatementDateTo, m => m.MapFrom(src => src.TempReinstatementTo))
              .ForMember(d => d.ContactPerson, m => m.MapFrom(src => src.ContactPerson))
              .ForMember(d => d.EmailAddr, m => m.MapFrom(src => src.EmailAddress))
              ;  
        }
    }
}