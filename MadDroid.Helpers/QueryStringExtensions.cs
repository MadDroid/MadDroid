using System;
using System.Collections.Specialized;
using System.Linq;

namespace MadDroid.Helpers
{
    /// <summary>
    /// A helper class to build query string
    /// </summary>
    public static class QueryStringExtensions
    {
        /// <summary>
        /// Build a query string from a <see cref="NameValueCollection"/>
        /// </summary>
        /// <param name="nvc">The <see cref="NameValueCollection"/> in which the query string will be built</param>
        /// <param name="encode">A <see cref="Func{T, TResult}"/> to encode de string</param>
        /// <returns>Returns the query string beginning with a ?</returns>
        public static string ToQueryString(this NameValueCollection nvc, Func<string, string> encode)
        {
            // Get all the keys with their respective values to an Array
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", encode(key), encode(value)))
                        .ToArray();
            // Returns the keys and their values append
            return "?" + string.Join("&", array);
        }

        /// <summary>
        /// Build a query string from a <see cref="NameValueCollection"/> escaped from <see cref="Uri.EscapeDataString(string)"/>
        /// </summary>
        /// <param name="nvc">The <see cref="NameValueCollection"/> in which the query string will be built</param>
        /// <returns>Returns the query string beginning with a ?</returns>
        public static string ToQueryString(this NameValueCollection nvc) => ToQueryString(nvc, (arg) => Uri.EscapeDataString(arg));
    }
}
