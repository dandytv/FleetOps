using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CCMS.ModelSector;


namespace ModelSector
{
   public class WebModule
   {
        public int Level { get; set; }
        public string ModuleId { get; set; }
        public string PageId { get; set; }
        public string CtrlId { get; set; }
        public string ShortDescp { get; set; }
        public string Descp { get; set; }
        public int Sts { get; set; }
        public int HourBitmap { get; set; }
    }

   public class WebPage
   {
         public int Level { get; set; }
         public string ModuleId { get; set; }
         public string PageId { get; set; }
         public string CtrlId { get; set; }
         public string ShortDescp { get; set; }
         public string Descp { get; set; }
         public string URL { get; set; }
         public string AccessLevel { get; set; }
         public int Sts { get; set; }     
     }

   public class WebPageSection
     {
         public int Level { get; set; }
         public string CtrlId { get; set; }
         public string PageId { get; set; }
         public string CtrlType { get; set; }
         public string URL { get; set; }
         public string Descp { get; set; }
         public int Len { get; set; }
         public string Fill { get; set; }
         public int MaxRow { get; set; }
         public int RefType { get; set; }
         public int Sts { get; set; }
         public string Section { get; set; }
         public string SectionId { get; set; }
         public string ModuleId { get; set; }
         public int SectionStatus { get; set; }

         public List<WebControl> CtrlList { get; set; }
         }
    public class WebControl 
    {
        public string CtrlId { get; set; }
        public int Sts { get; set; }
        public string CtrlDesp { get; set; }
        public int Level { get; set; }
        public string PageId { get; set; }
        public string ShortName { get; set; }
        public string ModuleId { get; set; }
        public string SectionId { get; set; }

    }
    public class UserAccess 
    {
        [Required]
        [DisplayName("UserId")]
        public string  UserId { get; set; }
        [DisplayName("Access Rights Template")]
        public string SelectedMapUserId { get; set; }
        public IEnumerable<SelectListItem> MapUserId { get; set; }
        [Required]
        [DisplayName("Name")]
        public string  Name { get; set; }
        [DisplayName("Old Password")]
        public string OldPassword { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }
        [DisplayName("Contact No")]
        [Required]
        public string ContactNo { get; set; }
        [DisplayName("Email Address")]
        [RegularExpression((@"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$"), ErrorMessage = "Not a valid Email Address")]
        [Required]
        public string EmailAddr { get; set; }
        [DisplayName("Title")]
        public string SeletedTitle { get; set; }
        public IEnumerable<SelectListItem> Title { get; set; }
        [DisplayName("Department")]
        public string SelectedDeptId  { get; set; }
        public IEnumerable<SelectListItem> DeptId { get; set; }
        [DisplayName("Privilege Code")]
        public string PrivilegeCd { get; set; }
        [DisplayName("Last Login")]
        public string LastLogin { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
        [DisplayName("Creation By")]
        public string CreatedBy { get; set; }
        [DisplayName("Status")]
        public string selectedSts { get; set; }
        public IEnumerable<SelectListItem> Sts { get; set; }
         [DisplayName("Access Indicator")]
        public string SelectedAccessInd { get; set; }
         public IEnumerable<SelectListItem> AccessInd { get; set; }
        [DisplayName("Change Password")]
         public bool ChangePasswordInd { get; set; }

    }
    public class forgetPassword 
    {
        [DisplayName("Email Address")]
        [RegularExpression((@"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$"), ErrorMessage = "Not a valid Email Address")]
        public string EmailAddr { get; set; }
        [DisplayName("AccessInd")]
        public string SelectedAccessInd { get; set; }
        public IEnumerable<SelectListItem> AccessInd { get; set; }
        [DisplayName("Secure Code")]
        public string secureCd { get; set; }
        [DisplayName("Ip Address")]
        public string  IpAddr { get; set; }
        [DisplayName("Password")]
        [MaxLength(8)]
        [MinLength(6)]
        public string password { get; set; }
    }
    public class UserIndexAccess 
    {
        [DisplayName("Url")]
        public string url { get; set; }
        [DisplayName("Description")]
        public string descp { get; set; }
        [DisplayName("Attribute Id")]
        public string attrId { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Icon")]
        public string icon { get; set; }
    }
}
