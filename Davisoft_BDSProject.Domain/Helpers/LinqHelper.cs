using System;
using System.Collections.Generic;
using System.Linq;

namespace Davisoft_BDSProject.Domain.Helpers
{
    public static class LinqHelper
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }

        public static void AddRange<TSource>(this ICollection<TSource> collection, IEnumerable<TSource> range)
        {
            foreach (var item in range)
            {
                collection.Add(item);
            }
        } 
    }
}
