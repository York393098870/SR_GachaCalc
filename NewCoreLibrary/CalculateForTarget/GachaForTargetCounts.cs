using NewCoreLibrary.SinglePool;

namespace NewCoreLibrary.CalculateForTarget;

public class GachaForTargetCounts(
    int targetAmountOfLimitedCharacter,
    int targetAmountOfLimitedWeapon,
    int alreadyGachaTimes = 0,
    bool isLastCharacterFailed = false,
    bool isLastWeaponFailed = false) //使用主构造函数

{
    private int TargetAmountOfLimitedCharacter { get; } = targetAmountOfLimitedCharacter;
    private int TargetAmountOfLimitedWeapon { get; } = targetAmountOfLimitedWeapon;
    private int AlreadyGachaTimes { get; } = alreadyGachaTimes;
    private bool IsLastCharacterFailed { get; } = isLastCharacterFailed;
    private bool IsLastWeaponFailed { get; } = isLastWeaponFailed;


    public (int AmountOfTotal, int AmountOfNormalCharacters, int AmountOfNormalWeapons, int AmountOfGachaInCharacterPool, int AmountOfGachaInWeaponPool) Calculate()
    {
        //Up角色池
        var (amountOfNormalCharacter, amountOfGachaInCharacterPool) = Target.Calculate(
            targetAmount: TargetAmountOfLimitedCharacter,
            thingName: "LimitedCharacter",
            probabilityWhenSucceed: GachaData.CalculateGachaProbability.ProbabilityForLimitedCharacterWhenSucceed,
            alreadyGachaTimes: AlreadyGachaTimes, lastTryFailed: IsLastCharacterFailed);

        //Up光锥池
        var (amountOfNormalWeapon, amountOfGachaInWeaponPool) = Target.Calculate(
            targetAmount: TargetAmountOfLimitedWeapon,
            thingName: "LimitedWeapon",
            probabilityWhenSucceed: GachaData.CalculateGachaProbability.ProbabilityForLimitedWeaponWhenSucceed,
            alreadyGachaTimes: AlreadyGachaTimes, lastTryFailed: IsLastWeaponFailed);

        var amountOfTotal = amountOfGachaInCharacterPool + amountOfGachaInWeaponPool;
        return (AmountOfTotal: amountOfTotal, AmountOfNormalCharacters: amountOfNormalCharacter,
            AmountOfNormalWeapons: amountOfNormalWeapon, AmountOfGachaInCharacterPool: amountOfGachaInCharacterPool,
            AmountOfGachaInWeaponPool: amountOfGachaInWeaponPool);
    }
}