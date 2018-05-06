using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto
{
   public class ColumnInfo
    {
        public string FieldName { get; set; }

        public string DataType { get; set; }

        public object Value { get; set; }

        public int ColLength { get; set; }
    }
}
