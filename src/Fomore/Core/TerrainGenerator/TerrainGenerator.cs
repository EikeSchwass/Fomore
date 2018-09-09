using System.Collections.Generic;

namespace Core.TerrainGenerator
{
    public abstract class TerrainGenerator
    {
        public double StepSize { get; set; }

        public abstract IEnumerable<double> Generate();
    }
}
