using System;
using System.IO;

namespace Losenordshanterare
{
    internal static class FileService
    {

        public static void Execute()
        {
            string filePath = "test.txt";
            string fileContent = ReadFile(filePath);
            Console.WriteLine("File content:");
            Console.WriteLine(fileContent);
        }

        public static void CreateFile(string path)
        {
            try
            {
                File.Create(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static string ReadFile(string path)
        {
            try
            {
                string content = File.ReadAllText(path);
                return content;
            }
            catch (Exception ex)
            {
                return $"An error occurred while reading the file: {ex.Message}";
            }
        }
      
        public static void WriteToFile(string jsonContent)
        {
            string filePath = "test.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(jsonContent);
                }
                Console.WriteLine("Write successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}