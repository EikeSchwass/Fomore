using System.Windows.Input;
using System.Windows.Media;
using Fomore.UI.ViewModel.Helper;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class RedoBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.MailForward, BehaviourBrush);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.History;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = new KeyGesture(Key.Y, ModifierKeys.Control);

        /// <inheritdoc />
        public override string ToString() => "Redo last operation";

        public RedoBehaviour(HistoryStackVM<CreatureStructureEditorCanvasVM> historyStack)
        {
            Command = historyStack.RedoCommand;
        }

        /*
         * There doesn't need to be any functionality here, because the constructor reroutes the command directly to the historystack commands
         */
    }
}