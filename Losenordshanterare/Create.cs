using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.Net.Sockets;

namespace Losenordshanterare
{
    internal class Create : ICommand
    {
        private readonly string _client;
        private readonly string _server;
        private string _masterPassword = string.Empty;
        private string _secret = string.Empty ;

        public Create(string[] args)
        {
            if (ValidateArguments.IsValidLengthCreate(args) && ValidateArguments.IsValidArgument(args))
            {
                _client = args[1];
                _server = args[2];
            }
            else
            {
                throw new Exception("Failed to instantiate Create object.");
            }
        }

        public void Execute()
        {
            string[] inputArr = UserInput.GetInputCreate();
            ProcessInput(inputArr);
            string secret = inputArr[1];
            string trimSecret = RemoveInvalidChars(secret);
            byte[] arr = Convert.FromBase64String(trimSecret);
            SecretKey secretKey = new SecretKey(arr);
            VaultKey vaultKey = new(_masterPassword, secretKey);
            byte[] iv = FileService.ReadIVFromFile(_server);
            string base64Vault = FileService.ReadVaultFromFile(_server);
            string jsonSecret = Converter.ConvertSecretKeyToJson(secretKey.GetKey);

            if (IsValidate(base64Vault, vaultKey, iv))
            {
                FileService.CreateFile(_client);
                FileService.WriteToFile(jsonSecret, _client);
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
            catch (Exception ex) { Console.WriteLine($"Could not decrypt the vault. Error: {ex.Message}"); return false; }

        }

        private void ProcessInput(string[] inputArr)
        {
             _masterPassword = inputArr[0];
             _secret = inputArr[1];
        }

        private string RemoveInvalidChars(string input)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            return regex.Replace(input, string.Empty);
        }
    }
}
