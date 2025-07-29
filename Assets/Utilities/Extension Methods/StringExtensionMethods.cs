using System;

namespace ExtensionMethods
{

    public static class StringExtensionMethods
    {

        /// <summary>
        /// Sets the maximum length of the string to be the given value
        /// </summary>
        /// <param name="value">string to change</param>
        /// <param name="maxLength">New maximum number of characters in string value</param>
        /// <returns></returns>
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static bool EqualsCaseInsensitive(this string originalString, string comparedString)
        {
            return originalString.ToLower().Equals(comparedString.ToLower());
        }
        public static string RemoveLineEndings(this string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();

            return value.Replace("\r\n", string.Empty)
                        .Replace("\n", string.Empty)
                        .Replace("\r", string.Empty)
                        .Replace(lineSeparator, string.Empty)
                        .Replace(paragraphSeparator, string.Empty);
        }

    }
}