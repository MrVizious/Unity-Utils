using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class DictionaryExtensionMethods
    {
        public static bool AddWithOptionalOverwrite<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value, bool overwrite) where TKey : notnull
        {
            if (overwrite)
            {
                dictionary[key] = value;
                return true;
            }
            try
            {
                // Only is added if there is no data for this key already
                dictionary.Add(key, value);
                return true;
            }
            // The argument exception is thrown only if the key already exists
            catch (ArgumentException) { return false; }
        }
    }
}
