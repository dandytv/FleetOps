using AutoMapper;
using CardTrend.Domain.Dto.EventConfiguration;
using CCMS.ModelSector;
using FleetOps.App_Start;
using FleetOps.Models;
using FleetSys.Models;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace FleetSys.Controllers
{
    [Authorize(Roles = "Internal")]
    public class EventConfigurationController : BaseController
    {
        // GET: EventConfiguration
        public ActionResult Index()
        {
            return View();
        }
        [AccessibilityXtra]
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                case "evc":
                    return PartialView(this.getPartialPath("EventConfiguration", "Event_Config_List"));
                case "dtl":
                    return PartialView(this.getPartialPath("EventConfiguration", "Event_Config_Details"), new LookupParameters());
                default:
                    return PartialView();
            }
        }
        public async Task<ActionResult> FillData(string PlanId)
        {
            var Selects = new LookupParameters
            {
                EventType = await BaseService.GetEvtType(),
                Priority = await BaseService.GetRefLib("Priority"),
                Status = await BaseService.GetRefLib("Status"),
                Scope = await BaseService.GetRefLib("Scope"),
                Owner = await BaseService.GetRefLib("NtfEventOwner"),
                Frequency = await BaseService.GetRefLib("NtfEventPeriodType"),
                Languages = await BaseService.GetRefLib("Language")
            };
            var Model = (await EventConfigService.GetNtfyEventConf(PlanId)).lookupParameters;
            if (Model.Any())
            {
                Selects.RefTo = await BaseService.GetEvtRefConf(Model.FirstOrDefault().EventTypeId); //WebGetEvtRefConf
            }
            if (!Model.Any())
            {
                Model.Add(new LookupParameters());
            }
            return Json(new { Model = Model, Selects = Selects }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> WebNtfyEvtConfListSelect(jQueryDataTableParamModel Params)
        {
            //var list = await _EventConfigMaint.WebNtfyEvtConfListSelect();
            var list = (await EventConfigService.GetNotifyEventConfDetails(GetUserId)).lookupParameters;
            var _filtered = new List<LookupParameters>();
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.EventTypeId) ? p.EventTypeId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ShortDescp) ? p.ShortDescp : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedRefTo) ? p.SelectedRefTo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.RefKey) ? p.RefKey : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedStatus) ? p.SelectedStatus : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UpdateDate) ? p.UpdateDate : string.Empty).ToLower().Contains(Params.sSearch)).ToList();

                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = _filtered.Select(p => new object[] { p.EventTypeId, p.SelectedEventType, p.ShortDescp, p.DetailedDescp, p.SelectedRefTo, p.RefKey, p.SelectedStatus, p.LastUpdated, p.UpdatedBy })
            }, JsonRequestBehavior.AllowGet);
        }      
        public async Task<ActionResult> WebNtfyEventRcptListSelect(string PlanId)
        {
            var list = Mapper.Map<List<EventRcptList>>((await EventConfigService.GetWebNtfyEventRcpts(PlanId)).eventRcpts);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> WebAcctEventConfSelect(string EventId,string EventScheduleId, string AcctNo)
        {
            var info = (await EventConfigService.GetEventAcctConfSelect(EventId, EventScheduleId, AcctNo)).lookupParameters;
            return Json(info);
        }
        [HttpGet]
        public async Task<ActionResult> WebEventSelect(String EventId)
        {
            var list = (await NotifSearchService.GetEventSelect(EventId)).lookupParameters;
           return Json(list, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> WebGetEvtType()
        {
            var list = await BaseService.GetEvtType();
            return Json(list);
        }
        public async Task<ActionResult> WebNtfEvtConfDelete(string ScheduleId)
        {
            var info = await EventConfigService.DeleteWebNtfEvtConf(ScheduleId);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> WebNtfyEventConfSelect(string PlanId)
        {
            var model = (await EventConfigService.GetNtfyEventConf(PlanId)).lookupParameters;
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> WebNtfyEventConfMaint(LookupParameters _Params)
        {
            _Params.UserId = GetUserId;
            var result = await EventConfigService.SaveNtfyEvtConfMaint(_Params);
            return Json(result);
        }
        public async Task<ActionResult> WebGetRefCmpyName(string SelectedRefTo, string RefKey)
        {
            var result = await EventConfigService.GetRefCmpyName(SelectedRefTo, RefKey);
            return Json(new { companyName = result }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> WebNtfEvtConfRcptDelete(string SchRcptId)
        {
            var result = await EventConfigService.DeleteWebNtfEvtConfRcpt(SchRcptId);
            return Json(result);
        }
    }
}