using AutoMapper;
using CardTrend.Common.Extensions;
using CardTrend.Domain.Dto.Account;
using CardTrend.Domain.Dto.ManualSlipEntry;
using CCMS.ModelSector;
using FleetOps.App_Start;
using FleetOps.Models;
using FleetOps.ViewModel;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace FleetSys.Controllers
{
    [Authorize]
    public class ManualSlipEntryController : BaseController
    {
        // GET: ManualSlipEntry
        [Accessibility]
        public async Task<ViewResult> Index()
        {
            return View(new ManualSlipEntry());
        }
        public async Task<JsonResult> GetModel()
        {
            var _ManualSlipEntry = new ManualSlipEntry
                {
                    Sts = await BaseService.GetRefLib("MerchBatchSts"),
                    TxnCd = await BaseService.WebGetTxnCode("A", "PurchTxnCategory", "y"),
                    ProdCd = await BaseService.WebGetProduct(null),
                    VATCd = await BaseService.GetRefLib("VATCode"),
                    TermId = new List<SelectListItem>()
                };
            return Json(new { Selects = _ManualSlipEntry, Model = new ManualSlipEntry() }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Select(string Id)
        {
            var _ManualSlipEntryViewModel = new ManualSlipEntryViewModal
            {
                _ManualSlipEntry = (await ManualSlipEntryOpService.GetManualSlipEntryBatchDetail(Id)).manualSlipEntryBatchDetail,
                _ManualTxnProduct = new ManualTxnProduct
                {
                    ProdCd = await BaseService.WebGetProduct(null)
                }
            };
            return View(_ManualSlipEntryViewModel);
        }

        #region "Txn"
        [CompressFilter]
        public async Task<ActionResult> ftManualSlipEntryTxnList(jQueryDataTableParamModel Params, string SettleId = null)
        {
            var _filtered = new List<ManualSlipEntry>();
            var list = (await ManualSlipEntryOpService.GetManualSlipEntryTxnList(SettleId)).merchManualTxns;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.SelectedSts.ToString()) ? p.SelectedSts.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedTermId) ? p.SelectedTermId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedTxnCd.ToString()) ? p.SelectedTxnCd.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SiteId) ? p.SiteId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.BatchId) ? p.BatchId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.InvoiceNo.ToString()) ? p.InvoiceNo.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.RcptNo) ? p.RcptNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnDate) ? p.TxnDate : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.CardNo) ? p.CardNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.AuthCardNo) ? p.AuthCardNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.DisplayTxnAmt) ? p.DisplayTxnAmt : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Descp) ? p.Descp : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.AuthNo) ? p.AuthNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Odometer.ToString()) ? p.Odometer.ToString() : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p._CreationDatenUserId.CreationDate) ? p._CreationDatenUserId.CreationDate : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p._CreationDatenUserId.UserId) ? p._CreationDatenUserId.UserId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnId) ? p.TxnId : string.Empty).Contains(Params.sSearch)).ToList();

                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = _filtered.Select(x => new object[] { x.SelectedSts, x.SelectedTermId, x.SelectedTxnCd, x.SiteId, x.BatchId, x.InvoiceNo, x.RcptNo, x.TxnDate, x.CardNo, x.AuthCardNo, x.DisplayTxnAmt, x.VATNo, x.VATAmt, x.Descp, x.AuthNo, x.Odometer, x.ArrayCnt, x._CreationDatenUserId.CreationDate, x._CreationDatenUserId.UserId, x.TxnId })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveManualSlipEntryTxn(ManualSlipEntry _ManualSlipEntry, string SettleId, string TxnId)
        {
            _ManualSlipEntry.SettleId = SettleId;
            _ManualSlipEntry.UserId = GetUserId;
            var _saveManualTxn = await ManualSlipEntryOpService.SaveManualSlipEntry(_ManualSlipEntry);
            return Json(new { resultCd = _saveManualTxn, txnid = _saveManualTxn.paraRes.TxnId, SettleId = _saveManualTxn.paraRes.SettleId }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ftManualSlipEntryTxn(string TxnId)
        {
            var data = (ManualSlipEntryOpService.GetManualSlipEntryTxnDetail(TxnId)).manualSlipEntryBatchDetail;
            return Json(new { txn = data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DelManualSlipEntryTxn(ManualSlipEntry _manualSlipEntry)
        {
            var result = ManualSlipEntryOpService.DeleteMerchManualTxn(_manualSlipEntry.BatchId, _manualSlipEntry.SettleId, _manualSlipEntry.TxnId, _manualSlipEntry.TxnDetailId);
            return Json(new { resultCd = result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult WebGetManualTxn(string SettleId)
        {
            var select = (ManualSlipEntryOpService.GetManualTransaction(SettleId)).manualSlipEntryBatchDetail;
            return Json(select, JsonRequestBehavior.AllowGet);
        }
        #endregion "txn"

        #region "batch"
        [HttpPost]
        public async Task<ActionResult> SaveManualSlipBatch(ManualSlipEntry _ManualSlipEntry)
        {
            _ManualSlipEntry.UserId = GetUserId;
            var SaveManualBatch = await ManualSlipEntryOpService.SaveMerchManualBatch(_ManualSlipEntry);
            return Json(new { resultCd = SaveManualBatch, batchId = _ManualSlipEntry.BatchId, settleId = _ManualSlipEntry.SettleId }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> BusnLocSiteId(ManualSlipEntry _ManualSlipEntry)//khairi
        {
            if (!String.IsNullOrEmpty(_ManualSlipEntry.BusnLocation))
            {
                var list = await BaseService.GetTermId(_ManualSlipEntry.BusnLocation);
                return Json(new { list = list }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
        [CompressFilter]
        public ActionResult ftManualSlipBatchList(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<ManualSlipEntry>();
            var list = (ManualSlipEntryOpService.GetManualSlipEntryBatchList()).merchManualTxns;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.SettleId.ToLower().Contains(Params.sSearch) || p.BusnLocation.ToLower().Contains(Params.sSearch) || p.SelectedTermId.ToString().ToLower().Contains(Params.sSearch) || p.SiteId.Contains(Params.sSearch) || p.BatchId.Contains(Params.sSearch) || p.InvoiceNo.ToString().Contains(Params.sSearch) || p.SettleDate.Contains(Params.sSearch) || p.TotalCnt.ToString().Contains(Params.sSearch) || p.DisplayTotalAmt.Contains(Params.sSearch) || p.Descp.ToLower().Contains(Params.sSearch) || p.SelectedSts.ToLower().Contains(Params.sSearch) || p._CreationDatenUserId.CreationDate.ToLower().Contains(Params.sSearch) || p._CreationDatenUserId.UserId.ToLower().Contains(Params.sSearch) || p.TxnDescp.ToLower().Contains(Params.sSearch)).ToList();
                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),

                aaData = _filtered.Select(x => new object[] { 
                    x.SettleId,
                    x.BusnLocation,
                    x.SelectedTermId,
                    x.SiteId,
                    x.BatchId,
                    x.InvoiceNo,
                    x.SettleDate,
                    x.TotalCnt, 
                    x.DisplayTotalAmt,
                    x.Descp,
                    x.SelectedSts,
                    x._CreationDatenUserId.CreationDate,
                    x._CreationDatenUserId.UserId,
                    x.TxnDescp
                })
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ftManualSlipBatchDetail(string SettleId)
        {
            var data = (await ManualSlipEntryOpService.GetManualSlipEntryBatchDetail(SettleId)).manualSlipEntryBatchDetail;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DelManualSlipBatch(ManualSlipEntry _manualSlipEntry)
        {
           var delManualSlip = ManualSlipEntryOpService.DeleteMerchManualTxn(_manualSlipEntry.BatchId, _manualSlipEntry.SettleId, _manualSlipEntry.TxnId, _manualSlipEntry.TxnDetailId);
            return Json(new { resultCd = delManualSlip }, JsonRequestBehavior.AllowGet);
        }
        #endregion "batch"

        #region "product"
        [CompressFilter]
        public  ActionResult ftManualSlipProdList(jQueryDataTableParamModel Params, string TxnId)
        {
            var _filtered = new List<ManualTxnProduct>();
            var list = ManualSlipEntryOpService.GetManualTxnProducts(TxnId).manualTxnProducts;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.BatchId) ? p.BatchId : string.Empty).ToLower().Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.SelectedProdCd) ? p.SelectedProdCd : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.Quantity) ? p.Quantity : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ProdAmt) ? p.ProdAmt : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.ProdDesc) ? p.ProdDesc : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p._CreationDatenUserId.CreationDate) ? p._CreationDatenUserId.CreationDate : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p._CreationDatenUserId.UserId) ? p._CreationDatenUserId.UserId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnId) ? p.TxnId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.TxnDetailId) ? p.TxnDetailId : string.Empty).Contains(Params.sSearch) ||
                                            (!string.IsNullOrEmpty(p.LastUpdDate) ? p.LastUpdDate : string.Empty).Contains(Params.sSearch)).ToList();

                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = _filtered.Select(x => new object[] { x.BatchId, x.SelectedProdCd, x.Quantity, x.ProdAmt, x.ProdDesc, x.SelectedVATCd, x.VATRate, x.VATAmt, x._CreationDatenUserId.CreationDate, x._CreationDatenUserId.UserId, x.LastUpdDate, x.SettleId, x.TxnId, x.TxnDetailId })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ftManualSlipProdDetail(string TxnId, string TxnDetailId)
        {
            var data = (await ManualSlipEntryOpService.GetMerchManualTxnProductDetail(TxnId, TxnDetailId)).manualSlipEntryBatchDetail;
            return Json(new { resultCd = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DelManualSlipProd(ManualSlipEntry _TxnProduct)
        {
            var data = ManualSlipEntryOpService.DeleteMerchManualTxn(_TxnProduct.BatchId, _TxnProduct.SettleId, _TxnProduct.TxnId, _TxnProduct.TxnDetailId);
            return Json(new { resultCd = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveManualSlipProd(ManualSlipEntry _ManualTxnProduct)
        {
            decimal totalPrice = 0;
            _ManualTxnProduct.UserId = GetUserId;
            if (_ManualTxnProduct.UnitPrice == "0")
            {
                if (_ManualTxnProduct.Quantity != "0" && _ManualTxnProduct.ProdAmt != "0")
                {
                    totalPrice = Convert.ToDecimal(_ManualTxnProduct.ProdAmt) / Convert.ToDecimal(_ManualTxnProduct.Quantity);
                    _ManualTxnProduct.UnitPrice = Convert.ToString(totalPrice);
                    var SaveManualProd = await ManualSlipEntryOpService.SaveMerchManualTxnProduct(_ManualTxnProduct);
                    return Json(new { resultCd = SaveManualProd, txnDetailId = SaveManualProd.paraRes.TxnDetailId }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    totalPrice = Convert.ToDecimal(_ManualTxnProduct.UnitPrice);
                    var SaveManualProd = await ManualSlipEntryOpService.SaveMerchManualTxnProduct(_ManualTxnProduct);
                    return Json(new { resultCd = SaveManualProd, txnDetailId = SaveManualProd.paraRes.TxnDetailId }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var SaveManualProd = await ManualSlipEntryOpService.SaveMerchManualTxnProduct(_ManualTxnProduct);
                return Json(new { resultCd = SaveManualProd, txnDetailId = SaveManualProd.paraRes.TxnDetailId }, JsonRequestBehavior.AllowGet);
            }
        }      
        #endregion "product"
    }
}