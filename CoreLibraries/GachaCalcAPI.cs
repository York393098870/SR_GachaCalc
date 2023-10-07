using System.Collections.Concurrent;
using System.Diagnostics;

namespace CoreLibraries;

public static class GachaCalcApi
{
    //本类用于提供API供GUI调用
    public static (int, int, string) GachaInPoolByTimes(int tryRoleTimes, int typeCode)
        //给定卡池类型和抽数，返回抽卡结果
    {
        string poolType;
        string thingType;
        string quantifier;
        switch (typeCode)
        {
            case 1:
                poolType = "role";
                thingType = "角色";
                quantifier = "只";
                break;
            case 2:
                poolType = "weapon";
                thingType = "武器";
                quantifier = "把";
                break;
            default:
                Environment.Exit(3);
                poolType = null;
                thingType = null;
                quantifier = null;
                break;
        }

        //给定抽数，计算在Up池中抽卡的情况
        var gachaResult = GachaCalcByTimes.GachaByTimes(tryRoleTimes, poolType);

        //计算限定角色/武器的平均抽数
        var averagelimitedcount = Math.Round((double)tryRoleTimes / gachaResult.Item1, 1);
        //返回消息
        var resultMessage =
            $"模拟{thingType}池抽卡{tryRoleTimes}次的情况：累计获得{gachaResult.Item1}{quantifier}限定{thingType}，{gachaResult.Item2}{quantifier}常驻{thingType}，平均每{averagelimitedcount}抽获得1{quantifier}限定{thingType}。";
        return (gachaResult.Item1, gachaResult.Item2, resultMessage);
    }

    public static (double, double, double) GachaLuckyValueCalculate(int characterCount, int weaponCount,
            int totalGachaTimes,
            int accuracyControl = 2)
        //给定限定角色/武器数量和总抽数，计算幸运值
    {
        int simulations;
        _ = accuracyControl switch
        {
            1 => simulations = 100000,
            2 => simulations = 1000000,
            3 => simulations = 10000000,
            _ => simulations = 100000
        };

        var allTries = new ConcurrentBag<int>();

        var stopwatch = Stopwatch.StartNew(); //开始计时
        Parallel.For(0, simulations, i =>
        {
            var gachaResult = GachaCalcByCounts.GachaCalc(characterCount, weaponCount);
            allTries.Add(gachaResult.gachaTimesOfRole + gachaResult.gachaTimesOfWeapon);
        });

        var sortedTries = allTries.OrderBy(x => x).ToList();

        // 找出玩家幸运值的位置
        var rank = sortedTries.Count(x => x < totalGachaTimes);

        // 计算百分比
        var percentile = (double)rank / simulations * 100;

        // 计算超越了多少玩家
        var surpassed = 100 - percentile;

        stopwatch.Stop();

        var timeUsed = Math.Round(stopwatch.ElapsedMilliseconds / 1000d, 2);

        return (percentile, surpassed, timeUsed);
    }
}