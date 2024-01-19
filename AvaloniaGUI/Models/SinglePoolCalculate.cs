using System;
using static AvaloniaGUI.Models.Tools;

namespace AvaloniaGUI.Models;

using NewCoreLibrary.SinglePool;

public class SinglePoolCalculate
{
    public static string Calculate(PoolType poolType, int totalGachaTimes, bool isLastTryFailed = false)
    {
        switch (poolType)
        {
            case PoolType.LimitedCharacterPool:
                var limitedCharacterPool = new LimitedCharacterPool(totalGachaTimes, isLastTryFailed);
                var resultOfLimitedCharacterPool = limitedCharacterPool.CalculateByGachaTimes();
                var timesPerLimitedFiveStarCharacter = totalGachaTimes / (double)resultOfLimitedCharacterPool.Item1;
                var ratioOfCharacters =
                    RatioCalculate(resultOfLimitedCharacterPool.Item1, resultOfLimitedCharacterPool.Item2);
                return
                    $"模拟Up角色池抽卡{totalGachaTimes}次的情况：\n获得限定五星角色{resultOfLimitedCharacterPool.Item1}个，\n常驻五星角色{resultOfLimitedCharacterPool.Item2}个，\n平均每{timesPerLimitedFiveStarCharacter:F2}抽获得一个限定五星角色，限定：常驻比例：{ratioOfCharacters}";
            case PoolType.LimitedWeaponPool:
                var limitedWeaponPool = new LimitedWeaponPool(totalGachaTimes, isLastTryFailed);
                var resultOfLimitedWeaponPool = limitedWeaponPool.CalculateByGachaTimes();
                var timesPerLimitedFiveStarWeapon = totalGachaTimes / (double)resultOfLimitedWeaponPool.Item1;
                var ratioOfWeapons = RatioCalculate(resultOfLimitedWeaponPool.Item1, resultOfLimitedWeaponPool.Item2);
                return
                    $"模拟Up光锥池抽卡{totalGachaTimes}次的情况，\n获得限定五星光锥{resultOfLimitedWeaponPool.Item1}把，\n常驻五星光锥{resultOfLimitedWeaponPool.Item2}把，\n平均每{timesPerLimitedFiveStarWeapon:F2}抽获得一把限定五星光锥，限定：常驻比例：{ratioOfWeapons}";
            case PoolType.UnknownPoolType:
                throw new ArgumentException("未知的池子类型");
            default:
                throw new ArgumentException("未知的池子类型（默认）");
        }
    }

    private static string RatioCalculate(int num1, int num2)
    {
        //三种情况，正常计算，num2为0
        if (num2 == 0)
        {
            return $"{num1}:{num2}";
        }

        var ratio = num1 / (double)num2;
        return $"{ratio:F1}:1";
    }
}