using CCMS.ModelSector;
using FleetOps.App_Start;
using FleetOps.Models;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FleetSys.Controllers
{
    public class UnbalanceTxnController : Controller
    {
        private UnallocatedTxnOps ObjUnallocatedTxnOps = new UnallocatedTxnOps();

        // GET: UnbalanceTxn
        [Accessibility]
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ftUnbalanceTxnSearchList(jQueryDataTableParamModel Params, string AcctNo)
        {
            var list = await ObjUnallocatedTxnOps.UnbalanceTxnSearchList(AcctNo);
            var filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength);

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecord = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = list.Select(x => new object[] { x.SelectedRecType, x.LBE, x.TxnId, x.AcctNo, x.selectedTxnCd, x.DisplayTxnDate, x.TxnAmount, x.SettledAmt, x.BookingAmt, x.UnallocatedAmount, x.Descp })
            }, JsonRequestBehavior.AllowGet);
        }

        [CompressFilter]
        public async Task<ActionResult> ftGetUnbalanceTxnList(jQueryDataTableParamModel Params, string RecType, string AcctNo, string TxnId)
        {
            var list = await ObjUnallocatedTxnOps.GetUnbalanceTxnList(RecType, AcctNo, TxnId);
            var filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength);

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = list.Select(x => new object[] { x.TxnSequence, x.TxnId, x.AcctNo, x.TxnDate, x.DueDate, x.DisplayTxnAmount, x.OutStandingAmt, x.Descp, x.BookingAmt })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveTxnUnbalance(string InputSrc, List<UnbalanceTxnSearch> unallocatedModel)
        {
            var _Savetxn = await ObjUnallocatedTxnOps.SaveBalanceTxn(InputSrc, unallocatedModel);
            return Json(new { result = _Savetxn }, JsonRequestBehavior.AllowGet);
        }


    }
}