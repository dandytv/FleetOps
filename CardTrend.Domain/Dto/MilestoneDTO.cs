using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CardTrend.Common;

namespace CardTrend.Domain.Dto
{
    public class MilestoneDTO
    {
        public Int64 Id { get; set; }
        public int Ind { get; set; }
        public string BatchId { get; set; }
       // public string EventId { get; set; }
       // public string ReqType { get; set; }
        //public string ReqValue { get; set; }
        //public DateTime ReqDate { get; set; }
        //public string ReqBy { get; set; }

        public string UserId { get; set; }
        public int TaskNo { get; set; }
        public string TaskDescp { get; set; }
        public string StsDescp { get; set; }

        public string Descp { get; set; }
        public string ReasonCd { get; set; }
        public string Remarks { get; set; }
        public string CmpyName1 { get; set; }
        public string RecallDate { get; set; }
        public string WorkflowCd { get; set; }
        public string RefNo { get; set; }
        public Int64 RefKey { get; set; }
        public string Priority { get; set; }

        public string RequestType { get; set; }
        public Int64 CardNo { get; set; }
        public Int64 AcctNo { get; set; }
        public string RefCd { get; set; }
        public string AreaCode { get; set; }
        public long ChequeNo { get; set; }
        public decimal ChequeAmt { get; set; }
        public DateTime StmtDate { get; set; }
        public string ReqVal { get; set; }
        public string CmpyName { get; set; }
        public string RequestBy { get; set; }
        public string Sts { get; set; }
        public string ActionSP { get; set; }
        public string SelectedOwner { get; set; }
        public string Owner { get; set; }
        public Int64 PrevId { get; set; }
        public Int64 aprId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Url { get; set; }
        public string ValidationSP { get; set; }
        public Int16 SLADay { get; set; }
        // MilestoneApplValidation
        public string AssessmentType { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal SecurityAmt { get; set; }
        public Nullable<DateTime> LastUpdDate { get; set; }
    }
}
