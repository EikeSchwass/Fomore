using System;
using System.Collections.Generic;

namespace Core.TerrainGenerator
{
    [Serializable]
    public class PowerGenerator : TerrainGenerator
    {
        public double Gradualness { get; set; } = 0.01;
        public double Power { get; set; } = 1.5;
        public double Offset { get; set; } = 0;

        /// <inheritdoc />
        public override IEnumerable<Vector2> Generate()
        {
            for (double x = 0; ; x += StepSize)
            {
                double y = Gradualness * Math.Pow(x - Offset, Power);
                if (x < Offset)
                    y = 0;
                yield return new Vector2(x, y);
            }
        }
    }
}