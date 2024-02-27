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
                using (FileStream fs = File.Create(path)) { }
                Console.WriteLine("File created successfully: " + path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating file: " + ex.Message);
            }
        }

        public static void ReadFile(string path)
        {
            try
            {
                string fileContent = File.ReadAllText(path);
                Console.WriteLine("File content:");
                Console.WriteLine(fileContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }
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