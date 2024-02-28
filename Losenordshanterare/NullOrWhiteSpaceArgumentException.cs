using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class NullOrWhiteSpaceArgumentException : Exception
    {
        public NullOrWhiteSpaceArgumentException(string message) : base(message) { }
    }
}
