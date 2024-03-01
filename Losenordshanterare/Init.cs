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
        private readonly string _client;
        private readonly string _server;
        private readonly string _password;
        private readonly SecretKey _secretKey;
        private readonly VaultKey _vaultKey;
        private readonly Vault _vault;
        private readonly Aes _aes;

        public Init(string[] args)
        {
            if (ValidateArguments.IsValidLengthInit(args) && ValidateArguments.IsValidArgument(args))
            {
                _client = args[1];
                _server = args[2];
                _password = args[3];
                _secretKey = new SecretKey();
                _vaultKey = new VaultKey(_password, _secretKey);
                _aes = Aes.Create();
                _vault = new Vault();
            }
            else
            {
                throw new Exception("Failed to instantiate Init object.");
            }
        }

        public void Execute()
        {
            string base64Vault = _vault.EncryptVault(_vaultKey, _aes);
            string base64IV = Converter.ConvertIVToBase64(_aes.IV);
            string jsonSecretKey = Converter.ConvertSecretKeyToJson(_secretKey.GetKey);
            string jsonDict = Converter.ConvertToJson(base64Vault, base64IV);

            try
            {
                FileService.CreateFile(_client);
                FileService.CreateFile(_server);
                FileService.WriteToFile(jsonDict, _server); 
                FileService.WriteToFile(jsonSecretKey, _client); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to execute 'init'. Error: {ex.Message}");
            }
        }

    
    }  
    
}
