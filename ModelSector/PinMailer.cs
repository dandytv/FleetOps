using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelSector;
using CCMS.ModelSector;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModelSector.Global_Resources;
using System.Web.Mvc;

namespace ModelSector
{
   public class PinMailerBatchList
    {
       [DisplayName("Batch ID")]
       public long BatchID { get; set; }
       [DisplayName("Creation Date")]
       public string CreationDate { get; set; }
       [DisplayName("Status")]
       public string Sts { get; set; }
       [DisplayName("Count")]
       public long Count { get; set; }
    }

   public class PinMailerBatchView
   {
       [DisplayName("Seq No")]
       public long SeqNo { get; set; }
       [DisplayName("Status")]
       public string StsDescp { get; set; }
       [DisplayName("Card No")]
       public string CardNo { get; set; }
       [DisplayName("Card Creation Date")]
       public string CardCreationDate { get; set; }
       [DisplayName("Company Name")]
       public string CompName { get; set; }
       [DisplayName("Driver Name")]
       public string DriverName { get; set; }
       [DisplayName("PIC")]
       public string PIC { get; set; }
       [DisplayName("Address")]
       public string Address { get; set; }
   }

}
