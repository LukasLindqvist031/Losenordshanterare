using System;

public class Vault
{
    private Dictionary<string, string> Vault = new Dictionary<string, string>;
    private VaultKey vaultKey;
    private string path;

    public Vault(string path, VaultKey vaultkey)
    {
        this.path = path;
        this.vaultKey = vaultkey;

        if (!File.Exists(path))
        {
            File.Create(path).Close();
            try
            {
                byte[] vaultBytes = File.ReadAllBytes(path);

                if (vaultBytes.Lenght > 0)
                {
                    //Funktionalitet för att låsa upp valvet 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The following error occured {ex}");
            }     
        }
    }
}
