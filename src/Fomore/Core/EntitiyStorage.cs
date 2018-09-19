using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Core
{
    public class EntitiyStorage
    {
        public List<Creature> Creatures { get; } = new List<Creature>();
        public List<Environment> Environments { get; } = new List<Environment>();

        public event EventHandler EntitiesLoaded;

        public void SaveEntities()
        {
            SaveCreatures();
            SaveEnvironments();
        }

        public void LoadEntities()
        {
            var creatures = LoadCreatures();
            var environments = LoadEnvironments();
            Creatures.AddRange(creatures);
            Environments.AddRange(environments);
            EntitiesLoaded?.Invoke(this, new EventArgs());
        }

        public string GetFolderPath()
        {
            string localAppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            localAppData = Path.Combine(localAppData, "Fomore");
            if (!Directory.Exists(localAppData))
                Directory.CreateDirectory(localAppData);
            localAppData = Path.Combine(localAppData, "Data");
            if (!Directory.Exists(localAppData))
                Directory.CreateDirectory(localAppData);
            return localAppData;
        }

        private IEnumerable<Creature> LoadCreatures()
        {
            string creaturePath = Path.Combine(GetFolderPath(), "creatures.data");
            if (!File.Exists(creaturePath))
            {
                return new List<Creature>();
            }
            using (var fileStream = new FileStream(creaturePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                var bf = new BinaryFormatter();
                var creatures = ((IEnumerable<Creature>)bf.Deserialize(fileStream)).ToList();

                return creatures;
            }
        }

        private IEnumerable<Environment> LoadEnvironments()
        {
            string environmentPath = Path.Combine(GetFolderPath(), "environments.data");
            if (!File.Exists(environmentPath))
            {
                return new List<Environment>();
            }
            using (var fileStream = new FileStream(environmentPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                var bf = new BinaryFormatter();
                return (IEnumerable<Environment>)bf.Deserialize(fileStream);
            }
        }

        private void SaveCreatures()
        {
            string creaturePath = Path.Combine(GetFolderPath(), "creatures.data");
            using (var fileStream = new FileStream(creaturePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fileStream, Creatures);
            }
        }

        private void SaveEnvironments()
        {
            string environmentPath = Path.Combine(GetFolderPath(), "environments.data");
            using (var fileStream = new FileStream(environmentPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fileStream, Environments);
            }
        }
    }
}