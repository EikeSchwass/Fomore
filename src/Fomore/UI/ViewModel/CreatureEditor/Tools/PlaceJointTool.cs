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
        private CreatureStructureEditorCanvasVM CanvasVM { get; set; }

        /// <inheritdoc />
        public override ImageSource Image { get; } = new BitmapImage(new Uri("/assets/images/joint.png", UriKind.Relative));

        /// <inheritdoc />
        public override ToolType ToolType { get; } = ToolType.PlacementTool;

        private PanTool PanTool { get; } = new PanTool();

        /// <inheritdoc />
        public override bool OnCanvasMouseDown(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if (mouseInfo.MiddleMouseButtonDown)
                return PanTool.OnCanvasMouseDown(mouseInfo, canvasVM);

            var jointVM = new JointVM(new Joint { Position = canvasVM.PreviewJoint.Position });
            var creatureVM = canvasVM.HistoryStack.Current.Clone();
            creatureVM.CreatureStructureVM.JointCollectionVM.Add(jointVM);
            canvasVM.HistoryStack.NewEntry(creatureVM);

            return true;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseUp(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            PanTool.OnCanvasMouseUp(mouseInfo, canvasVM);
            return false;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseMove(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            PanTool.OnCanvasMouseMove(mouseInfo, canvasVM);
            canvasVM.PreviewJoint.Visibility = Visibility.Visible;
            canvasVM.PreviewJoint.Position = new Vector2(mouseInfo.RelativePosition.X - canvasVM.PreviewJoint.JointSize / 2, mouseInfo.RelativePosition.Y - canvasVM.PreviewJoint.JointSize / 2);
            return false;
        }

        /// <inheritdoc />
        public override void OnDeselected()
        {
            if (CanvasVM != null)
                CanvasVM.PreviewJoint.Visibility = Visibility.Hidden;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseWheel(MouseWheelInfo mouseWheelInfo, CreatureStructureEditorCanvasVM canvasVM)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            bool result = base.OnCanvasMouseWheel(mouseWheelInfo, canvasVM);
            canvasVM.PreviewJoint.Visibility = Visibility.Hidden;
            return result;
        }

        /// <inheritdoc />
        public override void OnCanvasMouseLeave(CreatureStructureEditorCanvasVM canvasVM)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            canvasVM.PreviewJoint.Visibility = Visibility.Hidden;
            PanTool.OnCanvasMouseLeave(canvasVM);
        }

        /// <inheritdoc />
        public override void OnCanvasMouseEnter(CreatureStructureEditorCanvasVM canvasVM)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            canvasVM.PreviewJoint.Visibility = Visibility.Visible;
            PanTool.OnCanvasMouseEnter(canvasVM);
        }
    }
}