using System.Windows.Input;
using System.Windows.Media;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class CutBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Cut, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Clipboard;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = new KeyGesture(Key.X, ModifierKeys.Control);

        /// <inheritdoc />
        public override string ToString() => "Cut Selection";
    }
}