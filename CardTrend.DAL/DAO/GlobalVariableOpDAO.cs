using CardTrend.Common.Extensions;
using CardTrend.DAL.Contexts;
using CardTrend.Domain.Dto;
using CardTrend.Domain.Dto.GlobalVariables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.DAO
{
    public interface IGlobalVariableOpDAO
    {
        Task<IList<LookupParameterDTO>> WebUndefinedProdType();
        Task<List<EventTypeDTO>> WebEventTypeSelect(string eventTypeId);
        Task<IList<ProdGroupRefDTO>> WebProdGroupRefs();
        Task<IList<LookupParameterDTO>> WebRefListSelect(string refType);
        Task<LookupParameterDTO> WebRefDetail(string refType, string refCd, string refNo, string refId);
        Task<IList<ProdRefDTO>> WebProdRefListSelect();
        Task<List<RebatePlanDTO>> WebRebatePlanSelect(string planId);
        Task<IList<RebatePlanDetailDTO>> WebRebatePlanListSelect();
        Task<List<EventTypeDTO>> WebEventTypeListSelect();
        Task<List<TmplDisplayDTO>> GetEventTypeTemplates(Int64 eventTypeId);
        Task<IList<ProdRefDTO>> WebProdRefSelect(string prodCd);
        Task<IList<LookupParameterDTO>> WebProdGroupRefSelect(string prodGroup);
        Task<int> WebRefMaint(string refType, string refCd, string refNo, string refId, string descp, string flag);
        Task<int> WebProdGroupRefMaint(LookupParameterDTO _LookupParameters, string userId);
        Task<int> WebProdRefMaint(ProdRefDTO _LookupParameters, string userId);
        Task<int> WebEventTypeMaint(EventTypeDTO _LookupParameters, string userId);
        Task<int> WebRebatePlanMaint(RebatePlanDTO _LookupParameters, string userId);
    }
    public class GlobalVariableOpDAO : DAOBase, IGlobalVariableOpDAO
    {
        private readonly string _connectionString = string.Empty;

        public GlobalVariableOpDAO(string connString)
        {
            _connectionString = connString;
        }

        public async Task<IList<LookupParameterDTO>> WebUndefinedProdType()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo()};
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<LookupParameterDTO>
                    (BuildSqlCommand("WebUndefinedProdType", paramCollection), paramCollection.ToArray())
                    .ToListAsync();

                return results;
            }
        }
        public async Task<List<EventTypeDTO>> WebEventTypeSelect(string eventTypeId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { eventTypeId };
                var paramNameList = new[]
                                   {
                                        "@EventTypeId"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<EventTypeDTO>
                              (BuildSqlCommand("WebEventTypeSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<IList<ProdGroupRefDTO>> WebProdGroupRefs()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo()};
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<ProdGroupRefDTO>
                              (BuildSqlCommand("WebProdGroupRefListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();

                return results;
            }
        }
        public async Task<IList<LookupParameterDTO>> WebRefListSelect(string refType)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), refType };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@RefType"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<LookupParameterDTO>
                              (BuildSqlCommand("WebRefListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<LookupParameterDTO> WebRefDetail(string refType, string refCd, string refNo, string refId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), refType, refCd, refNo ,refId};
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@RefType",
                                        "@RefCd",
                                        "@RefNo",
                                        "@RefId"
                                   };

                var paramCollection = BuildParameterList(parameters, paramNameList);
                var result = await cardtrendentities.Database.SqlQuery<LookupParameterDTO>
                              (BuildSqlCommand("WebRefSelect", paramCollection), paramCollection.ToArray()).FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task<IList<ProdRefDTO>> WebProdRefListSelect()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo()};
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<ProdRefDTO>
                              (BuildSqlCommand("WebProdRefListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<List<RebatePlanDTO>> WebRebatePlanSelect(string planId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), planId };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@PlanId"
                                   };

                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<RebatePlanDTO>
                                    (BuildSqlCommand("WebRebatePlanSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<List<TmplDisplayDTO>> GetEventTypeTemplates(Int64 eventTypeId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { eventTypeId };
                var paramNameList = new[]
                                   {
                                        "@EventTypeId"
                                   };

                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<TmplDisplayDTO>
                                    (BuildSqlCommand("WebEventTypeTemplateSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<IList<RebatePlanDetailDTO>> WebRebatePlanListSelect()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo()};
                var paramNameList = new[]
                                   {
                                        "@IssNo"
                                   };

                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<RebatePlanDetailDTO>
                                    (BuildSqlCommand("WebRebatePlanListSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<List<EventTypeDTO>> WebEventTypeListSelect()
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var results = await cardtrendentities.Database.SqlQuery<EventTypeDTO>("WebEventTypeListSelect").ToListAsync();
                return results;
            }
        }
        public async Task<int> WebRefMaint(string refType, string refCd, string refNo, string refId, string descp, string flag)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                IssMessageDTO issMessage = new IssMessageDTO();
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(),refType,refCd,refNo,refId,descp,flag };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@RefType",
                                        "@RefCd",
                                        "@RefNo",
                                        "@RefId",
                                        "@Descp",
                                        "@Flag"
                                   };
                var paramCollection = BuildParameterListWithRrn(parameters, paramNameList);
                var result = await cardtrendentities.Database.ExecuteSqlCommandAsync
                             (BuildSqlCommandWithRrn("WebRefMaint", paramCollection), paramCollection.ToArray());
                var resultCode = paramCollection.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<IList<ProdRefDTO>> WebProdRefSelect(string prodCd)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), prodCd };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@ProdCd"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<ProdRefDTO>
                             (BuildSqlCommand("WebProdRefSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<IList<LookupParameterDTO>> WebProdGroupRefSelect(string prodGroup)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                var parameters = new object[] { Common.Helpers.Common.GetIssueNo(), prodGroup };
                var paramNameList = new[]
                                   {
                                        "@IssNo",
                                        "@ProdGroup"
                                   };
                var paramCollection = BuildParameterList(parameters, paramNameList);
                var results = await cardtrendentities.Database.SqlQuery<LookupParameterDTO>
                             (BuildSqlCommand("WebProdGroupRefSelect", paramCollection), paramCollection.ToArray()).ToListAsync();
                return results;
            }
        }
        public async Task<int> WebProdGroupRefMaint(LookupParameterDTO _LookupParameters, string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ProdCd");
                foreach (var item in _LookupParameters.ProductItems)
                {
                    DataRow dr = dt.NewRow();
                    dr["ProdCd"] = item.ProductCode;
                    dt.Rows.Add(dr);
                }

                var parameters = new[] { 
                    new SqlParameter("@IssNo", SqlDbType.SmallInt) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                    new SqlParameter("@ProdGroup", SqlDbType.VarChar) {SqlValue = (object)_LookupParameters.ProductGroup?? DBNull.Value},
                    new SqlParameter("@ProdDescp", SqlDbType.VarChar) {SqlValue = (object)_LookupParameters.Descp?? DBNull.Value},
                    new SqlParameter("@UserId",SqlDbType.NVarChar) {SqlValue = userId},
                    new SqlParameter("@ProdDetail", SqlDbType.Structured) {SqlValue = dt,TypeName ="ProdCdData"},                  
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };

                await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebProdGroupRefMaint @IssNo,@ProdGroup,@ProdDescp,@UserId,@ProdDetail", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> WebProdRefMaint(ProdRefDTO _LookupParameters,string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ProdId");
                dt.Columns.Add("ProdCd");
                dt.Columns.Add("ProdPrice", typeof(decimal));
                dt.Columns.Add("EffDate");
                dt.Columns.Add("ExpDate");
                dt.Columns.Add("UserId");
                dt.Columns.Add("LastUpdDate");
                foreach (var item in _LookupParameters.ProductItems)
                {
                    DataRow dr = dt.NewRow();

                    if (!string.IsNullOrEmpty(_LookupParameters.ProdCd))
                    {
                        dr["ProdCd"] = _LookupParameters.ProdCd;
                    }
                    else
                    {
                        dr["ProdCd"] = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(item.ProdId))
                    {
                        dr["ProdId"] = item.ProdId;
                    }
                    else
                    {
                        dr["ProdId"] = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(item.LastUpdated))
                    {
                        dr["LastUpdDate"] = NumberExtensions.ConvertDatetimeDB(item.LastUpdated);
                    }
                    else
                    {
                        dr["LastUpdDate"] = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(item.EffectiveFrom))
                    {
                        dr["EffDate"] = NumberExtensions.ConvertDatetimeDB(item.EffectiveFrom);
                    }
                    else
                    {
                        dr["EffDate"] = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(item.ExpiryDate))
                    {
                        dr["ExpDate"] = NumberExtensions.ConvertDatetimeDB(item.ExpiryDate);
                    }
                    else
                    {
                        dr["ExpDate"] = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(item.UnitPrice))
                    {
                        dr["ProdPrice"] = item.UnitPrice;
                    }
                    else
                    {
                        dr["ProdPrice"] = DBNull.Value;
                    }
                    dr["UserId"] = userId;
                    dt.Rows.Add(dr);
                }

                var parameters = new[] { 
                    new SqlParameter("@IssNo", SqlDbType.SmallInt) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                    new SqlParameter("@ProdCd", SqlDbType.VarChar) {SqlValue = (object)_LookupParameters.ProdCd?? DBNull.Value},
                    new SqlParameter("@ProdName", SqlDbType.VarChar) {SqlValue = (object)_LookupParameters.ProdDescp?? DBNull.Value},
                    new SqlParameter("@ProdCat",SqlDbType.VarChar) {SqlValue = (object)_LookupParameters.ProductCategory?? DBNull.Value},
                    new SqlParameter("@ProdType", SqlDbType.VarChar) {SqlValue = (object)_LookupParameters.ProductType?? DBNull.Value},    
                    new SqlParameter("@BillPlanId",SqlDbType.Int) {SqlValue = (object)_LookupParameters.BillingPlan?? DBNull.Value},
                    new SqlParameter("@ShortDescp",SqlDbType.NVarChar) {SqlValue = (object)_LookupParameters.ShortDescription?? DBNull.Value},
                    new SqlParameter("@UserId",SqlDbType.NVarChar) {SqlValue = userId},
                    new SqlParameter("@UpdatedOn",SqlDbType.DateTime) {SqlValue = (object)NumberExtensions.ConvertDatetimeDB(_LookupParameters.UpdatedOn)?? DBNull.Value},
                    new SqlParameter("@Flag",SqlDbType.Char) {SqlValue =  (object)_LookupParameters.Flag ?? "U"},
                    new SqlParameter("@ProdPriceTbl",SqlDbType.Structured) {SqlValue = dt,TypeName ="ProdPriceData"},
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };

                await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebProdRefMaint @IssNo,@ProdCd,@ProdName,@ProdCat,@ProdType,@BillPlanId,@ShortDescp,@UserId,@UpdatedOn,@Flag,@ProdPriceTbl", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> WebEventTypeMaint(EventTypeDTO _LookupParameters,string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("EvtNtfyDetailId");
                dt.Columns.Add("MinIntVal");
                dt.Columns.Add("MaxIntVal");
                dt.Columns.Add("MinMoneyVal");
                dt.Columns.Add("MaxMoneyVal");
                dt.Columns.Add("MinDateVal");
                dt.Columns.Add("MaxDateVal");
                dt.Columns.Add("MinTimeVal");
                dt.Columns.Add("MaxTimeVal");
                dt.Columns.Add("VarCharVal");
                dt.Columns.Add("PeriodType");
                dt.Columns.Add("PeriodInterval");
                foreach (var item in _LookupParameters.ProductItems)
                {
                    DataRow dr = dt.NewRow();
                    dr["EvtNtfyDetailId"] = string.IsNullOrEmpty(item.EvtPlanDetailId) ? (object)DBNull.Value : item.EvtPlanDetailId;
                    dr["MinIntVal"] = NumberExtensions.ConvertLongToDb(item.MinIntVal);
                    dr["MaxIntVal"] = NumberExtensions.ConvertLongToDb(item.MaxIntVal);
                    dr["MinMoneyVal"] = NumberExtensions.ConvertDecimalToDb(item.MinMoneyVal);
                    dr["MaxMoneyVal"] = NumberExtensions.ConvertDecimalToDb(item.MaxMoneyVal);
                    dr["MinDateVal"] = NumberExtensions.ConvertDatetimeDB(item.MinDateVal);
                    dr["MaxDateVal"] = NumberExtensions.ConvertDatetimeDB(item.MaxDateVal);
                    dr["MinTimeVal"] = string.IsNullOrEmpty(item.MinTimeVal) ? (object)DBNull.Value : item.MinTimeVal;
                    dr["MaxTimeVal"] = string.IsNullOrEmpty(item.MaxTimeVal) ? (object)DBNull.Value : item.MaxTimeVal;
                    dr["VarCharVal"] = string.IsNullOrEmpty(item.VarCharVal) ? (object)DBNull.Value : item.VarCharVal;
                    dr["PeriodType"] = string.IsNullOrEmpty(item.PeriodType) ? (object)DBNull.Value : item.PeriodType;
                    dr["PeriodInterval"] = string.IsNullOrEmpty(item.PeriodInterval) ? (object)DBNull.Value : item.PeriodInterval;
                    dt.Rows.Add(dr);
                }

                var parameters = new[] { 
                    new SqlParameter("@EventTypeID", SqlDbType.BigInt) {SqlValue = (object)_LookupParameters.EvtTypeID?? DBNull.Value}, 
                    new SqlParameter("@EventPlanId", SqlDbType.BigInt) {SqlValue = (object)_LookupParameters.EvtPlanId?? DBNull.Value},
                    new SqlParameter("@ShortDescp", SqlDbType.VarChar) {SqlValue = (object)_LookupParameters.ShortDescription?? DBNull.Value},
                    new SqlParameter("@Type",SqlDbType.VarChar) {SqlValue = (object)_LookupParameters.Type?? DBNull.Value},
                    new SqlParameter("@Severity", SqlDbType.VarChar) {SqlValue = (object)_LookupParameters.Severity?? DBNull.Value},    
                    new SqlParameter("@Scope",SqlDbType.VarChar) {SqlValue = (object)_LookupParameters.Scope?? DBNull.Value},
                    new SqlParameter("@Sts",SqlDbType.NVarChar) {SqlValue = (object)_LookupParameters.Status?? DBNull.Value},
                    new SqlParameter("@Descp",SqlDbType.NVarChar) {SqlValue = (object)_LookupParameters.FullDescription?? DBNull.Value},
                    new SqlParameter("@CntEvtOccur",SqlDbType.Int) {SqlValue = (object)_LookupParameters.TotalOccurs?? DBNull.Value},
                    new SqlParameter("@EvtOccurType",SqlDbType.NVarChar) {SqlValue = (object)_LookupParameters.SetFrequencyType?? DBNull.Value},
                    new SqlParameter("@UserId",SqlDbType.NVarChar) {SqlValue = userId},
                    new SqlParameter("@ChannelInd",SqlDbType.BigInt) {SqlValue = (object)_LookupParameters.NtfyInd?? DBNull.Value},
                    new SqlParameter("@ApplyAllInd",SqlDbType.VarChar) {SqlValue =  (object)_LookupParameters.ApplyAllInd},
                    new SqlParameter("@NtfyEventTbl",SqlDbType.Structured) {SqlValue = dt,TypeName ="NtfyEventTbl"},
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };
                await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebEventTypeMaint @EventTypeID,@EventPlanId,@ShortDescp,@Type,@Severity,@Scope,@Sts,@Descp,@CntEvtOccur,@EvtOccurType,@UserId,@ChannelInd,@ApplyAllInd,@NtfyEventTbl", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
        public async Task<int> WebRebatePlanMaint(RebatePlanDTO _LookupParameters,string userId)
        {
            using (var cardtrendentities = new pdb_ccmsContext(_connectionString))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PlanId");
                dt.Columns.Add("TierValue", typeof(decimal));
                dt.Columns.Add("BasisValue", typeof(decimal));
                dt.Columns.Add("BilledValue", typeof(decimal));
                dt.Columns.Add("UserId");
                dt.Columns.Add("LastUpdDate");
                foreach (var item in _LookupParameters.ProductItems)
                {
                    DataRow dr = dt.NewRow();

                    dr["PlanId"] = _LookupParameters.PlanId;

                    if (!string.IsNullOrEmpty(item.MinPurchaseAmt))
                    {
                        dr["TierValue"] = item.MinPurchaseAmt;
                    }
                    else
                    {
                        dr["TierValue"] = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(item.SubSeqPurchaseAmt))
                    {
                        dr["BasisValue"] = item.SubSeqPurchaseAmt;
                    }
                    else
                    {
                        dr["BasisValue"] = DBNull.Value;
                    }

                    if (!string.IsNullOrEmpty(item.SubSeqBillingAmt))
                    {
                        dr["BilledValue"] = item.SubSeqBillingAmt;
                    }
                    else
                    {
                        dr["BilledValue"] = DBNull.Value;
                    }

                    dr["UserId"] = userId;

                    if (!string.IsNullOrEmpty(item.LastUpdated))
                    {
                        dr["LastUpdDate"] = NumberExtensions.ConvertDatetimeDB(item.LastUpdated);
                    }
                    else
                    {
                        dr["LastUpdDate"] = DBNull.Value;
                    }
                    dt.Rows.Add(dr);
                }

                var parameters = new[] { 
                    new SqlParameter("@IssNo", SqlDbType.BigInt) {SqlValue = Common.Helpers.Common.GetIssueNo()}, 
                    new SqlParameter("@PlanId", SqlDbType.Int) {SqlValue = (object)_LookupParameters.PlanId?? DBNull.Value},
                    new SqlParameter("@Descp", SqlDbType.VarChar) {SqlValue = (object)_LookupParameters.Descp?? DBNull.Value},
                    new SqlParameter("@Type",SqlDbType.VarChar) {SqlValue = (object)_LookupParameters.Type?? DBNull.Value},
                    new SqlParameter("@EffDate", SqlDbType.DateTime) {SqlValue = (object)_LookupParameters.EffectiveDate?? DBNull.Value},    

                    new SqlParameter("@ExpDate",SqlDbType.DateTime) {SqlValue = (object)_LookupParameters.ExpiredDate?? DBNull.Value},
                    new SqlParameter("@UserId",SqlDbType.NVarChar) {SqlValue = userId},
                    new SqlParameter("@RebatePlanTbl",SqlDbType.Structured) {SqlValue = dt,TypeName ="RebatePlan"},
                    new SqlParameter("@RETURN_VALUE",SqlDbType.BigInt){Direction = ParameterDirection.Output}
                    };

                await cardtrendentities.Database.ExecuteSqlCommandAsync("exec @RETURN_VALUE = WebRebatePlanMaint @IssNo,@PlanId,@Descp,@Type,@EffDate,@ExpDate,@UserId,@RebatePlanTbl", parameters);
                var resultCode = parameters.Where(x => x.ParameterName == "@RETURN_VALUE").FirstOrDefault().Value;
                return Convert.ToInt32(resultCode);
            }
        }
    }
}
