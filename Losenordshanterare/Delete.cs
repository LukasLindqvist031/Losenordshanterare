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
        private readonly string _client;
        private readonly string _server;
        private readonly string _property;
        private readonly string _masterPassword;

        public Delete(string[] args)
        {
            if (ValidateArguments.IsValidLengthDelete(args) && ValidateArguments.IsValidArgument(args))
            {
                _client = args[1];
                _server = args[2];
                _property = args[3];
                _masterPassword = RetrieveUserValues.GetMasterPass();
            }
            else
            {
                throw new Exception("Failed to instantiate delete.");
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

                vault.DeleteFromVault(_property);
                string encryptedBase64 = vault.EncryptVault(vaultKey, aes);
                string base64IV = Convert.ToBase64String(aes.IV);
                string jsonDict = Converter.ConvertToJson(encryptedBase64, base64IV);
                FileService.WriteToFile(jsonDict, _server);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to execute 'delete'. Error: {ex.Message}");
            }

        }


    }
}
