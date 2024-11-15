using UnityEngine;
using System;
using System.Collections.Generic;

namespace ExtensionMethods
{
    public static class GameObjectExtensionMethods
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null) component = gameObject.AddComponent<T>();
            return component;
        }

        public static Component GetOrAddComponent(this GameObject gameObject, Type t)
        {
            var component = gameObject.GetComponent(t);
            if (component == null) component = gameObject.AddComponent(t);
            return component;
        }

        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
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

        public static T[] GetComponentsInChildrenExcludingParent<T>(this GameObject gameObject) where T : Component
        {
            T[] allComponents = gameObject.GetComponentsInChildren<T>();
            List<T> listedComponents = new List<T>(allComponents);

            for (int i = listedComponents.Count - 1; i >= 0; i--)
            {
                if (listedComponents[i].gameObject == gameObject) listedComponents.Remove(listedComponents[i]);
            }

            return listedComponents.ToArray();
        }

        public static T GetComponentInChildrenExcludingParent<T>(this GameObject gameObject) where T : Component
        {
            T[] components = gameObject.GetComponentsInChildrenExcludingParent<T>();
            if (components.Length > 0) return components[0];
            return null;
        }

        public static T[] GetComponentsInDirectChildren<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.transform.GetComponentsInDirectChildren<T>();
        }
    }
}