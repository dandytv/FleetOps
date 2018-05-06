using AutoMapper;
using CardTrend.Domain.Dto.MultiplePayment;
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
namespace FleetSys.Controllers
{
    public class MerchantMultiAdjustmentController : BaseController
    {
        public ActionResult Index()
        {
            var info = new TxnAdjustment();
            return View(info);
        }
        [HttpPost]
        public async Task<JsonResult> ftMultipleAdjMaint(TxnAdjustment _MultipleTxn)
        {
            var txnAdjustmentObj = Mapper.Map<TxnAdjustmentDTO>(_MultipleTxn);
            var _SaveMultiAdj = await MerchMultitxnAdjustmentService.SaveMerchantMultiTxnAdjustmentMaint(txnAdjustmentObj, GetUserId);
            return Json(new { resultCd = _SaveMultiAdj, batchId = _SaveMultiAdj.returnValue.BatchId, rcptNo = _SaveMultiAdj.returnValue.RetCd }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetAdjDropDown()
        {
            var _Payment = new TxnAdjustment
            {
                Owner = (await UserAccessService.GetUserAccessListSelect()).RefLibLst,
                TxnCd = await BaseService.WebGetTxnCode("A", "AdjustTxnCategoryMapInd", "Y"),
                PaymentType = await BaseService.GetRefLib("TxnShortDesc", null, "2")
            };

            return Json(new { Selects = _Payment, Model = new MultiPayment() { ChequeAmt = "0.00"} }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> WebMerchantMultiTxnAdjustmentListSelect(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<TxnAdjustment>();
            var list = (await MerchMultitxnAdjustmentService.GetMerchantMultiTxnAdjustmentList()).txtAdjustments;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.BatchId) ? p.BatchId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnId) ? p.TxnId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ChequeNo) ? p.ChequeNo : string.Empty).ToLower().Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] { null, x.BatchId, x.CreationDate, x.SelectedAdjTxnCode, x.InvoiceNo, x.TxnCount, x.BillingTxnAmt, x.SelectedOwner, x.SelectedSts })//, x.XRefCardNo
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> WebmMerchantMultiTxnAdjustmentSelect(string BatchId, string InvoiceNo)
        {
            var multiTxnAdjustmentDetail = (await MerchMultitxnAdjustmentService.GetMerchantMultiTxnAdjustmentDetail(InvoiceNo, BatchId)).txnAdjustmentDetail;
            return Json(new { list = multiTxnAdjustmentDetail }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> WebGetGLCode(jQueryDataTableParamModel Params, MultiPayment _multipayment)
        {
            var list = (await MerchMultitxnAdjustmentService.GetGLCodes(_multipayment.SelectedAdjTxnCode)).multiPaymentGLCodes;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}