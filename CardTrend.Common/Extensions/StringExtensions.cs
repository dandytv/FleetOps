using CardTrend.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CardTrend.Common.Extensions
{
   public static class StringExtensions
    {
       private static char[] characters = new char [] { '*','&'};
       public static string Truncate(this string value, int length)
       {
           if (string.IsNullOrEmpty(value))
               return string.Empty;

           return value.Substring(0, Math.Min(length, value.Length));
       }
       public static string getPartialPath(string dir, string name)
       {
           return String.Format("~/Views/Shared/Partials/{0}/{1}.cshtml", dir, name);
       }
       /// <summary>
       /// Check if email is valid
       /// </summary>
       /// <param name="s"></param>
       /// <returns></returns>
       public static bool IsEmail(this string s)
       {
           if (string.IsNullOrWhiteSpace(s))
               return false;
           var regexString = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
           var regex = new Regex(regexString);
           return regex.IsMatch(s);
       }


       public static string ToMD5(this string value)
       {
           using (MD5 md5 = MD5.Create())
           {
               byte[] code = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
               return Convert.ToBase64String(code);
           }
       }

       public static double GetNumericValue(this string input)
       {
           Regex regex = new Regex(@"\d+");
           var match = regex.Match(input);
           return string.IsNullOrEmpty(match.Value)
                       ? default(double)
                       : double.Parse(match.Value);
       }

       public static string To10Characters(this string value)
       {
           if (value.Length <= 10)
               return value;
           return value.Substring(0, 10);
       }

       public static string CreateRandomPassword(int length)
       {
           string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
           Random randNum = new Random();
           char[] chars = new char[length];

           for (int i = 0; i < length; i++)
           {
               chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
           }
           return new string(chars);
       }
       public static bool ContainsOneCharacter(string text)
       {
           var intersection = text.Intersect(characters).ToList();
           if (intersection.Count != 1)
               return false; // Make sure there is only one character in the text

           // Get a count of all of the one found character
           if (1 == text.Count(t => t == intersection[0]))
               return true;

           return false;
       }
       public static string TruncatePercents(string oldText)
       {
           string newText = oldText;
           foreach(var cha in characters)
           {
               if (oldText.Contains(cha))
                   newText = newText.Replace(cha, '_');
           }
           return newText;
       }
    }
}
