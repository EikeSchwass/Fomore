using System.Windows.Input;
using System.Windows.Media;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class CopyBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Copy, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Clipboard;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = new KeyGesture(Key.C, ModifierKeys.Control);

        /// <inheritdoc />
        public override string ToString() => "Copy Selection";
    }
}