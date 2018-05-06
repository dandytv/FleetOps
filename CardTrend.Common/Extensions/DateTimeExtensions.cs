using CardTrend.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CardTrend.Common
{
   public static class DateTimeExtensions
    {
       public static string ToStringDatetime(this DateTime? input)
       {
           string tempDate;
           if (input == null)
           {
               tempDate = "";
           }
           else
           {
               tempDate = input.Value.ToString("dd/MM/yyyy, HH:mm:ss");
           }
           return tempDate;
       }
    }
}
