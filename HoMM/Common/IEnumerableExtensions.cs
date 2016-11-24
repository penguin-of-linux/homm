using HoMM.Generators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HoMM
{
    static class IEnumerableExtensions
    {
        /// <summary>
        /// Yields only indices that are lying inside of a triangle with 
        /// sides produced by lines: X = 0; Y = 0; X / size.X + Y / size.Y = 1
        /// </summary>
        public static IEnumerable<SigmaIndex> Clamp(this IEnumerable<SigmaIndex> source, MapSize size)
        {
            return source
                .Where(index => index.IsInside(size) && index.IsAboveDiagonal(size));
        }
        
        public static T Argmin<T>(this ICollection<T> source, Func<T, double> selector)
        {
            return source.Where(x => selector(x) == source.Min(selector)).FirstOrDefault();
        }
        
        public static T Argmax<T>(this ICollection<T> source, Func<T, double> selector)
        {
            return source.Where(x => selector(x) == source.Max(selector)).FirstOrDefault();
        }
    }
}
