using AutoMapper;
using CCMS.ModelSector;
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
    public class NotificationSearchController : BaseController
    {
        //private NotifSearchMaint _Maint = new NotifSearchMaint();
        // GET: NotificationSearch
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {

                case "lis":
                    return PartialView(this.getPartialPath("NotificationSearch", "Search_Partial"), new LookupParameters());
                case "det":
                    return PartialView(this.getPartialPath("NotificationSearch", "Notification_Search_Detail"), new LookupParameters());
                default:
                    return PartialView();
            }
        }
        public async Task<ActionResult> FillData(string Prefix, string Id)
        {
            //EventConfigMaint _EventConfigMaint = new EventConfigMaint();
            var _LookupParameters = new LookupParameters
            {
                EventInd = await BaseService.GetEvtInd(),
                //WebGetEvtInd(),
                RefTo = await BaseService.GetEvtRef("I"),
                //await WebGetEvtRef("I"),
                EventType = await BaseService.GetEvtType(),
                //await _EventConfigMaint.WebGetEvtType(),
                Priority = await BaseService.GetRefLib("Priority"),
                //await WebGetRefLib("Priority"),
                Status = await BaseService.GetRefLib("Status"),
                //await WebGetRefLib("Status"),
                Scope = await BaseService.GetRefLib("Scope"),
                //await WebGetRefLib("Scope"),
                Owner = await BaseService.GetRefLib("NtfEventOwner"),
                //await WebGetRefLib("NtfEventOwner"),
                Frequency = await BaseService.GetRefLib("NtfEventPeriodType"),
                //await WebGetRefLib("NtfEventPeriodType"),
                Languages = await BaseService.GetRefLib("Language"),
                //await WebGetRefLib("Language"),
                 
            };
            switch (Prefix)
            {
                case "lis":

                    return Json(new { Selects = _LookupParameters, Model = new LookupParameters() }, JsonRequestBehavior.AllowGet);
                case "det":
                    var Info = (await NotifSearchService.GetEventSelect(Id)).lookupParameters;
                    return Json(new { Selects = _LookupParameters, Model = Info }, JsonRequestBehavior.AllowGet);
                default:
                    return Json(new { }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> WebNtfyEventSearch(jQueryDataTableParamModel Params, LookupParameters _model)
        {
            var list = (await NotifSearchService.GetNtfyEventSearch(_model.SeletedEventInd, _model.SelectedEventType, _model.SelectedRefTo, _model.RefKey, _model.StartDate, _model.EndDate)).lookupParameters;
            var _filtered = new List<LookupParameters>();
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.Id.ToLower().Contains(Params.sSearch) || p.SeletedEventInd.ToLower().Contains(Params.sSearch) || 
                    p.ShortDescp.ToLower().Contains(Params.sSearch) || p.SelectedReason.ToLower().Contains(Params.sSearch) || p.CompanyName.Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(p => new object[] {
                    p.Id, 
                    p.SeletedEventInd,
                    p.ShortDescp,
                    p.SelectedReason,
                    p.SelectedOwner,
                    p.SelectedRefTo, 
                    p.RefKey,
                    p.CompanyName,
                    p.CreationDate,
                    p.Channel })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> WebEvtSelect(string EventId)
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
            var models = (await NotifSearchService.GetEventSelect(EventId)).lookupParameters;
            return Json(new {Model=models,Selects=Selects }, JsonRequestBehavior.AllowGet);
        }
    }
}