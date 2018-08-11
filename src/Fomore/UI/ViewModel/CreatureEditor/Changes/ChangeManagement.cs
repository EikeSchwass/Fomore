// : Fomore/UI/ChangeManagement.cs (2018/08/11)

using System.Windows.Input;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.CreatureEditor.Changes
{
    public class ChangeManagementVM : ViewModelBase
    {
        private ChangeStackVM ChangeStack { get; }

        public SelectionVM Selection { get; }

        public CreatureVM Creature { get; }

        public DelegateCommand RedoCommand { get; }
        public DelegateCommand UndoCommand { get; }
        

        public ChangeManagementVM(ChangeStackVM changeStackVM, SelectionVM selectionVM, CreatureVM creatureVM)
        {
            ChangeStack = changeStackVM;
            Selection = selectionVM;
            Creature = creatureVM;

            RedoCommand = ChangeStack.RedoCommand;
            UndoCommand = ChangeStack.UndoCommand;
        }

        public void PerformChange(IChange change)
        {
            ChangeStack.NewEntry(change);
        }
    }
}
