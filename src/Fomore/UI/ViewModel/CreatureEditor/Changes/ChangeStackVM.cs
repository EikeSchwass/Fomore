// : Fomore/UI/ChangeStack.cs (2018/08/09)

using System;
using System.Collections.Generic;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.CreatureEditor.Changes
{
    public class ChangeStackVM : ViewModelBase
    {
        private LinkedList<IChange> History { get; } = new LinkedList<IChange>();

        private LinkedListNode<IChange> CurrentNode { get; set; }

        public IChange Current => CurrentNode.Value;

        public bool CanUndo => CurrentNode.Previous != null;
        public bool CanRedo => CurrentNode.Next != null;

        public DelegateCommand UndoCommand { get; }
        public DelegateCommand RedoCommand { get; }

        public ChangeStackVM()
        {
            History.AddFirst(new StubChange());
            CurrentNode = History.First;
            UndoCommand = new DelegateCommand(o => Undo(), o => CanUndo);
            RedoCommand = new DelegateCommand(o => Redo(), o => CanRedo);
        }

        public void NewEntry(IChange item)
        {
            History.AddAfter(CurrentNode ?? throw new InvalidOperationException(), item);
            CurrentNode = CurrentNode.Next;
            while (CurrentNode?.Next != null)
                History.Remove(CurrentNode.Next);
            CurrentNode?.Value.Apply();
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
            CurrentNode.Value.Undo();
            CurrentNode = CurrentNode.Previous;
            NotifyChanges();
        }

        public void Redo()
        {
            if (!CanRedo)
                throw new InvalidOperationException("Cant redo right now");
            CurrentNode = CurrentNode.Next;
            CurrentNode?.Value.Apply();
            NotifyChanges();
        }
    }
}