using System;
using System.Collections.Generic;

namespace Core
{
    [Serializable]
    public class Environment
    {
        private string description;
        private double friction;
        private double gravity;
        private string name;

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

        public double Gravity
        {
            get => gravity;
            set
            {
                gravity = value;
                OnAccess();
            }
        }

        public double Friction
        {
            get => friction;
            set
            {
                friction = value;
                OnAccess();
            }
        }

        public DateTime LastAccess { get; private set; }

        public List<TerrainGenerator.TerrainGenerator> TerrainGenerators { get; } = new List<TerrainGenerator.TerrainGenerator>();

        public Environment()
        {
            OnAccess();
        }

        private void OnAccess()
        {
            LastAccess = DateTime.Now;
        }
    }
}