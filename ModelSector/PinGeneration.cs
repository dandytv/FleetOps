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
using ModelSector.Global_Resources;
namespace ModelSector
{
  public  class PinGeneration
    {
        public CardnAccNo _CardnAccNo { get; set; }
        [Required]
        public string PINIndicator { get; set; }
        [Display(Name = "pinmailingtype", ResourceType = typeof(locale))]
        public string SelectedPinMailingType { get; set; }
        public IEnumerable<SelectListItem> PINMailingType { get; set; }
    }
}
