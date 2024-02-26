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



namespace Losenordshanterare
{
    internal class Encryption
    {
        //Creates an Aes object
        private readonly Crypto.Aes _aes;
        
        public Encryption()
        {
            _aes = Crypto.Aes.Create();
        }
      
        public byte[] Encrypt(string data)
        {
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(ms, _aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    csEncrypt.Write(byteArray, 0, data.Length);
                }
                return ms.ToArray();
            }
        }

        public byte[] Decrypt(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream csDecrypt = new CryptoStream(ms, _aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    csDecrypt.Write(byteArray, 0, byteArray.Length);
                }
                return ms.ToArray();
            }
        }
    } 
}
