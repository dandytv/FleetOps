using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FleetOps.Models;
using ModelSector;
using FleetSys.Models;
namespace FleetOps.ViewModel
{
    public class CostCentreViewModel
    {
        public CostCentre CostCentre { get; set; }
        public VeloctyLimitListMaintModel Velocity { get; set; }
    }
}