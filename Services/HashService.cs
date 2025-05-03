using System.Text;

namespace Short_URLs_API.Services
{
    public class HashService
    {
        private const int HashLength = 11;
        private static readonly char[] Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%&*".ToCharArray();
        private readonly Random random;

        public HashService()
        {
            random = new Random();
        }

        public string Generate()
        {
            var sb = new StringBuilder(HashLength);
            for (int i = 0; i < HashLength; i++)
            {
                var index = random.Next(Characters.Length);
                sb.Append(Characters[index]);
            }
            return sb.ToString();
        }
    }
}
