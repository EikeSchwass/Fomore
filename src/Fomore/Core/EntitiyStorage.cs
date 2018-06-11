using System;
using System.Collections.Generic;

namespace Core
{
    public class EntitiyStorage
    {
        public List<Creature> Creatures { get; } = new List<Creature>();
        public List<Environment> Environments { get; } = new List<Environment>();

        public event EventHandler EntitiesLoaded;

        public void LoadEntities()
        {
            var creatures = LoadCreatures();
            var environments = LoadEnvironments();
            Creatures.AddRange(creatures);
            Environments.AddRange(environments);
            EntitiesLoaded?.Invoke(this, new EventArgs());
        }

        private IEnumerable<Creature> LoadCreatures() => throw new NotImplementedException();
        private IEnumerable<Environment> LoadEnvironments() => throw new NotImplementedException();

        public void SaveEntities()
        {
            SaveCreatures();
            SaveEnvironments();
        }

        private void SaveCreatures() => throw new NotImplementedException();
        private void SaveEnvironments() => throw new NotImplementedException();
    }
}