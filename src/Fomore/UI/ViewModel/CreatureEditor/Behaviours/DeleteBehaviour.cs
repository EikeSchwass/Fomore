using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class DeleteBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Trash, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = new KeyGesture(Key.Delete);

        /// <inheritdoc />
        public override string ToString() => "Delete Selection";

        /// <inheritdoc />
        protected override bool CanExecute(CreatureEditorPanelVM parameter) => true;// (parameter?.CreatureStructureEditorCanvasVM?.SelectedJoints?.Count ?? 0) > 0;

        /// <inheritdoc />
        public override void OnInvoked(CreatureEditorPanelVM parameter, ModifierKeys modifierKeys)
        {
            base.OnInvoked(parameter, modifierKeys);
            var canvasVM = parameter.CreatureStructureEditorCanvasVM;
            var creatureVM = canvasVM.HistoryStack.Current.Clone();
            var jointsToRemove = creatureVM.CreatureStructureVM.JointCollectionVM.Where(j => canvasVM.SelectedJoints.Any(k => k.Model.Tracker == j.Model.Tracker)).ToList();
            var bonesToRemove = creatureVM.CreatureStructureVM.BoneCollectionVM.Where(b => canvasVM.SelectedBones.Any(k => k.Model.Tracker == b.Model.Tracker))
                                    .ToList();
            foreach (var jointVM in jointsToRemove)
            {
                creatureVM.CreatureStructureVM.JointCollectionVM.Remove(jointVM);
                bonesToRemove = bonesToRemove.Concat(creatureVM.CreatureStructureVM.BoneCollectionVM.Where(b => b.FirstJoint.Model.Tracker == jointVM.Model.Tracker ||
                                                                                         b.SecondJoint.Model.Tracker == jointVM.Model.Tracker)).ToList();
            }
            foreach (var boneVM in bonesToRemove.Distinct())
            {
                creatureVM.CreatureStructureVM.BoneCollectionVM.Remove(boneVM);
            }
            canvasVM.HistoryStack.NewEntry(creatureVM);
        }
    }
}