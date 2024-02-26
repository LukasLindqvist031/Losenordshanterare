using System.Text.RegularExpressions;

namespace Losenordshanterare
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length > 0) 
            {
                UserInput.ProcessCommandLine(args);
            }

            
            //FileService.WriteToFile("LUKAS");
            //FileService.Execute();
            //Console.ReadLine();

            //string pattern = "[a-zA-Z0-9]{20}";
            //GeneratePassword pass = new();
            //pass.PrintPassword();

            //string generatedPass = pass.GetPassword();

            //Match m = Regex.Match(generatedPass, pattern);

            //Console.WriteLine(m.Success);
        }
    }
}