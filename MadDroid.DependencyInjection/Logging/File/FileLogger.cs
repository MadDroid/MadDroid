using System;
using System.Collections.Concurrent;
using System.IO;
using Microsoft.Extensions.Logging;

namespace MadDroid.DependencyInjection.Logging
{
    /// <summary>
    /// A logger that writes the logs to file
    /// </summary>
    public class FileLogger : ILogger
    {
        #region Static Properties

        /// <summary>
        /// A list of file locks based on path
        /// </summary>
        protected static ConcurrentDictionary<string, object> FileLocks = new ConcurrentDictionary<string, object>();

        #endregion

        #region Protected Members

        /// <summary>
        /// The category for this logger
        /// </summary>
        protected string categoryName;

        /// <summary>
        /// The file path to write to
        /// </summary>
        protected string filePath;

        /// <summary>
        /// The configuration to use
        /// </summary>
        protected FileLoggerConfiguration configuration;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="categoryName">The category of this logger</param>
        /// <param name="filePath">The file path to write to</param>
        /// <param name="configuration">The configuration to use</param>
        public FileLogger(string categoryName, string filePath, FileLoggerConfiguration configuration)
        {
            // Get absolute path
            filePath = Path.GetFullPath(filePath);

            // Set members
            this.categoryName = categoryName;
            this.filePath = filePath;
            this.configuration = configuration;
        }

        #endregion

        /// <summary>
        /// File loggers are not scoped so this is always null
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// Enabled if the log level is the same or greater than the configuration
        /// </summary>
        /// <param name="logLevel">The log level to check against</param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            // Enabled if the log level is greater or equal to what we want to log
            return logLevel >= configuration.LogLevel;
        }

        /// <summary>
        /// Logs the message to file
        /// </summary>
        /// <typeparam name="TState">The type of the details of the message</typeparam>
        /// <param name="logLevel">The log level</param>
        /// <param name="eventId">The id</param>
        /// <param name="state">The details of the message</param>
        /// <param name="exception">Any exception to log</param>
        /// <param name="formatter">The formatter for converting the state and exception to a message string</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // If we should not log...
            if (!IsEnabled(logLevel))
                // Return
                return;

            // Get current time
            string currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss");

            // Prepend the time to the log if desired
            string timeLogToString = configuration.LogTime ? $"[{currentTime}] " : "";

            // Get the formatted message string
            string message = formatter(state, exception);

            // Format the message
            var output = $"{timeLogToString}{message}{Environment.NewLine}";

            // Normalize and absolute the path
            var normalizedPath = filePath.ToUpper();

            // Get the file lock based on the absolute path
            var fileLock = FileLocks.GetOrAdd(normalizedPath, path => new object());

            // Lock the file
            lock (fileLock)
            {
                // Write the message to the file
                File.AppendAllText(filePath, output);
            }
        }
    }
}
