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

    public bool backwards = false;
    public bool perpendicularToFloor = false;

    void Update()
    {
        Vector3 lookDirection = (mainCam.position - transform.position).normalized;
        if (backwards) lookDirection = -lookDirection;
        if (perpendicularToFloor)
        {
            lookDirection.y = 0;
        }
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
