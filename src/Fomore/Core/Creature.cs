using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class Creature : ICloneable<Creature>
    {
        public Creature()
        {

        }

        private Creature(CreatureStructure creatureStructure, IEnumerable<MovementPattern> movementPatterns)
        {
            MovementPatterns = movementPatterns.ToList();
            CreatureStructure = creatureStructure;
        }

        public string CreatureName { get; set; }

        public string CreatureDescription { get; set; }

        public List<MovementPattern> MovementPatterns { get; } = new List<MovementPattern>();
        public CreatureStructure CreatureStructure { get; } = new CreatureStructure();

        public Creature Clone()
        {
            var creature = new Creature(CreatureStructure.Clone(), MovementPatterns.Select(m => m.Clone())) {CreatureName = CreatureName, CreatureDescription = CreatureDescription};
            return creature;
        }
    }
}