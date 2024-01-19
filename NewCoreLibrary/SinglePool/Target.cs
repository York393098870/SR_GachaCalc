﻿namespace NewCoreLibrary.SinglePool;

public static class Target
{
    //给定目标数量，计算抽取需要的抽数
    public static (int AmountOfNormalThing, int AmountOfGacha) CalculateFromZero(int targetAmount, string thingName,
        double probabilityWhenSucceed /*歪常驻的概率*/, bool lastTryFailed = false)
    {
        var isLastTryFailed = lastTryFailed;
        var amountOfNormalThing = 0;
        var n = 0; //循环内的计数器
        var amountOfGacha = 0; //循环外的计数器

        for (var i = 0; i < targetAmount;)
        {
            //抽卡部分
            n++;
            amountOfGacha++;

            //获得五星后的判定部分
            if (!Tools.Lottery.CheckIfSucceed(
                    GachaData.GetProbability.Probability(n, thingName))) continue;
            n = 0; //重置循环内计数器
            //出金后的逻辑
            if (isLastTryFailed)
            {
                //有大保底，直接获得限定，并清空保底
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


        return (AmountOfNormalThing: amountOfNormalThing, AmountOfGacha: amountOfGacha);
    }
}