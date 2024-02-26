
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal static class GeneratePassword
    {
        public static string? GenerateNewPassword()
        {
            Random _random = new Random();
            const int passLength = 20;
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            char[] chars = new char[passLength];

            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] += validChars[_random.Next(0, validChars.Length)];
            }

            chars = Shuffle(chars);
            
            return new string(chars);
        }

        private static char[] Shuffle(char[] chars)
        {
            int count = chars.Length;

            while (count > 1)
            {
                int i = Random.Shared.Next(count--);
                (chars[i], chars[count]) = (chars[count], chars[i]);
            }
            return chars;
        }

        // Bara test för att se vad som sparas, tas bort sen

        //public string GetPassword()
        //{
        //    if (_password != null)
        //    {
        //        return _password;
        //    }
        //    return string.Empty;

        //}

        //public void PrintPassword()
        //{
        //    Console.WriteLine(GetPassword());
        //}
    }
}
