using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilities.DAL;
using FleetOps.Models;
using FleetOps.App_Start;
using CCMS.ModelSector;
using FleetOps.ViewModel;
using ModelSector;
using System.Threading.Tasks;
using FleetSys.Models;
using System.Globalization;
using FleetSys.Controllers;
using AutoMapper;
using CardTrend.Domain.Dto.Merchant;
using CardTrend.Domain.Dto.CardHolder;
using CardTrend.Domain.Dto.Account;
using CardTrend.Common.Extensions;
using FleetSys.Common;

namespace FleetOps.Controllers
{
    [Authorize(Roles = "Internal")]
    //[CustomLoad]
    public class MerchantController : BaseController
    {
        // GET: /MerchantSignUp/
        //[Accessibility]
        [Accessibility]
        public ActionResult Index()
        {

            return View();
        }
        [Accessibility]
        public async Task<ViewResult> New()
        {
            var viewModel = new MerchantAcctSignUpViewModel
            {
                _AccountDetails = new MA_GeneralInfo
                {
                    AffiliatedWithCorpCode = await BaseService.GetCAOReasonCd(),
                    BankAcctType = await BaseService.GetRefLib("BankAcctType"),
                    CurrentStatus = await BaseService.GetRefLib("MerchAcctSts"),
                    BankBranchCd = await BaseService.GetRefLib("BranchCd"),
                    ReasonCd = await BaseService.GetRefLib("MerchReasonCd"),
                    CycleNo = await BaseService.GetCycle("A"),
                    BusnModel = await BaseService.GetRefLib("BusnModel")
                }
            };
            return View(viewModel);
        }

        public ActionResult SmartPayFrame(string Id = "100000041079501")
        {
            ViewBag.Id = Id;
            return View(new eService());
        }

