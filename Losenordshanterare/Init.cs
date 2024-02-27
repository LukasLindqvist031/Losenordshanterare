using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class Init : ICommand
    {
        private readonly string? _client;
        private readonly string? _server;
        private readonly string? _password;
        private readonly SecretKey _secretKey;
        private readonly VaultKey _vaultKey;
        private readonly AesObject _aes;
        private readonly Vault _vault;

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
                throw new ArgumentException("The number of arguments are invalid. Try again.");
            }

            _secretKey = new SecretKey();
            _vaultKey = new VaultKey(_password, _secretKey);
            _aes = new AesObject(_vaultKey);
            _vault = new Vault();
        }
        


    }
}
