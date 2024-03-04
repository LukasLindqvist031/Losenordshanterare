using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal static class UserPrompt
    {
        public static string PromptMasterPass()
        {
            const string masterPrompt = "Please enter your master password:";

            Console.WriteLine(masterPrompt);
            string? master = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(master))
            {
                throw new ArgumentException("Input cannot be empty!");
            }
            else
            {
                return master;
            }
        }

        public static string PromptValuePass()
        {
            const string valuePrompt = "Please enter the password you want to store:";

            Console.WriteLine(valuePrompt);
            string? value = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Input cannot be empty!");
            }
            else
            {
                return value;
            }
        }

        public static string PromptSecret()
        {
            const string secretPrompt = "Please enter the secret key:";

            Console.WriteLine(secretPrompt);
            string? secret = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new ArgumentException("Input cannot be empty!");
            }
            else
            {
                return secret;
            }
        }
        

    }
}
