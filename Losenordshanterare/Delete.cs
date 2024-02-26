using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class Delete : ICommand
    {
        private readonly string? _client;
        private readonly string? _server;
        private readonly string? _property;
        private readonly string? _password;

        public Delete(string? client, string? server, string? property, string? password)
        {
            _client = client;
            _server = server;
            _property = property;
            _password = password;
        }
        private void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
