using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Fomore.UI.ViewModel.Helper;
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
            var joints = parameter.Creature.CreatureStructureVM.JointCollectionVM.ToList();
            var bones = parameter.Creature.CreatureStructureVM.BoneCollectionVM.ToList();
            var changeOperation = new ChangeOperation(c =>
                                                      {
                                                          c.Creature.CreatureStructureVM.JointCollectionVM.Clear();
                                                          c.Creature.CreatureStructureVM.BoneCollectionVM.Clear();
                                                      },
                                                      c =>
                                                      {
                                                          foreach (var joint in joints)
                                                              c.Creature.CreatureStructureVM.JointCollectionVM.Add(joint);
                                                          foreach (var bone in bones)
                                                              c.Creature.CreatureStructureVM.BoneCollectionVM.Add(bone);
                                                      });
            parameter.CreatureStructureEditorCanvasVM.HistoryStack.AddOperation(changeOperation);
        }
    }
}