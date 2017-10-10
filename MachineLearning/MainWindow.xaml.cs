namespace MachineLearning
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using MachineLearning.Converters.Data;
    using MachineLearning.Models;
    using MachineLearning.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            this.InitializeComponent();

            string path = Path.Combine(Environment.CurrentDirectory, "Data", "Wine");

            var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

            var filtered = files.Where(f =>
            {
                var name = Path.GetFileNameWithoutExtension(f);

                return name.EndsWith("training") || name.EndsWith("test");
            });

            var grouping = filtered.GroupBy(f => Path.GetFileNameWithoutExtension(f).EndsWith("training"));

            var trainingData = grouping.FirstOrDefault(g => g.Key == true);
            var testData = grouping.FirstOrDefault(g => g.Key == false);

            var lines = File.ReadAllLines(trainingData.Single()).Where(l => !string.IsNullOrWhiteSpace(l));

            var data = lines.Select(l =>
            {
                var split = l.Split(',');

                return new WineData
                {
                    Class = split[WineData.WineClassIndex],
                    Alcohol = Convert.ToDouble(split[WineData.AlcoholIndex]),
                    MalicAcid = Convert.ToDouble(split[WineData.MalicAcidIndex]),
                    Ash = Convert.ToDouble(split[WineData.AshIndex]),
                    AshAlcalinity = Convert.ToDouble(split[WineData.AshAlcalinityIndex]),
                    Magnesium = Convert.ToDouble(split[WineData.MagnesiumIndex]),
                    TotalPhenols = Convert.ToDouble(split[WineData.TotalPhenolsIndex]),
                    Flavanoids = Convert.ToDouble(split[WineData.FlavanoidsIndex]),
                    NonflavanoidPhenols = Convert.ToDouble(split[WineData.NonflavanoidPhenolsIndex]),
                    Proanthocyanins = Convert.ToDouble(split[WineData.ProanthocyaninsIndex]),
                    ColorIntensity = Convert.ToDouble(split[WineData.ColorIntensityIndex]),
                    Hue = Convert.ToDouble(split[WineData.HueIndex]),
                    DilutedWines = Convert.ToDouble(split[WineData.DilutedWinesIndex]),
                    Proline = Convert.ToDouble(split[WineData.ProlineIndex]),
                };
            })
            .ToArray();

            WineNormalisationConverter converter = new WineNormalisationConverter();
            IEnumerable<WineNormalisedData> normalisedData = converter.Convert(data);

            var dataBag = new NetworkDataBag(
                normalisedData.Select(d => new NetworkInputOutputData(d.Inputs, d.Outputs)).ToArray(),
                WineData.TotalAvailableInputs,
                WineData.TotalAvailableOutputs);

            this.viewModel = new MainViewModel(dataBag);

            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.Start();
        }
    }
}