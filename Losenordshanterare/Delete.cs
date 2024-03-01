﻿using System;
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
        private readonly string? _client;
        private readonly string? _server;
        private readonly string? _property;
        private string? _masterPassword;

        public Delete(string[] args)
        {
            if (args.Length < 4 || args.Length > 4)
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
            _property = args[3];
        }

        public void Execute()
        {
            string[] inputArr = GetInput();
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
                Dictionary<string, string> serverDict = ConvertToDict(encryptedBase64, base64IV);
                string jsonDict = SerializeDict(serverDict);
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

        private string[] GetInput() => UserPrompt.PromptUserSet();

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
