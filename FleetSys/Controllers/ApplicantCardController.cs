using System.Web.Mvc;
using FleetOps.Models;
using ModelSector;
using System.Linq;
using FleetOps.App_Start;
using CCMS.ModelSector;
using FleetSys.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace FleetSys.Controllers
{
    [Authorize(Roles = "Internal")]
    public class ApplicantCardController : BaseController
    {
        // GET: Card
        [Accessibility]
        public ActionResult Index()
        {
            return View();
        }
          
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                case "app":
                    return PartialView(this.getPartialPath("Applicant", "ApplicantList_Partial"));
                default:
                    return PartialView();
            }
        }
        
        [CompressFilter]
        public async Task<ActionResult> ftNewCardList(jQueryDataTableParamModel Params, string ApplId, string AcctNo)
        {
            var _filtered = new List<CardHolderInfoModel>();
            var list = (await CardAcctSignUpService.GetCardList(AcctNo)).cards;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.CardNo) ? p.CardNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.DriverName) ? p.DriverName : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedSKDSNo) ? p.SelectedSKDSNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.vehRegNo) ? p.vehRegNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.EmbossName) ? p.EmbossName : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedCardType) ? p.SelectedCardType : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.AcctNo) ? p.AcctNo : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedDialogueInd) ? p.SelectedDialogueInd : string.Empty).Contains(Params.sSearch)).ToList();

                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count,
                iTotalDisplayRecords = list.Count,
                aaData = _filtered.Select(x => new object[] { x.CardNo,
                    x.DriverName, 
                    x.SelectedSKDSNo, 
                    x.vehRegNo, 
                    x.EmbossName, 
                    x.SelectedCardType, 
                    x.SelectedDialogueInd, 
                          x.AcctNo,
                 
                    })
            }, JsonRequestBehavior.AllowGet);

        }
       
    }
}