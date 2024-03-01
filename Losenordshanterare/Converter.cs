using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal static class Converter
    {
        public static string ConvertToJson(string base64Vault, string base64IV)
        {
            Dictionary<string, string> dict = ConvertToDict(base64Vault, base64IV);
            return SerializeDict(dict);
        }
        private static Dictionary<string, string> ConvertToDict(string vault, string iv)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["EncodedIV"] = iv;
            dict["EncryptedVault"] = vault;
            return dict;
        }

        private static string SerializeDict(Dictionary<string, string> dict) => JsonSerializer.Serialize(dict);

        public static string ConvertIVToBase64(byte[] iv)
        {
            string encodedIV = Convert.ToBase64String(iv);
            return encodedIV;
        }

        public static string ConvertSecretKeyToJson(byte[] key)
        {
            string encodedSecret = Convert.ToBase64String(key);
            return JsonSerializer.Serialize(new { Secret = encodedSecret });
        }


    }
}
