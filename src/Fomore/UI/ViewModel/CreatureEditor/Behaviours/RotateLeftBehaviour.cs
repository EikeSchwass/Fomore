using System.Windows.Input;
using System.Windows.Media;
using Core;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class RotateLeftBehaviour : RotateBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.RotateLeft, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = new KeyGesture(Key.A, ModifierKeys.Control);

        /// <inheritdoc />
        public override string ToString() => "Rotate counter clockwise";

        /// <inheritdoc />
        public override void OnInvoked(ModifierKeys modifierKeys)
        {
            // base.OnInvoked(parameter, modifierKeys);
            // double angle = (modifierKeys & ModifierKeys.Shift) > 0 ? -10 : -90;
            // angle *= MathExtensions.DegreesToRadiansFactor;
            // RotateCreatureStructure(parameter, angle);
        }
    }
}