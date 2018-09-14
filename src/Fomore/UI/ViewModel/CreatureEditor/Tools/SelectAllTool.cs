using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Core;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Helper;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class SelectAllTool : Tool
    {
        private const double MinSelectionDiagonale = 4;
        private const double SinglePointSelectionTolerance = 5; //px

        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.MousePointer, Brushes.Black);

        /// <inheritdoc />
        public override ToolType ToolType { get; } = ToolType.SelectionTool;

        public CreatureStructureEditorCanvasVM CanvasVM { get; set; }

        private bool IsSelecting { get; set; }

        /// <inheritdoc />
        public override InputGesture InputGesture { get; } = new KeyGesture(Key.Q, ModifierKeys.Control);

        /// <inheritdoc />
        public override bool OnCanvasMouseDown(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            base.OnCanvasMouseDown(mouseInfo, canvasVM, modifierKeys);

            if (mouseInfo.MiddleMouseButtonDown)
                return false;

            var mousePosition = new Vector2(mouseInfo.RelativePosition.X, mouseInfo.RelativePosition.Y);
            if (!IsSelecting)
            {
                IsSelecting = true;
                CanvasVM.SelectionVM.StartPosition = mousePosition;
                CanvasVM.SelectionVM.Width = 0;
                CanvasVM.SelectionVM.Height = 0;
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public override void OnCanvasMouseLeave(CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            base.OnCanvasMouseLeave(canvasVM, modifierKeys);
            IsSelecting = false;
            CanvasVM.SelectionVM.Visibility = Visibility.Hidden;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseMove(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if (base.OnCanvasMouseMove(mouseInfo, canvasVM, modifierKeys))
                return true;

            var mousePosition = new Vector2(mouseInfo.RelativePosition.X, mouseInfo.RelativePosition.Y);

            if (IsSelecting)
            {
                var difference = mousePosition - CanvasVM.SelectionVM.StartPosition;
                CanvasVM.SelectionVM.Width = difference.X;
                CanvasVM.SelectionVM.Height = difference.Y;
                CanvasVM.SelectionVM.Visibility = difference.Length >= MinSelectionDiagonale ? Visibility.Visible : Visibility.Hidden;
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseUp(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            base.OnCanvasMouseUp(mouseInfo, canvasVM, modifierKeys);
            if (mouseInfo.LeftMouseButtonDown == false)
                return false;

            var mousePosition = new Vector2(mouseInfo.RelativePosition.X, mouseInfo.RelativePosition.Y);

            if (IsSelecting)
            {
                double length = new Vector2(CanvasVM.SelectionVM.Width, CanvasVM.SelectionVM.Height).Length;
                if (length < MinSelectionDiagonale)
                {
                    PointSelection(mousePosition, modifierKeys);
                    CanvasVM.SelectionVM.Visibility = Visibility.Hidden;
                }
                else
                    SelectElementsInSelectionArea(canvasVM, modifierKeys);

                IsSelecting = false;
                return true;
            }

            return false;
        }

        private void PointSelection(Vector2 relativePosition, ModifierKeys modifierKeys)
        {
            if ((modifierKeys & ModifierKeys.Shift) == 0)
                Reset();
            if (CanvasVM == null)
                return;
            CanvasVM.SelectionVM.Visibility = Visibility.Hidden;

            if ((modifierKeys & ModifierKeys.Alt) == 0)
            {
                var jointVM = (from joint in CanvasVM.Creature?.CreatureStructureVM?.JointCollectionVM
                               let distance = (joint.Position - relativePosition).Length
                               where distance < SinglePointSelectionTolerance
                               orderby distance
                               select joint).FirstOrDefault();
                if (jointVM != null)
                {
                    if (CanvasVM.SelectedJoints.Contains(jointVM))
                        CanvasVM.SelectedJoints.Remove(jointVM);
                    else
                        CanvasVM.SelectedJoints.Add(jointVM);
                    return;
                }
            }

            if ((modifierKeys & ModifierKeys.Control) == 0)
            {
                var boneVM = (from bone in CanvasVM.Creature?.CreatureStructureVM?.BoneCollectionVM
                              let distance = relativePosition.GetDistanceToBone(bone.Model)
                              where distance < SinglePointSelectionTolerance
                              orderby distance
                              select bone).FirstOrDefault();
                if (boneVM != null)
                {
                    if (CanvasVM.SelectedBones.Contains(boneVM))
                        CanvasVM.SelectedBones.Remove(boneVM);
                    else
                        CanvasVM.SelectedBones.Add(boneVM);
                }
            }
        }

        private void SelectElementsInSelectionArea(CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if ((modifierKeys & ModifierKeys.Shift) == 0)
                Reset();
            CanvasVM.SelectionVM.Visibility = Visibility.Hidden;

            var creatureStructureVM = CanvasVM.Creature.CreatureStructureVM;
            var jointsInRectangle =
                creatureStructureVM.JointCollectionVM.Where(jointVM => jointVM.Position.IsInsideRect(CanvasVM.SelectionVM.Rectangle));
            var bonesInRectangle =
                creatureStructureVM.BoneCollectionVM.Where(boneVM =>
                                                               boneVM.FirstJoint.Position.IsInsideRect(CanvasVM.SelectionVM.Rectangle) ||
                                                               boneVM.SecondJoint.Position.IsInsideRect(CanvasVM.SelectionVM.Rectangle) ||
                                                               CanvasVM.SelectionVM.Rectangle.IsBoneInside(boneVM.Model));
            if ((modifierKeys & ModifierKeys.Alt) == 0)
            {
                foreach (var jointVM in jointsInRectangle)
                {
                    if ((modifierKeys & ModifierKeys.Shift) == 0)
                        CanvasVM.SelectedJoints.Add(jointVM);
                    else
                    {
                        var joint = CanvasVM.SelectedJoints.FirstOrDefault(j => j.Model.Tracker == jointVM.Model.Tracker);
                        if (joint != null)
                            CanvasVM.SelectedJoints.Remove(joint);
                        else
                            CanvasVM.SelectedJoints.Add(jointVM);
                    }
                }
            }

            if ((modifierKeys & ModifierKeys.Control) == 0)
            {
                foreach (var boneVM in bonesInRectangle)
                {
                    if ((modifierKeys & ModifierKeys.Shift) == 0)
                        CanvasVM.SelectedBones.Add(boneVM);
                    else
                    {
                        var bone = CanvasVM.SelectedBones.FirstOrDefault(b => b.Model.Tracker == boneVM.Model.Tracker);
                        if (bone != null)
                            CanvasVM.SelectedBones.Remove(bone);
                        else
                            CanvasVM.SelectedBones.Add(boneVM);
                    }
                }
            }
        }

        private void Reset()
        {
            if (CanvasVM == null)
                return;
            CanvasVM.SelectionVM.Visibility = Visibility.Hidden;
            CanvasVM.SelectedJoints.Clear();
            CanvasVM.SelectedBones.Clear();
        }

        /// <inheritdoc />
        public override void OnSelected()
        {
            var jointMessage = new InfoMessage("By pressing the Ctrl-Key you will only select joints", TimeSpan.FromSeconds(10));
            var boneMessage = new InfoMessage("By pressing the Alt-Key you will only select bones", TimeSpan.FromSeconds(10));
            var shiftMessage = new InfoMessage("By pressing the Shift-Key you can select multiple elements", TimeSpan.FromSeconds(10));
            var panMessage = new InfoMessage("Hold down the middle mouse button to move the canvas around", TimeSpan.FromSeconds(10));
            if (!InfoMessageCollection.HasShownMessage(jointMessage))
            {
                InfoMessageCollection.AddInfoMessage(jointMessage);
            }

            if (!InfoMessageCollection.HasShownMessage(boneMessage))
            {
                InfoMessageCollection.AddInfoMessage(boneMessage);
            }

            if (!InfoMessageCollection.HasShownMessage(shiftMessage))
            {
                InfoMessageCollection.AddInfoMessage(shiftMessage);
            }

            if (!InfoMessageCollection.HasShownMessage(panMessage))
            {
                InfoMessageCollection.AddInfoMessage(panMessage);
            }
        }
    }
}