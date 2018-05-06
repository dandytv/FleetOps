using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Utilities.DAL;
using System.Data.SqlClient;
using System.Data;
namespace FleetOps.Models
{
    public class Login
    {

        [StringLength(20)]
        [DisplayName("USERNAME")]
        public string AppUid { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string Password { get; set; }
        public string dbName { get; set; }
        public string pwsBlock { get; set; }
        public int Captcha { get; set; }

        public string ReturnUrl { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string OldPassword { get; set; }

    }



    public class Users
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginStatus
    {
        public string IssNo { get; set; }
        public string Issuer { get; set; }
        public string Program { get; set; }
        public string Remark { get; set; }
        public string StatusCode { get; set; }
    }
    public partial class AccountLogin
    {
        public bool TryLogin(Login login)
        {
            // FleetDataEngine FDE = new FleetDataEngine(AccessMode.Admin, DBType.Maint, login.Username, "1492" + login.Password + "3875");
            //  return FDE.TestConnection();
            return true;
        }
        //public Dictionary<string, string> LogonToCCMS(Login login)
        //{
        //    Dictionary<string, string> htResultset = new Dictionary<string, string>();
        //    try
        //    {

        FleetDataEngine FDE = new FleetDataEngine(AccessMode.Admin, DBType.Maint);
        //        FDE.InitiateConnection();
        //        SqlParameter[] Parameters = new SqlParameter[6];
        //        SqlCommand cmd = new SqlCommand();
        //        Parameters[0] = new SqlParameter("@UserId", login.AppUid);
        //        Parameters[1] = new SqlParameter("@IssNo", SqlDbType.VarChar, 30);
        //        Parameters[2] = new SqlParameter("@Issuer", SqlDbType.VarChar, 30);
        //        Parameters[3] = new SqlParameter("@Program", SqlDbType.VarChar, 30);
        //        Parameters[4] = new SqlParameter("@Remark", SqlDbType.VarChar, 30);
        //        Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4);
        //        Parameters[1].Direction = ParameterDirection.Output;
        //        Parameters[2].Direction = ParameterDirection.Output;
        //        Parameters[3].Direction = ParameterDirection.Output;
        //        Parameters[4].Direction = ParameterDirection.Output;
        //        Parameters[5].Direction = ParameterDirection.ReturnValue;
        //cmd = FDE.ExecuteCommand("Logon", CommandType.StoredProcedure, Parameters);
        //htResultset.Add("Issno", Convert.ToString(cmd.Parameters["@IssNo"].Value));
        //htResultset.Add("Issuer", Convert.ToString(cmd.Parameters["@Issuer"].Value));
        //htResultset.Add("Program", Convert.ToString(cmd.Parameters["@Program"].Value));
        //htResultset.Add("Remark", Convert.ToString(cmd.Parameters["@Remark"].Value));
        //htResultset.Add("RETURN_VALUE", Convert.ToString(cmd.Parameters["@RETURN_VALUE"].Value));

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return htResultset;
        //}







    }

}