using System.Collections.Generic;
using System.Diagnostics;

namespace Core
{
    public class CreatureStructure
    {
        public IList<Bone> Bones { get; } = new List<Bone>();
        public IList<Joint> Joints { get; } = new List<Joint>();

        public CreatureStructure Clone()
        {
            var clonedStructure = new CreatureStructure();

            foreach (var joint in Joints)
            {
                var newJoint = joint.Clone();
                foreach (var bone in Bones)
                {
                    if (Equals(bone.FirstJoint, joint))
                        bone.FirstJoint = newJoint;
                    if (Equals(bone.SecondJoint, joint))
                        bone.SecondJoint = newJoint;
                }
                clonedStructure.Joints.Add(newJoint);
            }

            foreach (var bone in Bones)
            {
                var newBone = bone.Clone();
                Debug.Assert(clonedStructure.Joints.Contains(newBone.FirstJoint));
                Debug.Assert(clonedStructure.Joints.Contains(newBone.SecondJoint));
                clonedStructure.Bones.Add(newBone);
            }

            return clonedStructure;
        }
    }
}