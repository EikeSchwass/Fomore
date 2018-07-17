using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Core.TerrainGenerator
{
    public class PerlinGenerator : TerrainGenerator
    {
        private double Lerp(double a0, double a1, double w)
        {
            return (1.0 - w) * a0 + w * a1;
        }

        private double DotGridGradient(double ix, double iy, double x, double y)
        {

            // Compute the distance vector
            double dx = x - (double)ix;
            double dy = y - (double)iy;

            // Compute the Gradient
            double gradient = dy / dx;

            // Compute the dot-product
            return (dx * gradient + dy * gradient);
        }

        private double Perlin(double x, double y)
        {

            // Determine grid cell coordinates
            double x0 = x;
            double x1 = x0 + 1;
            double y0 = y;
            double y1 = y0 + 1;

            // Determine interpolation weights
            // Could also use higher order polynomial/s-curve here
            double sx = x - x0;
            double sy = y - y0;

            // Interpolate between grid point gradients
            double n0, n1, ix0, ix1, value;
            n0 = DotGridGradient(x0, y0, x, y);
            n1 = DotGridGradient(x1, y0, x, y);
            ix0 = Lerp(n0, n1, sx);
            n0 = DotGridGradient(x0, y1, x, y);
            n1 = DotGridGradient(x1, y1, x, y);
            ix1 = Lerp(n0, n1, sx);
            value = Lerp(ix0, ix1, sy);

            return value;
        }

        public override IEnumerable<double> Generate()
        {
            for (double x = 0; ; x += StepSize)
            {
                yield return Perlin(x, 1);
            }
        }
    }
}
