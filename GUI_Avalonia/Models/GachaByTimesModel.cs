namespace GUI_Avalonia.Models;

using NewCoreLibrary.SinglePool;
using System;

public class GachaByTimesModel
{
    public static string Process(int counts, int poolType, bool debugMode = false)
    {
        SinglePool singlePool = poolType switch
        {
            1 => new LimitedCharacterPool(counts),
            2 => new LimitedWeaponPool(counts),
            _ => throw new ArgumentException("Invalid pool type", nameof(poolType)),
        };
        var poolName = poolType switch
        {
            1 => "Up角色池",
            2 => "Up光锥池",
            _ => throw new ArgumentException("Invalid pool type", nameof(poolType)),
        };
        var thingName = poolType switch
        {
            1 => "角色",
            2 => "光锥",
            _ => throw new ArgumentException("Invalid pool type", nameof(poolType)),
        };

        var result = singlePool.CalculateByGachaTimes();
        var average = (double)counts / result.Item1;
        if (debugMode)
        {
            return
                $"模拟{poolName}抽卡{counts}次的情况，获得{result.Item1}个限定{thingName}，{result.Item2}个常驻{thingName}，平均{average:F1}抽获得一个限定{thingName}";
        }

        return
            $"模拟{poolName}抽卡{counts}次的情况：\r\n总计获得{result.Item1}个限定{thingName}，{result.Item2}个常驻{thingName}.\r\n平均{average:F1}抽获得一个限定{thingName}";
    }
}