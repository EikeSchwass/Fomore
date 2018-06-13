using Fomore.UI.ViewModel.CreatureEditor.Tools;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class CreatureEditorPanelVM
    {
        public CreatureVM CreatureVM { get; }
        public ToolCollectionVM ToolCollectionVM { get; }
        public CreatureStructureEditorCanvasVM CreatureStructureEditorCanvasVM { get; }

        public CreatureEditorPanelVM(CreatureVM creatureVM)
        {
            CreatureVM = creatureVM;
            ToolCollectionVM = new ToolCollectionVM();
            CreatureStructureEditorCanvasVM = new CreatureStructureEditorCanvasVM(CreatureVM, ToolCollectionVM);
            ToolCollectionVM.Tools.Add(new SelectTool(CreatureStructureEditorCanvasVM));
            ToolCollectionVM.Tools.Add(new PanTool(CreatureStructureEditorCanvasVM));
            ToolCollectionVM.Tools.Add(new PlaceJointTool(CreatureStructureEditorCanvasVM));
            ToolCollectionVM.Tools.Add(new PlaceBoneTool(CreatureStructureEditorCanvasVM));
        }
    }
}