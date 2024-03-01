
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal static class RandomPasswordGenerator
    {

        public static string GeneratePassword()
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
    }
}
