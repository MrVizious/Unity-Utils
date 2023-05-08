using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class CollliderExtensionMethods
    {
        public static bool IsPointInside(this Collider collider, Vector3 point)
        {
            return (collider.ClosestPoint(point) - point).sqrMagnitude < Mathf.Epsilon * Mathf.Epsilon;
        }
        public static bool IsPointInside(this Collider2D collider, Vector2 point)
        {
            return (collider.ClosestPoint(point) - point).sqrMagnitude < Mathf.Epsilon * Mathf.Epsilon;
        }
        public static bool LayerIsInside(this Collider collider, LayerMask layerMask)
        {
            return layerMask == (1 << collider.gameObject.layer);
        }
        public static bool LayerIsInside(this Collider2D collider, LayerMask layerMask)
        {
            return layerMask == (1 << collider.gameObject.layer);
        }
    }
}
