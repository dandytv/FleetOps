using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCMS.ModelSector;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModelSector;

namespace ModelSector
{
    public class TerminalInventory
    {
        [DisplayName("Terminal Id")]
        public string TerminalId { get; set; }
        [DisplayName("Terminal Type")]
        public string SelectedTerminalType { get; set; }
        public IEnumerable<SelectListItem> TerminalType { get; set; }
        [DisplayName("Status")]
        public string SelectedStatus { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Printer")]
        public string Printer { get; set; }
        [DisplayName("Pin Pad")]
        public string PinPad { get; set; }
        [DisplayName("User ID")]
        public string UserId { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
        public EventLogList _evenLogList { get; set; }
    }

}

