using ModelSector.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace ModelSector
{
    public class LookupParameters
    {
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "ParameterCodeLbl")]
        public string ParameterCode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "CityCodeLbl")]
        public string CityCode { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "ParameterDescpLbl")]
        public string ParameterDescp { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "StateCodeLbl")]
        public string StateCode { get; set; }
        [Required]
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "StateNameLbl")]
        public string StateName { get; set; }
        public string Country { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "CityNameLbl")]
        public string CityName { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "TypeLbl")]
        public string type { get; set; }
        public string flag { get; set; }
        public IEnumerable<SelectListItem> ProductGroup { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "SelectedProductGroupDdl")]
        public string SelectedProductGroup { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "DescpLbl")]
        [StringLength(10, ErrorMessage = "The Short Description value cannot exceed 10 characters. ")]
        public string Descp { get; set; }
        public string LastUpdated { get; set; }
        public string UserId { get; set; }
        public IEnumerable<SelectListItem> ProductType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "SelectedProductTypeDdl")]
        public string SelectedProductType { get; set; }
        public IEnumerable<SelectListItem> ProductCategory { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "SelectedProductCategoryDdl")]
        public string SelectedProductCategory { get; set; }
        public IEnumerable<SelectListItem> BillingPlan { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "SelectedBillingPlanDdl")]
        public string SelectedBillingPlan { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "ProductCodeLbl")]
        public string ProductCode { get; set; }
        public IEnumerable<SelectListItem> ProductCodes { get; set; }
        public string ProductName { get; set; }
        public string UnitPrice { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "ProdDescpLbl")]
        public string ProdDescp { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "EffectiveFromLbl")]
        public string EffectiveFrom { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "ExpiryDateLbl")]
        public string ExpiryDate { get; set; }
        public string ProdId { get; set; }
        public IEnumerable<ProductListItems> ProductItems { get; set; }
        public IEnumerable<EventRcptList> _EventRcptList { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "PlanIdLbl")]
        public string PlanId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "SelectedTypeDdl")]
        public string SelectedType { get; set; }
        public IEnumerable<SelectListItem> RebateType { get; set; }
        public string MinPurchaseAmt { get; set; }
        public string SubSeqPurchaseAmt { get; set; }
        public string SubSeqBillingAmt { get; set; }
        public string BillingPlanUserId { get; set; }
        public string BillingPlanLastUpdate { get; set; }
        public string PlanUserId { get; set; }
        public IEnumerable<SelectListItem> Priority { get; set; }
        public IEnumerable<SelectListItem> Scope { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        public IEnumerable<SelectListItem> EventType { get; set; }
        public IEnumerable<SelectListItem> Frequency { get; set; }
        public IEnumerable<SelectListItem> Owner { get; set; }
        public string SelectedReason { get; set; }
        public IEnumerable<SelectListItem> Reason { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "EventIdLbl")]
        public string Id { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "EventScheduleIdLbl")]
        public string EventScheduleId { get; set; }
        public string RcptName { get; set; }
        public string RcptContent { get; set; }
        public string RcptContTmpl { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "EventRcptIdLbl")]
        public string EventRcptId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "EventTypeIdLbl")]
        public string EventTypeId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "SelectedEventTypeDdl")]
        public string SelectedEventType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "ShortDescpLbl")]
        public string ShortDescp { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "DetailedDescpLbl")]
        public string DetailedDescp { get; set; }
        public string SelectedStatus { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "SelectedPriorityDdl")]
        public string SelectedPriority { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "SelectedScopeDdl")]
        public string SelectedScope { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "SelectedFrequencyDdl")]
        public string SelectedFrequency { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "SelectedOwnerDdl")]
        public string SelectedOwner { get; set; }
        public string UpdateDate { get; set; }
        public string UpdatedBy { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "TimesLbl")]
        public int Times { get; set; }
        public string BitmapAmt { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "MaxOccurLbl")]
        public string MaxOccur { get; set; }
        public string OccurPeriodInterval { get; set; }
        public string OccurPeriodType { get; set; }
        public string MinIntVal { get; set; }
        public string MaxIntVal { get; set; }
        public string MinMoneyVal { get; set; }
        public string MaxMoneyVal { get; set; }
        public string MinDateVal { get; set; }
        public string MaxDateVal { get; set; }
        public string MinTimeVal { get; set; }
        public string MaxTimeVal { get; set; }
        public string VarCharVal { get; set; }
        public string PeriodType { get; set; }
        public string PeriodInterval { get; set; }
        public string EvtPlanDetailId { get; set; }
        public int  NotifyInd { get; set; }
        public string TemplateDisplayer { get; set; }
        public string TmplLangInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "ApplyAllIndLbl")]
        public bool ApplyAllInd { get; set; }
        public string EventPlanId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "SelectedRefToDdl")]
        public string SelectedRefTo { get; set; }
        public IEnumerable<SelectListItem> RefTo { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "RefKeyLbl")]
        public string RefKey { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "SeletedEventIndDdl")]
        [Required]
        public string SeletedEventInd { get; set; }
        public IEnumerable<SelectListItem> EventInd { get; set; }
        public string ParamInd { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "CompanyNameLbl")]
        public string CompanyName { get; set; }
        public string CreationDate { get; set; }
        public string Channel { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "TypeDescLbl")]
        public string TypeDesc { get; set; }
        public string SentDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "DefaultIndLbl")]
        public bool DefaultInd { get; set; }
        public IEnumerable<SelectListItem> Languages { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendGlobalVariable", "SelectedlanguageDdl")]
        public string Selectedlanguage { get; set; }
    }
    public class ProductListItems
    {
        public string UnitPrice { get; set; }
        public string EffectiveFrom { get; set; }
        public string ExpiryDate { get; set; }
        public string LastUpdated { get; set; }
        public string UserId { get; set; }
        public string ProdId { get; set; }
        public string ProductCode { get; set; }
        public string MinPurchaseAmt { get; set; }
        public string SubSeqPurchaseAmt { get; set; }
        public string SubSeqBillingAmt { get; set; }
        public string BillingPlanUserId { get; set; }
        public string BillingPlanLastUpdate { get; set; }
        public string SelectedType { get; set; }
        ////////////////////////////////////////////////////
        public string BitmapAmt { get; set; }
        public string MinIntVal { get; set; }
        public string MaxIntVal { get; set; }
        public string MinMoneyVal { get; set; }
        public string MaxMoneyVal { get; set; }
        public string MinDateVal { get; set; }
        public string MaxDateVal { get; set; }
        public string MinTimeVal { get; set; }
        public string MaxTimeVal { get; set; }
        public string VarCharVal { get; set; }
        public string PeriodType { get; set; }
        public string PeriodInterval { get; set; }
        public string EvtPlanDetailId { get; set; }
    }

    public class EventRcptList
    {
        public Int64 Id { get; set; }
        public string EventScheduleId { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public Int64 ChannelInd { get; set; }
        public string LangInd { get; set; }
        public Int64 ContentId { get; set; }
    }

    public class TmplDisplayer
    {
        public string Type { get; set; }
        public string Descp { get; set; }
        public string ContentTmplt { get; set; }
        public string LangInd { get; set; }
    }

}
