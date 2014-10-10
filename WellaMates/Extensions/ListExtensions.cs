using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellaMates.Extensions
{
    public static class ListExtensions
    {
        public static List<TSource> SortAscending<TSource, TValue>(
          this List<TSource> source,
          Func<TSource, TValue> selector)
        {
            var comparer = Comparer<TValue>.Default;
            source.Sort((x, y) => comparer.Compare(selector(x), selector(y)));
            return source;
        }
        public static List<TSource> SortDescending<TSource, TValue>(
          this List<TSource> source,
          Func<TSource, TValue> selector)
        {
            var comparer = Comparer<TValue>.Default;
            source.Sort((x, y) => comparer.Compare(selector(y), selector(x)));
            return source;
        }
    }
}