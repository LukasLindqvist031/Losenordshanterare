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
        private readonly string? _password;

        public Get(string[] args)
        {
            if (args.Length != 5)
                throw new ArgumentException("Incorrect number of arguments for get command. Number of arguments: " + args.Length);

            _client = args[1];
            _server = args[2];
            _property = args[3];
            _password = args[4];
        }

        public void Execute()
        {
            SecretKey clientKey = FileService.ReadSecretKeyFromFile(_client);

            string serverContent = FileService.ReadFile(_server);
            ServerFormat serverFormat = JsonSerializer.Deserialize<ServerFormat>(serverContent);
            byte[] iv = serverFormat.IV;

            string encryptedPasswords = JsonSerializer.Serialize(serverFormat.PropertyPassword);
            byte[] encryptedPasswordBytes = Encoding.UTF8.GetBytes(encryptedPasswords);

            VaultKey vk = new VaultKey(_password, clientKey);

            string decryptedPasswordsJson;
            using (Aes aes = Aes.Create())
            {
                aes.IV = iv;
                decryptedPasswordsJson = Encryption.Decrypt(encryptedPasswordBytes, vk, aes);
            }

            var propertyPasswordDict = JsonSerializer.Deserialize<Dictionary<string, string>>(decryptedPasswordsJson);

            if (!string.IsNullOrEmpty(_property))
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

            else
            {
                foreach (var pair in propertyPasswordDict)
                {
                    Console.WriteLine($"Property: {pair.Key}, Password: {pair.Value}");
                }
            }
        }
    }
}
