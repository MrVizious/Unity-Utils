using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _mainCam;
    private Transform mainCam
    {
        get
        {
            if (_mainCam == null) _mainCam = Camera.main.transform;
            return _mainCam;
        }
    }

    [Header("Rotation Settings")]
    public bool backwards = false;
    public bool perpendicularToFloor = false;

    [Tooltip("Speed of the slerp rotation. Higher = faster.")]
    public float lerpSpeed = 5f;

    void Update()
    {
        Vector3 lookDirection = (mainCam.position - transform.position).normalized;

        if (backwards)
            lookDirection = -lookDirection;

        if (perpendicularToFloor)
            lookDirection.y = 0;

        if (lookDirection.sqrMagnitude > 0.0001f) // Prevent zero direction
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lerpSpeed);
        }
    }
}
