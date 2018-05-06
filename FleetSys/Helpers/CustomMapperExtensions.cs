using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetSys.Helpers
{
    public class CustomMapperExtensions
    {
        public static List<T> MapIEnumerableToList<T>(IEnumerable<T> source) where T : class
        {
            return source == null
                ? new List<T>()
                : source.ToList();
        }

        public static IEnumerable<T> MapListToIEnumerable<T>(List<T> source) where T : class
        {
            return source.Count > 0
                ? source.ToList()
                : new List<T>();
        }
    }
}