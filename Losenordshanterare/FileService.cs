using System;
using System.IO;

namespace Losenordshanterare
{
    internal class FileService
    {
      string filePath = "test.txt";

        public static void Execute()
        {
            string fileContent = ReadFile(filePath);
            Console.WriteLine("File content:");
            Console.WriteLine(fileContent);
        }

        static string ReadFile(string path)
        {
            try
            {
                string content = File.ReadAllText(path);
                return content;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                return null; // Ändra null till något annat?
              
        public void WriteToFile(string jsonContent)
        {
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