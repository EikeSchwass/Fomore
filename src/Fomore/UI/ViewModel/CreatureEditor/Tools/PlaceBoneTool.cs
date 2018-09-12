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
        public override InputGesture InputGesture { get; } = new KeyGesture(Key.B, ModifierKeys.Control);

        /// <inheritdoc />
        public override bool OnCanvasMouseDown(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if (base.OnCanvasMouseDown(mouseInfo, canvasVM, modifierKeys))
                return true;

            return true;
        }

        /// <inheritdoc />
        public override void OnSelected()
        {
            var shortCut =
                new InfoMessage("You can quickly switch between tools via their shortcuts. Just hover over the icons to see them",
                                TimeSpan.FromSeconds(10));
            if (!InfoMessageCollection.HasShownMessage(shortCut))
                InfoMessageCollection.AddInfoMessage(shortCut);
        }

        private void PlaceBone(JointVM firstJoint, JointVM secondJoint)
        {
            var bone = new Bone(firstJoint.Model, secondJoint.Model);
            var boneVM = new BoneVM(bone);
            if (CanvasVM.HistoryStack.Current.CreatureStructureVM.BoneCollectionVM.Any(b =>
                                                                    {
                                                                        if (b.FirstJoint.Model.Tracker == firstJoint.Model.Tracker &&
                                                                            b.SecondJoint.Model.Tracker == secondJoint.Model.Tracker)
                                                                            return true;
                                                                        if (b.FirstJoint.Model.Tracker == secondJoint.Model.Tracker &&
                                                                            b.SecondJoint.Model.Tracker == firstJoint.Model.Tracker)
                                                                            return true;
                                                                        return false;
                                                                    }))
            {
                var infoMessage = new InfoMessage("Bone already placed between those joints", TimeSpan.FromSeconds(2), Brushes.Red);
                InfoMessageCollection.AddInfoMessageWithoutTracking(infoMessage);
                return;
            }
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
            if (base.OnCanvasMouseUp(mouseInfo, canvasVM, modifierKeys))
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
            {
                if (FirstJoint != null)
                {
                    var infoMessage = new InfoMessage("To cancel the bone placement just right click anywhere on the canvas", TimeSpan.FromSeconds(5));
                    if (!InfoMessageCollection.InfoMessages.Any(m => string.Equals(m.Message, infoMessage.Message, StringComparison.Ordinal)))
                        InfoMessageCollection.AddInfoMessageWithoutTracking(infoMessage);
                }
                else
                {
                    var infoMessage = new InfoMessage("To place a bone, first place joints with the respective tool and then connect them with this tool", TimeSpan.FromSeconds(5));
                    if (!InfoMessageCollection.InfoMessages.Any(m => string.Equals(m.Message, infoMessage.Message, StringComparison.Ordinal)))
                        InfoMessageCollection.AddInfoMessageWithoutTracking(infoMessage);
                }
                return true;
            }

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
            if (base.OnCanvasMouseMove(mouseInfo, canvasVM, modifierKeys))
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
        public override bool OnCanvasMouseWheel(MouseWheelInfo mouseWheelInfo,
                                                CreatureStructureEditorCanvasVM canvasVM,
                                                ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            bool result = base.OnCanvasMouseWheel(mouseWheelInfo, canvasVM, modifierKeys);
            canvasVM.PreviewBone.Visibility = Visibility.Hidden;
            return result;
        }

        /// <inheritdoc />
        public override void OnCanvasMouseLeave(CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            base.OnCanvasMouseLeave(canvasVM, modifierKeys);
            canvasVM.PreviewBone.Visibility = Visibility.Hidden;
        }

        /// <inheritdoc />
        public override void OnCanvasMouseEnter(CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            base.OnCanvasMouseEnter(canvasVM, modifierKeys);
        }

        public void Reset()
        {
            FirstJoint = null;
            if (CanvasVM == null) return;
            CanvasVM.PreviewBone.Visibility = Visibility.Hidden;
        }
    }
}