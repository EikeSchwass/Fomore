using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Fomore.UI.ViewModel.Commands
{
    public class CompositeCommand : ICommand
    {
        public ReadOnlyCollection<ICommand> Commands { get; }

        public CompositeCommand(params ICommand[] commands)
        {
            var commandList = new List<ICommand>();
            foreach (var command in commands.Where(c => c != null))
            {
                command.CanExecuteChanged += (sender, eventArgs) => CanExecuteChanged?.Invoke(this, new EventArgs());
                commandList.Add(command);
            }

            Commands = commandList.AsReadOnly();
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter) => Commands.All(c => c.CanExecute(parameter));

        /// <inheritdoc />
        public void Execute(object parameter)
        {
            foreach (var command in Commands) command.Execute(parameter);
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;
    }
}