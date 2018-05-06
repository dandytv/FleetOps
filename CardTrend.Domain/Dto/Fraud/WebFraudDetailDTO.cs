using CardTrend.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.Fraud
{
    public class WebFraudDetailDTO
    {
        public string AcctNo { get; set; }
        public Int64 CardNo { get; set; }
        public string EventId { get; set; }
        public string ReportedBy { get; set; }
        public string ReportVia { get; set; }
        public string NatureOfIncident { get; set; }
        public string OtherNatureOfIncident { get; set; }

        public string ReportChannel { get; set; }
        public string ChannelDescp { get; set; }
        public DateTime? IncidentDate { get; set; }
        public decimal DisputeAmt { get; set; }
        public string IncidentType { get; set; }
        public string IncidentTypeDescp { get; set; }
        public string Descp { get; set; }
        public string InvestigatedBy { get; set; }
        public DateTime? InvestigationDate { get; set; }
        public string InvestigationVenue { get; set; }
        public string CaseBackGround { get; set; }
        public string InvestigationProcess { get; set; }
        public string Findings { get; set; }
        public string ActionTaken { get; set; }
        public string Recommendation { get; set; }
        public string Conclusion { get; set; }
        public string Sts { get; set; }
        public string StsDescp { get; set; }
        public string Remarks { get; set; }
        public string PreparedByName1 { get; set; }
        public string PreparedByPosition1 { get; set; }
        public string PreparedByName2 { get; set; }
        public string PreparedByPosition2 { get; set; }
        public string ReviewerName1 { get; set; }
        public string ReviewerPosition1 { get; set; }
        public string ReviewerName2 { get; set; }
        public string ReviewerPosition2 { get; set; }
        public string EndorsedByName1 { get; set; }
        public string EndorsedByPosition1 { get; set; }
        public string EndorsedByName2 { get; set; }
        public string EndorsedByPosition2 { get; set; }
        public string ApprovedByName1 { get; set; }
        public string ApprovedByPosition1 { get; set; }
        public string ApprovedByName2 { get; set; }
        public string ApprovedByPosition2 { get; set; }
    }
}
