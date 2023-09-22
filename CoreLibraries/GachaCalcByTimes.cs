﻿namespace CoreLibraries;

public static class GachaCalcByTimes
{
    //从上一次出金至下一次出金之间定义为一个循环

    private static Random _random = new Random();
    private static bool _isLastResultSuccess = true; //判断是否存在大保底，若结果为True则无大保底
    private static double _getlimitedrate = 0.5; //定义获得限定的概率

    //传入单次五星循环内抽卡的次数，获取单抽的概率


    private static bool get_single_result(double probability)
    {
        return _random.NextDouble() < probability;
    }

    public static (int, int) Gacha(int times)
    {
        var limitedFiveStarCount = 0;
        var normalFiveStarCount = 0;
        var n = 1; //n为一个循环内的抽卡次数
        for (var i = 1; i <= times; i++) //i为总抽卡次数

        {
            //判断是否出金
            if (get_single_result(GachaCalcTools.getSucceedProbabilityofRole(n)))
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