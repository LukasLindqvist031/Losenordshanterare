using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class Secret : ICommand
    {
        private readonly string? _client;

        public Secret(string? client)
        {
            _client = client;
        }
        private void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
