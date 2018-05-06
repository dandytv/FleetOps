using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ModelSector;
using CCMS.ModelSector;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModelSector.Helpers;

namespace ModelSector
{
   public class EventLogger
    {
       [DisplayNameLocalizedAttribute("CardtrendAccount", "EventIdLbl")]
        public string EventId { get; set; }
        
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedEventTypeDdl")]
        public string SelectedEventType { get; set; }
        public IEnumerable<SelectListItem> EventType { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "RefKeyLbl")]
        public string RefKey { get; set; }
       
        [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedEventStsDdl")]
        public string SelectedEventSts { get; set; }
        public IEnumerable<SelectListItem> EventSts { get; set; }

       [DisplayNameLocalizedAttribute("CardtrendAccount", "DescriptionLbl")]
        public string Description { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedReasonCodeDdl")]
        public string SelectedReasonCode { get; set; }
        public IEnumerable<SelectListItem> ReasonCd { get; set; }
        public string EffDateFrom { get; set; }
        public string EffDateTo { get; set; }

        [DisplayNameLocalizedAttribute("CardtrendAccount", "UserIdLbl")]
        public string UserId { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "CreationDateLbl")]
        public string CreationDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "EventDateLbl")]
        public string EventDate { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "ReminderDatetimeLbl")]
        public string ReminderDatetime { get; set; }
        [DisplayNameLocalizedAttribute("CardtrendAccount", "RefDocLbl")]
       public string refDoc { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAccount", "SysIndLbl")]
       public string sysInd { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAccount", "AcctNoLbl")]
       public string acctNo { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAccount", "CardNoLbl")]
       public string CardNo { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAccount", "ClosedDateLbl")]
        public string ClosedDate { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAccount", "XreferenceDocLbl")]
       public string XreferenceDoc { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAccount", "SelectedModuleDdl")]
       public string SelectedModule { get; set; }
       public IEnumerable<SelectListItem> Module { get; set; }
       [DisplayNameLocalizedAttribute("CardtrendAccount", "NewCommentLbl")]
       public string newComment { get; set; }


    }

    public class EventLogList 
    {
        [DisplayName("Seq No")]
        public string SeqNo { get; set; }
        [DisplayName("Event Date")]
        public string EventDate { get; set; }
        [DisplayName("Xreference No")]
        public string XreferenceNo { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("Reference Key")]
        public string RefKey { get; set; }
        [DisplayName("Alert Date")]
        public string AlertDate { get; set; }
        [DisplayName("Assign User Id")]
        public string AssignUserId { get; set; }
        [DisplayName("Status")]
        public string SelectedStatus { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
    }

    public class  EventDetails 
    {

        [DisplayName("Account No")]
        public string AccountNo { get; set; }
        [DisplayName("Business Location")]
        public string BusnLoc { get; set; }
        [DisplayName("Event Id")]
        public string EventId { get; set; }
        [DisplayName("Seq No")]
        public string SeqNo { get; set; }
        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }
        [DisplayName("Reminder DateTime")]
        public string ReminderDatetime { get; set; }
        [DisplayName("Creation By")]
        public string CreationBy { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }

        [DisplayName("Event Type")]
        public string SelectedEventType { get; set; }
        public IEnumerable<SelectListItem> EventType { get; set; }

        
        }


}
