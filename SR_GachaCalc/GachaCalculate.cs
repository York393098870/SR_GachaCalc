﻿using System;

namespace SR_GachaCalc;

public class GachaCalculate
{
    private static int total_gacha_times; //定义总抽卡次数
    private static Random _random = new Random();

    //传入单次五星循环内抽卡的次数，获取单抽的概率
    public static decimal get_succeed_probability(int singleGachaTimes)
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

    public static bool get_single_result(decimal probability)
    {
        return _random.NextDouble() < (double)probability;
    }

    public static int gacha(int times)
    {
        total_gacha_times = times;
        int success_times = 0;
        var n = 1; //n为出金循环内抽卡次数
        for (var i = 1; i <= times; i++) //i为总抽卡次数
        {
            if (get_single_result(get_succeed_probability(n)))
            {
                success_times++;
                n = 1; //重置出金循环n次数
            }
            else
            {
                n++;
            }
        }

        return success_times;
    }
}