        public async Task<JsonResult> iFrameMerchGeneralInfoSelect(string MerchId)
        {
            var date = new DateTime();
            var _listItem = new List<SelectListItem>();
            for (var i = 0; i < 10; i++)
            {
                date = DateTime.Now.AddMonths(-i);
                _listItem.Add(new SelectListItem
                {
                    Text = date.ToString("MMMM") + " " + date.ToString("yyyy"),
                    Value = date.ToString("yyyy") + date.ToString("MM")
                });
            }
            var Info = new eService
            {
                // double check
                TxnType = await BaseService.GetFrameGetTxnCategory(""),
                TxnDateRange = _listItem
            };
            var info = (await MechSignUpService.GetIFrameMerchGeneralInfo(MerchId)).eService;
            return Json(new { Model = info, Selects = Info }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> iFrameMerchTxnListSelect(jQueryDataTableParamModel Params, eService _Query)
        {
            var dt = DateTime.ParseExact(_Query.SelectedTxnDateRange, "yyyyMM", CultureInfo.InvariantCulture);
            _Query.Month = Convert.ToInt16(dt.ToString("MM"));
            _Query.Year = Convert.ToInt16(dt.ToString("yyyy"));

            var list = (await MechSignUpService.GetIFrameMerchGeneralInfoes(_Query.BusnLocation, Convert.ToInt32(_Query.SelectedTxnType), _Query.Month, _Query.Year)).eServices;
            var filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength);

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = filtered.Select(x => new object[] { x.PostingDate, x.TxnDate, x.TxnTime, x.CardNo, x.RRN, x.Quantity, x.Amount, x.MDR, x.VatAmount, x.NetAmount })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> DownloadExcelReport(eService _Query)
        {
            string Extension = ".xlsx";
            var dt = DateTime.ParseExact(_Query.SelectedTxnDateRange, "yyyyMM", CultureInfo.InvariantCulture);
            _Query.Month = Convert.ToInt16(dt.ToString("MM"));
            _Query.Year = Convert.ToInt16(dt.ToString("yyyy"));
            int colIndex = 1, rowIndex = 4;
            var list = (await MechSignUpService.GetIFrameMerchGeneralInfoes(_Query.BusnLocation, Convert.ToInt32(_Query.SelectedTxnType), _Query.Month, _Query.Year)).eServices;
            var headerName = string.Format("{0}_{1}", _Query.SelectedTxnType, _Query.SelectedTxnDateRange);
            headerName = headerName.Trim();
            var pkg = CommonHelpers.PrepareExcelHeader(headerName, new string[] { "Posting Date", "Txn Date", "Card No", "RRN", "Quantity", "Amount", "MDR", "VAT Amount", "Net Amount" });
            var ws = pkg.Workbook.Worksheets[1];
            var cell = ws.Cells[rowIndex, colIndex];
            foreach (var x in list)
            {
                colIndex = 1;
                cell = ws.Cells[rowIndex, colIndex];
                cell.Value = x.PostingDate;
                colIndex++;
                cell = ws.Cells[rowIndex, colIndex];
                cell.Value = x.TxnDate;
                colIndex++;
                cell = ws.Cells[rowIndex, colIndex];
                cell.Value = x.CardNo;
                colIndex++;
                cell = ws.Cells[rowIndex, colIndex];
                cell.Value = x.RRN;
                colIndex++;
                cell = ws.Cells[rowIndex, colIndex];
                cell.Value = x.Quantity;
                colIndex++;
                cell = ws.Cells[rowIndex, colIndex];
                cell.Value = x.Amount;
                colIndex++;
                cell = ws.Cells[rowIndex, colIndex];
                cell.Value = x.MDR;
                colIndex++;
                cell = ws.Cells[rowIndex, colIndex];
                cell.Value = x.VatAmount;
                colIndex++;
                cell = ws.Cells[rowIndex, colIndex];
                cell.Value = x.NetAmount;
                rowIndex++;
            }
            string contentType = CommonHelpers.ContentType(Extension);
            Byte[] bin = pkg.GetAsByteArray();
            return File(bin, contentType, headerName + Extension);
        }
        #region"Select Info"
        [Accessibility]
        public async Task<ViewResult> Select(string Acctno)//, int SrcFrom
        {
            var viewModel = new MerchantAcctSignUpViewModel()
            {
                _AccountDetails = (await MechSignUpService.GetMAGeneralInfoDetail(Acctno)).merchGeneralInfo//, SrcFrom
            };
            ViewBag.AcctNo = Acctno;
            return View(viewModel);
        }
        [AccessibilityXtra]
        //[Accessibility]
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                case "gen":
                    return PartialView(this.getPartialPath("MerchGeneralInfo", "MerchAgrmntList_Partial"), new MA_GeneralInfo());
                case "bus":
                    return PartialView(this.getPartialPath("MerchGeneralInfo", "MerchBusnLocation_Partial"), new MerchantDetails());
                case "con":
                    return PartialView(this.getPartialPath("MerchGeneralInfo",  "MerchContactsListMaint_Partial"), new ContactLstModel());
                case "add":
                    return PartialView(this.getPartialPath("MerchGeneralInfo", "MerchAddressListMaint_Partial"), new AddrListMaintModel());
                case "BusnTerm":
                    return PartialView(this.getPartialPath("MerchatSignUp", "MerchBusnLocation_Partial"), new BusnLocTerminal());
                case "mps":
                    return PartialView(this.getPartialPath("MerchGeneralInfo", "MerchPostedTxnSearch_Partial"), new MerchPostedTxnSearch());
                case "sts":
                    return PartialView(this.getPartialPath("MerchantSignUp", "Merch_StatusMaint"));
                default:
                    return PartialView();
            }
        }

