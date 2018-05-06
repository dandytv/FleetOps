using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSector
{
   public class EventConfiguration
    {
        public string Id { get; set; }
        public string SelectedType { get; set; }
        public string ShortDescp { get; set; }
        public string Descp { get; set; }
        public string RefTo { get; set; }
        public string RefKey { get; set; }
        public string SelectedStatus { get; set; }
        public string UpdateDate { get; set; }
        public string UpdatedBy { get; set; }
    }
   public class EventRef
   {
       public string ShortDescp { get; set; }
       public string Descp { get; set; }
   }

}
