namespace NewCoreLibrary;

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
}