using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CCMS.ModelSector;
using ModelSector;
using System.Data.SqlClient;
using System.Data;
using Utilities.DAL;
using System.Threading.Tasks;

namespace FleetOps.Models
{
    public class PinMailerOps : BaseClass
    {

        #region "PinMailer"
        public async Task<List<PinMailerBatchList>> GetPinMailerBatchList()
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);

            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[1];
                Parameters[0] = new SqlParameter("@IssNo", GetIssNo);

                var execResult = await objDataEngine.ExecuteCommandAsync("PINMailerGetBatch", CommandType.StoredProcedure, Parameters);
                var _PinMailerBatchList = new List<PinMailerBatchList>();

                while (await execResult.ReadAsync())
                {
                    _PinMailerBatchList.Add(new PinMailerBatchList
                    {
                        BatchID = ConvertToInt(execResult["BatchId"]),
                        CreationDate = DateTimeConverter(execResult["CreationDate"]),
                        Sts = Convert.ToString(execResult["Sts"]),
                        Count = ConvertToInt(execResult["Count"])
                    });

                };
                return _PinMailerBatchList;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

        }

        public async Task<List<PinMailerBatchView>> GetPinMailerBatchView(long batchID, int status)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[2];
                Parameters[0] = new SqlParameter("@BatchId", batchID);
                Parameters[1] = new SqlParameter("@Ind", status);

                var execResult = await objDataEngine.ExecuteCommandAsync("WebPINMailerListSelect", CommandType.StoredProcedure, Parameters);
                var _PinMailerBatchView = new List<PinMailerBatchView>();

                while (await execResult.ReadAsync())
                {
                    _PinMailerBatchView.Add(new PinMailerBatchView
                    {
                        //SeqNo = ConvertToInt(execResult["SeqNo"]),
                        CardNo = Convert.ToString(execResult["CardNo"]),
                        StsDescp = Convert.ToString(execResult["StsDescp"]),
                        CardCreationDate = DateTimeConverter(execResult["CardCreationDate"]),
                        CompName = Convert.ToString(execResult["CompName"]),
                        DriverName = Convert.ToString(execResult["DriverName"]),
                        PIC = Convert.ToString(execResult["PIC"]),
                        Address = Convert.ToString(execResult["Address"]),
                    });

                };

                return _PinMailerBatchView;
            }
            finally
            {
                objDataEngine.CloseConnection();
            }

        }

        public async Task<MsgRetriever> PinMailerPrint(int batchID, List<long> cardList)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CustomerService, DBType.Maint);


            try
            {
                objDataEngine.InitiateConnection();
                SqlParameter[] Parameters = new SqlParameter[3];
                Parameters[0] = new SqlParameter("@BatchId", batchID);

                DataTable dt = new DataTable();

                dt.Columns.Add("BatchId");
                dt.Columns.Add("CardNo", typeof(long));
                dt.Columns.Add("Sts");

                for (int i = 0; i < cardList.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["BatchId"] = batchID;
                    dr["CardNo"] = cardList[i];
                    dr["Sts"] = DBNull.Value;
                    dt.Rows.Add(dr);
                }

                Parameters[1] = new SqlParameter("@PINMailer", dt);
                Parameters[1].SqlDbType = SqlDbType.Structured;
                Parameters[2] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[2].Direction = ParameterDirection.ReturnValue;

                //try
                //{
                var execResult = await objDataEngine.ExecuteWithReturnValueAsync("WebPINMailerMaint", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(execResult.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;

            }
           finally
            {
                objDataEngine.CloseConnection();
            }
          
            //}
            //catch (Exception ex)
            //{
            //    objDataEngine.CloseConnection();
            //    return null;
            //}

        }
        #endregion
    }
}

