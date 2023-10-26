using System.Diagnostics;

namespace NewCoreLibrary.SinglePool;

public static class Target
{
    public static (int AmountOfNormalThing, int AmountOfGacha) Calculate(int targetAmount, string thingName,
        double probabilityWhenSucceed,
        int alreadyGachaTimes = 0, bool lastTryFailed = false)
    {
        var isLastTryFailed = lastTryFailed;
        var amountOfNormalThing = 0;
        var n = alreadyGachaTimes;


        for (var i = 0; i < targetAmount;)
        {
            n++;
            if (!Tools.Lottery.CheckIfSucceed(
                    GachaData.GetProbability.Probability(n, thingName))) continue;
            //出金后的逻辑
            if (isLastTryFailed)
            {
                //有保底则直接获得限定角色并清空保底
                i++;
                isLastTryFailed = false;
            }
            else
            {
                if (Tools.Lottery.CheckIfSucceed(probabilityWhenSucceed))
                {
                    i++;
                }
                else
                {
                    //歪常驻
                    amountOfNormalThing++;
                }
            }
        }

        var amountOfGacha = n - alreadyGachaTimes;

        return (AmountOfNormalThing: amountOfNormalThing, AmountOfGacha: amountOfGacha);
    }
}