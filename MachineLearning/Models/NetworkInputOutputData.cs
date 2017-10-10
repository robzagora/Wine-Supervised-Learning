namespace MachineLearning.Models
{
    public class NetworkInputOutputData
    {
        private double[] input, output;

        public NetworkInputOutputData(double[] input, double[] output)
        {
            this.input = input;
            this.output = output;
        }

        public double[] Input
        {
            get { return this.input; }
        }

        public double[] Output
        {
            get { return this.output; }
        }
    }
}