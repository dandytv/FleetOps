using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utilities.DAL;
using FleetOps.Models;
using CCMS.ModelSector;
using ModelSector;
using FleetSys.Models;

namespace FleetOps.ViewModel
{
    public class ReportViewModel
    {
        public AcctSignUp _AcctSignUp { get; set; }
        public ContactLstModel _ContactLstModel { get; set; }
        public VeloctyLimitListMaintModel _VeloctyLimitListMaintModel { get; set; }
        public AddrListMaintModel _AddrListMaintModel { get; set; }
        public VehiclesListModel _VehiclesListModel { get; set; }
        public SKDS _SKDS { get; set; }
    }
}