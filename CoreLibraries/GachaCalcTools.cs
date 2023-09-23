namespace CoreLibraries;

//抽卡计算所用的工具类
public static class GachaCalcTools
{
    //定义不歪常驻的概率
    private static double _getLimitedRoleRate = 0.5;
    private static double _getLimitedWeaponRate = 0.75;

    //保底逻辑的存储
    private static bool _hasPityofRole = false;
    private static bool _hasPityofWeapon = false;

    //角色Up池的抽奖概率计算
    public static double getSucceedProbabilityofRole(int singleGachaTimes) //判断单次抽卡循环内，单次抽奖成功的概率p
    {
        double pValue;
        switch (singleGachaTimes)
        {
            //判断已抽卡次数小于0的情况，并返回错误代码
            case < 1:
                Console.WriteLine("Invalid number in put for single circle calculate.");
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
                Console.WriteLine("Invalid number in put for single circle calculate.");
                Environment.Exit(1); //返回错误代码1，单次循环内不允许的输入
                return -1d;
        }
    }

    //武器Up池的抽奖计算
    public static double getSucceedProbabilityofWeapon(int singleGachaTimes)
    {
        double pValue;
        switch (singleGachaTimes)
        {
            //判断已抽卡次数小于0的情况，并返回错误代码
            case < 1:
                Console.WriteLine("Invalid number in put for single circle calculate.");
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
                Console.WriteLine("Invalid number in put for single circle calculate.");
                Environment.Exit(1); //返回错误代码1，单次循环内不允许的输入
                return -1d;
        }
    }


    public static (double, double) getLimitedRate()
    {
        return (_getLimitedRoleRate, _getLimitedWeaponRate);
    }
}