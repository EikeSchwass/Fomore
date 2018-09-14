using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class PlaceJointTool : Tool
    {
        private CreatureStructureEditorCanvasVM CanvasVM { get; set; }

        /// <inheritdoc />
        public override ImageSource Image { get; } = new BitmapImage(new Uri("/assets/images/joint.png", UriKind.Relative));

        /// <inheritdoc />
        public override ToolType ToolType { get; } = ToolType.PlacementTool;

        /// <inheritdoc />
        public override InputGesture InputGesture { get; } = new KeyGesture(Key.J, ModifierKeys.Control);

        /// <inheritdoc />
        public override bool OnCanvasMouseDown(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if (base.OnCanvasMouseDown(mouseInfo, canvasVM, modifierKeys))
                return true;

            var jointVM = new JointVM(new Joint { Position = canvasVM.PreviewJoint.Position });
            var changeOperation = new ChangeOperation(c => c.Creature.CreatureStructureVM.JointCollectionVM.Add(jointVM),
                                                      c => c.Creature.CreatureStructureVM.JointCollectionVM.Remove(jointVM));
            canvasVM.HistoryStack.AddOperation(changeOperation);
            return true;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseUp(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if (base.OnCanvasMouseUp(mouseInfo, canvasVM, modifierKeys))
                return true;
            return false;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseMove(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if (base.OnCanvasMouseMove(mouseInfo, canvasVM, modifierKeys))
                return true;
            canvasVM.PreviewJoint.Visibility = Visibility.Visible;
            canvasVM.PreviewJoint.Position = new Vector2(mouseInfo.RelativePosition.X, mouseInfo.RelativePosition.Y);
            return false;
        }

        /// <inheritdoc />
        public override void OnDeselected()
        {
            if (CanvasVM != null)
                CanvasVM.PreviewJoint.Visibility = Visibility.Hidden;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseWheel(MouseWheelInfo mouseWheelInfo,
                                                CreatureStructureEditorCanvasVM canvasVM,
                                                ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            bool result = base.OnCanvasMouseWheel(mouseWheelInfo, canvasVM, modifierKeys);
            canvasVM.PreviewJoint.Visibility = Visibility.Hidden;
            return result;
        }

        /// <inheritdoc />
        public override void OnCanvasMouseLeave(CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            base.OnCanvasMouseLeave(canvasVM, modifierKeys);
            canvasVM.PreviewJoint.Visibility = Visibility.Hidden;
        }

        /// <inheritdoc />
        public override void OnCanvasMouseEnter(CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            base.OnCanvasMouseEnter(canvasVM, modifierKeys);
            canvasVM.PreviewJoint.Visibility = Visibility.Visible;
        }
    }
}