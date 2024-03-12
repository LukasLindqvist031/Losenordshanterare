using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.Json;
using System.Buffers.Text;

namespace Losenordshanterare
{
    internal class Set : ICommand
    {
        private readonly string _client;
        private readonly string _server;
        private readonly string _property;
        private readonly string _masterPassword;
        private readonly string _valuePassword;

        public Set(string[] args)
        {
            if (args.Length == 4 && ValidateArguments.IsValidLengthSet(args) && ValidateArguments.IsValidArgument(args) && !IsAutoGenerate(args[3]))
            {
                _client = args[1];
                _server = args[2];
                _property = args[3];
                _masterPassword = RetrieveUserValues.GetMasterPass();
                _valuePassword = RetrieveUserValues.GetValuePass();
            }
            else if (args.Length == 5 && IsAutoGenerate(args[4]) == true && ValidateArguments.IsValidLengthSet(args) && ValidateArguments.IsValidArgument(args))
            {
                _client = args[1];
                _server = args[2];
                _property = args[3];
                _valuePassword = RandomPasswordGenerator.GeneratePassword();
                _masterPassword = RetrieveUserValues.GetMasterPass();
            }
            else
            {
                throw new Exception("Failed to instantiate Set object.");
            }
        }

        public void Execute()
        {
            try
            {
                SecretKey secretKey = FileService.ReadSecretKeyFromFile(_client);
                VaultKey vaultKey = new(_masterPassword, secretKey);
                byte[] iv = FileService.ReadIVFromFile(_server);
                string base64Vault = FileService.ReadVaultFromFile(_server);

                Dictionary<string, string> dict = Vault.DecryptVault(base64Vault, vaultKey, iv);
                Vault vault = new Vault(dict);
                Aes aes = Aes.Create();

                vault.AddToVault(_property, _valuePassword);
                string encryptedBase64 = vault.EncryptVault(vaultKey, aes);
                string base64IV = Convert.ToBase64String(aes.IV);
                string jsonDict = Converter.ConvertToJson(encryptedBase64, base64IV);
                FileService.WriteToFile(jsonDict, _server);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to execute 'set'. Error: {ex.Message}");
            }

        }

        private bool IsAutoGenerate(string arg)
        {
            const string g = "-g";
            const string generate = "--generate";

            if (arg == g || arg == generate)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        
    }
}
