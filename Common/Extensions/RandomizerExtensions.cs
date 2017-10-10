namespace Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RandomizerExtensions
    {
        private static readonly Random Random = new Random();

        public static T GetRandomElement<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                return default(T);
            }

            return source.ElementAt(RandomizerExtensions.Random.Next(0, source.Count()));
        }
    }
}