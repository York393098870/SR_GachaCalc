using NewCoreLibrary.SinglePool;

namespace NewCoreLibrary.CalculateForTarget;

public class GachaForTargetCounts(
    int targetAmountOfLimitedCharacter,
    int targetAmountOfLimitedWeapon,
    bool isLastCharacterFailed = false,
    bool isLastWeaponFailed = false) //使用主构造函数

{
    private int TargetAmountOfLimitedCharacter { get; } = targetAmountOfLimitedCharacter;
    private int TargetAmountOfLimitedWeapon { get; } = targetAmountOfLimitedWeapon;
    private bool IsLastCharacterFailed { get; } = isLastCharacterFailed;
    private bool IsLastWeaponFailed { get; } = isLastWeaponFailed;


    public (int AmountOfTotal, int AmountOfNormalCharacters, int AmountOfNormalWeapons, int AmountOfGachaInCharacterPool
        , int AmountOfGachaInWeaponPool) Calculate()
    {
        var amountOfNormalCharacter = 0;
        var amountOfGachaInCharacterPool = 0;
        var amountOfNormalWeapon = 0;
        var amountOfGachaInWeaponPool = 0;

        //Up角色池
        if (TargetAmountOfLimitedCharacter >= 1)
        {
            (amountOfNormalCharacter, amountOfGachaInCharacterPool) = Target.Calculate(
                targetAmount: TargetAmountOfLimitedCharacter,
                thingName: "LimitedCharacter",
                probabilityWhenSucceed: GachaData.CalculateGachaProbability.ProbabilityForLimitedCharacterWhenSucceed,
                lastTryFailed: IsLastCharacterFailed);
        }

        //Up光锥池
        if (TargetAmountOfLimitedWeapon >= 1)
        {
            (amountOfNormalWeapon, amountOfGachaInWeaponPool) = Target.Calculate(
                targetAmount: TargetAmountOfLimitedWeapon,
                thingName: "LimitedWeapon",
                probabilityWhenSucceed: GachaData.CalculateGachaProbability.ProbabilityForLimitedWeaponWhenSucceed,
                lastTryFailed: IsLastWeaponFailed);
        }

        var amountOfTotal = amountOfGachaInCharacterPool + amountOfGachaInWeaponPool;

        return (AmountOfTotal: amountOfTotal, AmountOfNormalCharacters: amountOfNormalCharacter,
            AmountOfNormalWeapons: amountOfNormalWeapon, AmountOfGachaInCharacterPool: amountOfGachaInCharacterPool,
            AmountOfGachaInWeaponPool: amountOfGachaInWeaponPool);
    }
}