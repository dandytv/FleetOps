using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCMS.ModelSector;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModelSector.Global_Resources;

namespace ModelSector
{
   public class DebtsCollection
    {


        [DisplayName("Task Id")]
        public string TaskId { get; set; }
        [DisplayName("Dunning Level")]
        public string DunningLvl { get; set; }
        [DisplayName("Dunning Date")]
        public string DunningDate { get; set; }
        [DisplayName("DueDate")]
        public string DueDate { get; set; }
  

        [DisplayName("Reminder Date")]
        public string ReminderDate { get; set; }
        [DisplayName("Aging No")]
        public string agingNo { get; set; }
        [DisplayName("Collector")]
        public string collector { get; set; }
        [DisplayName("Transfers To Collector")]
        public string TransferToCollector { get; set; }
        [DisplayName("Transferred Date")]
        public string TransferDate { get; set; }
        [DisplayName("Terminated Date")]
        public string TerminatedDate { get; set; }
        [DisplayName ( "Reason Code")]
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCd { get; set; }
        [DisplayName ("Remarks")]
        public string Remarks { get; set; }
        [DisplayName("Calendar Indicator")]//radio button
        public bool SelectedCalendarInd { get; set; }

        [DisplayName("Calendar Receiver Id")]
        public string CalendarReceiverId { get; set; }
        public DebtCollectionTrackingLst _debtCollectionTrackLst { get; set; }


       [DisplayName("Assign Date")]
       public string AssignDate { get; set; }

       [DisplayName ( "Aging No")]
       public string AgingNo { get; set; }

       [DisplayName ( "Collector Id")]
       public string CollectorID { get; set; }

       [DisplayName("Creation Date")]
       public string CreationDate { get; set; }

       [DisplayName("Current Status")]
       public string SelectedCurrentStatus { get; set; }
       public IEnumerable<SelectListItem> CurrentStatus { get; set; }

       [DisplayName("Change to Status")]
       public string SelectedChangetoStatus { get; set; }
       public IEnumerable<SelectListItem> ChangeToStatus { get; set; }

       [DisplayName("Description")]
       public string Descp { get; set; }

       [DisplayName ( "Fee Code") ]
       public string  SelectedFeeCd { get; set; }
       public IEnumerable<SelectListItem> FeeCd { get; set; }

       [DisplayName ( "Expiry Date")]
       public string ExpiryDate { get; set; }


       [DisplayName ( "New Card Expiry Date")]
       public string NewCardExpiryDate { get; set; }

       [DisplayName ( "Reassigned Collector Id")]
       public string ReassignedCollectorID { get; set; }

       [DisplayName ( "Reassigned Date")]
       public string ReassignedDate { get; set; }



       [DisplayName("Task No")]
       public string TaskNo { get; set; }

       [DisplayName ( "User Id")]
       public string UserId { get; set; }

       [DisplayName("xReference Id")]
       public string XReferenceID { get; set; }

       [DisplayName("xReference No")]
       public string XReferenceNo { get; set; }
     


    }
   public class DebtCollectionTrackingLst
   {
       [DisplayName("Action Id")]
       public string ActionId { get; set; }
       [DisplayName("Action Date")]
       public string ActionDate { get; set; }
       [DisplayName("Action Cd")]
       public string ActionCd { get; set; }
       [DisplayName("Reminder Date")]
       public string ReminderDate { get; set; }
       [DisplayName("Reason Code")]
       public string SelectedReasonCode { get; set; }
       public IEnumerable<SelectListItem> ReasonCd { get; set; }
       [DisplayName("Remarks")]
       public string Remarks { get; set; }
       [DisplayName("Xreference No")]
       public string XrefNo { get; set; }
       [DisplayName("User ID")]
       public string UserId { get; set; }
       [DisplayName("Status")]
       public string Selectedstatus { get; set; }
       public IEnumerable<SelectListItem> Status { get; set; }
       [DisplayName("Calendar Indicator")]//radio button
       public bool SelectedCalendarInd { get; set; }
 
       [DisplayName("Calendar Receiver Id")]
       public string CalendarReceiverId { get; set; }
       public DebtCollectionTrackingLst _debtCollectionTrackLst { get; set; }
   }
}
