using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.Fraud;
using CCMS.ModelSector;
using FleetOps.App_Start;
using FleetOps.Models;
using FleetSys.Common;
using FleetSys.Models;
using FleetSys.ViewModel;
using ModelSector.Fraud;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using En = FleetSys.Common.Enums;

namespace FleetSys.Controllers
{
    [Authorize(Roles = "Internal")]
    public class FraudController : BaseController
    {
        [Accessibility]
        public ActionResult Index()
        {

            if (Session["UserModules"] == null)
            {
                var objUserAccessOps = new UserAccessOps();
                Session["UserModules"] = objUserAccessOps.UserIndexAccess();
            }
            return View(new FraudCaseListViewModel());
        }
        [AccessibilityXtra(Order = 2)]
        [AccessibilitySection(Order = 1)]
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                case "fci":
                    return PartialView(this.getPartialPath("Fraud", "Fraud_Incident_Partial"), new FraudMainViewModel());

                case "txn":
                    return PartialView(this.getPartialPath("Fraud", "Fraud_Transaction_Partial"), new FraudTxnDisputeViewModel());

                case "prt":
                    return PartialView(this.getPartialPath("Fraud", "Fraud_Print_Partial"), new FraudMainViewModel());

                case "fil":
                    return PartialView(this.getPartialPath("Fraud", "Fraud_FileManager_Partial"));
                default:
                    return PartialView();
            }
        }
        public async Task<JsonResult> FillData(string Prefix, string EventId)
        {
            try
            {
                switch (Prefix.ToLower())
                {
                    case "details":
                        string Enum_NatureOfIncident = Convert.ToString(En.NatureOfIncident.O);

                        //load/bind dropdown
                        var DropdownFraudIncidentsViewModel = new FraudIncidentsViewModel
                        {
                            ReportedVia = await BaseService.GetRefLib("ReportVia"),
                            NextStatus = await BaseService.GetRefLib("FraudSts"),
                            NatureOfIncident = await BaseService.GetRefLib("NtfIncidentType"),
                            InvestigatedBy = await FraudOpService.GetFraudDropdown((int)En.FraudSection.IncidentInvestigationTeam, "Y", null, null, null)
                        };

                        var fraudMainViewModel = (await FraudOpService.GetFraudByEventId(EventId)).webFraudDetail;

                        //get customer sale list
                        var customerDetailsShowList = (await FraudOpService.FraudCustomerDetailsByacctNoEventId(fraudMainViewModel.FraudCustomerDetailsViewModel.AcctNo, EventId)).fraudCustomerDetails;
                        var cardNoLst = (await FraudOpService.GetCardNoListByAcctNo((int)En.FraudSection.CardDetails, "Y", null, fraudMainViewModel.FraudCustomerDetailsViewModel.AcctNo, EventId)).fraudCard;
                        var cardDetailsShowList = (await FraudOpService.GetFraudCardDetailsList(null, fraudMainViewModel.FraudCustomerDetailsViewModel.AcctNo, EventId, cardNoLst)).fraudCardDetails;

                        var CardMonth1Date = cardDetailsShowList.Where(s => !string.IsNullOrEmpty(s.CardMonth1Date)).Distinct().Select(s => s.CardMonth1Date).FirstOrDefault();
                        var CardMonth2Date = cardDetailsShowList.Where(s => !string.IsNullOrEmpty(s.CardMonth2Date)).Distinct().Select(s => s.CardMonth2Date).FirstOrDefault();
                        var CardMonth3Date = cardDetailsShowList.Where(s => !string.IsNullOrEmpty(s.CardMonth3Date)).Distinct().Select(s => s.CardMonth3Date).FirstOrDefault();
                        var CardMonth4Date = cardDetailsShowList.Where(s => !string.IsNullOrEmpty(s.CardMonth4Date)).Distinct().Select(s => s.CardMonth4Date).FirstOrDefault();
                        var CardMonth5Date = cardDetailsShowList.Where(s => !string.IsNullOrEmpty(s.CardMonth5Date)).Distinct().Select(s => s.CardMonth5Date).FirstOrDefault();
                        var CardMonth6Date = cardDetailsShowList.Where(s => !string.IsNullOrEmpty(s.CardMonth6Date)).Distinct().Select(s => s.CardMonth6Date).FirstOrDefault();
                        var CardAvgSalesDisplay = cardDetailsShowList.Where(s => !string.IsNullOrEmpty(s.CardAvgSalesDisplay)).Distinct().Select(s => s.CardAvgSalesDisplay).FirstOrDefault();

                        return Json(new
                        {
                            _FraudIncidentsViewModel = fraudMainViewModel.FraudIncidentsViewModel,
                            _FraudCustomerDetailsViewModel = fraudMainViewModel.FraudCustomerDetailsViewModel,
                            _FraudCardDetailsViewModel = fraudMainViewModel.FraudCardDetailsViewModel,
                            Selects = DropdownFraudIncidentsViewModel,
                            CardNo = cardNoLst,
                            model = customerDetailsShowList.Select(x => new object[] {  x.CmpyName1, x.AccountType,x.ClientType, x.AgeingDays, 
                            x.Month1Date, x.Month2Date, x.Month3Date, x.Month4Date, x.Month5Date, x.Month6Date,x.AvgSalesDisplay, x.SubsidyNo}).FirstOrDefault(),
                            aaData = customerDetailsShowList.Select(x => new object[] { x.Month1Amt, x.Month2Amt, x.Month3Amt, x.Month4Amt, x.Month5Amt, x.Month6Amt, x.AvgSales }),
                            cardModel = new object[] { CardMonth1Date, CardMonth2Date, CardMonth3Date, CardMonth4Date, CardMonth5Date, CardMonth6Date, CardAvgSalesDisplay },
                            cardaaData = cardDetailsShowList.Select(x => new object[] {x.FraudCards[0].SelectedCardNo, x.CardMonth1Amt, x.CardMonth2Amt, x.CardMonth3Amt, x.CardMonth4Amt, x.CardMonth5Amt, x.CardMonth6Amt, x.CardAvgSales,x.LitLimit, x.SingleTxn, x.DailyTxn, x.MonthlyTxn,x.DailyCnt,x.MonthlyCnt,x.DailyLitre,x.MonthlyLitre }),
                            liFraudCards = cardDetailsShowList.SelectMany(x => x.FraudCards),
                            Enum_NatureOfIncident = Enum_NatureOfIncident
                        }, JsonRequestBehavior.AllowGet);

                    case "txn":
                        var _FraudTxnDisputeViewModel = new FraudTxnDisputeViewModel
                        {
                            TxnCategory = await BaseService.GetTransactionCategory(),
                            TxnCd = await BaseService.WebGetTxnCode("I")
                        };

                        var json = Json(new { Selects = _FraudTxnDisputeViewModel }, JsonRequestBehavior.AllowGet);
                        json.MaxJsonLength = int.MaxValue;
                        return json;


                    default:
                        HttpContext.Response.StatusCode = 404;
                        return Json(null, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 404;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        #region Fraud Case List
        [CompressFilter]
        public async Task<ActionResult> FTGetFraudCaseList(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<FraudCaseListViewModel>();
            var list = (await FraudOpService.GetFraudCases()).fraudCases;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.EventId.ToString()) ? p.EventId.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.RefKey) ? p.RefKey : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.e_Descp) ? p.e_Descp : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CloseDate) ? p.CloseDate : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.LastUpdDate) ? p.LastUpdDate : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreatedBy) ? p.CreatedBy : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.et_Descp) ? p.et_Descp : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CmpyName1) ? p.CmpyName1 : string.Empty).Contains(Params.sSearch)).ToList();



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

                aaData = _filtered.Select(x => new object[] { x.EventId, x.et_Descp, x.RefKey, x.CmpyName1, x.e_Descp, x.CloseDate, x.UserId, x.LastUpdDate, x.CreatedBy, x.CreationDate })
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Fraud Customer Details
        public async Task<ActionResult> GetFraudCustomerDetailsList(string AcctNo, string EventId)
        {
            var customerDetailsShowList = (await FraudOpService.FraudCustomerDetailsByacctNoEventId(AcctNo, EventId)).fraudCustomerDetails;
            return Json(
            new
            {
                model = customerDetailsShowList.Select(x => new object[] {  x.CmpyName1, x.AccountType,x.ClientType, x.AgeingDays, 
                x.Month1Date, x.Month2Date, x.Month3Date, x.Month4Date, x.Month5Date, x.Month6Date,x.AvgSalesDisplay}).FirstOrDefault(),
                aaData = customerDetailsShowList.Select(x => new object[] { x.Month1Amt, x.Month2Amt, x.Month3Amt, x.Month4Amt, x.Month5Amt, x.Month6Amt, x.AvgSales })
            }
            , JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Fraud Card Details
        /// <summary>
        /// Get dropdown CardNo
        /// </summary>
        public async Task<JsonResult> GetCardNoList_ByAcctNo(string AcctNo, string EventId)
        {
            var cardList = (await FraudOpService.GetCardNoByAcctNo((int)En.FraudSection.CardDetails, "Y", null, AcctNo, EventId)).fraudCard;
            return Json(new { cardList = cardList }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> GetCardDetailsList_ByAcctNoCardNo(FraudCardDetailsViewModel _FraudCardDetailsViewModel)
        {
            try
            {
                FraudCards fraudCards = _FraudCardDetailsViewModel.FraudCards[0];
              
                var cardNoes = new List<string>();
                if(_FraudCardDetailsViewModel.FraudCards.Count() > 0)
                {
                    foreach (var item in _FraudCardDetailsViewModel.FraudCards)
                    {
                        cardNoes.Add(item.SelectedCardNo);
                    }
                }
                var listFraudIncident = (await FraudOpService.GetFraudCardDetailsList(cardNoes, _FraudCardDetailsViewModel.AcctNo, null, fraudCards)).fraudCardDetails;

                var CardMonth1Date = listFraudIncident.Where(s => !string.IsNullOrEmpty(s.CardMonth1Date)).Distinct().Select(s => s.CardMonth1Date).FirstOrDefault();
                var CardMonth2Date = listFraudIncident.Where(s => !string.IsNullOrEmpty(s.CardMonth2Date)).Distinct().Select(s => s.CardMonth2Date).FirstOrDefault();
                var CardMonth3Date = listFraudIncident.Where(s => !string.IsNullOrEmpty(s.CardMonth3Date)).Distinct().Select(s => s.CardMonth3Date).FirstOrDefault();
                var CardMonth4Date = listFraudIncident.Where(s => !string.IsNullOrEmpty(s.CardMonth4Date)).Distinct().Select(s => s.CardMonth4Date).FirstOrDefault();
                var CardMonth5Date = listFraudIncident.Where(s => !string.IsNullOrEmpty(s.CardMonth5Date)).Distinct().Select(s => s.CardMonth5Date).FirstOrDefault();
                var CardMonth6Date = listFraudIncident.Where(s => !string.IsNullOrEmpty(s.CardMonth6Date)).Distinct().Select(s => s.CardMonth6Date).FirstOrDefault();
                var CardAvgSalesDisplay = listFraudIncident.Where(s => !string.IsNullOrEmpty(s.CardAvgSalesDisplay)).Distinct().Select(s => s.CardAvgSalesDisplay).FirstOrDefault();

                return Json(
                new
                {
                    model = new object[] { CardMonth1Date, CardMonth2Date, CardMonth3Date, CardMonth4Date, CardMonth5Date, CardMonth6Date, CardAvgSalesDisplay },
                    aaData = listFraudIncident.Select(x => new object[] {x.FraudCards[0].SelectedCardNo, x.CardMonth1Amt, x.CardMonth2Amt, x.CardMonth3Amt, x.CardMonth4Amt, x.CardMonth5Amt, x.CardMonth6Amt, x.CardAvgSales, x.SingleTxn, x.DailyTxn, x.MonthlyTxn })
                }
                , JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion

        [HttpPost]
        public async Task<JsonResult> SaveFraud(FraudCustomerDetailsViewModel fraudCustomerDetailsViewModel, FraudCardDetailsViewModel fraudCardDetailsViewModel, FraudIncidentsViewModel fraudIncidentsViewModel)
        {
            var result = await FraudOpService.SaveFraud(fraudCustomerDetailsViewModel, fraudCardDetailsViewModel, fraudIncidentsViewModel, GetUserId);
            return Json(new { resultCd = result, _eventId = result.returnValue.BatchId });
        }

        #region Txn Dispute

        [CompressFilter]
        public async Task<ActionResult> GetFraudTxnSearch(jQueryDataTableParamModel Params, FraudTxnDisputeViewModel _FraudTxnDisputeViewModel, string IsPostedDispute)
        {
            var _filtered = new List<FraudTxnDisputeViewModel>();
            var list = (await FraudOpService.GetFraudDisputeTxnSearch(NumberExtensions.ConvertLongToDb(_FraudTxnDisputeViewModel.EventId), Convert.ToInt32(IsPostedDispute), _FraudTxnDisputeViewModel.SelectedTxnCategory, _FraudTxnDisputeViewModel.SelectedTxnCd, _FraudTxnDisputeViewModel.AcctNo, _FraudTxnDisputeViewModel.SelectedCardNo, _FraudTxnDisputeViewModel.FromDate, _FraudTxnDisputeViewModel.ToDate)).fraudTxnDisputes;
            _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count,
                iTotalDisplayRecords = list.Count,
                aaData = _filtered.Select(x => new object[] {null, x.TxnId, x.TxnDesp, x.TxnDate, x.AuthCardNo, 
                    x.VehRegNo, x.Stan, x.RRn, x.BSNLocation, x.BSNLocationName, x.TermId, x.TxnAmt })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveTxn(List<string> liTxnId, string EventId, string AcctNo, string CardNo, string IsPostedDispute)
        {
            var result = await FraudOpService.SaveTransaction(liTxnId, EventId, AcctNo, CardNo, IsPostedDispute);
            return Json(new { resultCd = result });
        }
        #endregion

    }
}