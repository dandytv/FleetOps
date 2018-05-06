using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelSector;
using Utilities.DAL;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CCMS.ModelSector;
using FleetOps.Models;

namespace FleetSys.Models
{
    public class GlobalVariableOps : BaseClass
    {

        #region "Lookup Parameters"

        public async Task<List<LookupParameters>> WebRefListSelect(string refType)
        {
            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[2];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@RefType", String.IsNullOrEmpty(refType) ? (object)DBNull.Value : refType);
                var getObjData = await objEngine.ExecuteCommandAsync("WebRefListSelect", CommandType.StoredProcedure, Parameters);
                var list = new List<LookupParameters>();
                while (getObjData.Read())
                {
                    var item = new LookupParameters();
                    if (refType.ToLower() == "state")
                    {
                        item.Country = Convert.ToString(getObjData["Country"]);
                        item.ParameterCode = Convert.ToString(getObjData["CtryCd"]);
                        item.StateName = Convert.ToString(getObjData["StateName"]);
                        item.StateCode = Convert.ToString(getObjData["StateCd"]);
                    }
                    else if (refType.ToLower() == "city")
                    {
                        item.Country = Convert.ToString(getObjData["Country"]);
                        item.StateName = Convert.ToString(getObjData["State"]);
                        item.ParameterCode = Convert.ToString(getObjData["ParamCd"]);
                        item.CityName = Convert.ToString(getObjData["City"]);
                    }
                    else
                    {
                        item.ParameterDescp = Convert.ToString(getObjData["Descp"]);
                        item.ParameterCode = Convert.ToString(getObjData["ParamCd"]);
                    }
                    list.Add(item);
                }
                return list;
            }
            finally
            {
                objEngine.CloseConnection();
            }
        }
        public async Task<MsgRetriever> WebRefMaint(LookupParameters _variable)
        { //@RefCd  to save address is the selected address type
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[8];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_variable.type) ? new SqlParameter("@RefType", DBNull.Value) : new SqlParameter("@RefType", _variable.type);
                Parameters[2] = new SqlParameter("@Flag", _variable.flag);
                Parameters[3] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[3].Direction = ParameterDirection.ReturnValue;
                if (_variable.type == "State")
                {
                    //@Html.CustomNgSelectListFor(model => model.Country, Model.Countries, new { required = true }, new { @Name = "Countries", @section = "con", ng_disabled = "isUpdate", on_select = "CountryChanged($item, $model);" })
                    //@Html.CustomNgTextBoxFor(model => model.StateCode, new { required = true }, new { @section = "con", @ng_disabled = "editMode" })
                    //@Html.CustomNgTextBoxFor(model => model.StateName, new { required = true }, new { @section = "con" })

                    Parameters[4] = String.IsNullOrEmpty(_variable.StateCode) ? new SqlParameter("@RefCd", DBNull.Value) : new SqlParameter("@RefCd", _variable.StateCode);
                    Parameters[5] = String.IsNullOrEmpty(_variable.Country) ? new SqlParameter("@RefNo", DBNull.Value) : new SqlParameter("@RefNo", _variable.Country);
                    Parameters[6] = String.IsNullOrEmpty(_variable.StateName) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _variable.StateName);
                    Parameters[7] = new SqlParameter("@RefId", DBNull.Value);
                }
                else if (_variable.type == "City")
                {
                    Parameters[4] = String.IsNullOrEmpty(_variable.ParameterCode) ? new SqlParameter("@RefCd", DBNull.Value) : new SqlParameter("@RefCd", _variable.ParameterCode);
                    Parameters[5] = String.IsNullOrEmpty(_variable.Country) ? new SqlParameter("@RefNo", DBNull.Value) : new SqlParameter("@RefNo", _variable.Country);
                    Parameters[6] = String.IsNullOrEmpty(_variable.StateCode) ? new SqlParameter("@RefId", DBNull.Value) : new SqlParameter("@RefId", _variable.StateCode);
                    Parameters[7] = String.IsNullOrEmpty(_variable.CityName) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _variable.CityName);
                    //@Html.CustomNgSelectListFor(model => model.Country, Model.Countries, new { required = true }, new { @Name = "Countries", @section = "con", ng_disabled = "isUpdate", on_select = "CountryChanged($item, $model);" })
                    //@Html.CustomNgSelectListFor(model => model.StateCode, Model.States, new { required = true }, new { @Name = "States", @section = "con", ng_disabled = "isUpdate" })
                    //@Html.CustomNgTextBoxFor(model => model.ParameterCode, new { required = true }, new { @section = "con", @ng_disabled = "editMode" })
                    //@Html.CustomNgTextBoxFor(model => model.CityName, new { required = true }, new { @section = "con" })
                }
                else
                {
                    Parameters[4] = String.IsNullOrEmpty(_variable.ParameterCode) ? new SqlParameter("@RefCd", DBNull.Value) : new SqlParameter("@RefCd", _variable.ParameterCode);
                    Parameters[5] = String.IsNullOrEmpty(_variable.ParameterDescp) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _variable.ParameterDescp);
                    Parameters[6] = new SqlParameter("@RefNo", DBNull.Value);
                    Parameters[7] = new SqlParameter("@RefId", DBNull.Value);
                }
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebRefMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }

        public async Task<LookupParameters> WebRefSelect(string RefType, string RefCd, string RefNo, string RefId)
        {
            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            var _globalVariable = new LookupParameters();
            try
            {
                await objEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[5];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", 1);
                Parameters[1] = String.IsNullOrEmpty(RefNo) ? new SqlParameter("@RefNo", DBNull.Value) : new SqlParameter("@RefNo", RefNo);
                Parameters[2] = String.IsNullOrEmpty(RefId) ? new SqlParameter("@RefId", DBNull.Value) : new SqlParameter("@RefId", RefNo);
                Parameters[3] = String.IsNullOrEmpty(RefType) ? new SqlParameter("@RefType", DBNull.Value) : new SqlParameter("@RefType", RefType);
                Parameters[4] = String.IsNullOrEmpty(RefCd) ? new SqlParameter("@RefCd", DBNull.Value) : new SqlParameter("@RefCd", RefCd);
                var getObjData = await objEngine.ExecuteCommandAsync("WebRefSelect", CommandType.StoredProcedure, Parameters);
                while (getObjData.Read())
                {
                    if (RefType == "State")
                    {
                        _globalVariable.Country = Convert.ToString(getObjData["CtryCd"]);
                        _globalVariable.StateCode = Convert.ToString(getObjData["StateCd"]);
                        _globalVariable.StateName = Convert.ToString(getObjData["Descp"]);
                    }
                    else if (RefType == "City")
                    {
                        _globalVariable.ParameterCode = Convert.ToString(getObjData["ParamCd"]);
                        _globalVariable.Country = Convert.ToString(getObjData["RefNo"]);
                        _globalVariable.StateCode = Convert.ToString(getObjData["RefId"]);
                        _globalVariable.CityName = Convert.ToString(getObjData["Descp"]);

                    }
                    else
                    {
                        _globalVariable.ParameterCode = Convert.ToString(getObjData["ParamCd"]);
                        _globalVariable.ParameterDescp = Convert.ToString(getObjData["Descp"]);
                    }
                }
                return _globalVariable;
            }
            finally
            {
                objEngine.CloseConnection();
            }

        }


        #endregion


        #region "Product Parameters"

        public async Task<IEnumerable<LookupParameters>> WebProdGroupRefListSelect(string ProdGroup)
        {

            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[1];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                var getObjData = await objEngine.ExecuteCommandAsync("WebProdGroupRefListSelect", CommandType.StoredProcedure, Parameters);
                var list = new List<LookupParameters>();
                while (getObjData.Read())
                {
                    var item = new LookupParameters
                    {
                        Descp = Convert.ToString(getObjData["Description"]),
                        SelectedProductGroup = Convert.ToString(getObjData["Product Group"]),
                        LastUpdated = Convert.ToString(getObjData["Update Date"]),
                        UserId = Convert.ToString(getObjData["User Id"]),
                        ProductCode = Convert.ToString(getObjData["Product Code"]),
                        SelectedProductCategory = Convert.ToString(getObjData["Product Category"]),
                        SelectedProductType = Convert.ToString(getObjData["Product Type"]),
                        ProductName = Convert.ToString(getObjData["Product Name"])
                    };
                    list.Add(item);
                }
                return list;
            }
            finally
            {
                objEngine.CloseConnection();
            }
        }

        public async Task<IEnumerable<LookupParameters>> WebProdGroupRefSelect(string ProdGroup)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@ProdGroup", String.IsNullOrEmpty(ProdGroup) ? (object)DBNull.Value : ProdGroup);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebProdGroupRefSelect", CommandType.StoredProcedure, Parameters);
                var _Parameters = new List<LookupParameters>();
                while (execResult.Read())
                {
                    var _Parameter = new LookupParameters();
                    _Parameter.SelectedProductGroup = Convert.ToString(execResult["Product Group"]);
                    _Parameter.Descp = Convert.ToString(execResult["Description"]);
                    _Parameter.ProductCode = Convert.ToString(execResult["Product Code"]);
                    _Parameter.ProductName = Convert.ToString(execResult["Product Name"]);
                    _Parameter.SelectedProductCategory = Convert.ToString(execResult["Product Category"]);
                    _Parameter.SelectedProductType = Convert.ToString(execResult["Product Type"]);
                    _Parameter.UnitPrice = ConverterDecimal(execResult["Unit Price"]);
                    _Parameter.LastUpdated = Convert.ToString(execResult["Updated On"]);
                    _Parameter.UserId = Convert.ToString(execResult["Updated By"]);
                    _Parameters.Add(_Parameter);
                }
                return _Parameters;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }



        public async Task<MsgRetriever> WebProdGroupRefMaint(LookupParameters _LookupParameters)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[6];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_LookupParameters.SelectedProductGroup) ? new SqlParameter("@ProdGroup", DBNull.Value) : new SqlParameter("@ProdGroup", _LookupParameters.SelectedProductGroup);
                Parameters[2] = String.IsNullOrEmpty(_LookupParameters.Descp) ? new SqlParameter("@ProdDescp", DBNull.Value) : new SqlParameter("@ProdDescp", _LookupParameters.Descp);
                Parameters[3] = new SqlParameter("@UserId", GetUserId);
                DataTable dt = new DataTable();
                dt.Columns.Add("ProdCd");
                foreach (var item in _LookupParameters.ProductItems)
                {
                    DataRow dr = dt.NewRow();
                    dr["ProdCd"] = item.ProductCode;
                    dt.Rows.Add(dr);
                }

                Parameters[4] = new SqlParameter("@ProdDetail", dt);
                Parameters[5] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[5].Direction = ParameterDirection.ReturnValue;
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("[WebProdGroupRefMaint]", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }


        public async Task<MsgRetriever> WebProdRefMaint(LookupParameters _LookupParameters)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();


                SqlParameter[] Parameters = new SqlParameter[12];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@UserId", GetUserId);
                Parameters[2] = String.IsNullOrEmpty(_LookupParameters.ProductCode) ? new SqlParameter("@ProdCd", DBNull.Value) : new SqlParameter("@ProdCd", _LookupParameters.ProductCode);
                Parameters[3] = String.IsNullOrEmpty(_LookupParameters.ProdDescp) ? new SqlParameter("@ProdName", DBNull.Value) : new SqlParameter("@ProdName", _LookupParameters.ProdDescp);
                Parameters[4] = String.IsNullOrEmpty(_LookupParameters.SelectedProductCategory) ? new SqlParameter("@ProdCat", DBNull.Value) : new SqlParameter("@ProdCat", _LookupParameters.SelectedProductCategory);
                Parameters[5] = String.IsNullOrEmpty(_LookupParameters.SelectedProductType) ? new SqlParameter("@ProdType", DBNull.Value) : new SqlParameter("@ProdType", _LookupParameters.SelectedProductType);
                Parameters[6] = String.IsNullOrEmpty(_LookupParameters.SelectedBillingPlan) ? new SqlParameter("@BillPlanId", DBNull.Value) : new SqlParameter("@BillPlanId", _LookupParameters.SelectedBillingPlan);
                Parameters[7] = String.IsNullOrEmpty(_LookupParameters.Descp) ? new SqlParameter("@ShortDescp", DBNull.Value) : new SqlParameter("@ShortDescp", _LookupParameters.Descp);
                Parameters[8] = String.IsNullOrEmpty(_LookupParameters.LastUpdated) ? new SqlParameter("@UpdatedOn", DBNull.Value) : new SqlParameter("@UpdatedOn", ConvertDatetimeDB(_LookupParameters.LastUpdated));
                Parameters[9] = String.IsNullOrEmpty(_LookupParameters.flag) ? new SqlParameter("@flag", "U") : new SqlParameter("@flag", _LookupParameters.flag);
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

                    if (!string.IsNullOrEmpty(_LookupParameters.ProductCode))
                    {
                        dr["ProdCd"] = _LookupParameters.ProductCode;
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
                        dr["LastUpdDate"] = ConvertDatetimeDB(item.LastUpdated);
                    }
                    else
                    {
                        dr["LastUpdDate"] = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(item.EffectiveFrom))
                    {
                        dr["EffDate"] = ConvertDatetimeDB(item.EffectiveFrom);
                    }
                    else
                    {
                        dr["EffDate"] = DBNull.Value;
                    }
                    if (!string.IsNullOrEmpty(item.ExpiryDate))
                    {
                        dr["ExpDate"] = ConvertDatetimeDB(item.ExpiryDate);
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
                    dr["UserId"] = GetUserId;
                    dt.Rows.Add(dr);
                }
                Parameters[10] = new SqlParameter("@ProdPriceTbl", dt);
                Parameters[11] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[11].Direction = ParameterDirection.ReturnValue;
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebProdRefMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<IEnumerable<LookupParameters>> WebProdRefSelect(string ProdCd)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@ProdCd", String.IsNullOrEmpty(ProdCd) ? (object)DBNull.Value : ProdCd);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebProdRefSelect", CommandType.StoredProcedure, Parameters);
                var _Parameters = new List<LookupParameters>();
                while (execResult.Read())
                {
                    var _Parameter = new LookupParameters
                    {
                        ProductCode = Convert.ToString(execResult["ProdCd"]),
                        Descp = Convert.ToString(execResult["ShortDescp"]),
                        ProdDescp = Convert.ToString(execResult["ProdDescp"]),
                        SelectedProductCategory = Convert.ToString(execResult["ProdCategory"]),
                        SelectedProductType = Convert.ToString(execResult["ProdType"]),
                        SelectedBillingPlan = Convert.ToString(execResult["BillingPlan"]),
                        UnitPrice = ConverterDecimal(execResult["UnitPrice"]),
                        EffectiveFrom = Convert.ToString(execResult["EffDate"]),
                        ExpiryDate = Convert.ToString(execResult["EffEndDate"]),
                        LastUpdated = Convert.ToString(execResult["UpdateDate"]),
                        UserId = Convert.ToString(execResult["UserId"]),
                        ProdId = Convert.ToString(execResult["ProdId"])
                    };

                    _Parameters.Add(_Parameter);
                }
                return _Parameters;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<List<LookupParameters>> WebProdRefListSelect()
        {
            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[1];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                var getObjData = await objEngine.ExecuteCommandAsync("WebProdRefListSelect", CommandType.StoredProcedure, Parameters);
                var list = new List<LookupParameters>();
                while (getObjData.Read())
                {
                    var item = new LookupParameters
                    {
                        ProductCode = Convert.ToString(getObjData["Product Code"]),
                        ProductName = Convert.ToString(getObjData["Product Name"]),
                        Descp = Convert.ToString(getObjData["Short Description"]),
                        SelectedProductCategory = Convert.ToString(getObjData["Product Category"]),
                        SelectedProductType = Convert.ToString(getObjData["Product Type"]),
                        UnitPrice = ConverterDecimal(getObjData["Unit Price"]),
                        SelectedBillingPlan = Convert.ToString(getObjData["Billing Plan"]),
                        LastUpdated = DateTimeConverter(getObjData["Update Date"])
                    };
                    list.Add(item);
                }
                return list;
            }
            finally
            {
                objEngine.CloseConnection();
            }
        }

        public async Task<List<LookupParameters>> WebUndefinedProdType()
        {
            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[1];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                var getObjData = await objEngine.ExecuteCommandAsync("WebUndefinedProdType", CommandType.StoredProcedure, Parameters);
                var list = new List<LookupParameters>();
                while (getObjData.Read())
                {
                    var item = new LookupParameters
                    {
                        ProductCode = Convert.ToString(getObjData["ProdCd"]),
                        ProductName = Convert.ToString(getObjData["ProdName"]),
                        Descp = Convert.ToString(getObjData["ProdDescp"]),
                        SelectedProductCategory = Convert.ToString(getObjData["ProdCategory"]),
                        SelectedProductType = Convert.ToString(getObjData["ProdType"]),
                        UnitPrice = ConverterDecimal(getObjData["ProdUnitPrice"])

                    };
                    list.Add(item);
                }
                return list;
            }
            finally
            {
                objEngine.CloseConnection();
            }
        }
        #endregion

        #region "Rebate parameters"


        public async Task<List<LookupParameters>> WebRebatePlanListSelect()
        {
            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[1];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                var getObjData = await objEngine.ExecuteCommandAsync("WebRebatePlanListSelect", CommandType.StoredProcedure, Parameters);
                var list = new List<LookupParameters>();
                while (getObjData.Read())
                {
                    var item = new LookupParameters
                    {
                        PlanId = Convert.ToString(getObjData["PlanId"]),
                        Descp = Convert.ToString(getObjData["Descp"]),
                        type = Convert.ToString(getObjData["Type"]),
                        EffectiveFrom = DateConverter(getObjData["Effective Date"]),
                        ExpiryDate = DateConverter(getObjData["Expired Date"]),
                        LastUpdated = Convert.ToString(getObjData["Plans Update Date"]),
                        MinPurchaseAmt = ConverterDecimal(getObjData["MinPurchAmt"]),
                        SubSeqPurchaseAmt = ConverterDecimal(getObjData["SubseqPurchAmt"]),
                        SubSeqBillingAmt = ConverterDecimal(getObjData["SubseqBillingAmt"]),
                        BillingPlanUserId = Convert.ToString(getObjData["BillingPlanUserId"]),
                        BillingPlanLastUpdate = Convert.ToString(getObjData["BillingPlanLastUpdate"]),
                        PlanUserId = Convert.ToString(getObjData["PlanUserId"])
                    };
                    list.Add(item);
                }
                return list;
            }
            finally
            {
                objEngine.CloseConnection();
            }
        }



        public async Task<List<LookupParameters>> WebRebatePlanSelect(string PlanId)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = new SqlParameter("@PlanId", String.IsNullOrEmpty(PlanId) ? (object)DBNull.Value : PlanId);
                var execResult = await objDataEngine.ExecuteCommandAsync("WebRebatePlanSelect", CommandType.StoredProcedure, Parameters);
                var _Parameters = new List<LookupParameters>();
                while (execResult.Read())
                {
                    var _Parameter = new LookupParameters
                    {
                        PlanId = Convert.ToString(execResult["PlanId"]),
                        Descp = Convert.ToString(execResult["Descp"]),
                        EffectiveFrom = DateConverter(execResult["Effective Date"]),
                        ExpiryDate = DateConverter(execResult["Expired Date"]),
                        SelectedType = Convert.ToString(execResult["Type"]),
                        LastUpdated = DateConverter(execResult["Plan_UpdateDate"]),
                        MinPurchaseAmt = ConverterDecimal(execResult["TierValue"]),
                        SubSeqPurchaseAmt = ConverterDecimal(execResult["BasisValue"]),
                        SubSeqBillingAmt = ConverterDecimal(execResult["BillValue"]),
                        BillingPlanLastUpdate = Convert.ToString("PlanDetail_UpdateDate"),
                        UserId = Convert.ToString(execResult["UserId"]),
                    };

                    _Parameters.Add(_Parameter);
                }
                return _Parameters;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        public async Task<MsgRetriever> WebRebatePlanMaint(LookupParameters _LookupParameters)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();


                SqlParameter[] Parameters = new SqlParameter[10];
                SqlCommand cmd = new SqlCommand();
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);
                Parameters[1] = String.IsNullOrEmpty(_LookupParameters.PlanId) ? new SqlParameter("@PlanId", DBNull.Value) : new SqlParameter("@PlanId", _LookupParameters.PlanId);
                Parameters[2] = String.IsNullOrEmpty(_LookupParameters.Descp) ? new SqlParameter("@Descp", DBNull.Value) : new SqlParameter("@Descp", _LookupParameters.Descp);
                Parameters[3] = String.IsNullOrEmpty(_LookupParameters.SelectedType) ? new SqlParameter("@Type", DBNull.Value) : new SqlParameter("@Type", _LookupParameters.SelectedType);
                Parameters[4] = String.IsNullOrEmpty(_LookupParameters.EffectiveFrom) ? new SqlParameter("@EffDate", DBNull.Value) : new SqlParameter("@EffDate", ConvertDatetimeDB(_LookupParameters.EffectiveFrom));
                Parameters[5] = String.IsNullOrEmpty(_LookupParameters.ExpiryDate) ? new SqlParameter("@ExpDate", DBNull.Value) : new SqlParameter("@ExpDate", ConvertDatetimeDB(_LookupParameters.ExpiryDate));
                Parameters[6] = new SqlParameter("@UserId", GetUserId);
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

                    dr["UserId"] = GetUserId;

                    if (!string.IsNullOrEmpty(item.LastUpdated))
                    {
                        dr["LastUpdDate"] = ConvertDatetimeDB(item.LastUpdated);
                    }
                    else
                    {
                        dr["LastUpdDate"] = DBNull.Value;
                    }

                    dt.Rows.Add(dr);
                }
                Parameters[7] = new SqlParameter("@RebatePlanTbl", dt);
                Parameters[8] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[8].Direction = ParameterDirection.ReturnValue;
                Parameters[9] = String.IsNullOrEmpty(_LookupParameters.flag) ? new SqlParameter("@Flag", "I") : new SqlParameter("@Flag", _LookupParameters.flag);
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebRebatePlanMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion
        #region "Event Types"
        public async Task<List<LookupParameters>> WebEventTypeListSelect()
        {
            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objEngine.InitiateConnectionAsync();
                var getObjData = await objEngine.ExecuteCommandAsync("[WebEventTypeListSelect]", CommandType.StoredProcedure);
                var list = new List<LookupParameters>();
                while (getObjData.Read())
                {
                    var item = new LookupParameters
                    {
                        EventTypeId = Convert.ToString(getObjData["ID"]),
                        SelectedEventType = Convert.ToString(getObjData["Type"]),
                        ShortDescp = Convert.ToString(getObjData["Short Description"]),
                        DetailedDescp = Convert.ToString(getObjData["Detailed Description"]),
                        SelectedStatus = Convert.ToString(getObjData["Status"]),
                        LastUpdated = DateConverter(getObjData["Update Date"]),
                        UpdatedBy = Convert.ToString(getObjData["Update By"])
                    };
                    list.Add(item);
                }
                return list;
            }
            finally
            {
                objEngine.CloseConnection();
            }
        }


        public async Task<List<LookupParameters>> WebEventTypeSelect(string EventTypeId)
        {
            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[1];
                Parameters[0] = new SqlParameter("@EventTypeId", EventTypeId);
                var execResult = await objEngine.ExecuteCommandAsync("WebEventTypeSelect", CommandType.StoredProcedure, Parameters);
                var _Parameters = new LookupParameters();
                var list = new List<LookupParameters>();
                while (execResult.Read())
                {
                    _Parameters = new LookupParameters
                    {
                        EventTypeId = Convert.ToString(execResult["EvtTypeID"]),
                        EventPlanId = Convert.ToString(execResult["EvtPlanId"]),
                        ShortDescp = Convert.ToString(execResult["Short Description"]),
                        TypeDesc = Convert.ToString(execResult["Short Description"]),
                        SelectedEventType = Convert.ToString(execResult["Type"]),
                        SelectedPriority = Convert.ToString(execResult["Severity"]),
                        SelectedOwner = Convert.ToString(execResult["Scope"]),
                        SelectedStatus = Convert.ToString(execResult["Status"]),
                        ApplyAllInd = BoolConverter(execResult["ApplyAllInd"]),
                        DetailedDescp = Convert.ToString(execResult["Full Description"]),
                        BitmapAmt = Convert.ToString(execResult["BitmapAmt"]),
                        MaxOccur = Convert.ToString(execResult["Total Occurs"]),
                        SelectedFrequency = Convert.ToString(execResult["Set Frequency Type"]),
                        MinIntVal = Convert.ToString(execResult["MinIntVal"]),
                        MaxIntVal = Convert.ToString(execResult["MaxIntVal"]),
                        EvtPlanDetailId = Convert.ToString(execResult["EvtPlanDetailId"]),
                        MinMoneyVal = ConverterDecimal(execResult["MinMoneyVal"]),
                        MaxMoneyVal = ConverterDecimal(execResult["MaxMoneyVal"]),
                        MinDateVal = DateConverter(execResult["MinDateVal"]),
                        MaxDateVal = DateConverter(execResult["MaxDateVal"]),
                        MinTimeVal = Convert.ToString(execResult["MinTimeVal"]),
                        MaxTimeVal = Convert.ToString(execResult["MaxTimeVal"]),
                        VarCharVal = Convert.ToString(execResult["VarCharVal"]),
                        PeriodType = Convert.ToString(execResult["Period Type"]),
                        PeriodInterval = Convert.ToString(execResult["Period Interval"]),
                        NotifyInd = ConvertInt(execResult["NtfyInd"]),
                        LastUpdated = Convert.ToString(execResult["Update On"]),
                        UpdatedBy = Convert.ToString(execResult["Update by"]),
                        DefaultInd = BoolConverter(execResult["DefaultInd"])
                    };
                    list.Add(_Parameters);
                }
                return list;
            }
            finally
            {

                objEngine.CloseConnection();
            }
        }


        public async Task<List<TmplDisplayer>> WebEventTypeTemplateSelect(string EventTypeId)
        {
            var objEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[1];
                Parameters[0] = new SqlParameter("@EventTypeId", EventTypeId);
                var execResult = await objEngine.ExecuteCommandAsync("WebEventTypeTemplateSelect", CommandType.StoredProcedure, Parameters);
                var _Parameters = new TmplDisplayer();
                var list = new List<TmplDisplayer>();
                while (execResult.Read())
                {
                    _Parameters = new TmplDisplayer
                    {
                        ContentTmplt = Convert.ToString(execResult["Template Displayer"]),
                        Descp = Convert.ToString(execResult["Descp"]),
                        LangInd = Convert.ToString(execResult["Template Language Indicator"]),
                        Type = Convert.ToString(execResult["Type"])
                    };
                    list.Add(_Parameters);
                }
                return list;
            }
            finally
            {
                objEngine.CloseConnection();
            }




        }

        public async Task<MsgRetriever> WebEventTypeMaint(LookupParameters _LookupParameters)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] _Parameter = new SqlParameter[15];
                SqlCommand cmd = new SqlCommand();

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
                    dr["MinIntVal"] = ConvertLongToDb(item.MinIntVal);
                    dr["MaxIntVal"] = ConvertLongToDb(item.MaxIntVal);
                    dr["MinMoneyVal"] = ConvertDecimalToDb(item.MinMoneyVal);
                    dr["MaxMoneyVal"] = ConvertDecimalToDb(item.MaxMoneyVal);
                    dr["MinDateVal"] = ConvertDatetimeDB(item.MinDateVal);
                    dr["MaxDateVal"] = ConvertDatetimeDB(item.MaxDateVal);
                    dr["MinTimeVal"] = string.IsNullOrEmpty(item.MinTimeVal) ? (object)DBNull.Value : item.MinTimeVal;
                    dr["MaxTimeVal"] = string.IsNullOrEmpty(item.MaxTimeVal) ? (object)DBNull.Value : item.MaxTimeVal;
                    dr["VarCharVal"] = string.IsNullOrEmpty(item.VarCharVal) ? (object)DBNull.Value : item.VarCharVal;
                    dr["PeriodType"] = string.IsNullOrEmpty(item.PeriodType) ? (object)DBNull.Value : item.PeriodType;
                    dr["PeriodInterval"] = string.IsNullOrEmpty(item.PeriodInterval) ? (object)DBNull.Value : item.PeriodInterval;
                    dt.Rows.Add(dr);
                }
                _Parameter[0] = new SqlParameter("@EventTypeID", string.IsNullOrEmpty(_LookupParameters.EventTypeId) ? (object)DBNull.Value : _LookupParameters.EventTypeId);
                _Parameter[1] = new SqlParameter("@EventPlanId", string.IsNullOrEmpty(_LookupParameters.EventPlanId) ? (object)DBNull.Value : _LookupParameters.EventPlanId);
                _Parameter[2] = new SqlParameter("@ShortDescp", string.IsNullOrEmpty(_LookupParameters.ShortDescp) ? (object)DBNull.Value : _LookupParameters.ShortDescp);
                _Parameter[3] = new SqlParameter("@Type", string.IsNullOrEmpty(_LookupParameters.SelectedEventType) ? (object)DBNull.Value : _LookupParameters.SelectedEventType);
                _Parameter[4] = new SqlParameter("@Severity", string.IsNullOrEmpty(_LookupParameters.SelectedPriority) ? (object)DBNull.Value : _LookupParameters.SelectedPriority);
                _Parameter[5] = new SqlParameter("@Scope", string.IsNullOrEmpty(_LookupParameters.SelectedOwner) ? (object)DBNull.Value : _LookupParameters.SelectedOwner);
                _Parameter[6] = new SqlParameter("@Sts", string.IsNullOrEmpty(_LookupParameters.SelectedStatus) ? (object)DBNull.Value : _LookupParameters.SelectedStatus);
                _Parameter[7] = new SqlParameter("@Descp", string.IsNullOrEmpty(_LookupParameters.DetailedDescp) ? (object)DBNull.Value : _LookupParameters.DetailedDescp);
                _Parameter[8] = new SqlParameter("@CntEvtOccur", string.IsNullOrEmpty(_LookupParameters.MaxOccur) ? (object)DBNull.Value : _LookupParameters.MaxOccur);
                _Parameter[9] = new SqlParameter("@EvtOccurType", string.IsNullOrEmpty(_LookupParameters.SelectedFrequency) ? (object)DBNull.Value : _LookupParameters.SelectedFrequency);
                _Parameter[10] = new SqlParameter("@UserId", GetUserId);
                _Parameter[11] = new SqlParameter("@ChannelInd", ConvertIntToDb(_LookupParameters.NotifyInd));
                _Parameter[12] = new SqlParameter("@ApplyAllInd", ConvertBoolDB(_LookupParameters.ApplyAllInd));
                _Parameter[13] = new SqlParameter("@NtfyEventTbl", dt);
                _Parameter[14] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                _Parameter[14].Direction = ParameterDirection.ReturnValue;
                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebEventTypeMaint", CommandType.StoredProcedure, _Parameter);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }
        }
        #endregion
    }
}