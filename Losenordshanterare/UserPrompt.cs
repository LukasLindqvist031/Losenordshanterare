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
        public static string[] PromptUser()
        {
            const string masterPrompt = "Please enter your master password: ";

            string[] inputArr = new string[1];

            try
            {
                Console.Write(masterPrompt);
                string? master = Console.ReadLine();
                inputArr[0] = master;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            if(!IsValidInput(inputArr))
            {
                throw new ArgumentException("Input cannot be empty!");
            }

            return inputArr;         
        }

        public static string[] PromptUserSet(string? value)
        {
            const string masterPrompt = "Please enter your master password: ";
            const string valuePrompt = "Please enter the password you want to store: ";

            string[] inputArr = new string[2];

            try
            {
                Console.Write(masterPrompt);
                string? master = Console.ReadLine();
                Console.Write(valuePrompt);
                value = Console.ReadLine();
                inputArr[0] = master;
                inputArr[1] = value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            if (!IsValidInput(inputArr))
            {
                throw new ArgumentException("Input cannot be empty!");
            }

            return inputArr;
        }

        public static string[] PromptUserCreate()
        {

        }

        private static bool IsValidInput(string[] args)
        {
            foreach (string arg in args)
            {
                if(string.IsNullOrWhiteSpace(arg))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
