using System.Windows.Input;
using System.Windows.Media;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class PasteBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Paste, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Clipboard;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = new KeyGesture(Key.V, ModifierKeys.Control);

        /// <inheritdoc />
        public override string ToString() => "Paste Selection";
    }
}