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
}