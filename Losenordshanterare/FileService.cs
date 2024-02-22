using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class FileSerivce
    {
        public static void Execute()
        {
            string filePath = "test.txt"; // Ändra till riktiga filnamnet när det är bestämt
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
            }
        }
    }
}