        public async Task<ActionResult> FillData(string prefix, string AcctNo)
        {
            switch (prefix)
            {
                case "gen":
                    var MerchGenInfo = (await MechSignUpService.GetMAGeneralInfoDetail(AcctNo)).merchGeneralInfo;
                    var _MAGeneralInfo = new MA_GeneralInfo
                    {
                        AffiliatedWithCorpCode = await BaseService.WebGetCorpCd(true),
                        BankAcctType = await BaseService.GetRefLib("BankAcctType"),
                        BankBranchCd = await BaseService.GetRefLib("BranchCd"),
                        CurrentStatus = await BaseService.GetRefLib("MerchAcctSts"),
                        CycleNo = await BaseService.GetCycle("A"),
                        ReasonCd = await BaseService.GetRefLib("MerchReasonCd"),
                        BusnModel = await BaseService.GetRefLib("BusnModel"),
                        BusnSize = await BaseService.GetRefLib("BusnSize"),
                        Ownership = await BaseService.GetRefLib("MerchOwnership"),
                        BankName = await BaseService.GetRefLib("Bank"),
                    };
                    return Json(new { Selects = _MAGeneralInfo, Model = MerchGenInfo }, JsonRequestBehavior.AllowGet);

                case "mai":

                case "bus":
                    var _MerchantDetails = new MerchantDetails
                    {
                        DBACity = await BaseService.GetRefLib("city"),
                        DBARegion = await BaseService.GetRefLib("RegionCd"),
                        DBAState = await BaseService.WebGetState(null),
                        BankAcctType = await BaseService.GetRefLib("BankAcctType"),
                        CurrentStatus = await BaseService.GetRefLib("MerchAcctSts"),
                        Ownership = await BaseService.GetRefLib("MerchOwnership"),
                        SIC = await BaseService.GetMerchType("S"),
                    };
                    return Json(new { Selects = _MerchantDetails, Model = new MerchantDetails() }, JsonRequestBehavior.AllowGet);

                case "car":
                    return PartialView(this.getPartialPath("MerchGeneralInfo", "CAMaint_CardRange_Partial"));


                case "agr":
                    var _MA_GeneralInfo = new MA_GeneralInfo
                    {
                        AffiliatedWithCorpCode = await BaseService.WebGetCorpCd(true),
                        BankAcctType = await BaseService.GetRefLib("BankAcctType"),
                        BankBranchCd = await BaseService.GetRefLib("BranchCd"),
                        BusnEst = await BaseService.GetRefLib("BusnEst"),
                        CurrentStatus = await BaseService.GetRefLib("MerchAcctSts"),
                        ReasonCd = await BaseService.GetRefLib("MerchReasonCd"),
                        BusnModel = await BaseService.GetRefLib("BusnModel")

                    };
                    return Json(new { Selects = _MA_GeneralInfo, Model = new MA_GeneralInfo() }, JsonRequestBehavior.AllowGet);

                case "BusnTerm":
                    var _BusnLocTerminal = new BusnLocTerminal
                    {
                        Status = await BaseService.GetRefLib("MerchAcctSts"),
                        ProdType = await BaseService.GetRefLib("ProdType"),
                        ReasonCd = await BaseService.GetRefLib("TermReasonCd"),
                        UserId = this.GetUserId,
                        CreationDate = NumberExtensions.DateConverter(System.DateTime.Now.ToString())
                    };
                    return Json(new { Selects = _BusnLocTerminal, Model = new BusnLocTerminal() }, JsonRequestBehavior.AllowGet);
                case "mps":
                    var _merchPstTxnSearch = new MerchPostedTxnSearch
                    {
                        Dealer = await BaseService.WebGetDealerByMerch(AcctNo),
                        TxnCd = await BaseService.WebGetTxnCode("A"),                       
                    };
                    return Json(new { Selects = _merchPstTxnSearch, Model = new MerchPostedTxnSearch() }, JsonRequestBehavior.AllowGet);
                case "evt":
                    var logger = new EventLogger
                    {
                        EventType = await BaseService.GetRefLib("EventType"),
                        ReasonCd = await BaseService.GetRefLib("MerchReasonCd")
                    };
                    return Json(new { Selects = logger, Model = new EventLogger() }, JsonRequestBehavior.AllowGet);
                case "sts":
                    var stsDetails = await CardHolderService.GetChangedAcctStsDetail(AcctNo.ToString(), "MERCH");
                    var sts = new ChangeStatus
                    {
                        CurrentStatus = await BaseService.GetRefLib("MerchAcctSts"),
                        RefType = await BaseService.GetRefLib("EventType"),
                        ReasonCode = await BaseService.GetRefLib("MerchReasonCd", ""),
                        ChangeStatusTo = await BaseService.GetRefLib("MerchAcctSts")
                    };
                    return Json(new { Selects = sts, Model = stsDetails.changeStatus }, JsonRequestBehavior.AllowGet);

                case "mpp":
                    var model = new MerchProductPrize
                    {
                        ProdCd = await BaseService.WebGetProduct(null, false)
                    };
                    return Json(new { Selects = model, Model = new MerchProductPrize() }, JsonRequestBehavior.AllowGet);
                default:
                    return PartialView();
            }
        }
        #endregion

