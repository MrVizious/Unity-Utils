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

    }
}