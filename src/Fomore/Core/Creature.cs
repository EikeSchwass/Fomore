using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    [Serializable]
    public class Creature : ICloneable<Creature>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; private set; }
        

        public List<MovementPattern> MovementPatterns { get; } = new List<MovementPattern>();
        public CreatureStructure CreatureStructure { get; } = new CreatureStructure();

        public Creature()
        {
            OnCreate();
        }

        private Creature(CreatureStructure creatureStructure, IEnumerable<MovementPattern> movementPatterns)
        {
            MovementPatterns = movementPatterns.ToList();
            CreatureStructure = creatureStructure;
            OnCreate();
        }

        private void OnCreate()
        {
            CreationDate = DateTime.Now;
        }

        public Creature Clone()
        {
            var creature =
                new Creature(CreatureStructure.Clone(), MovementPatterns.Select(m => m.Clone())) { Name = Name, Description = Description };
            return creature;
        }
    }
}