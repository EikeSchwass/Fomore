using System;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools {
    public class ToolEventArgs : EventArgs
    {
        public Tool Tool { get; }

        public ToolEventArgs(Tool tool)
        {
            Tool = tool;
        }
    }
}