using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;

namespace CoreLibraries;

public static class ProbabilityDistributionPlotter
{
    public static (double[] doubleSortedGachaTimes, double[] kdevalues) PlotProbabilityDistribution(
        int targetAmountsOfLimitedCharacter,
        int targetAmountsOfLimitedWeapon, int simulationTimes = 100000, bool outputPlot = false)
    {
        var resultOfGachaTimes = new List<int>();
        for (var i = 1; i <= simulationTimes; i++)
        {
            resultOfGachaTimes.Add(GachaCalcByTargetCounts
                .GachaCalc(targetAmountsOfLimitedCharacter, targetAmountsOfLimitedWeapon).totalGachaTimes);
        }

        var counts = new Dictionary<int, int>();

        foreach (var result in resultOfGachaTimes)
        {
            if (counts.ContainsKey(result))
            {
                counts[result]++;
            }
            else
            {
                counts[result] = 1;
            }
        }

        var sortedCounts = counts.OrderBy(pair => pair.Key).ToList();

        var sortedGachaTimes = new int[sortedCounts.Count];
        var probabilities = new double[sortedCounts.Count];
        for (var i = 0; i < sortedCounts.Count; i++)
        {
            sortedGachaTimes[i] = sortedCounts[i].Key;
            probabilities[i] = (double)sortedCounts[i].Value / simulationTimes;
        }

        var doubleSortedGachaTimes = sortedGachaTimes.Select(x => (double)x).ToArray();

        var defaultPlot = new ScottPlot.Plot(1600, 1200);
        defaultPlot.AddScatter(doubleSortedGachaTimes, probabilities);
        defaultPlot.Title($"获取{targetAmountsOfLimitedCharacter}只限定角色和{targetAmountsOfLimitedWeapon}只限定光锥的概率密度函数分布曲线");


        var kernel = new ContinuousUniform();

        var bandwidth = Math.Sqrt(doubleSortedGachaTimes.Variance()) *
                        Math.Pow((4.0 / 3.0 / resultOfGachaTimes.Count), 1.0 / 5.0); // 使用Silverman's rule of thumb来计算带宽
        var kdeValues = new double[doubleSortedGachaTimes.Length];
        for (var i = 0; i < doubleSortedGachaTimes.Length; i++)
        {
            kdeValues[i] =
                resultOfGachaTimes.Average(x => kernel.Density((x - doubleSortedGachaTimes[i]) / bandwidth)) /
                bandwidth;
        }

        var newPlot = new ScottPlot.Plot(1600, 1200);
        newPlot.AddScatter(doubleSortedGachaTimes, kdeValues);
        newPlot.Title(
            $"核密度优化后,获取{targetAmountsOfLimitedCharacter}只限定角色和{targetAmountsOfLimitedWeapon}只限定光锥的概率密度函数分布曲线");

        if (!outputPlot) return (doubleSortedGachaTimes, kdeValues);

        defaultPlot.SaveFig("标准散点图.png");
        newPlot.SaveFig("KDE.png");

        return (doubleSortedGachaTimes, kdeValues);
    }
}