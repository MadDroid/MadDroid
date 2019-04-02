using System.Collections.Generic;

namespace MadDroid.Helpers
{
    /// <summary>
    /// Extension methods for <see cref="ICollection{T}"/>
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Indicates whether the specified collection is null or <see cref="ICollection{T}.Count"/> is equal to 0
        /// </summary>
        /// <typeparam name="T">The type of the collection to test</typeparam>
        /// <param name="collection">The collection to test</param>
        /// <returns>True if the value parameter is null or an empty (Count = 0); otherwise, false</returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection) => collection is null || collection.Count == 0;
    }
}
