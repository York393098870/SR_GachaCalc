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
                return
                    $"模拟Up角色池抽卡{totalGachaTimes}次的情况，获得限定五星角色{resultOfLimitedCharacterPool.Item1}个，常驻五星角色{resultOfLimitedCharacterPool.Item2}个";
                break;
            case PoolType.LimitedWeaponPool:
                var limitedWeaponPool = new LimitedWeaponPool(totalGachaTimes, isLastTryFailed);
                var resultOfLimitedWeaponPool = limitedWeaponPool.CalculateByGachaTimes();
                return
                    $"模拟Up光锥池抽卡{totalGachaTimes}次的情况，获得限定五星光锥{resultOfLimitedWeaponPool.Item1}把，常驻五星光锥{resultOfLimitedWeaponPool.Item2}把";
                break;
            default:
                throw new ArgumentException("未知的池子类型");
        }
    }
}