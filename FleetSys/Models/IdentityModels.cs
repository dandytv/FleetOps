using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FleetSys.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
        public string ErrorCode { get; set; }
    }


}