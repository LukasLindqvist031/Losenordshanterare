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
                throw new InvalidNumberOfArgumentsException($"Error: Expected 4 arguments, but received {args.Length}.");
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(args[i]))
                {
                    throw new NullOrWhiteSpaceArgumentException($"Error: Argument '{args[i]}' at index {i} cannot be null or whitespace.");
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

        public void Execute(string property)
        {
            string encryptedVault = _vault.EncryptVault(); 
            string encodedIV = ConvertIVToJson(); 

            var propertyPassword = new Dictionary<string, string>
    {
        { "Property", property }, 
        { "Password", encryptedVault } 
    };

            
            var serverData = new
            {
                EncodedIV = encodedIV, 
                PropertyPassword = propertyPassword 
            };

            string jsonServerData = JsonSerializer.Serialize(serverData, new JsonSerializerOptions { WriteIndented = true });

            try
            {
                FileService.CreateFile(_client);
                FileService.CreateFile(_server);
                FileService.WriteToFile(jsonServerData, _server); 
                FileService.WriteToFile(ConvertSecretKeyToJson(), _client); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to execute 'init'. Error: {ex.Message}");
            }
        }


        private string ConvertIVToJson()
        {
            return Convert.ToBase64String(_aes.IV);
        }

        private string ConvertSecretKeyToJson()
        {
            string encodedSecret = Convert.ToBase64String(_secretKey.GetKey);
            return JsonSerializer.Serialize(new { Secret = encodedSecret });
        }

        private Dictionary<string, string> ConvertToDict(string vault, string iv)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["EncodedIV"] = iv;
            dict["EncryptedVault"] = vault;           
            return dict;
        }

        private string SerializeDict(Dictionary<string, string> dict) => JsonSerializer.Serialize(dict);
    }  
    
}
