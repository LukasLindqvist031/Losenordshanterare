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
        private readonly byte[] _key;

        public SecretKey()
        {
            _key  = GenerateSecretKey();
        }
        public SecretKey(byte[] key)
        {
            _key = key;
        }

        private static byte[] GenerateSecretKey()
        {
            const int arraySize = 32;
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[arraySize];
                rng.GetBytes(bytes);
                return bytes;
            }
        }

        public byte[] GetKey => _key;

    }
}
