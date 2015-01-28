namespace Fifthweek.Shared
{
    using System;

    public class ApplicationRandom : IRandom
    {
        private static readonly Random Global = new Random();
        
        [ThreadStatic]
        private static Random local;

        public int Next()
        {
            return GetLocal().Next();
        }

        public int Next(int maxValue)
        {
            return GetLocal().Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return GetLocal().Next(minValue, maxValue);
        }

        public void NextBytes(byte[] buffer)
        {
            GetLocal().NextBytes(buffer);
        }

        public double NextDouble()
        {
            return GetLocal().NextDouble();
        }

        private static Random GetLocal()
        {
            if (local != null)
            {
                return local;
            }

            int seed;
            lock (Global)
            {
                seed = Global.Next();
            }

            local = new Random(seed);
            return local;
        }
    }
}