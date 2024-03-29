﻿using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Core;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class RotateRightBehaviour : RotateBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.RotateRight, BehaviourBrush);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = new KeyGesture(Key.D, ModifierKeys.Control);

        /// <inheritdoc />
        public override string ToString() => "Rotate clockwise";

        /// <inheritdoc />
        public override void OnInvoked(CreatureEditorPanelVM parameter, ModifierKeys modifierKeys)
        {
            if (!parameter.Creature.CreatureStructureVM.JointCollectionVM.Any())
                return;
            base.OnInvoked(parameter, modifierKeys);
            double angle = (modifierKeys & ModifierKeys.Shift) > 0 ? 10 : 90;
            angle *= MathExtensions.DegreesToRadiansFactor;
            RotateCreatureStructure(parameter, angle);
        }
    }
}