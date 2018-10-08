using Microsoft.Extensions.Logging;

namespace MadDroid.DependencyInjection.Logging
{
    /// <summary>
    /// Extensions methods for the <see cref="FileLogger"/>
    /// </summary>
    public static class FileLoggerExtensions
    {
        /// <summary>
        /// Adds a new file logger to the specific path
        /// </summary>
        /// <param name="builder">The log builder to add to</param>
        /// <param name="path">The path where to write to</param>
        /// <returns></returns>
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string path, FileLoggerConfiguration configuration = null)
        {
            // Add file log provider to builder
            builder.AddProvider(new FileLoggerProvider(path, configuration ?? new FileLoggerConfiguration()));

            // Return the builder
            return builder;
        }
    }
}
