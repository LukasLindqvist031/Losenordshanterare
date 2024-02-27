using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class VaultKey
    {
        private readonly Rfc2898DeriveBytes _key;

        public VaultKey(string input)
        {
            string password = input;
            byte[] secretKey = SecretKey.NewKey();
            _key = GenerateVaultKey(password, secretKey);
        }

        private Rfc2898DeriveBytes GenerateVaultKey(string password, byte[] secretKey)
        {
            const int iteration = 1000;

            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, secretKey, iteration, HashAlgorithmName.SHA256);

            return key;
        }
    }
}
