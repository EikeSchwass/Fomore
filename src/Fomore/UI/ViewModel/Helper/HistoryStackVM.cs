using System;
using System.Collections.Generic;
using System.Linq;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.Helper
{
    public class HistoryStackVM<T> : ViewModelBase
    {
        private List<IOperation<T>> History { get; } = new List<IOperation<T>>();
        private int CurrentIndex { get; set; }

        public bool CanUndo => CurrentIndex > 0;
        public bool CanRedo => CurrentIndex<History.Count;
        public T Entity { get; }

        public DelegateCommand UndoCommand { get; }
        public DelegateCommand RedoCommand { get; }

        public HistoryStackVM(T entity)
        {
            Entity = entity;
            UndoCommand = new DelegateCommand(o => Undo(), o => CanUndo);
            RedoCommand = new DelegateCommand(o => Redo(), o => CanRedo);
        }

        private void NotifyChanges()
        {
            OnPropertyChanged(nameof(CanRedo));
            OnPropertyChanged(nameof(CanUndo));
            OnPropertyChanged(nameof(Entity));
            UndoCommand.OnCanExecuteChanged();
            RedoCommand.OnCanExecuteChanged();
        }

        public void AddOperation(IOperation<T> operation)
        {
            operation.PerformOperation(Entity);
            History.Insert(CurrentIndex, operation);
            CurrentIndex++;
            var operations = History.Where((o, i) => i >= CurrentIndex).ToList();
            foreach (var operationToRemove in operations) History.Remove(operationToRemove);

            NotifyChanges();
        }

        public void Undo()
        {
            if (!CanUndo)
                throw new InvalidOperationException("Cant undo right now");

            CurrentIndex--;
            History[CurrentIndex].Undo(Entity);

            NotifyChanges();
        }

        public void Redo()
        {
            if (!CanRedo)
                throw new InvalidOperationException("Cant redo right now");
            History[CurrentIndex].PerformOperation(Entity);
            CurrentIndex++;
            NotifyChanges();
        }
    }
}