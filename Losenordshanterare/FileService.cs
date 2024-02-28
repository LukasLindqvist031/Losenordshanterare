using System;
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

        public static SecretKey ReadSecretKeyFromFile(string client)
        {
            string fileContent = File.ReadAllText(client);

            SecretKey secretKey = JsonSerializer.Deserialize<SecretKey>(fileContent);

            return secretKey;
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