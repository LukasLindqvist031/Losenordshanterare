using System;

public class Vault
{
    private Dictionary<string, string> _logInDict = new Dictionary<string, string>();

    public Vault() { }

    public Dictionary<string, string> GetVault => _logInDict;
}
