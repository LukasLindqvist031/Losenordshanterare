using System;

public class AesObject
{
    public byte[] Key { get; set; }
    public byte[] Value { get; set; }
    public CipherMode Mode { get; set; }

    public AesObject(Aes aes)
	{
        Key = aes.Key;
        IV = aes.IV;
        Mode = aes.Mode;
    }
}
