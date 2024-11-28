#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using Scaffolding;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;

namespace Scaffolding
{

    [RequireComponent(typeof(RectTransform))]
    [ExecuteInEditMode]
    public class Scaffold : SerializedMonoBehaviour
    {
        public bool showScaffold = true;
        // The color to tint the RectTransform in the editor
        [SerializeField]
        private Color tintColor;

        private RectTransform _rectTransform;
        private RectTransform rectTransform
        {
            get
            {
                if (_rectTransform == null) TryGetComponent<RectTransform>(out _rectTransform);
                return _rectTransform;
            }
        }

        [Button]
        public void RandomizeColor()
        {
            tintColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 0.25f);
        }
        private void OnDrawGizmos()
        {
            if (!showScaffold) return;
            if (rectTransform == null)
                return;

            // Get the four corners of the RectTransform in world space
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            // Draw the filled rectangle using Handles
            Handles.BeginGUI();

            // Convert world points to GUI points for the Scene view
            Vector3 screenPos1 = HandleUtility.WorldToGUIPoint(corners[0]);
            Vector3 screenPos2 = HandleUtility.WorldToGUIPoint(corners[1]);
            Vector3 screenPos3 = HandleUtility.WorldToGUIPoint(corners[2]);
            Vector3 screenPos4 = HandleUtility.WorldToGUIPoint(corners[3]);

            // Create a polygon shape in the Scene view using GUI coordinates
            Vector3[] guiPoints = new Vector3[]
            {
            screenPos1,
            screenPos2,
            screenPos3,
            screenPos4
            };

            // Define the color for the filled area
            Handles.color = tintColor;

            // Draw the filled rectangle
            Handles.DrawAAConvexPolygon(guiPoints);

            // Draw the outline of the RectTransform (border lines)
            Gizmos.color = tintColor.WithAlpha(tintColor.a * 3f);
            Gizmos.DrawLine(corners[0], corners[1]); // Left side
            Gizmos.DrawLine(corners[1], corners[2]); // Top side
            Gizmos.DrawLine(corners[2], corners[3]); // Right side
            Gizmos.DrawLine(corners[3], corners[0]); // Bottom side

            // Draw the crossing lines (diagonals)
            Gizmos.DrawLine(corners[0], corners[2]); // Diagonal from bottom-left to top-right
            Gizmos.DrawLine(corners[1], corners[3]); // Diagonal from top-left to bottom-right
            Handles.EndGUI();
        }

        private void Awake()
        {
            RandomizeColor();
        }
    }
}
#endif