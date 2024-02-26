using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class VaultKey
    {
        private readonly string? _vaultKey;
        private readonly SecretKey _secretKey = new SecretKey();

        public VaultKey()
        {

        }

    }
}
