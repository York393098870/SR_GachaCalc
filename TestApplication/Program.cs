using CoreLibraries;

//主要用于测试
if (false)
{
    const int times = 1000000;
    var gachaResult = GachaCalcByTimes.Gacha(times, "role");
    var averagelimitedcount = Math.Round((double)times / gachaResult.Item1, 2);
    var averagetotalcount = Math.Round((double)times / (gachaResult.Item1 + gachaResult.Item2), 2);
    Console.WriteLine(
        $"你总共模拟抽卡{times}次，获得五星数量{gachaResult.Item1 + gachaResult.Item2}只，五星平均抽数为{averagetotalcount}。其中，获得限定五星数量{gachaResult.Item1}只，限定五星平均抽数为{averagelimitedcount}；获得常驻五星数量{gachaResult.Item2}只。");
}

if (false)
{
    const int targetNumbersOfRole = 3;
    const int targetNumberOfWeapon = 1;
    for (var i = 1; i <= 10; i++)
    {
        var gachaResult = GachaCalcByCounts.GachaCalc(targetNumbersOfRole, targetNumberOfWeapon);
        Console.WriteLine(
            $"模拟获得{targetNumbersOfRole - 1}命限定角色+{targetNumberOfWeapon}叠影限定武器的情况，本次模拟抽卡总共花费了{gachaResult.gachaTimesOfRole + gachaResult.gachaTimesOfWeapon}抽，歪出常驻池角色{gachaResult.normalRoleCount}只，常驻池武器{gachaResult.normalWeaponCount}把");
    }
}

if (false)
{
    const int times = 1000000000;
    var gachaResult = GachaCalcByTimes.Gacha(times, "weapon");
    var averagelimitedcount = Math.Round((double)times / gachaResult.Item1, 1);
    var averagetotalcount = Math.Round((double)times / (gachaResult.Item1 + gachaResult.Item2), 1);
    Console.WriteLine(
        $"你总共模拟抽卡{times}次，获得五星数量{gachaResult.Item1 + gachaResult.Item2}只，五星平均抽数为{averagetotalcount}。其中，获得限定五星数量{gachaResult.Item1}只，限定五星平均抽数为{averagelimitedcount}；获得常驻五星数量{gachaResult.Item2}只。");
}

if (true)
{
    Console.Write("请输入你抽取的限定角色数量：");
    var targetNumbersOfRole = int.Parse(Console.ReadLine());

    Console.Write("请输入你抽取的限定武器数量：");
    var targetNumberOfWeapon = int.Parse(Console.ReadLine());

    const int simulations = 100000;

    Console.Write("请输入你花费的抽数：");
    var playerATries = int.Parse(Console.ReadLine());

    var allTries = new List<int>();

    for (var i = 1; i <= simulations; i++)
    {
        var gachaResult = GachaCalcByCounts.GachaCalc(targetNumbersOfRole, targetNumberOfWeapon); // 模拟函数
        allTries.Add(gachaResult.gachaTimesOfRole + gachaResult.gachaTimesOfWeapon);
    }

    allTries.Sort(); // 排序结果

    // 找出玩家A的位置
    var rank = allTries.Count(x => x < playerATries);

    // 计算百分比
    var percentile = (double)rank / simulations * 100;

    // 计算超越了多少玩家
    var surpassed = 100 - percentile;

    Console.WriteLine(
        $"你总共花费{playerATries}抽，获得{targetNumbersOfRole}只限定角色，{targetNumberOfWeapon}把限定武器，在模拟一百万名玩家进行相同抽卡的情况下，你的运气超越了{surpassed:F3}%的玩家。");
    Console.ReadKey();
}