using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;

namespace Losenordshanterare
{
    internal class Get : ICommand
    {
        private readonly string? _client;
        private readonly string? _server;
        private readonly string? _property;
        private string? _masterPassword;
        private readonly int _argsLength;

        public Get(string[] args)
        {
            _argsLength = args.Length;

            if (args.Length < 3 || args.Length > 4)
            {
                throw new InvalidNumberOfArgumentsException($"Error: Expected 3 or 4 arguments, but received {args.Length}.");
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(args[i]))
                {
                    throw new NullOrWhiteSpaceArgumentException($"Error: Argument '{args[i]}' at index {i} cannot be null or whitespace.");
                }
            }

            if (args.Length == 3)
            {
                _client = args[1];
                _server = args[2];
            }

            if(args.Length == 4)
            {
                _client = args[1];
                _server = args[2];
                _property = args[3];
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

            
            Dictionary<string, string> dict = Vault.DecryptVault(base64Vault, vaultKey, iv);

            PrintPasswords(dict, _argsLength);
        }

        private void ProcessInput(string[] inputArr)
        {
            _masterPassword = inputArr[0];
        }

        private string[] GetInput()
        {
            return UserPrompt.PromptUserSet();
        }

        private void PrintPasswords(Dictionary<string, string> propertyPasswordDict, int argsLength)
        {
            if (argsLength == 3)
            {
                foreach (var pair in propertyPasswordDict)
                {
                    Console.WriteLine($"Property: {pair.Key}");
                }
            }

            if(argsLength == 4)
            {
                if (propertyPasswordDict.TryGetValue(_property, out string password))
                {
                    Console.WriteLine($"Password for {_property}: {password}");
                }
                else
                {
                    Console.WriteLine($"Property {_property} not found.");
                }
            }
        }
    }
}
