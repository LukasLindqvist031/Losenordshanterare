using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Losenordshanterare
{
    internal class Delete : ICommand
    {
        private readonly string _client;
        private readonly string _server;
        private readonly string _property;
        private string _masterPassword = string.Empty;

        public Delete(string[] args)
        {
            if (ValidateArguments.IsValidLengthDelete(args) && ValidateArguments.IsValidArgument(args))
            {
                _client = args[1];
                _server = args[2];
                _property = args[3];
            }
            else
            {
                throw new Exception("Failed to instantiate Delete object.");
            }
        }

        public void Execute()
        {
            string[] inputArr = UserInput.GetInput();
            ProcessInput(inputArr);
            SecretKey secretKey = FileService.ReadSecretKeyFromFile(_client);
            VaultKey vaultKey = new(_masterPassword, secretKey);
            byte[] iv = FileService.ReadIVFromFile(_server);
            string base64Vault = FileService.ReadVaultFromFile(_server);

            Dictionary<string, string> dict = Vault.DecryptVault(base64Vault, vaultKey, iv);
            Vault vault = new Vault(dict);
            Aes aes = Aes.Create();

            try
            {
                vault.DeleteFromVault(_property);
                string encryptedBase64 = vault.EncryptVault(vaultKey, aes);
                string base64IV = Convert.ToBase64String(aes.IV);
                string jsonDict = Converter.ConvertToJson(base64Vault, base64IV);
                FileService.WriteToFile(jsonDict, _server);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to execute 'delete'. Error: {ex.Message}");
            }

        }

        private void ProcessInput(string[] inputArr)
        {
            _masterPassword = inputArr[0];
        }

    }
}
