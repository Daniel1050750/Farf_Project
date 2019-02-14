using System;
using System.Collections.Generic;
using System.Text;

namespace Farf_Project.Core
{
    public static class EnumHelper
    {
        #region Private Constants

        private const string DEFAULT_ZERO_VALUE = "0";
        
        #endregion

        #region Public Static Methods
        public static TEnum StringToEnum<TEnum>(string value)
            where TEnum : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(TEnum);
            }

            Enum.TryParse(value, out TEnum type);
            return type;
        }

        public static string EnumToString<TEnum>(TEnum value)
            where TEnum : struct
        {
            var stringValue = value.ToString();
            if (stringValue.Equals(DEFAULT_ZERO_VALUE))
            {
                stringValue = null;
            }

            return stringValue;
        }

        #endregion
    }
}
