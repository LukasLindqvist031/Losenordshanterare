using System;

namespace Losenordshanterare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                ProcessUserInput.ProcessCommandLine(args);
            }

        }
    }
}