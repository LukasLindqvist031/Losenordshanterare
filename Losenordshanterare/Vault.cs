using System;

public class Vault
{
    private Dictionary<string, string> _logInDict = new Dictionary<string, string>();

    public Vault() { }

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
    
}
