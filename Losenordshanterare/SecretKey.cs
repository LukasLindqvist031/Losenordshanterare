using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal static class SecretKey
    {
        public static byte[] NewKey() => GenerateSecretKey();

        private static byte[] GenerateSecretKey()
        {
            const int arraySize = 20;
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[arraySize];
                rng.GetBytes(bytes);
                return bytes;
            }

        }

    }
}
