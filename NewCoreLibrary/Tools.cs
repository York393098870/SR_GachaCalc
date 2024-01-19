namespace NewCoreLibrary;

public class Tools
{
    public static class Lottery
    {
        public static bool CheckIfSucceed(double successProbability)
        {
            if (successProbability is < 0 or > 1)
            {
                throw new ArgumentException("成功率必须为0到1之间的double值！", nameof(successProbability));
            }

            var randomNumber = Random.Shared.NextDouble();
            return randomNumber < successProbability;
        }
    }

    public static string GetRateForLimitedFiveStarsAndNormalFiveStars(int limitedFiveStars, int normalFiveStars)
    {
        if (limitedFiveStars < 0 || normalFiveStars < 0)
        {
            throw new ArgumentException("限定角色的数量和常驻角色的数量必须大于等于0");
        }

        if (normalFiveStars == 0)
        {
            var rateWhenNoNormalFiveStars = (double)limitedFiveStars;
            var resultWhenNoNormalFiveStars = $"{rateWhenNoNormalFiveStars:F1}:0";
            return resultWhenNoNormalFiveStars;
        }

        var rate = (double)limitedFiveStars / normalFiveStars;
        var result = $"{rate:F2}:1";
        return result;
    }
}