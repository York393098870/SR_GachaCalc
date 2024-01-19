using NewCoreLibrary.CalculateForTarget;

namespace NewCoreLibrary.LuckyValue;

public class LuckyCalculate
{
    public double Calculate(int targetAmountOfLimitedCharacters, bool lastCharacterNormal,
        int targetAmountOfLimitedWeapons, bool lastWeaponNormal, int gachaTimes, int accuracy = 0)
    {
        List<int> simulateResult = [];

        var simulateTimes = accuracy switch
        {
            0 => 100000,
            1 => 1000000,
            2 => 10000000,
            _ => throw new ArgumentException(nameof(accuracy) + "出现问题，其值不在允许的范围内")
        };

        for (var i = 0; i < simulateTimes; i++)
        {
            var calculator = new GachaForTargetCounts(
                targetAmountOfLimitedCharacter: targetAmountOfLimitedCharacters,
                targetAmountOfLimitedWeapon: targetAmountOfLimitedWeapons,
                isLastCharacterFailed: lastCharacterNormal,
                isLastWeaponFailed: lastWeaponNormal);
            var result = calculator.Calculate();
            simulateResult.Add(result.AmountOfTotal);
        }

        simulateResult.Add(gachaTimes);
        simulateResult.Sort();
        Console.WriteLine($"日志：循环平均数：{simulateResult.Average()}");

        var index = simulateResult.IndexOf(gachaTimes);
        var percentAgeRank = (double)index / simulateResult.Count;
        return percentAgeRank;
    }
}