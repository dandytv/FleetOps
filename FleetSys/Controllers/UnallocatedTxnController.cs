using CCMS.ModelSector;
using FleetOps.App_Start;
using FleetOps.Models;
using FleetSys.Models;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FleetSys.Controllers
{
    public class UnallocatedTxnController : Controller
    {
        private UnallocatedTxnOps objUnallocatedTxn = new UnallocatedTxnOps();

        // GET: UnallocatedTxn
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SaveUnsettleTxn(UnsettleTxn _ust)
        {
            var _saveTxn = await objUnallocatedTxn.SaveUnsettleTxn(_ust);
            return Json(new { result = _saveTxn }, JsonRequestBehavior.AllowGet);
        }


        [CompressFilter]
        public async Task<ActionResult> ftUnsettleTxnList(jQueryDataTableParamModel Params, string TxnId)
        {
            var list = await objUnallocatedTxn.GetUnsettleTxnList(TxnId);
            var filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength);

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = list.Select(x => new object[] { x.TxnId, x.BatchId, x.RecType, x.AcctNo, x.TxnCd, x.CheqNo, x.PayeeName, x.TxnDate, x.TxnAmt, x.Descp, x.Sts, x.creationDate })

            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ftUnsettleTxn(string TxnId)
        {
            var data = await objUnallocatedTxn.GetUnsettleTxnDetail(TxnId);
            return Json(new { result = data }, JsonRequestBehavior.AllowGet);
        }

    }
}