using System;
using System.Collections.Generic;
using Core;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.Helper
{
    public class HistoryStackVM<T> : ViewModelBase where T : ICloneable<T>
    {
        private LinkedList<T> History { get; } = new LinkedList<T>();

        private LinkedListNode<T> CurrentNode { get; set; }

        public T Current => CurrentNode.Value;

        public bool CanUndo => CurrentNode.Previous != null;
        public bool CanRedo => CurrentNode.Next != null;
        public T Original { get; }

        public DelegateCommand UndoCommand { get; }
        public DelegateCommand RedoCommand { get; }

        public HistoryStackVM(T original)
        {
            Original = original;
            History.AddFirst(original.Clone());
            CurrentNode = History.First;
            UndoCommand = new DelegateCommand(o => Undo(), o => CanUndo);
            RedoCommand = new DelegateCommand(o => Redo(), o => CanRedo);
        }

        public void NewEntry(T item)
        {
            History.AddAfter(CurrentNode ?? throw new InvalidOperationException(), item);
            CurrentNode = CurrentNode.Next;
            while (CurrentNode?.Next != null)
                History.Remove(CurrentNode.Next);
            NotifyChanges();
        }

        private void NotifyChanges()
        {
            OnPropertyChanged(nameof(CanRedo));
            OnPropertyChanged(nameof(CanUndo));
            OnPropertyChanged(nameof(Current));
            UndoCommand.OnCanExecuteChanged();
            RedoCommand.OnCanExecuteChanged();
        }

        public void Undo()
        {
            if (!CanUndo)
                throw new InvalidOperationException("Cant undo right now");
            CurrentNode = CurrentNode.Previous;
            NotifyChanges();
        }

        public void Redo()
        {
            if (!CanRedo)
                throw new InvalidOperationException("Cant redo right now");
            CurrentNode = CurrentNode.Next;
            NotifyChanges();
        }
    }
}