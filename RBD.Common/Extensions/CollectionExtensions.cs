using System;
using System.Collections.Generic;
using System.Linq;

namespace RBD.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T[]> GetAllVariants<T>(this IEnumerable<T> col)
        {
            var maxNumber = Math.Pow(2, col.Count());
            for (int i = 1; i < maxNumber; i++)
            {
                yield return GetVariant(col, i).ToArray();
            }
        }

        public static IEnumerable<T[]> GetAllVariants<T>(this IEnumerable<T> col, int length)
        {
            return col.GetAllVariants(length, length);
        }

        public static IEnumerable<T[]> GetAllVariants<T>(this IEnumerable<T> col, int min, int max)
        {
            var maxNumber = Math.Pow(2, col.Count());
            for (int i = 1; i < maxNumber; i++)
            {
                var qwont = GetBits(i);
                if (qwont >= min && qwont <= max)
                {
                    yield return GetVariant(col, i).ToArray();
                }
            }
        }

        private static int GetBits(int number)
        {
            var sum = 0;

            while (number > 0)
            {
                sum += number & 1;
                number >>= 1;
            }

            return sum;
        }

        private static bool BitIsSet(int number, int bit)
        {
            return ((number >> bit) & 1) > 0;
        }

        public static IEnumerable<T> GetVariant<T>(IEnumerable<T> col, int number)
        {
            for (int i = 0; i < col.Count(); i++)
            {
                if (BitIsSet(number, i))
                {
                    yield return col.ElementAt(i);
                }
            }
        }
    }
}
