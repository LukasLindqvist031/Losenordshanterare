using System;

namespace Losenordshanterare
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                UserInput.ProcessCommandLine(args);
            }

        }
    }
}