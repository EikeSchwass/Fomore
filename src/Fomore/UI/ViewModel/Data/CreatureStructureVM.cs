using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Physics;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Helper;

namespace Fomore.UI.ViewModel.Data
{
    public class CreatureStructureVM : ViewModelBase<CreatureStructure>
    {
        private double totalHeight;
        private double? totalWeight;
        private double totalWidth;
        public EncapsulatingObservableCollection<BoneVM, Bone> BoneCollectionVM { get; }
        public EncapsulatingObservableCollection<JointVM, Joint> JointCollectionVM { get; }

        public double? TotalWeight
        {
            get => totalWeight;
            private set
            {
                if (value.Equals(totalWeight)) return;
                totalWeight = value;
                OnPropertyChanged();
            }
        }

        public float Scale
        {
            get => Model.Scale;
            set
            {
                if (value.Equals(Model.Scale)) return;
                Model.Scale = value;
                OnPropertyChanged();
                RecalculateDimensions();
            }
        }

        public double TotalWidth
        {
            get => totalWidth;
            private set
            {
                if (value.Equals(totalWidth)) return;
                totalWidth = value;
                OnPropertyChanged();
            }
        }

        public double TotalHeight
        {
            get => totalHeight;
            private set
            {
                if (value.Equals(totalHeight)) return;
                totalHeight = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand RecalculateWeightCommand { get; }

        public CreatureStructureVM(CreatureStructure creatureStructure) : base(creatureStructure)
        {
            JointCollectionVM = new EncapsulatingObservableCollection<JointVM, Joint>(creatureStructure.Joints,
                                                                                      creatureStructure.Joints.Select(j =>
                                                                                                                      {
                                                                                                                          var jointVM =
                                                                                                                              new
                                                                                                                                  JointVM(j);
                                                                                                                          jointVM
                                                                                                                                 .PropertyChanged
                                                                                                                              +=
                                                                                                                              BoneChanged;
                                                                                                                          return jointVM;
                                                                                                                      })
                                                                                                       .ToList());
            var boneVMs = creatureStructure.Bones.Select(b =>
                                                         {
                                                             var boneVM = new BoneVM(b)
                                                             {
                                                                 FirstJoint =
                                                                     JointCollectionVM.First(j => ReferenceEquals(b.FirstJoint, j.Model)),
                                                                 SecondJoint =
                                                                     JointCollectionVM.First(j => ReferenceEquals(b.SecondJoint, j.Model))
                                                             };
                                                             if (boneVM.FirstJoint != null)
                                                             {
                                                                 boneVM.FirstJoint.PropertyChanged += BoneChanged;
                                                             }

                                                             if (boneVM.SecondJoint != null)
                                                             {
                                                                 boneVM.SecondJoint.PropertyChanged += BoneChanged;
                                                             }

                                                             return boneVM;
                                                         });
            BoneCollectionVM = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, boneVMs.ToList());

            BoneCollectionVM.CollectionChanged += BoneCollectionChanged;
            JointCollectionVM.CollectionChanged += JointCollectionChanged;
            RecalculateWeightCommand = new DelegateCommand(o => RecalculateWeight(), o => true);
            RecalculateDimensions();
        }

        private async Task RecalculateWeightAsync()
        {
            var simulation = new Simulation();
            double weight = 0;
            foreach (var boneVM in BoneCollectionVM) weight += await simulation.GetBoneWeightAsync(boneVM.Model, Scale);

            TotalWeight = weight;
        }

        private void RecalculateWeight()
        {
            var simulation = new Simulation();
            double weight = 0;
            foreach (var boneVM in BoneCollectionVM) weight += simulation.GetBoneWeight(boneVM.Model, Scale);

            TotalWeight = weight;
        }

        private void BoneCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var removedBones = e.OldItems?.Cast<BoneVM>() ?? Enumerable.Empty<BoneVM>();
            foreach (var removedBone in removedBones)
            {
                removedBone.PropertyChanged -= BoneChanged;
                if (removedBone.FirstJoint != null)
                    removedBone.FirstJoint.PropertyChanged -= BoneChanged;
                if (removedBone.SecondJoint != null)
                    removedBone.SecondJoint.PropertyChanged -= BoneChanged;
            }

            var addedBones = e.NewItems?.Cast<BoneVM>() ?? Enumerable.Empty<BoneVM>();
            foreach (var addedBone in addedBones)
            {
                addedBone.PropertyChanged += BoneChanged;
                if (addedBone.FirstJoint != null)
                    addedBone.FirstJoint.PropertyChanged += BoneChanged;
                if (addedBone.SecondJoint != null)
                    addedBone.SecondJoint.PropertyChanged += BoneChanged;
            }

            RecalculateDimensions();
        }

        private void JointCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var removedJoins = e.OldItems?.Cast<JointVM>() ?? Enumerable.Empty<JointVM>();
            foreach (var removedJoin in removedJoins) removedJoin.PropertyChanged -= BoneChanged;

            var addedJoins = e.NewItems?.Cast<JointVM>() ?? Enumerable.Empty<JointVM>();
            foreach (var addedJoin in addedJoins) addedJoin.PropertyChanged += BoneChanged;

            RecalculateDimensions();
        }

        private void BoneChanged(object sender, PropertyChangedEventArgs e)
        {
            RecalculateDimensions();
        }

        private void RecalculateDimensions()
        {
            if (JointCollectionVM.Any())
            {
                TotalWidth = (JointCollectionVM.Max(j => j.Position.X) - JointCollectionVM.Min(j => j.Position.X)) * Scale;
                TotalHeight = (JointCollectionVM.Max(j => j.Position.Y) - JointCollectionVM.Min(j => j.Position.Y)) * Scale;
            }
            else
            {
                TotalWeight = 0;
                TotalHeight = 0;
            }
        }
    }
}