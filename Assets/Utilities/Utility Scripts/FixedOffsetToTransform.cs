using UnityEngine;

public class FixedOffsetToTransform : MonoBehaviour
{
    public Transform _targetTransform;
    private Transform targetTransform
    {
        get
        {
            if (_targetTransform == null) _targetTransform = transform.parent;
            return _targetTransform;
        }
    }

    public Vector3 worldPositionOffset;
    public Vector3 localPositionOffset;

    void Update()
    {
        if (targetTransform == null) return;
        Vector3 newWorldPosition = targetTransform.position + worldPositionOffset + targetTransform.rotation * Vector3.Scale(localPositionOffset, targetTransform.lossyScale);
        targetTransform.position = newWorldPosition;
    }

}
