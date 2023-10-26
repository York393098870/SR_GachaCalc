using NewCoreLibrary.SinglePool;

namespace NewCoreLibrary.CalculateForTarget;

public class GachaForTargetCounts
{
    private int TargetAmountOfLimitedCharacter { get; }
    private int TargetAmountOfLimitedWeapon { get; }
    private int AlreadyGachaTimes { get; }
    private bool IsLastCharacterFailed { get; }
    private bool IsLastWeaponFailed { get; }


    public GachaForTargetCounts(int targetAmountOfLimitedCharacter, int targetAmountOfLimitedWeapon,
        int alreadyGachaTimes = 0, bool isLastCharacterFailed = false, bool isLastWeaponFailed = false)
    {
        TargetAmountOfLimitedCharacter = targetAmountOfLimitedCharacter;
        TargetAmountOfLimitedWeapon = targetAmountOfLimitedWeapon;
        AlreadyGachaTimes = alreadyGachaTimes;
        IsLastCharacterFailed = isLastCharacterFailed;
        IsLastWeaponFailed = isLastWeaponFailed;
    }

    public (int AmountOfTotal, int AmountOfNormalCharacter, int AmountOfNormalWeapon, int AmountOfGachaInCharacterPool,
        int AmountOfGachaInWeaponPool) Calculate()
    {
        //限定角色池
        var (amountOfNormalCharacter, amountOfGachaInCharacterPool) = Target.Calculate(
            targetAmount: TargetAmountOfLimitedCharacter,
            thingName: "LimitedCharacter",
            probabilityWhenSucceed: GachaData.CalculateGachaProbability.ProbabilityForLimitedCharacterWhenSucceed,
            alreadyGachaTimes: AlreadyGachaTimes, lastTryFailed: IsLastCharacterFailed);

        //限定武器池
        var (amountOfNormalWeapon, amountOfGachaInWeaponPool) = Target.Calculate(
            targetAmount: TargetAmountOfLimitedWeapon,
            thingName: "LimitedWeapon",
            probabilityWhenSucceed: GachaData.CalculateGachaProbability.ProbabilityForLimitedWeaponWhenSucceed,
            alreadyGachaTimes: AlreadyGachaTimes, lastTryFailed: IsLastWeaponFailed);

        var amountOfTotal = amountOfGachaInCharacterPool + amountOfGachaInWeaponPool;
        return (AmountOfTotal: amountOfTotal, AmountOfNormalCharacter: amountOfNormalCharacter,
            AmountOfNormalWeapon: amountOfNormalWeapon, AmountOfGachaInCharacterPool: amountOfGachaInCharacterPool,
            AmountOfGachaInWeaponPool: amountOfGachaInWeaponPool);
    }
}