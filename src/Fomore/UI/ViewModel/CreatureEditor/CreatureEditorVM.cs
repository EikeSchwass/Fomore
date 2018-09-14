// Eike Stein: Fomore/UI/CreatureEditorVM.cs (2018/06/12)

using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class CreatureEditorVM : ViewModelBase
    {
        public CreatureVM OriginalCreature { get; }

        public CreatureEditorPanelVM CreatureEditorPanelVM { get; }

        public CreatureEditorVM(CreatureVM creatureVM)
        {
            OriginalCreature = creatureVM;
            CreatureEditorPanelVM = new CreatureEditorPanelVM(OriginalCreature.Clone());
        }
    }
}