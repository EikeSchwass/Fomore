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
        protected override InputGesture InputGesture { get; } = new KeyGesture(Key.Delete, ModifierKeys.Control);

        public override void OnInvoked(ModifierKeys modifierKeys)
        {
            base.OnInvoked(modifierKeys);
            // var creatureVM = parameter.HistoryStack.Current.Clone();
            //
            // creatureVM.CreatureStructureVM.BoneCollectionVM.Clear();
            // creatureVM.CreatureStructureVM.JointCollectionVM.Clear();
            //
            // parameter.HistoryStack.NewEntry(creatureVM);
        }

        /// <inheritdoc />
        public override string ToString() => "Clear All";
    }
}