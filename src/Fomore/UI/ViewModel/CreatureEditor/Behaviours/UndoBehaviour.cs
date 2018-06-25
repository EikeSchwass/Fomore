using System.Windows.Input;
using System.Windows.Media;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class UndoBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Reply, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.History;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = new KeyGesture(Key.Z, ModifierKeys.Control);

        public UndoBehaviour(HistoryStackVM<CreatureVM> historyStack)
        {
            Command = historyStack.UndoCommand;
        }

        /// <inheritdoc />
        public override string ToString() => "Undo last operation";

        /*
         * There doesn't need to be any functionality here, because the constructor reroutes the command directly to the historystack commands
         */
    }
}