namespace NewCoreLibrary.GachaData;

public static class GetProbability
{
    public static double Probability(int n, string thingName)
    {
        return thingName switch
        {
            "LimitedCharacter" => CalculateGachaProbability.ProbabilityForLimitedCharacterPool[n],
            "LimitedWeapon" => CalculateGachaProbability.ProbabilityForLimitedWeaponPool[n],
            _ => throw new ArgumentOutOfRangeException(nameof(thingName), "不允许的抽奖池类型")
        };
    }
}