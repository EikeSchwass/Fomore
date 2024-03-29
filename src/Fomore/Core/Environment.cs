﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    [Serializable]
    public class Environment : ICloneable<Environment>
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

        public Environment Clone()
        {
            var environment = new Environment { Name = Name, Description = Description, Gravity = Gravity, Friction = Friction };

            environment.TerrainGenerators.AddRange(TerrainGenerators.Select(t => t.Clone()));

            return environment;
        }
    }
}