using System.Collections.ObjectModel;
using Fomore.UI.ViewModel.CreatureEditor.Behaviours;
using Fomore.UI.ViewModel.CreatureEditor.Tools;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class CreatureEditorPanelVM
    {
        public HistoryStackVM<CreatureVM> HistoryStack { get; }
        public ToolCollectionVM ToolCollectionVM { get; }
        public ObservableCollection<BaseBehaviour> Behaviours { get; }
        public CreatureStructureEditorCanvasVM CreatureStructureEditorCanvasVM { get; }

        public CreatureEditorPanelVM(HistoryStackVM<CreatureVM> historyStack)
        {
            HistoryStack = historyStack;
            ToolCollectionVM = new ToolCollectionVM();
            ToolCollectionVM.Tools.Add(new SelectAllTool());
            ToolCollectionVM.Tools.Add(new SelectJointsTool());
            ToolCollectionVM.Tools.Add(new SelectBonesTool());
            ToolCollectionVM.Tools.Add(new PanTool());
            ToolCollectionVM.Tools.Add(new PlaceJointTool());
            ToolCollectionVM.Tools.Add(new PlaceBoneTool());
            CreatureStructureEditorCanvasVM = new CreatureStructureEditorCanvasVM(HistoryStack, ToolCollectionVM);

            Behaviours = new ObservableCollection<BaseBehaviour>
            {
                new UndoBehaviour(),
                new RedoBehaviour(),
                new CopyBehaviour(),
                new CutBehaviour(),
                new PasteBehaviour(),
                new RotateLeftBehaviour(),
                new RotateRightBehaviour(),
                new FlipHorizontalBehaviour(),
                new FlipVeticalBehaviour(),
                new SaveBehaviour(),
                new DeleteBehaviour(),
                new ClearBehaviour()
            };
        }
    }
}