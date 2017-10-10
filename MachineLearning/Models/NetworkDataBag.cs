namespace MachineLearning.Models
{
    public class NetworkDataBag
    {
        private readonly NetworkInputOutputData[] data;
        private readonly int totalAvailableInputs, totalAvailableOutputs;

        public NetworkDataBag(NetworkInputOutputData[] data, int totalAvailableInputs, int totalAvailableOutputs)
        {
            this.data = data;
            this.totalAvailableInputs = totalAvailableInputs;
            this.totalAvailableOutputs = totalAvailableOutputs;
        }

        public NetworkInputOutputData[] Data
        {
            get { return this.data; }
        }

        public int TotalAvailableInputs
        {
            get { return this.totalAvailableInputs; }
        }

        public int TotalAvailableOutputs
        {
            get { return this.totalAvailableOutputs; }
        }
    }
}