using System.Web.Mvc;
using FleetOps.Models;
using ModelSector;
using System.Linq;
using FleetOps.App_Start;
using CCMS.ModelSector;
using FleetSys.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using CardTrend.Domain.Dto.CardHolder;
using CardTrend.Domain.Dto.Applicant;
namespace FleetSys.Controllers
{
    [Authorize(Roles = "Internal")]
    public class ApplicantController : BaseController
    {
        // GET: Card
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
                    return PartialView(this.getPartialPath("Applicant", "CardAppc_AppcGeneralInfo_Partial"), new CardAppcInfoModel());
                case "per":
                    return PartialView(this.getPartialPath("Applicant", "CardAppc_PersonInfo_Partial"), new PersonInfoModel());
                case "fin":
                    return PartialView(this.getPartialPath("Applicant", "CardAppc_FinancialInfoCard_Partial"), new CardFinancialInfoModel());
                case "Vel":
                    return PartialView(this.getPartialPath("Applicant", "CardAppc_VelocityLimits_Partial"), new VeloctyLimitListMaintModel());
                case "add":
                    return PartialView(this.getPartialPath("Applicant", "CardAppc_AddressList_Partial"), new AddrListMaintModel());
                case "sts":
                    return PartialView(this.getPartialPath("Applicant", "CardAppc_StatusMaint"), new ChangeStatus());
                case "con":
                    return PartialView(this.getPartialPath("Applicant", "CardAppc_ContactsList_Partial"), new ContactLstModel());
                case "app":
                    return PartialView(this.getPartialPath("Applicant", "ApplicantList_Partial"));
                default:
                    return PartialView();
            }
        }

        #region "Fill Data"
        public async Task<JsonResult> FillData(string Prefix, string AcctNo, string AppcId, string ApplId)
        {
            switch (Prefix)
            {
                case "gen":
                    var _GeData = (await ApplicantSignUpService.GetApplicantInfo(ApplId, AppcId, AcctNo)).cardAppcInfo;
                    var CardData = new CardAppcInfoModel
                     {
                         CardType = await BaseService.GetCardType(AppcId, null, ApplId, AcctNo),
                         PinInd = await BaseService.GetRefLib("PinInd"),
                         SKDSNo = await BaseService.GetSKDS(ApplId, AcctNo),
                         DialogueInd = await BaseService.GetRefLib("DialogueInd"),
                         CurrentStatus = await BaseService.GetRefLib("AppcSts"),
                         ProductUtilization = await BaseService.WebProductGroupSelect(),
                         VehicleModel = await BaseService.GetRefLib("VehType"),
                         CostCentre = !string.IsNullOrEmpty(ApplId) ? await BaseService.GetCostCentre(ApplId, "Appl",true) : await BaseService.GetCostCentre(AcctNo, "Acct",true),
                         AnnualFee = await BaseService.GetFeeCd("ANN"),
                         JoiningFee = await BaseService.GetFeeCd("JON"),
                         BranchCd = await BaseService.GetRefLib("BranchCd"),
                         DivisionCode = await BaseService.GetRefLib("DivisionCd"),
                         DeptCd = await BaseService.GetRefLib("DeptCd"),
                         CardMedia = await BaseService.GetCardMedia(),
                     };
                    return Json(new { Model = _GeData, Selects = CardData }, JsonRequestBehavior.AllowGet);
                case "fin":
                    var data = (await ApplicantSignUpService.GetFinancialInfo(AppcId)).cardFinancialInfo;
                    return Json(new { Model = data }, JsonRequestBehavior.AllowGet);

                case "per":
                    var _perData = (await CardHolderService.GetPersonInfo(Request.QueryString["EntityId"])).personalInfo;
                    var perSelects = new PersonInfoModel
                    {
                        title = await BaseService.GetRefLib("Title"),
                        IdType = await BaseService.GetRefLib("IcType"),
                        AltIdType = await BaseService.GetRefLib("IcType"),
                        gender = await BaseService.GetRefLib("Gender"),
                        Occupation = await BaseService.GetRefLib("Occupation"),
                    };
                    return Json(new { Model = _perData, Selects = perSelects }, JsonRequestBehavior.AllowGet);

                case "sts":
                    var stsDetails = await CardHolderService.GetChangedAcctStsDetail(AcctNo, AppcId);
                    var selecs = new ChangeStatus
                    {
                        CurrentStatus = await BaseService.GetRefLib("CardSts"),
                        RefType = await BaseService.GetRefLib("EventType"),
                        ReasonCode = await BaseService.GetRefLib("ReasonCd", "64"),
                        ChangeStatusTo = await BaseService.GetRefLib("AcctSts", "")
                    };
                    return Json(new { Model = stsDetails.changeStatus, Selects = selecs }, JsonRequestBehavior.AllowGet);

                default:
                    HttpContext.Response.StatusCode = 404;
                    return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion "Fill Data"
        #region "General Info"
        [HttpPost]
        public async Task<ActionResult> SaveNewCardInfo(CardAppcInfoModel _cardRawData, string AccNo, string _AppcID)
        {
            _cardRawData.AcctNo = AccNo;
            var _SaveApplGeneralInfoMaint = await ApplicantSignUpService.SaveApplicantInfo(_cardRawData, null, _AppcID, GetUserId);
            return Json(new { resultCd = _SaveApplGeneralInfoMaint, AppcID = _SaveApplGeneralInfoMaint.returnValue.AppcId, EntityId = _SaveApplGeneralInfoMaint.returnValue.EntityId }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveApplicantInfo(CardAppcInfoModel _CardData, string _ApplID, string _AppcID)
        {
            var _SaveApplGeneralInfoMaint = await ApplicantSignUpService.SaveApplicantInfo(_CardData, _ApplID, _AppcID, GetUserId);
            return Json(new { resultCd = _SaveApplGeneralInfoMaint, AppcID = _SaveApplGeneralInfoMaint.returnValue.AppcId, EntityId = _SaveApplGeneralInfoMaint.returnValue.EntityId }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public async Task<ActionResult> ftCardHolderList(jQueryDataTableParamModel Params, string _ApplicationId, string _AcctNo)
        {
            var _filtered = new List<CardAppcInfoModel>();
            var list = (await ApplicantSignUpService.GetApllicants(_ApplicationId, _AcctNo)).cardAppcInfoLst;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.AppcId.ToString().Contains(Params.sSearch) || p.SelectedCardType.ToLower().Contains(Params.sSearch) || p.EmbossName.ToLower().Contains(Params.sSearch) || p.DriverName.ToLower().Contains(Params.sSearch) || p.vehRegNo.Contains(Params.sSearch) || p.CardNo.Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.AppcId,
                          x.SelectedCardType, 
                          x.EmbossName,
                          x.DriverName,
                          x.vehRegNo,
                          x.CardNo,
                          x.SelectedCurrentStatus
                 
                    })
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ftApplicantInfoSelect(string applidData, string appcidData, string acctNo)
        {
            var data = (await ApplicantSignUpService.GetApplicantInfo(applidData, appcidData, acctNo)).cardAppcInfo;
            return Json(new { CardHolder = data }, JsonRequestBehavior.AllowGet);
        }

        #endregion "General Info"

        #region "Person Info"
        [HttpPost]
        public async Task<ActionResult> SavePersonInfo(PersonInfoModel _SavePersonInfo, string entityId)
        {
            var _SavePersonInfoMaint = await CardHolderService.SavePersonInfo(_SavePersonInfo, entityId);
            return Json(new { resultCd = _SavePersonInfoMaint }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ftPersonInfoSelect(int _issNo, string _entityId)
        {
            var _SelectPersonInfo = (await CardHolderService.GetPersonInfo(_entityId)).personalInfo;
            return Json(new { result = _SelectPersonInfo }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Person Info"

        #region "Financial"
        [HttpPost]
        public async Task<ActionResult> SaveFinancialInfo(CardFinancialInfoModel cardfinInfo, string _AppcId)
        {
            var _SaveFinancialInfoMaint = await ApplicantSignUpService.SaveFinancial(cardfinInfo, _AppcId);
            return Json(new { resultCd = _SaveFinancialInfoMaint }, JsonRequestBehavior.AllowGet);
        }

        #endregion "Financial"

        #region "Status"
        [HttpPost]
        public async Task<ActionResult> SaveChangedStatus(ChangeStatus ChangeSts, CardnAccNo _CardNAcct)
        {
            ChangeSts._CardnAccNo = _CardNAcct;          
            var _SaveChangedStatus = await CardHolderService.StatusSave(ChangeSts, GetUserId);
            return Json(new { resultCd = _SaveChangedStatus }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ftChangeStatusDetail(string AcctNo, string AppcId)
        {
            var data = await CardHolderService.GetChangedAcctStsDetail(AcctNo, AppcId);
            return Json(new { address = data.changeStatus }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Status"

    }
}