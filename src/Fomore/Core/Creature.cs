using System.Collections.Generic;

namespace Core
{
    public class Creature : Entitiy
    {
        private string creatureDescription;
        private string creatureName;
        public bool isOpen { get; set; }

        public  Creature()
        {

        }

        public string CreatureName
        {
            get => creatureName;
            set
            {
                if (value == creatureName) return;
                creatureName = value;
            }
        }

        public string CreatureDescription
        {
            get => creatureDescription;
            set
            {
                if (value == creatureDescription) return;
                creatureDescription = value;
            }
        }
    }
    public class Creature
    {
        public List<MovementPattern> MovementPatterns { get; } = new List<MovementPattern>();
        public CreatureStructure CreatureStructure { get; } = new CreatureStructure();
    }
}