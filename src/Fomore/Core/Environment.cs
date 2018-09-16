using System;
using System.Collections.Generic;

namespace Core
{
    [Serializable]
    public class Environment
    {
        public string Name { get; set; }

        public string Description { get; set; }
        
        public double Gravity { get; set; }
        
        public double Friction { get; set; }

        public DateTime CreationDate { get; private set; }

        public List<TerrainGenerator.TerrainGenerator> TerrainGenerators { get; } = new List<TerrainGenerator.TerrainGenerator>();

        public Environment()
        {
            OnCreate();
        }

        private void OnCreate()
        {
            CreationDate = DateTime.Now;
        }
    }
}