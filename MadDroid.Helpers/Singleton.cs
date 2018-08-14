using System;
using System.Collections.Concurrent;

namespace MadDroid.Helpers
{
    /// <summary>
    /// A Helper class to create singletons
    /// </summary>
    /// <typeparam name="T">The type of the object to get the singleton instace</typeparam>
    public static class Singleton<T> where T : new()
    {
        private static ConcurrentDictionary<Type, T> _instances = new ConcurrentDictionary<Type, T>();

        /// <summary>
        /// Gets the instace of the object
        /// </summary>
        public static T Instance
        {
            get
            {
                // Gets or add the instance to the ConcurrentDictionary
                return _instances.GetOrAdd(typeof(T), (t) => new T());
            }
        }
    }
}
