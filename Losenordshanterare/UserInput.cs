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
            string arg = args[0];

            if (string.IsNullOrWhiteSpace(arg))
            {
                throw new ArgumentException("Argument cannot be null or empty. Please try again.");
            }

            ArgumentSwitchCase(args);

        }

        private static void ArgumentSwitchCase(string[] args)
        {
            string argument = args[0];

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
                    Console.WriteLine("The command is set");
                    break;
                case "delete":
                    Console.WriteLine("The command is delete");
                    break;
                case "secret":
                    Console.WriteLine("The command is secret");
                    break;
                default:
                    Console.WriteLine("Invalid parameter as argument. Please try again.");
                    break;
            }
        }
    }
}
