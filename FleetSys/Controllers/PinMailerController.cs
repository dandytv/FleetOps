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
    public class PinMailerController : BaseController
    {
        // GET: Applications
        [Accessibility]
        public ActionResult Index()
        {
            return View();
        }
        [AccessibilityXtra]
        public ActionResult Tmpl(string Prefix)
        {
            switch (Prefix)
            {
                case "NULL":
                    return PartialView(this.getPartialPath("PinMailer", "BatchInfo_Partial"), new PinMailerBatchView());
                default:
                    return PartialView();
            }
        }

        [HttpPost]
        public async Task<ActionResult> ftPinMailerPrintList(int batchId, List<long> cardList)
        {
            var _TraceInfo = await PinMailerOpService.SavePinMailerPrint(batchId, cardList);
            return Json(new { result = _TraceInfo }, JsonRequestBehavior.AllowGet);

        }
        public async Task<ActionResult> ftGetPinMailerBatchList(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<PinMailerBatchList>();
            var list = (await PinMailerOpService.GetPinMailerBatchList()).pinMailerBatchs;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.BatchID.ToString().Contains(Params.sSearch)).ToList();
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
                aaData = _filtered.Select(x => new object[] { x.BatchID,
                    x.CreationDate,
                    x.Sts, 
                    x.Count
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ftGetPinMailerBatchView(jQueryDataTableParamModel Params, long batchID, int status)
        {
            var _filtered = new List<PinMailerBatchView>();
            var list = (await PinMailerOpService.GetPinMailerBatchView(batchID, status)).pinMailerBatchViews;

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => p.CardNo.Contains(Params.sSearch) || p.CompName.ToLower().Contains(Params.sSearch) || p.DriverName.ToLower().Contains(Params.sSearch)).ToList();
                _filtered = _filtered.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }
            else
            {
                _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();
            }

            if (status == 0 || status == 1)
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { 
                    null,
                    x.CardNo,
                    x.StsDescp,
                    x.CardCreationDate,
                    x.CompName,
                    x.DriverName,
                    x.PIC,
                    x.Address
                })
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    sEcho = Params.sEcho,
                    iTotalRecords = list.Count(),
                    iTotalDisplayRecords = list.Count(),
                    aaData = _filtered.Select(x => new object[] { 
                    x.CardNo,
                    x.StsDescp,
                    x.CardCreationDate,
                    x.CompName,
                    x.DriverName,
                    x.PIC,
                    x.Address
                })
                }, JsonRequestBehavior.AllowGet);
            }

        }
        //    #endregion
    }
}
