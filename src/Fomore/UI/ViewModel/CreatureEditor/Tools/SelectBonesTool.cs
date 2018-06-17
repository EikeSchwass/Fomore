using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class SelectBonesTool : SelectAllTool
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = new BitmapImage(new Uri("/assets/images/arrowselectionbones.png", UriKind.Relative));

        /// <inheritdoc />
        public SelectBonesTool(CreatureStructureEditorCanvasVM canvasVM) : base(canvasVM) { }
    }
}