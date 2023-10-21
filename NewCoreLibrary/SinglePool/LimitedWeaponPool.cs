namespace NewCoreLibrary.SinglePool;

using static NewCoreLibrary.GachaData.GachaProbability;

public class LimitedWeaponPool : SinglePool
{
    public LimitedWeaponPool(int totalGachaTimes, bool isLastTryFailed=false) : base(totalGachaTimes, isLastTryFailed)
    {
    }

    protected override double GetProbability(int i)
    {
        return ProbabilityForLimitedWeaponPool[i];
    }

    protected override double GetLimitedRateWhenSucceed()
    {
        return ProbabilityForLimitedWeaponWhenSucceed;
    }
}