using System.Windows.Media;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class SelectAllTool : Tool
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.MousePointer, Brushes.Black);

        /// <inheritdoc />
        public override ToolType ToolType { get; } = ToolType.SelectionTool;

        /// <inheritdoc />
        public SelectAllTool(CreatureStructureEditorCanvasVM canvasVM) : base(canvasVM) { }
    }
}