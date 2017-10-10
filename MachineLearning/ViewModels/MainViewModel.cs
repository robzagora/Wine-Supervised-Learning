namespace MachineLearning.ViewModels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Accord.Neuro;
    using Accord.Neuro.ActivationFunctions;
    using Accord.Neuro.Learning;
    using Accord.Neuro.Networks;
    using MachineLearning.Controls;
    using MachineLearning.Models;
    using static Common.Extensions.RandomizerExtensions;

    public class MainViewModel : ViewModelBase
    {
        private double learningRate, momentum, currentError;
        private int totalEpochs, currentEpoch;

        private readonly NetworkDataBag trainingDataSet, testDataSet;

        private DeepBeliefNetwork network;
        private ISupervisedLearning teacher;

        private readonly double[][] inputVector, outputVector;

        private bool canTestNetwork, canModifyNetwork;
        private string testExpectedOutput, testActualOutput;

        // https://github.com/accord-net/framework/tree/master/Samples/Neuro/Deep%20Learning
        public MainViewModel(NetworkDataBag trainingDataSet, NetworkDataBag testDataSet)
        {
            this.trainingDataSet = trainingDataSet ?? throw new ArgumentNullException(nameof(trainingDataSet));
            this.testDataSet = testDataSet ?? throw new ArgumentNullException(nameof(testDataSet));
            
            this.network = new DeepBeliefNetwork(new BernoulliFunction(), WineData.TotalAvailableInputs, hiddenNeurons: WineData.TotalAvailableOutputs);

            this.ToInitialSettings();

            double[][] inputs = new double[trainingDataSet.Data.Count()][],
                outputs = new double[trainingDataSet.Data.Count()][];

            for (int i = 0; i < inputs.Length; i++)
            {
                var dataSet = trainingDataSet.Data.ElementAt(i);

                inputs[i] = dataSet.Input;
                outputs[i] = dataSet.Output;
            }

            this.inputVector = inputs;
            this.outputVector = outputs;
        }
        
        public double LearningRate
        {
            get { return this.learningRate; }
            set { this.SetField(ref this.learningRate, value, nameof(this.LearningRate)); }
        }

        public double Momentum
        {
            get { return this.momentum; }
            set { this.SetField(ref this.momentum, value, nameof(this.Momentum)); }
        }

        public int TotalEpochs
        {
            get { return this.totalEpochs; }
            set { this.SetField(ref this.totalEpochs, value, nameof(this.TotalEpochs)); }
        }
        
        public int CurrentEpoch
        {
            get { return this.currentEpoch; }
            set { this.SetField(ref this.currentEpoch, value, nameof(this.CurrentEpoch)); }
        }

        public double CurrentError
        {
            get { return this.currentError; }
            set { this.SetField(ref this.currentError, value, nameof(this.CurrentError)); }
        }

        public bool CanModifyNetwork
        {
            get { return this.canModifyNetwork; }
            private set { this.SetField(ref this.canModifyNetwork, value, nameof(this.CanModifyNetwork)); }
        }

        public bool CanTestNetwork
        {
            get { return this.canTestNetwork; }
            private set { this.SetField(ref this.canTestNetwork, value, nameof(this.CanTestNetwork)); }
        }

        public string TestExpectedOutput
        {
            get { return this.testExpectedOutput; }
            private set { this.SetField(ref this.testExpectedOutput, value, nameof(this.TestExpectedOutput)); }
        }

        public string TestActualOutput
        {
            get { return this.testActualOutput; }
            private set { this.SetField(ref this.testActualOutput, value, nameof(this.TestActualOutput)); }
        }

        public async void Start()
        {
            this.CanModifyNetwork = false;

            await Task.Run(() =>
            {
                for (int i = 0; i < this.totalEpochs; i++)
                {
                    double error = this.teacher.RunEpoch(this.inputVector, this.outputVector);

                    this.CurrentEpoch += 1;
                    this.CurrentError = error;
                }
            });

            this.network.UpdateVisibleWeights();

            this.CanModifyNetwork = true;
            this.CanTestNetwork = true;
        }

        public void Reset()
        {
            this.ToInitialSettings();
        }
        
        public void TestNetworkWithRandomTestData()
        {
            NetworkInputOutputData data = this.testDataSet.Data.GetRandomElement();

            var output = this.network.Compute(data.Input);

            this.TestExpectedOutput = data.Output.Select(d => d.ToString()).Aggregate((curr, next) => curr + "-" + next);
            this.TestActualOutput = output.Select(d => Math.Round(d, 3).ToString()).Aggregate((curr, next) => curr + "-" + next);
        }

        private void ToInitialSettings()
        {
            this.Momentum = 0.9;
            this.LearningRate = 0.3;
            this.TotalEpochs = 2000;

            this.CurrentEpoch = 0;
            this.CurrentError = 0;

            this.CanModifyNetwork = true;
            this.CanTestNetwork = false;

            new GaussianWeights(this.network).Randomize();
            this.network.UpdateVisibleWeights();

            this.teacher = new BackPropagationLearning(this.network)
            {
                LearningRate = this.learningRate,
                Momentum = this.momentum
            };
        }
    }
}