using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CCMS.ModelSector
{

    //public class FraudCard
    //{
    //    public Int64 CardNo { get; set; }
    //    public Int64 EventId { get; set; }
    //}

    //public class Fraud_Details
    //{
    //    public int Event_DetailId { get; set; }
    //    public int EventId { get; set; }
    //    public string Seq { get; set; }
    //    public DateTime CreationDate { get; set; }
    //    public string CreatedBy { get; set; }
    //    public string ReportedBy { get; set; }
    //    public string ReportChannel { get; set; }
    //    public DateTime IncidentDate { get; set; }
    //    public string IncidentType { get; set; }
    //    public string DisputeAmt { get; set; }
    //    public string Desc { get; set; }
    //    public string InvestigatedBy { get; set; }
    //    public DateTime InvestigationDate { get; set; }
    //    public string InvestigationVenue { get; set; }
    //    public string CaseBackGroud { get; set; }
    //    public string InvestigationProcess { get; set; }
    //    public string Findings { get; set; }
    //    public string ActionTaken { get; set; }
    //    public string Recommendation { get; set; }
    //    public string Conclusion { get; set; }
    //    public DateTime RecallDate { get; set; }
    //}

    public class FraudCards
    {
        [DisplayName("Card No")]
        public string SelectedCardNo { get; set; }
        public List<string> CardNo { get; set; }
        public string AcctNo { get; set; }
        public string EventId { get; set; }
    }
}
