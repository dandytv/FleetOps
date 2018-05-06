using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using Utilities.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using FleetOps.Models;
using FleetOps.ViewModel;
using ModelSector;
using CCMS.ModelSector;
using System.IO;
using System.Threading.Tasks; 
using FleetSys.Models;
using System.Net;
namespace FleetOps.Models
{
    public class CardAcctSignUpOps : BaseClass
    {
        public string ApplId { get; set; }
        public string EntityId { get; set; }
        public string DocPath { get; set; }

        #region "Payment Approval"
        public async Task<MsgRetriever> SaveMilestonePayment(Milestone _milestone)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[12];
                Parameters[0] = new SqlParameter("@Id", ConvertLongToDb(_milestone.ID));
                Parameters[1] = String.IsNullOrEmpty(_milestone.SelectedTaskNo) ? new SqlParameter("@TaskNo", DBNull.Value) : new SqlParameter("@TaskNo", _milestone.SelectedTaskNo);//new SqlParameter("@TaskNo", ConvertIntToDb(_milestone.SelectedTaskNo));
                Parameters[2] = new SqlParameter("@RefKey", ConvertLongToDb(_milestone.aprId));
                Parameters[3] = String.IsNullOrEmpty(_milestone.RefNo) ? new SqlParameter("@RefNo", DBNull.Value) : new SqlParameter("@RefNo", _milestone.RefNo);
                Parameters[4] = String.IsNullOrEmpty(_milestone.selectedOwner) ? new SqlParameter("@Owner", DBNull.Value) : new SqlParameter("@Owner", _milestone.selectedOwner);
                Parameters[5] = String.IsNullOrEmpty(_milestone.selectedPriority) ? new SqlParameter("@Priority", DBNull.Value) : new SqlParameter("@Priority", _milestone.selectedPriority); //new SqlParameter("@Priority", ConvertIntToDb(_milestone.selectedPriority));
                Parameters[6] = String.IsNullOrEmpty(_milestone.Remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", _milestone.Remarks);
                Parameters[7] = String.IsNullOrEmpty(_milestone.selectedReasonCd) ? new SqlParameter("@ReasonCd", DBNull.Value) : new SqlParameter("@ReasonCd", _milestone.selectedReasonCd);
                Parameters[8] = new SqlParameter("@RecallDate", ConvertDatetimeDB(_milestone.RecallDate));
                Parameters[9] = String.IsNullOrEmpty(_milestone.selectedStatus) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _milestone.selectedStatus);
                Parameters[10] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
                Parameters[11] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[11].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebMilestonePaymentApproval", CommandType.StoredProcedure, Parameters);
                var Result = Convert.ToInt32(Cmd.Parameters["@RETURN_VALUE"].Value);
                var Descp = await GetMessageCode(Result);
                return Descp;
            }
            finally
            {
                objDataEngine.CloseConnection();
                
            }
          
        }
        #region "Merchant Approval"
        public async Task<MsgRetriever> SaveMilestoneMerchant(Milestone _milestone)
        {
            var objDataEngine = new FleetDataEngine(AccessMode.CardHolder, DBType.Maint);
            try
            {
                await objDataEngine.InitiateConnectionAsync();
                SqlParameter[] Parameters = new SqlParameter[12];
                Parameters[0] = new SqlParameter("@Id", ConvertLongToDb(_milestone.ID));
                Parameters[1] = String.IsNullOrEmpty(_milestone.SelectedTaskNo) ? new SqlParameter("@TaskNo", DBNull.Value) : new SqlParameter("@TaskNo", _milestone.SelectedTaskNo);//new SqlParameter("@TaskNo", ConvertIntToDb(_milestone.SelectedTaskNo));
                Parameters[2] = new SqlParameter("@RefKey", ConvertLongToDb(_milestone.aprId));
                Parameters[3] = String.IsNullOrEmpty(_milestone.RefNo) ? new SqlParameter("@RefNo", DBNull.Value) : new SqlParameter("@RefNo", _milestone.RefNo);
                Parameters[4] = String.IsNullOrEmpty(_milestone.selectedOwner) ? new SqlParameter("@Owner", DBNull.Value) : new SqlParameter("@Owner", _milestone.selectedOwner);
                Parameters[5] = String.IsNullOrEmpty(_milestone.selectedPriority) ? new SqlParameter("@Priority", DBNull.Value) : new SqlParameter("@Priority", _milestone.selectedPriority); //new SqlParameter("@Priority", ConvertIntToDb(_milestone.selectedPriority));
                Parameters[6] = String.IsNullOrEmpty(_milestone.Remarks) ? new SqlParameter("@Remarks", DBNull.Value) : new SqlParameter("@Remarks", _milestone.Remarks);
                Parameters[7] = String.IsNullOrEmpty(_milestone.selectedReasonCd) ? new SqlParameter("@ReasonCd", DBNull.Value) : new SqlParameter("@ReasonCd", _milestone.selectedReasonCd);
                Parameters[8] = new SqlParameter("@RecallDate", ConvertDatetimeDB(_milestone.RecallDate));
                Parameters[9] = String.IsNullOrEmpty(_milestone.selectedStatus) ? new SqlParameter("@Sts", DBNull.Value) : new SqlParameter("@Sts", _milestone.selectedStatus);
                Parameters[10] = String.IsNullOrEmpty(this.GetUserId) ? new SqlParameter("@UserId", DBNull.Value) : new SqlParameter("@UserId", this.GetUserId);
                Parameters[11] = new SqlParameter("@RETURN_VALUE", SqlDbType.BigInt);
                Parameters[11].Direction = ParameterDirection.ReturnValue;

                var Cmd = await objDataEngine.ExecuteWithReturnValueAsync("WebMilestoneMerchAdjustmentApproval", CommandType.StoredProcedure, Parameters);
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
        #endregion
    }
}