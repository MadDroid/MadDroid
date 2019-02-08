using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MadDroid.Helpers
{
    /// <summary>
    /// A helper class to serialize/deserialize json
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// Deserialize a json to object
        /// </summary>
        /// <typeparam name="T">The type of the object which the json will be deserialized</typeparam>
        /// <param name="value">The json to be deserialized</param>
        /// <returns></returns>
        public static async Task<T> ToObjectAsync<T>(string value)
        {
            // Make async
            return await Task.Run<T>(() =>
            {
                // Deserialize json
                return JsonConvert.DeserializeObject<T>(value);
            });
        }

        /// <summary>
        /// Serialize a object to json
        /// </summary>
        /// <param name="value">The object to be serialized</param>
        /// <param name="formatting">Formatting option</param>
        /// <returns></returns>
        public static async Task<string> StringifyAsync(object value, Formatting formatting = Formatting.None)
        {
            // Make async
            return await Task.Run<string>(() =>
            {
                // Serialize object
                return JsonConvert.SerializeObject(value, formatting);
            });
        }

        // TODO: Documentation
        public static async Task<T> ToObjectAsync<T>(string value, string token)
        {
            return await Task.Run<T>(() =>
            {
                return JObject.Parse(value).SelectToken(token).ToObject<T>();
            });
        }
    }
}
