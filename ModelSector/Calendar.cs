using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ModelSector
{
   public class Comment
    {
        public string EventId { get; set; }
        public string Commenter { get; set; }
        public string CommentId { get; set; }
        public string CommentText { get; set; }
        public string CommentDate { get; set; }
    }
    public class Events
    {
        public string id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool allDay { get; set; }
        public string className { get; set; }
    }
}
