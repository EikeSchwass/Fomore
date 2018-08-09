﻿using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class FlipVerticalBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = new BitmapImage(new Uri("/assets/images/flipvertical.png", UriKind.Relative));

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = null;

        /// <inheritdoc />
        public override string ToString() => "Flip Vertical";

        /// <inheritdoc />
        public override void OnInvoked(CreatureEditorPanelVM parameter, ModifierKeys modifierKeys)
        {
            base.OnInvoked(parameter, modifierKeys);
            var creatureVM = parameter.HistoryStack.Current.Clone();

            var jointCollectionVM = creatureVM.CreatureStructureVM.JointCollectionVM;
            var center = new Vector2((jointCollectionVM.Max(j => j.Position.X) + jointCollectionVM.Min(j => j.Position.X)) / 2,
                                     (jointCollectionVM.Max(j => j.Position.Y) + jointCollectionVM.Min(j => j.Position.Y)) / 2);

            foreach (var jointVM in jointCollectionVM)
            {
                jointVM.Position = new Vector2(jointVM.Position.X, 2 * center.Y - jointVM.Position.Y);
            }

            // // Move Horizontically in view
            // {
            //     double min = jointCollectionVM.Min(j => j.Position.X) - 10;
            //     if (min < 0)
            //     {
            //         foreach (var jointVM in jointCollectionVM)
            //             jointVM.Position = new Vector2(jointVM.Position.X - min, jointVM.Position.Y);
            //     }
            //
            //     double max = jointCollectionVM.Max(j => j.Position.X) + 10;
            //     if (max > CreatureStructureEditorCanvasVM.CanvasWidth)
            //     {
            //         foreach (var jointVM in jointCollectionVM)
            //         {
            //             jointVM.Position = new Vector2(jointVM.Position.X - (max - CreatureStructureEditorCanvasVM.CanvasWidth),
            //                                            jointVM.Position.Y);
            //         }
            //     }
            // }

            parameter.HistoryStack.NewEntry(creatureVM);
        }
    }
}