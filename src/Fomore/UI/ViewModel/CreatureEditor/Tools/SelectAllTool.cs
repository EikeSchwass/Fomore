using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class SelectAllTool : Tool
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = new BitmapImage(new Uri("/assets/images/arrowselectionall.png", UriKind.Relative));

        /// <inheritdoc />
        public override ToolType ToolType { get; } = ToolType.SelectionTool;

        /// <inheritdoc />
        public SelectAllTool(CreatureStructureEditorCanvasVM canvasVM) : base(canvasVM) { }
    }
}