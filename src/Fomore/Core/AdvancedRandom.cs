using System;

namespace Core
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
    }
}