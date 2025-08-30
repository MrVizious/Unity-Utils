using System.Collections.Generic;
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
        public static void LocalReset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
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
        public static T[] GetComponentsInChildrenExcludingParent<T>(this Transform transform) where T : Component
        {
            return transform.gameObject.GetComponentsInChildrenExcludingParent<T>();
        }
        public static T GetComponentInChildrenExcludingParent<T>(this Transform transform) where T : Component
        {
            return transform.gameObject.GetComponentInChildrenExcludingParent<T>();
        }
        public static T[] GetComponentsInDirectChildren<T>(this Transform transform, bool includeInactive = true) where T : Component
        {
            List<T> childrenComponents = new List<T>();
            foreach (Transform child in transform)
            {

                T childComponent = child.GetComponent<T>();
                if (childComponent != null) childrenComponents.Add(childComponent);
            }
            return childrenComponents.ToArray();
        }

        public static Transform CopyValuesFrom(this Transform destination, Transform from)
        {
            destination.position = from.position;
            destination.rotation = from.rotation;
            destination.localScale = from.localScale;
            return destination;
        }


        public static Quaternion GetAverageRotation(IEnumerable<Transform> transforms)
        {
            int count = 0;
            Quaternion rotationSum = new Quaternion(0, 0, 0, 0);
            Quaternion referenceRotation = Quaternion.identity;
            bool hasReference = false;

            foreach (var data in transforms)
            {
                if (!hasReference)
                {
                    referenceRotation = data.rotation;
                    hasReference = true;
                }

                // Align quaternion signs to avoid cancellation
                var rot = data.rotation;
                if (Quaternion.Dot(rot, referenceRotation) < 0f)
                    rot = new Quaternion(-rot.x, -rot.y, -rot.z, -rot.w);

                rotationSum.x += rot.x;
                rotationSum.y += rot.y;
                rotationSum.z += rot.z;
                rotationSum.w += rot.w;

                count++;
            }

            if (count == 0)
                return Quaternion.identity;

            Quaternion avgRotation = rotationSum.normalized;
            return avgRotation;


        }
    }
}