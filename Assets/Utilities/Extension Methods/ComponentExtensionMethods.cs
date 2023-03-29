using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ExtensionMethods
{

    public static class ComponentExtensionMethods
    {
        public static T AddComponent<T>(this Component component) where T : Component
        {
            return component.gameObject.AddComponent<T>();
        }
        public static Component AddComponent(this Component component, Type t)
        {
            return component.gameObject.AddComponent(t);
        }

        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            return component.GetComponent<T>() ?? component.AddComponent<T>();
        }
        public static Component GetOrAddComponent(this Component component, Type t)
        {
            return component.GetComponent(t) ?? component.AddComponent(t);
        }

        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() != null;
        }
    }
}
