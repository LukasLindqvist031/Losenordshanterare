﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Losenordshanterare
{
    internal class InvalidNumberOfArgumentsException : Exception
    {
        public InvalidNumberOfArgumentsException(string message) : base(message) { }
    }
}
