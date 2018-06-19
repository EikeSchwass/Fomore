// Eike Stein: Fomore/UI/CreatureEditorVM.cs (2018/06/12)

using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class CreatureEditorVM : ViewModelBase
    {
        public HistoryStackVM<CreatureVM> CreatureHistoryStackVM { get; }

        public CreatureEditorPanelVM CreatureEditorPanelVM { get; }

        public CreatureEditorVM(CreatureVM creatureVM)
        {
            CreatureHistoryStackVM=new HistoryStackVM<CreatureVM>(creatureVM);
            CreatureEditorPanelVM = new CreatureEditorPanelVM(CreatureHistoryStackVM);
        }
    }
}