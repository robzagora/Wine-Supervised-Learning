namespace Common.Extensions
{
    using System;

    public static class MathExtensions
    {
        public static double RescaleNormalization(this double value, double min, double max, int decimals = 3)
        {
            return Math.Round((value - min) / (max - min), decimals);
        }

        public static double RescaleNormalization(this int value, int min, int max)
        {
            return (value - min) / (max - min);
        }
    }
}