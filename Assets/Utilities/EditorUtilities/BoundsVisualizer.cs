using System.Collections.Generic;
using UnityEngine;

public class BoundsVisualizer : MonoBehaviour
{
    public BoundsSource boundsSource;
    public Color gizmosColor = Color.black; // Allows customization of the Gizmos color

    public enum BoundsSource
    {
        MeshFilter,
        Collider
    }

    private MeshFilter _meshFilter;
    private MeshFilter meshFilter
    {
        get
        {
            if (_meshFilter == null) TryGetComponent<MeshFilter>(out _meshFilter);
            return _meshFilter;
        }
    }

    private Collider _col;
    private Collider col
    {
        get
        {
            if (_col == null) TryGetComponent<Collider>(out _col);
            return _col;
        }
    }

    private void OnDrawGizmos()
    {
        // Initialize bounds to an empty bounds structure
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        bool hasBounds = false;

        switch (boundsSource)
        {
            case BoundsSource.MeshFilter:
                if (meshFilter != null && meshFilter.sharedMesh != null)
                {
                    bounds = meshFilter.sharedMesh.bounds;
                    bounds = new Bounds(transform.TransformPoint(bounds.center), Vector3.Scale(bounds.size, transform.lossyScale)); // Adjust bounds to GameObject's transform
                    hasBounds = true;
                }
                break;
            case BoundsSource.Collider:
                if (col != null)
                {
                    bounds = col.bounds; // Collider bounds are already in world space
                    hasBounds = true;
                }
                break;
        }

        if (hasBounds)
        {
            Gizmos.color = gizmosColor; // Use the customizable color for Gizmos

            Vector3 center = bounds.center;
            Vector3 size = bounds.size;

            // Calculate the half extents of the bounds
            Vector3 extents = size * 0.5f;
            Vector3 frontTopLeft = center + new Vector3(-extents.x, extents.y, -extents.z);
            Vector3 frontTopRight = center + new Vector3(extents.x, extents.y, -extents.z);
            Vector3 frontBottomLeft = center + new Vector3(-extents.x, -extents.y, -extents.z);
            Vector3 frontBottomRight = center + new Vector3(extents.x, -extents.y, -extents.z);
            Vector3 backTopLeft = center + new Vector3(-extents.x, extents.y, extents.z);
            Vector3 backTopRight = center + new Vector3(extents.x, extents.y, extents.z);
            Vector3 backBottomLeft = center + new Vector3(-extents.x, -extents.y, extents.z);
            Vector3 backBottomRight = center + new Vector3(extents.x, -extents.y, extents.z);

            // Draw lines between the calculated corners to form the box
            Gizmos.DrawLine(frontTopLeft, frontTopRight);
            Gizmos.DrawLine(frontTopRight, frontBottomRight);
            Gizmos.DrawLine(frontBottomRight, frontBottomLeft);
            Gizmos.DrawLine(frontBottomLeft, frontTopLeft);

            Gizmos.DrawLine(backTopLeft, backTopRight);
            Gizmos.DrawLine(backTopRight, backBottomRight);
            Gizmos.DrawLine(backBottomRight, backBottomLeft);
            Gizmos.DrawLine(backBottomLeft, backTopLeft);

            Gizmos.DrawLine(frontTopLeft, backTopLeft);
            Gizmos.DrawLine(frontTopRight, backTopRight);
            Gizmos.DrawLine(frontBottomRight, backBottomRight);
            Gizmos.DrawLine(frontBottomLeft, backBottomLeft);
        }
    }
}