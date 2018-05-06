using FleetOps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FleetOps.App_Start;
using System.Collections;
using CCMS.ModelSector;
using ModelSector;
using FleetOps.ViewModel;
using System.Threading.Tasks;
using FleetSys.Models;
using CardTrend.Common.Extensions;

namespace FleetSys.Controllers
{
    [Authorize(Roles = "Internal")]
    public class DealerController : BaseController
    {
        // GET: Dealer
        //[CompressFilter]
        [AccessibilityXtra]
        public ActionResult Tmpl(string prefix)
        {
            switch (prefix)
            {
                case "gen":
                    return PartialView(this.getPartialPath("Dealer", "Merch_BusnLocation_Partial"), new MerchantDetails());
                case "con":
                    return PartialView(this.getPartialPath("Applications", "Contacts_Partial"), new ContactLstModel());
                case "add":
                    return PartialView(this.getPartialPath("Applications", "Address_Partial"), new AddrListMaintModel());
                case "sts":
                    return PartialView(this.getPartialPath("Accounts", "StatusMaint_Partial"), new ChangeStatus());
                case "ter":
                    return PartialView(this.getPartialPath("Dealer", "Merch_TerminalInventoryList_Partial"), new BusnLocTerminal());
                case "prs":
                    return PartialView(this.getPartialPath("Dealer", "ProductPrize_Partial"), new MerchProductPrize());
                case "moc":
                    return PartialView(this.getPartialPath("Dealer", "Merch_OwnershipChange_Partial"), new MerchChangeOwnership());
                default:
                    return PartialView();
            }
        }
        [Accessibility]
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> FillData(string prefix, string id)
        {
            switch (prefix)
            {
                case "gen":
                    var data = (await MechSignUpService.GetBusinessLocationGeneralInfoDetail(id)).merchantDetail;
                    var _dealerGenInfo = new MerchantDetails
                    {
                        Ownership = await BaseService.GetRefLib("MerchOwnership"),
                        SIC = await BaseService.GetMerchType("S"),
                        DBARegion = await BaseService.GetRefLib("RegionCd"),
                        DBACity = await BaseService.GetRefLib("City"),
                        DBAState = await BaseService.WebGetState(null),
                        BankAcctType = await BaseService.GetRefLib("BankAcctType"),
                        BankBranchCode = await BaseService.GetRefLib("BranchCd"),
                        cycNo = await BaseService.GetCycle("A"),
                        CurrentStatus = await BaseService.GetRefLib("MerchAcctSts"),
                        ReasonCd = await BaseService.GetRefLib("MerchReasonCd"),
                        MCC = await BaseService.GetMerchType("MerchType"),
                        BankAcctName = await BaseService.GetRefLib("Bank"),
                        AreaCodes = await BaseService.GetRefLib("AreaCd")
                    };
                    return Json(new { Selects = _dealerGenInfo, Model = data }, JsonRequestBehavior.AllowGet);

                case "ter":
                    var term = new BusnLocTerminal
                    {
                        Status = await BaseService.GetRefLib("TermSts"),
                        ProdType = await BaseService.GetDeviceModel(),
                        TermType = await BaseService.GetRefLib("TermType"),
                        ReasonCd = await BaseService.GetRefLib("TermReasonCd"),
                        UserId = this.GetUserId,
                        CreationDate = NumberExtensions.DateConverter(System.DateTime.Now.ToString())
                    };
                    return Json(new { Selects = term, Model = new BusnLocTerminal() }, JsonRequestBehavior.AllowGet);

                case "con":
                    var otherContactList = new ContactLstModel
                    {
                        ContactType = await BaseService.GetRefLib("Contact"),
                        Occupation = await BaseService.GetRefLib("Occupation"),
                        Sts = await BaseService.GetRefLib("ContactSts"),

                    };
                    var model = new ContactLstModel
                    {
                        UserId = HttpContext.User.Identity.Name,
                        CreationDate = System.DateTime.Now.ToString()
                    };
                    return Json(new { Selects = otherContactList, Model = model }, JsonRequestBehavior.AllowGet);

                case "add":
                    var countries = await BaseService.GetRefLib("Country");
                    var AddrSelects = new AddrListMaintModel
                    {
                        addrtype = await BaseService.GetRefLib("Address"),
                        State = countries.Count() > 1 ? await BaseService.WebGetState(countries[1].Value) : null,//changes
                        Country = countries,
                        region = await BaseService.GetRefLib("RegionCd"),
                    };
                    var AddrModel = new AddrListMaintModel
                    {
                        UserId = this.GetUserId,
                        CreationDate = System.DateTime.Now.ToString(),
                        RefTo = "BUSN",
                        RefKey = Convert.ToString(id)
                    };
                    return Json(new { Selects = AddrSelects, Model = AddrModel }, JsonRequestBehavior.AllowGet);

                case "sts":
                    var StsDetails = (await CardHolderService.GetChangedAcctStsDetail(id.ToString(), "BUSN")).changeStatus;
                    var selecs = new ChangeStatus
                    {
                        CurrentStatus = await BaseService.GetRefLib("MerchAcctSts"),
                        RefType = await BaseService.GetRefLib("EventType"),
                        ReasonCode = await BaseService.GetRefLib("MerchReasonCd", ""),
                        ChangeStatusTo = await BaseService.GetRefLib("MerchAcctSts")
                    };
                    return Json(new { Model = StsDetails, Selects = selecs }, JsonRequestBehavior.AllowGet);

                case "prs":
                    var _Prz = new MerchProductPrize
                    {
                        ProdCd = await BaseService.WebGetProduct(null, false)
                    };
                    return Json(new { Selects = _Prz, Model = new MerchProductPrize() }, JsonRequestBehavior.AllowGet);

                case "own":
                    var _OwnerShip = new MerchChangeOwnership
                    {
                        BankAcctType = await BaseService.GetRefLib("BankAcctType"),
                        BankName = await BaseService.GetRefLib("Bank"),
                        DBAState = await BaseService.WebGetState(null)
                    };
                    var ownershipInfo = (await MechSignUpService.GetMerchChgOwnership(id)).merchChangeOwnership;
                    return Json(new { Selects = _OwnerShip, Model = ownershipInfo }, JsonRequestBehavior.AllowGet);
                default:
                    HttpContext.Response.StatusCode = 404;
                    return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        #region "Dealer General Info"
        [CompressFilter]
        public async Task<ActionResult> ftBusinessLocationList(jQueryDataTableParamModel Params, string AcctNo)//Dealers Table
        {
            var _filtered = new List<MerchantDetails>();
            var list = (await MechSignUpService.GetBusinessLocationList(AcctNo)).merchantDetails;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.BusnLoc) ? p.BusnLoc : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SiteId) ? p.SiteId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.BusinessName) ? p.BusinessName : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.DBAName) ? p.DBAName : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.PayeeName) ? p.PayeeName : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CoRegNo) ? p.CoRegNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectDBAState) ? p.SelectDBAState : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.PersonInCharge) ? p.PersonInCharge : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.selectedCurrentStatus) ? p.selectedCurrentStatus : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] {x.BusnLoc,x.SiteId,x.BusinessName,x.DBAName,x.PayeeName,x.CoRegNo,x.SelectDBAState,x.PersonInCharge,x.selectedCurrentStatus,
                    x.CreationDate })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ftGetBusinessLocationGeneralInfoDetail(string acctNo, string BusnLocation)//SelectDealersGeneralInfo
        {
            var data = (await MechSignUpService.GetBusinessLocationGeneralInfoDetail(BusnLocation)).merchantDetail;
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SaveBusnLocationGeneralInfo(MerchantDetails merch)//SaveDealersGeneralInfo
        {
            merch.UserID = GetUserId;
            var data = await MechSignUpService.SaveBusnLocationGeneralInfo(merch);
            return Json(new { result = data, BusnLoc = data.returnValue.BusnLocation, EntityId = data.returnValue.EntityId }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Terminal List"
        [CompressFilter]
        public async Task<ActionResult> ftBusnLocTermList(jQueryDataTableParamModel Params, string BusnLoc)//DealerTerminalList
        {
            var _filtered = new List<BusnLocTerminal>();
            var list = (await MechSignUpService.GetBusnLocTerms(BusnLoc)).busnLocTerminals;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.TermId) ? p.TermId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedStatus) ? p.SelectedStatus : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.DeployDate) ? p.DeployDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Replacement) ? p.Replacement : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ReplacedDate) ? p.ReplacedDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedReasonCode) ? p.SelectedReasonCode : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SettlementStart) ? p.SettlementStart : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SettlementEnd) ? p.SettlementEnd : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SettleTxnId.ToString()) ? p.SettleTxnId.ToString() : string.Empty).Contains(Params.sSearch)).ToList();

                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count,
                iTotalDisplayRecords = list.Count,
                aaData = _filtered.Select(x => new object[] { x.TermId, x.SelectedStatus, x.DeployDate, x.SelectedTermType, x.SettlementStart, x.SettlementEnd, x.SettleTxnId })//, x.Replacement, x.ReplacedDate, x.SelectedReasonCode

            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ftBusnLocTermDetail(string TermId, string BusnLocation)//DealerTerminalSelect
        {
            var data = (await MechSignUpService.GetBusnLocTermDetail(TermId, BusnLocation)).busnLocTerminal;
            return Json(new { term = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SaveTermInventory(BusnLocTerminal _BusnLocTerminal)//DealerSaveTerminal
        {
            var _saveTermInv = await MechSignUpService.SaveBusnLocTerm(_BusnLocTerminal);
            return Json(new { resultCd = _saveTermInv }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Product Price Search"
        [CompressFilter]
        public async Task<JsonResult> WebMerchProductPriceSearch(jQueryDataTableParamModel Params, MerchProductPrize _Price, bool isList = true)//dealersProductPriceSearch
        {
            var _filtered = new List<MerchProductPrize>();
            var list = (await MechSignUpService.GetMerchProductPriceSearch(_Price, isList)).merchProductPrizes;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.EffDateFrom) ? p.EffDateFrom : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.EffDateTo) ? p.EffDateTo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Descp) ? p.Descp : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Price) ? p.Price : string.Empty).Contains(Params.sSearch) ||
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
                iTotalRecords = _filtered.Count(),
                iTotalDisplayRecords = _filtered.Count(),
                aaData = _filtered.Select(x => new object[] { x.EffDateFrom, x.EffDateTo, x.Descp, x.Price, x.CreationDate })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Change ownership"
        [HttpPost]
        public async Task<ActionResult> WebMerchChgOwnershipMaint(MerchChangeOwnership merch)//SaveDealersGeneralInfo
        {
            var data = await MechSignUpService.SaveMerchChgOwnershipMaint(merch, GetUserId);
            return Json(new { result = data });
        }
        [CompressFilter]
        public async Task<ActionResult> WebMerchOwnershipChangeListSelect(jQueryDataTableParamModel Params, string DealerId)//Dealers Table
        {
            var _filtered = new List<MerchChangeOwnership>();
            var list = (await MechSignUpService.GetMerchOwnershipChanges(DealerId)).merchChangeOwnerships;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CutoffDate) ? p.CutoffDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.FromMerchantId) ? p.FromMerchantId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ToMerchantId) ? p.ToMerchantId : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] {x.FromMerchantId,x.CutoffDate,x.ToMerchantId,x.FloatAcctInd,x.CreationDate})
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}