using CardTrend.Business;
using CardTrend.Business.CcmsServices;
using CardTrend.Business.CcmsWebServices;
using CCMS.ModelSector;
using FleetSys.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utilities.DAL;

namespace FleetSys.Controllers
{
    public class BaseController : Controller
    {
        #region private properties
        private IBaseService _baseService;
        private ICardAcctSignUpService _cardAcctSignUpService;
        private IAccountOpService _accountOpService;
        private ICollectionOpService _collectionOpService;
        private IUserAccessService _userAccessService;
        private ICorporateOpService _coporateService;
        private IGlobalVariableOpService _globalVariableOpService;
        private IMechSignUpService _mechSignUpService;
        private IManualSlipEntryOpService _manualSlipEntryService;
        private IMultiPaymentOpService _multipPaymentOpService;
        private IMultipleTxnOpService _multipleTxnOpService;
        private IAccountSOAOpService _accountSOAOpService;
        private IReportOpService _reportOpService;
        private IPinMailerOpService _pinMailerOpService;
        private IPukalAcctOpService _pukalAcctOpService;
        private IMerchMultitxnAdjustmentService _merchMultitxnAdjustmentService;
        private ITransactionSearchService _transactionSearchService;
        private IFraudOpService _fraudOpService;
        private IEventConfigService _eventConfigService;
        private INotifSearchService _notifSearchService;
        private ICardHolderService _cardHolderService;
        private IApplicantSignUpService _applicantSignUpService;
        private ISecurityOpService _securityOpService;
        #endregion private properties
        #region public properties
        public IBaseService BaseService
        {
            get
            {
                if (_baseService == null)
                {
                    _baseService = new BaseService();
                }
                return _baseService;
            }
        }
        public ICardAcctSignUpService CardAcctSignUpService
        {
            get
            {
                if (_cardAcctSignUpService == null)
                {
                    _cardAcctSignUpService = new CardAcctSignUpService();
                }
                return _cardAcctSignUpService;
            }
        }
        public IAccountOpService AccountOpService
        {
            get
            {
                if (_accountOpService == null)
                {
                    _accountOpService = new AccountOpService();
                }
                return _accountOpService;
            }
        }
        public ICollectionOpService CollectionOpService
        {
            get
            {
                if(_collectionOpService == null)
                {
                    _collectionOpService = new CollectionOpService();
                }
                return _collectionOpService;
            }
        }
        public IUserAccessService UserAccessService
        {
            get
            {
                if (_userAccessService == null)
                {
                    _userAccessService = new UserAccessService();
                }
                return _userAccessService;
            }
        }
        public ICorporateOpService CorporateOpService
        {
            get
            {
                if (_coporateService == null)
                {
                    _coporateService = new CorporateOpService();
                }
                return _coporateService;
            }
        }
        public IGlobalVariableOpService GlobalVariableOpService
        {
            get
            {
                if (_globalVariableOpService == null)
                {
                    _globalVariableOpService = new GlobalVariableOpService ();
                }
                return _globalVariableOpService;
            }
        }
        public IMechSignUpService MechSignUpService
        {
            get
            {
                if(_mechSignUpService == null)
                {
                    _mechSignUpService = new MechSignUpService();
                }
                return _mechSignUpService;
            }
        }
        public IManualSlipEntryOpService ManualSlipEntryOpService
        {
            get
            {
                if(_manualSlipEntryService == null)
                {
                    _manualSlipEntryService = new ManualSlipEntryOpService();
                }
                return _manualSlipEntryService;
            }
        }
        public IMultiPaymentOpService MultiPaymentOpService
        {
            get
            {
                if(_multipPaymentOpService == null)
                {
                    _multipPaymentOpService = new MultiPaymentOpService();
                }
                return _multipPaymentOpService;
            }
        }
        public IMultipleTxnOpService MultipleTxnOpService
        {
            get
            {
                if(_multipleTxnOpService == null)
                {
                    _multipleTxnOpService = new MultipleTxnOpService();
                }
                return _multipleTxnOpService;
            }
        }
        public IAccountSOAOpService AccountSOAOpService
        {
            get
            {
                if(_accountSOAOpService == null)
                {
                    _accountSOAOpService = new AccountSOAOpService();
                }
                return _accountSOAOpService;
            }
        }
        public IReportOpService ReportOpService
        {
            get
            {
                if(_reportOpService == null)
                {
                    _reportOpService = new ReportOpService();
                }
                return _reportOpService;
            }
        }
        public IPinMailerOpService PinMailerOpService
        {
            get
            {
                if(_pinMailerOpService == null)
                {
                    _pinMailerOpService = new PinMailerOpService();
                }
                return _pinMailerOpService;
            }
        }
        public IPukalAcctOpService PukalAcctOpService
        {
            get
            {
                if(_pukalAcctOpService == null)
                {
                    _pukalAcctOpService = new PukalAcctOpService();
                }
                return _pukalAcctOpService;
            }
        }
        public IMerchMultitxnAdjustmentService MerchMultitxnAdjustmentService
        {
            get
            {
                if (_merchMultitxnAdjustmentService == null)
                {
                    _merchMultitxnAdjustmentService = new MerchMultitxnAdjustmentService();
                }
                return _merchMultitxnAdjustmentService;
            }
        }
        public ITransactionSearchService TransactionSearchService
        {
            get
            {
                if(_transactionSearchService == null)
                {
                    _transactionSearchService = new TransactionSearchService();
                }
                return _transactionSearchService;
            }
        }
        public IFraudOpService FraudOpService
        {
            get
            {
                if (_fraudOpService == null)
                {
                    _fraudOpService = new FraudOpService();
                }
                return _fraudOpService;
            }
        }
        public IEventConfigService EventConfigService
        {
            get
            {
                if(_eventConfigService == null)
                {
                    _eventConfigService = new EventConfigService();
                }
                return _eventConfigService;
            }
        }
        public INotifSearchService NotifSearchService
        {
            get
            {
                if(_notifSearchService == null)
                {
                    _notifSearchService = new NotifSearchService();
                }
                return _notifSearchService;
            }
        }
        public ICardHolderService CardHolderService
        {
            get
            {
                if(_cardHolderService == null)
                {
                    _cardHolderService = new CardHolderService();
                }
                return _cardHolderService;
            }
        }
        public IApplicantSignUpService ApplicantSignUpService
        {
            get
            {
                if (_applicantSignUpService == null)
                {
                    _applicantSignUpService = new ApplicantSignUpService();
                }
                return _applicantSignUpService;
            }
        }
        public ISecurityOpService SecurityOpService
        {
            get
            {
                if (_securityOpService == null)
                {
                    _securityOpService = new SecurityOpService();
                }
                return _securityOpService;
            }
        }
        #endregion
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            filterContext.Controller.TempData["ExcMessage"] = filterContext.Exception.Message;
        }
        public static string GetClaimsInfo(string type)
        {
            string Value = String.Empty;
            var Identity = ClaimsPrincipal.Current.Identities.First();
            if (type.ToLower() == "userid")
            {
                Value = Identity.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name).Value;
            }
            return Value;
        }
        public string GetUserId
        {
            get
            {
                //_UserManager = new CustomUserManager();
                return GetClaimsInfo("userid");
            }
        }
        public bool GenerateUserFolder(string userId)
        {
            string subfileRootImages, subfileRootDownload, subfileRootUpload, subfileCustomise, subfileRootFile;
            if (!Directory.Exists(userId))
            {
                try
                {
                    subfileRootFile = System.Web.HttpContext.Current.Server.MapPath("~/Users/") + userId;
                    string tempSubFileRoot = subfileRootFile.Replace("\\", "/");
                    Directory.CreateDirectory(tempSubFileRoot);
                    if (!string.IsNullOrEmpty(tempSubFileRoot) || !string.IsNullOrWhiteSpace(tempSubFileRoot))
                    {
                        subfileRootDownload = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Users/") + userId + "/" + "Download/");
                        string tempSubFileRootDownload = subfileRootDownload.Replace("\\", "/");
                        Directory.CreateDirectory(tempSubFileRootDownload);
                        subfileRootUpload = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Users/") + userId + "/" + "Upload/");
                        string tempSubFileRootUpload = subfileRootUpload.Replace("\\", "/");
                        Directory.CreateDirectory(tempSubFileRootUpload);
                        subfileRootImages = System.Web.HttpContext.Current.Server.MapPath("~/Users/") + userId + "/" + "Images";
                        string tempSubFileImage = subfileRootImages.Replace("\\", "/");
                        Directory.CreateDirectory(tempSubFileImage);
                        if (!string.IsNullOrEmpty(tempSubFileImage) || !string.IsNullOrWhiteSpace(tempSubFileImage))
                        {
                            subfileCustomise = Path.Combine(tempSubFileImage + "/" + "Custom");
                            string tempSubFileCustomise = subfileCustomise.Replace("\\", "/");
                            Directory.CreateDirectory(tempSubFileCustomise);
                            return true;
                        }
                        else
                        {
                            ModelState.AddModelError("Error", "Custom Folder Fail To Created");
                            return false;
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Folders Fail To Created");
                        return false;
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Error", "Folders Fail To Created");
                    return false;
                }

            }
            ModelState.AddModelError("Error", " User Folders Was Created Before");
            return false;

        }
        public string getPartialPath(string dir, string name)
        {
            return String.Format("~/Views/Shared/PartialViews/{0}/{1}.cshtml", dir, name);
        }
        public string ToCSV(DataTable table)
        {
            var columnHeaders = (from DataColumn x in table.Columns
                                 select x.ColumnName).ToArray();
            StringBuilder builder = new StringBuilder(String.Join(";", columnHeaders));
            builder.Append("\n");

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    builder.Append(row[i].ToString());
                    builder.Append(i == table.Columns.Count - 1 ? "\n" : ";");
                }
            }

            return builder.ToString();
        }
        public BaseController()
            : base()
        {

        }
    }
}