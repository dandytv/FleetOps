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
using CardTrend.Common.Extensions;

namespace FleetSys.Controllers
{
    public class SOASummaryController : BaseController
    {
        [Accessibility]
        public ActionResult Index()
        {
            return View(new AcctSOA());
        }

        public JsonResult FIllData()
        {
            return Json(new { Model = new AcctSOA()}, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> WebAcctSOASumm(AcctSOA _AcctSOA)
        {
            var accountSOA = (await AccountSOAOpService.GetAcctSOASummSelect(_AcctSOA.AcctNo)).acctSOASummary;
            var AcctSOA = Mapper.Map<AcctSOA>(accountSOA);
            return Json(AcctSOA, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> WebAcctSOASummList(AcctSOA _AcctSOA, jQueryDataTableParamModel Params)
        {
            var _filtered = new List<AcctSOA>();
            var list = Mapper.Map<List<AcctSOA>>((await AccountSOAOpService.GetAcctSOASummList(_AcctSOA.AcctNo)).AcctSOASummaryLst);

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.Month) ? p.Month : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.SelectedStmtDate) ? p.SelectedStmtDate : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.ClseBalance) ? p.ClseBalance : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.MinimumPayment) ? p.MinimumPayment : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.Debits) ? p.Debits : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.Credits) ? p.Credits : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.Sales) ? p.Sales : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.DBAdjust) ? p.DBAdjust : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.Payment) ? p.Payment : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.age) ? p.age : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.Rchq) ? p.Rchq : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.Lpay) ? p.Lpay : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.Dun) ? p.Dun : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.Rv) ? p.Rv : string.Empty).ToLower().Contains(Params.sSearch)).ToList();

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
                aaData = _filtered.Select(x => new object[] { x.Month, x.SelectedStmtDate, x.ClseBalance, x.MinimumPayment, x.Debits, x.Credits, x.Sales, x.DBAdjust, x.Charges, x.Payment, x.CRAdjust, x.age, x.Rchq, x.Lpay, x.Rv, x.Dun })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> WebAcctSOATxnCategoryList(AcctSOA _AcctSOA, jQueryDataTableParamModel Params)
        {
            var _filtered = new List<AcctSOA>();
            var acctSOA = await AccountSOAOpService.GetAcctSOATxnCategoryList(_AcctSOA.AcctNo, _AcctSOA.SelectedStmtDate);
            var list = Mapper.Map<List<AcctSOA>>(acctSOA);

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.TxnCode) ? p.TxnCode : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.TxnEventCd) ? p.TxnEventCd : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.TxnDesc) ? p.TxnDesc : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.TotalCount) ? p.TotalCount : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.TotalAmt) ? p.TotalAmt : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.TotalItemQty) ? p.TotalItemQty : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.TotalItemAmt) ? p.TotalItemAmt : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.SelectedStmtDate) ? p.SelectedStmtDate : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.CompanyName) ? p.CompanyName : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.AcctNo) ? p.AcctNo : string.Empty).ToLower().Contains(Params.sSearch) ).ToList();


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
                    x.TxnCode,
                    x.TxnEventCd,
                    x.TxnDesc,
                    x.TotalCount,
                    x.TotalAmt,
                    x.TotalItemQty,
                    x.TotalItemAmt,
                    x.SelectedStmtDate,
                    x.CompanyName,
                    x.AcctNo
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> WebAcctSOATxnList(AcctSOA _AcctSOA, jQueryDataTableParamModel Params)
        {
            // await objAccountSOAOps.WebAcctSOATxnList(_AcctSOA);
            var _filtered = new List<AcctSOA>();
            var data = await AccountSOAOpService.GetAcctSOATxnList(_AcctSOA.AcctNo, _AcctSOA.SelectedStmtDate, _AcctSOA.TxnCode);
            var list = Mapper.Map<List<AcctSOA>>(data);

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {

                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.CardHolderNo) ? p.CardHolderNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.DriverCardNo) ? p.DriverCardNo : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.TxnDate) ? p.TxnDate : string.Empty).ToLower().Contains(Params.sSearch) ||

                                             (!string.IsNullOrEmpty(p.txnTime) ? p.txnTime : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.PostDate) ? p.PostDate : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.TxnCode) ? p.TxnCode : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.Curr) ? p.Curr : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.TxnAmt) ? p.TxnAmt : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.Amt) ? p.Amt : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.MerchantName) ? p.MerchantName : string.Empty).ToLower().Contains(Params.sSearch) ||
                                              (!string.IsNullOrEmpty(p.MCC) ? p.MCC : string.Empty).ToLower().Contains(Params.sSearch) ||
                                             (!string.IsNullOrEmpty(p.ChqRefNo) ? p.ChqRefNo : string.Empty).ToLower().Contains(Params.sSearch)).ToList();



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
                    
                    x.CardHolderNo,
                    x.MerchantID,
                    x.DriverCardNo,
                    x.MerchantName,
                    x.TxnDate,
                    x.txnTime,
                    x.PostDate,
                    x.DBAName,
                    x.TxnCode,
                    x.MCC,
                    x.Curr,
                    x.TxnAmt,
                    x.Amt,
                    x.RRn,
                    x.ChqRefNo,
                    //x.CompanyName,
                })
            }, JsonRequestBehavior.AllowGet);
        }
    }
}