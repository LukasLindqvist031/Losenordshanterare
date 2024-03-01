using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Losenordshanterare
{
    internal class Create : ICommand
    {
        private readonly string? _client;
        private readonly string? _server;
        private string? _masterPassword;
        private string? _secret;

        public Create(string[] args)
        {
            if (args.Length < 3 || args.Length > 3)
            {
                throw new InvalidNumberOfArgumentsException($"Error: Expected 3 arguments, but received {args.Length}.");
            }

            if(args.Length == 3)
            {
                _client = args[1];
                _server = args[2];
            }
        }
        private SecretKey GetSecretKey(string clientPath) => FileService.ReadSecretKeyFromFile(clientPath);

        public void Execute()
        {
            string[] inputArr = GetInput();
            ProcessInput(inputArr);
            SecretKey secretKey = GetSecretKey(_client);
            VaultKey vaultKey = new(_masterPassword, secretKey);
            byte[] iv = FileService.ReadIVFromFile(_server);
            string base64Vault = FileService.ReadVaultFromFile(_server);

            if (IsValidate(base64Vault, vaultKey, iv))
            {
                FileService.CreateFile(_client);
            }
        }

        public bool IsValidate(string base64Vault, VaultKey vaultKey, byte[] iv)
        {
            try
            {
                Dictionary<string, string> dict = Vault.DecryptVault(base64Vault, vaultKey, iv);
                Vault vault = new Vault(dict);
                Aes aes = Aes.Create();

                return true;
            }
            catch (Exception ex) { Console.WriteLine("Could not decrypt the vault. Access denied."); return false; }

        }

        private void ProcessInput(string[] inputArr)
        {
            if (inputArr.Length > 1)
            {
                _masterPassword = inputArr[0];
                _secret = inputArr[1];
            }
            else
            {
                _masterPassword = inputArr[0];
            }
        }


        private string[] GetInput()
        {
            if (string.IsNullOrEmpty(_secret))
            {
                return UserPrompt.PromptUserSet(_secret);
            }
            else
            {
                return UserPrompt.PromptUserSet();
            }
        }
    }
}
