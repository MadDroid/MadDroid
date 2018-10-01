using System;
using System.Collections.Generic;
using System.Linq;

namespace MadDroid.Helpers
{
    /// <summary>
    /// Extensions for <see cref="System.Collections.Generic.IList{T}"/>
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Performs the specified action on each element of the <see cref="System.Collections.Generic.IList{T}"/> with index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action">The <see cref="System.Action{T1, T2}"/> delegate to perform on each element of the <see cref="System.Collections.Generic.List{T}"/></param>
        public static void ForEach<T>(this IList<T> list, Action<T, int> action)
        {
            // If the action is null...
            if (action == null)
                // Throw an exception
                throw new ArgumentNullException(nameof(action));

            // For each item in the list...
            for (int i = 0; i < list.Count; i++)
            {
                // Run the action
                action(list[i], i);
            }
        }

        /// <summary>
        /// Returns random item of the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Random<T>(this IList<T> list)
        {
            // Crate a random instance
            Random r = new Random();

            // return the random item of the list
            return list[r.Next(0, list.Count)];
        }

        /// <summary>
        /// Spit an <see cref="IList{T}"/> into chunks of the specified size
        /// </summary>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <param name="list">The IList to be splited</param>
        /// <param name="size">The size of the chunks</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IList<T> list, int size)
        {
            for (var i = 0; i < (float)list.Count / size; i++)
            {
                yield return list.Skip(i * size).Take(size);
            }
        }
    }
}
