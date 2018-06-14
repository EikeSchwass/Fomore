using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class PlaceJointTool : Tool
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = new BitmapImage(new Uri("/assets/images/joint.png", UriKind.Relative));

        private PanTool PanTool { get; }

        /// <inheritdoc />
        public PlaceJointTool(CreatureStructureEditorCanvasVM canvasVM) : base(canvasVM)
        {
            PanTool = new PanTool(canvasVM);
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseDown(MouseInfo mouseInfo)
        {
            if (mouseInfo.MiddleMouseButtonDown)
                return PanTool.OnCanvasMouseDown(mouseInfo);

            var jointVM = new JointVM(new Joint {Position = new Vector2(mouseInfo.RelativePosition.X, mouseInfo.RelativePosition.Y)});
            CanvasVM.CreatureVM.CreatureStructureVM.JointCollectionVM.Add(jointVM);

            return true;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseUp(MouseInfo mouseInfo)
        {
            PanTool.OnCanvasMouseUp(mouseInfo);
            return false;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseMove(MouseInfo mouseInfo)
        {
            PanTool.OnCanvasMouseMove(mouseInfo);
            CanvasVM.PreviewJoint.Visibility = Visibility.Visible;
            CanvasVM.PreviewJoint.Position = new Vector2(mouseInfo.RelativePosition.X, mouseInfo.RelativePosition.Y);
            return false;
        }

        /// <inheritdoc />
        public override void OnSelected()
        {
            CanvasVM.PreviewJoint.Visibility = Visibility.Visible;
        }

        /// <inheritdoc />
        public override void OnDeselected()
        {
            CanvasVM.PreviewJoint.Visibility = Visibility.Hidden;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseWheel(MouseWheelInfo mouseWheelInfo)
        {
            bool result = base.OnCanvasMouseWheel(mouseWheelInfo);
            CanvasVM.PreviewJoint.Visibility = Visibility.Hidden;
            return result;
        }

        /// <inheritdoc />
        public override void OnCanvasMouseLeave()
        {
            CanvasVM.PreviewJoint.Visibility = Visibility.Hidden;
            PanTool.OnCanvasMouseLeave();
        }

        /// <inheritdoc />
        public override void OnCanvasMouseEnter()
        {
            CanvasVM.PreviewJoint.Visibility = Visibility.Visible;
            PanTool.OnCanvasMouseEnter();
        }
    }
}