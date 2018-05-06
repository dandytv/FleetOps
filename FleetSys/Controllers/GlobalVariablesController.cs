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
using CardTrend.Domain.Dto.GlobalVariables;
namespace FleetSys.Controllers
{
    [Authorize(Roles = "Internal")]
    public class GlobalVariablesController : BaseController
    {
        [Accessibility]
        public ActionResult Index()
        {
            return View(new LookupParameters());
        }


        #region "List Of Web Pages"
        //[CompressFilter]
        [AccessibilityXtra]
        public ActionResult Tmpl(string Prefix, string BehaveAsPrefix = null)
        {
            if (!string.IsNullOrEmpty(BehaveAsPrefix))
                Prefix = BehaveAsPrefix;
            switch (Prefix)
            {
                case "lup":
                    return PartialView(this.getPartialPath("GlobalVariables", "LookupParameter_Partial"), new LookupParameters());
                case "lag":
                    return PartialView(this.getPartialPath("GlobalVariables", "Language_Partial"), new LookupParameters());
                case "sta":
                    return PartialView(this.getPartialPath("GlobalVariables", "State_Partial"), new LookupParameters());
                case "city":
                    return PartialView(this.getPartialPath("GlobalVariables", "City_Partial"), new LookupParameters());
                case "pdt":
                    return PartialView(this.getPartialPath("GlobalVariables", "ProdGroup_Partial"), new LookupParameters());
                case "pdl":
                    return PartialView(this.getPartialPath("GlobalVariables", "ProdList_Partial"), new LookupParameters());
                case "rbp":
                    return PartialView(this.getPartialPath("GlobalVariables", "RebatePlan_Partial"), new LookupParameters());
                case "evt":
                    return PartialView(this.getPartialPath("GlobalVariables", "Event_Type_List_Partial"), new LookupParameters());
                case "evt_dtl":
                    return PartialView(this.getPartialPath("GlobalVariables", "Event_Type_Detail_Partial"), new LookupParameters());

                default:
                    return PartialView();
            }
        }
        #endregion
        public async Task<ActionResult> FillData(string Code, string id = null)
        {
            LookupParameters dropDowns;
            switch (Code)
            {
                case "lp":
                    var Countries = await BaseService.GetRefLib("Country");
                    dropDowns = new LookupParameters
                      {
                          Countries = Countries,
                          States = Countries.Count() > 1? await BaseService.WebGetState(Countries[1].Value): null
                      };
                    var buttons = System.Web.HttpContext.Current.Session["CtrlAccessibility"];
                    return Json(new { Model = new LookupParameters(), Selects = dropDowns, buttonPermissions = buttons }, JsonRequestBehavior.AllowGet);
                case "pg":
                    dropDowns = new LookupParameters
                      {
                          ProductCategory = await BaseService.GetRefLib("ProdCategory"),
                          ProductType = await BaseService.GetRefLib("ProdType"),
                          BillingPlan = (await BaseService.WebGetPlan("0")),
                          ProductCodes = await BaseService.WebGetProduct(null, false)
                      };
                    var values = dropDowns.BillingPlan.Select(p => p.Value).ToList();
                    return Json(new { Model = new LookupParameters(), Selects = dropDowns }, JsonRequestBehavior.AllowGet);

                case "rb":
                    dropDowns = new LookupParameters
                    {
                        RebateType = await BaseService.GetRefLib("RebatePlanInd"),
                    };

                    return Json(new { Model = new LookupParameters(), Selects = dropDowns }, JsonRequestBehavior.AllowGet);

                case "ev":
                    dropDowns = new LookupParameters
                  {
                      EventType = await BaseService.GetRefLib("NtfEventType"),
                      Priority = await BaseService.GetRefLib("Priority"),
                      Status = await BaseService.GetRefLib("Status"),
                      Scope = await BaseService.GetRefLib("NtfEventOwner",null,"1",null),
                      Owner = await BaseService.GetRefLib("NtfEventOwner", null, "1", null),
                      Frequency = await BaseService.GetRefLib("NtfEventPeriodType"),
                      RefTo = await BaseService.GetEvtRefConf(id),
                      Languages = await BaseService.GetRefLib("Language"),

                  };
                    var result = (await GlobalVariableOpService.GetEventTypes(id)).lookupParameters;
                    return Json(new { Model = result, Selects = dropDowns }, JsonRequestBehavior.AllowGet);
                default:
                    return Json(new { }, JsonRequestBehavior.AllowGet);
            }
        }
        #region "Lookup Parameters"

