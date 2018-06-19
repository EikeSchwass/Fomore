using System.Windows.Media;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class SelectBonesTool : SelectAllTool
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Expand, Brushes.Blue);

        /// <inheritdoc />
        public SelectBonesTool(CreatureStructureEditorCanvasVM canvasVM) : base(canvasVM) { }
    }
}