using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ModelSector;
using CCMS.ModelSector;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelSector
{
    public class AuditLoggerModel
    {
        [DisplayName(@"Module")]
        public string SelectedModule { get; set; }
        public IEnumerable<SelectListItem> Module { get; set; }
         [DisplayName("Table Name")]
        public string SelectedTblName { get; set; }
         public IEnumerable<SelectListItem> TblName { get; set; }
        [DisplayName("Date")]
        public string Date { get; set; }
        [DisplayName("Field Name")]
        public string FieldName { get; set; }
        [DisplayName("Ref Key")]
        public string RefKey { get; set; }
        [DisplayName("Action")]
        public string ActionAud { get; set; }
        [DisplayName("Old Data")]
        public string OldData { get; set; }
        [DisplayName("New Data")]
        public string NewData { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
        [DisplayName("Audit Id")]
        public string AudId { get; set; }
        [DisplayName("Sub Ref Key1")]
        public string subRefKey1 { get; set; }
        [DisplayName("Sub Ref Key2")]
        public string subRefKey2 { get; set; }
        [DisplayName("Action By")]
        public string ActionBy { get; set; }
        public string UserId { get; set; }

    }
}
