using Losenordshanterare;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Cryptography;
using System.Collections.Generic;

internal class Vault
{
    private Dictionary<string, string> _logInDict;
    
    public Vault() { _logInDict = new(); }

    public Vault(Dictionary<string, string> dict)
    {
        _logInDict = new Dictionary<string, string> (dict);
    }

    public Dictionary<string, string> GetVault => _logInDict;

    public void AddToVault(string property, string password)
    {
        if (!string.IsNullOrWhiteSpace(property) && !string.IsNullOrWhiteSpace(password))
        {
            _logInDict[property] = password;
        }
        else
        {
            throw new NullOrWhiteSpaceArgumentException("Key and value cannot be null or whitespace!");
        }
    }

    public void DeleteFromVault(string property)
    {
        if (!string.IsNullOrWhiteSpace(property))
        {
            _logInDict.Remove(property);
        }
        else
        {
            throw new NullOrWhiteSpaceArgumentException("Property cannot be null or whitespace!");
        }
    }

    public void PrintProperties()
    {
        foreach(KeyValuePair<string, string> kvp in _logInDict)
        {
            Console.WriteLine(kvp.Key);
        }
    }

    public void PrintPass(string property)
    {
        if(_logInDict.TryGetValue(property, out string? password))
        {
            Console.WriteLine(password);
        }
    }


    public string EncryptVault(VaultKey vk, Aes aes)
    {
        string jsonDict = SerializeVault();
        byte[] encryptedVault = Encrypt(jsonDict, vk, aes);
        string vaultBase64 = VaultToBase64(encryptedVault);
        return vaultBase64;
    }


    //Used when EncryptVault is called.
    private string VaultToBase64(byte[] encryptedVault) => Convert.ToBase64String(encryptedVault);
    private byte[] Encrypt(string jsonVault, VaultKey vk, Aes aes ) => Encryption.Encrypt(jsonVault, vk, aes);
    private string SerializeVault() => JsonSerializer.Serialize(_logInDict);


    public static Dictionary<string, string> DecryptVault(string vaultBase64, VaultKey vk, byte[] oldIV)
    {
        byte[] vaultArr = Base64ToByteArr(vaultBase64);
        string decryptedVault = Decrypt(vaultArr, vk, oldIV);
        return DeserializeVault(decryptedVault);   
    }

    //Used when DecryptVault is called.
    private static Dictionary<string, string> DeserializeVault(string jsonDict)
    {
        Dictionary<string, string>? dict = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonDict);

        if(dict == null)
        {
            throw new Exception("Error: Couldn't deserialize vault.");
        }

        return dict;
    }
    private static string Decrypt(byte[] encryptedVault, VaultKey vk, byte[] oldIV) => Encryption.Decrypt(encryptedVault, vk, oldIV);
    private static byte[] Base64ToByteArr(string vaultBase64) => Convert.FromBase64String(vaultBase64);



}
