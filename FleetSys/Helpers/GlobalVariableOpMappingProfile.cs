﻿using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.GlobalVariables;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class GlobalVariableOpMappingProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<ProdRefDTO, LookupParameters>()
               .ForMember(d => d.ProductCategory, m => m.Ignore())
               .ForMember(d => d.ProductType, m => m.Ignore())
               .ForMember(d => d.BillingPlan, m => m.Ignore())
               .ForMember(d => d.Descp, m => m.MapFrom(src => src.ShortDescription))
               .ForMember(d => d.ProductCode, m => m.MapFrom(src => src.ProdCd))
               .ForMember(d => d.SelectedProductCategory, m => m.MapFrom(src => src.ProductCategory))
               .ForMember(d => d.SelectedProductType, m => m.MapFrom(src => src.ProductType))
               .ForMember(d => d.UnitPrice, m => m.MapFrom(src => src.UnitPrice.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.UnitPrice)) : "0.00"))
               .ForMember(d => d.SelectedBillingPlan, m => m.MapFrom(src => Convert.ToString(src.BillingPlan)))
               .ForMember(d => d.EffectiveFrom, m => m.MapFrom(src => src.EffDate))
               .ForMember(d => d.ExpiryDate, m => m.MapFrom(src => src.EffEndDate))
               .ForMember(d => d.LastUpdated, m => m.MapFrom(src => src.UpdateDate))
             ;

            this.CreateMap<LookupParameters, ProdRefDTO>()
              .ForMember(d => d.ProductCategory, m => m.Ignore())
              .ForMember(d => d.ProductType, m => m.Ignore())
              .ForMember(d => d.BillingPlan, m => m.Ignore())
              .ForMember(d => d.ShortDescription, m => m.MapFrom(src => src.Descp))
              .ForMember(d => d.ProdCd, m => m.MapFrom(src => src.ProductCode))
              .ForMember(d => d.ProductCategory, m => m.MapFrom(src => src.SelectedProductCategory))
              .ForMember(d => d.ProductType, m => m.MapFrom(src => src.SelectedProductType))
              .ForMember(d => d.UnitPrice, m => m.MapFrom(src => NumberExtensions.ConvertDecimalToDb(src.UnitPrice)))
              .ForMember(d => d.BillingPlan, m => m.MapFrom(src => Convert.ToInt32(src.SelectedBillingPlan)))
              .ForMember(d => d.EffDate, m => m.MapFrom(src => src.EffectiveFrom))
              .ForMember(d => d.EffEndDate, m => m.MapFrom(src => src.ExpiryDate))
              .ForMember(d => d.UpdateDate, m => m.MapFrom(src => src.LastUpdated))
              .ForMember(d => d.ProductItems, m => m.MapFrom(src => CustomMapperExtensions.MapIEnumerableToList(src.ProductItems)))
              ;

            this.CreateMap<ProdGroupRefDTO, LookupParameters>()
                .ForMember(d => d.ProductGroup, m => m.Ignore())
                .ForMember(d => d.ProductCategory, m => m.Ignore())
                .ForMember(d => d.ProductType, m => m.Ignore())
                .ForMember(d => d.Descp, m => m.MapFrom(src => src.Description))
                .ForMember(d => d.SelectedProductGroup, m => m.MapFrom(src => src.ProductGroup))
                .ForMember(d => d.LastUpdated, m => m.MapFrom(src => Convert.ToString(src.UpdateDate)))
                .ForMember(d => d.UserId, m => m.MapFrom(src => src.UserId))
                .ForMember(d => d.ProductCode, m => m.MapFrom(src => Convert.ToString(src.ProductCode)))
                .ForMember(d => d.SelectedProductCategory, m => m.MapFrom(src => src.ProductCategory))
                .ForMember(d => d.SelectedProductType, m => m.MapFrom(src => src.ProductType))
            ;
            this.CreateMap<TmplDisplayDTO, TmplDisplayer>()
                .ForMember(d => d.ContentTmplt, m => m.MapFrom(src => src.TemplateDisplayer))
                .ForMember(d => d.LangInd, m => m.MapFrom(src => src.TemplateLanguageIndicator))
            ;
            this.CreateMap<LookupParameterDTO, LookupParameters>()
                .ForMember(d => d.ProductCode, m => m.MapFrom(src => src.ProdCd))
                .ForMember(d => d.ProductName, m => m.MapFrom(src => src.ProdName))
                .ForMember(d => d.Descp, m => m.MapFrom(src => src.ProdDescp))
                .ForMember(d => d.SelectedProductCategory, m => m.MapFrom(src => src.ProdCategory))
                .ForMember(d => d.SelectedProductType, m => m.MapFrom(src => src.ProdType))
                .ForMember(d => d.UnitPrice, m => m.MapFrom(src => src.ProdUnitPrice.HasValue ? (Convert.ToString(src.ProdUnitPrice)) : ""))
                .ForMember(d => d.ParameterCode, m => m.MapFrom(src => src.ParamCd))
                .ForMember(d => d.CityCode, m => m.MapFrom(src => src.ParamCd))
                .ForMember(d => d.StateName, m => m.MapFrom(src => src.StateName))
                .ForMember(d => d.StateCode, m => m.MapFrom(src => src.StateCd))
                .ForMember(d => d.CityName, m => m.MapFrom(src => src.City))
                .ForMember(d => d.ParameterDescp, m => m.MapFrom(src => src.Descp))
                .ForMember(d => d.SelectedProductGroup, m => m.MapFrom(src => src.ProductGroup))
                .ForMember(d => d.LastUpdated, m => m.MapFrom(src => src.UpdatedOn))
                .ForMember(d => d.UserId, m => m.MapFrom(src => src.UpdatedBy))
                .ForMember(d => d.ProductItems, m => m.MapFrom(src => CustomMapperExtensions.MapIEnumerableToList(src.ProductItems)))
                ;
            this.CreateMap<LookupParameters, LookupParameterDTO>()
             .ForMember(d => d.Type, m => m.MapFrom(src => src.type))
             .ForMember(d => d.StateCd, m => m.MapFrom(src => src.StateCode))
             .ForMember(d => d.ParamCd, m => m.MapFrom(src => src.type.ToLower() == "city" ?src.CityCode: src.ParameterCode))
             .ForMember(d => d.City, m => m.MapFrom(src => src.CityName))
             .ForMember(d => d.Descp, m => m.MapFrom(src => src.Descp))
             .ForMember(d => d.ProductGroup, m => m.MapFrom(src => src.SelectedProductGroup))
             .ForMember(d => d.ProductItems, m => m.MapFrom(src => CustomMapperExtensions.MapIEnumerableToList(src.ProductItems)))
             ;
            this.CreateMap<EventTypeDTO, LookupParameters>()
               .ForMember(d => d.Scope, m => m.Ignore())
               .ForMember(d => d.EventTypeId, m => m.MapFrom(src => Convert.ToString(src.EvtTypeID)))
               .ForMember(d => d.EventPlanId, m => m.MapFrom(src => Convert.ToString(src.EvtPlanId)))
               .ForMember(d => d.ShortDescp, m => m.MapFrom(src => src.ShortDescription))
               .ForMember(d => d.TypeDesc, m => m.MapFrom(src => src.ShortDescription))
               .ForMember(d => d.SelectedEventType, m => m.MapFrom(src => src.Type))
               .ForMember(d => d.SelectedPriority, m => m.MapFrom(src => src.Severity))
               .ForMember(d => d.SelectedOwner, m => m.MapFrom(src => src.Scope))
               .ForMember(d => d.SelectedStatus, m => m.MapFrom(src => src.Status))
               .ForMember(d => d.ApplyAllInd, m => m.MapFrom(src => src.ApplyAllInd == "Y" ? true : false))
               .ForMember(d => d.DetailedDescp, m => m.MapFrom(src => src.FullDescription))
               .ForMember(d => d.BitmapAmt, m => m.MapFrom(src => Convert.ToString(src.BitmapAmt)))
               .ForMember(d => d.MaxOccur, m => m.MapFrom(src => Convert.ToString(src.TotalOccurs)))
               .ForMember(d => d.SelectedFrequency, m => m.MapFrom(src => src.SetFrequencyType))
               .ForMember(d => d.MinIntVal, m => m.MapFrom(src => src.MinIntVal.HasValue ? Convert.ToString(src.MinIntVal) : ""))
               .ForMember(d => d.MaxIntVal, m => m.MapFrom(src => src.MaxIntVal.HasValue ? Convert.ToString(src.MaxIntVal) : ""))
               .ForMember(d => d.EvtPlanDetailId, m => m.MapFrom(src => Convert.ToString(src.EvtPlanDetailId)))
               .ForMember(d => d.MinMoneyVal, m => m.MapFrom(src => src.MinMoneyVal.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.MinMoneyVal)) : ""))
               .ForMember(d => d.MaxMoneyVal, m => m.MapFrom(src => src.MaxMoneyVal.HasValue ? NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.MaxMoneyVal)) : ""))

               .ForMember(d => d.MinDateVal, m => m.MapFrom(src => src.MinDateVal.HasValue ? NumberExtensions.DateConverter(src.MinDateVal) : ""))
               .ForMember(d => d.MaxDateVal, m => m.MapFrom(src => src.MaxDateVal.HasValue ? NumberExtensions.DateConverter(src.MaxDateVal) : ""))
               .ForMember(d => d.MinTimeVal, m => m.MapFrom(src => src.MinTimeVal.HasValue ? Convert.ToString(src.MinTimeVal) : ""))
               .ForMember(d => d.MaxTimeVal, m => m.MapFrom(src => src.MaxTimeVal.HasValue ? Convert.ToString(src.MaxTimeVal) : ""))

               .ForMember(d => d.PeriodInterval, m => m.MapFrom(src => Convert.ToString(src.PeriodInterval)))
               .ForMember(d => d.NotifyInd, m => m.MapFrom(src => Convert.ToInt32(src.NtfyInd)))
               .ForMember(d => d.LastUpdated, m => m.MapFrom(src => NumberExtensions.DateConverter(src.UpdateOn)))
               .ForMember(d => d.UpdatedBy, m => m.MapFrom(src => src.Updateby))
               .ForMember(d => d.DefaultInd, m => m.MapFrom(src => src.DefaultInd == "Y" ? true : false))
               .ForMember(d => d.ProductItems, m => m.MapFrom(src => CustomMapperExtensions.MapIEnumerableToList(src.ProductItems)))
              ;
            this.CreateMap<LookupParameters,EventTypeDTO>()
               .ForMember(d => d.Scope, m => m.Ignore())
               .ForMember(d => d.EvtTypeID, m => m.MapFrom(src => NumberExtensions.ConvertLongToDb(src.EventTypeId)))
               .ForMember(d => d.EvtPlanId, m => m.MapFrom(src => NumberExtensions.ConvertLongToDb(src.EventPlanId)))
               .ForMember(d => d.ShortDescription, m => m.MapFrom(src => src.ShortDescp))
               .ForMember(d => d.ShortDescription, m => m.MapFrom(src => src.TypeDesc))
               .ForMember(d => d.Type, m => m.MapFrom(src => src.SelectedEventType))
               .ForMember(d => d.Severity, m => m.MapFrom(src => src.SelectedPriority))
               .ForMember(d => d.Scope, m => m.MapFrom(src => src.SelectedOwner))
               .ForMember(d => d.Status, m => m.MapFrom(src => src.SelectedStatus))
               .ForMember(d => d.ApplyAllInd, m => m.MapFrom(src => NumberExtensions.ConvertBoolDB(src.ApplyAllInd)))
               .ForMember(d => d.FullDescription, m => m.MapFrom(src => src.DetailedDescp))
               .ForMember(d => d.BitmapAmt, m => m.MapFrom(src => NumberExtensions.ConvertLongToDb(src.BitmapAmt)))
               .ForMember(d => d.TotalOccurs, m => m.MapFrom(src =>Convert.ToInt32(src.MaxOccur)))
               .ForMember(d => d.SetFrequencyType, m => m.MapFrom(src => src.SelectedFrequency))
               .ForMember(d => d.MinIntVal, m => m.MapFrom(src => Convert.ToInt32(src.MinIntVal)))
               .ForMember(d => d.MaxIntVal, m => m.MapFrom(src => Convert.ToInt32(src.MaxIntVal)))
               .ForMember(d => d.EvtPlanDetailId, m => m.MapFrom(src => NumberExtensions.ConvertLongToDb(src.EvtPlanDetailId)))
               .ForMember(d => d.MinMoneyVal, m => m.MapFrom(src => NumberExtensions.ConvertDecimalToDb(src.MinMoneyVal)))
               .ForMember(d => d.MaxMoneyVal, m => m.MapFrom(src => NumberExtensions.ConvertDecimalToDb(src.MaxMoneyVal)))
               .ForMember(d => d.MinDateVal, m => m.MapFrom(src => NumberExtensions.ConvertDatetimeDB(src.MinDateVal)))
               .ForMember(d => d.MaxDateVal, m => m.MapFrom(src => NumberExtensions.ConvertDatetimeDB(src.MaxDateVal)))
               .ForMember(d => d.MinTimeVal, m => m.Ignore())
               .ForMember(d => d.MaxTimeVal, m => m.Ignore())
               .ForMember(d => d.PeriodInterval, m => m.MapFrom(src => Convert.ToInt32(src.PeriodInterval)))
               .ForMember(d => d.NtfyInd, m => m.MapFrom(src => NumberExtensions.ConvertLongToDb(src.NotifyInd)))
               .ForMember(d => d.UpdateOn, m => m.MapFrom(src => NumberExtensions.ConvertDatetimeDB(src.LastUpdated)))
               .ForMember(d => d.Updateby, m => m.MapFrom(src => src.UpdatedBy))
               .ForMember(d => d.DefaultInd, m => m.MapFrom(src => NumberExtensions.ConvertBoolDB(src.DefaultInd)))
               .ForMember(d => d.ProductItems, m => m.MapFrom(src => CustomMapperExtensions.MapIEnumerableToList(src.ProductItems)))
              ;
            this.CreateMap<RebatePlanDTO, LookupParameters>()
                .ForMember(d => d.PlanId, m => m.MapFrom(src => Convert.ToString(src.PlanId)))
                .ForMember(d => d.EffectiveFrom, m => m.MapFrom(src => NumberExtensions.DateConverter(src.EffectiveDate)))
                .ForMember(d => d.ExpiryDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.ExpiredDate)))
                .ForMember(d => d.SelectedType, m => m.MapFrom(src => Convert.ToString(src.Type)))
                .ForMember(d => d.LastUpdated, m => m.MapFrom(src => NumberExtensions.DateConverter(src.Plan_UpdateDate)))
                .ForMember(d => d.MinPurchaseAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.TierValue))))
                .ForMember(d => d.SubSeqPurchaseAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.BasisValue))))
                .ForMember(d => d.BillingPlanLastUpdate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.PlanDetail_UpdateDate)))
                .ForMember(d => d.UserId, m => m.MapFrom(src => src.UserId))
              ;
             this.CreateMap<LookupParameters, RebatePlanDTO>()
                 .ForMember(d => d.PlanId, m => m.MapFrom(src => Convert.ToInt32(src.PlanId)))
                 .ForMember(d => d.EffectiveDate, m => m.MapFrom(src => NumberExtensions.ConvertDatetimeDB(src.EffectiveFrom)))
                 .ForMember(d => d.ExpiredDate, m => m.MapFrom(src => NumberExtensions.ConvertDatetimeDB(src.ExpiryDate)))
                 .ForMember(d => d.Type, m => m.MapFrom(src => NumberExtensions.ConvertLongToDb(src.SelectedType)))
                 .ForMember(d => d.Plan_UpdateDate, m => m.MapFrom(src => NumberExtensions.ConvertDatetimeDB(src.LastUpdated)))
                 .ForMember(d => d.TierValue, m => m.MapFrom(src => NumberExtensions.ConvertDecimalToDb(src.MinPurchaseAmt)))
                 .ForMember(d => d.BasisValue, m => m.MapFrom(src => NumberExtensions.ConvertDecimalToDb(src.SubSeqPurchaseAmt)))
                 .ForMember(d => d.PlanDetail_UpdateDate, m => m.MapFrom(src => NumberExtensions.ConvertDatetimeDB(src.BillingPlanLastUpdate)))
                 .ForMember(d => d.UserId, m => m.MapFrom(src => src.UserId))
               ;
            this.CreateMap<RebatePlanDetailDTO, LookupParameters>()
                .ForMember(d => d.PlanId, m => m.MapFrom(src => Convert.ToString(src.PlanId)))
                .ForMember(d => d.type, m => m.MapFrom(src => Convert.ToString(src.Type)))
                .ForMember(d => d.EffectiveFrom, m => m.MapFrom(src => NumberExtensions.DateConverter(src.EffectiveDate)))
                .ForMember(d => d.ExpiryDate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.ExpiredDate)))
                .ForMember(d => d.LastUpdated, m => m.MapFrom(src => Convert.ToString(src.PlansUpdateDate)))
                .ForMember(d => d.MinPurchaseAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.MinPurchAmt))))
                .ForMember(d => d.SubSeqPurchaseAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.SubseqPurchAmt))))
                .ForMember(d => d.SubSeqBillingAmt, m => m.MapFrom(src => NumberExtensions.CustomNumberFormat(String.Format("{0:0.00}", src.SubseqBillingAmt))))
                .ForMember(d => d.BillingPlanUserId, m => m.MapFrom(src => src.BillingPlanUserId))
                .ForMember(d => d.BillingPlanLastUpdate, m => m.MapFrom(src => NumberExtensions.DateConverter(src.BillingPlanLastUpdate)))
                ;
        }
    }
}