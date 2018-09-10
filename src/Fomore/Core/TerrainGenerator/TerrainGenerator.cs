using System;
using System.Collections.Generic;

namespace Core.TerrainGenerator
{
    [Serializable]
    public abstract class TerrainGenerator
    {
        /// <summary>
        /// Describes the horizontal resolution of the generated terrain. Lower values yield smoother terrains but might negativly impact performance.
        /// </summary>
        public double StepSize { get; set; } = 1;

        /// <summary>
        /// Generates a never ending series of height values.
        /// </summary>
        /// <returns>A never ending series of height values.</returns>
        public abstract IEnumerable<Vector2> Generate();
    }
}
