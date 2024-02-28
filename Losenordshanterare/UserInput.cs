using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal static class UserInput
    {
        
        public static void ProcessCommandLine(string[] args)
        {
            string argument = args[0];

            try
            {
                switch (argument)
                {
                    case "init":
                        Init init = new Init(args);
                        init.Execute();
                        break;
                    case "create":
                        Console.WriteLine("The command is create");
                        break;
                    case "get":
                        Console.WriteLine("The command is get");
                        break;
                    case "set":
                        Set set = new Set(args);
                        set.Execute();
                        break;
                    case "delete":
                        Console.WriteLine("The command is delete");
                        break;
                    case "secret":
                        Console.WriteLine("The command is secret");
                        break;
                    default:
                        Console.WriteLine($"Error: Invalid operation parameter '{args[0]}'.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


    }
}
