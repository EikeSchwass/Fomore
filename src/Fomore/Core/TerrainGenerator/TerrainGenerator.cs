using System;
using System.Collections.Generic;
using System.Text;

namespace Core.TerrainGenerator
{
    public abstract class TerrainGenerator
    {
        public double StepSize { get; set; }

        public int Case { get; set; }

        public abstract IEnumerable<double> Generate();
    }
}
