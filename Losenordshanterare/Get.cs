using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Crypto = System.Security.Cryptography;

namespace Losenordshanterare
{
    internal class Get : ICommand
    {
        //private readonly string? _client;
        //private readonly string? _server;
        //private readonly string? _property;
        //private readonly string? _password;

        //public Get(string[] args)
        //{
        //    if (args.Length != 5)
        //        throw new ArgumentException("Incorrect number of arguments for get command. Number of arguments: " + args.Length);

        //    _client = args[1];
        //    _server = args[2];
        //    _property = args[3];
        //    _password = args[4];
        //}

        //public void Execute()
        //{
        //    SecretKey clientKey = FileService.ReadSecretKeyFromFile(_client);

        //    string serverContent = FileService.ReadFile(_server);
        //    ServerFormat serverFormat = JsonSerializer.Deserialize<ServerFormat>(serverContent);
        //    byte[] iv = serverFormat.IV;

        //    byte[] encryptedPasswordBytes = GetEncryptedPasswords(serverFormat);

        //    VaultKey vk = new VaultKey(_password, clientKey);

        //    string decryptedPasswordsJson = DecryptPasswords(iv, encryptedPasswordBytes, vk);

        //    var propertyPasswordDict = JsonSerializer.Deserialize<Dictionary<string, string>>(decryptedPasswordsJson);

        //    PrintPasswords(propertyPasswordDict);
        //}

        //private byte[] GetEncryptedPasswords(ServerFormat serverFormat)
        //{
        //    string encryptedPasswords = JsonSerializer.Serialize(serverFormat.PropertyPassword);
        //    return Encoding.UTF8.GetBytes(encryptedPasswords);
        //}

        //private string DecryptPasswords(byte[] iv, byte[] encryptedPasswordBytes, VaultKey vk)
        //{
        //    string decryptedPasswordsJson;
        //    using (Crypto.Aes aes = Crypto.Aes.Create())
        //    {
        //        aes.IV = iv;
        //        decryptedPasswordsJson = Encryption.Decrypt(encryptedPasswordBytes, vk, aes);
        //    }
        //    return decryptedPasswordsJson;
        //}

        //private void PrintPasswords(Dictionary<string, string> propertyPasswordDict)
        //{
        //    if (!string.IsNullOrEmpty(_property))
        //    {
        //        if (propertyPasswordDict.TryGetValue(_property, out string password))
        //        {
        //            Console.WriteLine($"Password for {_property}: {password}");
        //        }
        //        else
        //        {
        //            Console.WriteLine($"Property {_property} not found.");
        //        }
        //    }
        //    else
        //    {
        //        foreach (var pair in propertyPasswordDict)
        //        {
        //            Console.WriteLine($"Property: {pair.Key}, Password: {pair.Value}");
        //        }
        //    }
        //}
    }
}
