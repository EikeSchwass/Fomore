// Eike Stein: Fomore/UI/CreatureEditorVM.cs (2018/06/12)

using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class CreatureEditorVM : ViewModelBase
    {
        public CreatureVM CreatureVM { get; }

        public CreatureEditorPanelVM CreatureEditorPanelVM { get; }

        public CreatureEditorVM(CreatureVM creatureVM)
        {
            CreatureVM = creatureVM;
            CreatureEditorPanelVM = new CreatureEditorPanelVM(CreatureVM);
        }
    }
}