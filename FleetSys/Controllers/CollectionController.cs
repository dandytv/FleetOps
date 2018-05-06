using CCMS.ModelSector;
using FleetOps.App_Start;
using FleetOps.Models;
using FleetSys.Common;
using FleetSys.Models;
using FleetSys.ViewModel;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using AutoMapper;
using CardTrend.Domain.Dto.Collection;

namespace FleetSys.Controllers
{
    [Authorize(Roles = "Internal")]
    public class CollectionController : BaseController
    {
        //private CollectionOps objCollectionOps = new CollectionOps();

        [Accessibility]
        public ActionResult Index()
        {
            if (Session["UserModules"] == null)
            {
                var objUserAccessOps = new UserAccessOps();
                Session["UserModules"] = objUserAccessOps.UserIndexAccess();
            }
            return View(new CollectionTaskListViewModel());
        }

        [AccessibilityXtra(Order = 2)]
        [AccessibilitySection(Order = 1)] 
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                case "dal":
                    return PartialView(this.getPartialPath("Collection", "Collection_TaskList_Partial"), new CollectionTaskListViewModel());

                case "cfu":
                    return PartialView(this.getPartialPath("Collection", "Collection_FollowUp_Partial"), new CollectionFollowUpViewModel());

                case "cai":
                    return PartialView(this.getPartialPath("Collection", "Collection_AccountInfo_Partial"), new CollectionAcctInfoViewModel());

                case "cfi":
                    return PartialView(this.getPartialPath("Collection", "Collection_FinancialInfo_Partial"), new CollInfoViewModel());

                case "cch":
                    return PartialView(this.getPartialPath("Collection", "Collection_History_Partial"), new CollectionHistoryViewModel());

