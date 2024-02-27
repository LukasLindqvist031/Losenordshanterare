using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Crypto = System.Security.Cryptography;
using System.Runtime.Intrinsics.X86;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text.Json;

namespace Losenordshanterare
{
    internal class Encryption
    {
        public readonly Crypto.Aes _aes;

        public Encryption()
        {
            _aes = Crypto.Aes.Create();
        }

        public byte[] Encrypt(string data)
        {
            _aes.GenerateIV(); 
            byte[] encrypted;
            byte[] byteArray = Encoding.UTF8.GetBytes(data);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(ms, _aes.CreateEncryptor(_aes.Key, _aes.IV), CryptoStreamMode.Write))
                {
                    csEncrypt.Write(byteArray, 0, byteArray.Length);
                    csEncrypt.FlushFinalBlock();
                    encrypted = ms.ToArray();
                }
            }
            return encrypted;
        }

        public string Decrypt(byte[] cipherText)
        {
            string plaintext = null;
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(ms, _aes.CreateDecryptor(_aes.Key, _aes.IV), CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(csDecrypt))
                    {
                        plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }

        //Generate an IV
        public byte[] GenerateIV()
        {
            _aes.GenerateIV();
            return _aes.IV;
        }
    }
}
