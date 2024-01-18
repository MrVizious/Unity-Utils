#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UtilityMethods
{
    public static class HandlesMethods
    {
        public static void DrawAALine(Vector3 pA, Vector3 pB, float thickness = 1f)
        {
            Vector3[] vectors = { pA, pB };
            Handles.DrawAAPolyLine(thickness, vectors);
        }
    }
}
#endif