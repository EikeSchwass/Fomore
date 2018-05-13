// Eike Stein: Fomore/UI/DelegateCommand.cs (2018/05/13)

using System;
using System.Windows.Input;

namespace Fomore.UI.ViewModel.Commands
{
    public class DelegateCommand : ICommand
    {
        public Action<object> ExecutionDelegate { get; }
        public Func<object, bool> CanExecuteDelegate { get; }

        public DelegateCommand(Action<object> executionDelegate, Func<object, bool> canExecuteDelegate)
        {
            ExecutionDelegate = executionDelegate;
            CanExecuteDelegate = canExecuteDelegate;
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter) => CanExecuteDelegate?.Invoke(parameter) ?? false;

        /// <inheritdoc />
        public void Execute(object parameter) => ExecutionDelegate?.Invoke(parameter);

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}