using UnityEngine;

namespace ExtensionMethods
{
    public static class GameObjectExtensionMethods
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : MonoBehaviour
        {
            var component = gameObject.GetComponent<T>();
            if (component == null) gameObject.AddComponent<T>();
            return component;
        }

        public static bool HasComponent<T>(this GameObject gameObject) where T : MonoBehaviour
        {
            return gameObject.GetComponent<T>() != null;
        }

        public static void DestroyChildren(this GameObject gameObject)
        {
            for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(gameObject.transform.GetChild(i).gameObject);
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