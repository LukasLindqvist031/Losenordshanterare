﻿using System;
using System.IO;
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

        public static Vault ReadVaultFromFile(string serverPath)
        {
            string fileContent = File.ReadAllText(serverPath);
            Dictionary<string, string> jsonData = JsonSerializer.Deserialize<Dictionary<string, string>>(fileContent);
            string jsonVault = jsonData["EncryptedVault"];
            Vault encodedVault = JsonSerializer.Deserialize<Vault>(jsonVault);
            return encodedVault;
        }

        public static SecretKey ReadSecretKeyFromFile(string clientPath)
        {
            string fileContent = File.ReadAllText(clientPath);

            Dictionary<string, string> jsonData = JsonSerializer.Deserialize<Dictionary<string, string>>(fileContent);
            string secretBase64 = jsonData["Secret"];
            byte[] secretKey = Convert.FromBase64String(secretBase64);
            SecretKey secret = new(secretKey);
            return secret;
        }

        public static byte[] ReadIVFromFile(string serverPath)
        {
            string fileContent = File.ReadAllText(serverPath);
            Dictionary<string, string> jsonData = JsonSerializer.Deserialize<Dictionary<string, string>>(fileContent);
            string encodedIV = jsonData["EncodedIV"];
            byte[] iv = Convert.FromBase64String(encodedIV);
            return iv;
        }

        public static void WriteToFile(string jsonContent, string path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, append: true))
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