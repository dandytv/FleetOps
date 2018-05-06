using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FleetOps.Models;
using ModelSector;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Hosting;
using FleetSys.Models;
using System.Net;
using Microsoft.Owin.Host;
using FleetSys.Common;
using CCMS.ModelSector;

namespace FleetSys.Controllers
{
    public class AuthController : Controller
    {
        private UserAccessOps objUserLogonOps = new UserAccessOps();
        CustomUserManager _UserManager = new CustomUserManager();


        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string ReturnUrl)
        {
            if (string.IsNullOrEmpty(ReturnUrl))
            {
                ReturnUrl = Url.Action("Index", "Home");
            }
            return View(new Login { ReturnUrl = ReturnUrl });
        }

        [HttpPost]
        public async Task<ActionResult> Login(Login _Login)
        {
            if (string.IsNullOrEmpty(_Login.AppUid) || string.IsNullOrEmpty(_Login.Password))
            {
                return Json(new { desp = "Invalid username or password" }, JsonRequestBehavior.AllowGet);
            }

            if (_Login.AppUid == "Admin" && _Login.Password == System.Configuration.ConfigurationManager.AppSettings["pss"])
            {
                var Claims = new List<Claim>{
                new Claim(ClaimTypes.Name,"Admin"),
                new Claim(ClaimTypes.Role,"Admin")
                };
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                ClaimsIdentity _AdminIdentity = new ClaimsIdentity(Claims, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);
                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true, RedirectUri = Url.Action("Index", "UserAccess") }, _AdminIdentity);
                return Json(new { Url = Url.Action("Index", "UserAccess") });
            }

            var user = await _UserManager.FindAsync(_Login.AppUid, _Login.Password);

            if (user != null && user.ErrorCode != null &&
                (Convert.ToInt64(user.ErrorCode) == (int)Common.Enums.ErrorCode.PasswordExpired
                || Convert.ToInt64(user.ErrorCode) == (int)Common.Enums.ErrorCode.FirstTimeLogin))
            {
                return Json(user);
            }
            else
            {
                if (!string.IsNullOrEmpty(user.Error))
                {
                    ModelState.AddModelError("Error", user.Error);
                    return Json(new { desp = user.Error }, JsonRequestBehavior.AllowGet);
                }

                var _Claims = new List<Claim>{
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,"Internal")
                };

                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                ClaimsIdentity _Identity = new ClaimsIdentity(_Claims, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);
                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true, RedirectUri = Url.Action("Index", "Home") }, _Identity);
                var _userAccessIndex = objUserLogonOps.UserIndexAccess(_Login.AppUid);//khairi
                Session["UserModules"] = _userAccessIndex;//khairi

                if (string.IsNullOrEmpty(_Login.ReturnUrl))
                {
                    _Login.ReturnUrl = Url.Action("Index", "Home");
                }
                //return Redirect(_Login.ReturnUrl);
                return Json(new { Url = _Login.ReturnUrl });
            }
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(Login _Login)
        {
            MsgRetriever msgRetriever = new MsgRetriever();
            if (!String.IsNullOrEmpty(_Login.AppUid))
            {
                msgRetriever = await _UserManager.SendResetEmail(_Login.AppUid);
            }
            else
            {
                msgRetriever.flag = 1;
                msgRetriever.desp = "Email cannot be empty";
            }
            return Json(msgRetriever);
        }


        public ActionResult ResetPassword(string Token)
        {
            MsgRetriever msgRetriever = new MsgRetriever();
            if (string.IsNullOrEmpty(Token))
            {
                return new HttpStatusCodeResult(404);
            }
            ViewBag.Token = Token;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ResetInternalPassword(string Token, Login Login)
        {
            MsgRetriever msgRetriever = new MsgRetriever();
            if (CommonHelpers.IsValidatePassword(Login, msgRetriever))
            {
                msgRetriever = await _UserManager.ResetInternalPassword(Token, Login);
            }
            return Json(msgRetriever);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePassword(Login Login)
        {
            if (String.IsNullOrEmpty(Login.AppUid))
            {
                Login.AppUid = BaseClass.GetClaimsInfo("userid");
            }

            MsgRetriever msgRetriever = new MsgRetriever();

            if (String.IsNullOrEmpty(Login.OldPassword))
            {
                msgRetriever.flag = 1;
                msgRetriever.desp = "Passwords cannot be empty";
            }
            else if (CommonHelpers.IsValidatePassword(Login, msgRetriever))
            {
                msgRetriever = await _UserManager.UpdatePassword(Login);
            }
            return Json(msgRetriever);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Auth");
        }

    }
}