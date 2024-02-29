using System;
using System.Collections.Generic;

namespace Losenordshanterare
{
    internal class ServerFormat
    {
        public byte[] IV { get; set; } 
        public Dictionary<string, string> PropertyPassword { get; set; } 

    }
}
