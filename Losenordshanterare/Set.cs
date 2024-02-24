using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class Set : ICommand
    {
        private readonly string? _client;
        private readonly string? _server;
        private readonly string? _property;
        private readonly string? _password;
        private readonly string? _value;

        public Set(string? client, string? server, string? property)
        {
            _client = client;
            _server = server;
            _property = property;
        }

        public void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
