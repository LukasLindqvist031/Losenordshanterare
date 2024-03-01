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
                inputArr[0] += master;
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

        public static string[] PromptUserSet()
        {
            const string masterPrompt = "Please enter your master password: ";
            const string valuePrompt = "Please enter the password you want to store: ";

            string[] inputArr = new string[2];

            try
            {
                Console.Write(masterPrompt);
                string? master = Console.ReadLine();
                Console.Write(valuePrompt);
                string? value = Console.ReadLine();
                inputArr[0] += master;
                inputArr[1] += value;
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
            const string masterPrompt = "Please enter your master password: ";
            const string secretPrompt = "Please enter the secret key: ";

            string[] inputArr = new string[2];

            try
            {
                Console.Write(masterPrompt);
                string? master = Console.ReadLine();
                Console.Write(secretPrompt);
                string? secret = Console.ReadLine();
                inputArr[0] += master;
                inputArr[1] += secret;
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
