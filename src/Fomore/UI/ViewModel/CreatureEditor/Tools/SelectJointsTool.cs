using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class SelectJointsTool : SelectAllTool
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = new BitmapImage(new Uri("/assets/images/arrowselectionjoints.png", UriKind.Relative));

        /// <inheritdoc />
        public SelectJointsTool(CreatureStructureEditorCanvasVM canvasVM) : base(canvasVM) { }
    }
}