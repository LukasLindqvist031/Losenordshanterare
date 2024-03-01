using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class Secret : ICommand
    {
        private readonly string _client;

        public Secret(string[] args)
        {
            if (ValidateArguments.IsValidLengthSecret(args) && ValidateArguments.IsValidArgument(args))
            {
                _client = args[1];
            }
            else
            {
                throw new Exception("Failed to instantiate Delete object.");
            }
            
        }

        public void Execute()
        {
            try
            {
                SecretKey key = FileService.ReadSecretKeyFromFile(_client);
                byte[] keyArr = key.GetKey;
                PrintSecretKey(keyArr);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to execute 'secret'. Error: {ex.Message}");
            }
        }

        private void PrintSecretKey(byte[] keyArr)
        {
            string base64 = Convert.ToBase64String(keyArr);
            Console.WriteLine($"Secret Key: {base64}");
        }
    }
}
