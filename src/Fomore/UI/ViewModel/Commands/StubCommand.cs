// Eike Stein: Fomore/UI/StubCommand.cs (2018/05/12)

using System;
using System.Windows.Input;

namespace Fomore.UI.ViewModel.Commands
{
    /// <summary>
    /// Placeholder command that executed no code.
    /// </summary>
    public class StubCommand : ICommand
    {
        /// <inheritdoc />
        public bool CanExecute(object parameter) => true;

        /// <inheritdoc />
        public void Execute(object parameter) { }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;
    }
}