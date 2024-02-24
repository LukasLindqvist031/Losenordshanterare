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

        public Init(string? client, string? server, string? password)
        {
            _client = client;
            _server = server;
            _password = password;
        }

        public void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
