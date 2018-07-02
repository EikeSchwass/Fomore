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
        public override void OnInvoked(CreatureEditorPanelVM parameter)
        {
            base.OnInvoked(parameter);
            var original = parameter.HistoryStack.Original;
            original.CreatureStructureVM.BoneCollectionVM.Clear();
            var current = parameter.HistoryStack.Current;
            foreach (var boneVM in current.CreatureStructureVM.BoneCollectionVM)
            {
                original.CreatureStructureVM.BoneCollectionVM.Add(boneVM);
            }
            foreach (var jointVM in current.CreatureStructureVM.JointCollectionVM)
            {
                original.CreatureStructureVM.JointCollectionVM.Add(jointVM);
            }
        }
    }
}