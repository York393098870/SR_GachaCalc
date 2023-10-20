﻿namespace NewCoreLibrary.SinglePool;

using static NewCoreLibrary.GachaData.GachaProbability;

public class LimitedCharacterPool : SinglePool
{
    public LimitedCharacterPool(int totalGachaTimes, bool isLastTryFailed) : base(totalGachaTimes, isLastTryFailed)
    {
    }

    protected override double GetProbability(int i)
    {
        return ProbabilityForLimitedCharacterPool[i];
    }

    protected override double GetLimitedRateWhenSucceed()
    {
        return ProbabilityForLimitedCharacterWhenSucceed;
    }
}