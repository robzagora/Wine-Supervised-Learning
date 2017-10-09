namespace MachineLearning.Models
{
    using System;

    public class MinMaxRange
    {
        private readonly double min, max;

        public MinMaxRange(double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min));
            }

            this.min = min;
            this.max = max;
        }

        public double Min
        {
            get { return this.min; }
        }

        public double Max
        {
            get { return this.max; }
        }
    }
}