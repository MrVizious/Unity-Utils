using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class TransformGuidedLineRenderer : SerializedMonoBehaviour
{
    private LineRenderer _lineRenderer;
    private LineRenderer lineRenderer
    {
        get
        {
            if (_lineRenderer == null) TryGetComponent<LineRenderer>(out _lineRenderer);
            return _lineRenderer;
        }
    }

    public List<Transform> transforms = new List<Transform>();

    private void Awake()
    {
        lineRenderer.positionCount = 0;
    }
    private void LateUpdate()
    {
        UpdatePositions();
    }
    private void OnDrawGizmos()
    {
        if (Application.isPlaying) return;
        UpdatePositions();
    }
    private void UpdatePositions()
    {
        Vector3[] positions = new Vector3[transforms.Count];
        for (int i = 0; i < transforms.Count; i++)
        {
            positions[i] = transforms[i].position;
        }
        lineRenderer.positionCount = transforms.Count;
        lineRenderer.SetPositions(positions);
    }

    private void OnEnable()
    {
        lineRenderer.enabled = true;
    }
    private void OnDisable()
    {
        lineRenderer.enabled = false;
    }

}
