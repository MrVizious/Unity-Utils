using Sirenix.OdinInspector;
using UnityEngine;

public class TransformFollower : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Follow Toggles")]
    public bool followPosition = true;
    public bool followRotation = true;
    public bool followScale = true;

    [Header("Offsets")]
    public Vector3 worldPositionOffset = Vector3.zero;
    public Vector3 localPositionOffset = Vector3.zero;

    public Vector3 worldRotationOffset = Vector3.zero; // in degrees
    public Vector3 localRotationOffset = Vector3.zero;

    public Vector3 worldScaleOffset = Vector3.zero;
    public Vector3 localScaleOffset = Vector3.zero;

    [Header("Interpolation")]
    [ShowIf("followPosition")]
    [Range(0f, 1f)] public float positionLerpSpeed = 0.1f;
    [ShowIf("followRotation")]
    [Range(0f, 1f)] public float rotationLerpSpeed = 0.1f;
    [ShowIf("followScale")]
    [Range(0f, 1f)] public float scaleLerpSpeed = 0.1f;

    void LateUpdate()
    {
        if (target == null) return;

        // ----- POSITION -----
        Vector3 targetPosition = target.position + worldPositionOffset;
        targetPosition += target.TransformDirection(localPositionOffset); // apply local offset in world space

        if (followPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, positionLerpSpeed);
        }
        else
        {
            transform.position = targetPosition;
        }

        // ----- ROTATION -----
        Quaternion targetRotation = target.rotation;

        if (worldRotationOffset != Vector3.zero)
            targetRotation *= Quaternion.Euler(worldRotationOffset);

        if (localRotationOffset != Vector3.zero)
            targetRotation *= Quaternion.Euler(localRotationOffset); // applied in target's local space

        if (followRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationLerpSpeed);
        }
        else
        {
            transform.rotation = targetRotation;
        }

        // ----- SCALE -----
        Vector3 baseScale = target.localScale;
        Vector3 finalScale = baseScale + worldScaleOffset + localScaleOffset;

        if (followScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, finalScale, scaleLerpSpeed);
        }
        else
        {
            transform.localScale = finalScale;
        }

    }
}
