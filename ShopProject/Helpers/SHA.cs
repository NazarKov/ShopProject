using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers
{
    internal class SHA
    {
        internal static string GenerateSHA256File(string FilePathString, bool Old = true)
        {
            string shA256File;
            SHA256 shA256 = SHA256.Create();
            FileStream fileStream = File.Open(FilePathString, FileMode.Open);
            FileStream inputStream = fileStream;

            byte[] hash = shA256.ComputeHash((Stream)inputStream);
            fileStream.Flush();
            fileStream.Close();
            StringBuilder stringBuilder = new StringBuilder();
            int num = checked(hash.Length - 1);
            int index = 0;
            while (index <= num)
            {
                stringBuilder.Append(hash[index].ToString("X2"));
                checked { ++index; }
            }
            shA256File = stringBuilder.ToString().ToLower();
            return shA256File;
        }

        public static string GenerateSHA256String(string inputString)
        {
            byte[] hash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(inputString));
            StringBuilder stringBuilder = new StringBuilder();
            int num = checked(hash.Length - 1);
            int index = 0;
            while (index <= num)
            {
                stringBuilder.Append(hash[index].ToString("X2"));
                checked { ++index; }
            }
            return stringBuilder.ToString();
        }
        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
    }
}
