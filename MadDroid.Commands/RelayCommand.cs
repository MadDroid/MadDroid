using System;
using System.Windows.Input;

namespace MadDroid.Commands
{
    /// <summary>
    /// A basic command that run an Action
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Fields

        /// <summary>
        /// The action to run
        /// </summary>
        readonly Action action;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action action)
        {
            this.action = action;
        }

        #endregion

        #region Interface implementation
        /// <summary>
        /// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// A relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Executes the commands Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) => action();
        #endregion
    }
}
