namespace MachineLearning.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using MachineLearning.Attributes;

    public class WineData
    {
        private static readonly int totalAvailableInputs;

        private static string classOne = "1",
            classTwo = "2",
            classThree = "3";

        private static readonly IReadOnlyCollection<string> Classes;

        static WineData()
        {
            WineData.totalAvailableInputs = typeof(WineData)
                .GetProperties()
                .Count(p => p.GetCustomAttribute<NeuralInputAttribute>() != null);

            WineData.Classes = new[]
            {
                WineData.classOne,
                WineData.classTwo,
                WineData.classThree
            };
        }

        public const sbyte WineClassIndex = 0,
            AlcoholIndex = 1,
            MalicAcidIndex = 2,
            AshIndex = 3,
            AshAlcalinityIndex = 4,
            MagnesiumIndex = 5,
            TotalPhenolsIndex = 6,
            FlavanoidsIndex = 7,
            NonflavanoidPhenolsIndex = 8,
            ProanthocyaninsIndex = 9,
            ColorIntensityIndex = 10,
            HueIndex = 11,
            DilutedWinesIndex = 12,
            ProlineIndex = 13;

        public static int TotalAvailableOutputs
        {
            get { return WineData.Classes.Count; }
        }

        public static int TotalAvailableInputs
        {
            get { return WineData.totalAvailableInputs; }
        }

        public static string ClassOne
        {
            get { return WineData.classOne; }
        }

        public static string ClassTwo
        {
            get { return WineData.classTwo; }
        }

        public static string ClassThree
        {
            get { return WineData.classThree; }
        }

        public double this[int index]
        {
            get
            {
                switch(index)
                {
                    case WineData.AlcoholIndex:
                        return this.Alcohol;
                    case WineData.MalicAcidIndex:
                        return this.MalicAcid;
                    case WineData.AshIndex:
                        return this.Ash;
                    case WineData.AshAlcalinityIndex:
                        return this.AshAlcalinity;
                    case WineData.MagnesiumIndex:
                        return this.Magnesium;
                    case WineData.TotalPhenolsIndex:
                        return this.TotalPhenols;
                    case WineData.FlavanoidsIndex:
                        return this.Flavanoids;
                    case WineData.NonflavanoidPhenolsIndex:
                        return this.NonflavanoidPhenols;
                    case WineData.ProanthocyaninsIndex:
                        return this.Proanthocyanins;
                    case WineData.ColorIntensityIndex:
                        return this.ColorIntensity;
                    case WineData.HueIndex:
                        return this.Hue;
                    case WineData.DilutedWinesIndex:
                        return this.DilutedWines;
                    case WineData.ProlineIndex:
                        return this.Proline;
                    default:
                        return -1;
                }
            }
        }

        public string Class { get; set; }

        [NeuralInput]
        public double Alcohol { get; set; }

        [NeuralInput]
        public double MalicAcid { get; set; }

        [NeuralInput]
        public double Ash { get; set; }

        [NeuralInput]
        public double AshAlcalinity { get; set; }

        [NeuralInput]
        public double Magnesium { get; set; }

        [NeuralInput]
        public double TotalPhenols { get; set; }

        [NeuralInput]
        public double Flavanoids { get; set; }

        [NeuralInput]
        public double NonflavanoidPhenols { get; set; }

        [NeuralInput]
        public double Proanthocyanins { get; set; }

        [NeuralInput]
        public double ColorIntensity { get; set; }

        [NeuralInput]
        public double Hue { get; set; }

        [NeuralInput]
        public double DilutedWines { get; set; }

        [NeuralInput]
        public double Proline { get; set; }
    }
}