using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace MadDroid.Helpers
{
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
        /// <remarks>If DEBUG configuration is selected, the json will be indented</remarks>
        /// <returns></returns>
        public static async Task<string> StringifyAsync(object value)
        {
            // Declare the formating type
            Formatting formatting = Formatting.None;
#if DEBUG
            // If DEBUG mode, set de formating type to indented
            formatting = Formatting.Indented;
#endif
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
