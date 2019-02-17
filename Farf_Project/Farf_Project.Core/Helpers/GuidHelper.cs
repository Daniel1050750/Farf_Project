using System;
using System.Collections.Generic;
using System.Text;

namespace Farf_Project.Core
{
    public static class GuidHelper
    {
        public static string GuidToString(Guid value)
        {
            if (Guid.Empty.Equals(value))
            {
                return null;
            }
            return value.ToString();
        }

        public static Guid StringToGuid(string value)
        {
            if (!string.IsNullOrEmpty(value) && Guid.TryParse(value, out Guid guidValue)) {
                return guidValue;
            }
            return Guid.Empty;
        }
    }
}
