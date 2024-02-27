using System;

public class Vault
{
    private Dictionary<string, string> LogInDict = new Dictionary<string, string>();
    private string path;

    public Vault(string path)
    {
        this.path = path;

        if (!File.Exists(path))
        {
            File.Create(path).Close();
            try
            {
                byte[] vaultBytes = File.ReadAllBytes(path);

                if (vaultBytes.Length > 0)
                {
                    //Funktionalitet för att låsa upp och läsa från valvet
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The following error occured {ex}");
            }     
        }
    }
}
