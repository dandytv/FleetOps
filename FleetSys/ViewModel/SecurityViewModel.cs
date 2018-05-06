using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utilities.DAL;
using FleetOps.Models;
using CCMS.ModelSector;
using ModelSector;

namespace FleetOps.ViewModel
{
    public class SecurityViewModel
    {
        public WebModule _WebModule { get; set; }
        public WebPage _WebPage { get; set; }
        public WebPageSection _WebPageControl { get; set; }
        public UserAccess _UserAccess { get; set; }
    }
}