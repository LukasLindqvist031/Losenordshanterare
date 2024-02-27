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
        //Creates an Aes object
        private readonly Crypto.Aes _aes;
        
        public Encryption()
        {
            _aes = Crypto.Aes.Create();
        }
      
        //Encrypt the data using the _aes object
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

        //Decrypt the data using the _aes object
        public string Decrypt(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream csDecrypt = new CryptoStream(ms, _aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    csDecrypt.Write(byteArray, 0, byteArray.Length);
                }
                return ms.ToString();
            }
        }

        //Generate an IV
        public byte[] GenerateIV()
        {
            _aes.GenerateIV();
            return _aes.IV;
        }

        //Use the _aes object to create a JSON format
        public string createJson()
        {
            AesObject aesobject = new AesObject(_aes);
            string content = JsonSerializer.Serialize(aesobject);
            return content;
        }

        //Use the _aes object to deserialize the data
        public void LoadFromJson(string data)
        {
            AesObject aesobject = JsonSerializer.Deserialize<AesObject>(data);
            _aes.Key = aesobject.Key;
            _aes.IV = aesobject.IV;
            _aes.Mode = aesobject.Mode;
            _aes.Padding = aesobject.Padding;
        }
    } 
}
