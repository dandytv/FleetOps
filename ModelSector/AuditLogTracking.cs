using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ModelSector
{
    public class AuditLogTracking
    {
        [DisplayName("Audit No")]
        public string AuditNo { get; set; }
        [DisplayName("Reference Type")]
        public string SelectedRefType { get; set; }
        public IEnumerable<SelectListItem> RefType { get; set; }
        [DisplayName("Reference Key")]
        public string Refkey { get; set; }
        [DisplayName("Old Data")]
        public string OldData { get; set; }
        [DisplayName("New Data")]
        public string NewData { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Creative Id")]
        public string CreativeId { get; set; }
        
    }
}
