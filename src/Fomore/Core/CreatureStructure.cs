using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Core
{
    [Serializable]
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
                clonedStructure.Joints.Add(newJoint);
            }

            foreach (var bone in Bones)
            {
                var newBone = bone.Clone();
                newBone.FirstJoint = clonedStructure.Joints.First(j => j.Tracker == bone.FirstJoint.Tracker);
                newBone.SecondJoint = clonedStructure.Joints.First(j => j.Tracker == bone.SecondJoint.Tracker);
                Debug.Assert(clonedStructure.Joints.Contains(newBone.FirstJoint));
                Debug.Assert(clonedStructure.Joints.Contains(newBone.SecondJoint));
                clonedStructure.Bones.Add(newBone);
            }

            return clonedStructure;
        }
    }
}