using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.ControlList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.DAL.DAO
{
    public interface IControlDAO
    {
        Task<IList<SelectListItem>> GetPlasticTypeSelect();
        Task<IList<SelectListItem>> GetPlasticType(string module);
        Task<IList<SelectListItem>> WebGetCorpCd(bool value);
        Task<IList<SelectListItem>> GetCycle(string module);
        Task<IList<SelectListItem>> GetCAOReasonCd();
        Task<IList<SelectListItem>> GetRefLib(string refType, string RefNo = null, string RefInd = null, string RefId = null, bool PrependNull = true);
        Task<IList<SelectListItem>> GetRefLib2(string refType, string RefNo = null, string RefInd = null, string RefId = null, bool PrependNull = true);
        Task<IList<SelectListItem>> WebGetCardType(string AppcId = null, string CardNo = null, string ApplId = null, string AcctNo = null);
        IList<SelectListItem> GetDataVersion(string DataType = null, string Ind = null);
        Task<IList<SelectListItem>> WebGetState( string CtryCd);
        Task<IList<SelectListItem>> WebProductGroupSelect();
        Task<IList<SelectListItem>> WebGetPlan(string MapInd);
        Task<IList<SelectListItem>> WebGetTxnCode(string Module, string CategoryType = null, string ManualEntry = "y");
        Task<IList<SelectListItem>> WebGetProduct(string ApplId, bool combineProdCdnPrice = true);
        Task<IList<SelectListItem>> WebGetRptType();
        Task<IList<SelectListItem>> WebGetNextMilestone(int currentTaskNo, string workFlowCd);
        Task<IList<SelectListItem>> WebGetCycleStmt(string cycNo);
        Task<IList<SelectListItem>> WebGetTxnCategory();
        Task<IList<SelectListItem>> WebGetEvtRefConf(string evtTypeId);
        Task<IList<SelectListItem>> WebGetEvtType();
        Task<IList<SelectListItem>> WebGetEvtInd();
        Task<IList<SelectListItem>> WebGetEvtRef(string evtTypeId);
        Task<IList<SelectListItem>> WebGetUserAccess(string userAccess, bool PrependNull = true);
        Task<IList<SelectListItem>> WebGetPymtTxnCd(string TxnCat, string ShortDescp);
        Task<IList<SelectListItem>> WebGetTermId(string busnLocation);
        Task<IList<SelectListItem>> WebGetCostCentre(string refKey, string refTo, bool PrependNull = true);
        Task<IList<SelectListItem>> WebGetSKDS(string applId, string acctNo);
        Task<IList<SelectListItem>> WebGetFeeCd(string feeType, bool PrependNull = true);
        Task<IList<SelectListItem>> WebGetCardMedia();
        Task<IList<SelectListItem>> WebGetDealerBusnLoc(string acctNo, string cardNo, string state);
        Task<IList<SelectListItem>> WebGetMerchType(string merchType);
        Task<IList<SelectListItem>> WebGetCardNo(string acctNo);
        Task<IList<SelectListItem>> WebGetBillItemTxnCategory(string id);
        Task<IList<SelectListItem>> WebGetDealerByMerch(string acctNo);
        Task<IList<SelectListItem>> iFrameGetTxnCategory(string refName);
        Task<IList<SelectListItem>> WebGetDeviceModel();
        Task<IList<SelectListItem>> WebGetAcctSubsidy(string acctNo);
        IEnumerable<SelectListItem> WebGetYear();
        Task<IssMessageDTO> GetMessageCode(int msgCd);
    }
    public class ControlDAO : DAOBase, IControlDAO
    {
        private readonly string _connectionString = string.Empty;
        public ControlDAO(string connString)
        {
            _connectionString = connString;
        }
        public async Task<IList<SelectListItem>> GetRefLib(string refType, string RefNo = null, string RefInd = null, string RefId = null, bool PrependNull = true)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), refType, RefNo, RefInd, RefId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@RefType",
                                        "@RefNo",
                                        "@RefInd",
                                        "@RefId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<RefLibDTO>
                    (BuildSqlCommand("WebGetRefLib", paramCollection), paramCollection.ToArray())
                    .ToListAsync();


                var list = new List<SelectListItem>();
                if (PrependNull)
                {
                    list.Add(new SelectListItem { Text = "", Value = "" });
                }

                if(results.Count() > 0)
                {
                    foreach(var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                             Text = refLib.Descp,
                             Value = refLib.RefCd,
                             Selected = refLib.RefCd == "1"?true:false
                        });
                    }
                }

                return list;
            }
        }
        public async Task<IList<SelectListItem>> GetRefLib2(string refType, string RefNo = null, string RefInd = null, string RefId = null, bool PrependNull = true)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), refType, RefNo, RefInd, RefId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@RefType",
                                        "@RefNo",
                                        "@RefInd",
                                        "@RefId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<RefLibDTO>
                    (BuildSqlCommand("WebGetRefLib", paramCollection), paramCollection.ToArray())
                    .ToListAsync();


                var list = new List<SelectListItem>();
                if (PrependNull)
                {
                    list.Add(new SelectListItem { Text = "", Value = "" });
                }

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        string descp = Convert.ToString(refLib.Descp);
                        string[] stringseperators = new string[] { ":" };
                        var text = descp.Split(stringseperators, StringSplitOptions.None);

                        list.Add(new SelectListItem
                        {
                            Text = text[1],
                            Value = refLib.RefCd,
                            Selected = refLib.RefCd == "1" ? true : false
                        });
                    }
                }

                return list;
            }
        }
        public async Task<IList<SelectListItem>> GetPlasticTypeSelect()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo() };
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<PlasticTypeDTO>
                    (BuildSqlCommand("WebPlasticTypeSelect", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = refLib.PlasticType
                        });
                    }
                }

                if (list.Count > 0)
                    list.First().Selected = true;
                return list;
            }
        }
        public async Task<IList<SelectListItem>> GetPlasticType(string module)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), module };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@Module"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<PlasticTypeDTO>
                    (BuildSqlCommand("WebGetPlasticType", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = refLib.PlasticType
                        });
                    }
                }

                if (list.Count > 0)
                    list.First().Selected = true;
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetCorpCd(bool value)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo() };
                var paramNameList = new[]
                                   {
                                        "@AcqNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<CorpCdDTO>
                    (BuildSqlCommand("WebGetCorpCd", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        if(value)
                        {
                            list.Add(new SelectListItem
                            {
                                Text = refLib.Descp,
                                Value = refLib.CorpCd
                            });
                        }else
                        {
                            list.Add(new SelectListItem
                            {
                                Text = refLib.CorpCd,
                                Value = refLib.CorpCd
                            });
                        }

                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> GetCycle(string module)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), module };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@Module"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<CycleDTO>
                    (BuildSqlCommand("WebGetCycle", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = refLib.CycNo.ToString()
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> GetCAOReasonCd()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo() };
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<RefLibDTO>
                    (BuildSqlCommand("WebGetCAOReasonCd", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = refLib.RefCd
                        });
                    }
                }
                return list;
            }
        }
        public IEnumerable<SelectListItem> WebGetYear()
        {
            var yearlist = new List<SelectListItem>();
            foreach (var year in Enumerable.Range(1970, 60))
            {
                yearlist.Add(new SelectListItem
                {
                    Text = year.ToString(),
                    Value = year.ToString()
                });
            }
            return yearlist;
        }
        public async Task<IList<SelectListItem>> WebGetCardType(string AppcId = null, string CardNo = null, string ApplId = null, string AcctNo = null)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), AppcId, CardNo, ApplId, AcctNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
	                                    "@RefType",
                                        "@RefNo",
                                        "@RefInd",
                                        "@RefId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<CardTypeDTO>
                    (BuildSqlCommand("WebGetCardType", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = Convert.ToString((refLib.CardType))
                        });
                    }
                }

                return list;
            }
        }
        public IList<SelectListItem> GetDataVersion(string DataType = null, string Ind = null)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), DataType, Ind };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@DataType",
                                        "@Ind"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = cardtrendentities.Database.SqlQuery<DataVersionDTO>
                    (BuildSqlCommand("GetDataVersion", paramCollection), paramCollection.ToArray())
                    .ToList();

                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = refLib.DataType.ToString()
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetState(string CtryCd)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), CtryCd };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@CtryCd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);

                var results = await cardtrendentities.Database.SqlQuery<StateDTO>
                (BuildSqlCommand("WebGetState", paramCollection), paramCollection.ToArray())
                .ToListAsync();
                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = refLib.StateCd
                        });
                    }
                }
                return list;
            }

        }
        public async Task<IList<SelectListItem>> WebProductGroupSelect()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo()};
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);

                var results = await cardtrendentities.Database.SqlQuery<WebProductGroupDTO>
                (BuildSqlCommand("WebProductGroupSelect", paramCollection), paramCollection.ToArray())
                .ToListAsync();
                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = refLib.ProdGroup
                        });
                    }
                }
                return list;
            }

        }
        public async Task<IList<SelectListItem>> WebGetPlan(string MapInd)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Convert.ToInt64(MapInd) };
                var paramNameList = new[]
                                   {
                                        "@MapInd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);

                var results = await cardtrendentities.Database.SqlQuery<WebGetPlanDTO>
                (BuildSqlCommand("WebGetPlan", paramCollection), paramCollection.ToArray())
                .ToListAsync();
                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = refLib.PlanId.ToString()
                        });
                    }
                }
                return list;
            }

        }
        public async Task<IList<SelectListItem>> WebGetTxnCode(string Module, string CategoryType = null, string ManualEntry = "y")
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Module, CategoryType, ManualEntry};
                var paramNameList = new[]
                                   {
                                        "@Module",
                                        "@Deft",
                                        "@ManualEntry"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);

                var results = await cardtrendentities.Database.SqlQuery<TxnCodeDTO>
                (BuildSqlCommand("WebGetTxnCode", paramCollection), paramCollection.ToArray())
                .ToListAsync();
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "", Value = "" });
                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = refLib.TxnCd.ToString()
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetProduct(string ApplId, bool combineProdCdnPrice = true)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), ApplId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@ProdCd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);

                var results = await cardtrendentities.Database.SqlQuery<GetProductDTO>
                (BuildSqlCommand("WebGetProduct", paramCollection), paramCollection.ToArray())
                .ToListAsync();
                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        if (combineProdCdnPrice)
                        {
                            list.Add(new SelectListItem
                            {
                                Text = refLib.Descp + "-" + (refLib.PricePerUnit.HasValue ? Convert.ToString(CardTrend.Common.Extensions.NumberExtensions.ConverterDecimal(refLib.PricePerUnit.ToString())) : ""),
                                Value = refLib.ProdCd.ToString()
                            });

                        }else
                        {
                            list.Add(new SelectListItem
                            {
                                Text = Convert.ToString(refLib.Descp),
                                Value = Convert.ToString(refLib.ProdCd),
                                Selected = true
                            });
                        }
                    }
                }
                return list;
            }
        }
        public async Task<IssMessageDTO> GetMessageCode(int msgCd)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { msgCd, "EN"};
                var paramNameList = new[]
                                   {
                                        "@MsgCd",
                                        "@LangId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<IssMessageDTO>
                    (BuildSqlCommand("WebGetMsg", paramCollection), paramCollection.ToArray())
                    .FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task<IList<SelectListItem>> WebGetRptType()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo() };
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<RptTypeDTO>
                (BuildSqlCommand("WebGetRptType", paramCollection), paramCollection.ToArray()).ToListAsync();

                var list = new List<SelectListItem>();
                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = refLib.RptId.ToString()
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetNextMilestone(int currentTaskNo, string workFlowCd)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { workFlowCd, currentTaskNo };
                var paramNameList = new[]
                                   {
                                        "@WorkflowCd",
                                        "@CurrentTaskNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);

                var results = await cardtrendentities.Database.SqlQuery<WorkflowTaskDTO>
                (BuildSqlCommand("WebGetNextMilestone", paramCollection), paramCollection.ToArray()).ToListAsync();
                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var mileStone in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = mileStone.Descp,
                            Value = mileStone.TaskNo.ToString()
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetCycleStmt(string cycNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { cycNo };
                var paramNameList = new[]
                                   {
                                        "@CycNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<CycleStmtDTO>
                              (BuildSqlCommand("WebGetCycleStmt", paramCollection), paramCollection.ToArray()).ToListAsync();
                var list = new List<SelectListItem>();
                if (results.Count() > 0)
                {
                    foreach (var cycleStmt in results)
                    {
                        list.Add(new SelectListItem
                        {
                            //cycleStmt.CycStmtId.ToString() + ":" +
                            Text = cycleStmt.StmtDate.ToString("yyyy-MM-dd"),
                            Value =  cycleStmt.StmtDate.ToString("yyyy-MM-dd"),
                            Selected = cycleStmt.StmtDate.ToString() == "1"?true:false
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetTxnCategory()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo()};
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);

                var results = await cardtrendentities.Database.SqlQuery<TransactionCategoryDTO>
                (BuildSqlCommand("WebGetCategory", paramCollection), paramCollection.ToArray())
                .ToListAsync();
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "", Value = "" });
                if (results.Count() > 0)
                {
                    foreach (var transCategory in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = transCategory.Descp,
                            Value = transCategory.Category.ToString()
                        });
                    }
                }
                return list;
            }

        }
        public async Task<IList<SelectListItem>> WebGetEvtRefConf(string evtTypeId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { evtTypeId };
                var paramNameList = new[]
                                   {
                                        "@EvtTypeId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);

                var results = await cardtrendentities.Database.SqlQuery<EvtRefConfDTO>
                (BuildSqlCommand("WebGetEvtRefConf", paramCollection), paramCollection.ToArray())
                .ToListAsync();
                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var transCategory in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = transCategory.Descp,
                            Value = transCategory.ShortDescp
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetEvtType()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var results = await cardtrendentities.Database.SqlQuery<EvtTypeDTO>("WebGetEvtType").ToListAsync();
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "", Value = "" });

                if (results.Count() > 0)
                {
                    foreach (var eventType in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = eventType.Descp,
                            Value = Convert.ToString(eventType.EventTypeId)
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetEvtInd()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var results = await cardtrendentities.Database.SqlQuery<RefLibDTO>("WebGetEvtInd").ToListAsync();
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "", Value = "" });
                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = refLib.RefCd
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetEvtRef(string evtTypeId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { evtTypeId };
                var paramNameList = new[]
                                   {
                                        "@Category"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);

                var results = await cardtrendentities.Database.SqlQuery<EvtRefConfDTO>
                (BuildSqlCommand("WebGetEvtRef", paramCollection), paramCollection.ToArray())
                .ToListAsync();
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "", Value = "" });
                if (results.Count() > 0)
                {
                    foreach (var transCategory in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = transCategory.Descp,
                            Value = transCategory.ShortDescp
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetUserAccess(string userAccess, bool PrependNull = true)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), userAccess };
                var paramNameList = new[]
                                   {
                                       "@IssNo",
                                       "@DropDown"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);

                var results = await cardtrendentities.Database.SqlQuery<WebDropDownInfoDTO>(BuildSqlCommand("WebDropDownInfo", paramCollection), paramCollection.ToArray()).ToListAsync();
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "", Value = "" });
                if (results.Count() > 0)
                {
                    foreach (var transCategory in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = transCategory.Name,
                            Value = transCategory.UserId
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetPymtTxnCd(string TxnCat,string ShortDescp)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
               
                    var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), TxnCat, ShortDescp };
                    var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@TxnCat",
                                        "@settleval"
                                   };
                    var paramCollection = BuildParameterList(parameters, paramNameList);
                    var results = await cardtrendentities.Database.SqlQuery<TxnCodeDTO>(BuildSqlCommand("WebGetPymtTxnCd", paramCollection), paramCollection.ToArray()).ToListAsync();
                    var list = new List<SelectListItem>();
                    if (results.Count() > 0)
                    {
                        foreach (var transCategory in results)
                        {
                            list.Add(new SelectListItem
                            {
                                Text = transCategory.Descp,
                                Value = Convert.ToString(transCategory.TxnCd)
                            });
                        }
                    }

                    return list;
                
            }
        }
        public async Task<IList<SelectListItem>> WebGetTermId(string busnLocation)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), busnLocation };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@Busnlocation"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<WebGetTermDTO>(BuildSqlCommand("WebGetTermId", paramCollection), paramCollection.ToArray()).ToListAsync();
                var list = new List<SelectListItem>();
                if (results.Count() > 0)
                {
                    foreach (var getTermId in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = getTermId.TermId,
                            Value = getTermId.SiteId
                        });
                    }
                }

                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetCostCentre(string refKey, string refTo, bool PrependNull = true)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), refTo, refKey };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@RefTo",
                                        "@RefKey"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<CostCentreDTO>(BuildSqlCommand("WebGetCostCentre", paramCollection), paramCollection.ToArray()).ToListAsync();
                var list = new List<SelectListItem>();
                if(PrependNull)
                {
                    list.Add(new SelectListItem { Text = "", Value = "" });
                }

                if (results.Count() > 0)
                {
                    foreach (var costCentre in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = costCentre.descp,
                            Value = costCentre.costcentre
                        });
                    }
                }

                return list;
            }

        }
        public async Task<IList<SelectListItem>> WebGetSKDS(string applId,string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), applId, acctNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@ApplId",
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<GetSKDS_DTO>(BuildSqlCommand("WebGetSKDS", paramCollection), paramCollection.ToArray()).ToListAsync();
                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var skds in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = skds.Descp,
                            Value = skds.SKDSNo
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetAcctSubsidy(string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(),acctNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<GetSKDS_DTO>(BuildSqlCommand("WebGetAcctSubsidy", paramCollection), paramCollection.ToArray()).ToListAsync();
                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var skds in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = skds.Descp,
                            Value = skds.SKDSNo
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetFeeCd(string feeType, bool PrependNull = true)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(),feeType};
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@FeeType"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<FeeCdDTO>(BuildSqlCommand("WebGetFeeCd", paramCollection), paramCollection.ToArray()).ToListAsync();
                var list = new List<SelectListItem>();
                if (PrependNull)
                {
                    list.Add(new SelectListItem { Text = "", Value = "" });
                }
                if (results.Count() > 0)
                {
                    foreach (var feeCd in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = feeCd.Descp,
                            Value = feeCd.FeeCd
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetCardMedia()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo()};
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<CardMediaDTO>(BuildSqlCommand("WebGetCardMedia", paramCollection), paramCollection.ToArray()).ToListAsync();
                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var media in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = media.descp,
                            Value = Convert.ToString(media.CardMedia)
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetDealerBusnLoc(string acctNo, string cardNo, string state)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), NumberExtensions.ConvertLong(acctNo), NumberExtensions.ConvertLong(cardNo), state };
                var paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@AcctNo",
                                        "@CardNo",
                                        "@State"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<DealerBusnDTO>(BuildSqlCommand("WebGetDealer", paramCollection), paramCollection.ToArray()).ToListAsync();
                var list = new List<SelectListItem>();
                if (results.Count() > 0)
                {
                    foreach (var getDealer in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = getDealer.BusnLocation,
                            Value = getDealer.BusnName
                        });
                    }
                }

                return list;
            }

        }
        public async Task<IList<SelectListItem>> WebGetDealerByMerch(string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(),acctNo };
                var paramNameList = new[]
                                   {
                                        "@AcqNo",
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<DealerBusnDTO>(BuildSqlCommand("WebGetDealerByMerch", paramCollection), paramCollection.ToArray()).ToListAsync();
                var list = new List<SelectListItem>();
                if (results.Count() > 0)
                {
                    foreach (var getDealer in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = getDealer.Descp,
                            Value = getDealer.BusnLocation
                        });
                    }
                }

                return list;
            }

        }
        public async Task<IList<SelectListItem>> WebGetMerchType(string merchType)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { merchType };
                var paramNameList = new[]
                                   {
                                        "@MerchType"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<MerchTypeDTO>(BuildSqlCommand("WebGetMerchType", paramCollection), paramCollection.ToArray()).ToListAsync();
                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var merch in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = merch.Descp,
                            Value = merch.Type
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetCardNo(string acctNo)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] {Common.Helpers.Common.GetIssueNo(), acctNo };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@AcctNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<IacCardDTO>(BuildSqlCommand("WebGetCardNo", paramCollection), paramCollection.ToArray()).ToListAsync();
                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var cardNo in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = cardNo.Descp,
                            Value = cardNo.CardNo
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> WebGetBillItemTxnCategory(string id)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] {id};
                var paramNameList = new[]
                                   {
                                        "@Module"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<BillItemTxnDTO>(BuildSqlCommand("WebGetBillItemTxnCategory", paramCollection), paramCollection.ToArray()).ToListAsync();
                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var billItem in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = billItem.Descp,
                            Value = Convert.ToString(billItem.CategoryId)
                        });
                    }
                }
                return list;
            }
        }
        public async Task<IList<SelectListItem>> iFrameGetTxnCategory(string refName)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { refName };
                var paramNameList = new[]
                                   {
                                        "@RefName"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);

                var results = await cardtrendentities.Database.SqlQuery<FrameTxnCategoryDTO>
                (BuildSqlCommand("iFrameGetTxnCategory", paramCollection), paramCollection.ToArray())
                .ToListAsync();
                var list = new List<SelectListItem>();

                if (results.Count() > 0)
                {
                    foreach (var transCategory in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = transCategory.RefDescp,
                            Value = transCategory.RefCd
                        });
                    }
                    if (list.Count > 0)
                        list.First().Selected = true;
                    return list;
                }
                return list;
            }

        }
        public async Task<IList<SelectListItem>> WebGetDeviceModel()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo() };
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<DeviceDTO>
                (BuildSqlCommand("WebGetDeviceModel", paramCollection), paramCollection.ToArray()).ToListAsync();

                var list = new List<SelectListItem>();
                if (results.Count() > 0)
                {
                    foreach (var refLib in results)
                    {
                        list.Add(new SelectListItem
                        {
                            Text = refLib.Descp,
                            Value = refLib.ProdType.ToString()
                        });
                    }
                }
                return list;
            }
        }
    }
}
