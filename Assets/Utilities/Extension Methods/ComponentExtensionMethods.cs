using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{

    public static class ComponentExtensionMethods
    {
        public static T AddComponent<T>(this Component component) where T : Component
        {
            return component.gameObject.AddComponent<T>();
        }
        public static T GetOrAddComponent<T>(this Component component) where T : MonoBehaviour
        {
            return component.GetComponent<T>() ?? component.AddComponent<T>();
        }
        public static bool HasComponent<T>(this GameObject gameObject) where T : MonoBehaviour
        {
            return gameObject.GetComponent<T>() != null;
        }
    }
}
