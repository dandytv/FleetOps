using System;
using System.Collections.Generic;
using System.Web;
using Utilities.DAL;
using System.Data;
using System.Data.SqlClient;
using ModelSector;
using System.Web.Security;
using CCMS.ModelSector;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
namespace FleetOps.Models
{
    public class UserAccessOps : BaseClass
    {
        #region "UserAccess"
        public List<UserAccess> GetUserAccessList(string AccessInd)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(AccessInd) ? new SqlParameter("@AccessInd", DBNull.Value) : new SqlParameter("@AccessInd", AccessInd);
                var execResult = objDataEngine.ExecuteCommand("WebUserAccessListSelect", CommandType.StoredProcedure, Parameters);
                var _GetUserAccessList = new List<UserAccess>();

                while (execResult.Read())
                {
                    _GetUserAccessList.Add(new UserAccess
                    {
                        UserId = Convert.ToString(execResult["UserId"]),
                        Name = Convert.ToString(execResult["UserName"]),
                        SeletedTitle = Convert.ToString(execResult["Title"]),
                        selectedSts = Convert.ToString(execResult["Status"]),
                        ContactNo = Convert.ToString(execResult["ContactNo"]),
                        EmailAddr = Convert.ToString(execResult["EmailAddress"]),
                        SelectedDeptId = Convert.ToString(execResult["DeptId"]),
                        SelectedAccessInd = Convert.ToString(execResult["AccessInd"])
                    });
                };
                return _GetUserAccessList;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public UserAccess GetUserAccessDetail(string AccessInd, string UserId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@AccessInd", String.IsNullOrEmpty(AccessInd) ? "" : AccessInd);
                Parameters[2] = new SqlParameter("@UserId", String.IsNullOrEmpty(UserId) ? "" : UserId);


                var execResult = objDataEngine.ExecuteCommand("WebUserAccessSelect", CommandType.StoredProcedure, Parameters);
                var _UserAccess = new UserAccess();
                while (execResult.Read())
                {
                    _UserAccess.UserId = Convert.ToString(execResult["UserId"]);
                    _UserAccess.SelectedMapUserId = Convert.ToString(execResult["AccessTmpl"]);
                    _UserAccess.Name = Convert.ToString(execResult["Name"]);
                    _UserAccess.selectedSts = Convert.ToString(execResult["Sts"]);
                    _UserAccess.ContactNo = Convert.ToString(execResult["ContactNo"]);
                    _UserAccess.EmailAddr = Convert.ToString(execResult["EmailAddr"]);
                    _UserAccess.SeletedTitle = Convert.ToString(execResult["Title"]);
                    _UserAccess.SelectedDeptId = Convert.ToString(execResult["DeptId"]);
                    _UserAccess.PrivilegeCd = Convert.ToString(execResult["PrivilegeCd"]);
                    _UserAccess.LastLogin = Convert.ToString(execResult["LastLogin"]);
                    _UserAccess.CreationDate = Convert.ToString(execResult["CreationDate"]);
                    _UserAccess.SelectedAccessInd = Convert.ToString(execResult["AccessInd"]);
                };
                return _UserAccess;
            }
            finally
            {

                objDataEngine.CloseConnection();
            }




        }
        public async Task<MsgRetriever> SaveUserAccess(UserAccess _UserAccess, bool isUpdate = false)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[15];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_UserAccess.SelectedAccessInd) ? new SqlParameter("@AccessInd", DBNull.Value) : new SqlParameter("@AccessInd", _UserAccess.SelectedAccessInd);
                Parameters[2] = String.IsNullOrEmpty(_UserAccess.selectedSts) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts ", _UserAccess.selectedSts);
                Parameters[3] = String.IsNullOrEmpty(_UserAccess.UserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId ", _UserAccess.UserId);
                Parameters[4] = String.IsNullOrEmpty(_UserAccess.Password) ? new SqlParameter("@Pw", DBNull.Value) : new SqlParameter("@Pw", _UserAccess.Password);
                Parameters[5] = String.IsNullOrEmpty(_UserAccess.Name) ? new SqlParameter("@Name", DBNull.Value) : new SqlParameter("@Name ", _UserAccess.Name);
                Parameters[6] = String.IsNullOrEmpty(_UserAccess.ContactNo) ? new SqlParameter("@ContactNo", DBNull.Value) : new SqlParameter("@ContactNo ", _UserAccess.ContactNo);
                Parameters[7] = String.IsNullOrEmpty(_UserAccess.EmailAddr) ? new SqlParameter("@EmailAddr", DBNull.Value) : new SqlParameter("@EmailAddr ", _UserAccess.EmailAddr);
                Parameters[8] = String.IsNullOrEmpty(_UserAccess.SeletedTitle) ? new SqlParameter("@Title", DBNull.Value) : new SqlParameter("@Title ", _UserAccess.SeletedTitle);
                Parameters[9] = String.IsNullOrEmpty(_UserAccess.SelectedDeptId) ? new SqlParameter("@DeptId", DBNull.Value) : new SqlParameter("@DeptId ", _UserAccess.SelectedDeptId);
                Parameters[10] = String.IsNullOrEmpty(_UserAccess.PrivilegeCd) ? new SqlParameter("@PrivilegeCd", DBNull.Value) : new SqlParameter("@PrivilegeCd ", _UserAccess.PrivilegeCd);
                Parameters[11] = String.IsNullOrEmpty(_UserAccess.CreatedBy) ? new SqlParameter("@CreatedBy", DBNull.Value) : new SqlParameter("@CreatedBy ", _UserAccess.CreatedBy);
                Parameters[12] = new SqlParameter("@ChangePassInd", BaseClass.ConvertBoolDB(_UserAccess.ChangePasswordInd));
                Parameters[13] = new SqlParameter("@Flag", isUpdate ? "U" : "N");
                Parameters[14] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[14].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebUserAccessMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                if (Descp.flag == 0)
                {
                    //this.GenerateUserFolder(_UserAccess.UserId);
                    return Descp;
                }
                else
                {
                    return Descp;
                }
            }
            finally
            {

                objDataEngine.CloseConnection();
            }
        }
        #endregion
        #region "UserLogon"
        public string SaveUserLogin(string Username, string Password)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = String.IsNullOrEmpty(Username) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", Username);
                Parameters[1] = String.IsNullOrEmpty(Password) ? new SqlParameter("@Pw", DBNull.Value) : new SqlParameter("@Pw", Password);
                Parameters[2] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[2].Direction = ParameterDirection.ReturnValue;
                var Cmd = objDataEngine.ExecuteWithReturnValue("WebUserSignIn", CommandType.StoredProcedure, Parameters);

                var Result = Convert.ToString(Cmd.Parameters["@RETURN_VALUE"].Value);
                return Result;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }


        public bool GetUserExists(string UserId, string Password)
        {

            var _Users = new List<Users>();
            var Reader = new StreamReader(System.IO.File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("~/Markups/internal_Users.csv")));
            while (!Reader.EndOfStream)
            {
                var line = Reader.ReadLine();
                var values = line.Split(',');
                _Users.Add(new Users
                {
                    Username = values[0],
                    Password = values[1]
                });
            }
            if (_Users.Where(p => p.Username.ToLower().Contains(UserId.ToLower()) && p.Password.Contains(Password)).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public object GetAccessFlag(string val)
        {
            string error = string.Empty;
            bool ind;
            switch (val)
            {
                case "60004":
                    error = "User Account not found";
                    break;
                case "95184":
                    error = "Invalid Password";
                    break;
                case "70922":
                    error = "Please call system administrator for help";
                    break;
                case "70923":
                    error = "Failed to update user profile";
                    break;
                case "95008":
                    error = "User has been disabled";
                    break;
                case "95670":
                    error = "Your password exceeds 90 days"; //Your password exceeds 90 days. Reset your pasword
                    break;
                case "95674":
                    error = "Please change your password Upon first logon"; //Please change your password Upon first logon
                    break;
                case "0":
                    break;
                default:
                    error = "Login failed";
                    break;
            }
            ind = string.IsNullOrEmpty(error) ? true : false;
            return new { message = error, ind = ind };
        }


        public T Cast<T>(object obj, T type)
        {
            return (T)obj;
        }

        #endregion
        #region "UserPasswordPermit"
        public string SaveUserPasswordPermit(forgetPassword _forgetUserPass)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[6];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_forgetUserPass.SelectedAccessInd) ? new SqlParameter("@AccessInd", DBNull.Value) : new SqlParameter("@AccessInd", _forgetUserPass.SelectedAccessInd);
                Parameters[2] = String.IsNullOrEmpty(_forgetUserPass.EmailAddr) ? new SqlParameter("@EmailAddr", DBNull.Value) : new SqlParameter("@EmailAddr", _forgetUserPass.EmailAddr);
                Parameters[3] = String.IsNullOrEmpty(_forgetUserPass.secureCd) ? new SqlParameter("@SecureCd", DBNull.Value) : new SqlParameter("@SecureCd", _forgetUserPass.secureCd);
                Parameters[4] = String.IsNullOrEmpty(_forgetUserPass.IpAddr) ? new SqlParameter("@IPAddr", DBNull.Value) : new SqlParameter("@IPAddr", _forgetUserPass.IpAddr);
                Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[5].Direction = ParameterDirection.ReturnValue;
                var Cmd = objDataEngine.ExecuteWithReturnValue("WebUserAccessInfoVerify", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToString(Cmd.Parameters["@RETURN_VALUE"].Value);
                return Result;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion
        #region "UserPasswordConfirm"
        public string SaveUserPasswordConfirm(forgetPassword _UserPassConfirm)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[6];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_UserPassConfirm.EmailAddr) ? new SqlParameter("@EmailAddr", DBNull.Value) : new SqlParameter("@EmailAddr", _UserPassConfirm.EmailAddr);
                Parameters[2] = String.IsNullOrEmpty(_UserPassConfirm.password) ? new SqlParameter("@Pw", DBNull.Value) : new SqlParameter("@Pw", _UserPassConfirm.password);
                Parameters[3] = String.IsNullOrEmpty(_UserPassConfirm.secureCd) ? new SqlParameter("@SecureCd", DBNull.Value) : new SqlParameter("@SecureCd", _UserPassConfirm.secureCd);
                Parameters[4] = String.IsNullOrEmpty(_UserPassConfirm.IpAddr) ? new SqlParameter("@IPAddr", DBNull.Value) : new SqlParameter("@IPAddr", _UserPassConfirm.IpAddr);
                Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[5].Direction = ParameterDirection.ReturnValue;
                var Cmd = objDataEngine.ExecuteWithReturnValue("WebUserAccessPwMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToString(Cmd.Parameters["@RETURN_VALUE"].Value);
                return Result;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion
        #region "UserIndexAccess"
        public List<UserIndexAccess> UserIndexAccess(string UserId = null)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);
            if (UserId == null)
                UserId = this.GetUserId;

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = String.IsNullOrEmpty(UserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", UserId);
                Parameters[1] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[1].Direction = ParameterDirection.ReturnValue;
                var execResult = objDataEngine.ExecuteCommand("WebGetIndexAccess", CommandType.StoredProcedure, Parameters);

                var userAccessList = new List<UserIndexAccess>();
                while (execResult.Read())
                {
                    var userAccess = new UserIndexAccess()
                    {
                        url = Convert.ToString(execResult["url"]),
                        descp = Convert.ToString(execResult["Descp"]),
                        //attrId = Convert.ToString(execResult["attrid"]),
                        //icon = Convert.ToString(execResult["icon"])
                    };
                    userAccessList.Add(userAccess);
                }               
                return userAccessList;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }



        }
        #endregion

        public bool SignOutUser()
        {

            try
            {
                FormsAuthentication.SignOut();
                HttpCookie myCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
                myCookie.Expires = DateTime.Now.AddYears(-5);
                HttpContext.Response.Cookies.Add(myCookie);
                Session.Abandon();
                HttpCookie rSessionCookie = new HttpCookie("ASP.NET_SessionId", "");
                rSessionCookie.Expires = DateTime.Now.AddYears(-5);
                HttpContext.Response.Cookies.Add(rSessionCookie);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<MsgRetriever> SaveWebUserAccessMapping(UserAccess _userAccess, bool isUpdate = true)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Web);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[10];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@UserId", _userAccess.UserId);
                Parameters[2] = String.IsNullOrEmpty(_userAccess.SelectedMapUserId) ? new SqlParameter("@MapUserId", DBNull.Value) : new SqlParameter("@MapUserId", _userAccess.SelectedMapUserId);
                Parameters[3] = String.IsNullOrEmpty(_userAccess.Name) ? new SqlParameter("@ContactName", DBNull.Value) : new SqlParameter("@ContactName", _userAccess.Name);
                Parameters[4] = String.IsNullOrEmpty(_userAccess.ContactNo) ? new SqlParameter("@ContactNo", DBNull.Value) : new SqlParameter("@ContactNo", _userAccess.ContactNo);
                Parameters[5] = String.IsNullOrEmpty(_userAccess.SelectedDeptId) ? new SqlParameter("@DeptId", DBNull.Value) : new SqlParameter("@DeptId", _userAccess.SelectedDeptId);
                Parameters[6] = String.IsNullOrEmpty(_userAccess.SelectedAccessInd) ? new SqlParameter("@AccessInd", DBNull.Value) : new SqlParameter("@AccessInd", _userAccess.SelectedAccessInd);
                Parameters[7] = String.IsNullOrEmpty(_userAccess.EmailAddr) ? new SqlParameter("@EmailAddr", DBNull.Value) : new SqlParameter("@EmailAddr", _userAccess.EmailAddr);
                Parameters[8] = String.IsNullOrEmpty(_userAccess.Password) ? new SqlParameter("@PwBlock", DBNull.Value) : new SqlParameter("@PwBlock", _userAccess.Password);
                Parameters[9] = new SqlParameter("@RETURN_VALUE", SqlDbType.TinyInt);
                Parameters[9].Direction = ParameterDirection.ReturnValue;
                // Parameters[10] = new SqlParameter("@Flag", isUpdate ? "U" : "N");
                var Cmd = objDataEngine.ExecuteWithReturnValue("WebUserAccessMapping", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        public async Task<MsgRetriever> ResetInternalUserPassword(string Token, Login Login)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Web);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[3];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = String.IsNullOrEmpty(Token) ? new SqlParameter("@Token", DBNull.Value) : new SqlParameter("@Token", Token);
                Parameters[1] = String.IsNullOrEmpty(Login.Password) ? new SqlParameter("@NewPass", DBNull.Value) : new SqlParameter("@NewPass", Login.Password);
                Parameters[2] = new SqlParameter("@RETURN_VALUE", SqlDbType.TinyInt);
                Parameters[2].Direction = ParameterDirection.ReturnValue;
                // Parameters[10] = new SqlParameter("@Flag", isUpdate ? "U" : "N");

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebInternalAdminPassValidate", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);

                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

        }

        public async Task<MsgRetriever> UpdatePassword(Login _UserAccess)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Web);
            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[4];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@UserId", _UserAccess.AppUid);
                Parameters[1] = new SqlParameter("@OldPass", _UserAccess.OldPassword);
                Parameters[2] = new SqlParameter("@NewPass", _UserAccess.Password);
                Parameters[3] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[3].Direction = ParameterDirection.ReturnValue;
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebInternalChangesPassMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

        }

        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public async Task<MsgRetriever> RecoverInternalPassword(string userId)
        {

            var Email = IsValid(userId);


            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Web);
            objDataEngine.InitiateConnection();
            SqlParameter[] Parameters = new SqlParameter[3];
            SqlCommand cmd = new SqlCommand();

            if (Email)
            {
                Parameters[0] = new SqlParameter("@UserId", DBNull.Value);
                Parameters[1] = new SqlParameter("@Email", userId);
            }
            else
            {
                Parameters[0] = new SqlParameter("@UserId", userId);
                Parameters[1] = new SqlParameter("@Email", DBNull.Value);
            }

            Parameters[2] = new SqlParameter("@RETURN_VALUE", SqlDbType.TinyInt);
            Parameters[2].Direction = ParameterDirection.ReturnValue;
            // Parameters[10] = new SqlParameter("@Flag", isUpdate ? "U" : "N");

            var Cmd = objDataEngine.ExecuteWithReturnValue("WebInternalForgetPassMaint", CommandType.StoredProcedure, Parameters);
            var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
            var Descp = await GetMessageCode(Result);
            objDataEngine.CloseConnection();
            return Descp;
        }

    }
}