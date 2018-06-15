using System.Collections.Generic;

namespace Core
{
    public class Creature
    {
        public List<MovementPattern> MovementPatterns { get; } = new List<MovementPattern>();
        public CreatureStructure CreatureStructure { get; } = new CreatureStructure();
    }
}