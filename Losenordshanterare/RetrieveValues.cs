using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal static class RetrieveValues
    {
        public static string GetMasterPass()
        {
            string pass = UserPrompt.PromptMasterPass();

            if (string.IsNullOrWhiteSpace(pass))
            {
                throw new NullOrWhiteSpaceArgumentException("Password cannot be empty!");

            }
            else
            {
                return pass;
            }
        }

        public static string GetValuePass()
        {
            string value = UserPrompt.PromptValuePass();

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NullOrWhiteSpaceArgumentException("Password cannot be empty!");

            }
            else
            {
                return value;
            }
        }

        public static string GetSecret()
        {
            string secret = UserPrompt.PromptSecret();

            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new NullOrWhiteSpaceArgumentException("Password cannot be empty!");

            }
            else
            {
                return secret;
            }
        }
    }
}
