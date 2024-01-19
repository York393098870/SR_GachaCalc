namespace NewCoreLibrary.SinglePool;

using static Tools;

public abstract class SinglePool
//用于模拟单个池子抽奖的类
{
    private int TotalGachaTimes { get; }

    private bool IsLastTryFailed { get; set; }

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

//总体抽卡逻辑：先判断单次抽卡是否成功，若成功则判断是否存在大保底，若存在大保底则直接获得限定，若不存在大保底则判断是否歪常驻，若歪常驻则增加大保底，若不歪常驻则获得限定
    public (int, int) CalculateByGachaTimes()
    {
        var limitedFiveStarCount = 0;
        var normalFiveStarCount = 0;

        var n = 1; //循环内的计数器，用于判断单抽的概率

        for (var i = 1;
             i <= TotalGachaTimes;
             i++) //循环外的计数器，用于判断是否达到总抽卡次数
        {
            //判断单次抽卡是否成功
            if (Lottery.CheckIfSucceed(GetProbability(n)))
            {
                //出金时的逻辑
                n = 1; //重置循环内计数器
                if (IsLastTryFailed) //判断保底，有保底则直接获得限定
                {
                    limitedFiveStarCount++;
                    IsLastTryFailed = false; //清空已有的大保底，重置循环
                }
                else
                {
                    //无保底的情况下判断是否歪常驻
                    if (Lottery.CheckIfSucceed(GetLimitedRateWhenSucceed()))
                    {
                        //没歪常驻
                        limitedFiveStarCount++;
                    }
                    else
                    {
                        //歪常驻
                        normalFiveStarCount++;
                        IsLastTryFailed = true; //增加大保底
                    }
                }
            }
            else //单次抽卡没有出金的逻辑
            {
                n++; //单次抽卡失败，循环内计数器+1
            }
        }

        return (limitedFiveStarCount, normalFiveStarCount);
    }
}