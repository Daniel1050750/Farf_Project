using System;

namespace Farf_Project.Core
{
    public class CloneableObject : Object, ICloneable
    {
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}