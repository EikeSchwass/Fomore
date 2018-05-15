using System.Collections.Generic;

namespace Core.Creatures
{
    public class Creature : ICloneable<Creature>
    {
        public List<Bone> Bones { get; } = new List<Bone>();
        public List<Joint> Joints { get; } = new List<Joint>();
        public CreatureInformation CreatureInformation { get; } = new CreatureInformation();

#warning NotImplemented
        public Creature Clone() => new Creature();
    }
}