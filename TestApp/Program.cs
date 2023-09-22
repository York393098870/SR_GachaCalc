using CoreLibraries;

if (false)
{
    const int times = 1000000;
    var gachaResult = GachaCalcByTimes.Gacha(times);
    var averagelimitedcount = Math.Round((double)times / gachaResult.Item1, 2);
    var averagetotalcount = Math.Round((double)times / (gachaResult.Item1 + gachaResult.Item2), 2);
    Console.WriteLine(
        $"你总共模拟抽卡{times}次，获得五星数量{gachaResult.Item1 + gachaResult.Item2}只，五星平均抽数为{averagetotalcount}。其中，获得限定五星数量{gachaResult.Item1}只，限定五星平均抽数为{averagelimitedcount}；获得常驻五星数量{gachaResult.Item2}只。");
}

if (true)
{
    const int targetNumbersOfRole = 7;
    for (var i = 1; i <= 10; i++)
    {
        var totalGachaTimes = GachaCalcByCounts.GachaCalc(targetNumbersOfRole, 0);
        Console.WriteLine($"你希望获得{targetNumbersOfRole}只限定五星角色，本次模拟抽卡总共花费了{totalGachaTimes}抽");
    }
}