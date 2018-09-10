using System;
using System.Linq;

namespace Core.TerrainGenerator
{
    // From: https://stackoverflow.com/questions/8659351/2d-perlin-noise
    public static class PerlinNoise
    {
        private static Random Random { get; } = new Random();
        private static int[] Permutation { get; set; }

        private static Vector2[] Gradients { get; set; }

        static PerlinNoise()
        {
            CalculatePermutation();
            CalculateGradients();
        }

        private static void CalculatePermutation()
        {
            Permutation = Enumerable.Range(0, 256).ToArray();

            for (var i = 0; i < Permutation.Length; i++)
            {
                var source = Random.Next(Permutation.Length);

                var t = Permutation[i];
                Permutation[i] = Permutation[source];
                Permutation[source] = t;
            }
        }

        /// <summary>
        /// generate a new permutation.
        /// </summary>
        public static void Reseed()
        {
            CalculatePermutation();
        }

        private static void CalculateGradients()
        {
            Gradients = new Vector2[256];

            for (var i = 0; i < Gradients.Length; i++)
            {
                Vector2 gradient;

                do
                {
                    gradient = new Vector2((float)(Random.NextDouble() * 2 - 1), (float)(Random.NextDouble() * 2 - 1));
                }
                while (gradient.Length >= 1);

                gradient = gradient.Normalize();

                Gradients[i] = gradient;
            }

        }

        private static double Drop(double t)
        {
            t = Math.Abs(t);
            return 1f - t * t * t * (t * (t * 6 - 15) + 10);
        }

        private static double Q(double u, double v)
        {
            return Drop(u) * Drop(v);
        }

        public static double Noise(double x, double y)
        {
            var cell = new Vector2((float)Math.Floor(x), (float)Math.Floor(y));

            double total = 0;

            var corners = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };

            foreach (var n in corners)
            {
                var ij = cell + n;
                var uv = new Vector2(x - ij.X, y - ij.Y);

                int index = Permutation[(int)ij.X % Permutation.Length];
                index = Permutation[(index + (int)ij.Y) % Permutation.Length];

                var grad = Gradients[index % Permutation.Length];

                total += Q(uv.X, uv.Y) * Dot(grad, uv);
            }

            return Math.Max(Math.Min(total, 1f), -1f);
        }

        private static double Dot(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }
    }
}