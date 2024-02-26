using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class Init : ICommand
    {
        private readonly string? _client;
        private readonly string? _server;
        private readonly string? _password;

        public Init(string[] args)
        {
            if (args.Length == 4)
            {
                _client = args[1];
                _server = args[2];
                _password = args[3];
            }
            else
            {
                Console.WriteLine("The number of arguments are invalid. Try again.");
            }
            Execute();
        }


        private void Execute()
        {
            FileService.CreateFile(_client);
            FileService.CreateFile(_server);
        }
    }
}
