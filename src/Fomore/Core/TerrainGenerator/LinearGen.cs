using System;
using System.Collections.Generic;
using System.Text;

namespace Core.TerrainGenerator
{
    public class LinearGenerator : TerrainGenerator
    {
        public double Inclination { get; set; }

        public override IEnumerable<double> Generate()
        {
            for (double x = 0; ; x += StepSize)
            {
                double y = x * Inclination;
                yield return y;
            }
        }
    }
}
