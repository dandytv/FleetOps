using System.Collections.Generic;
using System.Web.Mvc;
using CCMS.ModelSector;
using System.ComponentModel;
using ModelSector.Global_Resources;
using System.ComponentModel.DataAnnotations;
namespace ModelSector
{
   public class CardCourierCollection
    {
        public CardnAccNo _CardnAccNo { get; set; }

        [Display(Name = "batchid", ResourceType = typeof(locale))]
        public string BatchId { get; set; }

        public IEnumerable<SelectListItem> DeliveryMethod { get; set; }
        [Display(Name="deliverymethod", ResourceType = typeof(locale))]
        public string SelectedDeliveryMethod { get; set; }

        public IEnumerable<SelectListItem> Status { get; set; }
        [Display(Name="deliverystatus", ResourceType = typeof(locale))]
        public string DeliveryStatus { get; set; }

        public IEnumerable<SelectListItem> DeliveredBy { get; set; }
        [Display(Name="deliveredby", ResourceType = typeof(locale))]
        public string SelectedCourierCompany { get; set; }

        [Display(Name="courierconsignmentnote", ResourceType = typeof(locale))]
        public string CourierConsignmentNote { get; set; }

        [Display(Name="recipientname", ResourceType = typeof(locale))] 
        public string RecipientName { get; set; }

        [Display(Name="descp", ResourceType = typeof(locale))]
        public string Descp { get; set; }

        [Display(Name="recieveddate", ResourceType = typeof(locale))]
        public string RecievedDate { get; set; }
    }
}




