using System;
using System.Collections.Generic;
using System.Text;

namespace Farf_Project.Core
{
    public class UnauthorizedException: Exception
    {
        public UnauthorizedException()
        {
        }

        public UnauthorizedException(string message)
            : base (message)
        {
        }

        public UnauthorizedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