        #region "Fetch Table List & Details | Save & Delete Info "
        [HttpPost]
        public async Task<ActionResult> SaveMerchGeneralInfo(MA_GeneralInfo _MerchGeneralInfo, string Func)//SaveMerchantGeneralInfo
        {
            var _generalInfo = await MechSignUpService.SaveMAGeneralInfo(_MerchGeneralInfo, Func);
            return Json(new { resultCd = _generalInfo, AcctNo = _generalInfo.returnValue.BatchId, EntityId = _generalInfo.returnValue.RetCd }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ftMechGeneralInfoDetail(string Acctno)//SelectMerchantGeneralInfo , Int16 SrcFrom
        {
            var data = (await MechSignUpService.GetMAGeneralInfoDetail(Acctno)).merchGeneralInfo;
            return Json(new { CardHolder = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SaveChangedStatus(ChangeStatus _ChangeStatus, CardnAccNo _CardnAcctNo)
        {
            _ChangeStatus._CardnAccNo = _CardnAcctNo;
            var _SaveChangedStatus = await CardHolderService.StatusSave(_ChangeStatus, GetUserId);
            return Json(new { resultCd = _SaveChangedStatus }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public async Task<ActionResult> ftMechAggreementList(jQueryDataTableParamModel Params)//MerchantTableList
        {
            var _filtered = new List<MA_GeneralInfo>();
            var list = (await MechSignUpService.GetMerchAgreementList()).merchAgreements;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.AcctNo) ? p.AcctNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.PIC) ? p.PIC : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SAPNo) ? p.SAPNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.BusnName) ? p.BusnName : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedAffiliatedWithCorpCode) ? p.SelectedAffiliatedWithCorpCode : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.WithholdingTaxInd) ? p.WithholdingTaxInd : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.WithholdingTaxRate) ? p.WithholdingTaxRate : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TaxId) ? p.TaxId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedCurrentStatus) ? p.SelectedCurrentStatus : string.Empty).ToLower().Contains(Params.sSearch)).ToList();


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
                aaData = _filtered.Select(x => new object[] { x.AcctNo, x.PIC, x.SAPNo, x.BusnName, x.SelectedAffiliatedWithCorpCode, x.TaxId, x.SelectedCurrentStatus, x.CreationDate })
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ftMechAggreementDetail(string AcctNo)
        {
            var data = (await MechSignUpService.GetMAGeneralInfoDetail(AcctNo)).merchGeneralInfo;
            return Json(new { CardHolder = data }, JsonRequestBehavior.AllowGet);
        }

        [CompressFilter]
        public async Task<ActionResult> ftGetMerchtPostedTxnSearchList(jQueryDataTableParamModel Params, MerchPostedTxnSearch _MerchPostedTxnSearch)//MerchantTxnSearch
        {
            var _filtered = new List<MerchPostedTxnSearch>();
            var list = (await MechSignUpService.GetMerchtPostedTxnSearch(_MerchPostedTxnSearch.AcctNo, _MerchPostedTxnSearch.SelectedDealer, _MerchPostedTxnSearch.SelectedTxnCd, _MerchPostedTxnSearch.TxnDate)).merchPostedTxnSearches;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.SelectedDealer) ? p.SelectedDealer : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TermBatch) ? p.TermBatch : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnDate) ? p.TxnDate : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.cardNo) ? p.cardNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnDesp) ? p.TxnDesp : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnAmt) ? p.TxnAmt : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TermId) ? p.TermId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.AuthNo) ? p.AuthNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.AuthCardNo) ? p.AuthCardNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnId) ? p.TxnId : string.Empty).ToLower().Contains(Params.sSearch)).ToList();


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
                aaData = list.Select(x => new object[] { x.SelectedDealer, x.TermBatch, x.TxnDate, x.cardNo, x.TxnDesp, x.TxnAmt, x.TermId, x.AuthNo, x.AuthCardNo, x.PrcsDate, x.TxnId })
            }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public async Task<ActionResult> MerchftEventLoggerList(jQueryDataTableParamModel Params, string AccNo)
        {
            // check value when passing @BusnLocation
            var _filtered = new List<EventLogger>();
            var list = (await AccountOpService.GetEventlist("A", AccNo, null)).eventLoggers;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.EventId) ? p.EventId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedEventType) ? p.SelectedEventType : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.RefKey) ? p.RefKey : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Description) ? p.Description : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ReminderDatetime) ? p.ReminderDatetime : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ClosedDate) ? p.ClosedDate : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).ToLower().Contains(Params.sSearch)).ToList();


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
                aaData = _filtered.Select(x => new object[] { x.EventId, x.SelectedEventType, x.RefKey, x.Description, x.ReminderDatetime, x.ClosedDate, x.UserId, x.CreationDate })
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> MerchSaveEventLogger(EventLogger _Logger, string module)
        {
            _Logger.UserId = GetUserId;
            var _saveEventLogger = await MechSignUpService.SaveEventMaint(_Logger,module);
            return Json(new { result = _saveEventLogger }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> MerchGetEventDetail(EventLogger _Logger, string module, string eventID)
        {
            var data = (await AccountOpService.GetEventLoggerDetail("A", eventID)).eventLogger;
            return Json(new { eventDetail = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}