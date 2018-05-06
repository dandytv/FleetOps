using AutoMapper;
using CCMS.ModelSector;
using FleetOps.Models;
using FleetSys.Models;
using ModelSector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Utilities.DAL;
namespace FleetSys.Controllers
{
    public class TxnSearchController : BaseController
    {
        // GET: TxnSearch
        public ActionResult Index()
        {
            return View(new TxnSearchModel());
        }
        public async Task<ActionResult> FillData()
        {
            var Selects = new TxnSearchModel
            {
                TxnCategory = await BaseService.GetTransactionCategory(),
                TxnCd = await BaseService.WebGetTxnCode("I",null,null),
                MerchTxnCd = await BaseService.WebGetTxnCode("A",null,null),
                StatementDate = (await BaseService.GetCycleStmt(null)).RefLibLst
            };
            var StatementDateLst = Selects.StatementDate.ToList();
            StatementDateLst.Insert(0, new SelectListItem { Value = "", Text = "" });
            Selects.StatementDate = StatementDateLst as IEnumerable<SelectListItem>;
            return Json(new { Selects = Selects, Model = new TxnSearchModel() }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> WebAcctTxnSearch(jQueryDataTableParamModel Params, TxnSearchModel Model, bool isDownload = false)
        {
            var _filtered = new List<AcctPostedTxnSearch>();
            var list = (await TransactionSearchService.GetAccountTransactionSearch(Convert.ToInt64(Model.AcctNo), Convert.ToInt64(Model.CardNo), Model.SelectedTxnCategory, Convert.ToInt32(Model.SelectedTxnCd), Model.FromDate, Model.ToDate, Model.SelectedStatementDate)).transactionSearches;
        
            if (!isDownload)
            {
                if (!string.IsNullOrEmpty(Params.sSearch))
                {
                    Params.sSearch = Params.sSearch.ToLower();
                }
                if (!string.IsNullOrEmpty(Params.sSearch))
                {
                    _filtered = list.Where(p => p.InvoicDt.ToLower().Contains(Params.sSearch) || p.TxnDate.ToLower().Contains(Params.sSearch)).ToList();
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
                  aaData = _filtered.Select(x => new object[] {null,x.InvoicDt, x.TxnDate,x.PrcsDate, x. AcctNo,x.SelectedCardNo,x.AuthCardNo , x.TxnDesp,x.VehRegNo,x.Stan, x.ApproveCd, x.RRn,x.VATNo, x.Dealer, x.TxnId, x.TxnAmt,x.ProductDescp,x.Quantity,x.ProductAmt,x.VATAmt,x.VATCd,x.VATRate,
                })
              }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var title = "Transaction search | Account";
                var toExport = new List<string[]>();
                var Header = list.First().ExcelHeader;
                foreach (var item in list)
                {
                    toExport.Add(item.ExcelBody());
                }
                var ExcelPkg = Common.CommonHelpers.CreateExcel(Header, toExport, title);
                return File(ExcelPkg.GetAsByteArray(), "application/vnd.ms-excel", title + ".xlsx");
            }
        }
        public async Task<ActionResult> WebMerchTxnSearch(jQueryDataTableParamModel Params, TxnSearchModel Model, bool isDownload)
        {
            TxnSearchMaint _Maint = new TxnSearchMaint();
            var _filtered = new List<MerchPostedTxnSearch>();

            var list = (await TransactionSearchService.GetMerchTransactionSearch(Model.BusnLocation,Model.MerchAcctNo,Model.SelectedMerchTxnCd, Model.MerchFromDate, Model.MerchToDate, Model.SelectedTxnCategory)).merchPostedTxnSearches;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }
            if (!isDownload)
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
                return Json(new
              {
                  sEcho = Params.sEcho,
                  iTotalRecords = list.Count(),
                  iTotalDisplayRecords = list.Count(),
                  aaData = _filtered.Select(x => new object[] {null, x.SelectedDealer,x.TermBatch, x.TxnDate,x.cardNo,x.TxnDesp,x.TxnAmt,x.TermId,
                      x.AuthNo,x.AuthCardNo,x.PrcsDate,x.TxnId,x.ProductDescp, x.ProductQty,x.ProductAmt,x.VATAmt,x.BaseAmt,x.VATCd,x.VATRate,
                })
              }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var title = "Transaction search | Merchant";
                var toExport = new List<string[]>();
                var Header = list.First().ExcelHeader;
                foreach (var item in list)
                {
                    toExport.Add(item.ExcelBody());
                }
                var ExcelPkg = Common.CommonHelpers.CreateExcel(Header, toExport, title);
                return File(ExcelPkg.GetAsByteArray(), "application/vnd.ms-excel", title + ".xlsx");
            }
        }
        public async Task<JsonResult> WebGetObjectDetail(string Prefix, string Value)
        {
            Prefix = Prefix.Substring(0, 3);
            var objectDetail = (await TransactionSearchService.GetObjectDetail(Prefix, Value)).objectDetail;
            return Json(new { objectDetail = objectDetail }, JsonRequestBehavior.AllowGet);
        }
    }
}