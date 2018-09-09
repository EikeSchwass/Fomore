using System.Windows.Input;
using System.Windows.Media;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class ClearBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Remove, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = new KeyGesture(Key.Delete, ModifierKeys.Shift);

        /// <inheritdoc />
        public override string ToString() => "Clear All";

        /// <inheritdoc />
        public override void OnInvoked(CreatureEditorPanelVM parameter, ModifierKeys modifierKeys)
        {
            var creatureVM = parameter.CreatureStructureEditorCanvasVM.HistoryStack.Current.Clone();
            creatureVM.CreatureStructureVM.JointCollectionVM.Clear();
            creatureVM.CreatureStructureVM.BoneCollectionVM.Clear();
            parameter.CreatureStructureEditorCanvasVM.HistoryStack.NewEntry(creatureVM);
        }
    }
}