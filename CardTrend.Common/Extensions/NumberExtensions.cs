using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Common.Extensions
{
   public static class NumberExtensions
    {
       public static Boolean BoolConverter(object tempData) // YN to Bool
       {
           if (tempData != DBNull.Value)
           {
               if (string.IsNullOrEmpty(Convert.ToString(tempData)))
                   return false;
               string opt = Convert.ToString(tempData);
               if ((opt == "No") || (opt == "N"))
               {
                   return false;
               }
               else
                   if ((opt == "Yes") || (opt == "Y"))
                   {
                       return true;
                   }
           }
           else
           {
               return false;
           }
           return false;
       }
       public static string ConvertBoolDB(object tempObj) // Bool to YN
       {
           string DbValue;
           if (tempObj != null)
           {
               bool tempData = Convert.ToBoolean(tempObj);
               if (tempData == true)
               {
                   return DbValue = "Y";
               }
               else
               {
                   return DbValue = "N";
               }
           }
           else
           {
               return DbValue = Convert.ToString(DBNull.Value);
           }
       }
       public static int ConvertInt(object tempdata)
       {
           int temp = 0;
           if (tempdata != null && tempdata != DBNull.Value)
           {
               return temp = Convert.ToInt32(tempdata);
           }
           else
           {
               return temp;
           }
       }
       public static Int64 ConvertLong(object tempdata)
       {
           Int64 temp = 0;
           if (tempdata != null && tempdata != DBNull.Value)
           {
               return temp = Convert.ToInt64(tempdata);
           }
           else
           {
               return temp;
           }
       }
       public static Object ConvertToDatetime(string tempData)//to database
       {
           DateTime tempDate;
           string datetime;
           if (string.IsNullOrEmpty(tempData) || String.IsNullOrWhiteSpace(tempData))
           {
               return DBNull.Value;
           }
           else
           {
               tempDate = DateTime.ParseExact(tempData, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
               datetime = tempDate.ToString("yyyy-MM-dd HH:mm:ss");
               return datetime;
           }
       }

       public static DateTime? ConvertToDatetimeDB2(string tempData)//to database
       {
           DateTime tempDate;
           if (string.IsNullOrEmpty(tempData) || String.IsNullOrWhiteSpace(tempData))
           {
               return null ;
           }
           else
           {
               tempDate = DateTime.ParseExact(tempData, "dd/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
               return tempDate;
           }
       }
       public static Object ConvertToDatetimeDB(string tempData)//to database
       {
           DateTime tempDate;
           string datetime;
           if (string.IsNullOrEmpty(tempData) || String.IsNullOrWhiteSpace(tempData))
           {
               return DBNull.Value;
           }
           else
           {
               tempDate = DateTime.ParseExact(tempData, "dd/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
               datetime = tempDate.ToString("yyyy-MM-dd HH:mm:ss");
               return datetime;
           }
       }
       public static Object DateConverterDB(string tempData)//to database
       {
           DateTime tempDate;
           string datetime;

           if (string.IsNullOrEmpty(tempData) || String.IsNullOrWhiteSpace(tempData))
           {
               return DBNull.Value;
           }
           else
           {
               tempDate = DateTime.ParseExact(tempData, "dd/MM/yyyy", CultureInfo.InvariantCulture);
               datetime = tempDate.ToString("yyyy-MM-dd");
               return datetime;
           }
       }
       public static string DateConverter(object tempData)//from database
       {
           string tempDate;

           if (tempData == DBNull.Value || string.IsNullOrEmpty(Convert.ToString(tempData)))
           {
               tempDate = "";
           }
           else
           {
               tempDate = Convert.ToDateTime(tempData).ToString("dd/MM/yyyy");
           }
           return tempDate;
       }
       public static Int64 ConvertToInt(object value)
       {

           if (value == DBNull.Value)
               return 0;
           return Convert.ToInt64(value);
       }
       public static Int32 ConvertToInt32(object value)
       {

           if (value == DBNull.Value)
               return 0;
           return Convert.ToInt32(value);
       }
       public static string DateTimeConverter(object tempData)//from database
       {
           string tempDate;

           if (tempData == DBNull.Value || string.IsNullOrEmpty(Convert.ToString(tempData)))
           {
               tempDate = "";
           }
           else
           {
               tempDate = Convert.ToDateTime(tempData).ToString("dd/MM/yyyy hh:mm:ss tt");
           }

           return tempDate;
       }
       public static string DateTimeConverter2(object tempData)//from database
       {
           string tempDate;

           if (tempData == DBNull.Value || string.IsNullOrEmpty(Convert.ToString(tempData)))
           {
               tempDate = "";
           }
           else
           {
               tempDate = Convert.ToDateTime(tempData).ToString("hh:mm tt - dd MMM yyyy");
           }
           return tempDate;
       }
       public static decimal ConvertDecimalToDb(object value)//to database
       {
           if (value != null && value != DBNull.Value)
           {
               return Convert.ToDecimal(value);
           }
           else
           {
               return 0;
           }
       }
       public static int ConvertIntToDb(object value)//to database
       {
           if (value != null && value != DBNull.Value)
           {
               return Convert.ToInt16(value);
           }
           else
           {
               return 0;
           }
       }
       public static Int64 ConvertLongToDb(object value)//to database
       {
           if (value != null && value != DBNull.Value)
           {
               return Convert.ToInt64(value);
           }
           else
           {
               return 0;
           }
       }
        public static decimal ConverterDecimal(this string value)
        {
            decimal number;
            string tempValue = value;

            var punctuation = value.Where(x => char.IsPunctuation(x)).Distinct();
            int count = punctuation.Count();

            NumberFormatInfo format = CultureInfo.InvariantCulture.NumberFormat;
            switch (count)
            {
                case 0:
                    break;
                case 1:
                    tempValue = value.Replace(",", ".");
                    break;
                case 2:
                    if (punctuation.ElementAt(0) == '.')
                        tempValue = SwapChar(value, '.', ',');
                    break;
                default:
                    throw new InvalidCastException();
            }

            number = decimal.Parse(tempValue, format);
            return number;
        }
        public static string CustomNumberFormat(string myNumber)
        {
            try
            {
            if(!string.IsNullOrEmpty(myNumber))
            {
                int dot = myNumber.IndexOf(".");

                string strNumberWithoutDecimals = myNumber.Substring(0, (dot == -1 ? myNumber.Length : dot));
                string strNumberDecimals = (dot == -1 ? "" : myNumber.Substring(dot));

                strNumberWithoutDecimals = System.Convert.ToInt64(strNumberWithoutDecimals).ToString("#,##0");

                return strNumberWithoutDecimals + strNumberDecimals;
            }else
            {
                return "0.00";
            }
            }
            catch (Exception ex)
            {
                return myNumber;
            }
        }
        public static string SwapChar(this string value, char from, char to)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            StringBuilder builder = new StringBuilder();

            foreach (var item in value)
            {
                char c = item;
                if (c == from)
                    c = to;
                else if (c == to)
                    c = from;

                builder.Append(c);
            }
            return builder.ToString();
        }
        public static Object ConvertDatetimeDB(string tempData)//to database
        {
            string[] formats = {"dd/MM/yyyy h:m tt", "dd/MM/yyyy hh:mm tt","dd/MM/yyyy H:mm","dd/MM/yyyy HH:mm", "dd/MM/yyyy h:mm:ss tt","dd/MM/yyyy hh:mm:ss tt","dd/MM/yyyy H:mm:ss", "dd/MM/yyyy HH:mm:ss",
								"d/M/yyyy h:m tt", "d/M/yyyy hh:mm tt","d/M/yyyy H:mm","d/M/yyyy HH:mm", "d/M/yyyy h:mm:ss tt","d/M/yyyy hh:mm:ss tt","d/M/yyyy H:mm:ss", "d/M/yyyy HH:mm:ss", 
                                "dd/MM/yyyy","d/M/yy h:m tt", "M/d/yy hh:mm tt","M/d/yy H:mm","M/d/yyyy HH:mm", "M/d/yy h:mm:ss tt","M/d/yy hh:mm:ss tt","M/d/yy H:mm:ss", "M/d/yy HH:mm:ss",
								"MM/dd/yy h:m tt", "MM/dd/yy hh:mm tt","MM/dd/yy H:mm","MM/dd/yy HH:mm", "MM/dd/yy h:mm:ss tt","MM/dd/yy hh:mm:ss tt","MM/dd/yy H:mm:ss", "MM/dd/yy HH:mm:ss",
								"dd/MM/yyyy", "d/M/yyyy", "M/d/yy", "MM/dd/yy"};
            DateTime OutDateTime;
            string datetime;
            if (string.IsNullOrEmpty(tempData) || String.IsNullOrWhiteSpace(tempData))
            {
                return DBNull.Value;
            }
            else
            {
                foreach (string i in formats)
                {
                    if (DateTime.TryParseExact(tempData, i, new CultureInfo("en-US"), DateTimeStyles.None, out OutDateTime))
                    {
                        if (!i.ToLower().Contains("h"))
                        {
                            datetime = OutDateTime.ToString("yyyy-MMM-dd");

                        }
                        else
                        {
                            datetime = OutDateTime.ToString("yyyy-MMM-dd HH:mm:ss");
                        }
                        return datetime;

                    }

                }

            }
            return DateTime.MinValue;
        }
        public static int getFlagCode(object code)
        {
            return Convert.ToInt32(code) < 55000 ? 0 : 1;
        }
    }
}
