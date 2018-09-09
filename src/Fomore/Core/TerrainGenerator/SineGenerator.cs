using System;
using System.Collections.Generic;

namespace Core.TerrainGenerator
{
    public class SineGenerator : TerrainGenerator
    {
        /// <summary>
        /// Describes the horizontal Offset.
        /// </summary>
        public double Offset { get; set; }

        //
        /// <summary>
        /// The height of individual sine waves.
        /// </summary>
        public double Amplitude { get; set; }

        /// <summary>
        /// The frequency describes the amount of waves per step. So higher frequency yields steeper terrains.
        /// </summary>
        public double Frequency { get; set; }

        /// <inheritdoc />
        public override IEnumerable<double> Generate()
        {
            for (double x = 0; ; x += StepSize)
            {
                double y = Amplitude * Math.Sin((x + Offset) * Frequency);
                yield return y;
            }
        }
    }
}
