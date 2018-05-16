using System;
using Core.Creatures;

namespace Core.Simulations
{
    public class Environment : ICloneable<Environment>
    {
        public string Name { get; set; } = $"Environment #{Guid.NewGuid().ToString().Substring(0, 8)}";
        public Vector2 Gravity { get; set; }
        public Terrain Terrain { get; set; }

        public Environment Clone() => new Environment();
    }
}