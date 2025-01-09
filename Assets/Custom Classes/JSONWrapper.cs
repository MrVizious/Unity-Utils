using System;
using System.Collections.Generic;
using UnityEngine;

public static class JSONWrapper
{
    /// <summary>
    /// Deserializes a JSON string into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of object to deserialize into.</typeparam>
    /// <param name="json">The JSON string.</param>
    /// <returns>An object of type T.</returns>
    public static T FromJson<T>(string json)
    {
        try
        {
            return JsonUtility.FromJson<T>(json);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error deserializing JSON to object: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Deserializes a JSON string into a list of objects of type T.
    /// </summary>
    /// <typeparam name="T">The type of objects in the list.</typeparam>
    /// <param name="json">The JSON string.</param>
    /// <returns>A list of objects of type T.</returns>
    public static List<T> FromJsonList<T>(string json)
    {
        try
        {
            // Wrapping the JSON to make it compatible with JsonUtility
            string wrappedJson = $"{{\"Items\":{json}}}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(wrappedJson);
            return wrapper.Items;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error deserializing JSON to list: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Serializes an object of type T into a JSON string.
    /// </summary>
    /// <typeparam name="T">The type of object to serialize.</typeparam>
    /// <param name="obj">The object to serialize.</param>
    /// <returns>A JSON string representation of the object.</returns>
    public static string ToJson<T>(T obj)
    {
        try
        {
            return JsonUtility.ToJson(obj);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error serializing object to JSON: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Wrapper class to deserialize lists using JsonUtility.
    /// </summary>
    /// <typeparam name="T">The type of objects in the list.</typeparam>
    [Serializable]
    private class Wrapper<T>
    {
        public List<T> Items;
    }
}