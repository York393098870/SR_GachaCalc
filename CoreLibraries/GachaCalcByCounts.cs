namespace CoreLibraries;

//该部分主要以获得指定数量为目的，模拟抽卡结果
public class GachaCalcByCounts
{
    private static Random _random = new Random();

    private static bool get_single_result(double probability) //根据概率返回布尔结果
    {
        return _random.NextDouble() < probability;
    }

    public static int GachaCalc(int targetAmountsOfLimitedRole, int targetAmountsOfLimitedWeapon)
    {
        var _targetAmountsOfLimitedRole = targetAmountsOfLimitedRole;
        var _targetAmountsOfLimitedWeapon = targetAmountsOfLimitedWeapon;
        var normalRoleCount = 0;
        var normalWeaponCount = 0;
        var gachatimesofRole = 1;
        var gachatimesofWeapon = 0;

        //保底机制
        var _hasPityofRole = false;
        var _hasPityofWeapon = false;

        //先下角色池
        var m = 1; //m为循环内抽卡次数
        for (var i = 0; i < _targetAmountsOfLimitedRole;)
        {
            //先完成一次抽卡
            gachatimesofRole++;

            if (get_single_result(GachaCalcTools.getSucceedProbabilityofRole(m)))
            {
                //抽到五星时的情况
                m = 1; //只要获得五星，直接重置循环

                if (_hasPityofRole)
                {
                    //有大保底时的逻辑
                    i++;
                    _hasPityofRole = false; //清空已有的保底
                }
                else
                {
                    //无大保底时的逻辑
                    if (get_single_result(GachaCalcTools.getLimitedRate().Item1))
                    {
                        //出限定的情况
                        i++;
                    }
                    else
                    {
                        //歪常驻的情况
                        normalRoleCount++;
                        _hasPityofRole = true; //为下一次添加大保底
                    }
                }
            }
            else
            {
                //没有抽到五星的情况
                m++; //相当于垫池子
            }
        }

        return gachatimesofRole;
    }
}