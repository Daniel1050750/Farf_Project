using System;
using System.Collections.Generic;
using System.Text;

namespace Farf_Project.Core
{
    public class DateTimeHelper
    {
        #region Private Constants

        private const string DATE_FORMAT = "O";
        
        #endregion

        #region Public Static Methods

        public static DateTime ParserDateTime(string dateTimeString)
        {
            if (string.IsNullOrEmpty(dateTimeString))
            {
                return DateTime.MinValue;
            }

            return DateTime.Parse(dateTimeString);
        }

        public static string ConvertDateTimeToString(DateTime date)
        {
            if (date == null)
            {
                return string.Empty;
            }

            return date.ToUniversalTime().ToString(DATE_FORMAT);
        }

        public static bool IsStringValidDateTime(string dateTimeString)
        {
            if (string.IsNullOrEmpty(dateTimeString))
            {
                return false;
            }

            return DateTime.TryParse(dateTimeString, out DateTime res);
        }

        #endregion
    }
}
