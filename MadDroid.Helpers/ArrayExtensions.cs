using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MadDroid.Helpers
{
    /// <summary>
    /// Extensions methods for arrays
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Spit an array into chunks of the specified size
        /// </summary>
        /// <typeparam name="T">The type of the array</typeparam>
        /// <param name="array">The array to be splited</param>
        /// <param name="size">The size of the chunks</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] array, int size)
        {
            for (int i = 0; i < (float)array.Length / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }
    }
}
