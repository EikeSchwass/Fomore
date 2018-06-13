using System;
using System.Windows.Input;

namespace Fomore.UI.ViewModel.Commands
{
    /// <summary>
    /// In contrast to <see cref="DelegateCommand{T}"/> this command class has a boolean return value for the execute method. This return value signals that the command is handled or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DelegateHandleCommand<T> : ICommand
    {
        public Func<T, bool> ExecutionDelegate { get; }
        public Func<T, bool> CanExecuteDelegate { get; }

        public DelegateHandleCommand(Func<T, bool> executionDelegate, Func<T, bool> canExecuteDelegate)
        {
            ExecutionDelegate = executionDelegate;
            CanExecuteDelegate = canExecuteDelegate;
        }

        bool ICommand.CanExecute(object parameter) => CanExecute((T)parameter);
        void ICommand.Execute(object parameter) => ExecutionDelegate?.Invoke((T)parameter);

        public bool CanExecute(T parameter) => CanExecuteDelegate?.Invoke(parameter) ?? false;

        public bool Execute(T parameter) => ExecutionDelegate?.Invoke(parameter) == true;

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Call this method to invoke the <see cref="CanExecuteChanged" /> event and trigger an reevaluation of the
        ///     <see cref="CanExecuteDelegate" />.
        /// </summary>
        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}