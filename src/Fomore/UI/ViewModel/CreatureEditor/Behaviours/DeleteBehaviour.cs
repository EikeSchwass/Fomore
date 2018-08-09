using System;
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

        protected override bool CanExecute(CreatureEditorPanelVM parameter)
        {
            return parameter?.CreatureStructureEditorCanvasVM.SelectedJoints.Count > 0 ||
                   parameter?.CreatureStructureEditorCanvasVM.SelectedBones.Count > 0;
        }

        public override void OnInvoked(CreatureEditorPanelVM parameter, ModifierKeys modifierKeys)
        {
            base.OnInvoked(parameter, modifierKeys);
            var creatureVM = parameter.HistoryStack.Current.Clone();

            Console.WriteLine(creatureVM.CreatureStructureVM.BoneCollectionVM.Count);

            Console.WriteLine("All bone hashes");
            int counter = 1;
            foreach (var bone in creatureVM.CreatureStructureVM.BoneCollectionVM)
                Console.WriteLine(counter++ + ": " + bone.GetHashCode() + "[" + bone.Model.GetHashCode() + "]");

            foreach (var bone in parameter.CreatureStructureEditorCanvasVM.SelectedBones)
            {
                Console.WriteLine("Processing selected bone " + bone.GetHashCode() + "[" + bone.Model.GetHashCode() + "]");
                Console.WriteLine(creatureVM.CreatureStructureVM.BoneCollectionVM.Contains(bone));
                if (creatureVM.CreatureStructureVM.BoneCollectionVM.Remove(bone))
                    Console.WriteLine("Bone " + bone.GetHashCode() + " deleted");
            }
            Console.WriteLine(creatureVM.CreatureStructureVM.BoneCollectionVM.Count);

            foreach (var joint in parameter.CreatureStructureEditorCanvasVM.SelectedJoints)
            {
                Console.WriteLine("Joint deleted");
                creatureVM.CreatureStructureVM.JointCollectionVM.Remove(joint);
            }
            
            parameter.HistoryStack.NewEntry(creatureVM);
        }

        /// <inheritdoc />
        public override string ToString() => "Delete Selection";
    }
}