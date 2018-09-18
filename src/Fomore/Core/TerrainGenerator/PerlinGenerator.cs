using System;
using System.Collections.Generic;

namespace Core.TerrainGenerator
{
    [Serializable]
    public class PerlinGenerator : TerrainGenerator
    {
        public double Phase { get; set; } = 1;
        public double Smoothness { get; set; } = 100;
        public double Amplitude { get; set; } = 1;

        public override IEnumerable<Vector2> Generate()
        {
            for (double x = 0; ; x += StepSize)
            {
                yield return new Vector2(x, Amplitude * PerlinNoise.Noise(x / Smoothness + 1, Phase + 1));
            }
        }

        /// <inheritdoc />
        public override TerrainGenerator Clone()
        {
            return new PerlinGenerator() { Phase = Phase, StepSize = StepSize, Smoothness = Smoothness, Amplitude = Amplitude };
        }

    }

    // Transcribed from http://www.siafoo.net/snippet/144?nolinenos#perlin2003
}