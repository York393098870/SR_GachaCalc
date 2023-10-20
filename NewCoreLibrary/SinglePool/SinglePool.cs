namespace NewCoreLibrary.SinglePool;

using static NewCoreLibrary.Tools;

public abstract class SinglePool
//用于模拟单个池子抽奖的类
{
    protected int TotalGachaTimes;
    protected bool IsLastTryFailed; //大保底

    protected SinglePool(int totalGachaTimes, bool isLastTryFailed = false)
    {
        if (totalGachaTimes < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(totalGachaTimes), "总抽卡次数不允许小于1");
        }

        TotalGachaTimes = totalGachaTimes;
        IsLastTryFailed = isLastTryFailed;
    }

    protected virtual double GetProbability(int i)
    {
        throw new NotSupportedException("该方法只允许在基类中被使用");
    }

    protected virtual double GetLimitedRateWhenSucceed()
    {
        throw new NotSupportedException("该方法只允许在基类中被使用");
    }

    public (int, int) CalculateByGachaTimes()
    {
        var LimitedFiveStarCount = 0;
        var NormalFiveStarCount = 0;

        for (var i = 1;
             i <= TotalGachaTimes;
             i++)
        {
            //判断单次抽卡是否成功
            if (!Lottery.CheckIfSucceed(GetProbability(i))) continue;
            //判断保底，有保底则直接强娶
            if (IsLastTryFailed)
            {
                LimitedFiveStarCount++;
                IsLastTryFailed = false; //清空已有的大保底
            }
            else
            {
                //无保底的情况下判断是否歪常驻
                if (Lottery.CheckIfSucceed(GetLimitedRateWhenSucceed()))
                {
                    //没歪常驻
                    LimitedFiveStarCount++;
                }
                else
                {
                    //歪常驻
                    NormalFiveStarCount++;
                    IsLastTryFailed = true; //增加大保底
                }
            }
        }

        return (LimitedFiveStarCount, NormalFiveStarCount);
    }
}