using System.Security.Cryptography;

namespace ShopProjectWebServer.Helpers
{
    public static class GenerationToken
    {
        public static string Generate(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            using (var rng = RandomNumberGenerator.Create())
            {
                var byteBuffer = new byte[length];
                rng.GetBytes(byteBuffer);
                var tokenChars = new char[length];

                for (int i = 0; i < length; i++)
                {
                    int randomIndex = byteBuffer[i] % chars.Length;
                    tokenChars[i] = chars[randomIndex];
                }

                return new string(tokenChars);
            }
        }
    }
}
