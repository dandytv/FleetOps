using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FleetSys.Common
{
    public class Enums
    {
        public enum ErrorCode
        {
            PasswordExpired = 95670,
            FirstTimeLogin = 95674
        }

        public enum FraudSection
        {
            CustomerDeails = 1,
            CardDetails = 2,
            IncidentDesc = 3,
            IncidentInvestigationTeam = 4,
            IncidentDetailedDesc = 5,
            IncidentStatus = 6
        }
        public enum NatureOfIncident
        {
            [Description("KP Involvement/ Collaboration")]
            K = 1,
            [Description("KOthers")]
            O = 1,
            [Description("PIN Compromise")]
            P = 3,
            [Description("Repeated Case")]
            R = 4
        }

        public enum FileFolderPath
        {
            [Description("Application Id")]
            SuppDoc = 1,
            [Description("Fraud")]
            Fraud = 2
        }

        public enum CollectionCaseSts
        {
            [Description("Open")]
            O = 1,
            [Description("Close")]
            C = 2
        }

    }
}