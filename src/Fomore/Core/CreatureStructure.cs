using System.Collections.Generic;

namespace Core
{
    public class CreatureStructure
    {
        public IList<Bone> Bones { get; } = new List<Bone>();
        public IList<Joint> Joints { get; } = new List<Joint>();
    }
}