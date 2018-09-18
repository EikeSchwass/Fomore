using System;
using static System.Math;

namespace Core.Training
{
    public static class AdvancedRandom
    {
        [ThreadStatic] private static Random local;
        private static Random Global { get; } = new Random();

        public static Random Random
        {
            get
            {
                if (local == null)
                {
                    int seed;
                    lock (Global)
                        seed = Global.Next();
                    local = new Random(seed);
                }

                return local;
            }
        }

        public static double NextNormal(double mean, double sd)
        {
            double u = Random.NextDouble();
            double v = Random.NextDouble();
            double x = u * Sqrt(-2 * Log(u)) * Cos(2 * PI * v);
            x *= sd;
            x += mean;
            return x;
        }
    }
}