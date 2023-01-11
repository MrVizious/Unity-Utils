using UnityEngine;

namespace ExtensionMethods
{

    public static class TransformExtensionMethods
    {
        public static void DestroyChildren(this Transform transform)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(transform.GetChild(i).gameObject);
            }
        }

        public static void Reset(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public static void SetChildrenActive(this Transform transform, bool newValue)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                transform.GetChild(i).gameObject.SetActive(newValue);
            }
        }
    }
}