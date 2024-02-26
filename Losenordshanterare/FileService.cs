using System;
using System.IO;

namespace Losenordshanterare
{
    internal static class FileService
    {
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

        public static void ReadFile()
        {
            string filePath = "test.txt";
            try
            {
                string fileContent = File.ReadAllText(filePath);
                Console.WriteLine("File content:");
                Console.WriteLine(fileContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }
        }

        public static void WriteToFile(string jsonContent)
        {
            string filePath = "test.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
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