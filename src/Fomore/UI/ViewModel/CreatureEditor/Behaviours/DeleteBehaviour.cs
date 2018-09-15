using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Fomore.UI.ViewModel.Helper;
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
        [SuppressMessage("ReSharper", "ImplicitlyCapturedClosure")]
        public override void OnInvoked(CreatureEditorPanelVM parameter, ModifierKeys modifierKeys)
        {
            base.OnInvoked(parameter, modifierKeys);
            var canvasVM = parameter.CreatureStructureEditorCanvasVM;
            var creatureVM = parameter.Creature;
            var jointsToRemove = creatureVM.CreatureStructureVM.JointCollectionVM.Where(j => canvasVM.SelectedJoints.Any(k => k.Model.Tracker == j.Model.Tracker)).ToList();
            var bonesToRemove = creatureVM.CreatureStructureVM.BoneCollectionVM.Where(b => canvasVM.SelectedBones.Any(k => k.Model.Tracker == b.Model.Tracker))
                                    .ToList();
            foreach (var jointVM in jointsToRemove)
            {
                bonesToRemove = bonesToRemove.Concat(creatureVM.CreatureStructureVM.BoneCollectionVM.Where(b => b.FirstJoint.Model.Tracker == jointVM.Model.Tracker ||
                                                                                                                b.SecondJoint.Model.Tracker == jointVM.Model.Tracker)).ToList();
            }

            var changeOperation = new ChangeOperation(c =>
                                                      {
                                                          foreach (var jointVM in jointsToRemove)
                                                          {
                                                              c.Creature.CreatureStructureVM.JointCollectionVM.Remove(jointVM);
                                                          }
                                                          foreach (var boneVM in bonesToRemove.Distinct())
                                                          {
                                                              creatureVM.CreatureStructureVM.BoneCollectionVM.Remove(boneVM);
                                                          }
                                                      },
                                                      c =>
                                                      {

                                                          foreach (var jointVM in jointsToRemove)
                                                          {
                                                              c.Creature.CreatureStructureVM.JointCollectionVM.Add(jointVM);
                                                          }
                                                          foreach (var boneVM in bonesToRemove)
                                                          {
                                                              c.Creature.CreatureStructureVM.BoneCollectionVM.Add(boneVM);
                                                          }
                                                      });

            canvasVM.HistoryStack.AddOperation(changeOperation);
        }
    }
}