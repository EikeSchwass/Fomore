using System.Windows.Input;
using System.Windows.Media;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class SaveBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Save, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Storage;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = new KeyGesture(Key.S, ModifierKeys.Control);

        /// <inheritdoc />
        public override string ToString() => "Save";

        /// <inheritdoc />
        public override void OnInvoked(CreatureEditorPanelVM parameter, ModifierKeys modifierKeys)
        {
            parameter.OnSaveRequested();
        }
    }
}