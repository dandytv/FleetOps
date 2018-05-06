using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSector;
using CCMS.ModelSector;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModelSector.Global_Resources;
using System.Web.Mvc;
using ModelSector.Helpers;

namespace ModelSector
{
   public class Corporate
    {
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "CorpCdLbl")]
       [StringLength(15, ErrorMessage = "Maximum lengthis 15 characters")]
       public string CorpCd { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "CorpNameLbl")]
       [Required(ErrorMessage = "Please Fill In The Corporate Name")]
       public string CorpName { get; set;}
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "TradeCreditLimitLbl")]
       [RegularExpression(@"^\-?\(?\$?\s*\-?\s*\(?(((\d{1,3}((\,\d{3})*|\d*))?(\.\d{1,4})?)|((\d{1,3}((\,\d{3})*|\d*))(\.\d{0,4})?))\)?$", ErrorMessage = "Amount not valid")]
       public string TradeCreditLimit { get; set; }
       public string DispalyTradeCreditLimit { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "ContactNameLbl")]
       public string ContactName { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "SelectedContactCdDdl")]
       public string SelectedContactCd {get; set;}
       public IEnumerable<SelectListItem> ContactCd {get; set;}
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "ContactNoLbl")]
       [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
       public string ContactNo { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "ComplexIndLbl")]
       public bool ComplexInd { get; set; }
       public string SelectedComplexInd { get; set; }
       public IEnumerable<SelectListItem> ContactTypes { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "SelectedContactTypeDdl")]
       public string SelectedContactType { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "MobileNoLbl")]
       public string MobileNo { get; set; }       
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "CmpyFaxLbl")]
       public string CmpyFax { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "EmailAddrLbl")]
       [RegularExpression((@"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$"), ErrorMessage = "Not a valid Email Address")]
       public string EmailAddr { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "Addr1Lbl")]
       public string Addr1 { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "Addr2Lbl")]
       public string Addr2 { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "Addr3Lbl")]
       public string Addr3 { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "SelectedAddrCdDdl")]
       public string SelectedAddrCd { get; set; }
       public IEnumerable<SelectListItem> AddrCd { get; set; }

       [DisplayNameLocalizedAttribute("CardtrendCorporate", "SelectedStateDdl")]
       public string SelectedState { get; set; }
       public IEnumerable<SelectListItem> State { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "ZipCdLbl")]
       [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "Numbers only")]
       public string ZipCd { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "SelectedCtryDdl")]
       public string SelectedCtry { get; set; }
       public IEnumerable<SelectListItem> Ctry { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "PICLbl")]
       public string PIC { get; set; }
       public CreationDatenUserId _CreationDatenUserId { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "InvBillIndLbl")]
       public bool InvBillInd { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendCorporate", "PymtIndLbl")]
       public bool PymtInd { get; set; }
       public CreditAssesOperation _AcctDepositInfo { get; set; }
    }
}
