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
                var jointVM = (from joint in CanvasVM.HistoryStack?.Current?.CreatureStructureVM?.JointCollectionVM
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
                var boneVM = (from bone in CanvasVM.HistoryStack?.Current?.CreatureStructureVM?.BoneCollectionVM
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
            Reset();

            var creatureStructureVM = CanvasVM.HistoryStack.Current.CreatureStructureVM;
            var jointsInRectangle =
                creatureStructureVM.JointCollectionVM.Where(jointVM => jointVM.Position.IsInsideRect(CanvasVM.SelectionVM.Rectangle));
            var bonesInRectangle =
                creatureStructureVM.BoneCollectionVM.Where(boneVM =>
                                                               boneVM.FirstJoint.Position.IsInsideRect(CanvasVM.SelectionVM.Rectangle) ||
                                                               boneVM.SecondJoint.Position.IsInsideRect(CanvasVM.SelectionVM.Rectangle) ||
                                                               CanvasVM.SelectionVM.Rectangle.IsBoneInside(boneVM.Model));
            foreach (var jointVM in jointsInRectangle)
                CanvasVM.SelectedJoints.Add(jointVM);
            foreach (var boneVM in bonesInRectangle)
                CanvasVM.SelectedBones.Add(boneVM);
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
        public override void OnDeselected()
        {
            base.OnDeselected();
            //Reset();
        }
    }
}