﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Losenordshanterare
{
    internal class Set : ICommand
    {
        private readonly string? _client;
        private readonly string? _server;
        private readonly string? _property;
        private string? _masterPassword;
        private string? _valuePassword;
        private readonly Aes _aes;
       
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
            else if(args.Length == 5 && IsAutoGenerate(args[4]) == true) 
            {
                _client = args[1];
                _server = args[2];
                _property = args[3];
                _valuePassword = RandomPasswordGenerator.NewPassword();
            }

            _aes = Aes.Create();

        }

        public void Execute()
        {
            string[] inputArr = GetInput();
            ProcessInput(inputArr);
            SecretKey secretKey = GetSecretKey(_client);
            VaultKey vaultKey = new(_masterPassword, secretKey);
            _aes.IV = FileService.ReadIVFromFile(_server);
            string encryptedVault = FileService.ReadVaultFromFile(_server);
            Vault vault = new(vaultKey, _aes);
            vault = vault.DecryptVault(encryptedVault);

            try
            {
                vault.AddToVault(_property, _valuePassword);
                Console.WriteLine("Everything fine so far!");
                vault.EncryptVault();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to execute 'set'. Error: {ex.Message}");
            }

        }

        private void ProcessInput(string[] inputArr)
        {
            if(inputArr.Length > 1)
            {
                _masterPassword = inputArr[0];
                _valuePassword = inputArr[1];
            }
            else
            {
                _masterPassword = inputArr[0];
            }
        }

        private string[] GetInput()
        {
            if (string.IsNullOrEmpty(_valuePassword))
            {
                return UserPrompt.PromptUserSet(_valuePassword);
            }
            else
            {
                return UserPrompt.PromptUserSet();
            }
        }

        private SecretKey GetSecretKey(string clientPath) => FileService.ReadSecretKeyFromFile(clientPath);

                
        private bool IsAutoGenerate(string arg)
        {
            const string g = "-g";
            const string generate = "--generate";

            if(arg == g || arg == generate)
            {
                return true;

            }
            else
            {
                throw new ArgumentException("Incorrect term for auto generated password. Correct terms are '-g' or '--generate'.");
            }
        }
    }
}
