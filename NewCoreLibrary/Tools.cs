﻿namespace NewCoreLibrary;

public class Tools
{
    public static class Lottery
    {
        private static readonly ThreadLocal<Random> ThreadLocalRandom = new ThreadLocal<Random>(() =>
            new Random(Interlocked.Increment(ref _seed)));

        private static int _seed = Environment.TickCount;

        public static bool CheckIfSucceed(double successProbability)
        {
            if (successProbability is < 0 or > 1)
            {
                throw new ArgumentException("Success probability must be between 0 and 1.", nameof(successProbability));
            }

            var randomNumber = ThreadLocalRandom.Value.NextDouble();
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