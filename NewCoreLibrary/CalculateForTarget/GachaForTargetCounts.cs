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
        //Up角色池
        var (amountOfNormalCharacter, amountOfGachaInCharacterPool) = Target.CalculateFromZero(
            targetAmount: TargetAmountOfLimitedCharacter,
            thingName: "LimitedCharacter",
            probabilityWhenSucceed: GachaData.CalculateGachaProbability.ProbabilityForLimitedCharacterWhenSucceed,
            lastTryFailed: IsLastCharacterFailed);

        //Up光锥池
        var (amountOfNormalWeapon, amountOfGachaInWeaponPool) = Target.CalculateFromZero(
            targetAmount: TargetAmountOfLimitedWeapon,
            thingName: "LimitedWeapon",
            probabilityWhenSucceed: GachaData.CalculateGachaProbability.ProbabilityForLimitedWeaponWhenSucceed,
            lastTryFailed: IsLastWeaponFailed);

        var amountOfTotal = amountOfGachaInCharacterPool + amountOfGachaInWeaponPool;
        return (AmountOfTotal: amountOfTotal, AmountOfNormalCharacters: amountOfNormalCharacter,
            AmountOfNormalWeapons: amountOfNormalWeapon, AmountOfGachaInCharacterPool: amountOfGachaInCharacterPool,
            AmountOfGachaInWeaponPool: amountOfGachaInWeaponPool);
    }
}