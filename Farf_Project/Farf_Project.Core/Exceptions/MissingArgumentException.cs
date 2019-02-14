using System;
using System.Collections.Generic;
using System.Text;

namespace Farf_Project.Core
{
    public class MissingArgumentException: Exception
    {
        public MissingArgumentException()
        {
        }

        public MissingArgumentException(string message)
            : base (message)
        {
        }

        public MissingArgumentException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
