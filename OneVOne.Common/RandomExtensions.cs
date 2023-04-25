using OneVOne.GameService.Core.Entities;

namespace OneVOne.Common
{
    public static class RandomExtensions
    {
        public static int CoinFlip(this Random random, int upperBound)
        {
            return random.Next(upperBound);
        }

        public static byte Randomize(this Random random, int lowerBound, int upperBound)
        {
            return Convert.ToByte(random.Next(lowerBound, upperBound));
        }

    }
}