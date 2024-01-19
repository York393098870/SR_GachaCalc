namespace NewCoreLibrary.SinglePool;

public static class Target
{
    //给定目标数量，计算抽取需要的抽数
    public static (int AmountOfNormalThing, int AmountOfGacha) Calculate(int targetAmount, string thingName,
        double probabilityWhenSucceed /*歪常驻的概率*/, bool lastTryFailed = false, int alreadyGachaTimes = 0)
    {
        var isLastTryFailed = lastTryFailed;
        var amountOfNormalThing = 0;
        var n = alreadyGachaTimes; //循环内的计数器
        var amountOfGacha = 0; //循环外的计数器

        for (var i = 0; i < targetAmount;)
        {
            //抽卡部分
            n++;
            amountOfGacha++;

            //获得五星后的判定部分
            if (!Tools.Lottery.CheckIfSucceed(
                    GachaData.GetProbability.Probability(n, thingName))) continue; //没抽到五星，进入下一次循环

            //抽到五星后的逻辑
            n = 0; //重置循环内计数器

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
                    //歪常驻，添加大保底
                    amountOfNormalThing++;
                    isLastTryFailed = true;
                }
            }
        }


        return (AmountOfNormalThing: amountOfNormalThing, AmountOfGacha: amountOfGacha);
    }
}