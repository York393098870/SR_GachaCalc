namespace CoreLibraries;

//该部分主要以获得指定数量为目的，模拟抽卡结果
public static class GachaCalcByCounts
{
    private static ThreadLocal<Random> _random = new ThreadLocal<Random>(() => new Random());


    //给定希望获得的限定角色和武器数量，返回所需要的抽数
    public static (int normalRoleCount, int gachaTimesOfRole, int gachaTimesOfWeapon, int normalWeaponCount) GachaCalc(
        int targetAmountsOfLimitedRole, int targetAmountsOfLimitedWeapon)
    {
        var normalRoleCount = 0;
        var normalWeaponCount = 0;
        var gachaTimesOfRole = 1;
        var gachaTimesOfWeapon = 1;

        //保底机制
        var hasPityOfRole = false;
        var hasPityOfWeapon = false;

        //角色池抽奖逻辑
        var m = 1; //m为循环内抽卡次数
        for (var i = 0; i < targetAmountsOfLimitedRole;)
        {
            //先完成一次抽卡
            gachaTimesOfRole++;

            if (_random.Value.NextDouble() < GachaCalcTools.GetSucceedProbabilityOfCharacter(m))
            {
                //抽到五星时的情况
                m = 1; //只要获得五星，直接重置循环

                if (hasPityOfRole)
                {
                    //有大保底时的逻辑
                    i++;
                    hasPityOfRole = false; //清空已有的保底
                }
                else
                {
                    //无大保底时的逻辑
                    if (_random.Value.NextDouble() < GlobalVariables.GetLimitedCharacterRate)
                    {
                        //出限定的情况
                        i++;
                    }
                    else
                    {
                        //歪常驻的情况
                        normalRoleCount++;
                        hasPityOfRole = true; //为下一次添加大保底
                    }
                }
            }
            else
            {
                //没有抽到五星的情况
                m++; //相当于垫池子
            }
        }

        //武器池抽奖逻辑
        var n = 1;
        for (var i = 0; i < targetAmountsOfLimitedWeapon;)
        {
            //先完成一次抽卡
            gachaTimesOfWeapon++;

            if (_random.Value.NextDouble() < GachaCalcTools.GetSucceedProbabilityOfWeapon(n))
            {
                //抽到五星时的情况
                n = 1; //只要获得五星，直接重置循环

                if (hasPityOfWeapon)
                {
                    //有大保底时的逻辑
                    i++;
                    hasPityOfWeapon = false; //清空已有的保底
                }
                else
                {
                    //无大保底时的逻辑
                    if (_random.Value.NextDouble() < GlobalVariables.GetLimitedWeaponRate)
                    {
                        //出限定的情况
                        i++;
                    }
                    else
                    {
                        //歪常驻的情况
                        normalWeaponCount++;
                        hasPityOfWeapon = true; //为下一次添加大保底
                    }
                }
            }
            else
            {
                //没有抽到五星的情况
                n++; //相当于垫池子
            }
        }

        return (normalRoleCount, gachaTimesOfRole, gachaTimesOfWeapon, normalWeaponCount);
    }
}