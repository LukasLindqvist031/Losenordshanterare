using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal static class ValidateArguments
    {
        public static bool IsValidArgument(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(args[i]))
                {
                    throw new NullOrWhiteSpaceArgumentException($"Error: Argument '{args[i]}' at index {i} cannot be null or whitespace.");
                }
            }
            return true;
        }

        public static bool IsValidLengthGet(string[] args)
        {
            if (args.Length < 3 || args.Length > 4)
            {
                throw new InvalidNumberOfArgumentsException($"Error: Expected 3 or 4 arguments, but received {args.Length}.");
            }
            else
            {
                return true;
            }
        }

        public static bool IsValidLengthInit(string[] args)
        {
            if (args.Length != 3)
            {
                throw new InvalidNumberOfArgumentsException($"Error: Expected 3 arguments, but received {args.Length}.");

            }
            else
            {
                return true;
            }
        }

        public static bool IsValidLengthSet(string[] args)
        {
            if (args.Length < 4 || args.Length > 5)
            {
                throw new InvalidNumberOfArgumentsException($"Error: Expected 4 or 5 arguments, but received {args.Length}.");
            }
            else
            {
                return true;
            }
        }

        public static bool IsValidLengthCreate(string[] args)
        {
            if (args.Length != 3)
            {
                throw new InvalidNumberOfArgumentsException($"Error: Expected 3 arguments, but received {args.Length}.");
            }
            else
            {
                return true;
            }
        }

        public static bool IsValidLengthDelete(string[] args)
        {
            if (args.Length != 4)
            {
                throw new InvalidNumberOfArgumentsException($"Error: Expected 4 arguments, but received {args.Length}.");
            }
            else
            {
                return true;
            }
        }

        public static bool IsValidLengthSecret(string[] args)
        {
            if (args.Length != 2)
            {
                throw new InvalidNumberOfArgumentsException($"Error: Expected 2 arguments, but received {args.Length}.");
            }
            else
            {
                return true;
            }
        }


    }
}
