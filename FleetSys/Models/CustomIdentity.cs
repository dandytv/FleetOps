using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace FleetOps.Models
{
    public class CustomIdentity : System.Security.Principal.IIdentity
    {
        private FormsAuthenticationTicket _ticket;


        public CustomIdentity(FormsAuthenticationTicket tkt)
        {
            this._ticket = tkt;
        }


        public string AuthenticationType
        {
            get
            {
                return "Custom";
            }

        }



        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }



        public string Name
        {
            get
            {
                return _ticket.Name;

            }
        }


        public FormsAuthenticationTicket ticket
        {
            get {

                return _ticket;
            }
        
        }



        public string Skin
        {

            get
            {

                string[] userDataPieces = _ticket.UserData.Split("|".ToCharArray());

                return userDataPieces[0];

            }

        }

        //public string Title
        //{

        //    get
        //    {

        //        string[] userDataPieces = _ticket.UserData.Split("|".ToCharArray());

        //        return userDataPieces[1];

        //    }

        //}



       //this section enables you to add custom informations to the forms authenticatio  ticket... use  the properties and save it inside the ticket information //split


    }
}