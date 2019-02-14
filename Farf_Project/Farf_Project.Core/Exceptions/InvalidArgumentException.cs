using System;
using System.Collections.Generic;
using System.Text;

namespace Farf_Project.Core
{
    public class InvalidArgumentException: Exception
    {
        public InvalidArgumentException()
        {
        }

        public InvalidArgumentException(string message)
            : base (message)
        {
        }

        public InvalidArgumentException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
