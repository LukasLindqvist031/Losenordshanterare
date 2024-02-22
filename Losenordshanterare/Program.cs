
using Losenordshanterare;

RecieveArgs(args);

static void RecieveArgs(string[] args)
{
    if (args.Length > 0)
    {
        UserInput.ProcessCommandLine(args);
    }
}

