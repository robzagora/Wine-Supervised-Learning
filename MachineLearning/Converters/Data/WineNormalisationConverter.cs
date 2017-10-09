namespace MachineLearning.Converters.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure.Conversion;
    using MachineLearning.Models;
    using static Common.Extensions.MathExtensions;


    public class WineNormalisationConverter : IConvert<IEnumerable<WineData>, IEnumerable<WineNormalisedData>>
    {
        public IEnumerable<WineNormalisedData> Convert(IEnumerable<WineData> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Source data is null");
            }

            WineNormalisationRange range = new WineNormalisationRange
            {
                Alcohol = new MinMaxRange(source.Min(d => d.Alcohol), source.Max(d => d.Alcohol)),
                MalicAcid = new MinMaxRange(source.Min(d => d.MalicAcid), source.Max(d => d.MalicAcid)),
                Ash = new MinMaxRange(source.Min(d => d.Ash), source.Max(d => d.Ash)),
                AshAlcalinity = new MinMaxRange(source.Min(d => d.AshAlcalinity), source.Max(d => d.AshAlcalinity)),
                Magnesium = new MinMaxRange(source.Min(d => d.Magnesium), source.Max(d => d.Magnesium)),
                TotalPhenols = new MinMaxRange(source.Min(d => d.TotalPhenols), source.Max(d => d.TotalPhenols)),
                Flavanoids = new MinMaxRange(source.Min(d => d.Flavanoids), source.Max(d => d.Flavanoids)),
                NonflavanoidPhenols = new MinMaxRange(source.Min(d => d.NonflavanoidPhenols), source.Max(d => d.NonflavanoidPhenols)),
                Proanthocyanins = new MinMaxRange(source.Min(d => d.Proanthocyanins), source.Max(d => d.Proanthocyanins)),
                ColorIntensity = new MinMaxRange(source.Min(d => d.ColorIntensity), source.Max(d => d.ColorIntensity)),
                Hue = new MinMaxRange(source.Min(d => d.Hue), source.Max(d => d.Hue)),
                DilutedWines = new MinMaxRange(source.Min(d => d.DilutedWines), source.Max(d => d.DilutedWines)),
                Proline = new MinMaxRange(source.Min(d => d.Proline), source.Max(d => d.Proline))
            };

            return source.Select(s => new WineNormalisedData
            {
                Class = s.Class,
                Alcohol = s.Alcohol.RescaleNormalization(range.Alcohol.Min, range.Alcohol.Max),
                Ash = s.Ash.RescaleNormalization(range.Ash.Min, range.Ash.Max),
                AshAlcalinity = s.AshAlcalinity.RescaleNormalization(range.AshAlcalinity.Min, range.AshAlcalinity.Max),
                ColorIntensity = s.ColorIntensity.RescaleNormalization(range.ColorIntensity.Min, range.ColorIntensity.Max),
                DilutedWines = s.DilutedWines.RescaleNormalization(range.DilutedWines.Min, range.DilutedWines.Max),
                Flavanoids = s.Flavanoids.RescaleNormalization(range.Flavanoids.Min, range.Flavanoids.Max),
                Hue = s.Hue.RescaleNormalization(range.Hue.Min, range.Hue.Max),
                Magnesium = s.Magnesium.RescaleNormalization(range.Magnesium.Min, range.Magnesium.Max),
                MalicAcid = s.MalicAcid.RescaleNormalization(range.MalicAcid.Min, range.MalicAcid.Max),
                NonflavanoidPhenols = s.NonflavanoidPhenols.RescaleNormalization(range.NonflavanoidPhenols.Min, range.NonflavanoidPhenols.Max),
                Proanthocyanins = s.Proanthocyanins.RescaleNormalization(range.Proanthocyanins.Min, range.Proanthocyanins.Max),
                Proline = s.Proline.RescaleNormalization(range.Proline.Min, range.Proline.Max),
                TotalPhenols = s.TotalPhenols.RescaleNormalization(range.TotalPhenols.Min, range.TotalPhenols.Max)
            });
        }
    }
}