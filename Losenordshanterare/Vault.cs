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
        if(!string.IsNullOrWhiteSpace(property) && !string.IsNullOrWhiteSpace(password))
        {
            _logInDict[property] = password;
        }
        else
        {
            throw new NullOrWhiteSpaceArgumentException("Key and value cannot be null or whitespace!"); 
        }  
    }

    public string EncryptVault()
    {
        string jsonDict = SerializeVault();
        byte[] encryptedVault = Encrypt(jsonDict);
        string vaultBase64 = VaultToBase64(encryptedVault);
        string jsonBase64 = Base64ToJson(vaultBase64);
        return jsonBase64;
    }

    
    //Used when EncryptVault is called.
    private string Base64ToJson(string encodedData) => JsonSerializer.Serialize(new { EncryptedData = encodedData });
    private string VaultToBase64(byte[] encryptedVault) => Convert.ToBase64String(encryptedVault);
    private byte[] Encrypt(string jsonVault) => Encryption.Encrypt(jsonVault, _vaultKey, _aes);
    private string SerializeVault() => JsonSerializer.Serialize(_logInDict);


    public Vault DecryptVault(string vaultBase64)
    {
        byte[] encryptedVault = Base64ToVault(vaultBase64);
        string decryptedVault = Decrypt(encryptedVault);
        Vault vault = DeserializeVault(decryptedVault);
        return vault;
    }

    //Used when DecryptVault is called.
    private Vault DeserializeVault(string jsonDict) => JsonSerializer.Deserialize<Vault>(jsonDict);
    private string Decrypt(byte[] encryptedVault) => Encryption.Decrypt(encryptedVault, _vaultKey, _aes);
    private byte[] Base64ToVault(string vaultBase64) => Convert.FromBase64String(vaultBase64);
    
    

}
