using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace MadDroid.Helpers
{
    /// <summary>
    /// A helper class to save and retrieve objects
    /// </summary>
    public static class Storage
    {
        /// <summary>
        /// Save an object to specified path
        /// </summary>
        /// <typeparam name="T">The type of the object to be saved</typeparam>
        /// <param name="path">The path where to save the object</param>
        /// <param name="value">The object to be saved</param>
        /// <param name="formatting">The formatting of the json</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> used to serialize the object. If this
        ///                        is null, default serialization settings will be used.</param>
        /// <returns></returns>
        public static async Task SaveAsync<T>(string path, T value, Formatting formatting = Formatting.None, JsonSerializerSettings settings = null)
        {
            // Make async
            await Task.Run(async () =>
            {
                // Stringfy the object
                string json = await Json.StringifyAsync(value, formatting, settings);
                // Write the file to disk
                File.WriteAllText(path, json);
            });
        }

        /// <summary>
        /// Read an object from the specified path
        /// </summary>
        /// <typeparam name="T">The type of the object that will be retrieved</typeparam>
        /// <param name="path">The path where the object is stored</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> used to deserialize the object. If
        ///                        this is null, default serialization settings will be used.</param>
        /// <returns>The retrieved object</returns>
        /// <remarks>
        ///     Returns null if the file is empty
        /// </remarks>
        /// <exception cref="FileNotFoundException"/>
        public static async Task<T> ReadAsync<T>(string path, JsonSerializerSettings settings = null)
        {
            // Make async
            return await Task.Run(async () =>
            {
                // Read the file from disk
                string json = File.ReadAllText(path);
                // Return the object deserialized
                return await Json.ToObjectAsync<T>(json, settings);
            });
        }

        /// <summary>
        /// Try to read an object from the specified path
        /// </summary>
        /// <typeparam name="T">The type of the object that will be retrieved</typeparam>
        /// <param name="path">The path where the object is stored</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> used to deserialize the object. If
        ///                        this is null, default serialization settings will be used.</param>
        /// <returns>The object or null if the file is empty or doesn't exist</returns>
        public static async Task<T> TryReadAsync<T>(string path, JsonSerializerSettings settings = null)
        {
            // Make async
            return await Task.Run(async () =>
            {
                // Checks if the file exists
                if (!File.Exists(path))
                    return default;

                // Read the file from disk
                string json = File.ReadAllText(path);
                // Return the object deserialized
                return await Json.ToObjectAsync<T>(json, settings);
            });
        }
    }
}
