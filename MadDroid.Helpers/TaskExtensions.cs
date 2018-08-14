using System.Threading.Tasks;

namespace MadDroid.Helpers
{
    /// <summary>
    /// Extensions for <see cref="Task"/>
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Handles a <see cref="Task"/> that don't need to be waited to be completed
        /// </summary>
        /// <param name="task"></param>
        /// <remarks>
        ///    This method allows you to call an async method without awaiting it.
        ///    Use it when you don't want or need to wait for the task to complete.
        /// </remarks>
        public static void FireAndForget(this Task task) { }
    }
}
