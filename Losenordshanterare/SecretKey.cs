using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class SecretKey
    {
        private readonly byte[] _secretKey;

        public SecretKey()
        {
            _secretKey = GenerateSecretKey();
        }

        private byte[] GenerateSecretKey()
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
