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
        private readonly SecretKey _secretKey;
        private readonly string _password;
        private readonly byte[] _vaultKey;

        public VaultKey(string password, SecretKey secretKey)
        {
            _password = password;
            _secretKey = secretKey;
            _vaultKey = GenerateVaultKey(_password, _secretKey);
        }

       
        private byte[] GenerateVaultKey(string password, SecretKey secretKey)
        {
            const int iterations = 10000;
            byte[] salt = secretKey.GetKey;

            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);

            byte[] derivedKey = rfc.GetBytes(32);

            return derivedKey;
        }

        public byte[] GetKey => _vaultKey;

        
        
    }
}
