using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
using System;

public class BoundsVisualizer : MonoBehaviour
{
    public bool debug = true;
    public BoundsSource boundsSource;
    public Color gizmosColor = Color.black; // Allows customization of the Gizmos color

    public enum BoundsSource
    {
        MeshFilter,
        RendererLocal,
        RendererWorld,
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

    private Renderer _rend;
    private Renderer rend
    {
        get
        {
            if (_rend == null) TryGetComponent<Renderer>(out _rend);
            return _rend;
        }
    }

    private void OnDrawGizmos()
    {
        if (!debug) return;
        // Initialize bounds to an empty bounds structure
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        bool hasBounds = false;

        switch (boundsSource)
        {
            case BoundsSource.MeshFilter:
                if (meshFilter != null && meshFilter.mesh != null)
                {
                    bounds = meshFilter.sharedMesh.bounds;
                    hasBounds = true;
                }
                break;
            case BoundsSource.RendererLocal:
                if (rend != null)
                {
                    bounds = rend.localBounds; // Renderer bounds are already in world space
                    hasBounds = true;
                }
                break;
            case BoundsSource.RendererWorld:
                if (rend != null)
                {
                    bounds = rend.bounds; // Renderer bounds are already in world space
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
            BoundsExtensionMethods.BoundsSpace boundsSpace;
            switch (boundsSource)
            {
                case BoundsSource.RendererLocal:
                case BoundsSource.MeshFilter:
                    boundsSpace = BoundsExtensionMethods.BoundsSpace.Local;
                    break;
                case BoundsSource.RendererWorld:
                case BoundsSource.Collider:
                    boundsSpace = BoundsExtensionMethods.BoundsSpace.World;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("BoundsSource is not valid. " + nameof(boundsSource));
            }
            bounds.VisualizeInWorldSpace(gizmosColor, boundsSpace, transform);
        }
    }
}