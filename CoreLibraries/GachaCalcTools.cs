namespace CoreLibraries;

//抽卡计算所用的工具类
public static class GachaCalcTools
{
    //预计算处理
    private static readonly double[] PrecalculatedProbabilitiesOfCharacter = new double[91];
    private static readonly double[] PrecalculatedProbabilitiesOfWeapon = new double[81];

    static GachaCalcTools()
    {
        //预计算角色池和光锥池的概率
        for (var i = 1; i <= 90; i++)
        {
            PrecalculatedProbabilitiesOfCharacter[i] = CalculateSucceedProbabilityOfCharacter(i);
        }

        for (var i = 1; i < 80; i++)
        {
            PrecalculatedProbabilitiesOfWeapon[i] = CalculateSucceedProbabilityOfWeapon(i);
        }
    }

    //获取预计算的概率

    public static double GetSucceedProbabilityOfCharacter(int singleGachaTimes)
    {
        return PrecalculatedProbabilitiesOfCharacter[singleGachaTimes];
    }

    public static double GetSucceedProbabilityOfWeapon(int singleGachaTimes)
    {
        return PrecalculatedProbabilitiesOfWeapon[singleGachaTimes];
    }


    //角色Up池的抽奖概率计算
    private static double CalculateSucceedProbabilityOfCharacter(int singleGachaTimes) //计算单次抽卡循环内，单次抽奖成功的概率p
    {
        double pValue;
        switch (singleGachaTimes)
        {
            //判断已抽卡次数小于0的情况，并返回错误代码
            case < 1:
                Console.WriteLine("CharacterError:Invalid number '1' in put for single circle calculate.");
                Environment.Exit(1); //返回错误代码1，单次循环内输入不允许小于1
                return -1;

            //判断正常概率的情况（1-73次抽卡的情况）
            case >= 1 and <= 73:
                pValue = (0.6) / 100d;
                return pValue;
            //判断概率提升时的状况（74-89次抽卡的情况）
            case <= 89:
                pValue = (0.6 + 6 * (singleGachaTimes - 73)) / 100d;
                return pValue;
            //判断保底时的状况（第90次抽卡的情况）
            case 90:
                pValue = 1;
                return pValue;

            default:
                Console.WriteLine(
                    $"CharacterError:Invalid number {singleGachaTimes} in put for single circle calculate.");
                Environment.Exit(1); //返回错误代码1，单次循环内不允许的输入
                return -1d;
        }
    }

    //武器Up池的抽奖计算
    private static double CalculateSucceedProbabilityOfWeapon(int singleGachaTimes)
    {
        double pValue;
        switch (singleGachaTimes)
        {
            //判断已抽卡次数小于0的情况，并返回错误代码
            case < 1:
                Console.WriteLine("WeaponError:Invalid number '1' in put for single circle calculate.");
                Environment.Exit(1); //返回错误代码1，单次循环内输入不允许小于1
                return -1;

            //判断正常概率情况(1-66)
            case <= 66:
                pValue = 0.8 / 100;
                return pValue;

            //第一次概率提升(67-70)
            case <= 70:
                pValue = (0.8 + (singleGachaTimes - 66) * 11.2) / 100;
                return pValue;

            //第二次概率提升（71-79)
            case <= 79:
                pValue = (0.8 + (70 - 66) * 11.2 + (singleGachaTimes - 70) * 5.6) / 100;
                return pValue;

            //保底（80）
            case 80:
                pValue = 1d;
                return pValue;

            default:
                Console.WriteLine("WeaponError:Invalid number in put for single circle calculate.");
                Environment.Exit(1); //返回错误代码1，单次循环内不允许的输入
                return -1d;
        }
    }
}