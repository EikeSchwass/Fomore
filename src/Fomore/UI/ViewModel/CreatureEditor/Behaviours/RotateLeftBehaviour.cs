﻿using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Core;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class RotateLeftBehaviour : RotateBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.RotateLeft, BehaviourBrush);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = new KeyGesture(Key.A, ModifierKeys.Control);

        /// <inheritdoc />
        public override string ToString() => "Rotate counter clockwise";

        /// <inheritdoc />
        public override void OnInvoked(CreatureEditorPanelVM parameter, ModifierKeys modifierKeys)
        {
            if (!parameter.Creature.CreatureStructureVM.JointCollectionVM.Any())
                return;
            base.OnInvoked(parameter, modifierKeys);
            double angle = (modifierKeys & ModifierKeys.Shift) > 0 ? -10 : -90;
            angle *= MathExtensions.DegreesToRadiansFactor;
            RotateCreatureStructure(parameter, angle);
        }
    }
}