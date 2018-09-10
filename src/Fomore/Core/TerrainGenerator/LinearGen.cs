﻿using System.Collections.Generic;

namespace Core.TerrainGenerator
{
    public class LinearGenerator : TerrainGenerator
    {
        public double Inclination { get; set; } = .025;

        public override IEnumerable<Vector2> Generate()
        {
            for (double x = 0; ; x += StepSize)
            {
                double y = x * Inclination;
                yield return new Vector2(x, y);
            }
        }
    }
}
