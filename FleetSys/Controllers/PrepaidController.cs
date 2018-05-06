using FleetOps.Models;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCMS.ModelSector;
using FleetOps.App_Start;
using FleetOps.ViewModel;
using System.Threading.Tasks;

namespace FleetOps.Controllers
{
    [Authorize]
    [CustomLoad]
    public class PrepaidController : BaseClass
    {
        private PrepaidMaintOps _Ops = new Models.PrepaidMaintOps();
        //
        // GET: /Prepaid/
        //Godzilla
        public async Task<ActionResult> Index()
        {
            var param = "ReloadFund"; var Param2 = "ReloadAllocDetail";
            var Model = new Prepaid
            {
                Status = await BaseClass.WebGetRefLib(param.Trim())
            };
            var viewModel = new PrepaidViewModel
            {
                _DeliveryAdvice = new DeliveryAdvice
                {
                    Status = await BaseClass.WebGetRefLib("ReloadAlloc")
                },
                _Prepaid = Model,
                _PurchaseOrder = new PurchaseOrder
                {
                    Status = await BaseClass.WebGetRefLib(param.Trim())
                },
                _PrepaidCardnAcct = new PrepaidCardnAcct
                {
                    Status = await BaseClass.WebGetRefLib(Param2.Trim())
                }
            };
            return View(viewModel);
        }
        [CompressFilter]
        public JsonResult ftWebReloadFundSearch(PrepaidViewModel model, jQueryDataTableParamModel Params)
        {
            var list = _Ops.WebReloadFundSearch(model._Prepaid);
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = list.Select(x => new object[] { x.TxnId, x.DocNo, x.TxnDate, x.TxnAmt, x.Balance, x.SelectedStatus, x.UserId, x.CreationDate, x.XRefDoc })
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ftWebReloadFundAltSearch(Prepaid model, jQueryDataTableParamModel Params)
        {
            var list = _Ops.WebReloadFundSearch(model);
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = list.Select(x => new object[] { x.TxnId, x.DocNo, x.TxnDate, x.TxnAmt, x.Balance, x.SelectedStatus, x.UserId, x.CreationDate, x.XRefDoc })
            }, JsonRequestBehavior.AllowGet);
        }




        [CompressFilter]
        public JsonResult WebReloadFundSelect(string txnId)
        {
            var _po = _Ops.WebReloadFundSelect(txnId);
            return Json(new { po = _po }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult WebReloadFundMaint(PrepaidViewModel model, string Func)
        {
            var result = _Ops.WebReloadFundMaint(model._PurchaseOrder, Func);
            return Json(new { resultCd = result });
        }
        [CompressFilter]
        public JsonResult ftWebReloadAllocationListSelect(string TxnId, jQueryDataTableParamModel Params)
        {
            var list = _Ops.WebReloadAllocationListSelect(TxnId);
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = list.Select(x => new object[] { x.DocNo, x.TxnDate, x.EffDateFrom, x.TxnAmt, x.DABal, x.SelectedStatus, x.Remarks, x.CreatedBy, x.CreationDate, x.PoTxnId, x.DaTxnId, x.AcctNo })
            }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public JsonResult WebReloadAnllocationSelect(int TxnId, string AcctNo)
        {
            var _da = _Ops.WebReloadAllocationSelect(TxnId, AcctNo);
            return Json(new { da = _da }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult WebReloadAllocationMaint(PrepaidViewModel model, string Func)
        {
            var result = _Ops.WebReloadAllocationMaint(model._DeliveryAdvice, Func);
            return Json(new { resultCd = result });
        }

        public JsonResult WebReloadAllocationDetailListSelect(string TxnId, jQueryDataTableParamModel Params)
        {
            var list = _Ops.WebReloadAllocationDetailListSelect(TxnId);
            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = list.Select(x => new object[] { x.CardNo, x.ReloadAmt, x.SelectedStatus, x.Remarks, x.ReloadDate, x.UserId, x.CreationDate, x.TxnId, x.AcctNo })
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult WebReloadAllocationDetailSelect(string AcctNo, int TxnId = 0)
        {
            var ca = _Ops.WebReloadAllocationDetailSelect(TxnId, AcctNo);
            return Json(new { ca = ca }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult WebReloadAllocDetailMaint(PrepaidViewModel model, int ParentTxnId, string Func, string Acctno)
        {
            var result = _Ops.WebReloadAllocDetailMaint(model._PrepaidCardnAcct, ParentTxnId, Func, Acctno);
            return Json(new{ resultCd = result });
        }
    }
}