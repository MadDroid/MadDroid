using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MadDroid.Helpers
{
    /// <summary>
    /// A helper class to save and retrieve objects
    /// </summary>
    public static class Storage
    {
        #region Fields

        /// <summary>
        /// A list of <see cref="SemaphoreSlim"/> to use when manipulating files.
        /// </summary>
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> filesSamphore = new ConcurrentDictionary<string, SemaphoreSlim>();

        #endregion

        #region Save

        /// <summary>
        /// Save an object to specified path.
        /// </summary>
        /// <typeparam name="T">The type of the object to be saved.</typeparam>
        /// <param name="path">The path where to save the object.</param>
        /// <param name="value">The object to be saved.</param>
        /// <param name="formatting">The formatting of the json.</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> used to serialize the object. If this
        ///                        is null, default serialization settings will be used.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="PathTooLongException"/>
        /// <exception cref="DirectoryNotFoundException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="JsonException"/>
        /// <returns></returns>
        public static async Task SaveAsync<T>(string path, T value, Formatting formatting, JsonSerializerSettings settings)
        {
            // Make async
            await Task.Run(async () =>
            {
                // Stringfy the object
                string json = await Json.StringifyAsync(value, formatting, settings);
                // Write the file to disk
                File.WriteAllText(path, json);
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Save an object to specified path.
        /// </summary>
        /// <typeparam name="T">The type of the object to be saved.</typeparam>
        /// <param name="path">The path where to save the object.</param>
        /// <param name="value">The object to be saved.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="PathTooLongException"/>
        /// <exception cref="DirectoryNotFoundException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="JsonException"/>
        /// <returns></returns>
        public static async Task SaveAsync<T>(string path, T value) =>
            await SaveAsync(path, value, Formatting.None, null).ConfigureAwait(false);

        /// <summary>
        /// Save an object to specified path.
        /// </summary>
        /// <typeparam name="T">The type of the object to be saved.</typeparam>
        /// <param name="path">The path where to save the object.</param>
        /// <param name="value">The object to be saved.</param>
        /// <param name="formatting">The formatting of the json.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="PathTooLongException"/>
        /// <exception cref="DirectoryNotFoundException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="JsonException"/>
        /// <returns></returns>
        public static async Task SaveAsync<T>(string path, T value, Formatting formatting) =>
            await SaveAsync(path, value, formatting, null).ConfigureAwait(false);

        /// <summary>
        /// Save an object to specified path.
        /// </summary>
        /// <typeparam name="T">The type of the object to be saved.</typeparam>
        /// <param name="path">The path where to save the object.</param>
        /// <param name="value">The object to be saved.</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> used to serialize the object. If this
        ///                        is null, default serialization settings will be used.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="PathTooLongException"/>
        /// <exception cref="DirectoryNotFoundException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="JsonException"/>
        /// <returns></returns>
        public static async Task SaveAsync<T>(string path, T value, JsonSerializerSettings settings) =>
            await SaveAsync(path, value, Formatting.None, settings).ConfigureAwait(false);

        /// <summary>
        /// Try to save an object to specified path.
        /// </summary>
        /// <typeparam name="T">The type of the object to be saved.</typeparam>
        /// <param name="path">The path where to save the object.</param>
        /// <param name="value">The object to be saved.</param>
        /// <param name="formatting">The formatting of the json.</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> used to serialize the object. If this
        ///                        is null, default serialization settings will be used.</param>
        /// <returns>True if the object was successfully saved. Otherwise, false.</returns>
        public static async Task<bool> TrySaveAsync<T>(string path, T value, Formatting formatting, JsonSerializerSettings settings)
        {
            if (path is null)
                return false;

            var semaphore = GetSemaphore(path);
            try
            {
                await semaphore.WaitAsync().ConfigureAwait(false);
                await SaveAsync(path, value, formatting, settings).ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                semaphore.Release();
            }
        }

        /// <summary>
        /// Try to save an object to specified path.
        /// </summary>
        /// <typeparam name="T">The type of the object to be saved.</typeparam>
        /// <param name="path">The path where to save the object.</param>
        /// <param name="value">The object to be saved.</param>
        /// <returns>True if the object was successfully saved. Otherwise, false.</returns>
        public static async Task<bool> TrySaveAsync<T>(string path, T value) =>
            await TrySaveAsync(path, value, Formatting.None, null).ConfigureAwait(false);

        /// <summary>
        /// Try to save an object to specified path.
        /// </summary>
        /// <typeparam name="T">The type of the object to be saved.</typeparam>
        /// <param name="path">The path where to save the object.</param>
        /// <param name="value">The object to be saved.</param>
        /// <param name="formatting">The formatting of the json.</param>
        /// <returns>True if the object was successfully saved. Otherwise, false.</returns>
        public static async Task<bool> TrySaveAsync<T>(string path, T value, Formatting formatting) =>
            await TrySaveAsync(path, value, formatting, null).ConfigureAwait(false);

        /// <summary>
        /// Try to save an object to specified path.
        /// </summary>
        /// <typeparam name="T">The type of the object to be saved.</typeparam>
        /// <param name="path">The path where to save the object.</param>
        /// <param name="value">The object to be saved.</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> used to serialize the object. If this
        ///                        is null, default serialization settings will be used.</param>
        /// <returns>True if the object was successfully saved. Otherwise, false.</returns>
        public static async Task<bool> TrySaveAsync<T>(string path, T value, JsonSerializerSettings settings) =>
            await TrySaveAsync(path, value, Formatting.None, settings).ConfigureAwait(false);

        #endregion

        #region Read

        /// <summary>
        /// Read an object from the specified path.
        /// </summary>
        /// <typeparam name="T">The type of the object that will be retrieved.</typeparam>
        /// <param name="path">The path where the object is stored.</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> used to deserialize the object. If
        ///                        this is null, default serialization settings will be used.</param>
        /// <returns>The retrieved object.</returns>
        /// <remarks>
        ///     Returns null if the file is empty.
        /// </remarks>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="PathTooLongException"/>
        /// <exception cref="DirectoryNotFoundException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="FileNotFoundException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="JsonException"/>
        public static async Task<T> ReadAsync<T>(string path, JsonSerializerSettings settings)
        {
            // Make async
            return await Task.Run(async () =>
            {
                // Read the file from disk
                string json = File.ReadAllText(path);
                // Return the object deserialized
                return await Json.ToObjectAsync<T>(json, settings).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Read an object from the specified path.
        /// </summary>
        /// <typeparam name="T">The type of the object that will be retrieved.</typeparam>
        /// <param name="path">The path where the object is stored.</param>
        /// <returns>The retrieved object.</returns>
        /// <remarks>
        ///     Returns null if the file is empty.
        /// </remarks>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="PathTooLongException"/>
        /// <exception cref="DirectoryNotFoundException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="FileNotFoundException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="System.Security.SecurityException"/>
        /// <exception cref="JsonException"/>
        public static async Task<T> ReadAsync<T>(string path) =>
            await ReadAsync<T>(path, null).ConfigureAwait(false);

        /// <summary>
        /// Try to read an object from the specified path.
        /// </summary>
        /// <typeparam name="T">The type of the object that will be retrieved.</typeparam>
        /// <param name="path">The path where the object is stored.</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> used to deserialize the object. If
        ///                        this is null, default serialization settings will be used.</param>
        /// <returns>The object or default if the file is empty or an exception occurs.</returns>
        public static async Task<T> TryReadAsync<T>(string path, JsonSerializerSettings settings)
        {
            if (path is null)
                return default;

            var semaphore = GetSemaphore(path);
            try
            {
                await semaphore.WaitAsync().ConfigureAwait(false);
                return await ReadAsync<T>(path, settings).ConfigureAwait(false);
            }
            catch
            {
                return default;
            }
            finally
            {
                semaphore.Release();
            }
        }

        /// <summary>
        /// Try to read an object from the specified path.
        /// </summary>
        /// <typeparam name="T">The type of the object that will be retrieved.</typeparam>
        /// <param name="path">The path where the object is stored.</param>
        /// <returns>The object or default if the file is empty or an exception occurs.</returns>
        public static async Task<T> TryReadAsync<T>(string path) =>
            await TryReadAsync<T>(path, null).ConfigureAwait(false); 

        #endregion

        #region Helpers

        /// <summary>
        /// Gets the <see cref="SemaphoreSlim"/> of the given key.
        /// </summary>
        /// <param name="key">The key to get the semaphore.</param>
        /// <returns></returns>
        private static SemaphoreSlim GetSemaphore(string key) => filesSamphore.GetOrAdd(key, new SemaphoreSlim(1));

        #endregion
    }
}