                default:
                    return PartialView();
            }
        }

        public async Task<JsonResult> TaskListFillData()
        {
            var _collectionsts = await BaseService.GetRefLib("EventCollectionSts");
            var collectTaskList = new CollectionTaskListViewModel{
                SalesTerritory = await BaseService.GetRefLib("SaleTerritory"),
                Owner = await BaseService.GetUserAccess("InternalUsers")};

            var Dropdown = new { Collectionsts = _collectionsts.Where(x => x.Value.ToLower() != "c").Select(x => x).DefaultIfEmpty(), SalesTerritory = collectTaskList.SalesTerritory, Owner = collectTaskList.Owner };

            return Json(new { aaData = collectTaskList, Dropdown = Dropdown, currentUser=GetUserId}, JsonRequestBehavior.AllowGet);
        }

        [CompressFilter]
        public async Task<ActionResult> GetPendingAcctCollection(jQueryDataTableParamModel Params, CollectionTaskListViewModel collectionTaskListViewModel = null)
        {
            collectionTaskListViewModel.SelectedOwner = GetUserId;
            var _filtered = new List<CollectionTaskListViewModel>();
            var list = (await CollectionOpService.GetAllAcctCollection(collectionTaskListViewModel)).collectionTasks;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.EventId) ? p.EventId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.AcctNo) ? p.AcctNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CmpyName1) ? p.CmpyName1 : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedSalesTerritory) ? p.SelectedSalesTerritory : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.AccumAgeingAmt) ? p.AccumAgeingAmt : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.GraceDueDate) ? p.GraceDueDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CycAge) ? p.CycAge : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Priority) ? p.Priority : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedCollectionSts) ? p.SelectedCollectionSts : string.Empty).Contains(Params.sSearch) ||       
                                            (!string.IsNullOrEmpty(p.RecallDate) ? p.RecallDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).Contains(Params.sSearch)).ToList();


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
                aaData = _filtered.Select(x => new object[] { x.EventId, x.AcctNo, x.CmpyName1, x.SelectedSalesTerritory, x.AccumAgeingAmt, x.GraceDueDate, x.CycAge, x.Priority, x.SelectedCollectionSts, x.RecallDate, x.CreationDate })
            }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public async Task<ActionResult> GetAllAcctCollection(jQueryDataTableParamModel Params, CollectionTaskListViewModel collectionTaskListViewModel=null)
        {
            var _filtered = new List<CollectionTaskListViewModel>();
            var list = (await CollectionOpService.GetAllAcctCollection(collectionTaskListViewModel)).collectionTasks;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.EventId) ? p.EventId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.AcctNo) ? p.AcctNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CmpyName1) ? p.CmpyName1 : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedCorpCode) ? p.SelectedCorpCode : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CorpAcct) ? p.CorpAcct : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedSalesTerritory) ? p.SelectedSalesTerritory : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.AccumAgeingAmt) ? p.AccumAgeingAmt : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CycAge) ? p.CycAge : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.AccountSts) ? p.AccountSts : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedCollectionSts) ? p.SelectedCollectionSts : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.RecallDate) ? p.RecallDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedOwner) ? p.SelectedOwner : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] { x.EventId, x.AcctNo, x.CmpyName1,x.SelectedCorpCode, x.CorpAcct, x.SelectedSalesTerritory, x.AccumAgeingAmt, 
                                                            x.CycAge,x.AccountSts, x.SelectedCollectionSts,x.SelectedOwner, x.RecallDate, x.CreationDate })
            }, JsonRequestBehavior.AllowGet);

        }       
        [CompressFilter]
        public async Task<ActionResult> GetThresholdLmtCollection(jQueryDataTableParamModel Params)
        {
            long TotalNoOfRecs = 0;
            string searchText = Params.sSearch == null ? string.Empty : Params.sSearch.ToLower().Trim();
            var thresholdLmtCollection = await CollectionOpService.GetThresholdLimitCollection(Params.iDisplayStart, Params.iDisplayLength, TotalNoOfRecs, searchText);
            var _filtered = thresholdLmtCollection.collectionTasks;
          
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = thresholdLmtCollection.tOtalNoOfRecs,
                iTotalDisplayRecords = thresholdLmtCollection.tOtalNoOfRecs,
                aaData = _filtered.Select(x => new object[] {  x.AcctNo, x.CmpyName1,x.CorpAcct, x.CorpName, 
                x.SelectedSalesTerritory, x.PermCreditLimit, x.TempCreditLimit,x.PercentageUsage,x.AvailBalance, x.PukalAcctInd })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetCollAcctInfo(string AcctNo)
        {
            var collInfoViewModel = (await CollectionOpService.GetCollectionAccountInfo(AcctNo)).CollectionAcctInfo;
            return Json(new { aaData = collInfoViewModel }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetCollFinancialInfo(string AcctNo)
        {
            var collectionAcctInfoViewModel = (await CollectionOpService.GetCollectionFinancialInfo(AcctNo)).collInfoViewModel;
            return Json(new { aaData = collectionAcctInfoViewModel }, JsonRequestBehavior.AllowGet);
        }

        [CompressFilter]
        public async Task<JsonResult> GetCollAgeingHistory(jQueryDataTableParamModel Params, string AcctNo)
        {
            var list = (await CollectionOpService.GetCollAgeingHistory(AcctNo)).CollAgeingHists;
            list = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = list.Select(x => new object[] { x.Ageing, x.Category, x.TxnAmt, x.OutstandingAmt, x.BillingDate, x.DueDate, x.GraceDueDate, x.LatestPaymentReceived, x.LatestPaymentDate })
            }, JsonRequestBehavior.AllowGet);
        }

        [CompressFilter]
        public async Task<ActionResult> GetCollPaymentHistory(jQueryDataTableParamModel Params, string AcctNo)
        {
            //Int64 TotalNoOfRecs = 0;
            var collpayments = await CollectionOpService.GetCollPaymentHist(AcctNo, Params.iDisplayStart, Params.iDisplayLength);
            //var list = await objCollectionOps.GetCollPaymentHist(AcctNo, Params.iDisplayStart, Params.iDisplayLength, TotalNoOfRecs);
            var list = collpayments.collPaymentHistViews;
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = collpayments.tOtalNoOfRecs,
                iTotalDisplayRecords = collpayments.tOtalNoOfRecs,
                aaData = list.Select(x => new object[] { x.StatementDate, x.DueDate, x.TxnDate, x.PostingDate, x.TxnDesc, x.TxnAmt, x.ApprovalCode })
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetCollFollowUp(string EventId)
        {
            var ddlCollectionFollowUp = new CollectionFollowUpViewModel
            {
                CollectionSts = await BaseService.GetRefLib("EventCollectionSts"),
                Priority = await BaseService.GetRefLib("Priority")
            };
            var collectionFollowUpViewModel = (await CollectionOpService.GetCollFollowUp(EventId)).collectionFollowUps;
            return Json(new { Model = collectionFollowUpViewModel,Selects=ddlCollectionFollowUp }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SaveCollectionFollowUp(CollectionFollowUpViewModel collection)
        {
            collection.UserId = this.GetUserId;
            var result = await CollectionOpService.SaveCollectionFollowUp(Convert.ToInt32(collection.EventId),collection.UserId,collection.SelectedCollectionSts,collection.SelectedPriority,collection.RecallDate,collection.Remarks);
            return Json(new { resultCd = result });
        }

        /// <summary>
        /// CollectionCaseSts => Open(O), Close (C)
        /// </summary>
        [CompressFilter]
        public async Task<ActionResult> GetCollHistory(jQueryDataTableParamModel Params, string AcctNo, string CollectionCaseSts)
        {
            var _filtered = new List<CollectionHistoryViewModel>();
            var list = (await CollectionOpService.GetCollectionHistory(AcctNo, Convert.ToString(CollectionCaseSts))).collectionHistories;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.CollectionNo) ? p.CollectionNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Priority) ? p.Priority : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CollectSts) ? p.CollectSts : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CloseDate) ? p.CloseDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] { x.CollectionNo, x.Priority, x.CollectSts, x.UserId, x.CloseDate, x.CreationDate })
            }, JsonRequestBehavior.AllowGet);
        }

    }
}