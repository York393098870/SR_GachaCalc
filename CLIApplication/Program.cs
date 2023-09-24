using CoreLibraries;

const bool debugMode = false; //生产环境下请设置为FALSE

//主要用于测试
Console.WriteLine("请选择计算模式：（1,2,3）");
Console.WriteLine("输入1：计算自己欧非情况，需要提供你总共花费的抽数以及获得的武器数量，总抽数从没有大保底的时候开始计算。");
Console.WriteLine("输入2：模拟10位玩家连续在（Up角色池）抽卡1000次的情况。");
Console.WriteLine("输入3：模拟10位玩家连续在（Up光锥池）抽卡1000次的情况。");
var calculateMode = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
switch (calculateMode)
{
    case 1:
        Console.Write("请输入你抽取的限定角色数量：");
        var targetNumbersOfRole = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

        Console.Write("请输入你抽取的限定武器数量：");
        var targetNumberOfWeapon = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

        const int simulations = 100000;

        Console.Write("请输入你花费的抽数：");
        var playerATries = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

        var allTries = new List<int>();

        for (var i = 1; i <= simulations; i++)
        {
            var gachaResult = GachaCalcByCounts.GachaCalc(targetNumbersOfRole, targetNumberOfWeapon); // 模拟函数
            allTries.Add(gachaResult.gachaTimesOfRole + gachaResult.gachaTimesOfWeapon);
        }

        allTries.Sort(); // 排序结果

        // 找出玩家幸运值的位置
        var rank = allTries.Count(x => x < playerATries);

        // 计算百分比
        var percentile = (double)rank / simulations * 100;

        // 计算超越了多少玩家
        var surpassed = 100 - percentile;

        Console.WriteLine(
            $"你总共花费{playerATries}抽，获得{targetNumbersOfRole}只限定角色，{targetNumberOfWeapon}把限定武器，在模拟一百万名玩家进行相同抽卡的情况下，你的运气超越了{surpassed:F3}%的玩家。");
        if (!debugMode)
        {
            Console.ReadKey();
        }

        break;

    case 2:
        const int tryRoleTimes = 1000;
        for (var i = 1; i <= 10; i++)
        {
            var gachaResult = GachaCalcByTimes.Gacha(tryRoleTimes, "role");
            var averagelimitedcount = Math.Round((double)tryRoleTimes / gachaResult.Item1, 1);
            var averagetotalcount = Math.Round((double)tryRoleTimes / (gachaResult.Item1 + gachaResult.Item2), 1);
            Console.WriteLine(
                $"模拟Up角色池抽卡{tryRoleTimes}次的情况：获得五星数量{gachaResult.Item1 + gachaResult.Item2}只，五星平均抽数为{averagetotalcount}。其中，获得限定五星数量{gachaResult.Item1}只，限定五星平均抽数为{averagelimitedcount}；获得常驻五星数量{gachaResult.Item2}只。");
        }

        if (!debugMode)
        {
            Console.ReadKey();
        }

        break;

    case 3:
        const int tryWeaponTimes = 1000;
        for (var i = 1; i <= 10; i++)
        {
            var gachaResult = GachaCalcByTimes.Gacha(tryWeaponTimes, "weapon");
            var averagelimitedcount = Math.Round((double)tryWeaponTimes / gachaResult.Item1, 1);
            var averagetotalcount = Math.Round((double)tryWeaponTimes / (gachaResult.Item1 + gachaResult.Item2), 1);
            Console.WriteLine(
                $"模拟Up武器池抽卡{tryWeaponTimes}次的情况：获得五星数量{gachaResult.Item1 + gachaResult.Item2}只，五星平均抽数为{averagetotalcount}。其中，获得限定五星数量{gachaResult.Item1}只，限定五星平均抽数为{averagelimitedcount}；获得常驻五星数量{gachaResult.Item2}只。");
        }

        if (!debugMode)
        {
            Console.ReadKey();
        }

        break;

    default:
        Console.WriteLine("请输入正确的数字。");
        Environment.Exit(3);
        break;
}