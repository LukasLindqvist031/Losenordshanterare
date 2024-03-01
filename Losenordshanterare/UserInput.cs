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
                        Create create = new Create(args);
                        create.Execute();
                        break;
                    case "get":
                        Get get = new Get(args);
                        get.Execute();
                        break;
                    case "set":
                        Set set = new Set(args);
                        set.Execute();
                        break;
                    case "delete":
                        Delete delete = new Delete(args);
                        delete.Execute();
                        break;
                    case "secret":
                        Secret secret = new Secret(args);
                        secret.Execute();
                        break;
                    default:
                        Console.WriteLine($"Error: Invalid operation parameter '{args[0]}'.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        public static string[] GetInput(string valuePassword)
        {
            if (string.IsNullOrEmpty(valuePassword))
            {
                return UserPrompt.PromptUserSet(valuePassword);
            }
            else
            {
                return UserPrompt.PromptUser();
            }
        }

        public static string[] GetInput() => UserPrompt.PromptUser();

    }
}
