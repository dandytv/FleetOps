using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ModelSector
{
   public class CollectionFollowUpViewModel
    {
        public string EventId { get; set; }
        [DisplayName("Collection Status")]
        public string SelectedCollectionSts { get; set; }
        public IEnumerable<SelectListItem> CollectionSts { get; set; }

        [DisplayName("Priority")]
        public string SelectedPriority { get; set; }
        public IEnumerable<SelectListItem> Priority { get; set; }

        [DisplayName("Creation Date")]
        public string CreationDate { get; set; }

        [DisplayName("Recall Date")]
        public string RecallDate { get; set; }

        [DisplayName("Last Update")]
        public string LastUpdate { get; set; }

        [DisplayName("User Id")]
        public string UserId { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [DisplayName("Creation Date")]
        public string NoteCreationDate { get; set; }
    }
}
