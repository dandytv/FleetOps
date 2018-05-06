using System.Web.Mvc;
using FleetOps.Models;
using FleetOps.ViewModel;
using ModelSector;
using Utilities.DAL;
using System.Linq;
using System.Collections;
using FleetOps.App_Start;
using CCMS.ModelSector;
using FleetSys.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using AutoMapper;
using CardTrend.Domain.Dto.CardHolder;
namespace FleetSys.Controllers
{
    [Authorize(Roles = "Internal")]
    public class CardController : BaseController
    {
        // GET: ApplicantHolder
        //private ApplicantHolderOps objCardHoldersOps = new ApplicantHolderOps();
        //private AccountOps objAcctOps = new AccountOps();
        [Accessibility]
        public ActionResult Index()
        {
            return View();
        }
        [AccessibilityXtra]
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                case "gen":
                    return PartialView(this.getPartialPath("Card", "Card_GeneralInfo_Partial"), new CardHolderInfoModel());
                case "fin":
                    return PartialView(this.getPartialPath("Card", "Card_FinancialInfo_Partial"), new CardFinancialInfoModel());
                case "vel":
                    return PartialView(this.getPartialPath("Card", "Card_VelocityLimitsListMaint_Partial"), new VeloctyLimitListMaintModel());
                case "per":
                    return PartialView(this.getPartialPath("Card", "CardPersonInfo_Partial"), new PersonInfoModel());
                case "loc":
                    return PartialView(this.getPartialPath("Card", "LocationAcceptance_Partial"), new LocationAcceptListModel());
                case "rep":
                    return PartialView(this.getPartialPath("Card", "CardReplacement_Partial"), new CardReplacement());
                case "pdc":
                    return PartialView(this.getPartialPath("Accounts", "ProductDiscount_Partial"), new ProductDiscount());
                default:
                    return PartialView();
            }
        }
        public async Task<JsonResult> FillData(string Prefix, string CardNo)//,string acctNo
        {
            switch (Prefix)
            {
                case "gen":
                    var data = (await CardHolderService.GetGeneralInfo(CardNo)).cardHolderInfo;
                    var _cardAppcInfoSignUp = new CardHolderInfoModel
                     {
                         CardType = await BaseService.GetCardType(null, null, null, Request.QueryString["AcctNo"]),
                         CostCentre = await BaseService.GetCostCentre(Request.QueryString["AcctNo"], "ACCT",true),
                         ReasonCd = await BaseService.GetRefLib("ReasonCd"),
                         SKDSNo = await BaseService.GetSKDS(null, Request.QueryString["AcctNo"]),
                         RenewalInd = await BaseService.GetRefLib("RenewalInd"),
                         DialogueInd = await BaseService.GetRefLib("DialogueInd"),
                         PINInd = await BaseService.GetRefLib("PinInd"),
                         ProductUtilization = await BaseService.WebProductGroupSelect(),
                         VehicleModel = await BaseService.GetRefLib("VehSubModel"),
                         AnnualFee = await BaseService.GetFeeCd("ANN",true),
                         JonFee = await BaseService.GetFeeCd("JON",true),
                         BranchCd = await BaseService.GetRefLib("BranchCd"),
                         DivisionCode = await BaseService.GetRefLib("DivisionCd"),
                         DeptCd = await BaseService.GetRefLib("DeptCd"),
                         CardMedia = await BaseService.GetCardMedia()
                     };

                    ViewBag.AcctNo = Request.QueryString["AcctNo"];
                    return Json(new { Model = data, Selects = _cardAppcInfoSignUp }, JsonRequestBehavior.AllowGet);
                case "fin":
                    var fin = (await CardHolderService.GetFinancialInfo(CardNo)).cardFinancialInfoModel;
                    return Json(new { Model = fin }, JsonRequestBehavior.AllowGet);
                case "per":
                    var per = (await CardHolderService.GetPersonInfo(Request.QueryString["EntityId"])).personalInfo;
                    var Selects = new PersonInfoModel
                    {
                        title = await BaseService.GetRefLib("Title"),
                        IdType = await BaseService.GetRefLib("IcType"),
                        AltIdType = await BaseService.GetRefLib("IcType"),
                        gender = await BaseService.GetRefLib("Gender"),
                        Occupation = await BaseService.GetRefLib("Occupation"),
                        DeptId = await BaseService.GetRefLib("Dept")
                    };
                    return Json(new { Model = per, Selects = Selects }, JsonRequestBehavior.AllowGet);
                case "sts":
                    var stsDetails = await CardHolderService.GetChangedAcctStsDetail(CardNo, "CARD");
                    var selecs = new ChangeStatus
                    {
                        CurrentStatus = await BaseService.GetRefLib("CardSts"),
                        RefType = await BaseService.GetRefLib("EventType"),
                        ReasonCode = await BaseService.GetRefLib("ReasonCd", "32"),
                        ChangeStatusTo = await BaseService.GetRefLib("CardSts")
                    };
                    return Json(new { Model = stsDetails.changeStatus, Selects = selecs }, JsonRequestBehavior.AllowGet);
                case "rep":
                    var rep = (await CardHolderService.GetCardReplacementInfo(CardNo)).cardReplacement;
                    var repSelects = new CardReplacement
                    {
                        FeeCd = await BaseService.GetFeeCd("RPL"),
                        ReasonCd = await BaseService.GetRefLib("ReasonCd", "32"),
                        CurrentStatus = await BaseService.GetRefLib("CardSts"),
                        CardMedia = await BaseService.GetCardMedia() 
                    };
                    return Json(new { Model = rep, Selects = repSelects }, JsonRequestBehavior.AllowGet);
                case "loc":
                    var locationAcceptanceList = new LocationAcceptListModel
                   {
                       State = await BaseService.WebGetState("608"),
                       BusnLocations = new List<SelectListItem>(),
                       UserId = HttpContext.User.Identity.Name,
                       CreationDate = System.DateTime.Now.ToString()
                   };
                    return Json(new { Selects = locationAcceptanceList, Model = new LocationAcceptListModel() }, JsonRequestBehavior.AllowGet);

                case "pdc":
                    var _prodDiscount = new ProductDiscount
                    {
                        ProdCd = await BaseService.WebProductGroupSelect(),
                        RebatePlan = await BaseService.WebGetPlan("2"),
                        DiscPlan = await BaseService.WebGetPlan("1"),
                        ProdDiscType = await BaseService.GetRefLib("ProdDiscType"),
                        PlanId = new List<SelectListItem>()
                    };
                    return Json(new { Selects = _prodDiscount, Model = new ProductDiscount() }, JsonRequestBehavior.AllowGet);

                default:
                    return Json(new { });
            }
        }

        #region "General Info"
        #region "Table"
        [CompressFilter]
        public async Task<ActionResult> ftCardHolderList(jQueryDataTableParamModel Params, string _AcctNo, bool isExport = false)
        {
            //byte[] Bytes;
            var _filtered = new List<CardHolderInfoModel>();
            var list = (await CardHolderService.GetCardHolders(_AcctNo)).cardHolderInfos;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.CardNo) ? p.CardNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.EmbossName) ? p.EmbossName : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedCurrentStatus) ? p.SelectedCurrentStatus : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CardExpiry) ? p.CardExpiry : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.XRefCardNo) ? p.XRefCardNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedCardType) ? p.SelectedCardType : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedPINInd) ? p.SelectedPINInd : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.vehRegNo) ? p.vehRegNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.DriverCd) ? p.DriverCd : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.FullName) ? p.FullName : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.BlockedDate) ? p.BlockedDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TerminatedDate) ? p.TerminatedDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedCostCentre) ? p.SelectedCostCentre : string.Empty).Contains(Params.sSearch)).ToList();

                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }

            var xx = from x in _filtered
                     select new
                     {
                         x.CardNo,
                         x.EmbossName
                     };

            if (isExport)
            {
                var toExport = new List<string[]>();
                var Header = list.First().ExcelHeader;
                foreach (var item in list)
                {
                    toExport.Add(item.ExcelBody());
                }
                var ExcelPkg = Common.CommonHelpers.CreateExcel(Header, toExport, "CardsList-" + _AcctNo);
                return File(ExcelPkg.GetAsByteArray(), "application/vnd.ms-excel", string.Format("List of Cards-{0}.xlsx", _AcctNo));
            }
            return Json(new
            {

                sEcho = Params.sEcho,
                iTotalRecords = list.Count,
                iTotalDisplayRecords = list.Count,
                aaData = _filtered.Select(x => new object[] { x.CardNo, x.EmbossName, x.SelectedCurrentStatus, x.CardExpiry, x.SelectedCardType, x.SelectedPINInd, x.vehRegNo, x.DriverCd, x.FullName, x.BlockedDate, x.TerminatedDate, x.SelectedCostCentre }),
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Table"

        #region "form"
        public async Task<ActionResult> ftGeneralInfoSelect(string _cardNo)
        {
            var data = (await CardHolderService.GetGeneralInfo(_cardNo)).cardHolderInfo;
            return Json(new { info = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "form"

        #region "Save"
        [HttpPost]
        public async Task<ActionResult> WebCardGeneralInfoMaint(CardHolderInfoModel CardDataInfo)
        {
            var SaveApplGeneralInfoMaint = await CardHolderService.SaveGeneralInfo(CardDataInfo, GetUserId);
            return Json(new { resultCd = SaveApplGeneralInfoMaint }, JsonRequestBehavior.AllowGet);
        }

        #endregion "Save"
        #endregion "General Info"

        #region "Financial"
        #region "WebPIN"
        public async Task<ActionResult> SaveWebPinReset(string CardNo)
        {
            var _SaveWebPinReset = await CardHolderService.ResetWebPin(CardNo,GetUserId);
            return Json(new { resultCd = _SaveWebPinReset }, JsonRequestBehavior.AllowGet);
        }
        #endregion "WebPIN"

        #region "PINChange"
        public async Task<ActionResult> SaveWebPinChange(string cardNo)
        {
            var _SaveWebPinChange = await CardHolderService.ChangeWebPin(cardNo, GetUserId);
            return Json(new { resultCd = _SaveWebPinChange }, JsonRequestBehavior.AllowGet);
        }
        #endregion "PINChange"

        #region "form"
        public async Task<ActionResult> ftFinancialInfoSelect(int _IssNo, string cardNo)
        {
            var data = (await CardHolderService.GetFinancialInfo(cardNo)).cardHolderInfo;
            return Json(new { FinInfo = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "form"

        #region "Save"
        [HttpPost]
        public async Task<ActionResult> SaveFinancialInfo(CardFinancialInfoModel finInfo, string cardNo)
        {
            var _SaveFinancial = await CardHolderService.SaveFinancialInfo(finInfo, cardNo);
            return Json(new { resultCd = _SaveFinancial }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Save"
        #endregion "Financial"

        #region "Person Info"
        #region "form"
        public async Task<ActionResult> ftPersonInfoSelect(int _issNo, string _entityId)
        {
            var data = (await CardHolderService.GetPersonInfo(_entityId)).personalInfo;
            return Json(new { PersonInfo = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "form"

        #region "save"
        [HttpPost]
        public async Task<ActionResult> SavePersonInfo(PersonInfoModel PersonInfo, string _entityID)
        {
            var _SavePersonInfo = await CardHolderService.SavePersonInfo(PersonInfo, _entityID);
            return Json(new { resultCd = _SavePersonInfo }, JsonRequestBehavior.AllowGet);
        }
        #endregion "save"
        #endregion "Person Info"

        #region "Location Acceptance"
        #region "Table"
        [CompressFilter]
        public async Task<ActionResult> ftLocationList(jQueryDataTableParamModel Params, string AcctNo, CardnAccNo _CardnAccNo)
        {
            var _filtered = new List<LocationAcceptListModel>();
            var list = (await CardHolderService.GetLocationAcceptances(AcctNo, _CardnAccNo.CardNo)).locationAccepts;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p._CardnAccNo.CardNo) ? p._CardnAccNo.CardNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.DBAName) ? p.DBAName : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.s_state) ? p.s_state : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).Contains(Params.sSearch) ||
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
                aaData = _filtered.Select(x => new object[] { x._CardnAccNo.CardNo, x.DBAName, x.s_state, x.UserId, x.CreationDate })
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion "Table"

        #region "form"
        public async Task<ActionResult> ftLocationAcceptanceSelect(string _AcctNo, string _BusnLoc, string CardNo)
        {
            var data = (await CardHolderService.GetLocationAcceptance(_AcctNo, _BusnLoc, CardNo)).locationAccept;
            return Json(new { address = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "form"

        #region "Save"
        [HttpPost]
        public async Task<ActionResult> SaveLocationAccept(LocationAcceptListModel _LocationAcceptList, CardnAccNo _CardnAcct)
        {
            _LocationAcceptList._CardnAccNo = _CardnAcct;
            var _saveLocationAccept = await CardHolderService.SaveLocationAccept(_LocationAcceptList, _CardnAcct.AccNo, _CardnAcct.CardNo, GetUserId);
            return Json(new { resultCd = _saveLocationAccept }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Save"

        #region "delete"
        public async Task<ActionResult> DelLocAccept(string _AcctNo, string _BusnLocation, string _CardNo)
        {
            var result = await CardHolderService.DeleteLocationAcceptance(_AcctNo, _BusnLocation, _CardNo, GetUserId);
            return Json(new { resultCd = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion "delete"
        #endregion "Location Acceptance"


        #region "Card Replacement"
        #region "form"
        public async Task<ActionResult> ftCardReplacementInfoSelect(string _CardNo)
        {
            var data = (await CardHolderService.GetCardReplacementInfo(_CardNo)).cardReplacement;
            return Json(new { CardReplace = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "form"

        #region "Save"
        [HttpPost]
        public async Task<ActionResult> SaveCardReplacementInfo(CardReplacement CardReplace)
        {
            var _SaveChangedStatus = await CardHolderService.SaveCardReplacement(CardReplace, GetUserId);
            return Json(new { resultCd = _SaveChangedStatus, NewCard = _SaveChangedStatus.returnValue.NewcardNo }, JsonRequestBehavior.AllowGet); //, newCardNo = objCardHoldersOps.NewcardNo 
        }
        #endregion "Save"
        #endregion "Card Replacement"
    }
}