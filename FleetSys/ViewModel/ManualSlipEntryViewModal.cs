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
    public class ManualSlipEntryViewModal
    {
        public ManualSlipEntry _ManualSlipEntry { get; set; }
        public ManualTxnProduct _ManualTxnProduct { get; set; }
        //public ManualSlipDetailEntry _DetailEntry { get; set; }
        //public BatchList _BatchList { get; set; }
    }
}