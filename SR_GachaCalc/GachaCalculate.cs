using System;

namespace SR_GachaCalc;

public static class GachaCalculate
{
    //从上一次出金至下一次出金之间定义为一个循环

    private static Random _random = new Random();
    private static bool _isLastResultSuccess = true; //判断是否存在大保底，若结果为True则无大保底
    private static double _getlimitedrate = 0.5; //定义获得限定的概率

    //传入单次五星循环内抽卡的次数，获取单抽的概率
    private static decimal get_succeed_probability(int singleGachaTimes)
    {
        decimal pValue;
        switch (singleGachaTimes)
        {
            //判断已抽卡次数小于0的情况，并返回错误代码
            case < 1:
                Console.WriteLine("Invalid number in put for single circle calculate.");
                Environment.Exit(1); //返回错误代码1，单次循环内输入不允许小于1
                return -1m;

            //判断正常概率的情况（1-73次抽卡的情况）
            case >= 1 and <= 73:
                pValue = (0.6m) / 100m;
                return pValue;
            //判断概率提升时的状况（74-89次抽卡的情况）
            case <= 89:
                pValue = (0.6m + 6m * (singleGachaTimes - 73)) / 100m;
                return pValue;
            //判断保底时的状况（第90次抽卡的情况）
            case 90:
                pValue = 1m;
                return pValue;
            default:
                Console.WriteLine("Invalid number in put for single circle calculate.");
                Environment.Exit(1); //返回错误代码1，单次循环内不允许的输入
                return -1m;
        }
    }

    private static bool get_single_result(decimal probability)
    {
        return _random.NextDouble() < (double)probability;
    }

    public static (int, int) Gacha(int times)
    {
        var limitedFiveStarCount = 0;
        var normalFiveStarCount = 0;
        var n = 1; //n为一个循环内的抽卡次数
        for (var i = 1; i <= times; i++) //i为总抽卡次数

        {
            //判断是否出金
            if (get_single_result(get_succeed_probability(n)))
            {
                //判断大保底的情况
                if (_isLastResultSuccess)
                {
                    //没有大保底
                    if (_random.NextDouble() < 1 - _getlimitedrate) //判断是否歪常驻
                    {
                        //歪常驻，为下一次添加大保底
                        normalFiveStarCount++;
                        _isLastResultSuccess = false; //下一次默认为大保底
                    }
                    else
                    {
                        //没有歪常驻的情况，保底情况不做改动
                        limitedFiveStarCount++;
                    }
                }
                else
                {
                    limitedFiveStarCount++;
                    _isLastResultSuccess = true; //清空保底
                }

                n = 1; //开始新一轮循环
            }
            else
            {
                n++;
            }
        }

        return (limitedFiveStarCount, normalFiveStarCount);
    }
}