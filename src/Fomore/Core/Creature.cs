using System.Collections.Generic;

namespace Core
{
    public class Creature
    {
        public  Creature()
        {

        }

        public string CreatureName { get; set; }

        public string CreatureDescription { get; set; }

        public List<MovementPattern> MovementPatterns { get; } = new List<MovementPattern>();
        public CreatureStructure CreatureStructure { get; } = new CreatureStructure();
    }
}