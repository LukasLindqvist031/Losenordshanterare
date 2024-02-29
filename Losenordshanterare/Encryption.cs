using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;

namespace Losenordshanterare
{
    internal static class Encryption
    {

        public static byte[] Encrypt(string data, VaultKey vaultKey, Aes aes)
        {
            byte[] encrypted;
            byte[] key = vaultKey.GetKey;
            byte[] byteArray = Encoding.UTF8.GetBytes(data);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(ms, aes.CreateEncryptor(key, aes.IV), CryptoStreamMode.Write))
                {
                    csEncrypt.Write(byteArray, 0, byteArray.Length);
                    csEncrypt.FlushFinalBlock();
                    encrypted = ms.ToArray();
                }
            }
            return encrypted;
        }

        public static string Decrypt(byte[] cipherText, VaultKey vaultKey, byte[] oldIV)
        {
            Aes aes = Aes.Create();
            byte[] key = vaultKey.GetKey;
            string plaintext = string.Empty;
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(ms, aes.CreateDecryptor(key, oldIV), CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(csDecrypt))
                    {
                        plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }
}
