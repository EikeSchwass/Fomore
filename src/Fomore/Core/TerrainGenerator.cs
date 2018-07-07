using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    class TerrainGenerator
    {
        /// <summary>
        /// This method uses Perlin Noise (or any other technique) to generate a terrain with given properties.
        ///
        /// The terrain, as I (Jazib) have imagined it as is a double array of values ranging from 0 (horizontal axis) to
        /// 100 (top end of the horizontal axis).
        /// </summary>
        /// <returns> It returns an array of doubles which refer to the distance of the ground from the horizontal axis.</returns>
        public double[] GenerateTerrain(int terrainLength, string terrainSelector)
        {
            terrain = new double[terrainLength];
            switch (terrainSelector)
            {
                case "Perlin":
                    terrain = PerlinGenerator(terrainLength);
                    break;
                case "SlopeUpward":
                     terrain = SlopeUpwardGenerator(terrainLength);
                    break;
                case "SlopeDownward":
                    terrain = SlopeDownwardGenerator(terrainLength);
                    break;
                default:
                    terrain = FlatGenerator(terrainLength);
                    break;
            }

            return terrain;
        }

        private double[] PerlinGenerator(int terrainLength)
        {
            throw new NotImplementedException();
        }

        private double[] SlopeUpwardGenerator(int terrainLength)
        {
            for (int i=0; i < terrainLength; i++)
            {
                terrain[i] = 5 + ((70 - 5) / terrainLength) * i;
            }

            return terrain;
        }

        private double[] SlopeDownwardGenerator(int terrainLength)
        {
            for (int i = 0; i < terrainLength; i++)
            {
                terrain[i] = 70 - ((70 - 5) / terrainLength) * i;
            }

            return terrain;
        }

        private double[] FlatGenerator(int terrainLength)
        {
            for (int i = 0; i < terrainLength; i++)
            {
                terrain[i] = 30;
            }

            return terrain;
        }

        private double[] terrain;
    }
}
