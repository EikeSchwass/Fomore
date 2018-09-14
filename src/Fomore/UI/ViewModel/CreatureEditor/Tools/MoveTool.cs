using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Core;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class MoveTool : Tool
    {
        private Point? StartPosition { get; set; }

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

        private Dictionary<JointVM, Vector2> StartPositions { get; } = new Dictionary<JointVM, Vector2>();

        /// <inheritdoc />
        public override bool OnCanvasMouseDown(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            base.OnCanvasMouseDown(mouseInfo, CanvasVM, modifierKeys);

            StartPositions.Clear();

            var selectedJoint = (from joint in CanvasVM.Creature.CreatureStructureVM.JointCollectionVM
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
            StartPosition = mouseInfo.RelativePosition;

            foreach (var jointVM in
                CanvasVM.Creature.CreatureStructureVM.JointCollectionVM.Where(j => selectedJoins.Contains(j.Model.Tracker)))
            {
                CanvasVM.SelectedJoints.Add(jointVM);
                StartPositions.Add(jointVM, jointVM.Position);
            }

            return true;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseMove(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if (base.OnCanvasMouseMove(mouseInfo, canvasVM, modifierKeys))
                return true;
            if (StartPosition == null)
                return false;
            var delta = mouseInfo.RelativePosition - StartPosition.Value;
            foreach (var boneVM in CanvasVM.Creature.CreatureStructureVM.BoneCollectionVM)
            {
                boneVM.FirstJoint =
                    CanvasVM.Creature.CreatureStructureVM.JointCollectionVM.First(j => boneVM.FirstJoint.Model.Tracker == j.Model.Tracker);
                boneVM.SecondJoint =
                    CanvasVM.Creature.CreatureStructureVM.JointCollectionVM.First(j => boneVM.SecondJoint.Model.Tracker == j.Model.Tracker);
            }

            foreach (var jointVM in CanvasVM.SelectedJoints)
            {
                var pos = StartPositions[jointVM];
                pos = new Vector2(delta.X + pos.X, delta.Y + pos.Y);
                jointVM.Position = pos;
            }

            return true;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseUp(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            CanvasVM = CanvasVM ?? canvasVM;
            if (base.OnCanvasMouseMove(mouseInfo, canvasVM, modifierKeys))
                return true;

            CreateChangeOperation(canvasVM);

            Reset();
            return true;
        }

        [SuppressMessage("ReSharper", "ImplicitlyCapturedClosure")]
        private void CreateChangeOperation(CreatureStructureEditorCanvasVM canvasVM)
        {
            var startPositions = StartPositions.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            var currentPositions = new Dictionary<JointVM, Vector2>();
            var movedJoints = StartPositions.Keys;
            foreach (var jointVM in movedJoints)
            {
                currentPositions.Add(jointVM, jointVM.Position);
            }

            var changeOperation = new ChangeOperation(c =>
                                                      {
                                                          foreach (var movedJoint in movedJoints)
                                                          {
                                                              movedJoint.Position = currentPositions[movedJoint];
                                                          }
                                                      },
                                                      c =>
                                                      {
                                                          foreach (var movedJoint in movedJoints)
                                                          {
                                                              movedJoint.Position = startPositions[movedJoint];
                                                          }
                                                      });
            canvasVM.HistoryStack.AddOperation(changeOperation);
        }

        private void Reset()
        {
            StartPosition = null;
        }

        /// <inheritdoc />
        public override void OnCanvasMouseLeave(CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            base.OnCanvasMouseLeave(canvasVM, modifierKeys);
            if (StartPosition != null)
                CreateChangeOperation(canvasVM);
            StartPosition = null;
        }

        /// <inheritdoc />
        public override void OnDeselected()
        {
            base.OnDeselected();
            if (StartPosition != null && CanvasVM != null)
                CreateChangeOperation(CanvasVM);
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
    }
}