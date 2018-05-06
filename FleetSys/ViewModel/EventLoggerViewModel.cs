using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utilities.DAL;
using FleetOps.Models;
using CCMS.ModelSector;
using FleetOps.ViewModel;
using FleetSys.Controllers;
using ModelSector;

namespace FleetOps.ViewModel
{
    public class EventLoggerViewModel
    {
        public EventLogger _EventLogger { get; set; }
        public EventDetails _EventDetails { get; set; }
        public EventLogList _EventLogList { get; set; }
    }

}