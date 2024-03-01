using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;

namespace Losenordshanterare
{
    internal class Get : ICommand
    {
        private readonly string _client;
        private readonly string _server;
        private readonly int _argsLength;
        private readonly string _property = string.Empty;
        private string _masterPassword = string.Empty;
       

        public Get(string[] args)
        {
            _argsLength = args.Length;

            if (_argsLength == 3 && ValidateArguments.IsValidLengthGet(args) && ValidateArguments.IsValidArgument(args))
            {
                _client = args[1];
                _server = args[2];
            }
            else if (_argsLength == 4 && ValidateArguments.IsValidLengthGet(args) && ValidateArguments.IsValidArgument(args))
            {
                _client = args[1];
                _server = args[2];
                _property = args[3];
            }
            else
            {
                throw new Exception("Failed to instantiate Get object.");
            }
        }
        


        public void Execute()
        {
            try
            {
                string[] inputArr = UserInput.GetInput();
                ProcessInput(inputArr);
                SecretKey secretKey = FileService.ReadSecretKeyFromFile(_client);
                VaultKey vaultKey = new VaultKey(_masterPassword, secretKey);
                byte[] iv = FileService.ReadIVFromFile(_server);
                string base64Vault = FileService.ReadVaultFromFile(_server);

                Dictionary<string, string> dict = Vault.DecryptVault(base64Vault, vaultKey, iv);
                Vault vault = new Vault(dict);

                PrintPasswords(vault, _property);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Failed to execute 'get'. Error: {ex.Message}");
            }
        }

        private void ProcessInput(string[] inputArr)
        {
            _masterPassword = inputArr[0];
        }

        private void PrintPasswords(Vault vault, string property)
        {
            if (string.IsNullOrEmpty(property))
            {
                vault.PrintProperties();
            }
            else
            {
                vault.PrintPropAndPass(property);
            }
            
        }

    }
}
