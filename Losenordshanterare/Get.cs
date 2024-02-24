using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class Get : ICommand
    {
        private readonly string? _client;
        private readonly string? _server;
        private readonly string? _property;
        private readonly string? _password;

        public Get(string? client, string? server, string? property, string? password)
        {
            _client = client;
            _server = server;
            _property = property;
            _password = password;
        }
        public void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
