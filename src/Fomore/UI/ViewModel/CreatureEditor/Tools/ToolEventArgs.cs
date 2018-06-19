using System;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class ToolEventArgs : EventArgs
    {
        public Tool Tool { get; }
        public CreatureStructureEditorCanvasVM CanvasVM { get; }

        public ToolEventArgs(Tool tool, CreatureStructureEditorCanvasVM canvasVM)
        {
            Tool = tool;
            CanvasVM = canvasVM;
        }
    }
}