using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Losenordshanterare
{
    internal static class FileService
    {
        public static void CreateFile(string path)
        {

            try
            {
                using (FileStream fs = File.Create(path)) { }
                Console.WriteLine("File created successfully: " + path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating file: " + ex.Message);
            }
        }

        public static string ReadFile(string path)
        {
            try
            {
                string fileContent = File.ReadAllText(path);
                return fileContent;
            }
            catch (Exception ex)
            {
                return $"An error occurred while reading the file: {ex.Message}";
            }
        }

        public static string ReadVaultFromFile(string serverPath)
        {
            string fileContent = File.ReadAllText(serverPath);

            if (string.IsNullOrEmpty(fileContent))
            {
                throw new Exception("Error: Vault string from server is empty.");
            }

            Dictionary<string, string>? jsonData = JsonSerializer.Deserialize<Dictionary<string, string>>(fileContent);

            if (jsonData == null)
            {
                throw new Exception("Error: Vault from server is empty.");
            }

            string base64Vault = jsonData["EncryptedVault"];
            return base64Vault;
        }

        public static SecretKey ReadSecretKeyFromFile(string clientPath)
        {
            string fileContent = File.ReadAllText(clientPath);

            if (string.IsNullOrEmpty(fileContent))
            {
                throw new Exception("Error: Secret key string from client is empty.");
            }

            Dictionary<string, string>? jsonData = JsonSerializer.Deserialize<Dictionary<string, string>>(fileContent);

            if (jsonData == null)
            {
                throw new Exception("Error: Vault string from server is empty.");
            }

            string secretBase64 = jsonData["Secret"];
            byte[] secretArr = Convert.FromBase64String(secretBase64);
            SecretKey secretKey = new(secretArr);
            return secretKey;
        }

        public static byte[] ReadIVFromFile(string serverPath)
        {
            string fileContent = File.ReadAllText(serverPath);

            if (string.IsNullOrEmpty(fileContent))
            {
                throw new Exception("Error: Secret key string from client is empty.");
            }

            Dictionary<string, string>? jsonData = JsonSerializer.Deserialize<Dictionary<string, string>>(fileContent);

            if (jsonData == null)
            {
                throw new Exception("Error: IV string from server is empty.");
            }

            string encodedIV = jsonData["EncodedIV"];
            byte[] iv = Convert.FromBase64String(encodedIV);
            return iv;
        }

        public static void WriteToFile(string jsonContent, string path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, append: false))
                {
                    writer.WriteLine(jsonContent);
                }
                Console.WriteLine("Write successful");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }


        }
    }
}