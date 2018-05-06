using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaxMind.GeoIP;
using ModelSector;
using CCMS.ModelSector;
using FleetOps.Models;
using System.Threading.Tasks;
using FleetOps.App_Start;
using FleetOps.ViewModel;
using FleetSys.Models;
using System.IO;
using FleetSys.Common;
using AutoMapper;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.Application;
using CardTrend.Domain.Dto.Corporate;
using System.Globalization;
using CardTrend.Domain.Dto.Applicant;
using CardTrend.Domain.Dto.Account;
using CardTrend.Common.Extensions;
namespace FleetSys.Controllers
{
    [Authorize(Roles = "Internal")]
    public class ApplicationsController : BaseController
    {
        private CardAcctSignUpOps objCardAcctSignUpOps = new CardAcctSignUpOps();
        //private AccountOps objAcctOps = new AccountOps();
        // GET: Applications
        [Accessibility]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [AccessibilityXtra]
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                case "gen":
                    return PartialView(this.getPartialPath("Applications", "ApplicationGeneralInfo_Partial"), new AcctSignUp());
                case "fin":
                    return PartialView(this.getPartialPath("Applications", "FinancialInfo_Partial"), new FinancialInfoModel());
                case "cao":
                    return PartialView(this.getPartialPath("Applications", "AcctSignUp_CreditAssessmentOperation_Partial"), new CreditAssesOperation());
                case "skd":
                    return PartialView(this.getPartialPath("Applications", "DieselSubsidy_Partial"), new SKDS());
                case "vel":
                    return PartialView(this.getPartialPath("Applications", "VelocityLimit_Partial"), new VeloctyLimitListMaintModel());
                case "veh":
                    return PartialView(this.getPartialPath("Applications", "VehiclesList_Partial"), new VehiclesListModel());
                case "app":
                    return PartialView(this.getPartialPath("Applications", "ApplicantsListPartial"));
                case "con":
                    return PartialView(this.getPartialPath("Applications", "Contacts_Partial"), new ContactLstModel());
                case "add":
                    return PartialView(this.getPartialPath("Applications", "Address_Partial"), new AddrListMaintModel());
                case "dep":
                    return PartialView(this.getPartialPath("Applications", "DepositInfo_Partial"), new CreditAssesOperation());
                case "csc":
                    return PartialView(this.getPartialPath("Applications", "CostCentre_Partial"), new CostCentreViewModel());
                case "mis":
                    return PartialView(this.getPartialPath("Applications", "MiscellaniousInfo_Partial"), new MiscellaneousInfoModel());
                case "apr":
                    return PartialView(this.getPartialPath("Applications", "Approval_Partial"), new Milestone());
                case "fil":
                    return PartialView(this.getPartialPath("Applications", "FileManager_Partial"));
                case "ccv":
                    ViewBag.SectionCd = "ccv";
                    return PartialView(this.getPartialPath("Applications", "VelocityLimit_Partial"), new VeloctyLimitListMaintModel());
                default:
                    return PartialView();
            }
        }
        public async Task<JsonResult> FillData(string Prefix, string ApplId)
        {
            switch (Prefix)
            {
                case "gen":
                    var _Info = (await CardAcctSignUpService.GetApplicationGeneralInfo(ApplId)).acctSignUp;
                    var Selects = new AcctSignUp
                    {
                        CycleNo = await BaseService.GetCycle("I"),
                        PlasticType = await BaseService.GetPlasticType(),
                        CorporateAcct = await BaseService.WebGetCorpCd(true),
                        //Position = await BaseClass.WebGetRefLib("Occupation"),
                        CompanyType = await BaseService.GetRefLib("CmpyType"),
                        BillingType = await BaseService.GetRefLib("BillingType"),
                        InvoicePref = await BaseService.GetRefLib("InvPrefer"),
                        BusinessCategory = await BaseService.GetRefLib("BusnCategory"),
                        LangId = await BaseService.GetRefLib("Language"),
                        TaxCategory = await BaseService.GetRefLib("TaxCategory"),
                        NatureOfBusiness = await BaseService.GetRefLib("IndustryCd"),
                        ClientClass = await BaseService.GetRefLib("ClientClass"),
                        ClientType = await BaseService.GetRefLib("ClientType"),
                        PaymentMode = await BaseService.GetRefLib("PaymtMethod"),
                        ReasonCd = await BaseService.GetCAOReasonCd(),
                    };
                    return Json(new { Model = _Info, Selects = Selects }, JsonRequestBehavior.AllowGet);


                case "fin":
                    var _finInfo = (await AccountOpService.GetFinancialInfoForm(Convert.ToInt32(ApplId))).financialInfo;
                    return Json(_finInfo, JsonRequestBehavior.AllowGet);

                case "cao":
                    var _credAssesInfo = (await CardAcctSignUpService.GetCAOGeneralInfo(null, ApplId)).creditAssesOperation;
                    var PaymentTerm = await BaseService.GetRefLib("PaymtTerm");
                    var temp = PaymentTerm.SkipWhile(p => p.Value == "").ToList();
                    PaymentTerm = (temp.OrderBy(p => Convert.ToInt32(p.Value))).ToList();

                    var _CaoSelects = new CreditAssesOperation
                    {
                        PaymentMode = await BaseService.GetRefLib("PaymtMethod"),
                        PaymentTerm = PaymentTerm,
                        TerritoryCd = await BaseService.GetRefLib("SaleTerritory"),
                        RiskCategory = await BaseService.GetRefLib("RiskCategory"),
                        AssesmtType = await BaseService.GetRefLib("AssessmentType"),
                        DepositType = await BaseService.GetRefLib("DepositType"),
                        BankAcctType = await BaseService.GetRefLib("BankAcctType"),
                        BankName = await BaseService.GetRefLib("Bank"),
                        ReasonCd = await BaseService.GetCAOReasonCd(),
                        AppvStsBackOff = await BaseService.GetRefLib("ApplSts"),
                        AppvStsEDP = await BaseService.GetRefLib("ApplSts"),
                        AppvStsQAOff = await BaseService.GetRefLib("ApplSts"),
                        Qualitative = await BaseService.GetRefLib("QualitativeRating"),
                        Quantitative = await BaseService.GetRefLib("QuantitativeRating")
                        ,TradingArea = await BaseService.GetRefLib("TradingArea")
                    };
                    _credAssesInfo.GracePeriod = 20;
                    return Json(new { Model = _credAssesInfo, Selects = _CaoSelects }, JsonRequestBehavior.AllowGet);

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

                case "dep":
                    var _adi = new CreditAssesOperation
                    {
                        DepositType = await BaseService.GetRefLib("DepositType", null, "1"),
                        BankAcctType = await BaseService.GetRefLib("BankAcctType"),
                        BankName = await BaseService.GetRefLib("SecurityDepositBank"),
                    };
                    return Json(new { Selects = _adi, Model = new CreditAssesOperation() }, JsonRequestBehavior.AllowGet);

                case "skd":
                    var Model = new SKDS
                    {
                        UserId = this.GetUserId,
                        CreationDate = NumberExtensions.DateConverter(DateTime.Now.ToShortDateString()),
                    };
                    var selects = new SKDS
                    {
                        Sts = await BaseService.GetRefLib("prodsts"),
                        SubsidyType = await BaseService.GetRefLib("SubsidyLevel"),
                        Category =  BaseService.GetDataVersion().dataVersionLst,
                        SubsidyLevel = await BaseService.GetRefLib("SubsidyLevel")
                    };
                    return Json(new { Selects = selects, Model = Model }, JsonRequestBehavior.AllowGet);

                case "add":
                    var AddrSelects = new AddrListMaintModel
                    {
                        addrtype = await BaseService.GetRefLib("Address"),
                        Country = await BaseService.GetRefLib("Country"),
                        region = await BaseService.GetRefLib("RegionCd"),
                    };
                    var AddrModel = new AddrListMaintModel
                    {
                        UserId = this.GetUserId,
                        CreationDate = System.DateTime.Now.ToString(),
                        RefTo = "APPL"
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

                case "veh":
                    var _VehiclesListModel = new VehiclesListModel
                    {
                        VehColor = await BaseService.GetRefLib("Color"),
                        VehMaker = await BaseService.GetRefLib("VehMaker"),
                        VehModel = await BaseService.GetRefLib("VehSubModel"),
                        //check again
                        CardType = await BaseService.GetCardType(),
                        Sts = await BaseService.GetRefLib("AcctSts"),
                        VehYr = BaseService.WebGetYear(),
                        VehType = await BaseService.GetRefLib("VehType")
                    };
                    return Json(new { Selects = _VehiclesListModel, Model = new VehiclesListModel() }, JsonRequestBehavior.AllowGet);

                case "mis":
                    var _Mis = new MiscellaneousInfoModel
                    {
                        Designation = await BaseService.GetRefLib("Occupation")
                    };
                    return Json(new { Model = new MiscellaneousInfoModel(), Selects = _Mis }, JsonRequestBehavior.AllowGet);
                case "csc":
                    return Json(new { Model = new CostCentre(), Selects = "" }, JsonRequestBehavior.AllowGet);
                case "apr":
                    var _milestone = new Milestone
                    {
                        Priority = await BaseService.GetRefLib("MilestonePriority"),
                        ReasonCd = await BaseService.GetRefLib("ReasonCd"),
                        Status = await BaseService.GetRefLib("MilestoneSts"),
                        Owner = (await UserAccessService.GetUserAccessListSelect()).RefLibLst
                    };

                    var _Status = _milestone.Status.ToList();
                    var PendingItem = _Status.FirstOrDefault(p => p.Value == "P");
                    _Status.Remove(PendingItem);
                    _milestone.Status = _Status;
                    return Json(new { Selects = _milestone, Model = new Milestone() }, JsonRequestBehavior.AllowGet);

                default:
                    HttpContext.Response.StatusCode = 404;
                    return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> ftGetAcctDepositInfoList(jQueryDataTableParamModel Params, string ApplId, string acctNo, string CorpCd)//, string acctNo, string CorpCd
        {
            var _filtered = new List<CreditAssesOperation>();
            var list = (await CardAcctSignUpService.GetAcctDepositInfoList(ApplId, acctNo, CorpCd)).creditAssesOperations;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.SelectedDepositType) ? p.SelectedDepositType : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedBankName) ? p.SelectedBankName : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Txnid) ? p.Txnid : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedBankAcctType) ? p.SelectedBankAcctType : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.BankAcctNo) ? p.BankAcctNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.DepositAmt) ? p.DepositAmt : string.Empty).Contains(Params.sSearch)).ToList();


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
                aaData = _filtered.Select(x => new object[] { x.SelectedDirectDebitInd,
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
        public async Task<ActionResult> ftGetGetAcctDepositInfoDetail(string _applId, string TxnId, string CorpCd)
        {
            var data = (await CardAcctSignUpService.GetAcctDepositInfoDetail(_applId, null, TxnId, CorpCd)).creditAssesOperation;
            return Json(new { Adi = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SaveAcctDepositInfoOps(CreditAssesOperation _AcctDepositInfo, string applId, string CorpCd)//, string acctNo
        {
            _AcctDepositInfo.UserId = GetUserId;
            var _SaveTxn = await AccountOpService.SaveAcctDepositInfoMaint(_AcctDepositInfo, applId, CorpCd);
            return Json(new { result = _SaveTxn }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UploadFiles(string applId, string FolderPath)
        {
            try
            {
                IEnumerable<string> values = Request.Headers.GetValues("ApplId");
                string fileName = string.Empty;
                applId = values.FirstOrDefault().Split(',').First();

                var rootPath = GetDirectory(applId, FolderPath);

                var _Path = Path.Combine(rootPath, applId);
                var files = new List<fileManagerFiles>();
                if (!Directory.Exists(_Path))
                    Directory.CreateDirectory(_Path);
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (StringExtensions.ContainsOneCharacter(file.FileName))
                    {
                        fileName = StringExtensions.TruncatePercents(file.FileName);
                        var FileName = Path.Combine(rootPath, applId, fileName);
                        file.SaveAs(FileName);
                    }
                    else
                    {
                        fileName = file.FileName;
                        var FileName = Path.Combine(rootPath, applId, fileName);
                        file.SaveAs(FileName);
                    }

                   

                    var Info = new fileManagerFiles
                    {
                        FileName = fileName,
                        Size = (file.ContentLength / 1024).ToString(),
                        Extension = MimeMapping.GetMimeMapping(fileName),
                        CreatedDate = DateTime.Now.ToString()
                    };
                    files.Add(Info);
                }
                return Json(files);
            }
            catch (Exception)
            {
                return Json(new { });
            }
        }

        public async Task<ActionResult> GetFiles(string ApplId, string FolderPath)
        {
            var _Dir = GetDirectory(ApplId, FolderPath);
          _Dir = Path.Combine(_Dir, ApplId);
            if (!Directory.Exists(_Dir))
                return Json(new { files = new List<fileManagerFiles>() }, JsonRequestBehavior.AllowGet);
            var _Files = new DirectoryInfo(_Dir).GetFiles();
            var InfoList = new List<fileManagerFiles>();
            foreach (var item in _Files)
            {
                var file = new fileManagerFiles();
                file.FileName = item.Name;
                file.Extension = MimeMapping.GetMimeMapping(item.Name);
                file.CreatedDate = item.CreationTime.ToString();
                file.Size = (item.Length / 1024).ToString();
                InfoList.Add(file);
            }
            return Json(new { files = InfoList }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> RemoveFile(string FileName, string ApplId, string FolderPath)
        {
            var _Dir = GetDirectory(ApplId, FolderPath);
            var _File = Path.Combine(_Dir, ApplId, FileName);
            System.IO.File.Delete(_File);
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> DownloadFile(string FileName, string ApplId, string FolderPath)
        {
            var _Dir = GetDirectory(ApplId, FolderPath);
            var _File = Path.Combine(_Dir, ApplId, FileName);
            Byte[] fileContent = System.IO.File.ReadAllBytes(_File);
            string contentType = MimeMapping.GetMimeMapping(FileName);
            return File(fileContent, contentType, FileName);
        }

        [HttpPost]
        public async Task<ActionResult> SaveMisecInfo(MiscellaneousInfoModel _miscsInfo)
        {
            var _SaveFinancialInfoMaint = await CardAcctSignUpService.SaveMiscellaneousInfo(_miscsInfo);
            return Json(new { resultCd = _SaveFinancialInfoMaint }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ftMiscellaneousInfoDetail(int applId)
        {
            var data = (await CardAcctSignUpService.GetMiscellaneousInfoDetail(applId)).miscellaneousInfo;

            return Json(new { mis = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SaveCreditAssessmentOperation(CreditAssesOperation _SaveCreditAssesOperation)
        {
            var _saveCreditAssesOperation = await CardAcctSignUpService.SaveCreditAssessmentOperation(_SaveCreditAssesOperation, GetUserId);
            return Json(new { resultCd = _saveCreditAssesOperation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveContact(ContactLstModel _ContactList, string RefTo, string Func)
        {
            _ContactList.UserId = GetUserId;
            var _saveContactMaint = await CardAcctSignUpService.SaveContactsList(_ContactList, RefTo, Func);
            return Json(new { resultCd = _saveContactMaint }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> DelContact(string RefTo, string RefKey, string RefCd)
        {
            var _deleteContactMaint = await CardAcctSignUpService.DelContact(RefTo, RefKey, RefCd);
            return Json(new { resultCd = _deleteContactMaint }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public async Task<ActionResult> ftContactList(jQueryDataTableParamModel Params, string RefTo, string RefKey)
        {
            var _filtered = new List<ContactLstModel>();
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
                                            (!string.IsNullOrEmpty(p.EmailAddr) ? p.EmailAddr : string.Empty).Contains(Params.sSearch)).ToList();


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
                aaData = _filtered.Select(x => new object[] { x.RefCd, x.SelectedContactType, x.ContactName, x.ContactNo, x.SelectedSts, x.EmailAddr, x.CreationDate, x.UserId })
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ftContactDetail(string RefTo, string RefKey, string RefCd)
        {
            var data = (await CardAcctSignUpService.GetContactDetail(RefTo, RefKey, RefCd)).contact;
            return Json(new { contact = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveApplicationGeneralInfo(AcctSignUp _ApplicationSignUp)
        {
            var _SaveAcctSignUp = await CardAcctSignUpService.SaveApplicationGeneralInfoResult(_ApplicationSignUp, GetUserId);

            return Json(new { resultCd = _SaveAcctSignUp, ApplId = _SaveAcctSignUp.returnValue.ApplId, EntityId = _SaveAcctSignUp.returnValue.EntityId }, JsonRequestBehavior.AllowGet);
        }

        [CompressFilter]
        // [OutputCache(Duration = 30, VaryByParam = "iDisplayStart")]
        public async Task<ActionResult> ftAcctSignUpList(jQueryDataTableParamModel Params, bool isExport = false)
        {
            var _filtered = new List<AcctSignUp>();
            var list = (await CardAcctSignUpService.GetAcctSignUpList(null, null)).acctSignUps;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (isExport)
            {
                var title = "Applications List-" + DateTime.Now.ToString("ddMMyyyyhhmmss");
                var toExport = new List<string[]>();
                var Header = list.First().Excelheader();
                foreach (var item in list)
                {
                    toExport.Add(item.ExcelBody());
                }
                var ExcelPkg = Common.CommonHelpers.CreateExcel(Header, toExport, title);
                return File(ExcelPkg.GetAsByteArray(), "application/vnd.ms-excel", title + ".xlsx");
                //return this.File(new UTF8Encoding().GetBytes(toExport.ToString()), "text/csv", "CardsList-"+AcctNo+".csv");
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.ApplicationId) ? p.ApplicationId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.AcctNo) ? p.AcctNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CompanyName) ? p.CompanyName : string.Empty).Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] { x.ApplicationId, x.AcctNo, x.CompanyName, x.SelectedCorporateAcct, x.ShownCreditLimit, x.PendingReasons, x.CreationDatenUserid.UserId, x.CreationDatenUserid.CreationDate, x.ApprovedDate, x.AppvCd })
            }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public async Task<ActionResult> ftVelocityList(jQueryDataTableParamModel Params, VeloctyLimitListMaintModel _CustAcctVelocity, CardnAccNo cardnAccNo)
        {
            _CustAcctVelocity._CardnAccNo = cardnAccNo;
            var _filtered = new List<VeloctyLimitListMaintModel>();

            var list = (await CardAcctSignUpService.GetCustAcctVelocityList(_CustAcctVelocity)).veloctyLimits;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {

                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.VelocityIndDescp) ? p.VelocityIndDescp : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedProdCd) ? p.SelectedProdCd : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.velocityCounter) ? p.velocityCounter : string.Empty).Contains(Params.sSearch) ||
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
                aaData = _filtered.Select(x => new object[] {x.SelectedVelocityInd, x.VelocityIndDescp, x.velocityCounter, x.ddlVelocityLimit,x.VelocityLitre,x.SpentCnt, x.SpentLimit, x.SpentLitre, x.LastUpdateDate, x.UserId, x.CreationDate, x.SelectedVelocityInd, x.SelectedProdCd })//, x.ddlVelocityLitre

            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ftVelocityDetail(VeloctyLimitListMaintModel _VelocityLimit, CardnAccNo _CardnAcct)
        {
            _VelocityLimit._CardnAccNo = _CardnAcct;
            var data = (await CardAcctSignUpService.GetCustAcctVelocityDetail(_VelocityLimit)).veloctyLimit;
            return Json(new { velocity = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SaveVelocityLimit(VeloctyLimitListMaintModel _VelocityLimitnList, string applId, CardnAccNo _cardnAcct, string Func)
        {
            _VelocityLimitnList._CardnAccNo = _cardnAcct;
            _VelocityLimitnList.UserId = GetUserId;
            var _saveCustAcctVelocity = await CardAcctSignUpService.SaveCustAcctVelocity(_VelocityLimitnList, applId, Func);
            return Json(new { resultCd = _saveCustAcctVelocity }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> DelVelocityLimit(string AccNo, string CardNo, string ApplId, string AppcId, string VelInd, string ProdCd, string CostCentre, string corpCd)
        {
            var _deleteCustAcctVelocity = await CardAcctSignUpService.DelCustAcctVelocity(AccNo, CardNo, ApplId, AppcId, VelInd, ProdCd, CostCentre, corpCd);
            return Json(new { resultCd = _deleteCustAcctVelocity }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveAddress(AddrListMaintModel _AddressList, string RefCd, string RefTo, string RefKey, string Func)
        {
            var _saveAddressMaint = await CardAcctSignUpService.SaveAddressList(_AddressList, RefTo, RefCd, RefKey, Func, GetUserId);
            return Json(new { resultCd = _saveAddressMaint }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> DelAddress(string RefTo, string RefKey, string RefCd)
        {
            var _deleteAddressResult = await CardAcctSignUpService.DelAddress(RefTo, RefKey, RefCd, GetUserId);
            return Json(new { result = _deleteAddressResult }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public async Task<ActionResult> ftAddressList(jQueryDataTableParamModel Params, string RefTo, string RefKey)
        {
            var _filtered = new List<AddrListMaintModel>();
            var list = (await CardAcctSignUpService.GetAddressList(RefTo, RefKey)).Addresses;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch) && list != null)
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.Address1) ? p.Address1 : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Address2) ? p.Address2 : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Address3) ? p.Address3 : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedAddrType) ? p.SelectedAddrType : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Selectedstate) ? p.Selectedstate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.City) ? p.City : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.PostalCode) ? p.PostalCode : string.Empty).Contains(Params.sSearch)).ToList();


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

                aaData = _filtered.Select(x => new object[] { x.SelectedRefCd, x.SelectedAddrType, x.SelectedMailInd, x.Address1, x.Address2, x.Address3, x.Address4, x.Address5, x.City, x.Selectedstate, x.PostalCode, x.SelectedCountry, x.selectedregion, x.SelectedRefCd })//  


            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ftAddressDetail(string RefTo, string RefKey, string RefCd)
        {
            /// error here
            var data = (await CardAcctSignUpService.GetAddressDetail(RefTo, RefKey, RefCd)).Address;
            return Json(new { address = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> WebGetState(string CountryCd)
        {
            var States = await BaseService.WebGetState(CountryCd);
            return Json(States, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SaveVehicleLimit(VehiclesListModel _VehiclesListModel)
        {
            var _saveCustAcctVelocity = await CardAcctSignUpService.SaveVehicles(_VehiclesListModel, GetUserId);
            return Json(new { resultCd = _saveCustAcctVelocity }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public async Task<ActionResult> ftVehicleList(jQueryDataTableParamModel Params, string ApplId = null, string AcctNo = null, bool isExport = false)
        {
            var _filtered = new List<VehiclesListModel>();
            var list = (await CardAcctSignUpService.GetVehicles(AcctNo, ApplId)).vehicles;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.CardNo) ? p.CardNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedCardType) ? p.SelectedCardType : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.pin) ? p.pin : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.VehRegtNo) ? p.VehRegtNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.VehRegDate) ? p.VehRegDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedVehMaker) ? p.SelectedVehMaker : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedSts) ? p.SelectedSts : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.XrefCardNo) ? p.XrefCardNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.OdoMeterReading) ? p.OdoMeterReading : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.OdoMeterUpdate) ? p.OdoMeterUpdate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.AppcId) ? p.AppcId : string.Empty).Contains(Params.sSearch)).ToList();


                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }

            if (isExport)
            {
                var title = "Vehicle List- Application ID: " + ApplId;
                var toExport = new List<string[]>();
                var Header = list.First().CsvHeader();
                foreach (var item in list)
                {
                    toExport.Add(item.ToCsv());
                }
                var ExcelPkg = Common.CommonHelpers.CreateExcel(Header, toExport, title);
                return File(ExcelPkg.GetAsByteArray(), "application/vnd.ms-excel", title + ".xlsx");
            }
            if (!string.IsNullOrEmpty(AcctNo))
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { x.CardNo, x.SelectedCardType, x.pin, x.VehRegtNo, x.SelectedSts, x.XrefCardNo, x.OdoMeterReading, x.CardExpiry, x.PolicyExpDate, x.AppcId })
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { x.AppcId, x.SelectedCardType, x.pin, x.VehRegtNo, x.VehRegDate, x.SelectedSts, x.XrefCardNo, x.OdoMeterReading, x.CardExpiry, x.PolicyExpDate, x.AppcId })
                }, JsonRequestBehavior.AllowGet);
            }

        }
        public async Task<ActionResult> ftVehicleDetail(VehiclesListModel _VehiclesListModel)
        {
            var data = (await CardAcctSignUpService.GetVehicleDetail(_VehiclesListModel.AppcId, _VehiclesListModel.CardNo, _VehiclesListModel.VehRegtNo)).vehicle;
           return Json(new { vehicle = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SaveSKDS(SKDS _SKDS, CardnAccNo _CardnAcctNo)
        {
            _SKDS._CardnAccNo = _CardnAcctNo;
            _SKDS.UserId = GetUserId;
            var _SaveSkds = await CardAcctSignUpService.SaveSKDS(_SKDS);
            return Json(new { result = _SaveSkds }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public async Task<ActionResult> ftSKDSList(jQueryDataTableParamModel Params, SKDS _SKDS, CardnAccNo _CardnAcct)
        {
            _SKDS._CardnAccNo = _CardnAcct;
            var _filtered = new List<SKDS>();
            var list = (await CardAcctSignUpService.GetSKDSList(_SKDS._CardnAccNo.AccNo, _SKDS.ApplId,null)).skdses;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.SKDSNo) ? p.SKDSNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnId) ? p.TxnId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.EffFromDate) ? p.EffFromDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.EffToDate) ? p.EffToDate : string.Empty).Contains(Params.sSearch) ||
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
                aaData = _filtered.Select(x => new object[] { x.SKDSNo, x.SelectedSubsidyLevel, x.SKDSLitreQuota, x.EffFromDate, x.EffToDate, x.Refference, x.UserId, x.CreationDate, x.TxnId })
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ftSKDSDetail(SKDS _SKDS, CardnAccNo _CardNAcctNo)
        {
            _SKDS._CardnAccNo = _CardNAcctNo;
            var data = (await CardAcctSignUpService.GetSKDSDetail(_SKDS._CardnAccNo.AccNo, _SKDS.ApplId, _SKDS.TxnId)).skds;
            return Json(new { subs = data }, JsonRequestBehavior.AllowGet);
        }

        public string GetDirectory(string ApplId, string FolderPath)
        {
            string _Dir = string.Empty;

            if (!String.IsNullOrEmpty(FolderPath))
            {
                if (FolderPath.ToLower() == Convert.ToString((int)Enums.FileFolderPath.Fraud).ToLower())
                {
                    _Dir = System.Configuration.ConfigurationManager.AppSettings["uploadPathFraud"];
                }
                else
                {
                    //amended by max, 20160512 - just to make sure do not miss out any possibilities
                    _Dir = System.Configuration.ConfigurationManager.AppSettings["uploadPath"];
                }
            }
            else
            {
                _Dir = System.Configuration.ConfigurationManager.AppSettings["uploadPath"];
            }

            return _Dir;
        }

        #region "Milestone"
        [CompressFilter]
        public async Task<ActionResult> WebMilestoneListSelect(jQueryDataTableParamModel Params, Milestone _milestone)
        {
            var _filtered = new List<Milestone>();
            _milestone.UserId = GetUserId;
            var list = await CardAcctSignUpService.WebMilestoneListSelect(_milestone.UserId, _milestone.workflowcd,_milestone.Ind);

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }


            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.RefKey.ToString()) ? p.RefKey.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CompanyName) ? p.CompanyName : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedTaskNo) ? p.SelectedTaskNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.selectedPriority) ? p.selectedPriority : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.selectedStatus) ? p.selectedStatus : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).Contains(Params.sSearch)).ToList();
                
                
                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            if (_milestone.workflowcd == "Adj" || _milestone.workflowcd == "Pymt")
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { x.RefKey, x.AcctNo, x.CompanyName, x.SelectedTaskNo, x.TaskDescp, x.selectedPriority, x.selectedStatus, x.CreationDate, x.LastUpdDate })
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { x.RefKey, x.CompanyName, x.SelectedTaskNo, x.TaskDescp, x.selectedPriority, x.selectedStatus, x.CreationDate, x.LastUpdDate })
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public async Task<ActionResult> WebMilestoneHistorySelect(Milestone _milestone)
        {
            var data = (await CardAcctSignUpService.GetMilestoneHistorySelect(_milestone.workflowcd, _milestone.RefKey)).milestoneHistories;
            return Json(new { result = data, user = GetUserId }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> WebMilestoneApplValidation(Milestone _milestone)
        {
            var data = (await CardAcctSignUpService.MilestoneApplValidation(_milestone.aprId)).mileStoneInfo;
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SaveMileStone(Milestone _milestone)
        {
            var SaveMile = await CardAcctSignUpService.SaveMilestone(_milestone);
            return Json(new { result = SaveMile }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ftMilestoneInfo(Milestone _milestone)
        {
            var data = (await CardAcctSignUpService.GetMilestoneInfo(_milestone.workflowcd, Convert.ToInt32(_milestone.SelectedTaskNo))).mileStoneInfo;
            data.aprId = _milestone.aprId;
            if (!string.IsNullOrEmpty(data.validationSP))
            {
                // before await CardAcctSignUpService.MilestoneApplValidation(data.aprId)
                data = (await CardAcctSignUpService.MilestoneApplValidation(data.aprId)).mileStoneInfo;
            }
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetTaskNo(int CurrentTaskNo, string WorkFlowCd)//, string WorkFlowCd
        {
            var data = (await BaseService.GetNextMilestone(CurrentTaskNo, WorkFlowCd)).RefLibLst;
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> GetTaskNos(List<int> TaskNos, string WorkFlowCd)
        {
            var _list = new List<IEnumerable<SelectListItem>>();
            foreach (var item in TaskNos)
            {
                var data = (await BaseService.GetNextMilestone(item, WorkFlowCd)).RefLibLst;
                _list.Add(data);
            }
            return Json(new { list = _list }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> FtMileStoneInfoList(List<Milestone> _milestones)
        {
            var _list = new List<Milestone>();
            foreach (var item in _milestones)
            {
                var data = (await CardAcctSignUpService.GetMilestoneInfo(item.workflowcd, Convert.ToInt32(item.SelectedTaskNo))).mileStoneInfo;
                data.aprId = item.aprId;
                if (!string.IsNullOrEmpty(data.validationSP))
                {
                  data = (await CardAcctSignUpService.MilestoneApplValidation(data.aprId)).mileStoneInfo;
                }
                _list.Add(data);
            }
            return Json(new { lsItems = _list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveMilestoneAdj(Milestone _milestone)
        {
            var SaveMileAdj = await CardAcctSignUpService.SaveMileStoneAdj(_milestone);
            return Json(new { result = SaveMileAdj }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveMilestonePayment(Milestone _milestone)
        {
            var SaveMilePymt = await objCardAcctSignUpOps.SaveMilestonePayment(_milestone);
            return Json(new { result = SaveMilePymt }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveMilestoneMerchant(Milestone _milestone)
        {
            var SaveMileMerch = await objCardAcctSignUpOps.SaveMilestoneMerchant(_milestone);
            return Json(new { result = SaveMileMerch }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
