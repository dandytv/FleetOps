using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilities.DAL;
using FleetOps.Models;
using CCMS.ModelSector;
using FleetOps.App_Start;
using FleetOps.ViewModel;
using FleetOps.Controllers;
using ModelSector;
using System.Threading.Tasks;

namespace FleetOps.Controllers
{
    [Authorize]
    [CustomLoad]
    public class NonFleetTxnController : BaseClass
    {
        private NonFleetTxnOps objNonFleetTxnOps = new NonFleetTxnOps();
        //
        // GET: /NonFleetTxnOps/


        [Accessibility]
        public ActionResult Index(string id)
        {
            return View();
        }

        public async Task<ActionResult> LoadInfo()
        {
            var _NonFleetTxn = new NonFleetTxnViewModel
            {
                _nonFleetTxn = new NonFleetTxn
                {
                    TxnCd = await BaseClass.WebGetNonFleetTxnCode(),
                    Sts = await BaseClass.WebGetRefLib("TxnSts"),
                    Affiliate = await BaseClass.WebGetAffiliate(),
                    UserId = this.GetUserId,
                    TxnDate = DateConverter(DateTime.Now.ToString())
                }
            };
            return Json(new { Selects = _NonFleetTxn, Model = new NonFleetTxnViewModel() }, JsonRequestBehavior.AllowGet);
        }

        #region "Fetch Table List & Details"

        public ActionResult ftNonFleetTxnOpsList(jQueryDataTableParamModel Params, NonFleetTxn _NonFleetTxn)
        {
            var list = objNonFleetTxnOps.GetNonFleetTxnList(_NonFleetTxn);
            var filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength);

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = filtered.Select(x => new object[] { x.SelectedTxnCd, x.Descp, x.DisplayTotAmnt, x.TxnDate, x.DbCrInd, x.Account, x.UserId, x.TxnId })
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ftNonFleetTxnOpsDetail(NonFleetTxn _NonFleetTxn, string id)
        {
            var data = objNonFleetTxnOps.GetNonFleetTxnDetail(_NonFleetTxn, id);

            return Json(new { txn = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "Save & Delete Info"
        [HttpPost]
        public ActionResult SaveNonFleetTxnOps(NonFleetTxn _NonFleetTxn)
        {
            var _SaveTxn = objNonFleetTxnOps.SaveNonFleetTxn(_NonFleetTxn);

            return Json(new { txn = _SaveTxn }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DelTxnAdj(NonFleetTxn _NonFleetTxn, CardnAccNo _CardnAcctNo)
        {
            _NonFleetTxn._CardnAccNo = _CardnAcctNo;
            var _deleteTxnAdj = objNonFleetTxnOps.DelNonFleetTxn(_NonFleetTxn);
            return Json(new { resultCd = _deleteTxnAdj }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
