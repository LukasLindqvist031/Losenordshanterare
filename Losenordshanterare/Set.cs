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
        private readonly string? _client;
        private readonly string? _server;
        private readonly string? _property;
        private string? _masterPassword;
        private string? _valuePassword;

        public Set(string[] args)
        {
            if (args.Length < 4 || args.Length > 5)
            {
                throw new InvalidNumberOfArgumentsException($"Error: Expected 4 or 5 arguments, but received {args.Length}.");
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(args[i]))
                {
                    throw new NullOrWhiteSpaceArgumentException($"Error: Argument '{args[i]}' at index {i} cannot be null or whitespace.");
                }
            }

            if (args.Length == 4)
            {
                _client = args[1];
                _server = args[2];
                _property = args[3];
            }
            else if (args.Length == 5 && IsAutoGenerate(args[4]) == true)
            {
                _client = args[1];
                _server = args[2];
                _property = args[3];
                _valuePassword = RandomPasswordGenerator.NewPassword();
            }

        }

        public void Execute()
        {
            string[] inputArr = UserInput.GetInput(_valuePassword);
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
                throw new ArgumentException("Incorrect term for auto generated password. Correct terms are '-g' or '--generate'.");
            }
        }

        private void ProcessInput(string[] inputArr)
        {
            if (inputArr.Length > 1)
            {
                _masterPassword = inputArr[0];
                _valuePassword = inputArr[1];
            }
            else
            {
                _masterPassword = inputArr[0];
            }
        }
    }
}
