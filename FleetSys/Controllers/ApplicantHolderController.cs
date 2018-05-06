using System.Web.Mvc;
using FleetOps.Models;
using FleetOps.ViewModel;
using ModelSector;
using FleetOps.DAL;
using System.Linq;
using System.Collections;
using FleetOps.App_Start;
using CCMS.ModelSector;
using FleetSys.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace FleetSys.Controllers
{
    public class ApplicantHolderController : BaseClass
    {
        // GET: ApplicantHolder
        private ApplicantHolderOps objCardHoldersOps = new ApplicantHolderOps();
        //private CardAcctMaintOps objCardAcctMaintOps = new CardAcctMaintOps();
        private CardAcctSignUpOps objCardAcctSignUpOps = new CardAcctSignUpOps();
        public ActionResult Index()
        {
            return View();
        }

        #region "General Info"
        #region "Table"
        [CompressFilter]
        public async Task<ActionResult> ftCardHolderList(jQueryDataTableParamModel Params, string _AcctNo, bool isExport = false)
        {
            //byte[] Bytes;
            var list = await objCardHoldersOps.CardHolderList(_AcctNo);
            var filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength);

            var xx = from x in filtered
                     select new
                     {
                         x.CardNo,
                         x.EmbossName
                     };

            var aaData = filtered.Select(x => new object[] { x.CardNo, x.EmbossName, x.SelectedCurrentStatus, x.CardExpiry, x.XRefCardNo, x.SelectedCardType, x.SelectedPINInd, x.vehRegNo, x.DriverCd, x.FullName, x.BlockedDate, x.TerminatedDate, x.SelectedCostCentre });
            if (isExport)
            {
                var toExport = new List<string[]>();
                var Header = list.First().ExcelHeader;
                foreach (var item in list)
                {
                    toExport.Add(item.ExcelBody());
                }
                var ExcelPkg =CreateExcel(Header, toExport, "CardsList-" + _AcctNo);
                return File(ExcelPkg.GetAsByteArray(), "application/vnd.ms-excel", string.Format("List of Cards-{0}.xlsx", _AcctNo));
            }
            return Json(new
            {

                sEcho = Params.sEcho,
                iTotalRecords = list.Count,
                iTotalDisplayRecords = list.Count,
                aaData = aaData
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Table"

        #region "DropDown"
        //for dropdown function
        [Accessibility]
        public async Task<ActionResult> New2()
        {

            _cardApplInfoSignUp = new CardHolderInfoModel
            {
                CardType = await BaseClass.WebGetCardType(null, null, null, Request.QueryString["AcctNo"]),
                DialogueInd = await BaseClass.WebGetRefLib("DialogueInd"),
                CurrentStatus = await BaseClass.WebGetRefLib("AppcSts"),
                SKDSNo = await BaseClass.WebGetSKDS(Request.QueryString["AcctNo"], null),
                PINInd = await BaseClass.WebGetRefLib("PinInd"),
                ProductUtilization = await BaseClass.WebProductGroupSelect(1),
                VehicleModel = await BaseClass.WebGetRefLib("VehSubModel"),
                AnnualFee = await BaseClass.WebGetFeeCd("ANN"),
                JonFee = await BaseClass.WebGetFeeCd("JON"),
                BranchCd = await WebGetRefLib("BranchCd"),
                DivisionCode = await WebGetRefLib("DivisionCd"),
                DeptCd = await WebGetRefLib("DeptCd")
            };

            
            ViewBag.AcctNo = Request.QueryString["AcctNo"];
            return View(_cardApplInfoSignUp);
        }
        #endregion "Dropdown"

        #region "form"
        public async Task<ActionResult> ftGeneralInfoSelect(string _cardNo)
        {
            var data = await objCardHoldersOps.GeneralInfoSelect(_cardNo);

            return Json(new { info = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "form"

        #region "Save"
        [HttpPost]
        public async Task<ActionResult> SaveAppcGeneralInfo(CardHolderInfoModel _CardData)
        {
            var SaveApplGeneralInfoMaint = await objCardHoldersOps.SaveGeneralInfo(_CardData);
            return Json(new { resultCd = SaveApplGeneralInfoMaint}, JsonRequestBehavior.AllowGet);
        }

        #endregion "Save"
        #endregion "General Info"

        #region "Financial"
        #region "form"
        public async Task<ActionResult> ftFinancialInfoSelect(int _IssNo, string _CardNo)
        {
            var data = await objCardHoldersOps.FinancialInfoSelect(_IssNo, _CardNo);

            return Json(new { FinInfo = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "form"

        #region "Save"
        [HttpPost]
        public async Task<ActionResult> SaveFinancialInfo(CardFinancialInfoModel finInfo, string _CardNo)
        {
            var _SaveFinancial = await objCardHoldersOps.SaveFinancialInfo(finInfo, _CardNo);
            return Json(new { resultCd = _SaveFinancial }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Save"
        #endregion "Financial"

        #region "Velocity"
        #region "Table"
        [CompressFilter]
        public async Task<ActionResult>ftVelocityList(jQueryDataTableParamModel Params, VeloctyLimitListMaintModel VelocityList, CardnAccNo cardnAccNo)
        {
            VelocityList._CardnAccNo = cardnAccNo;
            var list = await objCardHoldersOps.VelocityList(VelocityList);
            var filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength);
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = filtered.Select(x => new object[] { x.VelocityIndDescp,
                    x.ProdCdDescp,  x.velocityCounter,x.ddlVelocityLimit, x.ddlVelocityLitre,x.SpentCnt,x.SpentLimit,x.SpentLitre,x.LastUpdateDate, x.UserId,x.CreationDate,x.SelectedVelocityInd,x.SelectedProdCd})
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Table"

        #region "Form"
        public async Task<ActionResult> ftVelocityLimitSelect(int _IssNo, string _AccNo, string _CardNo, string _ApplId, string _AppcId, string _SelectedVelocityInd, string _SelectedProdCd)
        {
            var data = await objCardHoldersOps.VelocityLimitSelect(_IssNo, _AccNo, _CardNo, _ApplId, _AppcId, _SelectedVelocityInd, _SelectedProdCd);

            return Json(new { velocity = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Form"

        #region "Save"
        [HttpPost]
        public async Task<ActionResult> SaveVelocityLimitMaint(VeloctyLimitListMaintModel VelocityInfo, string _Func)
        {
            var _saveVelocity = await objCardHoldersOps.SaveVelocityLimit(VelocityInfo, _Func);
            return Json(new { resultCd = _saveVelocity }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Save"
        #endregion "Velocity"

        #region "Person Info"
        #region "form"
        public async Task<ActionResult> ftPersonInfoSelect(int _issNo, string _entityId)
        {
            var data = await objCardHoldersOps.PersonInfoSelect(_issNo, _entityId);

            return Json(new { PersonInfo = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "form"

        #region "save"
        [HttpPost]
        public async Task<ActionResult> SavePersonInfo(PersonInfoModel PersonInfo, string _entityID)
        {
            var _SavePersonInfo = await objCardHoldersOps.SavePersonInfo(PersonInfo, _entityID);
            return Json(new { resultCd = _SavePersonInfo }, JsonRequestBehavior.AllowGet);
        }
        #endregion "save"
        #endregion "Person Info"

        #region "Location Acceptance"
        #region "Table"
        [CompressFilter]
        public async Task<ActionResult>ftLocationList(jQueryDataTableParamModel Params, string AcctNo, CardnAccNo _CardnAccNo)
        {
            var list = await objCardHoldersOps.GetLocationAcceptance(AcctNo, _CardnAccNo);

            var filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength);


            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = filtered.Select(x => new object[] { x._CardnAccNo.CardNo, x.DBAName, x.s_state, x.UserId, x.CreationDate })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Table"

        #region "form"
        public async Task<ActionResult> ftLocationAcceptanceSelect(string _AcctNo, string _BusnLoc)
        {
            var data = await objCardHoldersOps.LocationAcceptanceSelect(_AcctNo, _BusnLoc);

            return Json(new { address = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "form"

        #region "Save"
        [HttpPost]
        public ActionResult SaveLocationAccept(LocationAcceptListModel _LocationAcceptList, CardnAccNo _CardnAcctNo)
        {
            var _saveLocationAccept = objCardHoldersOps.SaveLocationAccept(_LocationAcceptList);
            return Json(new { resultCd = _saveLocationAccept }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Save"
        #endregion "Location Acceptance"

        #region "Contact List"
        #region "Table"
        [CompressFilter]
        public async Task<ActionResult>ftContactList(jQueryDataTableParamModel Params, string _RefTo, string _RefKey)
        {
            var list = await objCardHoldersOps.ContactList(_RefTo, _RefKey);

            var filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength);


            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = filtered.Count(),
                iTotalDisplayRecords = filtered.Count(),
                aaData = filtered.Select(x => new object[] { x.SelectedContactType, x.ContactName, x.ContactNo, x.SelectedSts, x.SelectedOccupation, x.EmailAddr, x.CreationDate, x.UserId, x.RefCd })

            }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Table"

        #region "form"
        public async Task<ActionResult> ftContactListSelect(string _RefTo, string _RefKey, string _RefCd)
        {
            var data = await objCardHoldersOps.ContactListSelect(_RefTo, _RefKey, _RefCd);

            return Json(new { contact = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "form"

        #region "Save"
        [HttpPost]
        public async Task<ActionResult> SaveAddr(ContactLstModel ContactLst, string _RefTo, string _Func)
        {
            var SaveAddrInfo = await objCardHoldersOps.SaveContact(ContactLst, _RefTo, _Func);
            return Json(new { AddrInfo = SaveAddrInfo }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Save"
        #endregion "Contact List"

        #region "Address List"
        #region "Table"
        [CompressFilter]
        public async Task<ActionResult> ftAddressList(jQueryDataTableParamModel Params, string RefTo, string RefKey)
        {
            var list = await objCardHoldersOps.GetAddrList(RefTo, RefKey);
            var filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength);

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count,
                iTotalDisplayRecords = list.Count,

                aaData = filtered.Select(x => new object[] { x.SelectedAddrType, x.Address, x.District, x.City, x.addrtype, x.PostalCode, x.Country, x.SelectedRefCd, x.CreationDate, x.region, x.City, x.SelectedCountry, x.MainMailingInd, x.selectedregion, x.Selectedstate, x.State, x.UserId })

            }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Table"

        #region "Form"
        public async Task<ActionResult> ftAddressListSelect(string _RefTo, string _RefKey, string _RefCd)
        {
            var data = await objCardHoldersOps.AddressListSelect(_RefTo, _RefKey, _RefCd);

            return Json(new { address = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Form"

        #region "Save"
        [HttpPost]
        public async Task<ActionResult> SaveAddressList(AddrListMaintModel AddressList, string _RefTo, string _RefCd, string _RefKey)
        {
            var _saveAddressMaint = await objCardHoldersOps.SaveAddress(AddressList, _RefTo, _RefCd, _RefKey);
            return Json(new { resultCd = _saveAddressMaint }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Save"
        #endregion "Address List"

        #region "Status Maintanence"
        #region "form"
        public async Task<ActionResult> ftStatusMaintSelect(string _id, string _refCd)
        {
            var data = await objCardHoldersOps.StatusMaintSelect(_id, _refCd);

            return Json(new { Sts = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "form"

        #region "Save"
        [HttpPost]
        public async Task<ActionResult> SaveChangedStatus(ChangeStatus ChangeSts)
        {
            var _SaveChangedStatus = await objCardHoldersOps.SaveStsMaint(ChangeSts);
            return Json(new { resultCd = _SaveChangedStatus }, JsonRequestBehavior.AllowGet);
        }
        #endregion "Save"
        #endregion "Status Maintanence"

        #region "Card Replacement"
        #region "form"
        public async Task<ActionResult> ftCardReplacementInfoSelect(string _CardNo)
        {
            var data = await objCardHoldersOps.CardReplacementInfoSelect(_CardNo);

            return Json(new { CardReplace = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion "form"

        #region "Save"
        [HttpPost]
        public async Task<ActionResult> SaveCardReplacementInfo(CardReplacement CardReplace)
        {
            var _SaveChangedStatus = await objCardHoldersOps.SaveCardReplacement(CardReplace);
            return Json(new { resultCd = _SaveChangedStatus }, JsonRequestBehavior.AllowGet); //, newCardNo = objCardHoldersOps.NewcardNo 
        }
        #endregion "Save"
        #endregion "Card Replacement"
    }
}