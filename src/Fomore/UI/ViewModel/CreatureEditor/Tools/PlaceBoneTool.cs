using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class PlaceBoneTool : Tool
    {
        public const double DistanceThreshold = 16;

        public override ImageSource Image { get; } = new BitmapImage(new Uri("/assets/images/bone.png", UriKind.Relative));

        public override ToolType ToolType { get; } = ToolType.PlacementTool;

        private CreatureStructureEditorCanvasVM CanvasVM { get; set; }

        private JointVM FirstJoint { get; set; }

        /// <inheritdoc />
        public override bool OnCanvasMouseDown(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if (base.OnCanvasMouseDown(mouseInfo, canvasVM,  modifierKeys))
                return true;



            return true;
        }

        private void PlaceBone(JointVM firstJoint, JointVM secondJoint)
        {
            var bone = new Bone { FirstJoint = firstJoint.Model.Clone(), SecondJoint = secondJoint.Model.Clone() };
            var boneVM = new BoneVM(bone);
            var creatureVM = CanvasVM.HistoryStack.Current.Clone();
            creatureVM.CreatureStructureVM.BoneCollectionVM.Add(boneVM);
            CanvasVM.HistoryStack.NewEntry(creatureVM);
            Reset();
            FirstJoint = secondJoint;
            CanvasVM.PreviewBone.From = FirstJoint.Position;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseUp(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if (base.OnCanvasMouseUp(mouseInfo, canvasVM,modifierKeys))
                return true;

            if (mouseInfo.RightMouseButtonDown)
            {
                bool block = FirstJoint != null;
                Reset();
                return block;
            }

            var mousePosition = new Vector2(mouseInfo.RelativePosition.X, mouseInfo.RelativePosition.Y);
            var jointCollectionVM = canvasVM.HistoryStack.Current.CreatureStructureVM.JointCollectionVM;
            var closestJointInRange =
                (from jointVM in jointCollectionVM
                 let distance = (jointVM.Position - mousePosition).Length
                 where distance < DistanceThreshold
                 orderby distance
                 select jointVM).FirstOrDefault();

            if (closestJointInRange == null)
                return true;

            canvasVM.PreviewBone.To = mousePosition;
            if (FirstJoint == null)
            {
                FirstJoint = closestJointInRange;

                canvasVM.PreviewBone.From = FirstJoint.Position;
            }
            else
                PlaceBone(FirstJoint, closestJointInRange);

            return false;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseMove(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if (base.OnCanvasMouseMove(mouseInfo, canvasVM,modifierKeys))
                return true;

            var mousePosition = new Vector2(mouseInfo.RelativePosition.X, mouseInfo.RelativePosition.Y);
            var jointCollectionVM = canvasVM.HistoryStack.Current.CreatureStructureVM.JointCollectionVM;
            var closestJointInRange =
                (from jointVM in jointCollectionVM
                 let distance = (jointVM.Position - mousePosition).Length
                 where distance < DistanceThreshold
                 orderby distance
                 select jointVM).FirstOrDefault();

            foreach (var highlightedJoint in canvasVM.PreviewBone.HighlightedJoints.ToList())
            {
                if (!Equals(highlightedJoint, FirstJoint) && !Equals(closestJointInRange, highlightedJoint))
                    canvasVM.PreviewBone.HighlightedJoints.Remove(highlightedJoint);
            }

            if (closestJointInRange != null && !canvasVM.PreviewBone.HighlightedJoints.Contains(closestJointInRange))
                canvasVM.PreviewBone.HighlightedJoints.Add(closestJointInRange);

            if (FirstJoint == null) return false;
            canvasVM.PreviewBone.To = closestJointInRange?.Position ?? mousePosition;
            canvasVM.PreviewBone.Visibility = Visibility.Visible;

            return false;
        }

        /// <inheritdoc />
        public override void OnDeselected()
        {
            Reset();
        }

        /// <inheritdoc />
        public override InputGesture InputGesture { get; } = new KeyGesture(Key.B, ModifierKeys.Control);

        /// <inheritdoc />
        public override bool OnCanvasMouseWheel(MouseWheelInfo mouseWheelInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            bool result = base.OnCanvasMouseWheel(mouseWheelInfo, canvasVM,modifierKeys);
            canvasVM.PreviewBone.Visibility = Visibility.Hidden;
            return result;
        }

        /// <inheritdoc />
        public override void OnCanvasMouseLeave(CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            base.OnCanvasMouseLeave(canvasVM,modifierKeys);
            canvasVM.PreviewBone.Visibility = Visibility.Hidden;
        }

        /// <inheritdoc />
        public override void OnCanvasMouseEnter(CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            base.OnCanvasMouseEnter(canvasVM,modifierKeys);
        }

        public void Reset()
        {
            FirstJoint = null;
            if (CanvasVM == null) return;
            CanvasVM.PreviewBone.Visibility = Visibility.Hidden;
        }
    }
}