using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaxMind.GeoIP;
using ModelSector;
using CCMS.ModelSector;
using FleetOps.Models;
using System.Threading.Tasks;
using FleetOps.App_Start;
using FleetOps.ViewModel;
using FleetSys.Models;
using System.IO;
using AutoMapper;
using CardTrend.Domain.Dto;
using CardTrend.Business.CcmsServices;

namespace FleetSys.Controllers
{
    public class ApprovalController : BaseController
    {
        //private CardAcctSignUpOps objCardAcctSignUpOps = new CardAcctSignUpOps();
        // GET: Approval
        [AccessibilityXtra("Approval","Index","apr")]
        public ActionResult Index()
        {
            return View();
        }
        [AccessibilityXtra]
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                case "apr":
                    return PartialView(this.getPartialPath("Applications", "Approval_Partial"), new Milestone());
                default:
                    return PartialView();
            }
        }
        #region "Milestone"
        [CompressFilter]
        public async Task<ActionResult> WebMilestoneListSelect(jQueryDataTableParamModel Params, Milestone _milestone)
        {
            var _filtered = new List<Milestone>();
            _milestone.UserId = GetUserId;
            var list = await CardAcctSignUpService.WebMilestoneListSelect(_milestone.UserId, _milestone.workflowcd,_milestone.Ind);

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }
        if (!string.IsNullOrEmpty(Params.sSearch))
            {
                if (_milestone.workflowcd.ToLower() == "SPOREQTRCKR".ToLower())
                {
                    _filtered = list.Where(p => p.RefKey.ToString().Contains(Params.sSearch) || p.Descp.Contains(Params.sSearch) ||
                                           p.RequestValue.ToLower().Contains(Params.sSearch) || p.TaskDescp.ToLower().Contains(Params.sSearch) ||
                                           p.CardNumber.ToLower().Contains(Params.sSearch) || p.AcctNo.ToLower().Contains(Params.sSearch) ||
                                           p.CompanyName.ToLower().Contains(Params.sSearch) || p.selectedStatus.ToLower().Contains(Params.sSearch) ||
                                           p.RecallDate.ToLower().Contains(Params.sSearch) || p.RequestBy.ToLower().Contains(Params.sSearch) ||
                                           p.CreationDate.Contains(Params.sSearch)).ToList();
                    _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
                }
                else
                {
                    _filtered = list.Where(p => p.RefKey.ToString().Contains(Params.sSearch) || p.SelectedTaskNo.Contains(Params.sSearch) ||
                                           p.selectedPriority.ToLower().Contains(Params.sSearch) || p.selectedStatus.ToLower().Contains(Params.sSearch) ||
                                           p.CreationDate.Contains(Params.sSearch)).ToList();
                    _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
                }

            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }

            if (_milestone.workflowcd == "APPL" || string.IsNullOrEmpty(_milestone.workflowcd))
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { x.RefKey, x.SelectedTaskNo, x.selectedPriority, x.selectedStatus, x.CreationDate, x.LastUpdDate })// x.selectedReasonCd, x.Remarks, x.Remarks, x.RecallDate,
                }, JsonRequestBehavior.AllowGet);
            }
            else if (_milestone.workflowcd.ToLower() == "SPOREQTRCKR".ToLower())
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { x.RefKey, x.Descp, x.RequestValue, x.SelectedTaskNo, x.TaskDescp, x.CardNumber, x.AcctNo, x.CompanyName, x.selectedStatus, x.CreationDate, x.RecallDate, x.RequestBy, x.workflowcd })
                }, JsonRequestBehavior.AllowGet);
            }
            else if (_milestone.workflowcd.ToLower() == "SPOREQTRCKR".ToLower())
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { x.RefKey, x.Descp, x.RequestValue, x.SelectedTaskNo, x.TaskDescp, x.CardNumber, x.AcctNo, x.CompanyName, x.selectedStatus, x.CreationDate, x.RecallDate, x.RequestBy, x.workflowcd })
                }, JsonRequestBehavior.AllowGet);
            }
            else if (_milestone.workflowcd.ToLower() == "SPOREQTRCKR".ToLower())
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { x.RefKey, x.Descp, x.RequestValue, x.SelectedTaskNo, x.TaskDescp, x.CardNumber, x.AcctNo, x.CompanyName, x.selectedStatus, x.CreationDate, x.RecallDate, x.RequestBy, x.workflowcd })
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { x.BatchID, x.RefKey, x.SelectedTaskNo, x.TaskDescp, x.selectedPriority, x.CreationDate, x.selectedStatus, x.ID })// x.selectedReasonCd, x.Remarks, x.Remarks, x.RecallDate,
                }, JsonRequestBehavior.AllowGet);
            }

        }
        [CompressFilter]
        public async Task<ActionResult> WebPukalApprovalMilestoneList(jQueryDataTableParamModel Params, Milestone _milestone)
        {
            var _filtered = new List<PukalApproval>();
            _milestone.UserId = GetUserId;
            var list = await CardAcctSignUpService.GetApprovalMilestoneListSelect(_milestone.UserId,_milestone.workflowcd,_milestone.Ind);

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                    _filtered = list.Where(p => p.AreaCode.ToLower().Contains(Params.sSearch.ToLower()) || p.Refkey.ToLower().Contains(Params.sSearch.ToLower()) ||
                                           p.TaskDescp.ToLower().Contains(Params.sSearch.ToLower())     || p.StsDescp.ToLower().Contains(Params.sSearch.ToLower())||
                                           p.RefCd.ToLower().Contains(Params.sSearch.ToLower()) || p.ChequeNo.ToString().Contains(Params.sSearch.ToLower()) ||
                                           p.ChequeAmount.ToString().Contains(Params.sSearch)).ToList();
                    _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();

            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }

            return Json(new{
                            sEcho = Params.sEcho,
                            iTotalRecords = list.Count(),
                            iTotalDisplayRecords = list.Count(),
                            aaData = _filtered.Select(x => new object[] { x.Refkey, x.RefCd, x.AreaCode, x.ChequeAmount, x.StmtDate })
                           }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> WebMilestoneHistorySelect(Milestone _milestone)
        {
            var data = (await CardAcctSignUpService.GetMilestoneHistorySelect(_milestone.workflowcd, _milestone.RefKey)).milestoneHistories;
            return Json(new { result = data, user = GetUserId }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> WebMilestoneApplValidation(Milestone _milestone)
        {
            var data = (await CardAcctSignUpService.MilestoneApplValidation(_milestone.aprId)).mileStoneInfo;
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveMileStone(Milestone _milestone)
        {
            var SaveMile = await CardAcctSignUpService.SaveMilestone(_milestone);
            return Json(new { result = SaveMile }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ftMilestoneInfo(Milestone _milestone)
        {
            var data = (await CardAcctSignUpService.GetMilestoneInfo(_milestone.workflowcd, Convert.ToInt32(_milestone.SelectedTaskNo))).mileStoneInfo;
            data.aprId = _milestone.aprId;
            if (!string.IsNullOrEmpty(data.validationSP))
            {
                await CardAcctSignUpService.MilestoneApplValidation(_milestone.aprId);
            }
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetTaskNo(int CurrentTaskNo, string type = "APPL")
        {
            var data = (await BaseService.GetNextMilestone(CurrentTaskNo, type)).RefLibLst;
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}