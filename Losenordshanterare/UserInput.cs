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

            if (IsValidArgument(argument))
            {
                ArgumentSwitchCase(args);
            }
            else
            {
                Console.WriteLine("Argument cannot be null or empty. Please try again.");
            }
        }

        private static void ArgumentSwitchCase(string[] args)
        {
            string argument = args[0];

            switch (argument)
            {
                case "init":
                    Console.WriteLine("The commmand is init");
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


        private static bool IsValidArgument(string arg) => !string.IsNullOrWhiteSpace(arg);
    }
}
