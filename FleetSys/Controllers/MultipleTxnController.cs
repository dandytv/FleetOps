using CCMS.ModelSector;
using FleetOps.Models;
using FleetSys.Models;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FleetOps.App_Start;
using AutoMapper;
using CardTrend.Domain.Dto.MultiplePayment;

namespace FleetSys.Controllers
{
    public class MultipleTxnController : BaseController
    {
       // private MultipleTxnOps objMultipleTxnOps = new MultipleTxnOps();
        // GET: MultipleTxn
        [Accessibility]
        public ActionResult Index()
        {
            return View(new TxnAdjustment());
        }
        #region "FillData"
        public async Task<ActionResult> GetDropDown()
        {
            var _Payment = new TxnAdjustment
            {
                TxnCode = await BaseService.GetPymtTxnCd("Pymt",null),
                Owner =  (await UserAccessService.GetUserAccessListSelect()).RefLibLst,
                IssueingBank = await BaseService.GetRefLib("Bank"),
                PaymentType = await BaseService.GetRefLib("TxnShortDesc",null,"10"),
                GLSettlement = await BaseService.GetRefLib("GLSettlement")
            };
            var multipayment = new MultiPayment();
            multipayment.ChequeAmt = "0";
            return Json(new { Selects = _Payment, Model = multipayment }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAdjDropDown()
        {
            var _Payment = new TxnAdjustment
            {
                Owner = (await UserAccessService.GetUserAccessListSelect()).RefLibLst,
                AdjTxnCode = await BaseService.WebGetTxnCode("I", "AdjustTxnCategoryMapInd", "Y"),
                PaymentType = await BaseService.GetRefLib("TxnShortDesc", null, "2")
            };
            return Json(new { Selects = _Payment, Model = new MultiPayment() }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region "MultipleTxnAdj"
        public async Task<JsonResult> ftMultipleAdj(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<TxnAdjustment>();
            var list = (await MultipleTxnOpService.GetMultiTxnAdjustmentListSelect()).txtAdjustments;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.BatchId) ? p.BatchId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.TxnNo) ? p.TxnNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.ChequeNo) ? p.ChequeNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.ChequeAmt) ? p.ChequeAmt : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.DisplayTotAmt) ? p.DisplayTotAmt : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CreationDate) ? p.CreationDate : string.Empty).ToLower().Contains(Params.sSearch)).ToList();


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
                aaData = _filtered.Select(x => new object[] {null, x.BatchId, x.CreationDate, x.SelectedTxnCode, x.ChequeNo, x.TxnNo, x.DisplayTotAmt, x.SelectedOwner, x.SelectedSts })//, x.XRefCardNo
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> ftMultipleAdjMaint(TxnAdjustment _MultipleTxn)
        {
            _MultipleTxn.UserId = GetUserId;
            var _SaveMultiAdj = await MultipleTxnOpService.SaveftMultipleAdjMaint(_MultipleTxn);
            return Json(new { resultCd = _SaveMultiAdj, batchId = _SaveMultiAdj.returnValue.BatchId, rcptNo = _SaveMultiAdj.returnValue.RetCd, chequeNo = _SaveMultiAdj.returnValue.ChequeNo }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> WebMultiTxnAdjustmentSelect(string BatchId, TxnAdjustment _Txn)
        {
            var txnList = (await MultipleTxnOpService.GetMultiTxnAdjustmentSelect(BatchId, _Txn.ChequeNo)).txnAdjustment;
            return Json(new { list = txnList }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> WebGetGLCode(jQueryDataTableParamModel Params, MultiPayment _multipayment)
        {
            var list = (await MultipleTxnOpService.GetGLCode(_multipayment.SelectedAdjTxnCode, _multipayment.AcctNo, null)).multiPayments;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "MultiplePaymentTxn"
        #endregion
    }
}