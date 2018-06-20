using System.Linq;
using System.Windows;
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

        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.MousePointer, Brushes.Black);

        /// <inheritdoc />
        public override ToolType ToolType { get; } = ToolType.SelectionTool;

        public CreatureStructureEditorCanvasVM CanvasVM { get; set; }

        private bool IsSelecting { get; set; }

        /// <inheritdoc />
        public override bool OnCanvasMouseDown(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            base.OnCanvasMouseDown(mouseInfo, canvasVM);

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
        public override void OnCanvasMouseLeave(CreatureStructureEditorCanvasVM canvasVM)
        {
            base.OnCanvasMouseLeave(canvasVM);
            IsSelecting = false;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseMove(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if (base.OnCanvasMouseMove(mouseInfo, canvasVM))
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
        public override bool OnCanvasMouseUp(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM)
        {
            base.OnCanvasMouseUp(mouseInfo, canvasVM);
            if (mouseInfo.LeftMouseButtonDown == false)
                return false;

            if (IsSelecting)
            {
                double length = new Vector2(CanvasVM.SelectionVM.Width, CanvasVM.SelectionVM.Height).Length;
                if (length < MinSelectionDiagonale)
                    CanvasVM.SelectionVM.Visibility = Visibility.Hidden;
                else
                    SelectElementsInSelectionArea(canvasVM);

                IsSelecting = false;
                return true;
            }

            return false;
        }

        private void SelectElementsInSelectionArea(CreatureStructureEditorCanvasVM canvasVM)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            CanvasVM.SelectionVM.Visibility = Visibility.Hidden;
            CanvasVM.SelectedJoints.Clear();
            CanvasVM.SelectedBones.Clear();

            var creatureStructureVM = CanvasVM.HistoryStack.Current.CreatureStructureVM;
            var jointsInRectangle =
                creatureStructureVM.JointCollectionVM.Where(jointVM => jointVM.Position.IsInsideRect(CanvasVM.SelectionVM.Rectangle));
            var bonesInRectangle =
                creatureStructureVM.BoneCollectionVM.Where(boneVM =>
                                                               boneVM.FirstJoint.Position.IsInsideRect(CanvasVM.SelectionVM.Rectangle) ||
                                                               boneVM.SecondJoint.Position.IsInsideRect(CanvasVM.SelectionVM.Rectangle));
            foreach (var jointVM in jointsInRectangle)
                CanvasVM.SelectedJoints.Add(jointVM);
            foreach (var boneVM in bonesInRectangle)
                CanvasVM.SelectedBones.Add(boneVM);
        }
    }
}