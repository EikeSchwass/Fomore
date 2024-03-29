﻿// Eike Stein: Fomore/UI/DelegateCommand.cs (2018/05/13)

using System;
using System.Windows.Input;

namespace Fomore.UI.ViewModel.Commands
{
    /// <summary>
    ///     This class works as a command that executes the action that is passed to the constructur, instead of a hard coded
    ///     method. Should be used instead of inheriting from <see cref="ICommand" /> over and over again.
    /// </summary>
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
        public virtual bool CanExecute(object parameter) => CanExecuteDelegate?.Invoke(parameter) ?? false;

        /// <inheritdoc />
        public virtual void Execute(object parameter) => ExecutionDelegate?.Invoke(parameter);

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Call this method to invoke the <see cref="CanExecuteChanged" /> event and trigger an reevaluation of the
        ///     <see cref="CanExecuteDelegate" />.
        /// </summary>
        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());
    }

    public class DelegateCommand<T> : DelegateCommand
    {
        public DelegateCommand(Action<T> executionDelegate, Func<T, bool> canExecuteDelegate) : base(o => executionDelegate((T)o),
                                                                                                     o => canExecuteDelegate((T)o)) { }

        /// <inheritdoc />
        public override bool CanExecute(object parameter) => CanExecute((T)parameter);

        public override void Execute(object parameter) => ExecutionDelegate?.Invoke((T)parameter);

        public bool CanExecute(T parameter) => CanExecuteDelegate?.Invoke(parameter) ?? false;

        public void Execute(T parameter) => ExecutionDelegate?.Invoke(parameter);
    }
}