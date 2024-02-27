using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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
        private readonly Vault _vault;
        private readonly Aes _aes;

        public Init(string[] args)
        {
            if (args.Length != 4)
            {
                throw new ArgumentException("The number of arguments are invalid. Try again.");
            }

            foreach (string arg in args)
            {
                if (string.IsNullOrWhiteSpace(arg))
                {
                    throw new ArgumentException("Arguments cannot be null or consist of white space.");
                }
            }

            _client = args[1];
            _server = args[2];
            _password = args[3];
            _secretKey = new SecretKey();
            _vaultKey = new VaultKey(_password, _secretKey);
            _aes = Aes.Create();
            _vault = new Vault(_vaultKey, _aes);
        }

        public void Execute()
        {
            string jsonVault = _vault.EncryptVault();
            string jsonIV = ConvertIVToString();
            string jsonSecretKey = ConvertSecretKeyToString();

            try
            {
                FileService.CreateFile(_client);
                FileService.CreateFile(_server);
                FileService.WriteToFile(jsonVault, _server);
                FileService.WriteToFile(jsonIV, _server);
                FileService.WriteToFile(jsonSecretKey, _client);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to execute 'init'. Error: {ex.Message}");
            }
        }

        private string ConvertIVToString()
        {
            string encodedIV = Convert.ToBase64String(_aes.IV);
            string jsonIV = JsonSerializer.Serialize(new { EncodedIV = encodedIV });
            return jsonIV;
        }

        private string ConvertSecretKeyToString()
        {
            string encodedSecret = Convert.ToBase64String(_secretKey.GetKey);
            string jsonSecret = JsonSerializer.Serialize(encodedSecret);
            return jsonSecret;
        }


    }  
    
}
