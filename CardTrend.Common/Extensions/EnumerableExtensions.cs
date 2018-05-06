using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Common.Extensions
{
    public enum ErrorCode
    {
        PasswordExpired = 95670,
        FirstTimeLogin = 95674
    }

    public static class EnumerableExtensions
    {
        public static int Count(this IEnumerable source)
        {
            int count = 0;

            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                count++;
            }

            return count;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            foreach (T item in source)
                action(item);
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return (source != null && source.Any());
        }
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return (source != null && source.Any(predicate));
        }
    }
}
