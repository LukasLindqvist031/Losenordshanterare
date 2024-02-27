using System;
using System.Security.Cryptography;

public class AesObject
{
    public byte[] Key { get; set; }
    public byte[] IV { get; set; }
    public CipherMode Mode { get; set; }
    public PaddingMode Padding { get; set; }

    public AesObject(Aes aes)
    {
        Key = aes.Key;
        IV = aes.IV;
        Mode = aes.Mode;
        Padding = aes.Padding;
    }
}
