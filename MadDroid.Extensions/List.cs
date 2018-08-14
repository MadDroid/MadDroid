using System;
using System.Collections.Generic;

namespace MadDroid.Extensions
{
    /// <summary>
    /// Extensions for <see cref="System.Collections.Generic.List{T}"/>
    /// </summary>
    public static class List
    {
        /// <summary>
        /// Performs the specified action on each element of the <see cref="System.Collections.Generic.List{T}"/> with index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action">The <see cref="System.Action{T1, T2}"/> delegate to perform on each element of the <see cref="System.Collections.Generic.List{T}"/></param>
        public static void ForEach<T>(this List<T> list, Action<T, int> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                action?.Invoke(list[i], i);
            }
        }

        /// <summary>
        /// Returns random item of the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Random<T>(this List<T> list)
        {
            Random r = new Random();
            return list[r.Next(0, list.Count)];
        }
    }
}
