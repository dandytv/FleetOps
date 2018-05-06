using CardTrend.Common.Helpers;
using CCMS.ModelSector;
using FleetOps.App_Start;
using FleetOps.Models;
using FleetOps.ViewModel;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace FleetSys.Controllers
{
    [Authorize(Users="Admin")]
    public class UserAccessController : BaseController
    {
        private SecurityOps objSecurityOps = new SecurityOps();
        // GET: UserAccess
        public ActionResult Index()
        {
            var viewModel = new ModelSector.UserAccess();
            return View(viewModel);
        }
        public ActionResult Select(string accessInd, string userId)
        {
            var viewModel = new UserAccessViewModel()
            {
                _userAccessPermission = (UserAccessService.GetUserAccessDetail(accessInd, userId)).userAccess
            };
            viewModel._userAccessPermission.SelectedAccessInd = accessInd;
            return View(viewModel);
        }
        public async Task<ActionResult> GetUserAccessDetail(string accessInd, string UserId)
        {
            var _userAccessPermission = (UserAccessService.GetUserAccessDetail(accessInd, UserId)).userAccess;
            var Selects = new UserAccess
            {
                Sts = await BaseService.GetRefLib("UserSts"),
                MapUserId = await UserAccessService.GetMap(),
                AccessInd = await BaseService.GetRefLib("AccessInd"),
                Title = await BaseService.GetRefLib("Title"),
                DeptId = await UserAccessService.GetRefLib("Dept")

            };
            return Json(new { User = _userAccessPermission, AccessInd = accessInd, _Selects = Selects }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> SaveUserAccess(UserAccessViewModel model, bool isUpdate = false)
        {
            string generatedPassword;
            var _permissionAccess = model._userAccessPermission;
            _permissionAccess.Password = AppConfigurationHelper.PasswordGenerator();
            generatedPassword = _permissionAccess.Password;


            _permissionAccess.Password = AppConfigurationHelper.AutoHashing(_permissionAccess.Password);
            var _SaveUserAccess = await UserAccessService.SaveUserAccess(_permissionAccess, isUpdate);
            if(_SaveUserAccess.flag == 0)
            {
                GenerateUserFolder(_permissionAccess.UserId);
            }
            if(_SaveUserAccess.flag == 0 && !string.IsNullOrEmpty(model._userAccessPermission.SelectedMapUserId))
            {
                var _SaveUserAccessMapping = await UserAccessService.SaveUserAccessMapping(model._userAccessPermission);
                if (isUpdate && !_permissionAccess.ChangePasswordInd)
                {
                    return Json(new { resultCd = _SaveUserAccessMapping }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    _SaveUserAccessMapping.desp = _SaveUserAccessMapping.flag == 0 ? _SaveUserAccessMapping.desp + ", password: " + generatedPassword : _SaveUserAccessMapping.desp;
                    return Json(new { resultCd = _SaveUserAccessMapping }, JsonRequestBehavior.AllowGet);
                }
            }
            if (isUpdate && !_permissionAccess.ChangePasswordInd)
            {
                return Json(new { resultCd = _SaveUserAccess }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _SaveUserAccess.desp = _SaveUserAccess.flag == 0 ? _SaveUserAccess.desp + ", password: " + generatedPassword : _SaveUserAccess.desp;
                return Json(new { resultCd = _SaveUserAccess }, JsonRequestBehavior.AllowGet);
            }
        }
        [CompressFilter]
        public ActionResult ftUserAccessList(jQueryDataTableParamModel Params, string _accessInd = null)
        {
            var _filtered = new List<UserAccess>();
            var list = UserAccessService.GetUserAccesses(_accessInd).userAccesses;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.UserId.ToLower().Contains(Params.sSearch) || p.Name.ToLower().Contains(Params.sSearch) || p.EmailAddr.ToLower().Contains(Params.sSearch) || p.SelectedDeptId.ToLower().Contains(Params.sSearch) || p.SelectedMapUserId.ToLower().Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.UserId, x.Name, x.SeletedTitle, x.selectedSts, x.ContactNo, x.EmailAddr, x.SelectedDeptId, x.SelectedMapUserId, x.SelectedAccessInd })

            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ftUserAccessDetail(string accessind, string userid)
        {
            var data = (UserAccessService.GetUserAccessDetail(accessind, userid)).userAccess;
            return Json(new { userAccess = data }, JsonRequestBehavior.AllowGet);
        }
        #region"Fetch List & Save"
        [HttpPost]
        public async Task<ActionResult> SaveWebUserAccessLevel(List<WebModule> ModuleList, List<WebPage> PageList, List<WebControl> CtrlList,List<WebPageSection> SectionList, string UserId)
        {
            var _SaveWebUserAccessLevel = await UserAccessService.SaveWebUserAccessLevel(ModuleList, PageList, CtrlList, SectionList, UserId);
            return Json(new { resultCd = _SaveWebUserAccessLevel }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public ActionResult ftWebUserAccessLevelList(string UserId)
        {
            var model = SecurityOpService.GetUserAccessModuleList(UserId, "0").webModules;
            return Json(new { aaData = model }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public async Task<ActionResult> ftWebGetUserAccessPgNCtrlList(string AccessInd, string UserId, List<string> ModuleList, List<string> PageList, string CtrlId, string mode, string pageId)
        {
            var list = objSecurityOps.WebGetUserAccessPgNCtrlList(AccessInd, UserId, ModuleList, PageList, CtrlId);
            //var list = await SecurityOpService.GetUserAccessPgNCtrlList(AccessInd, UserId, ModuleList, PageList, CtrlId);
            if (mode == "pages")
            {
                return Json(new { aaData = list.Select(p => p._WebPage).ToList() }, JsonRequestBehavior.AllowGet);
                //return Json(new { aaData = list.webPages }, JsonRequestBehavior.AllowGet);
            }
            else if (mode == "controls")
            {
                if(PageList==null)
                    return Json(new { }, JsonRequestBehavior.AllowGet);

                var pageControl = list.Select(p => p._WebPageControl).ToList();
                var pages = list.Select(p => p._WebPageControl).ToList();
                //var pageControl = list.webPageSections;
                //var pages = list.webPageSections;
                var PageSections = new List<WebPageSection>();
                foreach (var x in PageList)
                {
                    var Sessions = (from p in pageControl
                                    join c in pages on p.PageId equals c.PageId
                                    where p.PageId == x
                                    select new { p.Section, p.PageId, c.URL,p.SectionId,p.SectionStatus,p.ModuleId }).Distinct().ToList();

                    foreach (var y in Sessions)
                    {
                        var section = new WebPageSection
                        {
                            Section = y.Section,
                            PageId = y.PageId,
                           ModuleId=y.ModuleId,
                             SectionId=y.SectionId,
                             SectionStatus=y.SectionStatus
                        };

                        section.CtrlList = (from p in pageControl
                                            where p.SectionId == y.SectionId && p.PageId == y.PageId
                                            select new WebControl { CtrlId = p.CtrlId,ModuleId=p.ModuleId,SectionId=p.SectionId, Sts = p.Sts, CtrlDesp = p.Descp, Level = 2, PageId = y.PageId, ShortName = y.URL }).Distinct().ToList();
                        PageSections.Add(section);
                    }
                }

                return Json(new { aaData = PageSections }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
    #endregion
