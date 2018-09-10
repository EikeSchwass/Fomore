using System;
using System.Collections.Generic;

namespace Core.TerrainGenerator
{
    public class SineGenerator : TerrainGenerator
    {
        /// <summary>
        /// Describes the horizontal Offset.
        /// </summary>
        public double Offset { get; set; } = 0;

        //
        /// <summary>
        /// The height of individual sine waves.
        /// </summary>
        public double Amplitude { get; set; } = 1;

        /// <summary>
        /// The frequency describes the amount of waves per step. So higher frequency yields steeper terrains.
        /// </summary>
        public double Frequency { get; set; } = 0.1 / Math.PI;

        /// <inheritdoc />
        public override IEnumerable<Vector2> Generate()
        {
            for (double x = 0; ; x += StepSize)
            {
                double y = Amplitude * Math.Sin((x + Offset) * Frequency);
                yield return new Vector2(x, y);
            }
        }
    }
}
