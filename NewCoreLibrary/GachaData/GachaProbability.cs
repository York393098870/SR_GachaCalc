namespace NewCoreLibrary.GachaData;

public static class GachaProbability
{
    public static double[] ProbabilityForLimitedCharacterPool = new double[91];
    public static double[] ProbabilityForLimitedWeaponPool = new double[81];
    public static double ProbabilityForLimitedCharacterWhenSucceed = 0.5;
    public static double ProbabilityForLimitedWeaponWhenSucceed = 0.75;

    static GachaProbability()
    {
        for (var i = 1; i <= 90; i++)
        {
            ProbabilityForLimitedCharacterPool[i] = CalculateProbability(i, "LimitedCharacterPool");
        }

        for (var i = 1; i <= 80; i++)
        {
            ProbabilityForLimitedWeaponPool[i] = CalculateProbability(i, "LimitedWeaponPool");
        }
    }

    private static double CalculateProbability(int n, string poolType)
    {
        return poolType switch
        {
            "LimitedCharacterPool" => n switch
            {
                >= 1 and <= 73 => (0.6d) / 100,
                <= 89 => ((0.6d) + 6.6d * (n - 73)) / 100,
                90 => 1,
                _ => throw new ArgumentOutOfRangeException(nameof(n), "单次循环当中，不允许的抽卡次数")
            },
            "LimitedWeaponPool" => n switch
            {
                >= 1 and <= 66 => 0.8d / 100,
                <= 70 => (0.8d + 11.2d * (n - 66)) / 100,
                <= 79 => (0.8d + 11.2d * (70 - 66) + 5.6d * (n - 70)) / 100,
                80 => 1,
                _ => throw new ArgumentOutOfRangeException(nameof(n), "单次循环当中，不允许的抽卡次数")
            },
            _ => throw new ArgumentOutOfRangeException(nameof(poolType), "不允许的抽奖池类型")
        };
    }
}