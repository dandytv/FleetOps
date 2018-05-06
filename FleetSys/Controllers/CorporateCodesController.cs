using AutoMapper;
using CardTrend.Domain.Dto.Corporate;
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
    public class CorporateCodesController : BaseController
    {
        [Accessibility]
        public ActionResult Index()
        {
            return View(new Corporate());  //(_Corporate);
        }

        [AccessibilityXtra]
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                case "gen":
                    return PartialView(this.getPartialPath("CorporateCodes", "Corporate_GeneralInfo_Partial"), new Corporate());
                case "vel":
                    return PartialView(this.getPartialPath("Accounts", "VelocityLimitsListMaint_Partial"), new VeloctyLimitListMaintModel());
                case "con":
                    return PartialView(this.getPartialPath("Applications", "Contact_Partial"), new ContactLstModel());
                case "add":
                    return PartialView(this.getPartialPath("Applications", "Address_Partial"), new AddrListMaintModel());
                case "dep":
                    return PartialView(this.getPartialPath("Applications", "DepositInfo_Partial"), new CreditAssesOperation());
                case "pdc":
                    return PartialView(this.getPartialPath("Accounts", "ProductDiscount_Partial"), new ProductDiscount());
                case "acc":
                    return PartialView(this.getPartialPath("CorporateCodes", "AcctCorpList_Partial"), new GeneralInfoModel());
                case "usm":
                    return PartialView(this.getPartialPath("CorporateCodes", "Corp_Users_Partial"));
                default:
                    return PartialView();
            }
        }

        #region "filldata"
        public async Task<JsonResult> FillData(string Prefix, string CorpCd)
        {
            switch (Prefix)
            {
                case "corp":
                    Corporate _Corporate;
                    var Selects = new Corporate
                    {
                        Ctry = await BaseService.GetRefLib("country"),
                        State = await BaseService.WebGetState("CtryCd"), //GetIssNo, null
                        AddrCd = await BaseService.GetRefLib("Address"),
                        ContactCd = await BaseService.GetRefLib("Contact"),
                    };
                    if (!string.IsNullOrEmpty(CorpCd))
                    {
                        _Corporate = Mapper.Map<Corporate>((await CorporateOpService.GetCorpAcctDetail(CorpCd)).coporate);
                    }
                    else
                    {
                        _Corporate = new Corporate();
                    }
                    return Json(new { Model = _Corporate, Selects = Selects }, JsonRequestBehavior.AllowGet);


                case "pdc":
                    var _prodDiscount = new ProductDiscount
                    {
                        ProdCd = await BaseService.WebProductGroupSelect(),
                        RebatePlan = await BaseService.WebGetPlan("2"),
                        //BaseClass.WebGetPlan("2"),
                        DiscPlan = await BaseService.WebGetPlan("2"),
                        ProdDiscType = await BaseService.GetRefLib("ProdDiscType"),
                        PlanId = new List<SelectListItem>()
                    };
                    return Json(new { Selects = _prodDiscount, Model = new ProductDiscount() }, JsonRequestBehavior.AllowGet);

                default:
                    HttpContext.Response.StatusCode = 404;
                    return Json(null, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion "Filldata"

        #region "CorpCode"
        [CompressFilter]
        public async Task<ActionResult> ftGetCorpAcctList(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<Corporate>();
            var list = (await CorporateOpService.GetCorpAcctList()).corporates;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.CorpCd.ToLower().Contains(Params.sSearch) || p.CorpName.ToLower().Contains(Params.sSearch) || p.DispalyTradeCreditLimit.Contains(Params.sSearch) || p.ComplexInd.ToString().Contains(Params.sSearch) || p.PIC.ToLower().Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.CorpCd, x.CorpName, x.DispalyTradeCreditLimit, x.PIC, x.ComplexInd })//, x.SelectedContactType, x.ContactNo
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SaveCorpAcct(Corporate _Corporate, string func)
        {
            var _SaveCorporate = await CorporateOpService.SaveCorporateAcct(_Corporate, GetUserId, func);
            return Json(new { resultCd = _SaveCorporate });
        }

        public async Task<ActionResult> ftCorpAcctDetail(string Corpd)
        {
            var data = (await CorporateOpService.GetCorpAcctDetail(Corpd)).coporate;
            return Json(new { corp = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "CorpCode"

        #region "Deposit"
        public async Task<ActionResult> GetAcctDepositInfoList(jQueryDataTableParamModel Params, string CorpCd)
        {
            var list = (await AccountOpService.GetAcctDepositInfos(null, null, CorpCd)).creditAssesOperationLst;
            var filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength);

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = list.Select(x => new object[] {
                    x.SelectedDepositType,
                    x.SelectedBankAcctType, 
                    x.SelectedBankName,
                    x.BankAcctNo, 
                    x.DepositAmt, 
                    x.Txnid,
                    x.UserId,
                    x.Creationdt })
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SaveAcctDepositInfoOps(CreditAssesOperation _AcctDepositInfo, string applId, string CorpCd)
        {
            var _SaveTxn = await AccountOpService.SaveAcctDepositInfoMaint(_AcctDepositInfo, applId, CorpCd);
            return Json(new { result = _SaveTxn }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Deposit"

        #region "AcctCorpList"
        [CompressFilter]
        public async Task<ActionResult> ftAcctCorpList(jQueryDataTableParamModel Params, string CorpCd)
        {
            var _filtered = new List<GeneralInfoModel>();
            var list = (await CorporateOpService.GetAcctCorpList(CorpCd)).generalInfoes;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.AcctNo) ? p.AcctNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CmpyName1) ? p.CmpyName1 : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedAcctSts) ? p.SelectedAcctSts : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedPlasticType) ? p.SelectedPlasticType : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CmpyRegsNo) ? p.CmpyRegsNo : string.Empty).Contains(Params.sSearch) ||
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
                aaData = _filtered.Select(x => new object[] { x.AcctNo, x.CmpyName1, x.SelectedAcctSts, x.SelectedPlasticType, x.CmpyRegsNo, x.CreationDate })//, x.SelectedContactType, x.ContactNo
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}