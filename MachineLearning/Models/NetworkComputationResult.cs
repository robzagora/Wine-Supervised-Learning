namespace MachineLearning.Models
{
    public class NetworkComputationResult
    {
        private readonly NetworkInputOutputData source;
        private readonly double[] calculatedOutput;

        public NetworkComputationResult(NetworkInputOutputData source, double[] calculatedOutput)
        {
            this.source = source;
            this.calculatedOutput = calculatedOutput;
        }

        public NetworkInputOutputData Source
        {
            get { return this.source; }
        }

        public double[] CalculatedOutput
        {
            get { return this.calculatedOutput; }
        }
    }
}