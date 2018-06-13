using System.Windows;
using System.Windows.Media;
using Fomore.UI.ViewModel.Commands;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class PanTool : Tool
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.HandGrabOutline, Brushes.Black);

        private Point StartPosition { get; set; }
        protected bool IsDragging { get; private set; }

        /// <inheritdoc />
        public PanTool(CreatureStructureEditorCanvasVM canvasVM) : base(canvasVM) { }

        /// <inheritdoc />
        public override bool OnCanvasMouseMove(MouseInfo mouseInfo)
        {
            if (!IsDragging) return true;

            double deltaX = (mouseInfo.AbosultePosition - StartPosition).X;
            double deltaY = (mouseInfo.AbosultePosition - StartPosition).Y;
            StartPosition = mouseInfo.AbosultePosition;

            CanvasVM.CameraVM.OffsetX += deltaX;
            CanvasVM.CameraVM.OffsetY += deltaY;

            return true;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseDown(MouseInfo mouseInfo)
        {
            IsDragging = true;
            StartPosition = mouseInfo.AbosultePosition;

            return false;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseUp(MouseInfo mouseInfo)
        {
            IsDragging = false;
            return false;
        }

        /// <inheritdoc />
        public override void OnCanvasMouseLeave()
        {
            OnCanvasMouseUp(default(MouseInfo));
        }
    }
}