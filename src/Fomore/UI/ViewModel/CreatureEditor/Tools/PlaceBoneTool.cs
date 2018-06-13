using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools {
    public class PlaceBoneTool : Tool
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = new BitmapImage(new Uri("/assets/images/bone.png", UriKind.Relative));

        /// <inheritdoc />
        public PlaceBoneTool(CreatureStructureEditorCanvasVM canvasVM) : base(canvasVM) { }
    }
}