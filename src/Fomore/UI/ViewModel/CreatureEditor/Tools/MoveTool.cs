using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Core;
using Fomore.UI.ViewModel.Commands;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class MoveTool : Tool
    {
        private Point? LastPosition { get; set; }

        public CreatureStructureEditorCanvasVM CanvasVM { get; set; }

        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Arrows, Brushes.Black);

        /// <inheritdoc />
        public override ToolType ToolType { get; } = ToolType.SelectionTool;

        /// <inheritdoc />
        public override InputGesture InputGesture { get; } = new KeyGesture(Key.M, ModifierKeys.Control);

        public double SinglePointSelectionTolerance { get; } = 5; //px

        /// <inheritdoc />
        public override string ToString() => "Move Joints";

        /// <inheritdoc />
        public override bool OnCanvasMouseDown(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            base.OnCanvasMouseDown(mouseInfo, CanvasVM, modifierKeys);

            var currentCreature = CanvasVM.HistoryStack.Current.Clone();

            var selectedJoint = (from joint in CanvasVM.HistoryStack?.Current?.CreatureStructureVM?.JointCollectionVM
                                 let distance =
                                     (joint.Position - new Vector2(mouseInfo.RelativePosition.X, mouseInfo.RelativePosition.Y)).Length
                                 where distance < SinglePointSelectionTolerance
                                 orderby distance
                                 select joint).FirstOrDefault();
            if (selectedJoint != null)
            {
                if (CanvasVM.SelectedJoints.Count >= 1 && !CanvasVM.SelectedJoints.Contains(selectedJoint))
                    CanvasVM.SelectedJoints.Clear();
                if (!CanvasVM.SelectedJoints.Contains(selectedJoint))
                    CanvasVM.SelectedJoints.Add(selectedJoint);
            }
            else
                CanvasVM.SelectedJoints.Clear();

            var selectedJoins = CanvasVM.SelectedJoints.Select(j => j.Model.Tracker).ToList();
            LastPosition = mouseInfo.RelativePosition;
            CanvasVM.HistoryStack.NewEntry(currentCreature);

            foreach (var jointVM in
                CanvasVM.HistoryStack.Current.CreatureStructureVM.JointCollectionVM.Where(j => selectedJoins.Contains(j.Model.Tracker)))
                CanvasVM.SelectedJoints.Add(jointVM);
            return true;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseMove(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if (base.OnCanvasMouseMove(mouseInfo, canvasVM, modifierKeys))
                return true;
            if (LastPosition == null)
                return false;
            var delta = mouseInfo.RelativePosition - LastPosition.Value;
            LastPosition = mouseInfo.RelativePosition;
            foreach (var boneVM in CanvasVM.HistoryStack.Current.CreatureStructureVM.BoneCollectionVM)
            {
                boneVM.FirstJoint =
                    CanvasVM.HistoryStack.Current.CreatureStructureVM.JointCollectionVM.First(j => boneVM.FirstJoint.Model.Tracker ==
                                                                                                   j.Model.Tracker);
                boneVM.SecondJoint =
                    CanvasVM.HistoryStack.Current.CreatureStructureVM.JointCollectionVM.First(j => boneVM.SecondJoint.Model.Tracker ==
                                                                                                   j.Model.Tracker);
            }

            foreach (var jointVM in CanvasVM.SelectedJoints) jointVM.Position += new Vector2(delta.X, delta.Y);

            return true;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseUp(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if (base.OnCanvasMouseMove(mouseInfo, canvasVM, modifierKeys))
                return true;
            Reset();
            return true;
        }

        private void Reset()
        {
            LastPosition = null;
        }

        /// <inheritdoc />
        public override void OnCanvasMouseLeave(CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            base.OnCanvasMouseLeave(canvasVM, modifierKeys);
            LastPosition = null;
        }
    }
}