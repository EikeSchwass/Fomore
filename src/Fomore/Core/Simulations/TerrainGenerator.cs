using System;

namespace Core.Simulations
{
    public class TerrainGenerator
    {
        public virtual float GetHeight(float x)
        {
            throw new NotImplementedException($"{nameof(GetHeight)} not implemented");
        }
    }
}