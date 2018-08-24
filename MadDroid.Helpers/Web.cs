using System.Net.Http;
using System.Threading.Tasks;

namespace MadDroid.Helpers
{
    /// <summary>
    /// A helper class to get stuff from web
    /// </summary>
    public static class Web
    {
        /// <summary>
        /// Get a string async
        /// </summary>
        /// <param name="url">The url to get the string</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public static async Task<string> GetStringAsync(string url)
        {
            // Create the client
            using (var client = new HttpClient())
            {
                // Get and return the string
                return await client.GetStringAsync(url);
            }
        }
    }
}