        public async Task<ActionResult> GetStates(string CountryCd)
        {
            var States = await BaseService.WebGetState(CountryCd);
            return Json(States, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> WebRefSelect(string RefType, string RefCd, string RefNo, string RefId)
        {
            var result = (await GlobalVariableOpService.GetRefDetail(RefType, RefCd, RefNo, RefId)).lookupParameter;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> WebRefMaint(LookupParameters globalVariable)
        {
            var result = await GlobalVariableOpService.SaveRefMaint(globalVariable, globalVariable.flag);
            return Json(result);
        }

        public async Task<JsonResult> WebRefListSelect(string refType, jQueryDataTableParamModel Params)
        {
            ViewBag.CurrentStatus = refType;
            var _filtered = new List<LookupParameters>();
            var list = (await GlobalVariableOpService.GetRefList(refType)).lookupParameters;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (refType.ToLower() == "state")
            {


                if (!string.IsNullOrEmpty(Params.sSearch))
                {
                    _filtered = list.Where(p => (!string.IsNullOrEmpty(p.Country) ? p.Country: string.Empty).ToLower().Contains(Params.sSearch.ToLower()) || 
                                                (!string.IsNullOrEmpty(p.StateCode) ? p.StateCode : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) ||
                                                (!string.IsNullOrEmpty(p.StateName) ? p.StateName : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) ||
                                                (!string.IsNullOrEmpty(p.ParameterCode) ? p.ParameterCode : string.Empty).ToLower().Contains(Params.sSearch.ToLower())).ToList();
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
                    aaData = _filtered.Select(p => new object[] { p.Country, p.StateCode, p.StateName, p.ParameterCode })
                }, JsonRequestBehavior.AllowGet);
            }
            else if (refType.ToLower() == "city")
            {

                if (!string.IsNullOrEmpty(Params.sSearch))
                {
                    _filtered = list.Where(p => (!string.IsNullOrEmpty(p.Country) ? p.Country : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) ||
                                                (!string.IsNullOrEmpty(p.StateName) ? p.StateName : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) ||
                                                (!string.IsNullOrEmpty(p.ParameterCode) ? p.ParameterCode : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) ||
                                                (!string.IsNullOrEmpty(p.CityName) ? p.CityName : string.Empty).ToLower().Contains(Params.sSearch.ToLower())).ToList();
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
                    aaData = _filtered.Select(p => new object[] { p.Country, p.StateName, p.ParameterCode, p.CityName })
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                if (!string.IsNullOrEmpty(Params.sSearch))
                {
                    _filtered = list.Where(p => ( !string.IsNullOrEmpty(p.ParameterCode) ? p.ParameterCode : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) ||
                                                (!string.IsNullOrEmpty(p.ParameterDescp) ? p.ParameterDescp : string.Empty).ToLower().Contains(Params.sSearch.ToLower())).ToList();
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
                    aaData = _filtered.Select(p => new object[] { p.ParameterCode, p.ParameterDescp, null, null })
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region "Product Parameters"
        public async Task<ActionResult> WebProdGroupRefListSelect(jQueryDataTableParamModel Params)
        {
            var list = (await GlobalVariableOpService.GetProdGroupRefs()).lookupParameters;
            var _filtered = new List<LookupParameters>();
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.Descp) ? p.Descp : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) || 
                                            (!string.IsNullOrEmpty(p.SelectedProductGroup) ? p.SelectedProductGroup : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) || 
                                            (!string.IsNullOrEmpty(p.UserId) ? p.UserId : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) ||
                                            (!string.IsNullOrEmpty(p.LastUpdated) ? p.LastUpdated : string.Empty).ToLower().Contains(Params.sSearch.ToLower())).ToList();
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
                aaData = _filtered.Select(p => new object[] { null, p.SelectedProductGroup, p.Descp, p.LastUpdated, p.UserId, p.ProductCode, p.ProductName, p.SelectedProductCategory, p.SelectedProductType })
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> WebProdGroupRefSelect(string ProdGroup)
        {
            var data = (await GlobalVariableOpService.GetProdGroupRefSelect(ProdGroup)).lookupParameters;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> WebProdRefSelect(string ProdCd)
        {
            var data = (await GlobalVariableOpService.GetDetailProdRefs(ProdCd)).lookupParameters;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> WebProdGroupRefMaint(LookupParameters param)
        {
            var data = await GlobalVariableOpService.SaveProdGroupRefMaint(param, GetUserId);
            return Json(data);
        }
        [HttpPost]
        public async Task<ActionResult> WebProdRefMaint(LookupParameters _Param)
        {
            var result = await GlobalVariableOpService.SaveProdRefMaint(_Param, GetUserId);
            return Json(result);
        }

        public async Task<ActionResult> WebProdRefListSelect(jQueryDataTableParamModel Params)
        {
            var list = (await GlobalVariableOpService.GetProdRefs()).lookupParameters;
            var _filtered = new List<LookupParameters>();
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.Descp) ? p.Descp : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) ||
                                            (!string.IsNullOrEmpty(p.UnitPrice)?p.UnitPrice : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) ||
                                            (!string.IsNullOrEmpty(p.SelectedProductType) ? p.SelectedProductType : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) ||
                                            (!string.IsNullOrEmpty(p.SelectedProductCategory) ? p.SelectedProductCategory : string.Empty ).ToLower().Contains(Params.sSearch.ToLower()) ||
                                            (!string.IsNullOrEmpty(p.ProductName) ? p.ProductName: string.Empty).ToLower().Contains(Params.sSearch.ToLower()) ||
                                            (!string.IsNullOrEmpty(p.ProductCode) ? p.ProductCode : string.Empty ).ToLower().Contains(Params.sSearch.ToLower()) ||
                                            (!string.IsNullOrEmpty(p.LastUpdated) ? p.LastUpdated : string.Empty).Contains(Params.sSearch.ToLower())).ToList();
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
                aaData = _filtered.Select(p => new object[] { p.ProductCode, p.ProductName, p.Descp, p.SelectedProductCategory, p.SelectedProductType, p.UnitPrice, p.SelectedBillingPlan, p.LastUpdated })
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> WebUndefinedProdType(jQueryDataTableParamModel Params)
        {
            var list = (await GlobalVariableOpService.WebUndefinedProdType()).lookupParameters;
            var _filtered = new List<LookupParameters>();
            _filtered = list.Skip(Params.iDisplayStart).Take(Params.iDisplayLength).ToList();

            return Json(new
            {
                sEcho = Params.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = _filtered.Select(p => new object[] { p.ProductCode, p.ProductName, p.Descp, p.SelectedProductCategory, p.SelectedProductType, p.UnitPrice })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Rebate Plan Parameter"


        public async Task<ActionResult> WebRebatePlanSelect(string PlanId)
        {
            var list = (await GlobalVariableOpService.GetRebatePlans(PlanId)).lookupParameters;
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> WebRebatePlanListSelect(jQueryDataTableParamModel Params)
        {
            var list = (await GlobalVariableOpService.GetRebatePlanDetails()).lookupParameters;

            var _filtered = new List<LookupParameters>();
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }

            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.PlanId) ? p.PlanId : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) || 
                                            (!string.IsNullOrEmpty(p.Descp) ? p.Descp : string.Empty).ToLower().Contains(Params.sSearch.ToLower())).ToList();
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
                aaData = _filtered.Select(p => new object[] { null, p.PlanId, p.Descp, p.type, p.EffectiveFrom, p.ExpiryDate, p.LastUpdated, p.PlanUserId, p.MinPurchaseAmt, p.SubSeqPurchaseAmt, p.SubSeqBillingAmt, p.BillingPlanLastUpdate, p.BillingPlanUserId })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> WebRebatePlanMaint(LookupParameters Params)
        {
            var result = await GlobalVariableOpService.SaveRebatePlanMaint(Params,GetUserId);
            return Json(result);
        }
        #endregion

        #region "Event Types"

        public async Task<ActionResult> WebEventTypeListSelect(jQueryDataTableParamModel Params)
        {
            var _filtered = new List<LookupParameters>();
            var list = (await GlobalVariableOpService.GetDetailEventTypes()).lookupParameters;
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                Params.sSearch = Params.sSearch.ToLower();
            }
            if (!string.IsNullOrEmpty(Params.sSearch))
            {
                _filtered = list.Where(p => (!string.IsNullOrEmpty(p.EventTypeId) ? p.EventTypeId : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) || 
                                            (! string.IsNullOrEmpty(p.SelectedEventType) ? p.SelectedEventType : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) ||
                                            (!string.IsNullOrEmpty(p.ShortDescp) ? p.ShortDescp : string.Empty).ToLower().Contains(Params.sSearch.ToLower()) || 
                                            (!string.IsNullOrEmpty(p.DetailedDescp) ? p.DetailedDescp : string.Empty).ToLower().Contains(Params.sSearch.ToLower())).ToList();
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
                aaData = _filtered.Select(p => new object[] { p.EventTypeId, p.SelectedEventType, p.ShortDescp, p.DetailedDescp, p.SelectedStatus, p.LastUpdated, p.UpdatedBy })
            }, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> WebEventTypeSelect(string EventTypeId)
        {
            var result = await GlobalVariableOpService.GetEventTypes(EventTypeId);
            return Json(new { Model = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> WebEventTypeMaint(LookupParameters Params)
        {
            var result = await GlobalVariableOpService.SaveEventTypeMaint(Params,GetUserId);
            return Json(result);
        }

        public async Task<ActionResult> WebEventTypeTemplateSelect(string EventTypeId)
        {
            var list = (await GlobalVariableOpService.GetEventTypeTemplates(EventTypeId)).TmplDisplays;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadTmpl(string path)
        {
            path = path + ".pdf";
            byte[] fileData = System.IO.File.ReadAllBytes(path);
            string contentType = MimeMapping.GetMimeMapping(path);
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = path,
                Inline = true
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(fileData, contentType);
        }

        #endregion
    }
}