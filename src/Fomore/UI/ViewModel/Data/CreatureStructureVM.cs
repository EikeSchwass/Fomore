using Core;
using Fomore.UI.ViewModel.Helper;

namespace Fomore.UI.ViewModel.Data
{
    public class CreatureStructureVM : ViewModelBase<CreatureStructure>
    {
        public EncapsulatingObservableCollection<BoneVM, Bone> BoneCollectionVM { get; }
        public EncapsulatingObservableCollection<JointVM, Joint> JointCollectionVM { get; }
        
        public CreatureStructureVM(CreatureStructure creatureStructure) : base(creatureStructure)
        {
            BoneCollectionVM = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            JointCollectionVM = new EncapsulatingObservableCollection<JointVM, Joint>(creatureStructure.Joints, j => new JointVM(j));
        }
    }
}