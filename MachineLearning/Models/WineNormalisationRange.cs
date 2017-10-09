namespace MachineLearning.Models
{
    public class WineNormalisationRange
    {
        public MinMaxRange Alcohol { get; set; }

        public MinMaxRange MalicAcid { get; set; }

        public MinMaxRange Ash { get; set; }

        public MinMaxRange AshAlcalinity { get; set; }

        public MinMaxRange Magnesium { get; set; }

        public MinMaxRange TotalPhenols { get; set; }

        public MinMaxRange Flavanoids { get; set; }

        public MinMaxRange NonflavanoidPhenols { get; set; }

        public MinMaxRange Proanthocyanins { get; set; }

        public MinMaxRange ColorIntensity { get; set; }

        public MinMaxRange Hue { get; set; }

        public MinMaxRange DilutedWines { get; set; }

        public MinMaxRange Proline { get; set; }
    }
}