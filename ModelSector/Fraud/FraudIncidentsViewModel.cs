using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ModelSector.Fraud
{
    public class FraudIncidentsViewModel
    {
        //Incident Description
        [DisplayName("Reported By")]
        //  [Required(ErrorMessage = "Please Fill In Reported By")]
        public string SelectedReportedBy { get; set; }
        // public IEnumerable<SelectListItem> ReportedBy { get; set; }

        [DisplayName("Reported Via")]
        [Required(ErrorMessage = "Please Fill In Reported Via")]
        public string SelectedReportedVia { get; set; }
        public string SelectedReportedViaDescp { get; set; }
        public IEnumerable<SelectListItem> ReportedVia { get; set; }

        [DisplayName("Incident Date")]
        public string IncidentDate { get; set; }

        [DisplayName("Disputed Amount")]
        [Required()]
        public string DisputedAmt { get; set; }

        [DisplayName("Nature Of Incident")]
        public string SelectedNatureOfIncident { get; set; }
        public IEnumerable<SelectListItem> NatureOfIncident { get; set; }

        [DisplayName("If Others")]
        public string OtherNatureOfIncident { get; set; }

        public string Description { get; set; }


        //Incident Investigation Team

        [DisplayName("Investigated By")]
        [Required(ErrorMessage = "Please Fill In Investigated By")]
        public string SelectedInvestigatedBy { get; set; }
        public IEnumerable<SelectListItem> InvestigatedBy { get; set; }
        [DisplayName("Investigation Date")]
        public string InvestigationDate { get; set; }
        [DisplayName("Investigation Venue")]
        public string InvestigationVenue { get; set; }

        //Incident Detailed Description
        [DisplayName("Case Background")]
        public string CaseBackground { get; set; }

        [DisplayName("Investigation Process")]
        public string InvestigationProcess { get; set; }

        [DisplayName("Findings")]
        public string Findings { get; set; }

        [DisplayName("Action Taken")]
        public string ActionTaken { get; set; }

        [DisplayName("Recommendation/ Migration Plan")]
        public string MitigationPlan { get; set; }

        [DisplayName("Conclusion")]
        public string Conclusion { get; set; }

        //Incident Status
        [DisplayName("Current Status")]
        public string CurrentStatus { get; set; }

        [DisplayName("Next Status")]
        [Required(ErrorMessage = "Please Fill In Status")]
        public string SelectedNextStatus { get; set; }
        public IEnumerable<SelectListItem> NextStatus { get; set; }

        [DisplayName("Name")]
        public string PreparedByName1 { get; set; }

        [DisplayName("Position")]
        public string PreparedByPosition1 { get; set; }

        [DisplayName("Name")]
        public string PreparedByName2 { get; set; }

        [DisplayName("Position")]
        public string PreparedByPosition2 { get; set; }

        [DisplayName("Name")]
        public string ReviewdByName1 { get; set; }

        [DisplayName("Position")]
        public string ReviewdByPosition1 { get; set; }

        [DisplayName("Name")]
        public string ReviewdByName2 { get; set; }

        [DisplayName("Position")]
        public string ReviewdByPosition2 { get; set; }

        [DisplayName("Name")]
        public string EndorsedByName1 { get; set; }

        [DisplayName("Position")]
        public string EndorsedByPosition1 { get; set; }

        [DisplayName("Name")]
        public string EndorsedByName2 { get; set; }

        [DisplayName("Position")]
        public string EndorsedByPosition2 { get; set; }

        [DisplayName("Name")]
        public string ApprovedByName1 { get; set; }

        [DisplayName("Position")]
        public string ApprovedByPosition1 { get; set; }

        [DisplayName("Name")]
        public string ApprovedByName2 { get; set; }

        [DisplayName("Position")]
        public string ApprovedByPosition2 { get; set; }

        public string Remarks { get; set; }
    }
}
