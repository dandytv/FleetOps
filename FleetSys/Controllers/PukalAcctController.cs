using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaxMind.GeoIP;
using ModelSector;
using CCMS.ModelSector;
using FleetOps.Models;
using System.Threading.Tasks;
using FleetOps.App_Start;
using FleetOps.ViewModel;
using FleetSys.Models;
using AutoMapper;
namespace FleetSys.Controllers
{
    [Authorize(Roles = "Internal")]
    public class PukalAcctController : BaseController
    {
        private PukalAcctOps objPukalAcctOps = new PukalAcctOps();

        [AccessibilityXtra("PukalAcct","Index","pkl")]
        public ActionResult Index()
        {
            PukalAcctInfo acctInfo = new PukalAcctInfo();
            return View(acctInfo);
        }
        [AccessibilityXtra]
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                case "pkal":
                    return PartialView(this.getPartialPath("PukalAcct", "BatchInfo_Partial"), new PukalAcctMaintInfo());
                default:
                    return PartialView();
            }
        }
        public async Task<ActionResult> ftGetPukalAcctBatchList(jQueryDataTableParamModel Params, string refCd, string AcctOfficeCd, int cycStmtId)
        {
            var _filtered = new List<PukalAcctBatchList>();
            var list = (await PukalAcctOpService.GetPukalAccounts(refCd, AcctOfficeCd, cycStmtId)).pukalPayments;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.BatchId.ToString().Contains(Params.sSearch) 
                                            || p.RefCd.ToString().Contains(Params.sSearch) 
                                            || p.ChequeNo.ToString().Contains(Params.sSearch)
                                            || p.AreaCode.ToString().Contains(Params.sSearch)
                                            || p.ChequeAmt.ToString().Contains(Params.sSearch)
                                            || p.Owner.ToString().Contains(Params.sSearch)
                                            || p.SlipNo.ToString().Contains(Params.sSearch)
                                            || p.IssBank.ToString().Contains(Params.sSearch)
                                            ).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.BatchId,x.RefCd, x.AreaCode,x.ChequeNo,x.ChequeAmt,x.SlipNo, x.IssBank, x.CreationDate,x.StatementDate,x.StsDescp,x.Owner})
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ftGetPukalSedutView(jQueryDataTableParamModel Params, string refCd, string acctOfficeCd, string Sts)
        {
            var _filtered = new List<WebPukalSedutList>();
            var list = (await PukalAcctOpService.GetPukalSeduts(refCd, acctOfficeCd, Sts)).pukalSeduts;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.AcctNo.ToString().ToLower().Contains(Params.sSearch.ToLower())
                                            || p.ActivationDate.ToString().ToLower().Contains(Params.sSearch.ToLower())
                                            || p.CompanyName.ToString().ToLower().Contains(Params.sSearch.ToLower())
                                            || p.PukalAmt.ToString().ToLower().Contains(Params.sSearch.ToLower())
                                            || p.Status.ToString().ToLower().Contains(Params.sSearch.ToLower())
                                            || p.UserId.ToString().ToLower().Contains(Params.sSearch.ToLower())
                                            || p.StmtDate.ToString().ToLower().Contains(Params.sSearch.ToLower())).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.AcctNo, x.CompanyName, x.ActivationDate, x.TerminationDate, x.PukalAmt, x.StmtDate, x.Status, x.UserId })
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetWebTxnPukalPaymentSelect(long batchId, string refCd, string AcctOfficeCd,int cycStmtId)
        {
            var _SaveMultiAdj = (await PukalAcctOpService.GetPukalAcctBatches(batchId, refCd, AcctOfficeCd, cycStmtId)).pukalAcctBatches;
            return Json(new { list = _SaveMultiAdj }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> FTWebTxnPukalPaymentMaint(PukalAcctMaintInfo obj)
        {
            var _TraceInfo = await objPukalAcctOps.SavePukalAcctEdits(obj);
            return Json(new { result = _TraceInfo }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> WebPukalSedutMaint(WebPukalSedutList pukalSedut)
        {
            decimal _PukalAmt = 0;
            Decimal.TryParse(pukalSedut.PukalAmt, out _PukalAmt);
            var ResultInfo = await objPukalAcctOps.SavePukalSedut(pukalSedut.AcctNo, _PukalAmt, BaseClass.GetClaimsInfo("userid"));
            return Json(new { result = ResultInfo }, JsonRequestBehavior.AllowGet);
        }
        #region "Fill Data"
        [CompressFilter]
        public async Task<JsonResult> FillData(string Prefix)
        {
            switch (Prefix.ToLower())
            {
                case "gen":
                    var genData = new PukalAcctMaintInfo();
                    var genSelects = new PukalAcctInfo
                    {
                        AreaCode = await BaseService.GetRefLib("AcctOfficeCd",null,null,null,false),
                        RefCd = await BaseService.GetRefLib("PaymtInfoType", null, null, null, false),
                        StmDate = (await BaseService.GetCycleStmt(null)).RefLibLst,
                        IssBank = await BaseService.GetRefLib("Bank")
                    };
                    return Json(new { Model = genData, Selects = genSelects }, JsonRequestBehavior.AllowGet);

                default:
                    HttpContext.Response.StatusCode = 404;
                    return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        [CompressFilter]
        public async Task<JsonResult> FillMaintData(string batchId, string statementDate)
        {
            var maintData = new PukalAcctMaintInfo();
            var pkPaymentSelect = await objPukalAcctOps.GetPukapaymentSelect( Convert.ToInt32(batchId));
             if(pkPaymentSelect.Owner != null )
             {
                 maintData.ChequeAmt = pkPaymentSelect.ChequeAmt;
                 maintData.ChequeNo = pkPaymentSelect.ChequeNo;
                 maintData.SelectedTxnCd = pkPaymentSelect.TxnCd;
                 maintData.SelectedSettlement = pkPaymentSelect.GLSettlement;
                 maintData.SelectedOwner = pkPaymentSelect.Owner;
                 maintData.CreationDate = pkPaymentSelect.TxnDate;
                 maintData.StatementDate = pkPaymentSelect.StmtDate;
                 maintData.AcctOfficeCd = pkPaymentSelect.AcctOfficeCd;
                 maintData.RefCd = pkPaymentSelect.RefCd;
                 maintData.CycStmtId = pkPaymentSelect.CycStmtId;
                 maintData.Sts = pkPaymentSelect.Sts;
                 maintData.SlipNo = pkPaymentSelect.SlipNo;
                 maintData.SelectedIssBank = pkPaymentSelect.IssBank;

             }else
             {
                 maintData.SelectedTxnCd = string.Empty;
                 maintData.SelectedSettlement = string.Empty;
                 maintData.SelectedOwner = BaseClass.GetClaimsInfo("userid");
                 maintData.CreationDate = DateTime.Now.ToString("dd/MM/yyyy");
                 maintData.StatementDate = statementDate;
                 maintData.CycStmtId = 0;
                 maintData.ChequeNo = 0;
                 maintData.Sts = "A";
                 maintData.SlipNo = string.Empty;
                 maintData.SelectedIssBank = string.Empty;
             }
            maintData.BatchId = int.Parse(batchId);
            var maintSelects = new PukalAcctMaintInfo
            {
                TxnCd = await BaseService.WebGetTxnCode("I","PaymtTxnCategoryMapInd", "Y"),  
                Owner = await BaseClass.WebUserAccessListSelect(),
                GLSettlement = await BaseClass.WebGetRefLib("GLSettlement"),
                IssBank = await BaseClass.WebGetRefLib("Bank")
            };
            // remove upper case
            if(maintSelects.Owner.Count() > 0)
            {
                for(int i = 1;i< maintSelects.Owner.Count();i ++)
                {
                    maintSelects.Owner.ToList()[i].Text = maintSelects.Owner.ToList()[i].Text.ToLower();
                    maintSelects.Owner.ToList()[i].Value = maintSelects.Owner.ToList()[i].Value.ToLower();
                }
            }
            return Json(new { Model = maintData, Selects = maintSelects }, JsonRequestBehavior.AllowGet);
        }
    }
    #endregion
}
