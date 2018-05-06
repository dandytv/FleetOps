using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using FleetSys.Models;
using System;
using FleetOps.Models;
using System.Net.Mail;
using CCMS.ModelSector;
using System.Linq;
using System.Text.RegularExpressions;
using CardTrend.Common.Helpers;

namespace FleetSys.Models
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class CustomUserManager : UserManager<ApplicationUser>
    {
        private UserAccessOps objUserLogonOps = new UserAccessOps();

        public CustomUserManager()
            : base(new CustomUserStore<ApplicationUser>())
        {
            this.PasswordHasher = new CustomPasswordHasher();
        }
        public async override Task<ApplicationUser> FindAsync(string userName, string password)
        {
            Task<ApplicationUser> taskInvoke = Task<ApplicationUser>.Factory.StartNew(() =>
            {

                var hashed = this.PasswordHasher.HashPassword(password);

                string ReturnCode = objUserLogonOps.SaveUserLogin(userName, hashed);
              // var msgReceiver = BaseClass.GetMessageCode( Convert.ToInt32( ReturnCode));
                var obj = objUserLogonOps.GetAccessFlag(ReturnCode);
                var result = objUserLogonOps.Cast(obj, new { message = "", ind = true });
                if (!result.ind)
                {
                    var applicationUser = new ApplicationUser
                    {
                        Error = result.message,// , msgReceiver.Result.desp
                        ErrorCode = ReturnCode 
                    };
                    return applicationUser;
                }
                else
                {
                    var applicationUser = new ApplicationUser
                    {
                        Username = userName
                    };
                    return applicationUser;
                }
            });

            return await taskInvoke;
        }
        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public async Task<MsgRetriever> SendResetEmail(string email)
        {
            var result = await objUserLogonOps.RecoverInternalPassword(email);
            return result;
        }

        public async Task<MsgRetriever> ResetInternalPassword(string Token, Login _Login)
        {
            _Login.Password = this.PasswordHasher.HashPassword(_Login.Password);
            var result = await objUserLogonOps.ResetInternalUserPassword(Token, _Login);
            return result;
        }

        public async Task<MsgRetriever> UpdatePassword(Login _UserAccess)
        {
            _UserAccess.OldPassword = this.PasswordHasher.HashPassword(_UserAccess.OldPassword);
            _UserAccess.Password = this.PasswordHasher.HashPassword(_UserAccess.Password);
            _UserAccess.ConfirmPassword = this.PasswordHasher.HashPassword(_UserAccess.ConfirmPassword);
            var result = await objUserLogonOps.UpdatePassword(_UserAccess);
            return result;
        }
    }

    public class CustomPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            return AppConfigurationHelper.AutoHashing(password);
        }

    }
    public class CustomUserStore<T> : IUserStore<T> where T : ApplicationUser
    {
        public Task CreateAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }

}
