namespace MachineLearning.Models
{
    using System;

    public class WineNormalisedData : WineData
    {
        private Lazy<double[]> inputs, outputs;

        public WineNormalisedData()
        {
            this.inputs = new Lazy<double[]>(this.GetInputs);
            this.outputs = new Lazy<double[]>(this.GetOutputs);
        }

        public double[] Inputs
        {
            get { return this.inputs.Value; }
        }

        public double[] Outputs
        {
            get { return this.outputs.Value; }
        }

        private double[] GetInputs()
        {
            double[] inputs = new double[WineData.TotalAvailableInputs];

            for (int i = 0; i < WineData.TotalAvailableInputs; i++)
            {
                inputs[i] = this[i + 1];
            }

            return inputs;
        }

        private double[] GetOutputs()
        {
            double[] outputs = new double[WineData.TotalAvailableOutputs];

            if (this.Class == WineData.ClassOne)
            {
                return new double[] { 1, 0, 0 };
            }
            else if (this.Class == WineData.ClassTwo)
            {
                return new double[] { 0, 1, 0 };
            }
            else if (this.Class == WineData.ClassThree)
            {
                return new double[] { 0, 0, 1 };
            }

            return new double[] { -1, -1, -1 };
        }
    }
}