using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utilities.DAL;
using FleetOps.Models;
using CCMS.ModelSector;

namespace FleetOps.ViewModel
{
    public class viewerMerchantViewModel
    {
        public Int64 AcctNo { get; set; }
        public MerchantDtl Detail { get; set; }
        public MerchEntityDtl Entity { get; set; }
        public MerchantContacts Contacts { get; set; }
        public MerchAddress Address { get; set; }
        public CICnMCC CicAndMcc { get; set; }
        public List<Accessibility> Accessibility { get; set; }
    }
}