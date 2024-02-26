using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class Create : ICommand
    {
        private readonly string? _client;
        private readonly string? _server;
        private readonly string? _password;
        private readonly string? _secret;

        public Create(string? client, string? server, string? password, string? secret)
        {
            _client = client;
            _server = server;
            _password = password;
            _secret = secret;
        }

        public void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
