using Losenordshanterare;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Cryptography;

internal class Vault
{
    private Dictionary<string, string> _logInDict = new Dictionary<string, string>();
    private readonly VaultKey _vaultKey;
    private readonly Aes _aes;

    public Vault(VaultKey vaultKey, Aes aes) 
    { 
        _vaultKey = vaultKey; 
        _aes = aes;
    }

    public Dictionary<string, string> GetVault => _logInDict;

    public void AddToVault(string property, string password)
    {
        if(!string.IsNullOrEmpty(property) && !string.IsNullOrEmpty(password))
        {
            _logInDict[property] = password;
        }
        else
        {
            throw new ArgumentException("Key and value cannot be empty. Try again."); 
        }  
    }

    public string EncryptVault()
    {
        string jsonDict = SerializeVault();
        byte[] encryptedVault = EncryptDict(jsonDict);
        string vaultBase64 = VaultToBase64(encryptedVault);
        string jsonBase64 = Base64ToJson(vaultBase64);
        return jsonBase64;
    }

    private string Base64ToJson(string encodedData) => JsonSerializer.Serialize(new { EncryptedData = encodedData });

    private string VaultToBase64(byte[] encryptedVault) => Convert.ToBase64String(encryptedVault);

    private byte[] EncryptDict(string jsonVault) => Encryption.Encrypt(jsonVault, _vaultKey, _aes);

    private string SerializeVault() => JsonSerializer.Serialize(_logInDict);

}
