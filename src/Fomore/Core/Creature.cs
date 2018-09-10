using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    [Serializable]
    public class Creature : ICloneable<Creature>
    {
        private string name;
        private string description;

        public Creature()
        {
            OnAccess();
        }

        private Creature(CreatureStructure creatureStructure, IEnumerable<MovementPattern> movementPatterns)
        {
            MovementPatterns = movementPatterns.ToList();
            CreatureStructure = creatureStructure;
            OnAccess();
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnAccess();
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnAccess();
            }
        }

        public DateTime LastAccess { get; private set; }

        // Todo update on Structure updates as well
        private void OnAccess()
        {
            LastAccess = DateTime.Now;
        }

        public List<MovementPattern> MovementPatterns { get; } = new List<MovementPattern>();
        public CreatureStructure CreatureStructure { get; } = new CreatureStructure();

        public Creature Clone()
        {
            var creature =
                new Creature(CreatureStructure.Clone(), MovementPatterns.Select(m => m.Clone())) { Name = Name, Description = Description };
            return creature;
        }
    }
}