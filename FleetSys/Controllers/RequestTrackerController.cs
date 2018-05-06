using System;
using System.Collections.Generic;
using System.Linq;
using FleetOps.App_Start;
using FleetOps.ViewModel;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using FleetOps.Models;
using CCMS.ModelSector;
using FleetSys.Models;
using AutoMapper;

namespace FleetSys.Controllers
{
    public class RequestTrackerController :  BaseController
    {
        //private CardAcctSignUpOps objCardAcctSignUpOps = new CardAcctSignUpOps();
        private AccountOps objAcctOps = new AccountOps();

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [CompressFilter]
        public async Task<ActionResult> ftManualRequestTrackerList(jQueryDataTableParamModel Params, Milestone _milestone)
        {
            var _filtered = new List<Milestone>();
            var list = (await CardAcctSignUpService.GetSPOMilestones(_milestone.selectedStatus)).milestoneHistories;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.RefKey.ToString().Contains(Params.sSearch) || p.Descp.Contains(Params.sSearch) ||
                                        p.RequestValue.ToLower().Contains(Params.sSearch) || p.TaskDescp.ToLower().Contains(Params.sSearch) ||
                                        p.CardNumber.ToLower().Contains(Params.sSearch) || p.AcctNo.ToLower().Contains(Params.sSearch) ||
                                        p.CompanyName.ToLower().Contains(Params.sSearch) || p.selectedStatus.ToLower().Contains(Params.sSearch) ||
                                        p.RequestBy.ToLower().Contains(Params.sSearch) || p.CreationDate.Contains(Params.sSearch)).ToList();
                                       
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
                aaData = _filtered.Select(x => new object[] { x.RefKey, x.Descp, x.RequestValue, x.SelectedTaskNo, x.TaskDescp, x.CardNumber, x.AcctNo, x.CompanyName, x.selectedStatus, x.CreationDate, x.LastUpdDate, x.RequestBy, x.workflowcd })
            }, JsonRequestBehavior.AllowGet);
        }
    }
}