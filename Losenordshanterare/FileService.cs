using System;
using System.IO;

namespace Losenordshanterare
{
    internal class FileService
    {
        public void WriteToFile(string jsonContent)
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

