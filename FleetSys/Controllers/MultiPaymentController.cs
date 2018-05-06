using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FleetOps.Helpers;
using FleetSys.Models;
using ModelSector;
using System.Threading.Tasks;
using FleetOps.Models;
using CCMS.ModelSector;
using FleetOps.App_Start;
using AutoMapper;
using CardTrend.Domain.Dto.MultiplePayment;
namespace FleetSys.Controllers
{
    public class MultiPaymentController : BaseController
    {
        // GET: Collection
        [Accessibility]
        public ActionResult Index()
        {
            return View(new MultiPayment());
        }
        #region "FillData"
        public async Task<ActionResult> GetDropDown()
        {
            var _Payment = new MultiPayment
            {
                TxnCode = await BaseService.WebGetTxnCode("I", "PaymtTxnCategoryMapInd", "Y"),             
                IssueingBank = await BaseService.GetRefLib("Bank")
            };
            return Json(new { Selects = _Payment, Model = new MultiPayment() }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public async Task<JsonResult> ftMultiplePayment(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<MultiPayment>();
            var list = (await MultiPaymentOpService.WebMultiPaymentListSelect()).multiPayments;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.BatchId) ? p.BatchId : string.Empty).ToLower().Contains(Params.sSearch) ||
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
                aaData = _filtered.Select(x => new object[] { null, x.BatchId, x.CreationDate, x.SelectedTxnCode, x.RefNo, x.ChequeNo, x.TxnCnt, x.BillingAmt,x.SelectedOwner, x.SelectedSts })//, x.XRefCardNo
            }, JsonRequestBehavior.AllowGet);
        }        
        [HttpPost]
        public async Task<JsonResult> ftMultiplePaymentMaint(TxnAdjustment _MultipleTxn)
        {
            _MultipleTxn.SelectedTxnCd = _MultipleTxn.SelectedTxnCode;
            _MultipleTxn.UserId = GetUserId;
            var _saveMultiAdj = await MultiPaymentOpService.SaveWebMultiPaymentMaint(_MultipleTxn);
            var BatchId = _saveMultiAdj.returnValue.BatchId;
            return Json(new { resultCd = _saveMultiAdj, batchId = BatchId }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> WebMultiPaymentSelect(string BatchId, MultiPayment _MultiPymt)
        {
            var multipaymentSelectObj = (await MultiPaymentOpService.WebMultiPaymentSelect(BatchId,_MultiPymt)).multiPayment;
            return Json(new { list = multipaymentSelectObj }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> WebGetGLCode(jQueryDataTableParamModel Params, MultiPayment _multipayment)
        {
            var list = (await MultiPaymentOpService.WebGetGLCodeByTxnCd(_multipayment.SelectedTxnCode, _multipayment.SelectedGLSettlement, _multipayment.AcctNo)).multiPayments;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> WebGetAltPymtTxnCd(string GlSettlementCd)
        {
            var list = await MultiPaymentOpService.GetPymtTxnCd(GlSettlementCd, "Pymt");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}