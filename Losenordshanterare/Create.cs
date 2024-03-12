using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Losenordshanterare
{
    internal class Create : ICommand
    {
        private readonly string _client;
        private readonly string _server;
        private string _masterPassword;
        private string _secret;

        public Create(string[] args)
        {
            if (ValidateArguments.IsValidLengthCreate(args) && ValidateArguments.IsValidArgument(args))
            {
                _client = args[1];
                _server = args[2];
                _masterPassword = RetrieveUserValues.GetMasterPass();
                _secret = RetrieveUserValues.GetSecret();
            }
            else
            {
                throw new Exception("Failed to instantiate create.");
            }
        }

        public void Execute()
        {
            try
            {
                string trimSecret = RemoveInvalidChars(_secret);
                byte[] arr = Convert.FromBase64String(trimSecret);
                SecretKey secretKey = new SecretKey(arr);
                VaultKey vaultKey = new(_masterPassword, secretKey);
                byte[] iv = FileService.ReadIVFromFile(_server);
                string base64Vault = FileService.ReadVaultFromFile(_server);
                string jsonSecret = Converter.ConvertSecretKeyToJson(secretKey.GetKey);

                if (IsDecrypted(base64Vault, vaultKey, iv))
                {
                    FileService.CreateFile(_client);
                    FileService.WriteToFile(jsonSecret, _client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to execute 'create'. Error: {ex.Message}");
            }
        }

        public static bool IsDecrypted(string base64Vault, VaultKey vaultKey, byte[] iv)
        {
            try
            {
                Dictionary<string, string> dict = Vault.DecryptVault(base64Vault, vaultKey, iv);
                Vault vault = new Vault(dict);
                Aes aes = Aes.Create();

                return true;
            }
            catch (Exception ex) { Console.WriteLine($"Could not decrypt the vault. Error: {ex.Message}"); return false; }

        }


        private static string RemoveInvalidChars(string input)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            return regex.Replace(input, string.Empty);
        }
    }
}
