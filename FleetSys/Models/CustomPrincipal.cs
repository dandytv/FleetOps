using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using System.Security.Principal;

namespace FleetOps.Models
{
    public class CustomPrincipal: System.Security.Principal.IPrincipal
    {
        private CustomIdentity _customIdentity;


        public CustomPrincipal(CustomIdentity customIdentity)
        {

            this._customIdentity = customIdentity;
        }
        public IIdentity Identity
        {
            get 
            {

                return _customIdentity;
            
            }
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}