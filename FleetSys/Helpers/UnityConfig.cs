using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.Application;
using CardTrend.Domain.Dto.CardHolder;
using CardTrend.Domain.Dto.Collection;
using CardTrend.Domain.Dto.Corporate;
using CardTrend.Domain.Dto.EventConfiguration;
using CardTrend.Domain.Dto.Fraud;
using CardTrend.Domain.Dto.GlobalVariables;
using CardTrend.Domain.Dto.ManualSlipEntry;
using CardTrend.Domain.Dto.Merchant;
using CardTrend.Domain.Dto.MerchantMultiAdjustment;
using CardTrend.Domain.Dto.MultipleAdjustment;
using CardTrend.Domain.Dto.MultiplePayment;
using CardTrend.Domain.Dto.NotifSearch;
using CardTrend.Domain.Dto.PinMailer;
using CardTrend.Domain.Dto.PukaAcct;
using CardTrend.Domain.Dto.ReportViewer;
using CardTrend.Domain.Dto.SOASummary;
using CardTrend.Domain.Dto.TransactionSearch;
using CCMS.ModelSector;
using FleetOps.Models;
using FleetSys.ViewModel;
using ModelSector;
using ModelSector.Fraud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace FleetSys.Helpers
{
    public static class UnityConfig
    {
        public static void RegisterMapping()
        {
            Mapper.Initialize(cfg =>
             {
                 //Application   
                 cfg.CreateMissingTypeMaps = true;
                 cfg.AddProfile<UserAccessMappingProfile>();
                 cfg.AddProfile<MultiPaymentMappingProfile>();
                 cfg.AddProfile<TxnAdjustmentMappingProfile>();
                 cfg.AddProfile<PukalAcctMappingProfile>();
                 cfg.AddProfile<MultipleAdjMappingProfile>();
                 cfg.AddProfile<ApplicationMappingProfile>();
                 cfg.AddProfile<CollectionMappingProfile>();
                 cfg.AddProfile<SOASummaryMappingProfile>();
                 cfg.AddProfile<NotificationSearchMappingProfile>();
                 cfg.AddProfile<GlobalVariableOpMappingProfile>();
                 cfg.AddProfile<ManualSlipEntryMappingProfile>();
                 cfg.AddProfile<MechSignMappingProfile>();
                 cfg.AddProfile<TxnSearchMappingProfile>();
                 cfg.AddProfile<CorporateMappingProfile>();
                 cfg.AddProfile<EventConfigMappingProfile>();
                 cfg.AddProfile<CardAcctSignUpMappingProfile>();
                 cfg.AddProfile<CardHolderMappingProfile>();
                 cfg.AddProfile<AccountOpMappingProfile>();
                 cfg.AddProfile<ApplicantSignUpMappingProfile>();
                 cfg.AddProfile<FraudMappingProfile>();
             });
        }
    }
}