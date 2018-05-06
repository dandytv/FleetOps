using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Common.Helpers
{
   public static class DateTimeHelper
    {
       public static string DefaultDatetimeFormat()
       {
           return ConfigurationManager.AppSettings["DatetimeFormat"] != null ? ConfigurationManager.AppSettings["DatetimeFormat"].ToString() : "dd/MM/yyyy";
       }
    }
}
