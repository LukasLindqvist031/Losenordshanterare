using Losenordshanterare;
using System;
using System.Security.Cryptography;

internal class AesObject
{
    private readonly VaultKey _vaultKey;
    private readonly byte[] _iv;

    public AesObject(VaultKey vaultKey)
    {
        Aes aes = Aes.Create();
        _iv = aes.IV;
        _vaultKey = vaultKey;
    }

    public byte[] GetIV => _iv;
    public VaultKey GetVaultKey => _vaultKey;

    
}
