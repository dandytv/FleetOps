using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using ModelSector.Global_Resources;


namespace CCMS.ModelSector
{
    public class GhostCardModel
    {
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int IssNo { get; set; }
        [DisplayName("No of Cards")]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public int NoofCards { get; set; }
        [Display(Name = "noofaccounts", ResourceType = typeof(locale))]
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
        public string NoofAccounts { get; set; }
        public IEnumerable<SelectListItem> Logo { get; set; }
        [Required]
        [Display(Name = "cardlogo", ResourceType = typeof(locale))]
        public string SelectedLogo { get; set; }
        public IEnumerable<SelectListItem> cardType { get; set; }
        [Required]
        [Display(Name = "plastictype", ResourceType = typeof(locale))]
        public string SelectedPlasticType { get; set; }
        public IEnumerable<SelectListItem> plasticType { get; set; }
        [Required]
        [Display(Name = "cardtype", ResourceType = typeof(locale))]
        public string SelectedCardType { get; set; }
    }
}
