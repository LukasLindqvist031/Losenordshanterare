using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class Secret : ICommand
    {
        private readonly string? _client;

        public Secret(string[] args)
        {
            if (args.Length < 2 || args.Length > 2)
            {
                throw new InvalidNumberOfArgumentsException($"Error: Expected 2 arguments, but received {args.Length}.");
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(args[i]))
                {
                    throw new NullOrWhiteSpaceArgumentException($"Error: Argument '{args[i]}' at index {i} cannot be null or whitespace.");
                }
            }
            _client = args[1];
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
            Console.WriteLine(base64);
        }
    }
}
