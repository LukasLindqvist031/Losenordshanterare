using Losenordshanterare;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Cryptography;

internal class Vault
{
    private Dictionary<string, string> _logInDict = new Dictionary<string, string>();
    
    public Vault() { }

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


    public Vault DecryptVault(string vaultBase64, VaultKey vk, Aes aes)
    {
        byte[] encryptedVault = Base64ToVault(vaultBase64);
        string decryptedVault = Decrypt(encryptedVault, vk, aes);
        Vault vault = DeserializeVault(decryptedVault);
        return vault;
    }

    //Used when DecryptVault is called.
    private Vault DeserializeVault(string jsonDict) => JsonSerializer.Deserialize<Vault>(jsonDict);
    private string Decrypt(byte[] encryptedVault, VaultKey vk, Aes aes) => Encryption.Decrypt(encryptedVault, vk, aes);
    private byte[] Base64ToVault(string vaultBase64) => Convert.FromBase64String(vaultBase64);




}
