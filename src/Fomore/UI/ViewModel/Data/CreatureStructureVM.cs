using System.Linq;
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
            BoneCollectionVM = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones,
                                                                                   creatureStructure.Bones.Select(b => new BoneVM(b)
                                                                                                     {
                                                                                                         FirstJoint =
                                                                                                             new JointVM(creatureStructure
                                                                                                                        .Joints.First(j =>
                                                                                                                                          j.Tracker ==
                                                                                                                                          b.FirstJoint
                                                                                                                                           .Tracker)),
                                                                                                         SecondJoint =
                                                                                                             new JointVM(creatureStructure
                                                                                                                        .Joints.First(j =>
                                                                                                                                          j.Tracker ==
                                                                                                                                          b.SecondJoint
                                                                                                                                           .Tracker))
                                                                                                     })
                                                                                                    .ToList());
            JointCollectionVM =
                new EncapsulatingObservableCollection<JointVM, Joint>(creatureStructure.Joints,
                                                                      creatureStructure.Joints.Select(j => new JointVM(j)).ToList());
        }
    }
}