namespace MachineLearning.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Timers;
    using Accord.Neuro;
    using Accord.Neuro.ActivationFunctions;
    using Accord.Neuro.Learning;
    using Accord.Neuro.Networks;
    using MachineLearning.Controls;
    using MachineLearning.Models;

    public class MainViewModel : ViewModelBase
    {
        private double learningRate, momentum, currentError;
        private int totalEpochs, currentEpoch;

        private DeepBeliefNetwork network;
        private ISupervisedLearning teacher;

        private readonly double[][] inputVector, outputVector;

        public MainViewModel(NetworkDataBag dataBag)
        {
            if (dataBag == null)
            {
                throw new ArgumentNullException(nameof(dataBag));
            }

            // https://github.com/accord-net/framework/tree/master/Samples/Neuro/Deep%20Learning

            this.network = new DeepBeliefNetwork(new BernoulliFunction(), WineData.TotalAvailableInputs, hiddenNeurons: WineData.TotalAvailableOutputs);

            new GaussianWeights(this.network).Randomize();
            this.network.UpdateVisibleWeights();

            this.learningRate = 0.3;
            this.momentum = 0.9;
            this.totalEpochs = 200;

            this.teacher = new BackPropagationLearning(this.network)
            {
                LearningRate = this.learningRate,
                Momentum = this.momentum
            };

            double[][] inputs = new double[dataBag.Data.Count()][],
                outputs = new double[dataBag.Data.Count()][];

            for (int i = 0; i < inputs.Length; i++)
            {
                var dataSet = dataBag.Data.ElementAt(i);

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

        public async void Start()
        {
            this.CurrentEpoch = 0;
            this.CurrentError = 0;

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
        }
    }
}