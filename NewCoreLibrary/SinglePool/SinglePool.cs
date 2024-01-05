namespace NewCoreLibrary.SinglePool;

using static Tools;

public abstract class SinglePool
//用于模拟单个池子抽奖的类
{
    private int _totalGachaTimes;
    private bool _isLastTryFailed; //是否存在大保底

    public int TotalGachaTimes
    {
        get => _totalGachaTimes;

        protected set => _totalGachaTimes = value;
    }

    public bool IsLastTryFailed
    {
        get => _isLastTryFailed;
        protected set => _isLastTryFailed = value;
    }

    protected SinglePool(int totalGachaTimes, bool isLastTryFailed = false)
    {
        if (totalGachaTimes < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(totalGachaTimes), "总抽卡次数不允许小于1");
        }

        _totalGachaTimes = totalGachaTimes;
        _isLastTryFailed = isLastTryFailed;
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
        var limitedFiveStarCount = 0;
        var normalFiveStarCount = 0;

        var n = 1;

        for (var i = 1;
             i <= _totalGachaTimes;
             i++)
        {
            //判断单次抽卡是否成功
            if (Lottery.CheckIfSucceed(GetProbability(n)))
            {
                //出金时的逻辑
                //判断保底，有保底则直接获得
                if (_isLastTryFailed)
                {
                    limitedFiveStarCount++;
                    _isLastTryFailed = false; //清空已有的大保底
                    n = 1; //重置循环
                }
                else
                {
                    //无保底的情况下判断是否歪常驻
                    if (Lottery.CheckIfSucceed(GetLimitedRateWhenSucceed()))
                    {
                        //没歪常驻
                        limitedFiveStarCount++;
                        n = 1; //重置循环
                    }
                    else
                    {
                        //歪常驻
                        normalFiveStarCount++;
                        _isLastTryFailed = true; //增加大保底
                        n = 1; //重置循环
                    }
                }
            }

            n++;
        }

        return (limitedFiveStarCount, normalFiveStarCount);
    }
}