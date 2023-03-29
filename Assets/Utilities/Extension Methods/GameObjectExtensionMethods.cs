using UnityEngine;
using System;

namespace ExtensionMethods
{
    public static class GameObjectExtensionMethods
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Behaviour
        {
            var component = gameObject.GetComponent<T>();
            if (component == null) gameObject.AddComponent<T>();
            return component;
        }

        public static Component GetOrAddComponent(this GameObject gameObject, Type t)
        {
            var component = gameObject.GetComponent(t);
            if (component == null) gameObject.AddComponent(t);
            return component;
        }

        public static bool HasComponent<T>(this GameObject gameObject) where T : Behaviour
        {
            return gameObject.GetComponent<T>() != null;
        }
        public static bool HasComponent(this GameObject gameObject, Type t)
        {
            return gameObject.GetComponent(t) != null;
        }

        public static void DestroyChildren(this GameObject gameObject)
        {
            for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(gameObject.transform.GetChild(i).gameObject);
            }
        }

        public static void SetChildrenActive(this GameObject gameObject, bool newValue)
        {
            for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(newValue);
            }
        }
    }
}