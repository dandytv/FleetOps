using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using FleetOps.Models;
using FleetSys.Models;
using FleetOps.App_Start;
using CCMS.ModelSector;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using AutoMapper;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.Account;
using CardTrend.Common.Extensions;
using System.Globalization;
using CardTrend.Domain.Dto.CardHolder;
using CardTrend.Domain.Dto.EventConfiguration;
using FleetSys.Common;
namespace FleetSys.Controllers
{
    [Authorize(Roles = "Internal")]
    public class AccountController : BaseController
    {
        private AccountOps objAcctOps = new AccountOps();
        private TxnAdjustmentOps objTxnAdjustmentOps = new TxnAdjustmentOps();
        [Accessibility]
        public ActionResult Index(string id)
        {
            ViewBag.AcctNo = id;
            return View();
        }
        #region "List Of Web Pages"
        [CompressFilter]
        [AccessibilityXtra]
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                case "gen":
                    return PartialView(this.getPartialPath("Accounts", "AcctGeneralInfo_Partial"), new GeneralInfoModel());
                case "fin":
                    return PartialView(this.getPartialPath("Accounts", "FinancialInfoCardAcctMaint_Partial"), new FinancialInfoModel());
                case "cao":
                    return PartialView(this.getPartialPath("Accounts", "CreditAssessmentOperation_Partial"), new CreditAssesOperation());
                case "dep":
                    return PartialView(this.getPartialPath("Accounts", "DepositInfo_Partial"), new CreditAssesOperation());
                case "upd":
                    return PartialView(this.getPartialPath("Accounts", "UpToDateBalance_Partial"), new UpToDateBal());
                case "skd":
                    return PartialView(this.getPartialPath("Accounts", "DieselSubsidy_Partial"), new SKDS());
                case "ast":
                    return PartialView(this.getPartialPath("Accounts", "acctSubsidy_Partial"), new SKDS());
                case "car":
                    return PartialView(this.getPartialPath("Accounts", "CardHolderInfo_Partial"), new CardHolderInfoModel());
                case "veh":
                    return PartialView(this.getPartialPath("Accounts", "VehiclesList_Partial"), new VehiclesListModel());
                case "vel":
                    return PartialView(this.getPartialPath("Accounts", "VelocityLimitsListMaint_Partial"), new VeloctyLimitListMaintModel());
                case "txn":
                    return PartialView(this.getPartialPath("Accounts", "TxnAdjustment_Partial"), new TxnAdjustment());
                case "pay":
                    return PartialView(this.getPartialPath("Accounts", "PaymentTxn_Partial"), new PaymentTxn());
                case "loc":
                    return PartialView(this.getPartialPath("Accounts", "LocationAcceptanceList_Partial"), new LocationAcceptListModel());
                case "tem":
                    return PartialView(this.getPartialPath("Accounts", "TemporaryCreditControl_Partial"), new TempCreditCtrlModel());
                case "con":
                    return PartialView(this.getPartialPath("Accounts", "Contact_Partial"), new ContactLstModel());
                case "add":
                    return PartialView(this.getPartialPath("Applications", "Address_Partial"), new AddrListMaintModel());
                case "txs":
                    return PartialView(this.getPartialPath("Accounts", "TransactionSearch_Partial"), new AcctPostedTxnSearch());
                case "bil_Main":
                    return PartialView(this.getPartialPath("Accounts/BillingItem", "BillingItem_Partial"), new BillingItem());
                case "bil_detail":
                    return PartialView(this.getPartialPath("Accounts/BillingItem", "BillingItemDetail_Partial"), new BillingItem());
                case "usm":
                    return PartialView(this.getPartialPath("Accounts", "AccountUsers_Partial"));
                case "pdc":
                    return PartialView(this.getPartialPath("Accounts", "ProductDiscount_Partial"), new ProductDiscount());
                case "cos":
                    return PartialView(this.getPartialPath("Accounts", "CostCentre_Partial"), new CostCentre());
                case "puk":
                    return PartialView(this.getPartialPath("Accounts", "Pukal_Partial"), new Pukal());
                case "sta":
                    return PartialView(this.getPartialPath("Accounts", "StatusMaint_Partial"), new ChangeStatus());
                case "pad":
                    return PartialView(this.getPartialPath("Accounts", "PointAdjustment_Partial"), new PointAdjustment());
                case "aps":
                    return PartialView(this.getPartialPath("Accounts", "AcctPostedTxn_Search"), new AcctPostedTxnSearch());
                case "evc":
                    return PartialView(this.getPartialPath("Accounts", "EventConfigurations/EventConf_Partial"), new LookupParameters());
                default:
                    return PartialView();
            }
        }
        #endregion
        #region "Fill Data"
        [CompressFilter]
        public async Task<JsonResult> FillData(string Prefix, int AcctNo)
        {
            switch (Prefix.ToLower())
            {
                case "gen":
                    var generalAccount = (await AccountOpService.GetGeneralAccountInfo(AcctNo)).generalInfo;
                    var PaymentTerm = await BaseService.GetRefLib("PaymtTerm");
                    var temp = PaymentTerm.SkipWhile(p => p.Value == "").ToList();
                    PaymentTerm = (temp.OrderBy(p => Convert.ToInt32(p.Value))).ToList();

                    var GenSelects = new GeneralInfoModel
                    {
                        AccountType = await BaseService.GetRefLib("BankAcctType"),
                        PlasticType = await BaseService.GetPlasticType("I"),
                        CompanyType = await BaseService.GetRefLib("CmpyType"),
                        CorpName = await BaseService.WebGetCorpCd(false),
                        SIC = await BaseService.GetMerchType("S"),  //(M)CC or (S)IC
                        ClientClass = await BaseService.GetRefLib("ClientClass"),
                        ClientType = await BaseService.GetRefLib("ClientType"),
                        BusnEstablishment = await BaseService.GetRefLib("BusnEst"),
                        ReasonCode = await BaseService.GetRefLib("ReasonCd"),
                        OvrStatus = await BaseService.GetRefLib("OverrideSts"),
                        BusnCategory = await BaseService.GetRefLib("BusnCategory"),
                        SaleTerritory = await BaseService.GetRefLib("SaleTerritory"),
                        PaymentTerm = PaymentTerm,
                        LangId = await BaseService.GetRefLib("Language"),
                        TradingArea = await BaseService.GetRefLib("TradingArea")
                    };

                    return Json(new { Model = generalAccount, Selects = GenSelects }, JsonRequestBehavior.AllowGet);

                case "fin":
                    var _FiData = (await AccountOpService.GetFinancialInfoForm(AcctNo)).financialInfo;
                    var FinSelects = new FinancialInfoModel
                    {
                        DunningCd = await BaseService.GetRefLib("Dunning"),
                        CycNo = await BaseService.GetCycle("I"),
                        StmtType = await BaseService.GetRefLib("BillingType"),
                        StmtInd = await BaseService.GetRefLib("InvPrefer"),
                        PaymtMethod = await BaseService.GetRefLib("PaymtMethod"),
                        BankAcctType = await BaseService.GetRefLib("BankAcctType"),
                        BankName = await BaseService.GetRefLib("Bank"),
                        TaxCategory = await BaseService.GetRefLib("TaxCategory"),
                        AssessmentType = await BaseService.GetRefLib("AssessmentType"),
                        RiskCategory = await BaseService.GetRefLib("RiskCategory"),
                        AssignCollector = await BaseService.GetUserAccess("InternalUsers") 
                    };
                    return Json(new { Model = _FiData, Selects = FinSelects }, JsonRequestBehavior.AllowGet);

                case "upd":
                    var _uptodateBalance = (await AccountOpService.GetFtUpToBalDetail(Convert.ToString(AcctNo))).upToDateBal;
                    return Json(new { Model = _uptodateBalance }, JsonRequestBehavior.AllowGet);

                case "cao":
                    var _Cao = (await AccountOpService.GetCreditApplAssessentForm(Convert.ToString(AcctNo), "0")).creditAssesOperation;
                    PaymentTerm = await BaseService.GetRefLib("PaymtTerm");
                    temp = PaymentTerm.SkipWhile(p => p.Value == "").ToList();
                    PaymentTerm = (temp.OrderBy(p => Convert.ToInt32(p.Value))).ToList();

                    var CaoSelects = new CreditAssesOperation
                    {
                        PaymentMode = await BaseService.GetRefLib("PaymtMethod"),
                        PaymentTerm = PaymentTerm, //await WebGetRefLib("PaymtTerm"),
                        TerritoryCd = await BaseService.GetRefLib("SaleTerritory"),
                        RiskCategory = await BaseService.GetRefLib("RiskCategory"),
                        AssesmtType = await BaseService.GetRefLib2("AssessmentType"),
                        DepositType = await BaseService.GetRefLib("DepositType"),
                        BankAcctType = await BaseService.GetRefLib("BankAcctType"),
                        BankName = await BaseService.GetRefLib("Bank"),
                        ReasonCd = await BaseService.GetCAOReasonCd(),
                        Qualitative = await BaseService.GetRefLib("QualitativeRating"),
                        Quantitative = await BaseService.GetRefLib("QuantitativeRating")
                        ,TradingArea = await BaseService.GetRefLib("TradingArea")
                    };
                    _Cao.GracePeriod = 20;
                    return Json(new { Model = _Cao, Selects = CaoSelects }, JsonRequestBehavior.AllowGet);

                case "dep":
                    var _adi = new CreditAssesOperation
                    {
                        DepositType = await BaseService.GetRefLib("DepositType", null, "1"),
                        BankAcctType = await BaseService.GetRefLib("BankAcctType"),
                        BankName = await BaseService.GetRefLib("SecurityDepositBank"),
                    };
                    return Json(new { Selects = _adi, Model = new CreditAssesOperation() }, JsonRequestBehavior.AllowGet);
                case "add":
                    var AddrSelects = new AddrListMaintModel
                    {
                        addrtype = await BaseService.GetRefLib("Address"),
                        State = await BaseService.WebGetState("CtryCd"),//changes
                        Country = await BaseService.GetRefLib("Country"),
                        region = await BaseService.GetRefLib("RegionCd"),
                    };
                    var AddrModel = new AddrListMaintModel
                    {
                        UserId = this.GetUserId,
                        CreationDate = System.DateTime.Now.ToString(),
                        RefTo = "ACCT",
                        RefKey = Convert.ToString(AcctNo)
                    };
                    return Json(new { Selects = AddrSelects, Model = AddrModel }, JsonRequestBehavior.AllowGet);
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
                case "vel":
                    var velocityLimitsList = new VeloctyLimitListMaintModel
                    {
                        VelocityInd = await BaseService.GetRefLib("VelocityInd"),
                        ProdCd = await BaseService.WebGetProduct(null),
                        CtrlType = await BaseService.GetRefLib("CollateralType"),
                    };
                    var _Model = new VeloctyLimitListMaintModel
                    {
                        UserId = HttpContext.User.Identity.Name,
                        CreationDate = System.DateTime.Now.ToString("dd/MM/yyyy")
                    };
                    return Json(new { Selects = velocityLimitsList, Model = _Model }, JsonRequestBehavior.AllowGet);
                case "veh":
                    var _VehiclesListModel = new VehiclesListModel
                    {
                        VehColor = await BaseService.GetRefLib("Color"),
                        VehMaker = await BaseService.GetRefLib("VehMaker"),
                        VehModel = await BaseService.GetRefLib("VehSubModel"),
                        CardType = await BaseService.GetCardType(),
                        Sts = await BaseService.GetRefLib("AcctSts"),
                        VehYr = BaseService.WebGetYear(),
                        VehType = await BaseService.GetRefLib("VehType")
                    };
                    return Json(new { Selects = _VehiclesListModel, Model = new VehiclesListModel() }, JsonRequestBehavior.AllowGet);
                case "loc":
                    var locationAcceptanceList = new LocationAcceptListModel
                    {
                        State = await BaseService.WebGetState("608"),
                        BusnLocations = new List<SelectListItem>(),
                        UserId = HttpContext.User.Identity.Name,
                        CreationDate = System.DateTime.Now.ToString()
                    };
                    return Json(new { Selects = locationAcceptanceList, Model = new LocationAcceptListModel() }, JsonRequestBehavior.AllowGet);
                case "aps":
                    var _AcctPostedTxnSearch = new AcctPostedTxnSearch
                    {
                        CardNo = await BaseService.GetCardNo(Convert.ToString(AcctNo)),
                        TxnCategory = await BaseService.GetTransactionCategory(),
                        TxnCd = await BaseService.WebGetTxnCode("I")
                    };
                     var json = Json(new { Selects = _AcctPostedTxnSearch, Model = new AcctPostedTxnSearch() }, JsonRequestBehavior.AllowGet);
                    json.MaxJsonLength = int.MaxValue;
                    return json;
                case "evt":
                    var _eventLoggerInfo = new EventLogger
                   {
                       EventType = await BaseService.GetRefLib("EventType"),
                       EventSts = await BaseService.GetRefLib("EventSts"),
                       ReasonCd = await BaseService.GetRefLib("ReasonCd"),
                       Module = await BaseService.GetRefLib("ModuleId")
                   };
                    return Json(new { Selects = _eventLoggerInfo, Model = new EventLogger() }, JsonRequestBehavior.AllowGet);
                case "pay":
                    var _pyTxn = new PaymentTxn
                    {
                        PyTxnCd = await BaseService.WebGetTxnCode("I", "PaymtTxnCategoryMapInd", "Y"),
                        Sts = await BaseService.GetRefLib("entrysts"),
                        Owner = (await  UserAccessService.GetUserAccessListSelect()).RefLibLst

                    };
                    return Json(new { Selects = _pyTxn, Model = new PaymentTxn() }, JsonRequestBehavior.AllowGet);

                case "txn":
                    var _TxnAdjustment = new TxnAdjustment();
                        var _TxnAdjustments = new TxnAdjustment
                        {
                            TxnCd = await BaseService.WebGetTxnCode("I", "AdjustTxnCategoryMapInd", "Y"),//("Module", "Deff", "ManualEntry"),
                            Sts = await BaseService.GetRefLib("EntrySts", "0"),
                            Owner = (await  UserAccessService.GetUserAccessListSelect()).RefLibLst,
                            IssueingBank = await BaseService.GetRefLib("Bank")
                        };

                    return Json(new { Selects = _TxnAdjustment, Model = new TxnAdjustment() }, JsonRequestBehavior.AllowGet);
                case "bil":
                    var bil = new BillingItem
                    {
                        Sts = await BaseService.GetRefLib("BillItemSts"),
                        TxnCategory = await BaseService.WebGetBillItemTxnCategory("I")
                    };
                    return Json(new { Selects = bil, Model = new BillingItem() }, JsonRequestBehavior.AllowGet);
                case "pdc":
                    var _prodDiscount = new ProductDiscount
                    {
                        ProdCd = await BaseService.WebProductGroupSelect(),//BaseClass.WebGetProduct(null, false),
                        RebatePlan = await BaseService.WebGetPlan("2"),
                        DiscPlan = await BaseService.WebGetPlan("1"),
                        ProdDiscType = await BaseService.GetRefLib("ProdDiscType"),
                        PlanId = new List<SelectListItem>()
                    };
                    return Json(new { Selects = _prodDiscount, Model = new ProductDiscount() }, JsonRequestBehavior.AllowGet);
                case "tem":
                    //var data = await objAcctOps.FtTempCreditLimitDetail(AcctNo);
                    var data = (await AccountOpService.GetTempCreditLimitDetail(Convert.ToString(AcctNo))).tempCreditCtrl;
                    return Json(new { Model = data, Selects = "" }, JsonRequestBehavior.AllowGet);
                case "sta":
                    var stDetails = await CardHolderService.GetChangedAcctStsDetail(AcctNo.ToString(), "ACCT");
                    var selecs = new ChangeStatus
                    {
                        CurrentStatus = await BaseService.GetRefLib("AcctSts"),
                        RefType = await BaseService.GetRefLib("EventType"),
                        ReasonCode = await BaseService.GetRefLib("ReasonCd", "64"),
                        ChangeStatusTo = await BaseService.GetRefLib("AcctSts")
                    };
                    return Json(new { Model = stDetails.changeStatus, Selects = selecs }, JsonRequestBehavior.AllowGet);
                case "pad":
                    var _Adj = new PointAdjustment
                    {
                        Status = await BaseService.GetRefLib("EntrySts"),
                        TxnCd = await BaseService.WebGetTxnCode("I")
                    };
                    return Json(new { Model = new PointAdjustment(), Selects = _Adj }, JsonRequestBehavior.AllowGet);
                case "csc":
                    return Json(new { Model = new CostCentre(), Selects = "" }, JsonRequestBehavior.AllowGet);
                case "puk":
                    var _data = await objAcctOps.WebPukalSelect(AcctNo, "ACCT");
                    var _pukal = new Pukal
                    {
                        RefCd = await BaseService.GetRefLib("PaymtInfoType"),
                        AcctOfficeCode = await BaseService.GetRefLib("AcctOfficeCd"),
                        TermFlag = await BaseService.GetRefLib("TerminationFlag", null, null, null, false),
                        AgObjectCode = await BaseService.GetRefLib("AGObjCd")
                    };
                    return Json(new { Model = _data, Selects = _pukal }, JsonRequestBehavior.AllowGet);
                case "ast":
                    var acctSubsidy = new SKDS
                     {
                         SkdsAcctSub = await BaseService.GetAcctSubsidy(AcctNo.ToString()),
                         Sts = await BaseService.GetRefLib("SubsidySts"),
                     };
                    List<SelectListItem> xx = acctSubsidy.Sts.ToList();
                    xx.Insert(0, new SelectListItem { Text = "", Value = "" });
                    acctSubsidy.Sts = xx;
                    return Json(new { Model = new SKDS(), Selects = acctSubsidy }, JsonRequestBehavior.AllowGet);
                case "evc":
                    //EventConfigMaint _EventConfigMaint = new EventConfigMaint();
                    var Selects = new LookupParameters
                    {
                        EventType = await BaseService.GetEvtType(),
                        Priority = await BaseService.GetRefLib("Priority"),
                        Status = await BaseService.GetRefLib("Status"),
                        Scope = await BaseService.GetRefLib("Scope"),
                        Owner = await BaseService.GetRefLib("NtfEventOwner"),
                        Frequency = await BaseService.GetRefLib("NtfEventPeriodType"),
                        Languages = await BaseService.GetRefLib("Language"),
                    };
                    return Json(new { Selects = Selects, Model = new LookupParameters() }, JsonRequestBehavior.AllowGet);
                default:
                    HttpContext.Response.StatusCode = 404;
                    return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region "Cards Holder"
        [CompressFilter]
        public async Task<ActionResult> ftCardHolderList(jQueryDataTableParamModel Params, string AcctNo)
        {
            var _filtered = new List<CardHolderInfoModel>();
            var list = (await CardHolderService.GetCardHolders(AcctNo)).cardHolderInfos;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.CardNo) ? p.CardNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.EmbossName) ? p.EmbossName : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.vehRegNo) ? p.vehRegNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedCurrentStatus) ? p.SelectedCurrentStatus : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CardExpiry) ? p.CardExpiry : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedCardType) ? p.SelectedCardType : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.FullName) ? p.FullName : string.Empty).ToLower().Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.CardNo, x.EmbossName, x.SelectedCurrentStatus, x.CardExpiry, x.SelectedCardType, x.SelectedPINInd, x.vehRegNo, x.DriverCd, x.FullName, x.BlockedDate, x.TerminatedDate, x.SelectedCostCentre })//, x.XRefCardNo
            }, JsonRequestBehavior.AllowGet);
        }
        #region"Address"
        [CompressFilter]
        public async Task<ActionResult> FtAddressList(jQueryDataTableParamModel Params, string AcctNo)
        {
            var _filtered = new List<AddrListMaintModel>();
            //var list = await objAcctOps.FtAddressList("Acct", AcctNo);
            var list = (await CardAcctSignUpService.GetAddressList("Acct", AcctNo)).Addresses;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.SelectedAddrType) ? p.SelectedAddrType : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedMailInd) ? p.SelectedMailInd : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Address) ? p.Address : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.District) ? p.District : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.City) ? p.City : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Selectedstate) ? p.Selectedstate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.PostalCode) ? p.PostalCode : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedCountry) ? p.SelectedCountry : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.selectedregion) ? p.selectedregion : string.Empty).ToLower().Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.SelectedRefCd, x.SelectedAddrType, x.SelectedMailInd, x.Address, x.District, x.City, x.Selectedstate, x.PostalCode, x.SelectedCountry, x.selectedregion })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region"Contact"
        [CompressFilter]
        public async Task<ActionResult> ftContactList(jQueryDataTableParamModel Params, string RefTo, string RefKey)
        {
            var _filtered = new List<ContactLstModel>();
            //var list = await objAcctOps.FtContactlist(RefTo, RefKey);
            var list = (await CardAcctSignUpService.GetContactlist(RefTo, RefKey)).contacts;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.SelectedContactType) ? p.SelectedContactType : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ContactName) ? p.ContactName : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ContactNo) ? p.ContactNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedSts) ? p.SelectedSts : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedOccupation) ? p.SelectedOccupation : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.EmailAddr) ? p.EmailAddr : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.RefCd, x.SelectedContactType, x.ContactName, x.ContactNo, x.SelectedOccupation, x.EmailAddr, x.CreationDate, x.UserId })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Cost Centre"
        [CompressFilter]
        public async Task<ActionResult> ftCostCentreList(jQueryDataTableParamModel Params, CostCentre _costcentre, bool isExport = false)
        {
            var _filtered = new List<CostCentre>();
            var list = (await AccountOpService.GetCostCentres(_costcentre.RefTo,_costcentre.RefKey)).costCentres;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {

                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.SelectedCostCentre) ? p.SelectedCostCentre : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Descp) ? p.Descp : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.PersoninCharge) ? p.PersoninCharge : string.Empty).Contains(Params.sSearch)).ToList();
                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }

            if (isExport)
            {
                var title = "Cost Centre";
                var toExport = new List<string[]>();
                var Header = list.First().XlsHeader();
                foreach (var item in list)
                {
                    toExport.Add(item.XlsBody());
                }
                var ExcelPkg = CommonHelpers.CreateExcel(Header, toExport, title);
                return File(ExcelPkg.GetAsByteArray(), "application/vnd.ms-excel", title + ".xlsx");
            }
            return Json(new
             {
                 sEcho = Params.sEcho,
                 iTotalRecords = list.Count(),
                 iTotalDisplayRecords = list.Count(),
                 aaData = _filtered.Select(x => new object[] { 
                    x.SelectedCostCentre,x.Descp,x.PersoninCharge
                })
             }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Velocity Limit"
        [CompressFilter]
        public async Task<ActionResult> ftVelocityList(jQueryDataTableParamModel Params, VeloctyLimitListMaintModel _AcctVelocity, CardnAccNo cardnAccNo)
        {
            _AcctVelocity._CardnAccNo = cardnAccNo;

            var _filtered = new List<VeloctyLimitListMaintModel>();
            var list = (await CardAcctSignUpService.GetCustAcctVelocityList(_AcctVelocity)).veloctyLimits;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.VelocityIndDescp) ? p.VelocityIndDescp : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ProdCdDescp) ? p.ProdCdDescp : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CntrLimit.ToString()) ? p.CntrLimit.ToString() : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ddlVelocityLimit) ? p.ddlVelocityLimit : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ddlVelocityLitre.ToString()) ? p.ddlVelocityLitre.ToString() : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.LastUpdateDate) ? p.LastUpdateDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedVelocityInd) ? p.SelectedVelocityInd : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedProdCd) ? p.SelectedProdCd : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] { x.VelocityIndDescp, x.ProdCdDescp, x.CntrLimit, x.ddlVelocityLimit, x.ddlVelocityLitre, x.LastUpdateDate, x.UserId, x.CreationDate, x.SelectedVelocityInd, x.SelectedProdCd, })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Location Acceptance"
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
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.BusnLoc) ? p.BusnLoc : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.busnName) ? p.busnName : string.Empty).ToLower().Contains(Params.sSearch)||
                                            (!string.IsNullOrEmpty(p.DBAName) ? p.DBAName : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.s_state) ? p.s_state : string.Empty).Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { null, x.BusnLoc, x.busnName, x.DBAName, x.s_state, x.SiteId, x.UserId, x.CreationDate })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Event Logger"
        [CompressFilter]
        public async Task<ActionResult> ftEventLoggerList(jQueryDataTableParamModel Params, EventLogger _loggerevent)
        {
            var _filtered = new List<EventLogger>();
            var list = (await AccountOpService.GetEventlist(_loggerevent.SelectedModule, _loggerevent.acctNo, null)).eventLoggers;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.SelectedEventType) ? p.SelectedEventType : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedEventSts) ? p.SelectedEventSts : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.acctNo) ? p.acctNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.XreferenceDoc) ? p.XreferenceDoc : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedReasonCode) ? p.SelectedReasonCode : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ClosedDate) ? p.ClosedDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.sysInd) ? p.sysInd : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.EventId) ? p.EventId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Description) ? p.Description : string.Empty).Contains(Params.sSearch) ||
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
                aaData = _filtered.Select(x => new object[] { x.SelectedEventType, x.SelectedEventSts, x.acctNo, x.XreferenceDoc, x.SelectedReasonCode, x.ClosedDate, null, x.sysInd, x.EventId, x.Description, x.CreationDate })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Payment"
        public async Task<ActionResult> ftPaymentTxnList(jQueryDataTableParamModel Params, Int64 AcctNo)//
        {
            var _filtered = new List<PaymentTxn>();
            var list = (await AccountOpService.GetPaymentTxnList(AcctNo.ToString())).PaymentTxns;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.AcctNo) ? p.AcctNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnDate) ? p.TxnDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.displayTotAmnt) ? p.displayTotAmnt : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedTxnType) ? p.SelectedTxnType : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.PyTxnId) ? p.PyTxnId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedPyTxnCd) ? p.SelectedPyTxnCd : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Descp) ? p.Descp : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.StsDescp) ? p.StsDescp : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] { x.TxnDate, x.CreationDate, x.SelectedTxnType, x.displayTotAmnt, x.Descp, x.PyTxnId, x.AppRemarks, x.StsDescp, x.UserId })
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region "Settlement Billing Item"
        [CompressFilter]
        public async Task<ActionResult> ftBillingItemSettlementList(jQueryDataTableParamModel Params, int Txn)
        {
            var _filtered = new List<BillingItem>();
            var list = await objAcctOps.FtBillingItemSettlementList(Txn);

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.TxnId.ToString()) ? p.TxnId.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SettledDate) ? p.SettledDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.DisplaySettledAmt) ? p.DisplaySettledAmt : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.PrcsDate) ? p.PrcsDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p._CreationDatenUserId.UserId) ? p._CreationDatenUserId.UserId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p._CreationDatenUserId.CreationDate) ? p._CreationDatenUserId.CreationDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.RefId) ? p.RefId : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] { x.TxnId, x.SettledDate, x.DisplaySettledAmt, x.PrcsDate, x._CreationDatenUserId.UserId, x._CreationDatenUserId.CreationDate, x.RefId })
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region "Billing Item"
        [CompressFilter]
        public async Task<ActionResult> ftBillingItemTxnList(jQueryDataTableParamModel Params, int Txn)
        {
            var _filtered = new List<BillingItem>();
            var list = await objAcctOps.FtBillingItemSettlementList(Txn);

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.TxnId.ToString()) ? p.TxnId.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p._CardnAccNo.CardNo) ? p._CardnAccNo.CardNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnDate) ? p.TxnDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.BookingDate) ? p.BookingDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.PrcsDate) ? p.PrcsDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.DisplayBillingTxnAmt) ? p.DisplayBillingTxnAmt : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Descp) ? p.Descp : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.BusnLocation) ? p.BusnLocation : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TermId) ? p.TermId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p._CreationDatenUserId.UserId) ? p._CreationDatenUserId.UserId : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] { x.TxnId, x._CardnAccNo.CardNo, x.TxnDate, x.BookingDate, x.PrcsDate, x.DisplayBillingTxnAmt, x.Descp, x.BusnLocation, x.TermId, x._CreationDatenUserId.UserId })
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region "Deposit Info"
        [CompressFilter]
        public async Task<ActionResult> ftGetAcctDepositInfoList(jQueryDataTableParamModel Params, string AcctNo)
        {
            var _filtered = new List<CreditAssesOperation>();
            var list = (await AccountOpService.GetAcctDepositInfos(null, AcctNo)).creditAssesOperationLst;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.SelectedDepositType.ToString()) ? p.SelectedDepositType.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                           (!string.IsNullOrEmpty(p.SelectedBankAcctType) ? p.SelectedBankAcctType : string.Empty).Contains(Params.sSearch) ||
                                           (!string.IsNullOrEmpty(p.SelectedBankName) ? p.SelectedBankName : string.Empty).Contains(Params.sSearch) ||
                                           (!string.IsNullOrEmpty(p.BankAcctNo) ? p.BankAcctNo : string.Empty).Contains(Params.sSearch) ||
                                           (!string.IsNullOrEmpty(p.Txnid) ? p.Txnid : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = list.Select(x => new object[] {
                    x.SelectedDirectDebitInd,
                    x.SelectedDepositType,
                    x.SelectedBankAcctType, 
                    x.SelectedBankName,
                    x.BankAcctNo, 
                    x.DepositAmt, 
                    x.Txnid,
                    x.BgSerialNo,
                    x.SAPRefNo,
                    x.UserId,
                    x.Creationdt,
                    x.DepositFromDate+"-"+x.DepositToDate})
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #endregion
        #region "General Info Detail"
        public async Task<ActionResult> ftGeneralInfo(Int64 acctNo)
        {
            var generalAccount = (await AccountOpService.GetGeneralAccountInfo(Convert.ToInt32(acctNo))).generalInfo;
            return Json(generalAccount, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> GeneralInfoMaint(GeneralInfoModel _generalinfoData)
        {
            var _TraceInfo = await AccountOpService.SaveGeneralAccountInfo(_generalinfoData);
            return Json(new { result = _TraceInfo }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadPointBalance(String AcctNo, String RequestorId, String Token)
        {
            var PointBalance = objAcctOps.LoadPointBalance(RequestorId, Token, AcctNo);
            return Json(new { PointBal = PointBalance }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region "Financial Info Detail"
        public async Task<ActionResult> ftFinancialInfo(int acctNo)
        {
            var _finInfo = (await AccountOpService.GetFinancialInfoForm(acctNo)).financialInfo;
            return Json(_finInfo, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public async Task<ActionResult> FinancialInfoMaint(FinancialInfoModel _financialinfoData)
        {
            var _TraceInfo = await AccountOpService.SaveFinancialInfoMaint(_financialinfoData, GetUserId);
            return Json(new { result = _TraceInfo }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveCollectionCaseInfo(FinancialInfoModel _financialinfoData)
        {
            var _TraceInfo = await AccountOpService.SaveCollectionCaseInfo(_financialinfoData.AcctNo, _financialinfoData.SelectedAssignCollector);
            return Json(new { result = _TraceInfo }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Credit Appl Assessment Info Detail"
        public async Task<ActionResult> ftCreditApplAssessmentInfo(int acctNo)
        {
            var _CaoData = (await AccountOpService.GetCreditApplAssessentForm(Convert.ToString(acctNo), "0")).creditAssesOperation;
            return Json(_CaoData, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> FtCreditApplAssessmentMaint(CreditAssesOperation _CreditApplAssessmentData)
        {
            var _saveCreditAssesOperation = await CardAcctSignUpService.SaveCreditAssessmentOperation(_CreditApplAssessmentData, GetUserId);
            return Json(new { result = _saveCreditAssesOperation }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> WebAcctHistoryListSelect(string acctNo,string type, jQueryDataTableParamModel Params)
        {
            if (!string.IsNullOrEmpty(Params.sSearch)) 
                Params.sSearch = Params.sSearch.ToLower();
            var _filtered = new List<CreditLimitHistory>();
            var list = _filtered;
            if (type.ToLower() == "credit")
                list = (await AccountOpService.GetAcctHistoryByAccount(Convert.ToInt64(acctNo))).creditLimitHistories;
            else if (type.ToLower() == "sec")
                list = (await AccountOpService.GetSecHistoryDepositByAccount(acctNo)).creditLimitHistories;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.CreationDate.ToString()) ? p.CreationDate.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.From) ? p.From : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.To) ? p.To : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = (type.ToLower() == "sec"? _filtered.Select(x => new object[] { x.Field, x.AcctNo, x.DepositType, x.From, x.To, x.UserId, x.CreationDate, })
                         : _filtered.Select(x => new object[] { x.Field, x.AcctNo, x.From, x.To, x.UserId, x.CreationDate, }))
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> SecurityHistoryLogList(int acctNo, jQueryDataTableParamModel Params)
        {
            if (!string.IsNullOrEmpty(Params.sSearch))
                Params.sSearch = Params.sSearch.ToLower();
            var _filtered = new List<CreditLimitHistory>();
            var list = (await AccountOpService.GetSecHistoryDepositByAccount(Convert.ToString(acctNo))).creditLimitHistories;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.Field.ToString()) ? p.Field.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.From) ? p.From : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.To) ? p.To : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] { x.Field, x.AcctNo, x.From, x.To, x.UserId, x.CreationDate, })//, x.XRefCardNo
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> WebRemarksHistoryListSelect(int acctNo,string eventType,jQueryDataTableParamModel Params)
        {
            if (!string.IsNullOrEmpty(Params.sSearch))
                Params.sSearch = Params.sSearch.ToLower();
            var _filtered = new List<RemarkHistory>();
            var list = (await AccountOpService.GetSecDepRemarks(acctNo.ToString(), eventType, string.Empty)).WebSecDepRemarks;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.Content.Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.Content, x.UserId, x.CreationDate, })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Address"
        public async Task<ActionResult> ftAddress(Int64 acctNo, string RefTo, string refcd)
        {
            var data = await CardAcctSignUpService.GetAddressDetail(RefTo, Convert.ToString(acctNo), refcd);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> AddressMaint(AddrListMaintModel _AddressData, string _refto, string _refkey, string _refcd)
        {
            var _TraceInfo = await CardAcctSignUpService.SaveAddressList(_AddressData, _refto, _refcd, _refkey, null, GetUserId);
            return Json(new { result = _TraceInfo }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> DelAddress(string RefTo, string RefKey, string RefCd)
        {
            var _deleteContactMaint = await CardAcctSignUpService.DelAddress(RefTo, RefKey, RefCd, GetUserId);
            return Json(new { resultCd = _deleteContactMaint }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region"Contact"
        public async Task<ActionResult> ftContactDetail(string RefTo, string RefKey, string RefCd)
        {
            var data = (await CardAcctSignUpService.GetContactDetail(RefTo, RefKey, RefCd)).contact;
            return Json(new { contact = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> ContactMaint(ContactLstModel _ContactList, string RefTo, string Func)
        {
            var _saveContactMaint = await CardAcctSignUpService.SaveContactsList(_ContactList, RefTo, Func);
            return Json(new { resultCd = _saveContactMaint }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> DelContact(string RefTo, string RefKey, string RefCd)
        {
            var _deleteContactMaint = await CardAcctSignUpService.DelContact(RefTo, RefKey, RefCd);
            return Json(new { resultCd = _deleteContactMaint }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Cost Centre"
        public async Task<ActionResult> FtCostCentreDetail(CostCentre CostCentre)
        {
            var data = (await AccountOpService.GetCostCentreByRefToAndCostCentre(CostCentre.RefTo, CostCentre.RefKey, CostCentre.SelectedCostCentre)).costCentre;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> CostCentreMaint(CostCentre _CostCentreMaint)
        {
            var saveCostCentre = await AccountOpService.SaveCostCentreMaint(_CostCentreMaint,GetUserId);
            return Json(new { resultCd = saveCostCentre }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> SearchCostCentre(CostCentre CostCentre, jQueryDataTableParamModel Params)
        {
            var _filtered = new List<CostCentre>();
            var list = (await AccountOpService.GetWebCostCentreSearch(CostCentre.RefTo, CostCentre.RefKey, CostCentre.SelectedCostCentre)).costCentres;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.SelectedCostCentre.ToLower().Contains(Params.sSearch) || p.Descp.ToLower().Contains(Params.sSearch) || p.PersoninCharge.ToLower().Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.SelectedCostCentre, x.Descp, x.PersoninCharge })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region"Up Todate Balance"
        public async Task<ActionResult> FtUpToBalDetail(Int64 AcctNo)
        {
            var _UpToDateBal = (await AccountOpService.GetFtUpToBalDetail(Convert.ToString(AcctNo))).upToDateBal;
            return Json(_UpToDateBal, JsonRequestBehavior.AllowGet);
        }

        [CompressFilter]
        public async Task<ActionResult> TxnInstantListSelect(jQueryDataTableParamModel Params, string AcctNo)
        {
            var _filtered = new List<FinancilInfoItemsList>();
            var list = (await AccountOpService.GetTxnInstants(AcctNo)).financilInfoItems;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.TxnId.Contains(Params.sSearch) || p.Lbe.Contains(Params.sSearch) || p.CardNo.Contains(Params.sSearch) || p.TxnAmt.Contains(Params.sSearch) || p.BookingDate.Contains(Params.sSearch) || p.TxnDate.Contains(Params.sSearch) || p.DueDate.Contains(Params.sSearch) || p.Descp.ToLower().Contains(Params.sSearch) || p.UserId.ToLower().Contains(Params.sSearch) || p.CreationDate.Contains(Params.sSearch) || p.RcptNo.Contains(Params.sSearch) || p.TxnCd.ToLower().Contains(Params.sSearch) || p.ShortDescp.ToLower().Contains(Params.sSearch) || p.SiteId.Contains(Params.sSearch) || p.DriverCardNo.Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.TxnId, x.Lbe, x.CardNo, x.TxnAmt, x.BookingDate, x.TxnDate, x.DueDate, x.Descp, x.RcptNo, x.TxnCd, x.ShortDescp, x.SiteId, x.DriverCardNo, x.UserId, x.CreationDate })
            }, JsonRequestBehavior.AllowGet);
        }

        [CompressFilter]
        public async Task<ActionResult> TxnUnpostedListSelect(jQueryDataTableParamModel Params, string AcctNo)
        {
            var _filtered = new List<FinancilInfoItemsList>();
            var list = (await AccountOpService.GetTxnInstantUnposts(AcctNo)).financilInfoItems;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.TxnId.ToString()) ? p.TxnId.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Lbe) ? p.Lbe : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CardNo) ? p.CardNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnAmt) ? p.TxnAmt : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.BookingDate) ? p.BookingDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnDate) ? p.TxnDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.DueDate) ? p.DueDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Descp) ? p.Descp : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnCd) ? p.TxnCd : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ShortDescp) ? p.ShortDescp : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] { x.TxnId, x.Lbe, x.CardNo, x.TxnAmt, x.BookingDate, x.TxnDate, x.DueDate, x.Descp, x.TxnCd, x.ShortDescp, x.UserId, x.CreationDate })
            }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public async Task<ActionResult> GetOnlineTransactionList(jQueryDataTableParamModel Params, string AcctNo,int flag)
        {
            var _filtered = new List<OnlineTransaction>();
            var list = (await AccountOpService.GetOnlineTransactionList(AcctNo,flag)).onlineTransactions;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.TxnId.ToString()) ? p.TxnId.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CardNo) ? p.CardNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnDate) ? p.TxnDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnCd.ToString()) ? p.TxnCd.ToString() : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnAmt.ToString()) ? p.TxnAmt.ToString() : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.InvoiceNo.ToString()) ? p.InvoiceNo.ToString() : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Descp) ? p.Descp : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.BillingAmt.ToString()) ? p.BillingAmt.ToString() : string.Empty).Contains(Params.sSearch)).ToList();

                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            if(flag == 1)
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { x.TxnId, x.TxnDate, x.CardNo, x.TxnAmt, x.BusnLocation, x.TermId, x.InvoiceNo, x.TxnInd, x.Mti, x.PrcsCd, x.Rrn, x.Sts, x.CreationDate })
                }, JsonRequestBehavior.AllowGet);

            }else
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { x.Ids, x.CardNo, x.BillingAmt, x.TxnDate, x.DueDate, x.TxnCd, x.Descp, x.UserId, x.CreationDate })
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        #region "Velocity Limit"
        public async Task<ActionResult> ftVelocityDetail(VeloctyLimitListMaintModel _VelocityLimit, CardnAccNo _CardnAcct)
        {
            _VelocityLimit._CardnAccNo = _CardnAcct;
            var data = (await CardAcctSignUpService.GetCustAcctVelocityDetail(_VelocityLimit)).veloctyLimit;
            return Json(new { velocity = data }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> FtVelocityLimitMaint(VeloctyLimitListMaintModel _VelocityLimitnList, CardnAccNo _cardnAcct, string Func)
        {
            _VelocityLimitnList._CardnAccNo = _cardnAcct;
            var _saveCustAcctVelocity = await objAcctOps.ftCustAcctVelocityMaint(_VelocityLimitnList, Func);
            return Json(new { resultCd = _saveCustAcctVelocity }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> DelVelocityLimit(string AcctNo, string CardNo, string ApplId, string AppcId, string VelInd, string ProdCd, string CostCentre, string corpCd)
        {
            var _deleteCustAcctVelocity = await objAcctOps.DelCustAcctVelocity(AcctNo, CardNo, ApplId, AppcId, VelInd, ProdCd, CostCentre, corpCd);
            return Json(new { resultCd = _deleteCustAcctVelocity }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Location Acceptance"
        [CompressFilter]
        public async Task<ActionResult> ftLocationDetail(string AcctNo, string BusnLoc, string CardNo)
        {
            var data = (await CardHolderService.GetLocationAcceptance(AcctNo, BusnLoc, CardNo)).locationAccept;
            return Json(new { address = data }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> getBusnLocations(string States, string AcctNo = null, string CardNo = null)
        {
            var states = States.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<SelectListItem>();
            foreach (var item in states)
            {
                //var list_item = await WebGetDealerBusnLoc(AcctNo, CardNo, item);
                var list_item = await BaseService.WebGetDealerBusnLoc(AcctNo, CardNo, item);
                foreach (var item2 in list_item)
                {
                    list.Add(new SelectListItem
                    {
                        Text = string.Format("{0}:{1}:{2}", item, item2.Text, item2.Value),
                        Value = item2.Text + ":" + item
                    });
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SaveLocationAccept(LocationAcceptListModel _LocationAcceptList, CardnAccNo _CardnAcctNo)
        {
            _LocationAcceptList._CardnAccNo = _CardnAcctNo;
            var _saveLocationAccept = await CardHolderService.SaveLocationAccept(_LocationAcceptList, _CardnAcctNo.AccNo, _CardnAcctNo.CardNo, GetUserId);
            return Json(new { resultCd = _saveLocationAccept }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> DelLocation(string AcctNo, List<string> BusnLoc, string CardNo)
        {
            var resultCd = await AccountOpService.DeleteLocationAcceptance(AcctNo, BusnLoc, CardNo, GetUserId);
            return Json(new { resultCd = resultCd }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Temporary Credit Limit"
        [CompressFilter]
        public async Task<ActionResult> ftTempCreditCtrlDetail(int AcctNo)//int AcctNo
        {
            var data = (await AccountOpService.GetTempCreditLimitDetail(Convert.ToString(AcctNo))).tempCreditCtrl;
            return Json(new { TempCredit = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> TempCreditCtrlMaint(TempCreditCtrlModel _tempCredit, int acctNo)//, int acctNo
        {
            var _saveTempCreditCtrlMaint = await AccountOpService.SaveTempCreditCtrlMaint(Convert.ToString(acctNo), _tempCredit.AllowCreditLimit, _tempCredit.ExpDateFrom, _tempCredit.ExpDateTo, GetUserId);
            return Json(new { resultCd = _saveTempCreditCtrlMaint }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public async Task<ActionResult> WebEventSearchWithoutDate(jQueryDataTableParamModel Params, EventLogger _Logger)
        {
            var _filtered = new List<EventLogger>();
            var list = (await AccountOpService.GetEventSearchWithoutDate(_Logger.acctNo, "TmpCredit", _Logger.EventDate)).eventLoggers;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.SelectedEventType.ToString()) ? p.SelectedEventType.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.acctNo) ? p.acctNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ClosedDate) ? p.ClosedDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ReminderDatetime) ? p.ReminderDatetime : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.sysInd) ? p.sysInd : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.EventId) ? p.EventId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Description) ? p.Description : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.SelectedEventType, x.acctNo, x.Description, x.sysInd, x.CreationDate, x.UserId })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Status Maintenances"
        [CompressFilter]
        public async Task<ActionResult> ftChangeStatusDetail(string id)
        {
            var data = await CardHolderService.GetChangedAcctStsDetail(id, Request.QueryString["refCd"]);
            return Json(new { address = data.changeStatus }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> FtChangedStatusMaint(ChangeStatus _ChangeStatus, CardnAccNo _cardnAcctNo)
        {
            _ChangeStatus._CardnAccNo = _cardnAcctNo;
            var _SaveChangedStatus = await CardHolderService.StatusSave(_ChangeStatus, GetUserId);

            return Json(new { resultCd = _SaveChangedStatus }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Event Logger"

        public async Task<ActionResult> FtEventDetail(EventLogger _Logger, string eventID)
        {
            var data = (await AccountOpService.GetEventLoggerDetail(_Logger.SelectedModule, eventID)).eventLogger;
            return Json(new { evt = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [CompressFilter]
        public async Task<ActionResult> SaveEventLoggerDetails(EventDetails _loggerDetails)
        {
            var _saveEventDetailLogger = await objAcctOps.SaveEventDetailMaint(_loggerDetails);
            return Json(new { result = _saveEventDetailLogger }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public async Task<ActionResult> EventSearch(jQueryDataTableParamModel Params, EventLogger _Logger)
        {
            var _filtered = new List<EventLogger>();
            var list = (await AccountOpService.GetEventSearch(_Logger)).eventLoggers;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.SelectedEventType.ToString()) ? p.SelectedEventType.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedEventSts) ? p.SelectedEventSts : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.acctNo) ? p.acctNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CardNo) ? p.CardNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedReasonCode) ? p.SelectedReasonCode : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ClosedDate) ? p.ClosedDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ReminderDatetime) ? p.ReminderDatetime : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.sysInd) ? p.sysInd : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.EventId) ? p.EventId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Description) ? p.Description : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] { x.SelectedEventType, x.SelectedEventSts, x.acctNo, x.CardNo, x.SelectedReasonCode, x.ClosedDate, x.ReminderDatetime, x.sysInd, x.EventId, x.Description, x.UserId })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Accct Subsidy"

        public async Task<ActionResult> ftAcctSubsidyInfoList(jQueryDataTableParamModel Params, string AcctNo, string SkdsNo)
        {
            var _filtered = new List<SKDS>();
            var list = await objAcctOps.GetAcctSubsidyInfoList(AcctNo, SkdsNo);

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.VehRegsNo.Contains(Params.sSearch) || p.CardNo.Contains(Params.sSearch) || p.CardSts.ToLower().Contains(Params.sSearch) ||
                    p.SelectedSts.ToLower().Contains(Params.sSearch) || p.SKDSNo.Contains(Params.sSearch) || p.EffFromDate.Contains(Params.sSearch) ||
                    p.EffToDate.Contains(Params.sSearch)).ToList();//).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.VehRegsNo, x.CardNo, x.CardSts, x.SKDSNo, x.SKDSQuota, x.SelectedSts, x.EffFromDate, x.EffToDate })
            }, JsonRequestBehavior.AllowGet);

            //var list = await objAcctOps.GetAcctSubsidyInfoList(AcctNo, SkdsNo);
            //var filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength);
            //return Json(new
            //{
            //    sEcho = Params.sEcho,
            //    iTotalRecords = list.Count(),
            //    iTotalDisplayRecords = list.Count(),

            //    aaData = filtered.Select(x => new object[] { x.VehRegsNo, x.CardNo, x.CardSts, x.SKDSNo, x.SKDSQuota, x.SelectedSts, x.EffFromDate, x.EffToDate })

            //}, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> SaveAcctSubsidyInfoList(List<SKDS> skds, string AcctNo, string SKDSNo)
        {
            var result = await objAcctOps.SaveAcctSubsidyTag(skds, AcctNo, SKDSNo);
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }


        #endregion
        #region "Payment"
        [CompressFilter]
        public async Task<ActionResult> ftPaymentTxnDetail(PaymentTxn _PyTxn)
        {
            var data = (await AccountOpService.GetPaymentTxnDetail(_PyTxn.PyTxnId)).PaymentTxn;
            return Json(new { txn = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> ftPaymentTxnMaint(PaymentTxn _PyTxn)//_PyTxn
        {
            var data = await objAcctOps.FtPaymentTxnMaint(_PyTxn);

            return Json(new { resultCd = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Deposit"
        public async Task<ActionResult> ftGetGetAcctDepositInfoDetail(string AccNo, string TxnId)
        {
            var data = (await CardAcctSignUpService.GetAcctDepositInfoDetail(null, AccNo, TxnId, null)).creditAssesOperation;
            return Json(new { Adi = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> AcctDepositInfoMaint(CreditAssesOperation _AcctDepositInfo, string applId)
        {
            _AcctDepositInfo.UserId = GetUserId;
            var _SaveTxn = await AccountOpService.SaveAcctDepositInfoMaint(_AcctDepositInfo, applId,null);
            return Json(new { result = _SaveTxn }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "AcctGuarantee"
        [HttpPost]
        [CompressFilter]
        public async Task<ActionResult> AcctGuaranteeMaint(AcctGuarantee _AcctGuarantee)
        {
            var _saveAcctGuarantee = await objAcctOps.FtAcctGuaranteeMaint(_AcctGuarantee);
            return Json(new { resultCd = _saveAcctGuarantee }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Point Adjustment"
        public async Task<JsonResult> WebPointAdjustmentListSelect(string AcctNo, jQueryDataTableParamModel Params)
        {
            var _filtered = new List<PointAdjustment>();
            var list = await objAcctOps.WebPointAdjustmentListSelect(AcctNo);

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.TxnType.ToLower().Contains(Params.sSearch) || p.CardNo.Contains(Params.sSearch) || p.Points.Contains(Params.sSearch) || p.TxnDate.Contains(Params.sSearch) || p.TxnDescription.ToLower().Contains(Params.sSearch) || p.SelectedStatus.ToLower().Contains(Params.sSearch) || p.SelectedTxnCd.ToLower().Contains(Params.sSearch) || p.TxnId.Contains(Params.sSearch) || p.WUId.Contains(Params.sSearch) || p.CreationDate.Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.TxnType, x.CardNo, x.Points, x.TxnDate, x.TxnDescription, x.SelectedStatus, x.SelectedTxnCd, x.TxnId, x.WUId, x.CreationDate, x.TxnId })
            }, JsonRequestBehavior.AllowGet);

            //var list = await objAcctOps.WebPointAdjustmentListSelect(AcctNo);
            //var filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength);
            //return Json(new
            //{
            //    sEcho = Params.sEcho,
            //    iTotalRecords = list.Count(),
            //    iTotalDisplayRecords = list.Count(),
            //    aaData = filtered.Select(x => new object[] { 
            //        x.TxnType,x.CardNo,x.Points,x.TxnDate,x.TxnDescription,x.SelectedStatus,x.SelectedTxnCd,x.TxnId,x.WUId,x.CreationDate,x.TxnId
            //    })
            //}, JsonRequestBehavior.AllowGet);

        }


        public async Task<JsonResult> WebPointAdjustmentSelect(string AcctNo, string TxnId)
        {

            var Adj = await objAcctOps.WebPointAdjustmentSelect(TxnId);
            return Json(new { Adj = Adj }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> WebPointAdjustmentMaint(string AcctNo, PointAdjustment Adj)
        {
            var result = await objAcctOps.WebPointAdjustmentMaint(Adj, AcctNo);
            return Json(new { result = result });
        }

        #endregion "POint Adjustment"
        #region "AcctPostedTxnSearch"
        [CompressFilter]
        public async Task<ActionResult> ftAcctPostedTxnSearch(jQueryDataTableParamModel Params, AcctPostedTxnSearch _acctPostedTxnSearch, bool isExport = false)
        {
            var _filtered = new List<AcctPostedTxnSearch>();
            var list = await objAcctOps.FtAcctPostedTxnSearch(_acctPostedTxnSearch);

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.InvoicDt.ToString()) ? p.InvoicDt.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnDate) ? p.TxnDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedCardNo) ? p.SelectedCardNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnDesp) ? p.TxnDesp : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnAmt) ? p.TxnAmt : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Quantity) ? p.Quantity : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Dealer) ? p.Dealer : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.AuthCardNo) ? p.AuthCardNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.PrcsDate) ? p.PrcsDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnId) ? p.TxnId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.RecieptId) ? p.RecieptId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Batch) ? p.Batch : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.VehRegNo) ? p.VehRegNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.DriverName) ? p.DriverName : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TotalTxnAmt) ? p.TotalTxnAmt : string.Empty).Contains(Params.sSearch)).ToList();
                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }

            if (isExport)
            {
                var title = "Transaction Search-" + DateTime.Now.ToString("ddMMyyyyhhmmss");
                var toExport = new List<string[]>();
                var Header = list.First().ExcelHeader;
                foreach (var item in list)
                {
                    toExport.Add(item.ExcelBody());
                }
                var ExcelPkg = CommonHelpers.CreateExcel(Header, toExport, title);
                return File(ExcelPkg.GetAsByteArray(), "application/vnd.ms-excel", title + ".xlsx");
                //return this.File(new UTF8Encoding().GetBytes(toExport.ToString()), "text/csv", "CardsList-"+AcctNo+".csv");
            }
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count,
                iTotalDisplayRecords = list.Count,
                aaData = _filtered.Select(x => new object[] {null, x.InvoicDt, x.TxnDate, x.PrcsDate, x.SelectedCardNo, x.AuthCardNo, x.TxnDesp, x.VehRegNo, x.Stan, x.ApproveCd, x.RRn, x.VATNo, x.Dealer, x.TxnId, x.TxnAmt,
                    x.ProductDescp, x.Quantity, x.ProductAmt, x.VATAmt, x.VATCd, x.VATRate  })
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region "Billing Item"
        [CompressFilter]
        public async Task<ActionResult> ftSearchBillingItem(jQueryDataTableParamModel Params, BillingItem _BillingItem, CardnAccNo _CardnAccNo)
        {
            _BillingItem._CardnAccNo = _CardnAccNo;

            var _filtered = new List<BillingItem>();
            var list = (await AccountOpService.SearchBillingItem(_BillingItem._CardnAccNo.AccNo, _BillingItem.FromDate, _BillingItem.ToDate, _BillingItem.SelectedTxnCategory, _BillingItem.SelectedSts)).BillingItems;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.Level.ToLower().Contains(Params.sSearch) || p.Descp.ToLower().Contains(Params.sSearch) || p.TxnDate.Contains(Params.sSearch) || p.DueDate.Contains(Params.sSearch) || p.DisplayBillingTxnAmt.Contains(Params.sSearch) || p.SettledAmt.Contains(Params.sSearch) || p.SettledDate.Contains(Params.sSearch) || p._CreationDatenUserId.CreationDate.Contains(Params.sSearch) || p.SelectedSts.ToLower().Contains(Params.sSearch) || p.TxnId.ToString().Contains(Params.sSearch) || p.TarBalance.Contains(Params.sSearch) || p.ClosedDate.Contains(Params.sSearch) || p.TotalTxnAmount.Contains(Params.sSearch) || p.TotalSettledAmt.Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.Level, x.Descp, x.TxnDate, x.DueDate, x.DisplayBillingTxnAmt, x.SettledAmt, x.SettledDate, x._CreationDatenUserId.CreationDate, x.TxnId, x.TarBalance, x.ClosedDate, x.TotalTxnAmount, x.TotalSettledAmt })//x.SelectedSts, 
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region "Pukal"
        //[CompressFilter]
        //public async Task<ActionResult> ftWebPukalSelect(String RefKey, String RefTo)
        //{
        //    var data = await objAcctOps.WebPukalSelect(RefKey, RefTo);

        //    return Json(new { resultCd = data }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public async Task<JsonResult> SavePukalInfo(Pukal Pukal, string RefKey)
        {
            var result = await objAcctOps.WebPukalMaint(Pukal, RefKey);

            return Json(new { resultCd = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Pukal"
        #region "Fetch Table List & Details"
        [CompressFilter]
        public async Task<ActionResult> ftTxnAdjList(jQueryDataTableParamModel Params, string AcctNo, bool isExport = false)
        {
            var _filtered = new List<TxnAdjustment>();
            var list = await objTxnAdjustmentOps.GetTxnAdjustmentList(AcctNo);

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p._CardnAccNo.CardNo.Contains(Params.sSearch) || p.TxnDate.Contains(Params.sSearch) || p.DisplayTotAmt.Contains(Params.sSearch) || p.TxnId.Contains(Params.sSearch)).ToList();
                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }

            if (isExport)
            {
                var title = "Transaction Adjustment-" + AcctNo;
                var toExport = new List<string[]>();
                var Header = list.First().Excelheader();
                foreach (var item in list)
                {
                    toExport.Add(item.ExcelBody());
                }
                var ExcelPkg = CommonHelpers.CreateExcel(Header, toExport, title);
                return File(ExcelPkg.GetAsByteArray(), "application/vnd.ms-excel", title + ".xlsx");
                //return this.File(new UTF8Encoding().GetBytes(toExport.ToString()), "text/csv", "CardsList-"+AcctNo+".csv");
            }


            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = _filtered.Select(x => new object[] { x.RefType, x._CardnAccNo.CardNo, x.TxnDate, x.DisplayTotAmt, x.Descp, x.StsDescp, x.UserId, x.TxnId, x.AppvRemarks, x.CreationDate })
            }, JsonRequestBehavior.AllowGet);

        }
        public async Task<ActionResult> ftTxnAdjDetail(TxnAdjustment _TxnAdjustment)
        {
            var data = await objTxnAdjustmentOps.GetTxnAdjustmentDetail(_TxnAdjustment);
            return Json(new { txn = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Save & Delete Info"
        [HttpPost]
        public async Task<ActionResult> SaveTxnAdj(TxnAdjustment _TxnAdjustment, CardnAccNo _CardnAcct)
        {
            _TxnAdjustment._CardnAccNo = _CardnAcct;
            var _SaveTxn = await objTxnAdjustmentOps.SaveTxnAdjustment(_TxnAdjustment);

            return Json(new { txn = _SaveTxn }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> DelTxnAdj(TxnAdjustment _TxnAdjustment, CardnAccNo _CardnAcctNo)
        {
            _TxnAdjustment._CardnAccNo = _CardnAcctNo;
            var _deleteTxnAdj = await objTxnAdjustmentOps.DelTxnAdjustment(_TxnAdjustment);
            return Json(new { resultCd = _deleteTxnAdj }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Account Users"

        [CompressFilter]
        public async Task<ActionResult> AccountUsersListSelect(jQueryDataTableParamModel Params, string AcctNo, String CorpCd)
        {
            var _filtered = new List<AccountUser>();
            var list = await objAcctOps.GetAccountUsers(AcctNo, CorpCd);

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.Username.ToLower().Contains(Params.sSearch) || p.PrivilegeCd.Contains(Params.sSearch) || p.Status.Contains(Params.sSearch)).ToList();
                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }

            if (String.IsNullOrEmpty(CorpCd))
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { x.Username, x.PrivilegeCd, x.Status })
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { x.Username, x.AcctNo, x.CompanyName, x.PrivilegeCd, x.Status })
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public async Task<ActionResult> ResendActivationEmail(ResendAccountMail _Resend)
        {
            var resultCd = await objAcctOps.RensendActivationEmail(_Resend);
            return Json(new { resultCd = resultCd }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ResetPasswordCounter(string AcctNo, string UserId)
        {
            var resultCd = await objAcctOps.ResetPasswordCounter(AcctNo, UserId);
            return Json(new { resultCd = resultCd }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Product Discount"
        public async Task<JsonResult> WebProductDiscountListSelect(string AcctNo, string DiscType, string RefTo, jQueryDataTableParamModel Params)
        {
            var _filtered = new List<ProductDiscount>();
            var list = (await AccountOpService.GetProductDiscounts(AcctNo, DiscType, RefTo)).productDiscounts;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.SelectedProdCd.ToString()) ? p.SelectedProdCd.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ProdCdDescp) ? p.ProdCdDescp : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedProdDiscType) ? p.SelectedProdDiscType : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ProdDiscDescp) ? p.ProdDiscDescp : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedPlanId) ? p.SelectedPlanId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.EffDateFrom) ? p.EffDateFrom : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreatedBy) ? p.CreatedBy : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Remarks) ? p.Remarks : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Id) ? p.Id : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] { x.SelectedProdCd, x.ProdCdDescp, x.SelectedProdDiscType, x.ProdDiscDescp, x.SelectedPlanId, x.EffDateFrom, x.EffDateTo, x.CreatedBy, x.CreationDate, x.Remarks, x.Id })
            }, JsonRequestBehavior.AllowGet);

        }
        public async Task<JsonResult> ProductDiscountSelect(string AcctNo, string DiscType, string Id, string RefTo)
        {
            var prtDiscount = (await AccountOpService.GetProductDiscount(AcctNo, DiscType, Id, RefTo)).productDiscount;
            return Json(new { Discount = prtDiscount }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> PlanIDProdDisc(ProductDiscount _Discount)
        {
            var PlanId = _Discount.PlanId;

            if (_Discount.SelectedProdDiscType == "AMT")
            {
                _Discount.PlanId = await BaseService.WebGetPlan("1");
            }
            else
            {
                _Discount.PlanId = await BaseService.WebGetPlan("2");
            }

            return Json(new { PlanId = _Discount.PlanId }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> ProductDiscountMaint(string AcctNo, ProductDiscount _Discount, string Func, string RefTo)//_Discount _Dicount
        {
            var result = await AccountOpService.SaveProductDiscount(_Discount, AcctNo, Func, RefTo);
            return Json(new { result = result });
        }
        [HttpPost]
        public async Task<JsonResult> DeleteProductDiscount(ProductDiscount _Discount, string AcctNo, string RefTo)
        {
            var result = await AccountOpService.DeleteProductDiscount(_Discount, AcctNo, RefTo);
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region "Acct Milestone"
        [CompressFilter]
        public async Task<ActionResult> WebAcctMilestoneListSelect(jQueryDataTableParamModel Params, Milestone _milestone)
        {
            var _filtered = new List<Milestone>();
            var list = await objAcctOps.WebAcctMilestoneListSelect(_milestone);

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.RefKey.ToString().Contains(Params.sSearch) || p.SelectedTaskNo.Contains(Params.sSearch) || p.selectedPriority.ToLower().Contains(Params.sSearch) || p.selectedStatus.ToLower().Contains(Params.sSearch) || p.CreationDate.Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.RefKey, x.AcctNo, x.SelectedTaskNo, x.TaskDescp, x.selectedPriority, x.selectedStatus, x.CreationDate, x.LastUpdDate })
                //aaData = _filtered.Select(x => new object[] { x.RefKey, x.SelectedTaskNo, x.selectedPriority, x.selectedStatus, x.CreationDate, x.LastUpdDate })// x.selectedReasonCd, x.Remarks, x.Remarks, x.RecallDate,
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Account Event Configuration"

        public async Task<ActionResult> WebEventAcctConfListSelect(jQueryDataTableParamModel Params, string RefTo, string RefKey = "ACCT")
        {
            var _filtered = new List<LookupParameters>();
            var EventAcctConf = await EventConfigService.GetEventAcctConfListSelect(RefTo, RefKey);
            var list = EventAcctConf.lookupParameters;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.EventScheduleId.ToString()) ? p.EventScheduleId.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.type) ? p.type : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ShortDescp) ? p.ShortDescp : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.DetailedDescp) ? p.DetailedDescp : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedStatus) ? p.SelectedStatus : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] { x.EventTypeId, x.EventScheduleId, x.type, x.ShortDescp, x.DetailedDescp, x.SelectedStatus, x.LastUpdated, x.UpdatedBy })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> WebEventAcctConfSelect(string EventTypeId, string EventScheduleId, string AcctNo)
        {
            var Selects = new LookupParameters
            {
                EventType = await BaseService.GetEvtType(),
                Priority = await BaseService.GetRefLib("Priority"),
                Status = await BaseService.GetRefLib("Status"),
                Scope = await BaseService.GetRefLib("Scope"),
                Owner = await BaseService.GetRefLib("NtfEventOwner"),
                Frequency = await BaseService.GetRefLib("NtfEventPeriodType"),
                Languages = await BaseService.GetRefLib("Language"),
                RefTo = await BaseService.GetEvtRefConf(EventTypeId)
            };
            var eventAcctConf = await EventConfigService.GetEventAcctConfSelect(EventTypeId, EventScheduleId, AcctNo);
            var Model = eventAcctConf.lookupParameters;
            return Json(new { Model = Model, Selects = Selects }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> WebEventAcctRcptListSelect(string ScheduleId)
        {
            var eventAcctRcpt = await EventConfigService.GetEventAcctRcpts(ScheduleId);
            var list = Mapper.Map<List<EventRcptDTO>, List<EventRcptList>>(eventAcctRcpt.eventRcpts);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> WebEventAcctConfMaint(LookupParameters Param)
        {
            var result = await EventConfigService.SaveEventAcctConfMaint(Param);
            return Json(result);
        }
        #endregion
    }
}