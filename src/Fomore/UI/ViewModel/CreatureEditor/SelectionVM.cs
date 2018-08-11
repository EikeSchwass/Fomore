// : Fomore/UI/SelectionVM.cs (2018/08/11)

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class SelectionVM : ICloneable<SelectionVM>
    {
        public List<JointVM> Joints { get; } = new List<JointVM>();
        public List<BoneVM> Bones { get; } = new List<BoneVM>();

        public int Count => Joints.Count + Bones.Count;

        public float Weight => Bones.Sum(bone => bone.Density);

        public void Clear()
        {
            Joints.Clear();
            Bones.Clear();
        }

        public SelectionVM Clone()
        {
            SelectionVM clone = new SelectionVM();
            clone.Joints.AddRange(Joints);
            clone.Bones.AddRange(Bones);
            return clone;
        }
    }
}
