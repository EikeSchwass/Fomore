using System.Windows.Media;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools {
    public class SelectTool : Tool
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.MousePointer, Brushes.Black);

        /// <inheritdoc />
        public SelectTool(CreatureStructureEditorCanvasVM canvasVM) : base(canvasVM) { }
    }
}